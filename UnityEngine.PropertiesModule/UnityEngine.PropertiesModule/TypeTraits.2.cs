using System;
using System.Reflection;
using UnityEngine;

namespace Unity.Properties
{
	// Token: 0x02000061 RID: 97
	public static class TypeTraits<T>
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00009526 File Offset: 0x00007726
		public static bool IsValueType { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000952D File Offset: 0x0000772D
		public static bool IsPrimitive { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00009534 File Offset: 0x00007734
		public static bool IsInterface { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000953B File Offset: 0x0000773B
		public static bool IsAbstract { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00009542 File Offset: 0x00007742
		public static bool IsArray { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00009549 File Offset: 0x00007749
		public static bool IsEnum { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00009550 File Offset: 0x00007750
		public static bool IsNullable { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00009557 File Offset: 0x00007757
		public static bool IsObject { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000955E File Offset: 0x0000775E
		public static bool IsString { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00009565 File Offset: 0x00007765
		public static bool IsContainer { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000956C File Offset: 0x0000776C
		public static bool CanBeNull { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00009573 File Offset: 0x00007773
		public static bool IsAbstractOrInterface { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000957A File Offset: 0x0000777A
		public static bool IsUnityObject { get; }

		// Token: 0x06000235 RID: 565 RVA: 0x00009584 File Offset: 0x00007784
		static TypeTraits()
		{
			Type type = typeof(T);
			TypeTraits<T>.IsValueType = type.IsValueType;
			TypeTraits<T>.IsPrimitive = type.IsPrimitive;
			TypeTraits<T>.IsInterface = type.IsInterface;
			TypeTraits<T>.IsAbstract = type.IsAbstract;
			TypeTraits<T>.IsArray = type.IsArray;
			TypeTraits<T>.IsEnum = type.IsEnum;
			TypeTraits<T>.<IsEnumFlags>k__BackingField = TypeTraits<T>.IsEnum && type.GetCustomAttribute<FlagsAttribute>() != null;
			TypeTraits<T>.IsNullable = Nullable.GetUnderlyingType(typeof(T)) != null;
			TypeTraits<T>.<IsMultidimensionalArray>k__BackingField = TypeTraits<T>.IsArray && typeof(T).GetArrayRank() != 1;
			TypeTraits<T>.IsObject = type == typeof(object);
			TypeTraits<T>.IsString = type == typeof(string);
			TypeTraits<T>.IsContainer = TypeTraits.IsContainer(type);
			TypeTraits<T>.CanBeNull = !TypeTraits<T>.IsValueType;
			TypeTraits<T>.<IsPrimitiveOrString>k__BackingField = TypeTraits<T>.IsPrimitive || TypeTraits<T>.IsString;
			TypeTraits<T>.IsAbstractOrInterface = TypeTraits<T>.IsAbstract || TypeTraits<T>.IsInterface;
			TypeTraits<T>.CanBeNull |= TypeTraits<T>.IsNullable;
			TypeTraits<T>.<IsLazyLoadReference>k__BackingField = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(LazyLoadReference<>);
			TypeTraits<T>.IsUnityObject = typeof(global::UnityEngine.Object).IsAssignableFrom(type);
		}
	}
}
