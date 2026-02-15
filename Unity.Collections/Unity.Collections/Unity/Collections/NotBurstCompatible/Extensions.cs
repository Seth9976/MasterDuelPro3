using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.NotBurstCompatible
{
	// Token: 0x020000FF RID: 255
	public static class Extensions
	{
		// Token: 0x06000AD3 RID: 2771 RVA: 0x00021494 File Offset: 0x0001F694
		[ExcludeFromBurstCompatTesting("Returns managed array")]
		public static T[] ToArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> set) where T : struct, ValueType, IEquatable<T>
		{
			NativeArray<T> array = set.ToNativeArray(Allocator.TempJob);
			T[] array2 = array.ToArray();
			array.Dispose();
			return array2;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x000214C0 File Offset: 0x0001F6C0
		[ExcludeFromBurstCompatTesting("Returns managed array")]
		public static T[] ToArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> set) where T : struct, ValueType, IEquatable<T>
		{
			NativeArray<T> array = set.ToNativeArray(Allocator.TempJob);
			T[] array2 = array.ToArray();
			array.Dispose();
			return array2;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x000214EC File Offset: 0x0001F6EC
		[ExcludeFromBurstCompatTesting("Returns managed array")]
		public static T[] ToArrayNBC<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeList<T> list) where T : struct, ValueType
		{
			return list.AsArray().ToArray();
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00021508 File Offset: 0x0001F708
		[ExcludeFromBurstCompatTesting("Takes managed array")]
		public static void CopyFromNBC<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeList<T> list, T[] array) where T : struct, ValueType
		{
			list.Clear();
			list.Resize(array.Length, NativeArrayOptions.UninitializedMemory);
			list.AsArray().CopyFrom(array);
		}
	}
}
