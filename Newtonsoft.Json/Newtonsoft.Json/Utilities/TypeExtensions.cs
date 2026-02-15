using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000F5 RID: 245
	[NullableContext(1)]
	[Nullable(0)]
	internal static class TypeExtensions
	{
		// Token: 0x06000720 RID: 1824 RVA: 0x00024128 File Offset: 0x00022328
		public static MethodInfo Method(this Delegate d)
		{
			return d.Method;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00024130 File Offset: 0x00022330
		public static MemberTypes MemberType(this MemberInfo memberInfo)
		{
			return memberInfo.MemberType;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00024138 File Offset: 0x00022338
		public static bool ContainsGenericParameters(this Type type)
		{
			return type.ContainsGenericParameters;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00024140 File Offset: 0x00022340
		public static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00024148 File Offset: 0x00022348
		public static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00024150 File Offset: 0x00022350
		public static bool IsGenericTypeDefinition(this Type type)
		{
			return type.IsGenericTypeDefinition;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00024158 File Offset: 0x00022358
		[return: Nullable(2)]
		public static Type BaseType(this Type type)
		{
			return type.BaseType;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00024160 File Offset: 0x00022360
		public static Assembly Assembly(this Type type)
		{
			return type.Assembly;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00024168 File Offset: 0x00022368
		public static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00024170 File Offset: 0x00022370
		public static bool IsClass(this Type type)
		{
			return type.IsClass;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00024178 File Offset: 0x00022378
		public static bool IsSealed(this Type type)
		{
			return type.IsSealed;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00024180 File Offset: 0x00022380
		public static bool IsAbstract(this Type type)
		{
			return type.IsAbstract;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00024188 File Offset: 0x00022388
		public static bool IsVisible(this Type type)
		{
			return type.IsVisible;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00024190 File Offset: 0x00022390
		public static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00024198 File Offset: 0x00022398
		public static bool IsPrimitive(this Type type)
		{
			return type.IsPrimitive;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x000241A0 File Offset: 0x000223A0
		public static bool AssignableToTypeName(this Type type, string fullTypeName, bool searchInterfaces, [Nullable(2)] [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out Type match)
		{
			Type type2 = type;
			while (type2 != null)
			{
				if (string.Equals(type2.FullName, fullTypeName, StringComparison.Ordinal))
				{
					match = type2;
					return true;
				}
				type2 = type2.BaseType();
			}
			if (searchInterfaces)
			{
				Type[] interfaces = type.GetInterfaces();
				for (int i = 0; i < interfaces.Length; i++)
				{
					if (string.Equals(interfaces[i].Name, fullTypeName, StringComparison.Ordinal))
					{
						match = type;
						return true;
					}
				}
			}
			match = null;
			return false;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00024208 File Offset: 0x00022408
		public static bool AssignableToTypeName(this Type type, string fullTypeName, bool searchInterfaces)
		{
			Type type2;
			return type.AssignableToTypeName(fullTypeName, searchInterfaces, out type2);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00024220 File Offset: 0x00022420
		public static bool ImplementInterface(this Type type, Type interfaceType)
		{
			Type type2 = type;
			while (type2 != null)
			{
				foreach (Type type3 in ((IEnumerable<Type>)type2.GetInterfaces()))
				{
					if (type3 == interfaceType || (type3 != null && type3.ImplementInterface(interfaceType)))
					{
						return true;
					}
				}
				type2 = type2.BaseType();
			}
			return false;
		}
	}
}
