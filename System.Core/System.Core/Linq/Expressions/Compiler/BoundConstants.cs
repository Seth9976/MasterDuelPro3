using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020000DF RID: 223
	internal sealed class BoundConstants
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x00018078 File Offset: 0x00016278
		internal int Count
		{
			get
			{
				return this._values.Count;
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00018085 File Offset: 0x00016285
		internal object[] ToArray()
		{
			return this._values.ToArray();
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00018092 File Offset: 0x00016292
		internal void AddReference(object value, Type type)
		{
			if (this._indexes.TryAdd(value, this._values.Count))
			{
				this._values.Add(value);
			}
			Helpers.IncrementCount<BoundConstants.TypedConstant>(new BoundConstants.TypedConstant(value, type), this._references);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000180CC File Offset: 0x000162CC
		internal void EmitConstant(LambdaCompiler lc, object value, Type type)
		{
			if (!lc.CanEmitBoundConstants)
			{
				throw Error.CannotCompileConstant(value);
			}
			LocalBuilder localBuilder;
			if (this._cache.TryGetValue(new BoundConstants.TypedConstant(value, type), out localBuilder))
			{
				lc.IL.Emit(OpCodes.Ldloc, localBuilder);
				return;
			}
			BoundConstants.EmitConstantsArray(lc);
			this.EmitConstantFromArray(lc, value, type);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00018120 File Offset: 0x00016320
		internal void EmitCacheConstants(LambdaCompiler lc)
		{
			int num = 0;
			foreach (KeyValuePair<BoundConstants.TypedConstant, int> keyValuePair in this._references)
			{
				if (!lc.CanEmitBoundConstants)
				{
					throw Error.CannotCompileConstant(keyValuePair.Key.Value);
				}
				if (BoundConstants.ShouldCache(keyValuePair.Value))
				{
					num++;
				}
			}
			if (num == 0)
			{
				return;
			}
			BoundConstants.EmitConstantsArray(lc);
			this._cache.Clear();
			foreach (KeyValuePair<BoundConstants.TypedConstant, int> keyValuePair2 in this._references)
			{
				if (BoundConstants.ShouldCache(keyValuePair2.Value))
				{
					if (--num > 0)
					{
						lc.IL.Emit(OpCodes.Dup);
					}
					LocalBuilder localBuilder = lc.IL.DeclareLocal(keyValuePair2.Key.Type);
					this.EmitConstantFromArray(lc, keyValuePair2.Key.Value, localBuilder.LocalType);
					lc.IL.Emit(OpCodes.Stloc, localBuilder);
					this._cache.Add(keyValuePair2.Key, localBuilder);
				}
			}
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00018270 File Offset: 0x00016470
		private static bool ShouldCache(int refCount)
		{
			return refCount > 2;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00018276 File Offset: 0x00016476
		private static void EmitConstantsArray(LambdaCompiler lc)
		{
			lc.EmitClosureArgument();
			lc.IL.Emit(OpCodes.Ldfld, CachedReflectionInfo.Closure_Constants);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00018294 File Offset: 0x00016494
		private void EmitConstantFromArray(LambdaCompiler lc, object value, Type type)
		{
			int count;
			if (!this._indexes.TryGetValue(value, out count))
			{
				this._indexes.Add(value, count = this._values.Count);
				this._values.Add(value);
			}
			lc.IL.EmitPrimitive(count);
			lc.IL.Emit(OpCodes.Ldelem_Ref);
			if (type.IsValueType)
			{
				lc.IL.Emit(OpCodes.Unbox_Any, type);
				return;
			}
			if (type != typeof(object))
			{
				lc.IL.Emit(OpCodes.Castclass, type);
			}
		}

		// Token: 0x04000238 RID: 568
		private readonly List<object> _values = new List<object>();

		// Token: 0x04000239 RID: 569
		private readonly Dictionary<object, int> _indexes = new Dictionary<object, int>(global::System.Collections.Generic.ReferenceEqualityComparer<object>.Instance);

		// Token: 0x0400023A RID: 570
		private readonly Dictionary<BoundConstants.TypedConstant, int> _references = new Dictionary<BoundConstants.TypedConstant, int>();

		// Token: 0x0400023B RID: 571
		private readonly Dictionary<BoundConstants.TypedConstant, LocalBuilder> _cache = new Dictionary<BoundConstants.TypedConstant, LocalBuilder>();

		// Token: 0x020000E0 RID: 224
		private readonly struct TypedConstant : IEquatable<BoundConstants.TypedConstant>
		{
			// Token: 0x06000769 RID: 1897 RVA: 0x00018368 File Offset: 0x00016568
			internal TypedConstant(object value, Type type)
			{
				this.Value = value;
				this.Type = type;
			}

			// Token: 0x0600076A RID: 1898 RVA: 0x00018378 File Offset: 0x00016578
			public override int GetHashCode()
			{
				return RuntimeHelpers.GetHashCode(this.Value) ^ this.Type.GetHashCode();
			}

			// Token: 0x0600076B RID: 1899 RVA: 0x00018391 File Offset: 0x00016591
			public bool Equals(BoundConstants.TypedConstant other)
			{
				return this.Value == other.Value && this.Type.Equals(other.Type);
			}

			// Token: 0x0600076C RID: 1900 RVA: 0x000183B4 File Offset: 0x000165B4
			public override bool Equals(object obj)
			{
				return obj is BoundConstants.TypedConstant && this.Equals((BoundConstants.TypedConstant)obj);
			}

			// Token: 0x0400023C RID: 572
			internal readonly object Value;

			// Token: 0x0400023D RID: 573
			internal readonly Type Type;
		}
	}
}
