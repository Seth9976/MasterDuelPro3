using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace System.Xml.Serialization
{
	// Token: 0x02000195 RID: 405
	internal class SourceInfo
	{
		// Token: 0x0600132D RID: 4909 RVA: 0x0005DFCB File Offset: 0x0005C1CB
		public SourceInfo(string source, string arg, MemberInfo memberInfo, Type type, CodeGenerator ilg)
		{
			this.Source = source;
			this.Arg = arg ?? source;
			this.MemberInfo = memberInfo;
			this.Type = type;
			this.ILG = ilg;
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0005E000 File Offset: 0x0005C200
		public SourceInfo CastTo(TypeDesc td)
		{
			return new SourceInfo(string.Concat(new string[] { "((", td.CSharpName, ")", this.Source, ")" }), this.Arg, this.MemberInfo, td.Type, this.ILG);
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0005E05F File Offset: 0x0005C25F
		public void LoadAddress(Type elementType)
		{
			this.InternalLoad(elementType, true);
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0005E069 File Offset: 0x0005C269
		public void Load(Type elementType)
		{
			this.InternalLoad(elementType, false);
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0005E074 File Offset: 0x0005C274
		private void InternalLoad(Type elementType, bool asAddress = false)
		{
			Match match = SourceInfo.regex.Match(this.Arg);
			if (match.Success)
			{
				object variable = this.ILG.GetVariable(match.Groups["a"].Value);
				Type variableType = this.ILG.GetVariableType(variable);
				object variable2 = this.ILG.GetVariable(match.Groups["ia"].Value);
				if (variableType.IsArray)
				{
					this.ILG.Load(variable);
					this.ILG.Load(variable2);
					Type elementType2 = variableType.GetElementType();
					if (CodeGenerator.IsNullableGenericType(elementType2))
					{
						this.ILG.Ldelema(elementType2);
						this.ConvertNullableValue(elementType2, elementType);
						return;
					}
					if (elementType2.IsValueType)
					{
						this.ILG.Ldelema(elementType2);
						if (!asAddress)
						{
							this.ILG.Ldobj(elementType2);
						}
					}
					else
					{
						this.ILG.Ldelem(elementType2);
					}
					if (elementType != null)
					{
						this.ILG.ConvertValue(elementType2, elementType);
						return;
					}
				}
				else
				{
					this.ILG.Load(variable);
					this.ILG.Load(variable2);
					MethodInfo methodInfo = variableType.GetMethod("get_Item", CodeGenerator.InstanceBindingFlags, null, new Type[] { typeof(int) }, null);
					if (methodInfo == null && typeof(IList).IsAssignableFrom(variableType))
					{
						methodInfo = SourceInfo.iListGetItemMethod.Value;
					}
					this.ILG.Call(methodInfo);
					Type returnType = methodInfo.ReturnType;
					if (CodeGenerator.IsNullableGenericType(returnType))
					{
						LocalBuilder tempLocal = this.ILG.GetTempLocal(returnType);
						this.ILG.Stloc(tempLocal);
						this.ILG.Ldloca(tempLocal);
						this.ConvertNullableValue(returnType, elementType);
						return;
					}
					if (elementType != null && !returnType.IsAssignableFrom(elementType) && !elementType.IsAssignableFrom(returnType))
					{
						throw new CodeGeneratorConversionException(returnType, elementType, asAddress, "IsNotAssignableFrom");
					}
					this.Convert(returnType, elementType, asAddress);
					return;
				}
			}
			else
			{
				if (this.Source == "null")
				{
					this.ILG.Load(null);
					return;
				}
				Type type;
				if (this.Arg.StartsWith("o.@", StringComparison.Ordinal) || this.MemberInfo != null)
				{
					object obj = this.ILG.GetVariable(this.Arg.StartsWith("o.@", StringComparison.Ordinal) ? "o" : this.Arg);
					type = this.ILG.GetVariableType(obj);
					if (type.IsValueType)
					{
						this.ILG.LoadAddress(obj);
					}
					else
					{
						this.ILG.Load(obj);
					}
				}
				else
				{
					object obj = this.ILG.GetVariable(this.Arg);
					type = this.ILG.GetVariableType(obj);
					if (CodeGenerator.IsNullableGenericType(type) && type.GetGenericArguments()[0] == elementType)
					{
						this.ILG.LoadAddress(obj);
						this.ConvertNullableValue(type, elementType);
					}
					else if (asAddress)
					{
						this.ILG.LoadAddress(obj);
					}
					else
					{
						this.ILG.Load(obj);
					}
				}
				if (this.MemberInfo != null)
				{
					Type type2 = ((this.MemberInfo is FieldInfo) ? ((FieldInfo)this.MemberInfo).FieldType : ((PropertyInfo)this.MemberInfo).PropertyType);
					if (CodeGenerator.IsNullableGenericType(type2))
					{
						this.ILG.LoadMemberAddress(this.MemberInfo);
						this.ConvertNullableValue(type2, elementType);
						return;
					}
					this.ILG.LoadMember(this.MemberInfo);
					this.Convert(type2, elementType, asAddress);
					return;
				}
				else
				{
					match = SourceInfo.regex2.Match(this.Source);
					if (match.Success)
					{
						if (asAddress)
						{
							this.ILG.ConvertAddress(type, this.Type);
						}
						else
						{
							this.ILG.ConvertValue(type, this.Type);
						}
						type = this.Type;
					}
					this.Convert(type, elementType, asAddress);
				}
			}
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0005E467 File Offset: 0x0005C667
		private void Convert(Type sourceType, Type targetType, bool asAddress)
		{
			if (targetType != null)
			{
				if (asAddress)
				{
					this.ILG.ConvertAddress(sourceType, targetType);
					return;
				}
				this.ILG.ConvertValue(sourceType, targetType);
			}
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0005E490 File Offset: 0x0005C690
		private void ConvertNullableValue(Type nullableType, Type targetType)
		{
			if (targetType != nullableType)
			{
				MethodInfo method = nullableType.GetMethod("get_Value", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ILG.Call(method);
				if (targetType != null)
				{
					this.ILG.ConvertValue(method.ReturnType, targetType);
				}
			}
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0005E4E5 File Offset: 0x0005C6E5
		public static implicit operator string(SourceInfo source)
		{
			return source.Source;
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0005E4ED File Offset: 0x0005C6ED
		public static bool operator !=(SourceInfo a, SourceInfo b)
		{
			if (a != null)
			{
				return !a.Equals(b);
			}
			return b != null;
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0005E501 File Offset: 0x0005C701
		public static bool operator ==(SourceInfo a, SourceInfo b)
		{
			if (a != null)
			{
				return a.Equals(b);
			}
			return b == null;
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0005E514 File Offset: 0x0005C714
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return this.Source == null;
			}
			SourceInfo sourceInfo = obj as SourceInfo;
			return sourceInfo != null && this.Source == sourceInfo.Source;
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0005E551 File Offset: 0x0005C751
		public override int GetHashCode()
		{
			if (this.Source != null)
			{
				return this.Source.GetHashCode();
			}
			return 0;
		}

		// Token: 0x040008E4 RID: 2276
		private static Regex regex = new Regex("([(][(](?<t>[^)]+)[)])?(?<a>[^[]+)[[](?<ia>.+)[]][)]?");

		// Token: 0x040008E5 RID: 2277
		private static Regex regex2 = new Regex("[(][(](?<cast>[^)]+)[)](?<arg>[^)]+)[)]");

		// Token: 0x040008E6 RID: 2278
		private static readonly Lazy<MethodInfo> iListGetItemMethod = new Lazy<MethodInfo>(() => typeof(IList).GetMethod("get_Item", CodeGenerator.InstanceBindingFlags, null, new Type[] { typeof(int) }, null));

		// Token: 0x040008E7 RID: 2279
		public string Source;

		// Token: 0x040008E8 RID: 2280
		public readonly string Arg;

		// Token: 0x040008E9 RID: 2281
		public readonly MemberInfo MemberInfo;

		// Token: 0x040008EA RID: 2282
		public readonly Type Type;

		// Token: 0x040008EB RID: 2283
		public readonly CodeGenerator ILG;
	}
}
