using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml.Serialization.Configuration;

namespace System.Xml.Serialization
{
	// Token: 0x02000145 RID: 325
	internal class CodeGenerator
	{
		// Token: 0x06000FF0 RID: 4080 RVA: 0x0004EAD9 File Offset: 0x0004CCD9
		internal static bool IsValidLanguageIndependentIdentifier(string ident)
		{
			return CodeGenerator.IsValidLanguageIndependentIdentifier(ident);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0004EAE1 File Offset: 0x0004CCE1
		internal static void ValidateIdentifiers(CodeObject e)
		{
			CodeGenerator.ValidateIdentifiers(e);
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0004EAE9 File Offset: 0x0004CCE9
		internal CodeGenerator(TypeBuilder typeBuilder)
		{
			this.typeBuilder = typeBuilder;
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0004EB1C File Offset: 0x0004CD1C
		internal static bool IsNullableGenericType(Type type)
		{
			return type.Name == "Nullable`1";
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0000A558 File Offset: 0x00008758
		internal static void AssertHasInterface(Type type, Type iType)
		{
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0004EB30 File Offset: 0x0004CD30
		internal void BeginMethod(Type returnType, string methodName, Type[] argTypes, string[] argNames, MethodAttributes methodAttributes)
		{
			this.methodBuilder = this.typeBuilder.DefineMethod(methodName, methodAttributes, returnType, argTypes);
			this.ilGen = this.methodBuilder.GetILGenerator();
			this.InitILGeneration(argTypes, argNames, (this.methodBuilder.Attributes & MethodAttributes.Static) == MethodAttributes.Static);
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0004EB7F File Offset: 0x0004CD7F
		internal void BeginMethod(Type returnType, MethodBuilderInfo methodBuilderInfo, Type[] argTypes, string[] argNames, MethodAttributes methodAttributes)
		{
			this.methodBuilder = methodBuilderInfo.MethodBuilder;
			this.ilGen = this.methodBuilder.GetILGenerator();
			this.InitILGeneration(argTypes, argNames, (this.methodBuilder.Attributes & MethodAttributes.Static) == MethodAttributes.Static);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0004EBBC File Offset: 0x0004CDBC
		private void InitILGeneration(Type[] argTypes, string[] argNames, bool isStatic)
		{
			this.methodEndLabel = this.ilGen.DefineLabel();
			this.retLabel = this.ilGen.DefineLabel();
			this.blockStack = new Stack();
			this.whileStack = new Stack();
			this.currentScope = new LocalScope();
			this.freeLocals = new Dictionary<Tuple<Type, string>, Queue<LocalBuilder>>();
			this.argList = new Dictionary<string, ArgBuilder>();
			if (!isStatic)
			{
				this.argList.Add("this", new ArgBuilder("this", 0, this.typeBuilder.BaseType));
			}
			for (int i = 0; i < argTypes.Length; i++)
			{
				ArgBuilder argBuilder = new ArgBuilder(argNames[i], this.argList.Count, argTypes[i]);
				this.argList.Add(argBuilder.Name, argBuilder);
				this.methodBuilder.DefineParameter(argBuilder.Index, ParameterAttributes.None, argBuilder.Name);
			}
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0004EC9C File Offset: 0x0004CE9C
		internal MethodBuilder EndMethod()
		{
			this.MarkLabel(this.methodEndLabel);
			this.Ret();
			MethodBuilder methodBuilder = this.methodBuilder;
			this.methodBuilder = null;
			this.ilGen = null;
			this.freeLocals = null;
			this.blockStack = null;
			this.whileStack = null;
			this.argList = null;
			this.currentScope = null;
			this.retLocal = null;
			return methodBuilder;
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x0004ECF9 File Offset: 0x0004CEF9
		internal MethodBuilder MethodBuilder
		{
			get
			{
				return this.methodBuilder;
			}
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0004ED01 File Offset: 0x0004CF01
		internal static Exception NotSupported(string msg)
		{
			return new NotSupportedException(msg);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0004ED09 File Offset: 0x0004CF09
		internal ArgBuilder GetArg(string name)
		{
			return this.argList[name];
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0004ED17 File Offset: 0x0004CF17
		internal LocalBuilder GetLocal(string name)
		{
			return this.currentScope[name];
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x0004ED25 File Offset: 0x0004CF25
		internal LocalBuilder ReturnLocal
		{
			get
			{
				if (this.retLocal == null)
				{
					this.retLocal = this.DeclareLocal(this.methodBuilder.ReturnType, "_ret");
				}
				return this.retLocal;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x0004ED51 File Offset: 0x0004CF51
		internal Label ReturnLabel
		{
			get
			{
				return this.retLabel;
			}
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0004ED5C File Offset: 0x0004CF5C
		internal LocalBuilder GetTempLocal(Type type)
		{
			LocalBuilder localBuilder;
			if (!this.TmpLocals.TryGetValue(type, out localBuilder))
			{
				localBuilder = this.DeclareLocal(type, "_tmp" + this.TmpLocals.Count.ToString());
				this.TmpLocals.Add(type, localBuilder);
			}
			return localBuilder;
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0004EDAC File Offset: 0x0004CFAC
		internal Type GetVariableType(object var)
		{
			if (var is ArgBuilder)
			{
				return ((ArgBuilder)var).ArgType;
			}
			if (var is LocalBuilder)
			{
				return ((LocalBuilder)var).LocalType;
			}
			return var.GetType();
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0004EDDC File Offset: 0x0004CFDC
		internal object GetVariable(string name)
		{
			object obj;
			if (this.TryGetVariable(name, out obj))
			{
				return obj;
			}
			return null;
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0004EDF8 File Offset: 0x0004CFF8
		internal bool TryGetVariable(string name, out object variable)
		{
			LocalBuilder localBuilder;
			if (this.currentScope != null && this.currentScope.TryGetValue(name, out localBuilder))
			{
				variable = localBuilder;
				return true;
			}
			ArgBuilder argBuilder;
			if (this.argList != null && this.argList.TryGetValue(name, out argBuilder))
			{
				variable = argBuilder;
				return true;
			}
			int num;
			if (int.TryParse(name, out num))
			{
				variable = num;
				return true;
			}
			variable = null;
			return false;
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0004EE58 File Offset: 0x0004D058
		internal void EnterScope()
		{
			LocalScope localScope = new LocalScope(this.currentScope);
			this.currentScope = localScope;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0004EE78 File Offset: 0x0004D078
		internal void ExitScope()
		{
			this.currentScope.AddToFreeLocals(this.freeLocals);
			this.currentScope = this.currentScope.parent;
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0004EE9C File Offset: 0x0004D09C
		private bool TryDequeueLocal(Type type, string name, out LocalBuilder local)
		{
			Tuple<Type, string> tuple = new Tuple<Type, string>(type, name);
			Queue<LocalBuilder> queue;
			if (this.freeLocals.TryGetValue(tuple, out queue))
			{
				local = queue.Dequeue();
				if (queue.Count == 0)
				{
					this.freeLocals.Remove(tuple);
				}
				return true;
			}
			local = null;
			return false;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0004EEE4 File Offset: 0x0004D0E4
		internal LocalBuilder DeclareLocal(Type type, string name)
		{
			LocalBuilder localBuilder;
			if (!this.TryDequeueLocal(type, name, out localBuilder))
			{
				localBuilder = this.ilGen.DeclareLocal(type, false);
				if (DiagnosticsSwitches.KeepTempFiles.Enabled)
				{
					localBuilder.SetLocalSymInfo(name);
				}
			}
			this.currentScope[name] = localBuilder;
			return localBuilder;
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0004EF2C File Offset: 0x0004D12C
		internal LocalBuilder DeclareOrGetLocal(Type type, string name)
		{
			LocalBuilder localBuilder;
			if (!this.currentScope.TryGetValue(name, out localBuilder))
			{
				localBuilder = this.DeclareLocal(type, name);
			}
			return localBuilder;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0004EF54 File Offset: 0x0004D154
		internal object For(LocalBuilder local, object start, object end)
		{
			ForState forState = new ForState(local, this.DefineLabel(), this.DefineLabel(), end);
			if (forState.Index != null)
			{
				this.Load(start);
				this.Stloc(forState.Index);
				this.Br(forState.TestLabel);
			}
			this.MarkLabel(forState.BeginLabel);
			this.blockStack.Push(forState);
			return forState;
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0004EFB8 File Offset: 0x0004D1B8
		internal void EndFor()
		{
			ForState forState = this.blockStack.Pop() as ForState;
			if (forState.Index != null)
			{
				this.Ldloc(forState.Index);
				this.Ldc(1);
				this.Add();
				this.Stloc(forState.Index);
				this.MarkLabel(forState.TestLabel);
				this.Ldloc(forState.Index);
				this.Load(forState.End);
				if (this.GetVariableType(forState.End).IsArray)
				{
					this.Ldlen();
				}
				else
				{
					MethodInfo method = typeof(ICollection).GetMethod("get_Count", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					this.Call(method);
				}
				this.Blt(forState.BeginLabel);
				return;
			}
			this.Br(forState.BeginLabel);
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0004F086 File Offset: 0x0004D286
		internal void If()
		{
			this.InternalIf(false);
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0004F08F File Offset: 0x0004D28F
		internal void IfNot()
		{
			this.InternalIf(true);
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0004F098 File Offset: 0x0004D298
		private OpCode GetBranchCode(Cmp cmp)
		{
			return CodeGenerator.BranchCodes[(int)cmp];
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0004F0A8 File Offset: 0x0004D2A8
		internal void If(Cmp cmpOp)
		{
			IfState ifState = new IfState();
			ifState.EndIf = this.DefineLabel();
			ifState.ElseBegin = this.DefineLabel();
			this.ilGen.Emit(this.GetBranchCode(cmpOp), ifState.ElseBegin);
			this.blockStack.Push(ifState);
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0004F0F7 File Offset: 0x0004D2F7
		internal void If(object value1, Cmp cmpOp, object value2)
		{
			this.Load(value1);
			this.Load(value2);
			this.If(cmpOp);
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0004F110 File Offset: 0x0004D310
		internal void Else()
		{
			IfState ifState = this.PopIfState();
			this.Br(ifState.EndIf);
			this.MarkLabel(ifState.ElseBegin);
			ifState.ElseBegin = ifState.EndIf;
			this.blockStack.Push(ifState);
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0004F154 File Offset: 0x0004D354
		internal void EndIf()
		{
			IfState ifState = this.PopIfState();
			if (!ifState.ElseBegin.Equals(ifState.EndIf))
			{
				this.MarkLabel(ifState.ElseBegin);
			}
			this.MarkLabel(ifState.EndIf);
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0004F196 File Offset: 0x0004D396
		internal void BeginExceptionBlock()
		{
			this.leaveLabels.Push(this.DefineLabel());
			this.ilGen.BeginExceptionBlock();
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0004F1BA File Offset: 0x0004D3BA
		internal void BeginCatchBlock(Type exception)
		{
			this.ilGen.BeginCatchBlock(exception);
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0004F1C8 File Offset: 0x0004D3C8
		internal void EndExceptionBlock()
		{
			this.ilGen.EndExceptionBlock();
			this.ilGen.MarkLabel((Label)this.leaveLabels.Pop());
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0004F1F0 File Offset: 0x0004D3F0
		internal void Leave()
		{
			this.ilGen.Emit(OpCodes.Leave, (Label)this.leaveLabels.Peek());
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0004F212 File Offset: 0x0004D412
		internal void Call(MethodInfo methodInfo)
		{
			if (methodInfo.IsVirtual && !methodInfo.DeclaringType.IsValueType)
			{
				this.ilGen.Emit(OpCodes.Callvirt, methodInfo);
				return;
			}
			this.ilGen.Emit(OpCodes.Call, methodInfo);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0004F24C File Offset: 0x0004D44C
		internal void Call(ConstructorInfo ctor)
		{
			this.ilGen.Emit(OpCodes.Call, ctor);
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0004F25F File Offset: 0x0004D45F
		internal void New(ConstructorInfo constructorInfo)
		{
			this.ilGen.Emit(OpCodes.Newobj, constructorInfo);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0004F272 File Offset: 0x0004D472
		internal void InitObj(Type valueType)
		{
			this.ilGen.Emit(OpCodes.Initobj, valueType);
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0004F285 File Offset: 0x0004D485
		internal void NewArray(Type elementType, object len)
		{
			this.Load(len);
			this.ilGen.Emit(OpCodes.Newarr, elementType);
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0004F2A0 File Offset: 0x0004D4A0
		internal void LoadArrayElement(object obj, object arrayIndex)
		{
			Type elementType = this.GetVariableType(obj).GetElementType();
			this.Load(obj);
			this.Load(arrayIndex);
			if (CodeGenerator.IsStruct(elementType))
			{
				this.Ldelema(elementType);
				this.Ldobj(elementType);
				return;
			}
			this.Ldelem(elementType);
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0004F2E8 File Offset: 0x0004D4E8
		internal void StoreArrayElement(object obj, object arrayIndex, object value)
		{
			Type variableType = this.GetVariableType(obj);
			if (variableType == typeof(Array))
			{
				this.Load(obj);
				this.Call(typeof(Array).GetMethod("SetValue", new Type[]
				{
					typeof(object),
					typeof(int)
				}));
				return;
			}
			Type elementType = variableType.GetElementType();
			this.Load(obj);
			this.Load(arrayIndex);
			if (CodeGenerator.IsStruct(elementType))
			{
				this.Ldelema(elementType);
			}
			this.Load(value);
			this.ConvertValue(this.GetVariableType(value), elementType);
			if (CodeGenerator.IsStruct(elementType))
			{
				this.Stobj(elementType);
				return;
			}
			this.Stelem(elementType);
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0004F3A1 File Offset: 0x0004D5A1
		private static bool IsStruct(Type objType)
		{
			return objType.IsValueType && !objType.IsPrimitive;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0004F3B6 File Offset: 0x0004D5B6
		internal Type LoadMember(object obj, MemberInfo memberInfo)
		{
			if (this.GetVariableType(obj).IsValueType)
			{
				this.LoadAddress(obj);
			}
			else
			{
				this.Load(obj);
			}
			return this.LoadMember(memberInfo);
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0004F3E0 File Offset: 0x0004D5E0
		private static MethodInfo GetPropertyMethodFromBaseType(PropertyInfo propertyInfo, bool isGetter)
		{
			Type type = propertyInfo.DeclaringType.BaseType;
			string name = propertyInfo.Name;
			MethodInfo methodInfo = null;
			while (type != null)
			{
				PropertyInfo property = type.GetProperty(name);
				if (property != null)
				{
					if (isGetter)
					{
						methodInfo = property.GetGetMethod(true);
					}
					else
					{
						methodInfo = property.GetSetMethod(true);
					}
					if (methodInfo != null)
					{
						break;
					}
				}
				type = type.BaseType;
			}
			return methodInfo;
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0004F444 File Offset: 0x0004D644
		internal Type LoadMember(MemberInfo memberInfo)
		{
			Type type;
			if (memberInfo.MemberType == MemberTypes.Field)
			{
				FieldInfo fieldInfo = (FieldInfo)memberInfo;
				type = fieldInfo.FieldType;
				if (fieldInfo.IsStatic)
				{
					this.ilGen.Emit(OpCodes.Ldsfld, fieldInfo);
				}
				else
				{
					this.ilGen.Emit(OpCodes.Ldfld, fieldInfo);
				}
			}
			else
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
				type = propertyInfo.PropertyType;
				if (propertyInfo != null)
				{
					MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
					if (methodInfo == null)
					{
						methodInfo = CodeGenerator.GetPropertyMethodFromBaseType(propertyInfo, true);
					}
					this.Call(methodInfo);
				}
			}
			return type;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0004F4D0 File Offset: 0x0004D6D0
		internal Type LoadMemberAddress(MemberInfo memberInfo)
		{
			Type type;
			if (memberInfo.MemberType == MemberTypes.Field)
			{
				FieldInfo fieldInfo = (FieldInfo)memberInfo;
				type = fieldInfo.FieldType;
				if (fieldInfo.IsStatic)
				{
					this.ilGen.Emit(OpCodes.Ldsflda, fieldInfo);
				}
				else
				{
					this.ilGen.Emit(OpCodes.Ldflda, fieldInfo);
				}
			}
			else
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
				type = propertyInfo.PropertyType;
				if (propertyInfo != null)
				{
					MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
					if (methodInfo == null)
					{
						methodInfo = CodeGenerator.GetPropertyMethodFromBaseType(propertyInfo, true);
					}
					this.Call(methodInfo);
					LocalBuilder tempLocal = this.GetTempLocal(type);
					this.Stloc(tempLocal);
					this.Ldloca(tempLocal);
				}
			}
			return type;
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0004F578 File Offset: 0x0004D778
		internal void StoreMember(MemberInfo memberInfo)
		{
			if (memberInfo.MemberType != MemberTypes.Field)
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
				if (propertyInfo != null)
				{
					MethodInfo methodInfo = propertyInfo.GetSetMethod(true);
					if (methodInfo == null)
					{
						methodInfo = CodeGenerator.GetPropertyMethodFromBaseType(propertyInfo, false);
					}
					this.Call(methodInfo);
				}
				return;
			}
			FieldInfo fieldInfo = (FieldInfo)memberInfo;
			if (fieldInfo.IsStatic)
			{
				this.ilGen.Emit(OpCodes.Stsfld, fieldInfo);
				return;
			}
			this.ilGen.Emit(OpCodes.Stfld, fieldInfo);
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0004F5F4 File Offset: 0x0004D7F4
		internal void Load(object obj)
		{
			if (obj == null)
			{
				this.ilGen.Emit(OpCodes.Ldnull);
				return;
			}
			if (obj is ArgBuilder)
			{
				this.Ldarg((ArgBuilder)obj);
				return;
			}
			if (obj is LocalBuilder)
			{
				this.Ldloc((LocalBuilder)obj);
				return;
			}
			this.Ldc(obj);
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0004F646 File Offset: 0x0004D846
		internal void LoadAddress(object obj)
		{
			if (obj is ArgBuilder)
			{
				this.LdargAddress((ArgBuilder)obj);
				return;
			}
			if (obj is LocalBuilder)
			{
				this.LdlocAddress((LocalBuilder)obj);
				return;
			}
			this.Load(obj);
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0004F679 File Offset: 0x0004D879
		internal void ConvertAddress(Type source, Type target)
		{
			this.InternalConvert(source, target, true);
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0004F684 File Offset: 0x0004D884
		internal void ConvertValue(Type source, Type target)
		{
			this.InternalConvert(source, target, false);
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0004F68F File Offset: 0x0004D88F
		internal void Castclass(Type target)
		{
			this.ilGen.Emit(OpCodes.Castclass, target);
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0004F6A2 File Offset: 0x0004D8A2
		internal void Box(Type type)
		{
			this.ilGen.Emit(OpCodes.Box, type);
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0004F6B5 File Offset: 0x0004D8B5
		internal void Unbox(Type type)
		{
			this.ilGen.Emit(OpCodes.Unbox, type);
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0004F6C8 File Offset: 0x0004D8C8
		private OpCode GetLdindOpCode(TypeCode typeCode)
		{
			return CodeGenerator.LdindOpCodes[(int)typeCode];
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0004F6D8 File Offset: 0x0004D8D8
		internal void Ldobj(Type type)
		{
			OpCode ldindOpCode = this.GetLdindOpCode(Type.GetTypeCode(type));
			if (!ldindOpCode.Equals(OpCodes.Nop))
			{
				this.ilGen.Emit(ldindOpCode);
				return;
			}
			this.ilGen.Emit(OpCodes.Ldobj, type);
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0004F71E File Offset: 0x0004D91E
		internal void Stobj(Type type)
		{
			this.ilGen.Emit(OpCodes.Stobj, type);
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0004F731 File Offset: 0x0004D931
		internal void Ceq()
		{
			this.ilGen.Emit(OpCodes.Ceq);
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0004F743 File Offset: 0x0004D943
		internal void Clt()
		{
			this.ilGen.Emit(OpCodes.Clt);
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0004F755 File Offset: 0x0004D955
		internal void Cne()
		{
			this.Ceq();
			this.Ldc(0);
			this.Ceq();
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0004F76A File Offset: 0x0004D96A
		internal void Ble(Label label)
		{
			this.ilGen.Emit(OpCodes.Ble, label);
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0004F77D File Offset: 0x0004D97D
		internal void Throw()
		{
			this.ilGen.Emit(OpCodes.Throw);
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0004F78F File Offset: 0x0004D98F
		internal void Ldtoken(Type t)
		{
			this.ilGen.Emit(OpCodes.Ldtoken, t);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0004F7A4 File Offset: 0x0004D9A4
		internal void Ldc(object o)
		{
			Type type = o.GetType();
			if (o is Type)
			{
				this.Ldtoken((Type)o);
				this.Call(typeof(Type).GetMethod("GetTypeFromHandle", BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(RuntimeTypeHandle) }, null));
				return;
			}
			if (type.IsEnum)
			{
				this.Ldc(((IConvertible)o).ToType(Enum.GetUnderlyingType(type), null));
				return;
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				this.Ldc((bool)o);
				return;
			case TypeCode.Char:
				throw new NotSupportedException("Char is not a valid schema primitive and should be treated as int in DataContract");
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
				this.Ldc(((IConvertible)o).ToInt32(CultureInfo.InvariantCulture));
				return;
			case TypeCode.Int32:
				this.Ldc((int)o);
				return;
			case TypeCode.UInt32:
				this.Ldc((int)((uint)o));
				return;
			case TypeCode.Int64:
				this.Ldc((long)o);
				return;
			case TypeCode.UInt64:
				this.Ldc((long)((ulong)o));
				return;
			case TypeCode.Single:
				this.Ldc((float)o);
				return;
			case TypeCode.Double:
				this.Ldc((double)o);
				return;
			case TypeCode.Decimal:
			{
				ConstructorInfo constructor = typeof(decimal).GetConstructor(CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(int),
					typeof(int),
					typeof(int),
					typeof(bool),
					typeof(byte)
				}, null);
				int[] bits = decimal.GetBits((decimal)o);
				this.Ldc(bits[0]);
				this.Ldc(bits[1]);
				this.Ldc(bits[2]);
				this.Ldc(((long)bits[3] & (long)((ulong)int.MinValue)) == (long)((ulong)int.MinValue));
				this.Ldc((int)((byte)((bits[3] >> 16) & 255)));
				this.New(constructor);
				return;
			}
			case TypeCode.DateTime:
			{
				ConstructorInfo constructor2 = typeof(DateTime).GetConstructor(CodeGenerator.InstanceBindingFlags, null, new Type[] { typeof(long) }, null);
				this.Ldc(((DateTime)o).Ticks);
				this.New(constructor2);
				return;
			}
			case TypeCode.String:
				this.Ldstr((string)o);
				return;
			}
			if (type == typeof(TimeSpan) && LocalAppContextSwitches.EnableTimeSpanSerialization)
			{
				ConstructorInfo constructor3 = typeof(TimeSpan).GetConstructor(CodeGenerator.InstanceBindingFlags, null, new Type[] { typeof(long) }, null);
				this.Ldc(((TimeSpan)o).Ticks);
				this.New(constructor3);
				return;
			}
			throw new NotSupportedException("UnknownConstantType");
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0004FA75 File Offset: 0x0004DC75
		internal void Ldc(bool boolVar)
		{
			if (boolVar)
			{
				this.ilGen.Emit(OpCodes.Ldc_I4_1);
				return;
			}
			this.ilGen.Emit(OpCodes.Ldc_I4_0);
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0004FA9C File Offset: 0x0004DC9C
		internal void Ldc(int intVar)
		{
			switch (intVar)
			{
			case -1:
				this.ilGen.Emit(OpCodes.Ldc_I4_M1);
				return;
			case 0:
				this.ilGen.Emit(OpCodes.Ldc_I4_0);
				return;
			case 1:
				this.ilGen.Emit(OpCodes.Ldc_I4_1);
				return;
			case 2:
				this.ilGen.Emit(OpCodes.Ldc_I4_2);
				return;
			case 3:
				this.ilGen.Emit(OpCodes.Ldc_I4_3);
				return;
			case 4:
				this.ilGen.Emit(OpCodes.Ldc_I4_4);
				return;
			case 5:
				this.ilGen.Emit(OpCodes.Ldc_I4_5);
				return;
			case 6:
				this.ilGen.Emit(OpCodes.Ldc_I4_6);
				return;
			case 7:
				this.ilGen.Emit(OpCodes.Ldc_I4_7);
				return;
			case 8:
				this.ilGen.Emit(OpCodes.Ldc_I4_8);
				return;
			default:
				this.ilGen.Emit(OpCodes.Ldc_I4, intVar);
				return;
			}
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0004FB99 File Offset: 0x0004DD99
		internal void Ldc(long l)
		{
			this.ilGen.Emit(OpCodes.Ldc_I8, l);
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0004FBAC File Offset: 0x0004DDAC
		internal void Ldc(float f)
		{
			this.ilGen.Emit(OpCodes.Ldc_R4, f);
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0004FBBF File Offset: 0x0004DDBF
		internal void Ldc(double d)
		{
			this.ilGen.Emit(OpCodes.Ldc_R8, d);
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0004FBD2 File Offset: 0x0004DDD2
		internal void Ldstr(string strVar)
		{
			if (strVar == null)
			{
				this.ilGen.Emit(OpCodes.Ldnull);
				return;
			}
			this.ilGen.Emit(OpCodes.Ldstr, strVar);
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0004FBF9 File Offset: 0x0004DDF9
		internal void LdlocAddress(LocalBuilder localBuilder)
		{
			if (localBuilder.LocalType.IsValueType)
			{
				this.Ldloca(localBuilder);
				return;
			}
			this.Ldloc(localBuilder);
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0004FC17 File Offset: 0x0004DE17
		internal void Ldloc(LocalBuilder localBuilder)
		{
			this.ilGen.Emit(OpCodes.Ldloc, localBuilder);
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0004FC2C File Offset: 0x0004DE2C
		internal void Ldloc(string name)
		{
			LocalBuilder localBuilder = this.currentScope[name];
			this.Ldloc(localBuilder);
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0004FC50 File Offset: 0x0004DE50
		internal void Stloc(Type type, string name)
		{
			LocalBuilder localBuilder = null;
			if (!this.currentScope.TryGetValue(name, out localBuilder))
			{
				localBuilder = this.DeclareLocal(type, name);
			}
			this.Stloc(localBuilder);
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0004FC7F File Offset: 0x0004DE7F
		internal void Stloc(LocalBuilder local)
		{
			this.ilGen.Emit(OpCodes.Stloc, local);
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0004FC94 File Offset: 0x0004DE94
		internal void Ldloc(Type type, string name)
		{
			LocalBuilder localBuilder = this.currentScope[name];
			this.Ldloc(localBuilder);
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0004FCB5 File Offset: 0x0004DEB5
		internal void Ldloca(LocalBuilder localBuilder)
		{
			this.ilGen.Emit(OpCodes.Ldloca, localBuilder);
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0004FCC8 File Offset: 0x0004DEC8
		internal void LdargAddress(ArgBuilder argBuilder)
		{
			if (argBuilder.ArgType.IsValueType)
			{
				this.Ldarga(argBuilder);
				return;
			}
			this.Ldarg(argBuilder);
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0004FCE6 File Offset: 0x0004DEE6
		internal void Ldarg(string arg)
		{
			this.Ldarg(this.GetArg(arg));
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0004FCF5 File Offset: 0x0004DEF5
		internal void Ldarg(ArgBuilder arg)
		{
			this.Ldarg(arg.Index);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0004FD04 File Offset: 0x0004DF04
		internal void Ldarg(int slot)
		{
			switch (slot)
			{
			case 0:
				this.ilGen.Emit(OpCodes.Ldarg_0);
				return;
			case 1:
				this.ilGen.Emit(OpCodes.Ldarg_1);
				return;
			case 2:
				this.ilGen.Emit(OpCodes.Ldarg_2);
				return;
			case 3:
				this.ilGen.Emit(OpCodes.Ldarg_3);
				return;
			default:
				if (slot <= 255)
				{
					this.ilGen.Emit(OpCodes.Ldarg_S, slot);
					return;
				}
				this.ilGen.Emit(OpCodes.Ldarg, slot);
				return;
			}
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0004FD98 File Offset: 0x0004DF98
		internal void Ldarga(ArgBuilder argBuilder)
		{
			this.Ldarga(argBuilder.Index);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0004FDA6 File Offset: 0x0004DFA6
		internal void Ldarga(int slot)
		{
			if (slot <= 255)
			{
				this.ilGen.Emit(OpCodes.Ldarga_S, slot);
				return;
			}
			this.ilGen.Emit(OpCodes.Ldarga, slot);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0004FDD3 File Offset: 0x0004DFD3
		internal void Ldlen()
		{
			this.ilGen.Emit(OpCodes.Ldlen);
			this.ilGen.Emit(OpCodes.Conv_I4);
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0004FDF5 File Offset: 0x0004DFF5
		private OpCode GetLdelemOpCode(TypeCode typeCode)
		{
			return CodeGenerator.LdelemOpCodes[(int)typeCode];
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0004FE04 File Offset: 0x0004E004
		internal void Ldelem(Type arrayElementType)
		{
			if (arrayElementType.IsEnum)
			{
				this.Ldelem(Enum.GetUnderlyingType(arrayElementType));
				return;
			}
			OpCode ldelemOpCode = this.GetLdelemOpCode(Type.GetTypeCode(arrayElementType));
			if (ldelemOpCode.Equals(OpCodes.Nop))
			{
				throw new InvalidOperationException("ArrayTypeIsNotSupported");
			}
			this.ilGen.Emit(ldelemOpCode);
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0004FE58 File Offset: 0x0004E058
		internal void Ldelema(Type arrayElementType)
		{
			OpCode ldelema = OpCodes.Ldelema;
			this.ilGen.Emit(ldelema, arrayElementType);
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0004FE78 File Offset: 0x0004E078
		private OpCode GetStelemOpCode(TypeCode typeCode)
		{
			return CodeGenerator.StelemOpCodes[(int)typeCode];
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0004FE88 File Offset: 0x0004E088
		internal void Stelem(Type arrayElementType)
		{
			if (arrayElementType.IsEnum)
			{
				this.Stelem(Enum.GetUnderlyingType(arrayElementType));
				return;
			}
			OpCode stelemOpCode = this.GetStelemOpCode(Type.GetTypeCode(arrayElementType));
			if (stelemOpCode.Equals(OpCodes.Nop))
			{
				throw new InvalidOperationException("ArrayTypeIsNotSupported");
			}
			this.ilGen.Emit(stelemOpCode);
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0004FEDC File Offset: 0x0004E0DC
		internal Label DefineLabel()
		{
			return this.ilGen.DefineLabel();
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0004FEE9 File Offset: 0x0004E0E9
		internal void MarkLabel(Label label)
		{
			this.ilGen.MarkLabel(label);
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0004FEF7 File Offset: 0x0004E0F7
		internal void Nop()
		{
			this.ilGen.Emit(OpCodes.Nop);
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0004FF09 File Offset: 0x0004E109
		internal void Add()
		{
			this.ilGen.Emit(OpCodes.Add);
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0004FF1B File Offset: 0x0004E11B
		internal void Ret()
		{
			this.ilGen.Emit(OpCodes.Ret);
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0004FF2D File Offset: 0x0004E12D
		internal void Br(Label label)
		{
			this.ilGen.Emit(OpCodes.Br, label);
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0004FF40 File Offset: 0x0004E140
		internal void Br_S(Label label)
		{
			this.ilGen.Emit(OpCodes.Br_S, label);
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0004FF53 File Offset: 0x0004E153
		internal void Blt(Label label)
		{
			this.ilGen.Emit(OpCodes.Blt, label);
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0004FF66 File Offset: 0x0004E166
		internal void Brfalse(Label label)
		{
			this.ilGen.Emit(OpCodes.Brfalse, label);
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0004FF79 File Offset: 0x0004E179
		internal void Brtrue(Label label)
		{
			this.ilGen.Emit(OpCodes.Brtrue, label);
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0004FF8C File Offset: 0x0004E18C
		internal void Pop()
		{
			this.ilGen.Emit(OpCodes.Pop);
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0004FF9E File Offset: 0x0004E19E
		internal void Dup()
		{
			this.ilGen.Emit(OpCodes.Dup);
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0004FFB0 File Offset: 0x0004E1B0
		internal void Ldftn(MethodInfo methodInfo)
		{
			this.ilGen.Emit(OpCodes.Ldftn, methodInfo);
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0004FFC4 File Offset: 0x0004E1C4
		private void InternalIf(bool negate)
		{
			IfState ifState = new IfState();
			ifState.EndIf = this.DefineLabel();
			ifState.ElseBegin = this.DefineLabel();
			if (negate)
			{
				this.Brtrue(ifState.ElseBegin);
			}
			else
			{
				this.Brfalse(ifState.ElseBegin);
			}
			this.blockStack.Push(ifState);
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00050018 File Offset: 0x0004E218
		private OpCode GetConvOpCode(TypeCode typeCode)
		{
			return CodeGenerator.ConvOpCodes[(int)typeCode];
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00050028 File Offset: 0x0004E228
		private void InternalConvert(Type source, Type target, bool isAddress)
		{
			if (target == source)
			{
				return;
			}
			if (target.IsValueType)
			{
				if (source.IsValueType)
				{
					OpCode convOpCode = this.GetConvOpCode(Type.GetTypeCode(target));
					if (convOpCode.Equals(OpCodes.Nop))
					{
						throw new CodeGeneratorConversionException(source, target, isAddress, "NoConversionPossibleTo");
					}
					this.ilGen.Emit(convOpCode);
					return;
				}
				else
				{
					if (!source.IsAssignableFrom(target))
					{
						throw new CodeGeneratorConversionException(source, target, isAddress, "IsNotAssignableFrom");
					}
					this.Unbox(target);
					if (!isAddress)
					{
						this.Ldobj(target);
						return;
					}
				}
			}
			else if (target.IsAssignableFrom(source))
			{
				if (source.IsValueType)
				{
					if (isAddress)
					{
						this.Ldobj(source);
					}
					this.Box(source);
					return;
				}
			}
			else
			{
				if (source.IsAssignableFrom(target))
				{
					this.Castclass(target);
					return;
				}
				if (target.IsInterface || source.IsInterface)
				{
					this.Castclass(target);
					return;
				}
				throw new CodeGeneratorConversionException(source, target, isAddress, "IsNotAssignableFrom");
			}
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00050108 File Offset: 0x0004E308
		private IfState PopIfState()
		{
			return this.blockStack.Pop() as IfState;
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0005011C File Offset: 0x0004E31C
		internal static AssemblyBuilder CreateAssemblyBuilder(AppDomain appDomain, string name)
		{
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Name = name;
			assemblyName.Version = new Version(1, 0, 0, 0);
			if (DiagnosticsSwitches.KeepTempFiles.Enabled)
			{
				return appDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave, CodeGenerator.TempFilesLocation);
			}
			return appDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x00050168 File Offset: 0x0004E368
		// (set) Token: 0x0600105F RID: 4191 RVA: 0x000501BC File Offset: 0x0004E3BC
		internal static string TempFilesLocation
		{
			get
			{
				if (CodeGenerator.tempFilesLocation == null)
				{
					object section = ConfigurationManager.GetSection(ConfigurationStrings.XmlSerializerSectionPath);
					string text = null;
					if (section != null)
					{
						XmlSerializerSection xmlSerializerSection = section as XmlSerializerSection;
						if (xmlSerializerSection != null)
						{
							text = xmlSerializerSection.TempFilesLocation;
						}
					}
					if (text != null)
					{
						CodeGenerator.tempFilesLocation = text.Trim();
					}
					else
					{
						CodeGenerator.tempFilesLocation = Path.GetTempPath();
					}
				}
				return CodeGenerator.tempFilesLocation;
			}
			set
			{
				CodeGenerator.tempFilesLocation = value;
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x000501C4 File Offset: 0x0004E3C4
		internal static ModuleBuilder CreateModuleBuilder(AssemblyBuilder assemblyBuilder, string name)
		{
			if (DiagnosticsSwitches.KeepTempFiles.Enabled)
			{
				return assemblyBuilder.DefineDynamicModule(name, name + ".dll", true);
			}
			return assemblyBuilder.DefineDynamicModule(name);
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x000501ED File Offset: 0x0004E3ED
		internal static TypeBuilder CreateTypeBuilder(ModuleBuilder moduleBuilder, string name, TypeAttributes attributes, Type parent, Type[] interfaces)
		{
			return moduleBuilder.DefineType("Microsoft.Xml.Serialization.GeneratedAssembly." + name, attributes, parent, interfaces);
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00050204 File Offset: 0x0004E404
		internal void InitElseIf()
		{
			this.elseIfState = (IfState)this.blockStack.Pop();
			this.initElseIfStack = this.blockStack.Count;
			this.Br(this.elseIfState.EndIf);
			this.MarkLabel(this.elseIfState.ElseBegin);
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0005025A File Offset: 0x0004E45A
		internal void InitIf()
		{
			this.initIfStack = this.blockStack.Count;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x00050270 File Offset: 0x0004E470
		internal void AndIf(Cmp cmpOp)
		{
			if (this.initIfStack == this.blockStack.Count)
			{
				this.initIfStack = -1;
				this.If(cmpOp);
				return;
			}
			if (this.initElseIfStack == this.blockStack.Count)
			{
				this.initElseIfStack = -1;
				this.elseIfState.ElseBegin = this.DefineLabel();
				this.ilGen.Emit(this.GetBranchCode(cmpOp), this.elseIfState.ElseBegin);
				this.blockStack.Push(this.elseIfState);
				return;
			}
			IfState ifState = (IfState)this.blockStack.Peek();
			this.ilGen.Emit(this.GetBranchCode(cmpOp), ifState.ElseBegin);
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x00050324 File Offset: 0x0004E524
		internal void AndIf()
		{
			if (this.initIfStack == this.blockStack.Count)
			{
				this.initIfStack = -1;
				this.If();
				return;
			}
			if (this.initElseIfStack == this.blockStack.Count)
			{
				this.initElseIfStack = -1;
				this.elseIfState.ElseBegin = this.DefineLabel();
				this.Brfalse(this.elseIfState.ElseBegin);
				this.blockStack.Push(this.elseIfState);
				return;
			}
			IfState ifState = (IfState)this.blockStack.Peek();
			this.Brfalse(ifState.ElseBegin);
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x000503BD File Offset: 0x0004E5BD
		internal void IsInst(Type type)
		{
			this.ilGen.Emit(OpCodes.Isinst, type);
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x000503D0 File Offset: 0x0004E5D0
		internal void Beq(Label label)
		{
			this.ilGen.Emit(OpCodes.Beq, label);
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x000503E3 File Offset: 0x0004E5E3
		internal void Bne(Label label)
		{
			this.ilGen.Emit(OpCodes.Bne_Un, label);
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x000503F6 File Offset: 0x0004E5F6
		internal void GotoMethodEnd()
		{
			this.Br(this.methodEndLabel);
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x00050404 File Offset: 0x0004E604
		internal void WhileBegin()
		{
			CodeGenerator.WhileState whileState = new CodeGenerator.WhileState(this);
			this.Br(whileState.CondLabel);
			this.MarkLabel(whileState.StartLabel);
			this.whileStack.Push(whileState);
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0005043C File Offset: 0x0004E63C
		internal void WhileEnd()
		{
			CodeGenerator.WhileState whileState = (CodeGenerator.WhileState)this.whileStack.Pop();
			this.MarkLabel(whileState.EndLabel);
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00050468 File Offset: 0x0004E668
		internal void WhileBreak()
		{
			CodeGenerator.WhileState whileState = (CodeGenerator.WhileState)this.whileStack.Peek();
			this.Br(whileState.EndLabel);
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x00050494 File Offset: 0x0004E694
		internal void WhileContinue()
		{
			CodeGenerator.WhileState whileState = (CodeGenerator.WhileState)this.whileStack.Peek();
			this.Br(whileState.CondLabel);
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x000504C0 File Offset: 0x0004E6C0
		internal void WhileBeginCondition()
		{
			CodeGenerator.WhileState whileState = (CodeGenerator.WhileState)this.whileStack.Peek();
			this.Nop();
			this.MarkLabel(whileState.CondLabel);
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x000504F0 File Offset: 0x0004E6F0
		internal void WhileEndCondition()
		{
			CodeGenerator.WhileState whileState = (CodeGenerator.WhileState)this.whileStack.Peek();
			this.Brtrue(whileState.StartLabel);
		}

		// Token: 0x040007C7 RID: 1991
		internal static BindingFlags InstancePublicBindingFlags = BindingFlags.Instance | BindingFlags.Public;

		// Token: 0x040007C8 RID: 1992
		internal static BindingFlags InstanceBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x040007C9 RID: 1993
		internal static BindingFlags StaticBindingFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x040007CA RID: 1994
		internal static MethodAttributes PublicMethodAttributes = MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig;

		// Token: 0x040007CB RID: 1995
		internal static MethodAttributes PublicOverrideMethodAttributes = MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig;

		// Token: 0x040007CC RID: 1996
		internal static MethodAttributes ProtectedOverrideMethodAttributes = MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig;

		// Token: 0x040007CD RID: 1997
		internal static MethodAttributes PrivateMethodAttributes = MethodAttributes.Private | MethodAttributes.HideBySig;

		// Token: 0x040007CE RID: 1998
		internal static Type[] EmptyTypeArray = new Type[0];

		// Token: 0x040007CF RID: 1999
		internal static string[] EmptyStringArray = new string[0];

		// Token: 0x040007D0 RID: 2000
		private TypeBuilder typeBuilder;

		// Token: 0x040007D1 RID: 2001
		private MethodBuilder methodBuilder;

		// Token: 0x040007D2 RID: 2002
		private ILGenerator ilGen;

		// Token: 0x040007D3 RID: 2003
		private Dictionary<string, ArgBuilder> argList;

		// Token: 0x040007D4 RID: 2004
		private LocalScope currentScope;

		// Token: 0x040007D5 RID: 2005
		private Dictionary<Tuple<Type, string>, Queue<LocalBuilder>> freeLocals;

		// Token: 0x040007D6 RID: 2006
		private Stack blockStack;

		// Token: 0x040007D7 RID: 2007
		private Label methodEndLabel;

		// Token: 0x040007D8 RID: 2008
		internal LocalBuilder retLocal;

		// Token: 0x040007D9 RID: 2009
		internal Label retLabel;

		// Token: 0x040007DA RID: 2010
		private Dictionary<Type, LocalBuilder> TmpLocals = new Dictionary<Type, LocalBuilder>();

		// Token: 0x040007DB RID: 2011
		private static OpCode[] BranchCodes = new OpCode[]
		{
			OpCodes.Bge,
			OpCodes.Bne_Un,
			OpCodes.Bgt,
			OpCodes.Ble,
			OpCodes.Beq,
			OpCodes.Blt
		};

		// Token: 0x040007DC RID: 2012
		private Stack leaveLabels = new Stack();

		// Token: 0x040007DD RID: 2013
		private static OpCode[] LdindOpCodes = new OpCode[]
		{
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Ldind_I1,
			OpCodes.Ldind_I2,
			OpCodes.Ldind_I1,
			OpCodes.Ldind_U1,
			OpCodes.Ldind_I2,
			OpCodes.Ldind_U2,
			OpCodes.Ldind_I4,
			OpCodes.Ldind_U4,
			OpCodes.Ldind_I8,
			OpCodes.Ldind_I8,
			OpCodes.Ldind_R4,
			OpCodes.Ldind_R8,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Ldind_Ref
		};

		// Token: 0x040007DE RID: 2014
		private static OpCode[] LdelemOpCodes = new OpCode[]
		{
			OpCodes.Nop,
			OpCodes.Ldelem_Ref,
			OpCodes.Ldelem_Ref,
			OpCodes.Ldelem_I1,
			OpCodes.Ldelem_I2,
			OpCodes.Ldelem_I1,
			OpCodes.Ldelem_U1,
			OpCodes.Ldelem_I2,
			OpCodes.Ldelem_U2,
			OpCodes.Ldelem_I4,
			OpCodes.Ldelem_U4,
			OpCodes.Ldelem_I8,
			OpCodes.Ldelem_I8,
			OpCodes.Ldelem_R4,
			OpCodes.Ldelem_R8,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Ldelem_Ref
		};

		// Token: 0x040007DF RID: 2015
		private static OpCode[] StelemOpCodes = new OpCode[]
		{
			OpCodes.Nop,
			OpCodes.Stelem_Ref,
			OpCodes.Stelem_Ref,
			OpCodes.Stelem_I1,
			OpCodes.Stelem_I2,
			OpCodes.Stelem_I1,
			OpCodes.Stelem_I1,
			OpCodes.Stelem_I2,
			OpCodes.Stelem_I2,
			OpCodes.Stelem_I4,
			OpCodes.Stelem_I4,
			OpCodes.Stelem_I8,
			OpCodes.Stelem_I8,
			OpCodes.Stelem_R4,
			OpCodes.Stelem_R8,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Stelem_Ref
		};

		// Token: 0x040007E0 RID: 2016
		private static OpCode[] ConvOpCodes = new OpCode[]
		{
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Conv_I1,
			OpCodes.Conv_I2,
			OpCodes.Conv_I1,
			OpCodes.Conv_U1,
			OpCodes.Conv_I2,
			OpCodes.Conv_U2,
			OpCodes.Conv_I4,
			OpCodes.Conv_U4,
			OpCodes.Conv_I8,
			OpCodes.Conv_U8,
			OpCodes.Conv_R4,
			OpCodes.Conv_R8,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop
		};

		// Token: 0x040007E1 RID: 2017
		private static string tempFilesLocation = null;

		// Token: 0x040007E2 RID: 2018
		private int initElseIfStack = -1;

		// Token: 0x040007E3 RID: 2019
		private IfState elseIfState;

		// Token: 0x040007E4 RID: 2020
		private int initIfStack = -1;

		// Token: 0x040007E5 RID: 2021
		private Stack whileStack;

		// Token: 0x02000146 RID: 326
		internal class WhileState
		{
			// Token: 0x06001071 RID: 4209 RVA: 0x000509BD File Offset: 0x0004EBBD
			public WhileState(CodeGenerator ilg)
			{
				this.StartLabel = ilg.DefineLabel();
				this.CondLabel = ilg.DefineLabel();
				this.EndLabel = ilg.DefineLabel();
			}

			// Token: 0x040007E6 RID: 2022
			public Label StartLabel;

			// Token: 0x040007E7 RID: 2023
			public Label CondLabel;

			// Token: 0x040007E8 RID: 2024
			public Label EndLabel;
		}
	}
}
