using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200014B RID: 331
	internal static class UnsafeTextExtensions
	{
		// Token: 0x06000DAB RID: 3499 RVA: 0x0002A62C File Offset: 0x0002882C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref UnsafeList<byte> AsUnsafeListOfBytes(this UnsafeText text)
		{
			return UnsafeUtility.As<UntypedUnsafeList, UnsafeList<byte>>(ref text.m_UntypedListData);
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0002A639 File Offset: 0x00028839
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static UnsafeList<byte> AsUnsafeListOfBytesRO(this UnsafeText text)
		{
			return *UnsafeUtility.As<UntypedUnsafeList, UnsafeList<byte>>(ref text.m_UntypedListData);
		}
	}
}
