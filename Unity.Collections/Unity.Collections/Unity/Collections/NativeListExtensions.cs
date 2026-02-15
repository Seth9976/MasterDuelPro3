using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x0200009F RID: 159
	[GenerateTestsForBurstCompatibility]
	public static class NativeListExtensions
	{
		// Token: 0x060007C6 RID: 1990 RVA: 0x00018408 File Offset: 0x00016608
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static bool Contains<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this NativeList<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>((void*)list.GetUnsafeReadOnlyPtr<T>(), list.Length, value) != -1;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00018423 File Offset: 0x00016623
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static int IndexOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this NativeList<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>((void*)list.GetUnsafeReadOnlyPtr<T>(), list.Length, value);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00018438 File Offset: 0x00016638
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static bool ArraysEqual<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeArray<T> container, in NativeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			NativeList<T> nativeList = other;
			return container.ArraysEqual(nativeList.AsArray());
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00018459 File Offset: 0x00016659
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static bool ArraysEqual<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeList<T> container, in NativeArray<T> other) where T : struct, ValueType, IEquatable<T>
		{
			return other.ArraysEqual(in container);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00018468 File Offset: 0x00016668
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static bool ArraysEqual<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeList<T> container, in NativeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			NativeArray<T> nativeArray = container.AsArray();
			NativeList<T> nativeList = other;
			return nativeArray.ArraysEqual(nativeList.AsArray());
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001848F File Offset: 0x0001668F
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static bool ArraysEqual<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeList<T> container, in UnsafeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			return (*container.m_ListData).ArraysEqual(in other);
		}
	}
}
