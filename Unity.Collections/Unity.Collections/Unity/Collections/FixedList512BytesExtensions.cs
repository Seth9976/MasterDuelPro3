using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x0200005C RID: 92
	[GenerateTestsForBurstCompatibility]
	public static class FixedList512BytesExtensions
	{
		// Token: 0x06000348 RID: 840 RVA: 0x00009984 File Offset: 0x00007B84
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static int IndexOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList512Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>((void*)list.Buffer, list.Length, value);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00009998 File Offset: 0x00007B98
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Contains<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList512Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return (ref list).IndexOf(value) != -1;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000099A8 File Offset: 0x00007BA8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Remove<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList512Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			int index = (ref list).IndexOf(value);
			if (index < 0)
			{
				return false;
			}
			list.RemoveAt(index);
			return true;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000099CC File Offset: 0x00007BCC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool RemoveSwapBack<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList512Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
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
