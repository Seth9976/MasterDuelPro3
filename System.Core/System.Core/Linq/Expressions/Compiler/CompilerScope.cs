using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020000E1 RID: 225
	internal sealed class CompilerScope
	{
		// Token: 0x0600076D RID: 1901 RVA: 0x000183CC File Offset: 0x000165CC
		internal CompilerScope(object node, bool isMethod)
		{
			this.Node = node;
			this.IsMethod = isMethod;
			IReadOnlyList<ParameterExpression> variables = CompilerScope.GetVariables(node);
			this.Definitions = new Dictionary<ParameterExpression, VariableStorageKind>(variables.Count);
			foreach (ParameterExpression parameterExpression in variables)
			{
				this.Definitions.Add(parameterExpression, VariableStorageKind.Local);
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0001845C File Offset: 0x0001665C
		internal HoistedLocals NearestHoistedLocals
		{
			get
			{
				return this._hoistedLocals ?? this._closureHoistedLocals;
			}
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00018470 File Offset: 0x00016670
		internal CompilerScope Enter(LambdaCompiler lc, CompilerScope parent)
		{
			this.SetParent(lc, parent);
			this.AllocateLocals(lc);
			if (this.IsMethod && this._closureHoistedLocals != null)
			{
				this.EmitClosureAccess(lc, this._closureHoistedLocals);
			}
			this.EmitNewHoistedLocals(lc);
			if (this.IsMethod)
			{
				this.EmitCachedVariables();
			}
			return this;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000184C0 File Offset: 0x000166C0
		internal CompilerScope Exit()
		{
			if (!this.IsMethod)
			{
				foreach (CompilerScope.Storage storage in this._locals.Values)
				{
					storage.FreeLocal();
				}
			}
			CompilerScope parent = this._parent;
			this._parent = null;
			this._hoistedLocals = null;
			this._closureHoistedLocals = null;
			this._locals.Clear();
			return parent;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00018544 File Offset: 0x00016744
		internal void EmitVariableAccess(LambdaCompiler lc, ReadOnlyCollection<ParameterExpression> vars)
		{
			if (this.NearestHoistedLocals != null && vars.Count > 0)
			{
				global::System.Collections.Generic.ArrayBuilder<long> arrayBuilder = new global::System.Collections.Generic.ArrayBuilder<long>(vars.Count);
				foreach (ParameterExpression parameterExpression in vars)
				{
					ulong num = 0UL;
					HoistedLocals hoistedLocals = this.NearestHoistedLocals;
					while (!hoistedLocals.Indexes.ContainsKey(parameterExpression))
					{
						num += 1UL;
						hoistedLocals = hoistedLocals.Parent;
					}
					ulong num2 = (num << 32) | (ulong)hoistedLocals.Indexes[parameterExpression];
					arrayBuilder.UncheckedAdd((long)num2);
				}
				this.EmitGet(this.NearestHoistedLocals.SelfVariable);
				lc.EmitConstantArray<long>(arrayBuilder.ToArray());
				lc.IL.Emit(OpCodes.Call, CachedReflectionInfo.RuntimeOps_CreateRuntimeVariables_ObjectArray_Int64Array);
				return;
			}
			lc.IL.Emit(OpCodes.Call, CachedReflectionInfo.RuntimeOps_CreateRuntimeVariables);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001863C File Offset: 0x0001683C
		internal void AddLocal(LambdaCompiler gen, ParameterExpression variable)
		{
			this._locals.Add(variable, new CompilerScope.LocalStorage(gen, variable));
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00018651 File Offset: 0x00016851
		internal void EmitGet(ParameterExpression variable)
		{
			this.ResolveVariable(variable).EmitLoad();
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001865F File Offset: 0x0001685F
		internal void EmitSet(ParameterExpression variable)
		{
			this.ResolveVariable(variable).EmitStore();
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001866D File Offset: 0x0001686D
		internal void EmitAddressOf(ParameterExpression variable)
		{
			this.ResolveVariable(variable).EmitAddress();
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001867B File Offset: 0x0001687B
		private CompilerScope.Storage ResolveVariable(ParameterExpression variable)
		{
			return this.ResolveVariable(variable, this.NearestHoistedLocals);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001868C File Offset: 0x0001688C
		private CompilerScope.Storage ResolveVariable(ParameterExpression variable, HoistedLocals hoistedLocals)
		{
			for (CompilerScope compilerScope = this; compilerScope != null; compilerScope = compilerScope._parent)
			{
				CompilerScope.Storage storage;
				if (compilerScope._locals.TryGetValue(variable, out storage))
				{
					return storage;
				}
				if (compilerScope.IsMethod)
				{
					break;
				}
			}
			for (HoistedLocals hoistedLocals2 = hoistedLocals; hoistedLocals2 != null; hoistedLocals2 = hoistedLocals2.Parent)
			{
				int num;
				if (hoistedLocals2.Indexes.TryGetValue(variable, out num))
				{
					return new CompilerScope.ElementBoxStorage(this.ResolveVariable(hoistedLocals2.SelfVariable, hoistedLocals), num, variable);
				}
			}
			throw Error.UndefinedVariable(variable.Name, variable.Type, this.CurrentLambdaName);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001870C File Offset: 0x0001690C
		private void SetParent(LambdaCompiler lc, CompilerScope parent)
		{
			this._parent = parent;
			if (this.NeedsClosure && this._parent != null)
			{
				this._closureHoistedLocals = this._parent.NearestHoistedLocals;
			}
			ReadOnlyCollection<ParameterExpression> readOnlyCollection = (from p in this.GetVariables()
				where this.Definitions[p] == VariableStorageKind.Hoisted
				select p).ToReadOnly<ParameterExpression>();
			if (readOnlyCollection.Count > 0)
			{
				this._hoistedLocals = new HoistedLocals(this._closureHoistedLocals, readOnlyCollection);
				this.AddLocal(lc, this._hoistedLocals.SelfVariable);
			}
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001878C File Offset: 0x0001698C
		private void EmitNewHoistedLocals(LambdaCompiler lc)
		{
			if (this._hoistedLocals == null)
			{
				return;
			}
			lc.IL.EmitPrimitive(this._hoistedLocals.Variables.Count);
			lc.IL.Emit(OpCodes.Newarr, typeof(object));
			int num = 0;
			foreach (ParameterExpression parameterExpression in this._hoistedLocals.Variables)
			{
				lc.IL.Emit(OpCodes.Dup);
				lc.IL.EmitPrimitive(num++);
				Type type = typeof(StrongBox<>).MakeGenericType(new Type[] { parameterExpression.Type });
				int num2;
				if (this.IsMethod && (num2 = lc.Parameters.IndexOf(parameterExpression)) >= 0)
				{
					lc.EmitLambdaArgument(num2);
					lc.IL.Emit(OpCodes.Newobj, type.GetConstructor(new Type[] { parameterExpression.Type }));
				}
				else if (parameterExpression == this._hoistedLocals.ParentVariable)
				{
					this.ResolveVariable(parameterExpression, this._closureHoistedLocals).EmitLoad();
					lc.IL.Emit(OpCodes.Newobj, type.GetConstructor(new Type[] { parameterExpression.Type }));
				}
				else
				{
					lc.IL.Emit(OpCodes.Newobj, type.GetConstructor(Type.EmptyTypes));
				}
				if (this.ShouldCache(parameterExpression))
				{
					lc.IL.Emit(OpCodes.Dup);
					this.CacheBoxToLocal(lc, parameterExpression);
				}
				lc.IL.Emit(OpCodes.Stelem_Ref);
			}
			this.EmitSet(this._hoistedLocals.SelfVariable);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00018958 File Offset: 0x00016B58
		private void EmitCachedVariables()
		{
			if (this.ReferenceCount == null)
			{
				return;
			}
			foreach (KeyValuePair<ParameterExpression, int> keyValuePair in this.ReferenceCount)
			{
				if (this.ShouldCache(keyValuePair.Key, keyValuePair.Value))
				{
					CompilerScope.ElementBoxStorage elementBoxStorage = this.ResolveVariable(keyValuePair.Key) as CompilerScope.ElementBoxStorage;
					if (elementBoxStorage != null)
					{
						elementBoxStorage.EmitLoadBox();
						this.CacheBoxToLocal(elementBoxStorage.Compiler, keyValuePair.Key);
					}
				}
			}
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x000189F4 File Offset: 0x00016BF4
		private bool ShouldCache(ParameterExpression v, int refCount)
		{
			return refCount > 2 && !this._locals.ContainsKey(v);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00018A0C File Offset: 0x00016C0C
		private bool ShouldCache(ParameterExpression v)
		{
			int num;
			return this.ReferenceCount != null && this.ReferenceCount.TryGetValue(v, out num) && this.ShouldCache(v, num);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00018A40 File Offset: 0x00016C40
		private void CacheBoxToLocal(LambdaCompiler lc, ParameterExpression v)
		{
			CompilerScope.LocalBoxStorage localBoxStorage = new CompilerScope.LocalBoxStorage(lc, v);
			localBoxStorage.EmitStoreBox();
			this._locals.Add(v, localBoxStorage);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00018A68 File Offset: 0x00016C68
		private void EmitClosureAccess(LambdaCompiler lc, HoistedLocals locals)
		{
			if (locals == null)
			{
				return;
			}
			this.EmitClosureToVariable(lc, locals);
			while ((locals = locals.Parent) != null)
			{
				ParameterExpression selfVariable = locals.SelfVariable;
				CompilerScope.LocalStorage localStorage = new CompilerScope.LocalStorage(lc, selfVariable);
				localStorage.EmitStore(this.ResolveVariable(selfVariable));
				this._locals.Add(selfVariable, localStorage);
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00018AB7 File Offset: 0x00016CB7
		private void EmitClosureToVariable(LambdaCompiler lc, HoistedLocals locals)
		{
			lc.EmitClosureArgument();
			lc.IL.Emit(OpCodes.Ldfld, CachedReflectionInfo.Closure_Locals);
			this.AddLocal(lc, locals.SelfVariable);
			this.EmitSet(locals.SelfVariable);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00018AF0 File Offset: 0x00016CF0
		private void AllocateLocals(LambdaCompiler lc)
		{
			foreach (ParameterExpression parameterExpression in this.GetVariables())
			{
				if (this.Definitions[parameterExpression] == VariableStorageKind.Local)
				{
					CompilerScope.Storage storage;
					if (this.IsMethod && lc.Parameters.Contains(parameterExpression))
					{
						storage = new CompilerScope.ArgumentStorage(lc, parameterExpression);
					}
					else
					{
						storage = new CompilerScope.LocalStorage(lc, parameterExpression);
					}
					this._locals.Add(parameterExpression, storage);
				}
			}
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00018B7C File Offset: 0x00016D7C
		private IEnumerable<ParameterExpression> GetVariables()
		{
			if (this.MergedScopes != null)
			{
				return this.GetVariablesIncludingMerged();
			}
			return CompilerScope.GetVariables(this.Node);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00018BA5 File Offset: 0x00016DA5
		private IEnumerable<ParameterExpression> GetVariablesIncludingMerged()
		{
			foreach (ParameterExpression parameterExpression in CompilerScope.GetVariables(this.Node))
			{
				yield return parameterExpression;
			}
			IEnumerator<ParameterExpression> enumerator = null;
			foreach (BlockExpression blockExpression in this.MergedScopes)
			{
				foreach (ParameterExpression parameterExpression2 in blockExpression.Variables)
				{
					yield return parameterExpression2;
				}
				enumerator = null;
			}
			HashSet<BlockExpression>.Enumerator enumerator2 = default(HashSet<BlockExpression>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00018BB8 File Offset: 0x00016DB8
		private static IReadOnlyList<ParameterExpression> GetVariables(object scope)
		{
			LambdaExpression lambdaExpression = scope as LambdaExpression;
			if (lambdaExpression != null)
			{
				return new ParameterList(lambdaExpression);
			}
			BlockExpression blockExpression = scope as BlockExpression;
			if (blockExpression != null)
			{
				return blockExpression.Variables;
			}
			return new ParameterExpression[] { ((CatchBlock)scope).Variable };
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00018BFC File Offset: 0x00016DFC
		private string CurrentLambdaName
		{
			get
			{
				for (CompilerScope compilerScope = this; compilerScope != null; compilerScope = compilerScope._parent)
				{
					LambdaExpression lambdaExpression = compilerScope.Node as LambdaExpression;
					if (lambdaExpression != null)
					{
						return lambdaExpression.Name;
					}
				}
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x0400023E RID: 574
		private CompilerScope _parent;

		// Token: 0x0400023F RID: 575
		internal readonly object Node;

		// Token: 0x04000240 RID: 576
		internal readonly bool IsMethod;

		// Token: 0x04000241 RID: 577
		internal bool NeedsClosure;

		// Token: 0x04000242 RID: 578
		internal readonly Dictionary<ParameterExpression, VariableStorageKind> Definitions = new Dictionary<ParameterExpression, VariableStorageKind>();

		// Token: 0x04000243 RID: 579
		internal Dictionary<ParameterExpression, int> ReferenceCount;

		// Token: 0x04000244 RID: 580
		internal HashSet<BlockExpression> MergedScopes;

		// Token: 0x04000245 RID: 581
		private HoistedLocals _hoistedLocals;

		// Token: 0x04000246 RID: 582
		private HoistedLocals _closureHoistedLocals;

		// Token: 0x04000247 RID: 583
		private readonly Dictionary<ParameterExpression, CompilerScope.Storage> _locals = new Dictionary<ParameterExpression, CompilerScope.Storage>();

		// Token: 0x020000E2 RID: 226
		private abstract class Storage
		{
			// Token: 0x06000786 RID: 1926 RVA: 0x00018C43 File Offset: 0x00016E43
			internal Storage(LambdaCompiler compiler, ParameterExpression variable)
			{
				this.Compiler = compiler;
				this.Variable = variable;
			}

			// Token: 0x06000787 RID: 1927
			internal abstract void EmitLoad();

			// Token: 0x06000788 RID: 1928
			internal abstract void EmitAddress();

			// Token: 0x06000789 RID: 1929
			internal abstract void EmitStore();

			// Token: 0x0600078A RID: 1930 RVA: 0x00018C59 File Offset: 0x00016E59
			internal virtual void EmitStore(CompilerScope.Storage value)
			{
				value.EmitLoad();
				this.EmitStore();
			}

			// Token: 0x0600078B RID: 1931 RVA: 0x0000A01D File Offset: 0x0000821D
			internal virtual void FreeLocal()
			{
			}

			// Token: 0x04000248 RID: 584
			internal readonly LambdaCompiler Compiler;

			// Token: 0x04000249 RID: 585
			internal readonly ParameterExpression Variable;
		}

		// Token: 0x020000E3 RID: 227
		private sealed class LocalStorage : CompilerScope.Storage
		{
			// Token: 0x0600078C RID: 1932 RVA: 0x00018C67 File Offset: 0x00016E67
			internal LocalStorage(LambdaCompiler compiler, ParameterExpression variable)
				: base(compiler, variable)
			{
				this._local = compiler.GetLocal(variable.IsByRef ? variable.Type.MakeByRefType() : variable.Type);
			}

			// Token: 0x0600078D RID: 1933 RVA: 0x00018C98 File Offset: 0x00016E98
			internal override void EmitLoad()
			{
				this.Compiler.IL.Emit(OpCodes.Ldloc, this._local);
			}

			// Token: 0x0600078E RID: 1934 RVA: 0x00018CB5 File Offset: 0x00016EB5
			internal override void EmitStore()
			{
				this.Compiler.IL.Emit(OpCodes.Stloc, this._local);
			}

			// Token: 0x0600078F RID: 1935 RVA: 0x00018CD2 File Offset: 0x00016ED2
			internal override void EmitAddress()
			{
				this.Compiler.IL.Emit(OpCodes.Ldloca, this._local);
			}

			// Token: 0x06000790 RID: 1936 RVA: 0x00018CEF File Offset: 0x00016EEF
			internal override void FreeLocal()
			{
				this.Compiler.FreeLocal(this._local);
			}

			// Token: 0x0400024A RID: 586
			private readonly LocalBuilder _local;
		}

		// Token: 0x020000E4 RID: 228
		private sealed class ArgumentStorage : CompilerScope.Storage
		{
			// Token: 0x06000791 RID: 1937 RVA: 0x00018D02 File Offset: 0x00016F02
			internal ArgumentStorage(LambdaCompiler compiler, ParameterExpression p)
				: base(compiler, p)
			{
				this._argument = compiler.GetLambdaArgument(compiler.Parameters.IndexOf(p));
			}

			// Token: 0x06000792 RID: 1938 RVA: 0x00018D24 File Offset: 0x00016F24
			internal override void EmitLoad()
			{
				this.Compiler.IL.EmitLoadArg(this._argument);
			}

			// Token: 0x06000793 RID: 1939 RVA: 0x00018D3C File Offset: 0x00016F3C
			internal override void EmitStore()
			{
				this.Compiler.IL.EmitStoreArg(this._argument);
			}

			// Token: 0x06000794 RID: 1940 RVA: 0x00018D54 File Offset: 0x00016F54
			internal override void EmitAddress()
			{
				this.Compiler.IL.EmitLoadArgAddress(this._argument);
			}

			// Token: 0x0400024B RID: 587
			private readonly int _argument;
		}

		// Token: 0x020000E5 RID: 229
		private sealed class ElementBoxStorage : CompilerScope.Storage
		{
			// Token: 0x06000795 RID: 1941 RVA: 0x00018D6C File Offset: 0x00016F6C
			internal ElementBoxStorage(CompilerScope.Storage array, int index, ParameterExpression variable)
				: base(array.Compiler, variable)
			{
				this._array = array;
				this._index = index;
				this._boxType = typeof(StrongBox<>).MakeGenericType(new Type[] { variable.Type });
				this._boxValueField = this._boxType.GetField("Value");
			}

			// Token: 0x06000796 RID: 1942 RVA: 0x00018DCE File Offset: 0x00016FCE
			internal override void EmitLoad()
			{
				this.EmitLoadBox();
				this.Compiler.IL.Emit(OpCodes.Ldfld, this._boxValueField);
			}

			// Token: 0x06000797 RID: 1943 RVA: 0x00018DF4 File Offset: 0x00016FF4
			internal override void EmitStore()
			{
				LocalBuilder local = this.Compiler.GetLocal(this.Variable.Type);
				this.Compiler.IL.Emit(OpCodes.Stloc, local);
				this.EmitLoadBox();
				this.Compiler.IL.Emit(OpCodes.Ldloc, local);
				this.Compiler.FreeLocal(local);
				this.Compiler.IL.Emit(OpCodes.Stfld, this._boxValueField);
			}

			// Token: 0x06000798 RID: 1944 RVA: 0x00018E71 File Offset: 0x00017071
			internal override void EmitStore(CompilerScope.Storage value)
			{
				this.EmitLoadBox();
				value.EmitLoad();
				this.Compiler.IL.Emit(OpCodes.Stfld, this._boxValueField);
			}

			// Token: 0x06000799 RID: 1945 RVA: 0x00018E9A File Offset: 0x0001709A
			internal override void EmitAddress()
			{
				this.EmitLoadBox();
				this.Compiler.IL.Emit(OpCodes.Ldflda, this._boxValueField);
			}

			// Token: 0x0600079A RID: 1946 RVA: 0x00018EC0 File Offset: 0x000170C0
			internal void EmitLoadBox()
			{
				this._array.EmitLoad();
				this.Compiler.IL.EmitPrimitive(this._index);
				this.Compiler.IL.Emit(OpCodes.Ldelem_Ref);
				this.Compiler.IL.Emit(OpCodes.Castclass, this._boxType);
			}

			// Token: 0x0400024C RID: 588
			private readonly int _index;

			// Token: 0x0400024D RID: 589
			private readonly CompilerScope.Storage _array;

			// Token: 0x0400024E RID: 590
			private readonly Type _boxType;

			// Token: 0x0400024F RID: 591
			private readonly FieldInfo _boxValueField;
		}

		// Token: 0x020000E6 RID: 230
		private sealed class LocalBoxStorage : CompilerScope.Storage
		{
			// Token: 0x0600079B RID: 1947 RVA: 0x00018F20 File Offset: 0x00017120
			internal LocalBoxStorage(LambdaCompiler compiler, ParameterExpression variable)
				: base(compiler, variable)
			{
				Type type = typeof(StrongBox<>).MakeGenericType(new Type[] { variable.Type });
				this._boxValueField = type.GetField("Value");
				this._boxLocal = compiler.GetLocal(type);
			}

			// Token: 0x0600079C RID: 1948 RVA: 0x00018F72 File Offset: 0x00017172
			internal override void EmitLoad()
			{
				this.Compiler.IL.Emit(OpCodes.Ldloc, this._boxLocal);
				this.Compiler.IL.Emit(OpCodes.Ldfld, this._boxValueField);
			}

			// Token: 0x0600079D RID: 1949 RVA: 0x00018FAA File Offset: 0x000171AA
			internal override void EmitAddress()
			{
				this.Compiler.IL.Emit(OpCodes.Ldloc, this._boxLocal);
				this.Compiler.IL.Emit(OpCodes.Ldflda, this._boxValueField);
			}

			// Token: 0x0600079E RID: 1950 RVA: 0x00018FE4 File Offset: 0x000171E4
			internal override void EmitStore()
			{
				LocalBuilder local = this.Compiler.GetLocal(this.Variable.Type);
				this.Compiler.IL.Emit(OpCodes.Stloc, local);
				this.Compiler.IL.Emit(OpCodes.Ldloc, this._boxLocal);
				this.Compiler.IL.Emit(OpCodes.Ldloc, local);
				this.Compiler.FreeLocal(local);
				this.Compiler.IL.Emit(OpCodes.Stfld, this._boxValueField);
			}

			// Token: 0x0600079F RID: 1951 RVA: 0x00019076 File Offset: 0x00017276
			internal override void EmitStore(CompilerScope.Storage value)
			{
				this.Compiler.IL.Emit(OpCodes.Ldloc, this._boxLocal);
				value.EmitLoad();
				this.Compiler.IL.Emit(OpCodes.Stfld, this._boxValueField);
			}

			// Token: 0x060007A0 RID: 1952 RVA: 0x000190B4 File Offset: 0x000172B4
			internal void EmitStoreBox()
			{
				this.Compiler.IL.Emit(OpCodes.Stloc, this._boxLocal);
			}

			// Token: 0x060007A1 RID: 1953 RVA: 0x000190D1 File Offset: 0x000172D1
			internal override void FreeLocal()
			{
				this.Compiler.FreeLocal(this._boxLocal);
			}

			// Token: 0x04000250 RID: 592
			private readonly LocalBuilder _boxLocal;

			// Token: 0x04000251 RID: 593
			private readonly FieldInfo _boxValueField;
		}
	}
}
