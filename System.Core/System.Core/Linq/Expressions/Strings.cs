using System;

namespace System.Linq.Expressions
{
	// Token: 0x020000D7 RID: 215
	internal static class Strings
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x00016EE9 File Offset: 0x000150E9
		internal static string ReducibleMustOverrideReduce
		{
			get
			{
				return "reducible nodes must override Expression.Reduce()";
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00016EF0 File Offset: 0x000150F0
		internal static string MustReduceToDifferent
		{
			get
			{
				return "node cannot reduce to itself or null";
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x00016EF7 File Offset: 0x000150F7
		internal static string ReducedNotCompatible
		{
			get
			{
				return "cannot assign from the reduced node type to the original node type";
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00016EFE File Offset: 0x000150FE
		internal static string SetterHasNoParams
		{
			get
			{
				return "Setter must have parameters.";
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x00016F05 File Offset: 0x00015105
		internal static string PropertyCannotHaveRefType
		{
			get
			{
				return "Property cannot have a managed pointer type.";
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00016F0C File Offset: 0x0001510C
		internal static string IndexesOfSetGetMustMatch
		{
			get
			{
				return "Indexing parameters of getter and setter must match.";
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00016F13 File Offset: 0x00015113
		internal static string AccessorsCannotHaveVarArgs
		{
			get
			{
				return "Accessor method should not have VarArgs.";
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00016F1A File Offset: 0x0001511A
		internal static string AccessorsCannotHaveByRefArgs
		{
			get
			{
				return "Accessor indexes cannot be passed ByRef.";
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00016F21 File Offset: 0x00015121
		internal static string BoundsCannotBeLessThanOne
		{
			get
			{
				return "Bounds count cannot be less than 1";
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00016F28 File Offset: 0x00015128
		internal static string TypeMustNotBeByRef
		{
			get
			{
				return "Type must not be ByRef";
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x00016F2F File Offset: 0x0001512F
		internal static string TypeMustNotBePointer
		{
			get
			{
				return "Type must not be a pointer type";
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00016F36 File Offset: 0x00015136
		internal static string SetterMustBeVoid
		{
			get
			{
				return "Setter should have void type.";
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x00016F3D File Offset: 0x0001513D
		internal static string PropertyTypeMustMatchGetter
		{
			get
			{
				return "Property type must match the value type of getter";
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00016F44 File Offset: 0x00015144
		internal static string PropertyTypeMustMatchSetter
		{
			get
			{
				return "Property type must match the value type of setter";
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00016F4B File Offset: 0x0001514B
		internal static string BothAccessorsMustBeStatic
		{
			get
			{
				return "Both accessors must be static.";
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00016F52 File Offset: 0x00015152
		internal static string OnlyStaticFieldsHaveNullInstance
		{
			get
			{
				return "Static field requires null instance, non-static field requires non-null instance.";
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00016F59 File Offset: 0x00015159
		internal static string OnlyStaticPropertiesHaveNullInstance
		{
			get
			{
				return "Static property requires null instance, non-static property requires non-null instance.";
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00016F60 File Offset: 0x00015160
		internal static string OnlyStaticMethodsHaveNullInstance
		{
			get
			{
				return "Static method requires null instance, non-static method requires non-null instance.";
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00016F67 File Offset: 0x00015167
		internal static string PropertyTypeCannotBeVoid
		{
			get
			{
				return "Property cannot have a void type.";
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00016F6E File Offset: 0x0001516E
		internal static string InvalidUnboxType
		{
			get
			{
				return "Can only unbox from an object or interface type to a value type.";
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00016F75 File Offset: 0x00015175
		internal static string ExpressionMustBeWriteable
		{
			get
			{
				return "Expression must be writeable";
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x00016F7C File Offset: 0x0001517C
		internal static string ArgumentMustNotHaveValueType
		{
			get
			{
				return "Argument must not have a value type.";
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00016F83 File Offset: 0x00015183
		internal static string MustBeReducible
		{
			get
			{
				return "must be reducible node";
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00016F8A File Offset: 0x0001518A
		internal static string AllTestValuesMustHaveSameType
		{
			get
			{
				return "All test values must have the same type.";
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00016F91 File Offset: 0x00015191
		internal static string AllCaseBodiesMustHaveSameType
		{
			get
			{
				return "All case bodies and the default body must have the same type.";
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00016F98 File Offset: 0x00015198
		internal static string DefaultBodyMustBeSupplied
		{
			get
			{
				return "Default body must be supplied if case bodies are not System.Void.";
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00016F9F File Offset: 0x0001519F
		internal static string LabelMustBeVoidOrHaveExpression
		{
			get
			{
				return "Label type must be System.Void if an expression is not supplied";
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x00016FA6 File Offset: 0x000151A6
		internal static string LabelTypeMustBeVoid
		{
			get
			{
				return "Type must be System.Void for this label argument";
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x00016FAD File Offset: 0x000151AD
		internal static string QuotedExpressionMustBeLambda
		{
			get
			{
				return "Quoted expression must be a lambda";
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00016FB4 File Offset: 0x000151B4
		internal static string CollectionModifiedWhileEnumerating
		{
			get
			{
				return "Collection was modified; enumeration operation may not execute.";
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00016FBB File Offset: 0x000151BB
		internal static string VariableMustNotBeByRef(object p0, object p1)
		{
			return global::SR.Format("Variable '{0}' uses unsupported type '{1}'. Reference types are not supported for variables.", p0, p1);
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x00016FC9 File Offset: 0x000151C9
		internal static string CollectionReadOnly
		{
			get
			{
				return "Collection is read-only.";
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00016FD0 File Offset: 0x000151D0
		internal static string AmbiguousMatchInExpandoObject(object p0)
		{
			return global::SR.Format("More than one key matching '{0}' was found in the ExpandoObject.", p0);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00016FDD File Offset: 0x000151DD
		internal static string SameKeyExistsInExpando(object p0)
		{
			return global::SR.Format("An element with the same key '{0}' already exists in the ExpandoObject.", p0);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00016FEA File Offset: 0x000151EA
		internal static string KeyDoesNotExistInExpando(object p0)
		{
			return global::SR.Format("The specified key '{0}' does not exist in the ExpandoObject.", p0);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00016FF7 File Offset: 0x000151F7
		internal static string InvalidMetaObjectCreated(object p0)
		{
			return global::SR.Format("An IDynamicMetaObjectProvider {0} created an invalid DynamicMetaObject instance.", p0);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00017004 File Offset: 0x00015204
		internal static string BinderNotCompatibleWithCallSite(object p0, object p1, object p2)
		{
			return global::SR.Format("The result type '{0}' of the binder '{1}' is not compatible with the result type '{2}' expected by the call site.", p0, p1, p2);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00017013 File Offset: 0x00015213
		internal static string DynamicBindingNeedsRestrictions(object p0, object p1)
		{
			return global::SR.Format("The result of the dynamic binding produced by the object with type '{0}' for the binder '{1}' needs at least one restriction.", p0, p1);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00017021 File Offset: 0x00015221
		internal static string DynamicObjectResultNotAssignable(object p0, object p1, object p2, object p3)
		{
			return global::SR.Format("The result type '{0}' of the dynamic binding produced by the object with type '{1}' for the binder '{2}' is not compatible with the result type '{3}' expected by the call site.", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00017043 File Offset: 0x00015243
		internal static string DynamicBinderResultNotAssignable(object p0, object p1, object p2)
		{
			return global::SR.Format("The result type '{0}' of the dynamic binding produced by binder '{1}' is not compatible with the result type '{2}' expected by the call site.", p0, p1, p2);
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x00017052 File Offset: 0x00015252
		internal static string BindingCannotBeNull
		{
			get
			{
				return "Bind cannot return null.";
			}
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00017059 File Offset: 0x00015259
		internal static string DuplicateVariable(object p0)
		{
			return global::SR.Format("Found duplicate parameter '{0}'. Each ParameterExpression in the list must be a unique object.", p0);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00017066 File Offset: 0x00015266
		internal static string TypeParameterIsNotDelegate(object p0)
		{
			return global::SR.Format("Type parameter is {0}. Expected a delegate.", p0);
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x00017073 File Offset: 0x00015273
		internal static string NoOrInvalidRuleProduced
		{
			get
			{
				return "No or Invalid rule produced";
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0001707A File Offset: 0x0001527A
		internal static string TypeMustBeDerivedFromSystemDelegate
		{
			get
			{
				return "Type must be derived from System.Delegate";
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00017081 File Offset: 0x00015281
		internal static string FirstArgumentMustBeCallSite
		{
			get
			{
				return "First argument of delegate must be CallSite";
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00017088 File Offset: 0x00015288
		internal static string FaultCannotHaveCatchOrFinally
		{
			get
			{
				return "fault cannot be used with catch or finally clauses";
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0001708F File Offset: 0x0001528F
		internal static string TryMustHaveCatchFinallyOrFault
		{
			get
			{
				return "try must have at least one catch, finally, or fault clause";
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00017096 File Offset: 0x00015296
		internal static string BodyOfCatchMustHaveSameTypeAsBodyOfTry
		{
			get
			{
				return "Body of catch must have the same type as body of try.";
			}
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001709D File Offset: 0x0001529D
		internal static string ExtensionNodeMustOverrideProperty(object p0)
		{
			return global::SR.Format("Extension node must override the property {0}.", p0);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x000170AA File Offset: 0x000152AA
		internal static string UserDefinedOperatorMustBeStatic(object p0)
		{
			return global::SR.Format("User-defined operator method '{0}' must be static.", p0);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x000170B7 File Offset: 0x000152B7
		internal static string UserDefinedOperatorMustNotBeVoid(object p0)
		{
			return global::SR.Format("User-defined operator method '{0}' must not be void.", p0);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x000170C4 File Offset: 0x000152C4
		internal static string CoercionOperatorNotDefined(object p0, object p1)
		{
			return global::SR.Format("No coercion operator is defined between types '{0}' and '{1}'.", p0, p1);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x000170D2 File Offset: 0x000152D2
		internal static string UnaryOperatorNotDefined(object p0, object p1)
		{
			return global::SR.Format("The unary operator {0} is not defined for the type '{1}'.", p0, p1);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x000170E0 File Offset: 0x000152E0
		internal static string BinaryOperatorNotDefined(object p0, object p1, object p2)
		{
			return global::SR.Format("The binary operator {0} is not defined for the types '{1}' and '{2}'.", p0, p1, p2);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x000170EF File Offset: 0x000152EF
		internal static string ReferenceEqualityNotDefined(object p0, object p1)
		{
			return global::SR.Format("Reference equality is not defined for the types '{0}' and '{1}'.", p0, p1);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x000170FD File Offset: 0x000152FD
		internal static string OperandTypesDoNotMatchParameters(object p0, object p1)
		{
			return global::SR.Format("The operands for operator '{0}' do not match the parameters of method '{1}'.", p0, p1);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001710B File Offset: 0x0001530B
		internal static string OverloadOperatorTypeDoesNotMatchConversionType(object p0, object p1)
		{
			return global::SR.Format("The return type of overload method for operator '{0}' does not match the parameter type of conversion method '{1}'.", p0, p1);
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00017119 File Offset: 0x00015319
		internal static string ConversionIsNotSupportedForArithmeticTypes
		{
			get
			{
				return "Conversion is not supported for arithmetic types without operator overloading.";
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x00017120 File Offset: 0x00015320
		internal static string ArgumentMustBeArray
		{
			get
			{
				return "Argument must be array";
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00017127 File Offset: 0x00015327
		internal static string ArgumentMustBeBoolean
		{
			get
			{
				return "Argument must be boolean";
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001712E File Offset: 0x0001532E
		internal static string EqualityMustReturnBoolean(object p0)
		{
			return global::SR.Format("The user-defined equality method '{0}' must return a boolean value.", p0);
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x0001713B File Offset: 0x0001533B
		internal static string ArgumentMustBeFieldInfoOrPropertyInfo
		{
			get
			{
				return "Argument must be either a FieldInfo or PropertyInfo";
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x00017142 File Offset: 0x00015342
		internal static string ArgumentMustBeFieldInfoOrPropertyInfoOrMethod
		{
			get
			{
				return "Argument must be either a FieldInfo, PropertyInfo or MethodInfo";
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x00017149 File Offset: 0x00015349
		internal static string ArgumentMustBeInstanceMember
		{
			get
			{
				return "Argument must be an instance member";
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x00017150 File Offset: 0x00015350
		internal static string ArgumentMustBeInteger
		{
			get
			{
				return "Argument must be of an integer type";
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x00017157 File Offset: 0x00015357
		internal static string ArgumentMustBeArrayIndexType
		{
			get
			{
				return "Argument for array index must be of type Int32";
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0001715E File Offset: 0x0001535E
		internal static string ArgumentMustBeSingleDimensionalArrayType
		{
			get
			{
				return "Argument must be single-dimensional, zero-based array type";
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x00017165 File Offset: 0x00015365
		internal static string ArgumentTypesMustMatch
		{
			get
			{
				return "Argument types do not match";
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001716C File Offset: 0x0001536C
		internal static string CannotAutoInitializeValueTypeElementThroughProperty(object p0)
		{
			return global::SR.Format("Cannot auto initialize elements of value type through property '{0}', use assignment instead", p0);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00017179 File Offset: 0x00015379
		internal static string CannotAutoInitializeValueTypeMemberThroughProperty(object p0)
		{
			return global::SR.Format("Cannot auto initialize members of value type through property '{0}', use assignment instead", p0);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00017186 File Offset: 0x00015386
		internal static string IncorrectTypeForTypeAs(object p0)
		{
			return global::SR.Format("The type used in TypeAs Expression must be of reference or nullable type, {0} is neither", p0);
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x00017193 File Offset: 0x00015393
		internal static string CoalesceUsedOnNonNullType
		{
			get
			{
				return "Coalesce used with type that cannot be null";
			}
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001719A File Offset: 0x0001539A
		internal static string ExpressionTypeCannotInitializeArrayType(object p0, object p1)
		{
			return global::SR.Format("An expression of type '{0}' cannot be used to initialize an array of type '{1}'", p0, p1);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x000171A8 File Offset: 0x000153A8
		internal static string ArgumentTypeDoesNotMatchMember(object p0, object p1)
		{
			return global::SR.Format(" Argument type '{0}' does not match the corresponding member type '{1}'", p0, p1);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x000171B6 File Offset: 0x000153B6
		internal static string ArgumentMemberNotDeclOnType(object p0, object p1)
		{
			return global::SR.Format(" The member '{0}' is not declared on type '{1}' being created", p0, p1);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x000171C4 File Offset: 0x000153C4
		internal static string ExpressionTypeDoesNotMatchReturn(object p0, object p1)
		{
			return global::SR.Format("Expression of type '{0}' cannot be used for return type '{1}'", p0, p1);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x000171D2 File Offset: 0x000153D2
		internal static string ExpressionTypeDoesNotMatchAssignment(object p0, object p1)
		{
			return global::SR.Format("Expression of type '{0}' cannot be used for assignment to type '{1}'", p0, p1);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x000171E0 File Offset: 0x000153E0
		internal static string ExpressionTypeDoesNotMatchLabel(object p0, object p1)
		{
			return global::SR.Format("Expression of type '{0}' cannot be used for label of type '{1}'", p0, p1);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x000171EE File Offset: 0x000153EE
		internal static string ExpressionTypeNotInvocable(object p0)
		{
			return global::SR.Format("Expression of type '{0}' cannot be invoked", p0);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x000171FB File Offset: 0x000153FB
		internal static string InstanceFieldNotDefinedForType(object p0, object p1)
		{
			return global::SR.Format("Instance field '{0}' is not defined for type '{1}'", p0, p1);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00017209 File Offset: 0x00015409
		internal static string FieldInfoNotDefinedForType(object p0, object p1, object p2)
		{
			return global::SR.Format("Field '{0}.{1}' is not defined for type '{2}'", p0, p1, p2);
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00017218 File Offset: 0x00015418
		internal static string IncorrectNumberOfIndexes
		{
			get
			{
				return "Incorrect number of indexes";
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001721F File Offset: 0x0001541F
		internal static string IncorrectNumberOfLambdaDeclarationParameters
		{
			get
			{
				return "Incorrect number of parameters supplied for lambda declaration";
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00017226 File Offset: 0x00015426
		internal static string IncorrectNumberOfMembersForGivenConstructor
		{
			get
			{
				return " Incorrect number of members for constructor";
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0001722D File Offset: 0x0001542D
		internal static string IncorrectNumberOfArgumentsForMembers
		{
			get
			{
				return "Incorrect number of arguments for the given members ";
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00017234 File Offset: 0x00015434
		internal static string LambdaTypeMustBeDerivedFromSystemDelegate
		{
			get
			{
				return "Lambda type parameter must be derived from System.MulticastDelegate";
			}
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001723B File Offset: 0x0001543B
		internal static string MemberNotFieldOrProperty(object p0)
		{
			return global::SR.Format("Member '{0}' not field or property", p0);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00017248 File Offset: 0x00015448
		internal static string MethodContainsGenericParameters(object p0)
		{
			return global::SR.Format("Method {0} contains generic parameters", p0);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00017255 File Offset: 0x00015455
		internal static string MethodIsGeneric(object p0)
		{
			return global::SR.Format("Method {0} is a generic method definition", p0);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00017262 File Offset: 0x00015462
		internal static string MethodNotPropertyAccessor(object p0, object p1)
		{
			return global::SR.Format("The method '{0}.{1}' is not a property accessor", p0, p1);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00017270 File Offset: 0x00015470
		internal static string PropertyDoesNotHaveGetter(object p0)
		{
			return global::SR.Format("The property '{0}' has no 'get' accessor", p0);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001727D File Offset: 0x0001547D
		internal static string PropertyDoesNotHaveSetter(object p0)
		{
			return global::SR.Format("The property '{0}' has no 'set' accessor", p0);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001728A File Offset: 0x0001548A
		internal static string PropertyDoesNotHaveAccessor(object p0)
		{
			return global::SR.Format("The property '{0}' has no 'get' or 'set' accessors", p0);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00017297 File Offset: 0x00015497
		internal static string NotAMemberOfType(object p0, object p1)
		{
			return global::SR.Format("'{0}' is not a member of type '{1}'", p0, p1);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x000172A5 File Offset: 0x000154A5
		internal static string NotAMemberOfAnyType(object p0)
		{
			return global::SR.Format("'{0}' is not a member of any type", p0);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x000172B2 File Offset: 0x000154B2
		internal static string ParameterExpressionNotValidAsDelegate(object p0, object p1)
		{
			return global::SR.Format("ParameterExpression of type '{0}' cannot be used for delegate parameter of type '{1}'", p0, p1);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x000172C0 File Offset: 0x000154C0
		internal static string PropertyNotDefinedForType(object p0, object p1)
		{
			return global::SR.Format("Property '{0}' is not defined for type '{1}'", p0, p1);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x000172CE File Offset: 0x000154CE
		internal static string InstancePropertyNotDefinedForType(object p0, object p1)
		{
			return global::SR.Format("Instance property '{0}' is not defined for type '{1}'", p0, p1);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x000172DC File Offset: 0x000154DC
		internal static string InstanceAndMethodTypeMismatch(object p0, object p1, object p2)
		{
			return global::SR.Format("Method '{0}' declared on type '{1}' cannot be called with instance of type '{2}'", p0, p1, p2);
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x000172EB File Offset: 0x000154EB
		internal static string ElementInitializerMethodNotAdd
		{
			get
			{
				return "Element initializer method must be named 'Add'";
			}
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x000172F2 File Offset: 0x000154F2
		internal static string ElementInitializerMethodNoRefOutParam(object p0, object p1)
		{
			return global::SR.Format("Parameter '{0}' of element initializer method '{1}' must not be a pass by reference parameter", p0, p1);
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00017300 File Offset: 0x00015500
		internal static string ElementInitializerMethodWithZeroArgs
		{
			get
			{
				return "Element initializer method must have at least 1 parameter";
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x00017307 File Offset: 0x00015507
		internal static string ElementInitializerMethodStatic
		{
			get
			{
				return "Element initializer method must be an instance method";
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001730E File Offset: 0x0001550E
		internal static string TypeNotIEnumerable(object p0)
		{
			return global::SR.Format("Type '{0}' is not IEnumerable", p0);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001731B File Offset: 0x0001551B
		internal static string UnhandledBinary(object p0)
		{
			return global::SR.Format("Unhandled binary: {0}", p0);
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00017328 File Offset: 0x00015528
		internal static string UnhandledBinding
		{
			get
			{
				return "Unhandled binding ";
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001732F File Offset: 0x0001552F
		internal static string UnhandledBindingType(object p0)
		{
			return global::SR.Format("Unhandled Binding Type: {0}", p0);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001733C File Offset: 0x0001553C
		internal static string UnhandledUnary(object p0)
		{
			return global::SR.Format("Unhandled unary: {0}", p0);
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00017349 File Offset: 0x00015549
		internal static string UnknownBindingType
		{
			get
			{
				return "Unknown binding type";
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00017350 File Offset: 0x00015550
		internal static string UserDefinedOpMustHaveConsistentTypes(object p0, object p1)
		{
			return global::SR.Format("The user-defined operator method '{1}' for operator '{0}' must have identical parameter and return types.", p0, p1);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001735E File Offset: 0x0001555E
		internal static string UserDefinedOpMustHaveValidReturnType(object p0, object p1)
		{
			return global::SR.Format("The user-defined operator method '{1}' for operator '{0}' must return the same type as its parameter or a derived type.", p0, p1);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001736C File Offset: 0x0001556C
		internal static string LogicalOperatorMustHaveBooleanOperators(object p0, object p1)
		{
			return global::SR.Format("The user-defined operator method '{1}' for operator '{0}' must have associated boolean True and False operators.", p0, p1);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001737A File Offset: 0x0001557A
		internal static string MethodWithArgsDoesNotExistOnType(object p0, object p1)
		{
			return global::SR.Format("No method '{0}' on type '{1}' is compatible with the supplied arguments.", p0, p1);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00017388 File Offset: 0x00015588
		internal static string GenericMethodWithArgsDoesNotExistOnType(object p0, object p1)
		{
			return global::SR.Format("No generic method '{0}' on type '{1}' is compatible with the supplied type arguments and arguments. No type arguments should be provided if the method is non-generic. ", p0, p1);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00017396 File Offset: 0x00015596
		internal static string MethodWithMoreThanOneMatch(object p0, object p1)
		{
			return global::SR.Format("More than one method '{0}' on type '{1}' is compatible with the supplied arguments.", p0, p1);
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x000173A4 File Offset: 0x000155A4
		internal static string ArgumentCannotBeOfTypeVoid
		{
			get
			{
				return "Argument type cannot be System.Void.";
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x000173AB File Offset: 0x000155AB
		internal static string OutOfRange(object p0, object p1)
		{
			return global::SR.Format("{0} must be greater than or equal to {1}", p0, p1);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x000173B9 File Offset: 0x000155B9
		internal static string LabelTargetAlreadyDefined(object p0)
		{
			return global::SR.Format("Cannot redefine label '{0}' in an inner block.", p0);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000173C6 File Offset: 0x000155C6
		internal static string LabelTargetUndefined(object p0)
		{
			return global::SR.Format("Cannot jump to undefined label '{0}'.", p0);
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x000173D3 File Offset: 0x000155D3
		internal static string ControlCannotLeaveFinally
		{
			get
			{
				return "Control cannot leave a finally block.";
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x000173DA File Offset: 0x000155DA
		internal static string ControlCannotLeaveFilterTest
		{
			get
			{
				return "Control cannot leave a filter test.";
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x000173E1 File Offset: 0x000155E1
		internal static string AmbiguousJump(object p0)
		{
			return global::SR.Format("Cannot jump to ambiguous label '{0}'.", p0);
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x000173EE File Offset: 0x000155EE
		internal static string ControlCannotEnterTry
		{
			get
			{
				return "Control cannot enter a try block.";
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x000173F5 File Offset: 0x000155F5
		internal static string ControlCannotEnterExpression
		{
			get
			{
				return "Control cannot enter an expression--only statements can be jumped into.";
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x000173FC File Offset: 0x000155FC
		internal static string NonLocalJumpWithValue(object p0)
		{
			return global::SR.Format("Cannot jump to non-local label '{0}' with a value. Only jumps to labels defined in outer blocks can pass values.", p0);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00017409 File Offset: 0x00015609
		internal static string CannotCompileConstant(object p0)
		{
			return global::SR.Format("CompileToMethod cannot compile constant '{0}' because it is a non-trivial value, such as a live object. Instead, create an expression tree that can construct this value.", p0);
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x00017416 File Offset: 0x00015616
		internal static string CannotCompileDynamic
		{
			get
			{
				return "Dynamic expressions are not supported by CompileToMethod. Instead, create an expression tree that uses System.Runtime.CompilerServices.CallSite.";
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001741D File Offset: 0x0001561D
		internal static string InvalidLvalue(object p0)
		{
			return global::SR.Format("Invalid lvalue for assignment: {0}.", p0);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001742A File Offset: 0x0001562A
		internal static string UndefinedVariable(object p0, object p1, object p2)
		{
			return global::SR.Format("variable '{0}' of type '{1}' referenced from scope '{2}', but it is not defined", p0, p1, p2);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00017439 File Offset: 0x00015639
		internal static string CannotCloseOverByRef(object p0, object p1)
		{
			return global::SR.Format("Cannot close over byref parameter '{0}' referenced in lambda '{1}'", p0, p1);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00017447 File Offset: 0x00015647
		internal static string UnexpectedVarArgsCall(object p0)
		{
			return global::SR.Format("Unexpected VarArgs call to method '{0}'", p0);
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00017454 File Offset: 0x00015654
		internal static string RethrowRequiresCatch
		{
			get
			{
				return "Rethrow statement is valid only inside a Catch block.";
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0001745B File Offset: 0x0001565B
		internal static string TryNotAllowedInFilter
		{
			get
			{
				return "Try expression is not allowed inside a filter body.";
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00017462 File Offset: 0x00015662
		internal static string MustRewriteToSameNode(object p0, object p1, object p2)
		{
			return global::SR.Format("When called from '{0}', rewriting a node of type '{1}' must return a non-null value of the same type. Alternatively, override '{2}' and change it to not visit children of this type.", p0, p1, p2);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00017471 File Offset: 0x00015671
		internal static string MustRewriteChildToSameType(object p0, object p1, object p2)
		{
			return global::SR.Format("Rewriting child expression from type '{0}' to type '{1}' is not allowed, because it would change the meaning of the operation. If this is intentional, override '{2}' and change it to allow this rewrite.", p0, p1, p2);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00017480 File Offset: 0x00015680
		internal static string MustRewriteWithoutMethod(object p0, object p1)
		{
			return global::SR.Format("Rewritten expression calls operator method '{0}', but the original node had no operator method. If this is intentional, override '{1}' and change it to allow this rewrite.", p0, p1);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001748E File Offset: 0x0001568E
		internal static string TryNotSupportedForMethodsWithRefArgs(object p0)
		{
			return global::SR.Format("TryExpression is not supported as an argument to method '{0}' because it has an argument with by-ref type. Construct the tree so the TryExpression is not nested inside of this expression.", p0);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001749B File Offset: 0x0001569B
		internal static string TryNotSupportedForValueTypeInstances(object p0)
		{
			return global::SR.Format("TryExpression is not supported as a child expression when accessing a member on type '{0}' because it is a value type. Construct the tree so the TryExpression is not nested inside of this expression.", p0);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x000174A8 File Offset: 0x000156A8
		internal static string TestValueTypeDoesNotMatchComparisonMethodParameter(object p0, object p1)
		{
			return global::SR.Format("Test value of type '{0}' cannot be used for the comparison method parameter of type '{1}'", p0, p1);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x000174B6 File Offset: 0x000156B6
		internal static string SwitchValueTypeDoesNotMatchComparisonMethodParameter(object p0, object p1)
		{
			return global::SR.Format("Switch value of type '{0}' cannot be used for the comparison method parameter of type '{1}'", p0, p1);
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x000174C4 File Offset: 0x000156C4
		internal static string NonStaticConstructorRequired
		{
			get
			{
				return "The constructor should not be static";
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x000174CB File Offset: 0x000156CB
		internal static string NonAbstractConstructorRequired
		{
			get
			{
				return "Can't compile a NewExpression with a constructor declared on an abstract class";
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x000174D2 File Offset: 0x000156D2
		internal static string ExpressionMustBeReadable
		{
			get
			{
				return "Expression must be readable";
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x000174D9 File Offset: 0x000156D9
		internal static string ExpressionTypeDoesNotMatchConstructorParameter(object p0, object p1)
		{
			return global::SR.Format("Expression of type '{0}' cannot be used for constructor parameter of type '{1}'", p0, p1);
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x000174E7 File Offset: 0x000156E7
		internal static string EnumerationIsDone
		{
			get
			{
				return "Enumeration has either not started or has already finished.";
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x000174EE File Offset: 0x000156EE
		internal static string TypeContainsGenericParameters(object p0)
		{
			return global::SR.Format("Type {0} contains generic parameters", p0);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x000174FB File Offset: 0x000156FB
		internal static string TypeIsGeneric(object p0)
		{
			return global::SR.Format("Type {0} is a generic type definition", p0);
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x00017508 File Offset: 0x00015708
		internal static string InvalidArgumentValue
		{
			get
			{
				return "Invalid argument value";
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001750F File Offset: 0x0001570F
		internal static string NonEmptyCollectionRequired
		{
			get
			{
				return "Non-empty collection required";
			}
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00017516 File Offset: 0x00015716
		internal static string InvalidNullValue(object p0)
		{
			return global::SR.Format("The value null is not of type '{0}' and cannot be used in this collection.", p0);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00017523 File Offset: 0x00015723
		internal static string InvalidObjectType(object p0, object p1)
		{
			return global::SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this collection.", p0, p1);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00017531 File Offset: 0x00015731
		internal static string ExpressionTypeDoesNotMatchMethodParameter(object p0, object p1, object p2)
		{
			return global::SR.Format("Expression of type '{0}' cannot be used for parameter of type '{1}' of method '{2}'", p0, p1, p2);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00017540 File Offset: 0x00015740
		internal static string ExpressionTypeDoesNotMatchParameter(object p0, object p1)
		{
			return global::SR.Format("Expression of type '{0}' cannot be used for parameter of type '{1}'", p0, p1);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001754E File Offset: 0x0001574E
		internal static string IncorrectNumberOfMethodCallArguments(object p0)
		{
			return global::SR.Format("Incorrect number of arguments supplied for call to method '{0}'", p0);
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001755B File Offset: 0x0001575B
		internal static string IncorrectNumberOfLambdaArguments
		{
			get
			{
				return "Incorrect number of arguments supplied for lambda invocation";
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x00017562 File Offset: 0x00015762
		internal static string IncorrectNumberOfConstructorArguments
		{
			get
			{
				return "Incorrect number of arguments for constructor";
			}
		}
	}
}
