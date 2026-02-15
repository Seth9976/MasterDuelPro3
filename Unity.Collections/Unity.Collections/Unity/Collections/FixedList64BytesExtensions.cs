using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x02000054 RID: 84
	[GenerateTestsForBurstCompatibility]
	public static class FixedList64BytesExtensions
	{
		// Token: 0x060002AC RID: 684 RVA: 0x0000840C File Offset: 0x0000660C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static int IndexOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList64Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>((void*)list.Buffer, list.Length, value);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00008420 File Offset: 0x00006620
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Contains<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList64Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return (ref list).IndexOf(value) != -1;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00008430 File Offset: 0x00006630
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Remove<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList64Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			int index = (ref list).IndexOf(value);
			if (index < 0)
			{
				return false;
			}
			list.RemoveAt(index);
			return true;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00008454 File Offset: 0x00006654
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool RemoveSwapBack<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList64Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			int index = (ref list).IndexOf(value);
			if (index == -1)
			{
				return false;
			}
			list.RemoveAtSwapBack(index);
			return true;
		}
	}
}
