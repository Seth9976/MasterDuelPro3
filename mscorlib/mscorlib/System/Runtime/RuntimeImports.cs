using System;
using System.Runtime.CompilerServices;

namespace System.Runtime
{
	// Token: 0x0200040C RID: 1036
	public static class RuntimeImports
	{
		// Token: 0x060022B5 RID: 8885 RVA: 0x0008F410 File Offset: 0x0008D610
		internal unsafe static void RhZeroMemory(ref byte b, ulong byteLength)
		{
			fixed (byte* ptr = &b)
			{
				RuntimeImports.ZeroMemory((void*)ptr, (uint)byteLength);
			}
		}

		// Token: 0x060022B6 RID: 8886
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ZeroMemory(void* p, uint byteLength);

		// Token: 0x060022B7 RID: 8887
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void Memmove(byte* dest, byte* src, uint len);

		// Token: 0x060022B8 RID: 8888
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void Memmove_wbarrier(byte* dest, byte* src, uint len, IntPtr type_handle);

		// Token: 0x060022B9 RID: 8889
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void _ecvt_s(byte* buffer, int sizeInBytes, double value, int count, int* dec, int* sign);
	}
}
