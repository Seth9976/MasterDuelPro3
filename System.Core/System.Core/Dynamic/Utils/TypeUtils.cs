using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Dynamic.Utils
{
	// Token: 0x0200014F RID: 335
	internal static class TypeUtils
	{
		// Token: 0x06000AF7 RID: 2807 RVA: 0x0002B177 File Offset: 0x00029377
		public static Type GetNonNullableType(this Type type)
		{
			if (!type.IsNullableType())
			{
				return type;
			}
			return type.GetGenericArguments()[0];
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002B18B File Offset: 0x0002938B
		public static Type GetNullableType(this Type type)
		{
			if (type.IsValueType && !type.IsNullableType())
			{
				return typeof(Nullable<>).MakeGenericType(new Type[] { type });
			}
			return type;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0002B1B8 File Offset: 0x000293B8
		public static bool IsNullableType(this Type type)
		{
			return type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0002B1D9 File Offset: 0x000293D9
		public static bool IsNullableOrReferenceType(this Type type)
		{
			return !type.IsValueType || type.IsNullableType();
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0002B1EB File Offset: 0x000293EB
		public static bool IsBool(this Type type)
		{
			return type.GetNonNullableType() == typeof(bool);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0002B204 File Offset: 0x00029404
		public static bool IsNumeric(this Type type)
		{
			type = type.GetNonNullableType();
			if (!type.IsEnum)
			{
				TypeCode typeCode = type.GetTypeCode();
				if (typeCode - TypeCode.Char <= 10)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0002B234 File Offset: 0x00029434
		public static bool IsInteger(this Type type)
		{
			type = type.GetNonNullableType();
			if (!type.IsEnum)
			{
				TypeCode typeCode = type.GetTypeCode();
				if (typeCode - TypeCode.SByte <= 7)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0002B264 File Offset: 0x00029464
		public static bool IsInteger64(this Type type)
		{
			type = type.GetNonNullableType();
			if (!type.IsEnum)
			{
				TypeCode typeCode = type.GetTypeCode();
				if (typeCode - TypeCode.Int64 <= 1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0002B294 File Offset: 0x00029494
		public static bool IsArithmetic(this Type type)
		{
			type = type.GetNonNullableType();
			if (!type.IsEnum)
			{
				TypeCode typeCode = type.GetTypeCode();
				if (typeCode - TypeCode.Int16 <= 7)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0002B2C4 File Offset: 0x000294C4
		public static bool IsUnsignedInt(this Type type)
		{
			type = type.GetNonNullableType();
			if (!type.IsEnum)
			{
				switch (type.GetTypeCode())
				{
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.UInt64:
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0002B30C File Offset: 0x0002950C
		public static bool IsIntegerOrBool(this Type type)
		{
			type = type.GetNonNullableType();
			if (!type.IsEnum)
			{
				TypeCode typeCode = type.GetTypeCode();
				if (typeCode == TypeCode.Boolean || typeCode - TypeCode.SByte <= 7)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0002B340 File Offset: 0x00029540
		public static bool IsValidInstanceType(MemberInfo member, Type instanceType)
		{
			Type declaringType = member.DeclaringType;
			if (TypeUtils.AreReferenceAssignable(declaringType, instanceType))
			{
				return true;
			}
			if (declaringType == null)
			{
				return false;
			}
			if (instanceType.IsValueType)
			{
				if (TypeUtils.AreReferenceAssignable(declaringType, typeof(object)))
				{
					return true;
				}
				if (TypeUtils.AreReferenceAssignable(declaringType, typeof(ValueType)))
				{
					return true;
				}
				if (instanceType.IsEnum && TypeUtils.AreReferenceAssignable(declaringType, typeof(Enum)))
				{
					return true;
				}
				if (declaringType.IsInterface)
				{
					foreach (Type type in instanceType.GetTypeInfo().ImplementedInterfaces)
					{
						if (TypeUtils.AreReferenceAssignable(declaringType, type))
						{
							return true;
						}
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0002B410 File Offset: 0x00029610
		public static bool HasIdentityPrimitiveOrNullableConversionTo(this Type source, Type dest)
		{
			return TypeUtils.AreEquivalent(source, dest) || (source.IsNullableType() && TypeUtils.AreEquivalent(dest, source.GetNonNullableType())) || (dest.IsNullableType() && TypeUtils.AreEquivalent(source, dest.GetNonNullableType())) || (source.IsConvertible() && dest.IsConvertible() && (dest.GetNonNullableType() != typeof(bool) || (source.IsEnum && source.GetEnumUnderlyingType() == typeof(bool))));
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0002B4A4 File Offset: 0x000296A4
		public static bool HasReferenceConversionTo(this Type source, Type dest)
		{
			if (source == typeof(void) || dest == typeof(void))
			{
				return false;
			}
			Type nonNullableType = source.GetNonNullableType();
			Type nonNullableType2 = dest.GetNonNullableType();
			return nonNullableType.IsAssignableFrom(nonNullableType2) || nonNullableType2.IsAssignableFrom(nonNullableType) || (source.IsInterface || dest.IsInterface) || TypeUtils.IsLegalExplicitVariantDelegateConversion(source, dest) || ((source.IsArray || dest.IsArray) && source.StrictHasReferenceConversionTo(dest, true));
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0002B534 File Offset: 0x00029734
		private static bool StrictHasReferenceConversionTo(this Type source, Type dest, bool skipNonArray)
		{
			for (;;)
			{
				if (!skipNonArray)
				{
					if (source.IsValueType | dest.IsValueType)
					{
						break;
					}
					if (source.IsAssignableFrom(dest) || dest.IsAssignableFrom(source))
					{
						return true;
					}
					if (source.IsInterface)
					{
						if (dest.IsInterface || (dest.IsClass && !dest.IsSealed))
						{
							return true;
						}
					}
					else if (dest.IsInterface && source.IsClass && !source.IsSealed)
					{
						return true;
					}
				}
				if (!source.IsArray)
				{
					goto IL_00B2;
				}
				if (!dest.IsArray)
				{
					goto IL_00AA;
				}
				if (source.GetArrayRank() != dest.GetArrayRank() || source.IsSZArray != dest.IsSZArray)
				{
					return false;
				}
				source = source.GetElementType();
				dest = dest.GetElementType();
				skipNonArray = false;
			}
			return false;
			IL_00AA:
			return TypeUtils.HasArrayToInterfaceConversion(source, dest);
			IL_00B2:
			if (dest.IsArray)
			{
				return TypeUtils.HasInterfaceToArrayConversion(source, dest) || TypeUtils.IsImplicitReferenceConversion(typeof(Array), source);
			}
			return TypeUtils.IsLegalExplicitVariantDelegateConversion(source, dest);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0002B620 File Offset: 0x00029820
		private static bool HasArrayToInterfaceConversion(Type source, Type dest)
		{
			if (!source.IsSZArray || !dest.IsInterface || !dest.IsGenericType)
			{
				return false;
			}
			Type[] genericArguments = dest.GetGenericArguments();
			if (genericArguments.Length != 1)
			{
				return false;
			}
			Type genericTypeDefinition = dest.GetGenericTypeDefinition();
			foreach (Type type in TypeUtils.s_arrayAssignableInterfaces)
			{
				if (TypeUtils.AreEquivalent(genericTypeDefinition, type))
				{
					return source.GetElementType().StrictHasReferenceConversionTo(genericArguments[0], false);
				}
			}
			return false;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0002B694 File Offset: 0x00029894
		private static bool HasInterfaceToArrayConversion(Type source, Type dest)
		{
			if (!dest.IsSZArray || !source.IsInterface || !source.IsGenericType)
			{
				return false;
			}
			Type[] genericArguments = source.GetGenericArguments();
			if (genericArguments.Length != 1)
			{
				return false;
			}
			Type genericTypeDefinition = source.GetGenericTypeDefinition();
			foreach (Type type in TypeUtils.s_arrayAssignableInterfaces)
			{
				if (TypeUtils.AreEquivalent(genericTypeDefinition, type))
				{
					return genericArguments[0].StrictHasReferenceConversionTo(dest.GetElementType(), false);
				}
			}
			return false;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0002B705 File Offset: 0x00029905
		private static bool IsCovariant(Type t)
		{
			return (t.GenericParameterAttributes & GenericParameterAttributes.Covariant) > GenericParameterAttributes.None;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0002B712 File Offset: 0x00029912
		private static bool IsContravariant(Type t)
		{
			return (t.GenericParameterAttributes & GenericParameterAttributes.Contravariant) > GenericParameterAttributes.None;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0002B71F File Offset: 0x0002991F
		private static bool IsInvariant(Type t)
		{
			return (t.GenericParameterAttributes & GenericParameterAttributes.VarianceMask) == GenericParameterAttributes.None;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0002B72C File Offset: 0x0002992C
		private static bool IsDelegate(Type t)
		{
			return t.IsSubclassOf(typeof(MulticastDelegate));
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0002B740 File Offset: 0x00029940
		public static bool IsLegalExplicitVariantDelegateConversion(Type source, Type dest)
		{
			if (!TypeUtils.IsDelegate(source) || !TypeUtils.IsDelegate(dest) || !source.IsGenericType || !dest.IsGenericType)
			{
				return false;
			}
			Type genericTypeDefinition = source.GetGenericTypeDefinition();
			if (dest.GetGenericTypeDefinition() != genericTypeDefinition)
			{
				return false;
			}
			Type[] genericArguments = genericTypeDefinition.GetGenericArguments();
			Type[] genericArguments2 = source.GetGenericArguments();
			Type[] genericArguments3 = dest.GetGenericArguments();
			for (int i = 0; i < genericArguments.Length; i++)
			{
				Type type = genericArguments2[i];
				Type type2 = genericArguments3[i];
				if (!TypeUtils.AreEquivalent(type, type2))
				{
					Type type3 = genericArguments[i];
					if (TypeUtils.IsInvariant(type3))
					{
						return false;
					}
					if (TypeUtils.IsCovariant(type3))
					{
						if (!type.HasReferenceConversionTo(type2))
						{
							return false;
						}
					}
					else if (TypeUtils.IsContravariant(type3) && (type.IsValueType || type2.IsValueType))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0002B80C File Offset: 0x00029A0C
		public static bool IsConvertible(this Type type)
		{
			type = type.GetNonNullableType();
			if (type.IsEnum)
			{
				return true;
			}
			TypeCode typeCode = type.GetTypeCode();
			return typeCode - TypeCode.Boolean <= 11;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0002B83C File Offset: 0x00029A3C
		public static bool HasReferenceEquality(Type left, Type right)
		{
			return !left.IsValueType && !right.IsValueType && (left.IsInterface || right.IsInterface || TypeUtils.AreReferenceAssignable(left, right) || TypeUtils.AreReferenceAssignable(right, left));
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0002B874 File Offset: 0x00029A74
		public static bool HasBuiltInEqualityOperator(Type left, Type right)
		{
			if (left.IsInterface && !right.IsValueType)
			{
				return true;
			}
			if (right.IsInterface && !left.IsValueType)
			{
				return true;
			}
			if (!left.IsValueType && !right.IsValueType && (TypeUtils.AreReferenceAssignable(left, right) || TypeUtils.AreReferenceAssignable(right, left)))
			{
				return true;
			}
			if (!TypeUtils.AreEquivalent(left, right))
			{
				return false;
			}
			Type nonNullableType = left.GetNonNullableType();
			return nonNullableType == typeof(bool) || nonNullableType.IsNumeric() || nonNullableType.IsEnum;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0002B8FD File Offset: 0x00029AFD
		public static bool IsImplicitlyConvertibleTo(this Type source, Type destination)
		{
			return TypeUtils.AreEquivalent(source, destination) || TypeUtils.IsImplicitNumericConversion(source, destination) || TypeUtils.IsImplicitReferenceConversion(source, destination) || TypeUtils.IsImplicitBoxingConversion(source, destination) || TypeUtils.IsImplicitNullableConversion(source, destination);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0002B92C File Offset: 0x00029B2C
		public static MethodInfo GetUserDefinedCoercionMethod(Type convertFrom, Type convertToType)
		{
			Type nonNullableType = convertFrom.GetNonNullableType();
			Type nonNullableType2 = convertToType.GetNonNullableType();
			MethodInfo[] methods = nonNullableType.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			MethodInfo methodInfo = TypeUtils.FindConversionOperator(methods, convertFrom, convertToType);
			if (methodInfo != null)
			{
				return methodInfo;
			}
			MethodInfo[] methods2 = nonNullableType2.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			methodInfo = TypeUtils.FindConversionOperator(methods2, convertFrom, convertToType);
			if (methodInfo != null)
			{
				return methodInfo;
			}
			if (TypeUtils.AreEquivalent(nonNullableType, convertFrom) && TypeUtils.AreEquivalent(nonNullableType2, convertToType))
			{
				return null;
			}
			MethodInfo methodInfo2;
			if ((methodInfo2 = TypeUtils.FindConversionOperator(methods, nonNullableType, nonNullableType2)) == null && (methodInfo2 = TypeUtils.FindConversionOperator(methods2, nonNullableType, nonNullableType2)) == null)
			{
				methodInfo2 = TypeUtils.FindConversionOperator(methods, nonNullableType, convertToType) ?? TypeUtils.FindConversionOperator(methods2, nonNullableType, convertToType);
			}
			return methodInfo2;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0002B9C8 File Offset: 0x00029BC8
		private static MethodInfo FindConversionOperator(MethodInfo[] methods, Type typeFrom, Type typeTo)
		{
			foreach (MethodInfo methodInfo in methods)
			{
				if ((methodInfo.Name == "op_Implicit" || methodInfo.Name == "op_Explicit") && TypeUtils.AreEquivalent(methodInfo.ReturnType, typeTo))
				{
					ParameterInfo[] parametersCached = methodInfo.GetParametersCached();
					if (parametersCached.Length == 1 && TypeUtils.AreEquivalent(parametersCached[0].ParameterType, typeFrom))
					{
						return methodInfo;
					}
				}
			}
			return null;
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0002BA3C File Offset: 0x00029C3C
		private static bool IsImplicitNumericConversion(Type source, Type destination)
		{
			TypeCode typeCode = source.GetTypeCode();
			TypeCode typeCode2 = destination.GetTypeCode();
			switch (typeCode)
			{
			case TypeCode.Char:
				if (typeCode2 - TypeCode.UInt16 <= 7)
				{
					return true;
				}
				break;
			case TypeCode.SByte:
				switch (typeCode2)
				{
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.Byte:
				if (typeCode2 - TypeCode.Int16 <= 8)
				{
					return true;
				}
				break;
			case TypeCode.Int16:
				switch (typeCode2)
				{
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.UInt16:
				if (typeCode2 - TypeCode.Int32 <= 6)
				{
					return true;
				}
				break;
			case TypeCode.Int32:
				if (typeCode2 == TypeCode.Int64 || typeCode2 - TypeCode.Single <= 2)
				{
					return true;
				}
				break;
			case TypeCode.UInt32:
				if (typeCode2 - TypeCode.Int64 <= 4)
				{
					return true;
				}
				break;
			case TypeCode.Int64:
			case TypeCode.UInt64:
				if (typeCode2 - TypeCode.Single <= 2)
				{
					return true;
				}
				break;
			case TypeCode.Single:
				return typeCode2 == TypeCode.Double;
			}
			return false;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0002BB25 File Offset: 0x00029D25
		private static bool IsImplicitReferenceConversion(Type source, Type destination)
		{
			return destination.IsAssignableFrom(source);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0002BB30 File Offset: 0x00029D30
		private static bool IsImplicitBoxingConversion(Type source, Type destination)
		{
			return (source.IsValueType && (destination == typeof(object) || destination == typeof(ValueType))) || (source.IsEnum && destination == typeof(Enum));
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0002BB85 File Offset: 0x00029D85
		private static bool IsImplicitNullableConversion(Type source, Type destination)
		{
			return destination.IsNullableType() && source.GetNonNullableType().IsImplicitlyConvertibleTo(destination.GetNonNullableType());
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0002BBA4 File Offset: 0x00029DA4
		public static Type FindGenericType(Type definition, Type type)
		{
			while (type != null && type != typeof(object))
			{
				if (type.IsConstructedGenericType && TypeUtils.AreEquivalent(type.GetGenericTypeDefinition(), definition))
				{
					return type;
				}
				if (definition.IsInterface)
				{
					foreach (Type type2 in type.GetTypeInfo().ImplementedInterfaces)
					{
						Type type3 = TypeUtils.FindGenericType(definition, type2);
						if (type3 != null)
						{
							return type3;
						}
					}
				}
				type = type.BaseType;
			}
			return null;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0002BC48 File Offset: 0x00029E48
		public static MethodInfo GetBooleanOperator(Type type, string name)
		{
			MethodInfo anyStaticMethodValidated;
			for (;;)
			{
				anyStaticMethodValidated = type.GetAnyStaticMethodValidated(name, new Type[] { type });
				if (anyStaticMethodValidated != null && anyStaticMethodValidated.IsSpecialName && !anyStaticMethodValidated.ContainsGenericParameters)
				{
					break;
				}
				type = type.BaseType;
				if (!(type != null))
				{
					goto Block_3;
				}
			}
			return anyStaticMethodValidated;
			Block_3:
			return null;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0002BC94 File Offset: 0x00029E94
		public static Type GetNonRefType(this Type type)
		{
			if (!type.IsByRef)
			{
				return type;
			}
			return type.GetElementType();
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0002BCA6 File Offset: 0x00029EA6
		public static bool AreEquivalent(Type t1, Type t2)
		{
			return t1 != null && t1.IsEquivalentTo(t2);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0002BCBA File Offset: 0x00029EBA
		public static bool AreReferenceAssignable(Type dest, Type src)
		{
			return TypeUtils.AreEquivalent(dest, src) || (!dest.IsValueType && !src.IsValueType && dest.IsAssignableFrom(src));
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0002BCE0 File Offset: 0x00029EE0
		public static bool IsSameOrSubclass(Type type, Type subType)
		{
			return TypeUtils.AreEquivalent(type, subType) || subType.IsSubclassOf(type);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0002BCF4 File Offset: 0x00029EF4
		public static void ValidateType(Type type, string paramName)
		{
			TypeUtils.ValidateType(type, paramName, false, false);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0002BCFF File Offset: 0x00029EFF
		public static void ValidateType(Type type, string paramName, bool allowByRef, bool allowPointer)
		{
			if (TypeUtils.ValidateType(type, paramName, -1))
			{
				if (!allowByRef && type.IsByRef)
				{
					throw global::System.Linq.Expressions.Error.TypeMustNotBeByRef(paramName);
				}
				if (!allowPointer && type.IsPointer)
				{
					throw global::System.Linq.Expressions.Error.TypeMustNotBePointer(paramName);
				}
			}
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0002BD2F File Offset: 0x00029F2F
		public static bool ValidateType(Type type, string paramName, int index)
		{
			if (type == typeof(void))
			{
				return false;
			}
			if (type.ContainsGenericParameters)
			{
				throw type.IsGenericTypeDefinition ? global::System.Linq.Expressions.Error.TypeIsGeneric(type, paramName, index) : global::System.Linq.Expressions.Error.TypeContainsGenericParameters(type, paramName, index);
			}
			return true;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0002BD69 File Offset: 0x00029F69
		public static MethodInfo GetInvokeMethod(this Type delegateType)
		{
			return delegateType.GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0002BD78 File Offset: 0x00029F78
		internal static bool IsUnsigned(this Type type)
		{
			return type.GetNonNullableType().GetTypeCode().IsUnsigned();
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0002BD8A File Offset: 0x00029F8A
		internal static bool IsUnsigned(this TypeCode typeCode)
		{
			switch (typeCode)
			{
			case TypeCode.Char:
			case TypeCode.Byte:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
			case TypeCode.UInt64:
				return true;
			}
			return false;
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0002BDBD File Offset: 0x00029FBD
		internal static bool IsFloatingPoint(this Type type)
		{
			return type.GetNonNullableType().GetTypeCode().IsFloatingPoint();
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0002BDCF File Offset: 0x00029FCF
		internal static bool IsFloatingPoint(this TypeCode typeCode)
		{
			return typeCode - TypeCode.Single <= 1;
		}

		// Token: 0x04000357 RID: 855
		private static readonly Type[] s_arrayAssignableInterfaces = (from i in typeof(int[]).GetInterfaces()
			where i.IsGenericType
			select i.GetGenericTypeDefinition()).ToArray<Type>();
	}
}
