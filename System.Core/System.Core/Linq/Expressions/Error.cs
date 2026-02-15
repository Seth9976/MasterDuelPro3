using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000095 RID: 149
	internal static class Error
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x0001332D File Offset: 0x0001152D
		internal static Exception ReducibleMustOverrideReduce()
		{
			return new ArgumentException(Strings.ReducibleMustOverrideReduce);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00013339 File Offset: 0x00011539
		internal static Exception InvalidMetaObjectCreated(object p0)
		{
			return new InvalidOperationException(Strings.InvalidMetaObjectCreated(p0));
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00013346 File Offset: 0x00011546
		internal static Exception AmbiguousMatchInExpandoObject(object p0)
		{
			return new AmbiguousMatchException(Strings.AmbiguousMatchInExpandoObject(p0));
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00013353 File Offset: 0x00011553
		internal static Exception SameKeyExistsInExpando(object key)
		{
			return new ArgumentException(Strings.SameKeyExistsInExpando(key), "key");
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00013365 File Offset: 0x00011565
		internal static Exception KeyDoesNotExistInExpando(object p0)
		{
			return new KeyNotFoundException(Strings.KeyDoesNotExistInExpando(p0));
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00013372 File Offset: 0x00011572
		internal static Exception CollectionModifiedWhileEnumerating()
		{
			return new InvalidOperationException(Strings.CollectionModifiedWhileEnumerating);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0001337E File Offset: 0x0001157E
		internal static Exception CollectionReadOnly()
		{
			return new NotSupportedException(Strings.CollectionReadOnly);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001338A File Offset: 0x0001158A
		internal static Exception MustReduceToDifferent()
		{
			return new ArgumentException(Strings.MustReduceToDifferent);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00013396 File Offset: 0x00011596
		internal static Exception BinderNotCompatibleWithCallSite(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.BinderNotCompatibleWithCallSite(p0, p1, p2));
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000133A5 File Offset: 0x000115A5
		internal static Exception DynamicBindingNeedsRestrictions(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DynamicBindingNeedsRestrictions(p0, p1));
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000133B3 File Offset: 0x000115B3
		internal static Exception DynamicObjectResultNotAssignable(object p0, object p1, object p2, object p3)
		{
			return new InvalidCastException(Strings.DynamicObjectResultNotAssignable(p0, p1, p2, p3));
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x000133C3 File Offset: 0x000115C3
		internal static Exception DynamicBinderResultNotAssignable(object p0, object p1, object p2)
		{
			return new InvalidCastException(Strings.DynamicBinderResultNotAssignable(p0, p1, p2));
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000133D2 File Offset: 0x000115D2
		internal static Exception BindingCannotBeNull()
		{
			return new InvalidOperationException(Strings.BindingCannotBeNull);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000133DE File Offset: 0x000115DE
		internal static Exception ReducedNotCompatible()
		{
			return new ArgumentException(Strings.ReducedNotCompatible);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000133EA File Offset: 0x000115EA
		internal static Exception SetterHasNoParams(string paramName)
		{
			return new ArgumentException(Strings.SetterHasNoParams, paramName);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000133F7 File Offset: 0x000115F7
		internal static Exception PropertyCannotHaveRefType(string paramName)
		{
			return new ArgumentException(Strings.PropertyCannotHaveRefType, paramName);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00013404 File Offset: 0x00011604
		internal static Exception IndexesOfSetGetMustMatch(string paramName)
		{
			return new ArgumentException(Strings.IndexesOfSetGetMustMatch, paramName);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00013411 File Offset: 0x00011611
		internal static Exception TypeParameterIsNotDelegate(object p0)
		{
			return new InvalidOperationException(Strings.TypeParameterIsNotDelegate(p0));
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0001341E File Offset: 0x0001161E
		internal static Exception FirstArgumentMustBeCallSite()
		{
			return new ArgumentException(Strings.FirstArgumentMustBeCallSite);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0001342A File Offset: 0x0001162A
		internal static Exception AccessorsCannotHaveVarArgs(string paramName)
		{
			return new ArgumentException(Strings.AccessorsCannotHaveVarArgs, paramName);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00013437 File Offset: 0x00011637
		private static Exception AccessorsCannotHaveByRefArgs(string paramName)
		{
			return new ArgumentException(Strings.AccessorsCannotHaveByRefArgs, paramName);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00013444 File Offset: 0x00011644
		internal static Exception AccessorsCannotHaveByRefArgs(string paramName, int index)
		{
			return Error.AccessorsCannotHaveByRefArgs(Error.GetParamName(paramName, index));
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00013452 File Offset: 0x00011652
		internal static Exception TypeMustBeDerivedFromSystemDelegate()
		{
			return new ArgumentException(Strings.TypeMustBeDerivedFromSystemDelegate);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001345E File Offset: 0x0001165E
		internal static Exception NoOrInvalidRuleProduced()
		{
			return new InvalidOperationException(Strings.NoOrInvalidRuleProduced);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001346A File Offset: 0x0001166A
		internal static Exception BoundsCannotBeLessThanOne(string paramName)
		{
			return new ArgumentException(Strings.BoundsCannotBeLessThanOne, paramName);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00013477 File Offset: 0x00011677
		internal static Exception TypeMustNotBeByRef(string paramName)
		{
			return new ArgumentException(Strings.TypeMustNotBeByRef, paramName);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00013484 File Offset: 0x00011684
		internal static Exception TypeMustNotBePointer(string paramName)
		{
			return new ArgumentException(Strings.TypeMustNotBePointer, paramName);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00013491 File Offset: 0x00011691
		internal static Exception SetterMustBeVoid(string paramName)
		{
			return new ArgumentException(Strings.SetterMustBeVoid, paramName);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0001349E File Offset: 0x0001169E
		internal static Exception PropertyTypeMustMatchGetter(string paramName)
		{
			return new ArgumentException(Strings.PropertyTypeMustMatchGetter, paramName);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x000134AB File Offset: 0x000116AB
		internal static Exception PropertyTypeMustMatchSetter(string paramName)
		{
			return new ArgumentException(Strings.PropertyTypeMustMatchSetter, paramName);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x000134B8 File Offset: 0x000116B8
		internal static Exception BothAccessorsMustBeStatic(string paramName)
		{
			return new ArgumentException(Strings.BothAccessorsMustBeStatic, paramName);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x000134C5 File Offset: 0x000116C5
		internal static Exception OnlyStaticFieldsHaveNullInstance(string paramName)
		{
			return new ArgumentException(Strings.OnlyStaticFieldsHaveNullInstance, paramName);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000134D2 File Offset: 0x000116D2
		internal static Exception OnlyStaticPropertiesHaveNullInstance(string paramName)
		{
			return new ArgumentException(Strings.OnlyStaticPropertiesHaveNullInstance, paramName);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000134DF File Offset: 0x000116DF
		internal static Exception OnlyStaticMethodsHaveNullInstance()
		{
			return new ArgumentException(Strings.OnlyStaticMethodsHaveNullInstance);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000134EB File Offset: 0x000116EB
		internal static Exception PropertyTypeCannotBeVoid(string paramName)
		{
			return new ArgumentException(Strings.PropertyTypeCannotBeVoid, paramName);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000134F8 File Offset: 0x000116F8
		internal static Exception InvalidUnboxType(string paramName)
		{
			return new ArgumentException(Strings.InvalidUnboxType, paramName);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00013505 File Offset: 0x00011705
		internal static Exception ExpressionMustBeWriteable(string paramName)
		{
			return new ArgumentException(Strings.ExpressionMustBeWriteable, paramName);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00013512 File Offset: 0x00011712
		internal static Exception ArgumentMustNotHaveValueType(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustNotHaveValueType, paramName);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0001351F File Offset: 0x0001171F
		internal static Exception MustBeReducible()
		{
			return new ArgumentException(Strings.MustBeReducible);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001352B File Offset: 0x0001172B
		internal static Exception AllTestValuesMustHaveSameType(string paramName)
		{
			return new ArgumentException(Strings.AllTestValuesMustHaveSameType, paramName);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00013538 File Offset: 0x00011738
		internal static Exception AllCaseBodiesMustHaveSameType(string paramName)
		{
			return new ArgumentException(Strings.AllCaseBodiesMustHaveSameType, paramName);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00013545 File Offset: 0x00011745
		internal static Exception DefaultBodyMustBeSupplied(string paramName)
		{
			return new ArgumentException(Strings.DefaultBodyMustBeSupplied, paramName);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00013552 File Offset: 0x00011752
		internal static Exception LabelMustBeVoidOrHaveExpression(string paramName)
		{
			return new ArgumentException(Strings.LabelMustBeVoidOrHaveExpression, paramName);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0001355F File Offset: 0x0001175F
		internal static Exception LabelTypeMustBeVoid(string paramName)
		{
			return new ArgumentException(Strings.LabelTypeMustBeVoid, paramName);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001356C File Offset: 0x0001176C
		internal static Exception QuotedExpressionMustBeLambda(string paramName)
		{
			return new ArgumentException(Strings.QuotedExpressionMustBeLambda, paramName);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00013579 File Offset: 0x00011779
		internal static Exception VariableMustNotBeByRef(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.VariableMustNotBeByRef(p0, p1), paramName);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00013588 File Offset: 0x00011788
		internal static Exception VariableMustNotBeByRef(object p0, object p1, string paramName, int index)
		{
			return Error.VariableMustNotBeByRef(p0, p1, Error.GetParamName(paramName, index));
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00013598 File Offset: 0x00011798
		private static Exception DuplicateVariable(object p0, string paramName)
		{
			return new ArgumentException(Strings.DuplicateVariable(p0), paramName);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x000135A6 File Offset: 0x000117A6
		internal static Exception DuplicateVariable(object p0, string paramName, int index)
		{
			return Error.DuplicateVariable(p0, Error.GetParamName(paramName, index));
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000135B5 File Offset: 0x000117B5
		internal static Exception FaultCannotHaveCatchOrFinally(string paramName)
		{
			return new ArgumentException(Strings.FaultCannotHaveCatchOrFinally, paramName);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x000135C2 File Offset: 0x000117C2
		internal static Exception TryMustHaveCatchFinallyOrFault()
		{
			return new ArgumentException(Strings.TryMustHaveCatchFinallyOrFault);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000135CE File Offset: 0x000117CE
		internal static Exception BodyOfCatchMustHaveSameTypeAsBodyOfTry()
		{
			return new ArgumentException(Strings.BodyOfCatchMustHaveSameTypeAsBodyOfTry);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x000135DA File Offset: 0x000117DA
		internal static Exception ExtensionNodeMustOverrideProperty(object p0)
		{
			return new InvalidOperationException(Strings.ExtensionNodeMustOverrideProperty(p0));
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000135E7 File Offset: 0x000117E7
		internal static Exception UserDefinedOperatorMustBeStatic(object p0, string paramName)
		{
			return new ArgumentException(Strings.UserDefinedOperatorMustBeStatic(p0), paramName);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x000135F5 File Offset: 0x000117F5
		internal static Exception UserDefinedOperatorMustNotBeVoid(object p0, string paramName)
		{
			return new ArgumentException(Strings.UserDefinedOperatorMustNotBeVoid(p0), paramName);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00013603 File Offset: 0x00011803
		internal static Exception CoercionOperatorNotDefined(object p0, object p1)
		{
			return new InvalidOperationException(Strings.CoercionOperatorNotDefined(p0, p1));
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00013611 File Offset: 0x00011811
		internal static Exception UnaryOperatorNotDefined(object p0, object p1)
		{
			return new InvalidOperationException(Strings.UnaryOperatorNotDefined(p0, p1));
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001361F File Offset: 0x0001181F
		internal static Exception BinaryOperatorNotDefined(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.BinaryOperatorNotDefined(p0, p1, p2));
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001362E File Offset: 0x0001182E
		internal static Exception ReferenceEqualityNotDefined(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ReferenceEqualityNotDefined(p0, p1));
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001363C File Offset: 0x0001183C
		internal static Exception OperandTypesDoNotMatchParameters(object p0, object p1)
		{
			return new InvalidOperationException(Strings.OperandTypesDoNotMatchParameters(p0, p1));
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001364A File Offset: 0x0001184A
		internal static Exception OverloadOperatorTypeDoesNotMatchConversionType(object p0, object p1)
		{
			return new InvalidOperationException(Strings.OverloadOperatorTypeDoesNotMatchConversionType(p0, p1));
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00013658 File Offset: 0x00011858
		internal static Exception ConversionIsNotSupportedForArithmeticTypes()
		{
			return new InvalidOperationException(Strings.ConversionIsNotSupportedForArithmeticTypes);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00013664 File Offset: 0x00011864
		internal static Exception ArgumentMustBeArray(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeArray, paramName);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00013671 File Offset: 0x00011871
		internal static Exception ArgumentMustBeBoolean(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeBoolean, paramName);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001367E File Offset: 0x0001187E
		internal static Exception EqualityMustReturnBoolean(object p0, string paramName)
		{
			return new ArgumentException(Strings.EqualityMustReturnBoolean(p0), paramName);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001368C File Offset: 0x0001188C
		internal static Exception ArgumentMustBeFieldInfoOrPropertyInfo(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeFieldInfoOrPropertyInfo, paramName);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00013699 File Offset: 0x00011899
		private static Exception ArgumentMustBeFieldInfoOrPropertyInfoOrMethod(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeFieldInfoOrPropertyInfoOrMethod, paramName);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000136A6 File Offset: 0x000118A6
		internal static Exception ArgumentMustBeFieldInfoOrPropertyInfoOrMethod(string paramName, int index)
		{
			return Error.ArgumentMustBeFieldInfoOrPropertyInfoOrMethod(Error.GetParamName(paramName, index));
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000136B4 File Offset: 0x000118B4
		private static Exception ArgumentMustBeInstanceMember(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeInstanceMember, paramName);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000136C1 File Offset: 0x000118C1
		internal static Exception ArgumentMustBeInstanceMember(string paramName, int index)
		{
			return Error.ArgumentMustBeInstanceMember(Error.GetParamName(paramName, index));
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000136CF File Offset: 0x000118CF
		private static Exception ArgumentMustBeInteger(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeInteger, paramName);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000136DC File Offset: 0x000118DC
		internal static Exception ArgumentMustBeInteger(string paramName, int index)
		{
			return Error.ArgumentMustBeInteger(Error.GetParamName(paramName, index));
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000136EA File Offset: 0x000118EA
		internal static Exception ArgumentMustBeArrayIndexType(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeArrayIndexType, paramName);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000136F7 File Offset: 0x000118F7
		internal static Exception ArgumentMustBeSingleDimensionalArrayType(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeSingleDimensionalArrayType, paramName);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00013704 File Offset: 0x00011904
		internal static Exception ArgumentTypesMustMatch()
		{
			return new ArgumentException(Strings.ArgumentTypesMustMatch);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00013710 File Offset: 0x00011910
		internal static Exception ArgumentTypesMustMatch(string paramName)
		{
			return new ArgumentException(Strings.ArgumentTypesMustMatch, paramName);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001371D File Offset: 0x0001191D
		internal static Exception CannotAutoInitializeValueTypeElementThroughProperty(object p0)
		{
			return new InvalidOperationException(Strings.CannotAutoInitializeValueTypeElementThroughProperty(p0));
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001372A File Offset: 0x0001192A
		internal static Exception CannotAutoInitializeValueTypeMemberThroughProperty(object p0)
		{
			return new InvalidOperationException(Strings.CannotAutoInitializeValueTypeMemberThroughProperty(p0));
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00013737 File Offset: 0x00011937
		internal static Exception IncorrectTypeForTypeAs(object p0, string paramName)
		{
			return new ArgumentException(Strings.IncorrectTypeForTypeAs(p0), paramName);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00013745 File Offset: 0x00011945
		internal static Exception CoalesceUsedOnNonNullType()
		{
			return new InvalidOperationException(Strings.CoalesceUsedOnNonNullType);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00013751 File Offset: 0x00011951
		internal static Exception ExpressionTypeCannotInitializeArrayType(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ExpressionTypeCannotInitializeArrayType(p0, p1));
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001375F File Offset: 0x0001195F
		private static Exception ArgumentTypeDoesNotMatchMember(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.ArgumentTypeDoesNotMatchMember(p0, p1), paramName);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001376E File Offset: 0x0001196E
		internal static Exception ArgumentTypeDoesNotMatchMember(object p0, object p1, string paramName, int index)
		{
			return Error.ArgumentTypeDoesNotMatchMember(p0, p1, Error.GetParamName(paramName, index));
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001377E File Offset: 0x0001197E
		private static Exception ArgumentMemberNotDeclOnType(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.ArgumentMemberNotDeclOnType(p0, p1), paramName);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001378D File Offset: 0x0001198D
		internal static Exception ArgumentMemberNotDeclOnType(object p0, object p1, string paramName, int index)
		{
			return Error.ArgumentMemberNotDeclOnType(p0, p1, Error.GetParamName(paramName, index));
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0001379D File Offset: 0x0001199D
		internal static Exception ExpressionTypeDoesNotMatchReturn(object p0, object p1)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchReturn(p0, p1));
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000137AB File Offset: 0x000119AB
		internal static Exception ExpressionTypeDoesNotMatchAssignment(object p0, object p1)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchAssignment(p0, p1));
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x000137B9 File Offset: 0x000119B9
		internal static Exception ExpressionTypeDoesNotMatchLabel(object p0, object p1)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchLabel(p0, p1));
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000137C7 File Offset: 0x000119C7
		internal static Exception ExpressionTypeNotInvocable(object p0, string paramName)
		{
			return new ArgumentException(Strings.ExpressionTypeNotInvocable(p0), paramName);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000137D5 File Offset: 0x000119D5
		internal static Exception InstanceFieldNotDefinedForType(object p0, object p1)
		{
			return new ArgumentException(Strings.InstanceFieldNotDefinedForType(p0, p1));
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000137E3 File Offset: 0x000119E3
		internal static Exception FieldInfoNotDefinedForType(object p0, object p1, object p2)
		{
			return new ArgumentException(Strings.FieldInfoNotDefinedForType(p0, p1, p2));
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x000137F2 File Offset: 0x000119F2
		internal static Exception IncorrectNumberOfIndexes()
		{
			return new ArgumentException(Strings.IncorrectNumberOfIndexes);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x000137FE File Offset: 0x000119FE
		internal static Exception IncorrectNumberOfLambdaDeclarationParameters()
		{
			return new ArgumentException(Strings.IncorrectNumberOfLambdaDeclarationParameters);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001380A File Offset: 0x00011A0A
		internal static Exception IncorrectNumberOfMembersForGivenConstructor()
		{
			return new ArgumentException(Strings.IncorrectNumberOfMembersForGivenConstructor);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00013816 File Offset: 0x00011A16
		internal static Exception IncorrectNumberOfArgumentsForMembers()
		{
			return new ArgumentException(Strings.IncorrectNumberOfArgumentsForMembers);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00013822 File Offset: 0x00011A22
		internal static Exception LambdaTypeMustBeDerivedFromSystemDelegate(string paramName)
		{
			return new ArgumentException(Strings.LambdaTypeMustBeDerivedFromSystemDelegate, paramName);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001382F File Offset: 0x00011A2F
		internal static Exception MemberNotFieldOrProperty(object p0, string paramName)
		{
			return new ArgumentException(Strings.MemberNotFieldOrProperty(p0), paramName);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001383D File Offset: 0x00011A3D
		internal static Exception MethodContainsGenericParameters(object p0, string paramName)
		{
			return new ArgumentException(Strings.MethodContainsGenericParameters(p0), paramName);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001384B File Offset: 0x00011A4B
		internal static Exception MethodIsGeneric(object p0, string paramName)
		{
			return new ArgumentException(Strings.MethodIsGeneric(p0), paramName);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00013859 File Offset: 0x00011A59
		private static Exception MethodNotPropertyAccessor(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.MethodNotPropertyAccessor(p0, p1), paramName);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00013868 File Offset: 0x00011A68
		internal static Exception MethodNotPropertyAccessor(object p0, object p1, string paramName, int index)
		{
			return Error.MethodNotPropertyAccessor(p0, p1, Error.GetParamName(paramName, index));
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00013878 File Offset: 0x00011A78
		internal static Exception PropertyDoesNotHaveGetter(object p0, string paramName)
		{
			return new ArgumentException(Strings.PropertyDoesNotHaveGetter(p0), paramName);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00013886 File Offset: 0x00011A86
		internal static Exception PropertyDoesNotHaveGetter(object p0, string paramName, int index)
		{
			return Error.PropertyDoesNotHaveGetter(p0, Error.GetParamName(paramName, index));
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00013895 File Offset: 0x00011A95
		internal static Exception PropertyDoesNotHaveSetter(object p0, string paramName)
		{
			return new ArgumentException(Strings.PropertyDoesNotHaveSetter(p0), paramName);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000138A3 File Offset: 0x00011AA3
		internal static Exception PropertyDoesNotHaveAccessor(object p0, string paramName)
		{
			return new ArgumentException(Strings.PropertyDoesNotHaveAccessor(p0), paramName);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000138B1 File Offset: 0x00011AB1
		internal static Exception NotAMemberOfType(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.NotAMemberOfType(p0, p1), paramName);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000138C0 File Offset: 0x00011AC0
		internal static Exception NotAMemberOfType(object p0, object p1, string paramName, int index)
		{
			return Error.NotAMemberOfType(p0, p1, Error.GetParamName(paramName, index));
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x000138D0 File Offset: 0x00011AD0
		internal static Exception NotAMemberOfAnyType(object p0, string paramName)
		{
			return new ArgumentException(Strings.NotAMemberOfAnyType(p0), paramName);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000138DE File Offset: 0x00011ADE
		internal static Exception ParameterExpressionNotValidAsDelegate(object p0, object p1)
		{
			return new ArgumentException(Strings.ParameterExpressionNotValidAsDelegate(p0, p1));
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000138EC File Offset: 0x00011AEC
		internal static Exception PropertyNotDefinedForType(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.PropertyNotDefinedForType(p0, p1), paramName);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x000138FB File Offset: 0x00011AFB
		internal static Exception InstancePropertyNotDefinedForType(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.InstancePropertyNotDefinedForType(p0, p1), paramName);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001390A File Offset: 0x00011B0A
		internal static Exception InstanceAndMethodTypeMismatch(object p0, object p1, object p2)
		{
			return new ArgumentException(Strings.InstanceAndMethodTypeMismatch(p0, p1, p2));
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00013919 File Offset: 0x00011B19
		internal static Exception ElementInitializerMethodNotAdd(string paramName)
		{
			return new ArgumentException(Strings.ElementInitializerMethodNotAdd, paramName);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00013926 File Offset: 0x00011B26
		internal static Exception ElementInitializerMethodNoRefOutParam(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.ElementInitializerMethodNoRefOutParam(p0, p1), paramName);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00013935 File Offset: 0x00011B35
		internal static Exception ElementInitializerMethodWithZeroArgs(string paramName)
		{
			return new ArgumentException(Strings.ElementInitializerMethodWithZeroArgs, paramName);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00013942 File Offset: 0x00011B42
		internal static Exception ElementInitializerMethodStatic(string paramName)
		{
			return new ArgumentException(Strings.ElementInitializerMethodStatic, paramName);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001394F File Offset: 0x00011B4F
		internal static Exception TypeNotIEnumerable(object p0, string paramName)
		{
			return new ArgumentException(Strings.TypeNotIEnumerable(p0), paramName);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001395D File Offset: 0x00011B5D
		internal static Exception UnhandledBinary(object p0, string paramName)
		{
			return new ArgumentException(Strings.UnhandledBinary(p0), paramName);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001396B File Offset: 0x00011B6B
		internal static Exception UnhandledBinding()
		{
			return new ArgumentException(Strings.UnhandledBinding);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00013977 File Offset: 0x00011B77
		internal static Exception UnhandledBindingType(object p0)
		{
			return new ArgumentException(Strings.UnhandledBindingType(p0));
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00013984 File Offset: 0x00011B84
		internal static Exception UnhandledUnary(object p0, string paramName)
		{
			return new ArgumentException(Strings.UnhandledUnary(p0), paramName);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00013992 File Offset: 0x00011B92
		internal static Exception UnknownBindingType(int index)
		{
			return new ArgumentException(Strings.UnknownBindingType, string.Format("bindings[{0}]", index));
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x000139AE File Offset: 0x00011BAE
		internal static Exception UserDefinedOpMustHaveConsistentTypes(object p0, object p1)
		{
			return new ArgumentException(Strings.UserDefinedOpMustHaveConsistentTypes(p0, p1));
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000139BC File Offset: 0x00011BBC
		internal static Exception UserDefinedOpMustHaveValidReturnType(object p0, object p1)
		{
			return new ArgumentException(Strings.UserDefinedOpMustHaveValidReturnType(p0, p1));
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000139CA File Offset: 0x00011BCA
		internal static Exception LogicalOperatorMustHaveBooleanOperators(object p0, object p1)
		{
			return new ArgumentException(Strings.LogicalOperatorMustHaveBooleanOperators(p0, p1));
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000139D8 File Offset: 0x00011BD8
		internal static Exception MethodWithArgsDoesNotExistOnType(object p0, object p1)
		{
			return new InvalidOperationException(Strings.MethodWithArgsDoesNotExistOnType(p0, p1));
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x000139E6 File Offset: 0x00011BE6
		internal static Exception GenericMethodWithArgsDoesNotExistOnType(object p0, object p1)
		{
			return new InvalidOperationException(Strings.GenericMethodWithArgsDoesNotExistOnType(p0, p1));
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000139F4 File Offset: 0x00011BF4
		internal static Exception MethodWithMoreThanOneMatch(object p0, object p1)
		{
			return new InvalidOperationException(Strings.MethodWithMoreThanOneMatch(p0, p1));
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00013A02 File Offset: 0x00011C02
		internal static Exception ArgumentCannotBeOfTypeVoid(string paramName)
		{
			return new ArgumentException(Strings.ArgumentCannotBeOfTypeVoid, paramName);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00013A0F File Offset: 0x00011C0F
		internal static Exception OutOfRange(string paramName, object p1)
		{
			return new ArgumentOutOfRangeException(paramName, Strings.OutOfRange(paramName, p1));
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00013A1E File Offset: 0x00011C1E
		internal static Exception LabelTargetAlreadyDefined(object p0)
		{
			return new InvalidOperationException(Strings.LabelTargetAlreadyDefined(p0));
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00013A2B File Offset: 0x00011C2B
		internal static Exception LabelTargetUndefined(object p0)
		{
			return new InvalidOperationException(Strings.LabelTargetUndefined(p0));
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00013A38 File Offset: 0x00011C38
		internal static Exception ControlCannotLeaveFinally()
		{
			return new InvalidOperationException(Strings.ControlCannotLeaveFinally);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00013A44 File Offset: 0x00011C44
		internal static Exception ControlCannotLeaveFilterTest()
		{
			return new InvalidOperationException(Strings.ControlCannotLeaveFilterTest);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00013A50 File Offset: 0x00011C50
		internal static Exception AmbiguousJump(object p0)
		{
			return new InvalidOperationException(Strings.AmbiguousJump(p0));
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00013A5D File Offset: 0x00011C5D
		internal static Exception ControlCannotEnterTry()
		{
			return new InvalidOperationException(Strings.ControlCannotEnterTry);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00013A69 File Offset: 0x00011C69
		internal static Exception ControlCannotEnterExpression()
		{
			return new InvalidOperationException(Strings.ControlCannotEnterExpression);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00013A75 File Offset: 0x00011C75
		internal static Exception NonLocalJumpWithValue(object p0)
		{
			return new InvalidOperationException(Strings.NonLocalJumpWithValue(p0));
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00013A82 File Offset: 0x00011C82
		internal static Exception CannotCompileConstant(object p0)
		{
			return new InvalidOperationException(Strings.CannotCompileConstant(p0));
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00013A8F File Offset: 0x00011C8F
		internal static Exception CannotCompileDynamic()
		{
			return new NotSupportedException(Strings.CannotCompileDynamic);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00013A9B File Offset: 0x00011C9B
		internal static Exception InvalidLvalue(ExpressionType p0)
		{
			return new InvalidOperationException(Strings.InvalidLvalue(p0));
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00013AAD File Offset: 0x00011CAD
		internal static Exception UndefinedVariable(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.UndefinedVariable(p0, p1, p2));
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00013ABC File Offset: 0x00011CBC
		internal static Exception CannotCloseOverByRef(object p0, object p1)
		{
			return new InvalidOperationException(Strings.CannotCloseOverByRef(p0, p1));
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00013ACA File Offset: 0x00011CCA
		internal static Exception UnexpectedVarArgsCall(object p0)
		{
			return new InvalidOperationException(Strings.UnexpectedVarArgsCall(p0));
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00013AD7 File Offset: 0x00011CD7
		internal static Exception RethrowRequiresCatch()
		{
			return new InvalidOperationException(Strings.RethrowRequiresCatch);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00013AE3 File Offset: 0x00011CE3
		internal static Exception TryNotAllowedInFilter()
		{
			return new InvalidOperationException(Strings.TryNotAllowedInFilter);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00013AEF File Offset: 0x00011CEF
		internal static Exception MustRewriteToSameNode(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.MustRewriteToSameNode(p0, p1, p2));
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00013AFE File Offset: 0x00011CFE
		internal static Exception MustRewriteChildToSameType(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.MustRewriteChildToSameType(p0, p1, p2));
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00013B0D File Offset: 0x00011D0D
		internal static Exception MustRewriteWithoutMethod(object p0, object p1)
		{
			return new InvalidOperationException(Strings.MustRewriteWithoutMethod(p0, p1));
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00013B1B File Offset: 0x00011D1B
		internal static Exception TryNotSupportedForMethodsWithRefArgs(object p0)
		{
			return new NotSupportedException(Strings.TryNotSupportedForMethodsWithRefArgs(p0));
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00013B28 File Offset: 0x00011D28
		internal static Exception TryNotSupportedForValueTypeInstances(object p0)
		{
			return new NotSupportedException(Strings.TryNotSupportedForValueTypeInstances(p0));
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00013B35 File Offset: 0x00011D35
		internal static Exception TestValueTypeDoesNotMatchComparisonMethodParameter(object p0, object p1)
		{
			return new ArgumentException(Strings.TestValueTypeDoesNotMatchComparisonMethodParameter(p0, p1));
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00013B43 File Offset: 0x00011D43
		internal static Exception SwitchValueTypeDoesNotMatchComparisonMethodParameter(object p0, object p1)
		{
			return new ArgumentException(Strings.SwitchValueTypeDoesNotMatchComparisonMethodParameter(p0, p1));
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00004358 File Offset: 0x00002558
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00013B51 File Offset: 0x00011D51
		internal static Exception NonStaticConstructorRequired(string paramName)
		{
			return new ArgumentException(Strings.NonStaticConstructorRequired, paramName);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00013B5E File Offset: 0x00011D5E
		internal static Exception NonAbstractConstructorRequired()
		{
			return new InvalidOperationException(Strings.NonAbstractConstructorRequired);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00013B6A File Offset: 0x00011D6A
		internal static Exception InvalidProgram()
		{
			return new InvalidProgramException();
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00013B71 File Offset: 0x00011D71
		internal static Exception EnumerationIsDone()
		{
			return new InvalidOperationException(Strings.EnumerationIsDone);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00013B7D File Offset: 0x00011D7D
		private static Exception TypeContainsGenericParameters(object p0, string paramName)
		{
			return new ArgumentException(Strings.TypeContainsGenericParameters(p0), paramName);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00013B8B File Offset: 0x00011D8B
		internal static Exception TypeContainsGenericParameters(object p0, string paramName, int index)
		{
			return Error.TypeContainsGenericParameters(p0, Error.GetParamName(paramName, index));
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00013B9A File Offset: 0x00011D9A
		internal static Exception TypeIsGeneric(object p0, string paramName)
		{
			return new ArgumentException(Strings.TypeIsGeneric(p0), paramName);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00013BA8 File Offset: 0x00011DA8
		internal static Exception TypeIsGeneric(object p0, string paramName, int index)
		{
			return Error.TypeIsGeneric(p0, Error.GetParamName(paramName, index));
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00013BB7 File Offset: 0x00011DB7
		internal static Exception IncorrectNumberOfConstructorArguments()
		{
			return new ArgumentException(Strings.IncorrectNumberOfConstructorArguments);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00013BC3 File Offset: 0x00011DC3
		internal static Exception ExpressionTypeDoesNotMatchMethodParameter(object p0, object p1, object p2, string paramName)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchMethodParameter(p0, p1, p2), paramName);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00013BD3 File Offset: 0x00011DD3
		internal static Exception ExpressionTypeDoesNotMatchMethodParameter(object p0, object p1, object p2, string paramName, int index)
		{
			return Error.ExpressionTypeDoesNotMatchMethodParameter(p0, p1, p2, Error.GetParamName(paramName, index));
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00013BE5 File Offset: 0x00011DE5
		internal static Exception ExpressionTypeDoesNotMatchParameter(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchParameter(p0, p1), paramName);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00013BF4 File Offset: 0x00011DF4
		internal static Exception ExpressionTypeDoesNotMatchParameter(object p0, object p1, string paramName, int index)
		{
			return Error.ExpressionTypeDoesNotMatchParameter(p0, p1, Error.GetParamName(paramName, index));
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00013C04 File Offset: 0x00011E04
		internal static Exception IncorrectNumberOfLambdaArguments()
		{
			return new InvalidOperationException(Strings.IncorrectNumberOfLambdaArguments);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00013C10 File Offset: 0x00011E10
		internal static Exception IncorrectNumberOfMethodCallArguments(object p0, string paramName)
		{
			return new ArgumentException(Strings.IncorrectNumberOfMethodCallArguments(p0), paramName);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00013C1E File Offset: 0x00011E1E
		internal static Exception ExpressionTypeDoesNotMatchConstructorParameter(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchConstructorParameter(p0, p1), paramName);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00013C2D File Offset: 0x00011E2D
		internal static Exception ExpressionTypeDoesNotMatchConstructorParameter(object p0, object p1, string paramName, int index)
		{
			return Error.ExpressionTypeDoesNotMatchConstructorParameter(p0, p1, Error.GetParamName(paramName, index));
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00013C3D File Offset: 0x00011E3D
		internal static Exception ExpressionMustBeReadable(string paramName)
		{
			return new ArgumentException(Strings.ExpressionMustBeReadable, paramName);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00013C4A File Offset: 0x00011E4A
		internal static Exception ExpressionMustBeReadable(string paramName, int index)
		{
			return Error.ExpressionMustBeReadable(Error.GetParamName(paramName, index));
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00013C58 File Offset: 0x00011E58
		internal static Exception InvalidArgumentValue(string paramName)
		{
			return new ArgumentException(Strings.InvalidArgumentValue, paramName);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00013C65 File Offset: 0x00011E65
		internal static Exception NonEmptyCollectionRequired(string paramName)
		{
			return new ArgumentException(Strings.NonEmptyCollectionRequired, paramName);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00013C72 File Offset: 0x00011E72
		internal static Exception InvalidNullValue(Type type, string paramName)
		{
			return new ArgumentException(Strings.InvalidNullValue(type), paramName);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00013C80 File Offset: 0x00011E80
		internal static Exception InvalidTypeException(object value, Type type, string paramName)
		{
			return new ArgumentException(Strings.InvalidObjectType(((value != null) ? value.GetType() : null) ?? "null", type), paramName);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00013CA3 File Offset: 0x00011EA3
		private static string GetParamName(string paramName, int index)
		{
			if (index >= 0)
			{
				return string.Format("{0}[{1}]", paramName, index);
			}
			return paramName;
		}
	}
}
