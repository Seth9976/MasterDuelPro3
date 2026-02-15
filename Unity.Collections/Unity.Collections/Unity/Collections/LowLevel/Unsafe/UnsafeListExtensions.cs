using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200011C RID: 284
	[GenerateTestsForBurstCompatibility]
	public static class UnsafeListExtensions
	{
		// Token: 0x06000BF3 RID: 3059 RVA: 0x0002447C File Offset: 0x0002267C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static int IndexOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this UnsafeList<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>((void*)list.Ptr, list.Length, value);
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00024491 File Offset: 0x00022691
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Contains<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this UnsafeList<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return list.IndexOf(value) != -1;
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x000244A0 File Offset: 0x000226A0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static int IndexOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this UnsafeList<T>.ReadOnly list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>((void*)list.Ptr, list.Length, value);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x000244B4 File Offset: 0x000226B4
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Contains<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this UnsafeList<T>.ReadOnly list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return list.IndexOf(value) != -1;
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x000244C3 File Offset: 0x000226C3
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static int IndexOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this UnsafeList<T>.ParallelReader list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>((void*)list.Ptr, list.Length, value);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x000244D7 File Offset: 0x000226D7
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Contains<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this UnsafeList<T>.ParallelReader list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return list.IndexOf(value) != -1;
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x000244E8 File Offset: 0x000226E8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static bool ArraysEqual<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeList<T> container, in UnsafeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			if (container.Length != other.Length)
			{
				return false;
			}
			for (int i = 0; i != container.Length; i++)
			{
				T t = container[i];
				UnsafeList<T> unsafeList = other;
				if (!t.Equals(unsafeList[i]))
				{
					return false;
				}
			}
			return true;
		}
	}
}
