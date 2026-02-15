using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace System.Xml.Serialization
{
	// Token: 0x020001DC RID: 476
	internal class ReflectionAwareILGen
	{
		// Token: 0x06001893 RID: 6291 RVA: 0x00002127 File Offset: 0x00000327
		internal ReflectionAwareILGen()
		{
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x00094520 File Offset: 0x00092720
		internal void WriteReflectionInit(TypeScope scope)
		{
			foreach (object obj in scope.Types)
			{
				Type type = (Type)obj;
				scope.GetTypeDesc(type);
			}
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x0009457C File Offset: 0x0009277C
		internal void ILGenForEnumLongValue(CodeGenerator ilg, string variable)
		{
			ArgBuilder arg = ilg.GetArg(variable);
			ilg.Ldarg(arg);
			ilg.ConvertValue(arg.ArgType, typeof(long));
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x000945AE File Offset: 0x000927AE
		internal string GetStringForTypeof(string typeFullName)
		{
			return "typeof(" + typeFullName + ")";
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x000945C0 File Offset: 0x000927C0
		internal string GetStringForMember(string obj, string memberName, TypeDesc typeDesc)
		{
			return obj + ".@" + memberName;
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x000945CE File Offset: 0x000927CE
		internal SourceInfo GetSourceForMember(string obj, MemberMapping member, TypeDesc typeDesc, CodeGenerator ilg)
		{
			return this.GetSourceForMember(obj, member, member.MemberInfo, typeDesc, ilg);
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x000945E1 File Offset: 0x000927E1
		internal SourceInfo GetSourceForMember(string obj, MemberMapping member, MemberInfo memberInfo, TypeDesc typeDesc, CodeGenerator ilg)
		{
			return new SourceInfo(this.GetStringForMember(obj, member.Name, typeDesc), obj, memberInfo, member.TypeDesc.Type, ilg);
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00094606 File Offset: 0x00092806
		internal void ILGenForEnumMember(CodeGenerator ilg, Type type, string memberName)
		{
			ilg.Ldc(Enum.Parse(type, memberName, false));
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00094616 File Offset: 0x00092816
		internal string GetStringForArrayMember(string arrayName, string subscript, TypeDesc arrayTypeDesc)
		{
			return arrayName + "[" + subscript + "]";
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x00094629 File Offset: 0x00092829
		internal string GetStringForMethod(string obj, string typeFullName, string memberName)
		{
			return obj + "." + memberName + "(";
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x0009463C File Offset: 0x0009283C
		internal void ILGenForCreateInstance(CodeGenerator ilg, Type type, bool ctorInaccessible, bool cast)
		{
			if (ctorInaccessible)
			{
				this.ILGenForCreateInstance(ilg, type, cast ? type : null, ctorInaccessible);
				return;
			}
			ConstructorInfo constructor = type.GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			if (constructor != null)
			{
				ilg.New(constructor);
				return;
			}
			LocalBuilder tempLocal = ilg.GetTempLocal(type);
			ilg.Ldloca(tempLocal);
			ilg.InitObj(type);
			ilg.Ldloc(tempLocal);
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x000946A0 File Offset: 0x000928A0
		internal void ILGenForCreateInstance(CodeGenerator ilg, Type type, Type cast, bool nonPublic)
		{
			if (type == typeof(DBNull))
			{
				FieldInfo field = typeof(DBNull).GetField("Value", CodeGenerator.StaticBindingFlags);
				ilg.LoadMember(field);
				return;
			}
			if (type.FullName == "System.Xml.Linq.XElement")
			{
				Type type2 = type.Assembly.GetType("System.Xml.Linq.XName");
				if (type2 != null)
				{
					MethodInfo method = type2.GetMethod("op_Implicit", CodeGenerator.StaticBindingFlags, null, new Type[] { typeof(string) }, null);
					ConstructorInfo constructor = type.GetConstructor(CodeGenerator.InstanceBindingFlags, null, new Type[] { type2 }, null);
					if (method != null && constructor != null)
					{
						ilg.Ldstr("default");
						ilg.Call(method);
						ilg.New(constructor);
						return;
					}
				}
			}
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;
			if (nonPublic)
			{
				bindingFlags |= BindingFlags.NonPublic;
			}
			MethodInfo method2 = typeof(Activator).GetMethod("CreateInstance", CodeGenerator.StaticBindingFlags, null, new Type[]
			{
				typeof(Type),
				typeof(BindingFlags),
				typeof(Binder),
				typeof(object[]),
				typeof(CultureInfo)
			}, null);
			ilg.Ldc(type);
			ilg.Load((int)bindingFlags);
			ilg.Load(null);
			ilg.NewArray(typeof(object), 0);
			ilg.Load(null);
			ilg.Call(method2);
			if (cast != null)
			{
				ilg.ConvertValue(method2.ReturnType, cast);
			}
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x00094848 File Offset: 0x00092A48
		internal void WriteLocalDecl(string variableName, SourceInfo initValue)
		{
			Type type = initValue.Type;
			LocalBuilder localBuilder = initValue.ILG.DeclareOrGetLocal(type, variableName);
			if (initValue.Source != null)
			{
				if (initValue == "null")
				{
					initValue.ILG.Load(null);
				}
				else if (initValue.Arg.StartsWith("o.@", StringComparison.Ordinal))
				{
					initValue.ILG.LoadMember(initValue.ILG.GetLocal("o"), initValue.MemberInfo);
				}
				else if (initValue.Source.EndsWith("]", StringComparison.Ordinal))
				{
					initValue.Load(initValue.Type);
				}
				else if (initValue.Source == "fixup.Source" || initValue.Source == "e.Current")
				{
					string[] array = initValue.Source.Split('.', StringSplitOptions.None);
					object variable = initValue.ILG.GetVariable(array[0]);
					PropertyInfo property = initValue.ILG.GetVariableType(variable).GetProperty(array[1]);
					initValue.ILG.LoadMember(variable, property);
					initValue.ILG.ConvertValue(property.PropertyType, localBuilder.LocalType);
				}
				else
				{
					object variable2 = initValue.ILG.GetVariable(initValue.Arg);
					initValue.ILG.Load(variable2);
					initValue.ILG.ConvertValue(initValue.ILG.GetVariableType(variable2), localBuilder.LocalType);
				}
				initValue.ILG.Stloc(localBuilder);
			}
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x000949C4 File Offset: 0x00092BC4
		internal void WriteCreateInstance(string source, bool ctorInaccessible, Type type, CodeGenerator ilg)
		{
			LocalBuilder localBuilder = ilg.DeclareOrGetLocal(type, source);
			this.ILGenForCreateInstance(ilg, type, ctorInaccessible, ctorInaccessible);
			ilg.Stloc(localBuilder);
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x000949EE File Offset: 0x00092BEE
		internal void WriteInstanceOf(SourceInfo source, Type type, CodeGenerator ilg)
		{
			source.Load(typeof(object));
			ilg.IsInst(type);
			ilg.Load(null);
			ilg.Cne();
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x00094A14 File Offset: 0x00092C14
		internal void WriteArrayLocalDecl(string typeName, string variableName, SourceInfo initValue, TypeDesc arrayTypeDesc)
		{
			Type type = ((typeName == arrayTypeDesc.CSharpName) ? arrayTypeDesc.Type : arrayTypeDesc.Type.MakeArrayType());
			LocalBuilder localBuilder = initValue.ILG.DeclareOrGetLocal(type, variableName);
			if (initValue != null)
			{
				initValue.Load(localBuilder.LocalType);
				initValue.ILG.Stloc(localBuilder);
			}
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x00094A75 File Offset: 0x00092C75
		internal void WriteTypeCompare(string variable, Type type, CodeGenerator ilg)
		{
			ilg.Ldloc(typeof(Type), variable);
			ilg.Ldc(type);
			ilg.Ceq();
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00094A75 File Offset: 0x00092C75
		internal void WriteArrayTypeCompare(string variable, Type arrayType, CodeGenerator ilg)
		{
			ilg.Ldloc(typeof(Type), variable);
			ilg.Ldc(arrayType);
			ilg.Ceq();
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x00094A95 File Offset: 0x00092C95
		internal static string GetQuotedCSharpString(IndentedWriter notUsed, string value)
		{
			if (value == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("@\"");
			stringBuilder.Append(ReflectionAwareILGen.GetCSharpString(value));
			stringBuilder.Append("\"");
			return stringBuilder.ToString();
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x00094ACC File Offset: 0x00092CCC
		internal static string GetCSharpString(string value)
		{
			if (value == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in value)
			{
				if (c < ' ')
				{
					if (c == '\r')
					{
						stringBuilder.Append("\\r");
					}
					else if (c == '\n')
					{
						stringBuilder.Append("\\n");
					}
					else if (c == '\t')
					{
						stringBuilder.Append("\\t");
					}
					else
					{
						byte b = (byte)c;
						stringBuilder.Append("\\x");
						stringBuilder.Append("0123456789ABCDEF"[b >> 4]);
						stringBuilder.Append("0123456789ABCDEF"[(int)(b & 15)]);
					}
				}
				else if (c == '"')
				{
					stringBuilder.Append("\"\"");
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000A8B RID: 2699
		private const string hexDigits = "0123456789ABCDEF";

		// Token: 0x04000A8C RID: 2700
		private const string arrayMemberKey = "0";
	}
}
