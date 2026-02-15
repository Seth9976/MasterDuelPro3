using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x02000058 RID: 88
	[GenerateTestsForBurstCompatibility]
	public static class FixedList128BytesExtensions
	{
		// Token: 0x060002FA RID: 762 RVA: 0x00008EC8 File Offset: 0x000070C8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static int IndexOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList128Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>((void*)list.Buffer, list.Length, value);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00008EDC File Offset: 0x000070DC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Contains<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList128Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return (ref list).IndexOf(value) != -1;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00008EEC File Offset: 0x000070EC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Remove<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList128Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			int index = (ref list).IndexOf(value);
			if (index < 0)
			{
				return false;
			}
			list.RemoveAt(index);
			return true;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00008F10 File Offset: 0x00007110
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool RemoveSwapBack<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList128Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
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
