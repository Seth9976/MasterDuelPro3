using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x02000060 RID: 96
	[GenerateTestsForBurstCompatibility]
	public static class FixedList4096BytesExtensions
	{
		// Token: 0x06000396 RID: 918 RVA: 0x0000A440 File Offset: 0x00008640
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static int IndexOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList4096Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>((void*)list.Buffer, list.Length, value);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000A454 File Offset: 0x00008654
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Contains<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList4096Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return (ref list).IndexOf(value) != -1;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000A464 File Offset: 0x00008664
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Remove<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList4096Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			int index = (ref list).IndexOf(value);
			if (index < 0)
			{
				return false;
			}
			list.RemoveAt(index);
			return true;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000A488 File Offset: 0x00008688
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool RemoveSwapBack<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList4096Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
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
