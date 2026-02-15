using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x02000050 RID: 80
	[GenerateTestsForBurstCompatibility]
	public static class FixedList32BytesExtensions
	{
		// Token: 0x0600025E RID: 606 RVA: 0x0000794F File Offset: 0x00005B4F
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static int IndexOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList32Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>((void*)list.Buffer, list.Length, value);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00007963 File Offset: 0x00005B63
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Contains<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList32Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return (ref list).IndexOf(value) != -1;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00007974 File Offset: 0x00005B74
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Remove<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList32Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			int index = (ref list).IndexOf(value);
			if (index < 0)
			{
				return false;
			}
			list.RemoveAt(index);
			return true;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00007998 File Offset: 0x00005B98
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool RemoveSwapBack<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList32Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
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
