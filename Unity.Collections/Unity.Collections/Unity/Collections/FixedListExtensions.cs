using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x02000062 RID: 98
	public static class FixedListExtensions
	{
		// Token: 0x0600039C RID: 924 RVA: 0x0000A4C7 File Offset: 0x000086C7
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this FixedList32Bytes<T> list) where T : struct, ValueType, IComparable<T>
		{
			NativeSortExtension.Sort<T>((T*)(list.buffer + FixedList.PaddingBytes<T>()), list.Length);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000A4E0 File Offset: 0x000086E0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList32Bytes<T> list, U comp) where T : struct, ValueType, IComparable<T> where U : IComparer<T>
		{
			NativeSortExtension.Sort<T, U>((T*)(list.buffer + FixedList.PaddingBytes<T>()), list.Length, comp);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000A4FA File Offset: 0x000086FA
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this FixedList64Bytes<T> list) where T : struct, ValueType, IComparable<T>
		{
			NativeSortExtension.Sort<T>((T*)(list.buffer + FixedList.PaddingBytes<T>()), list.Length);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000A513 File Offset: 0x00008713
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList64Bytes<T> list, U comp) where T : struct, ValueType, IComparable<T> where U : IComparer<T>
		{
			NativeSortExtension.Sort<T, U>((T*)(list.buffer + FixedList.PaddingBytes<T>()), list.Length, comp);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000A52D File Offset: 0x0000872D
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this FixedList128Bytes<T> list) where T : struct, ValueType, IComparable<T>
		{
			NativeSortExtension.Sort<T>((T*)(list.buffer + FixedList.PaddingBytes<T>()), list.Length);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000A546 File Offset: 0x00008746
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList128Bytes<T> list, U comp) where T : struct, ValueType, IComparable<T> where U : IComparer<T>
		{
			NativeSortExtension.Sort<T, U>((T*)(list.buffer + FixedList.PaddingBytes<T>()), list.Length, comp);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000A560 File Offset: 0x00008760
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this FixedList512Bytes<T> list) where T : struct, ValueType, IComparable<T>
		{
			NativeSortExtension.Sort<T>((T*)(list.buffer + FixedList.PaddingBytes<T>()), list.Length);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000A579 File Offset: 0x00008779
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList512Bytes<T> list, U comp) where T : struct, ValueType, IComparable<T> where U : IComparer<T>
		{
			NativeSortExtension.Sort<T, U>((T*)(list.buffer + FixedList.PaddingBytes<T>()), list.Length, comp);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000A593 File Offset: 0x00008793
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this FixedList4096Bytes<T> list) where T : struct, ValueType, IComparable<T>
		{
			NativeSortExtension.Sort<T>((T*)(list.buffer + FixedList.PaddingBytes<T>()), list.Length);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000A5AC File Offset: 0x000087AC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList4096Bytes<T> list, U comp) where T : struct, ValueType, IComparable<T> where U : IComparer<T>
		{
			NativeSortExtension.Sort<T, U>((T*)(list.buffer + FixedList.PaddingBytes<T>()), list.Length, comp);
		}
	}
}
