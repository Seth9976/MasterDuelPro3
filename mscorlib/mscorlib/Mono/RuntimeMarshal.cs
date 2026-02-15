using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono
{
	// Token: 0x02000039 RID: 57
	internal static class RuntimeMarshal
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00002A68 File Offset: 0x00000C68
		internal unsafe static string PtrToUtf8String(IntPtr ptr)
		{
			if (ptr == IntPtr.Zero)
			{
				return string.Empty;
			}
			byte* ptr2 = (byte*)(void*)ptr;
			int num = 0;
			try
			{
				while (*(ptr2++) != 0)
				{
					num++;
				}
			}
			catch (NullReferenceException)
			{
				throw new ArgumentOutOfRangeException("ptr", "Value does not refer to a valid string.");
			}
			return new string((sbyte*)(void*)ptr, 0, num, Encoding.UTF8);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002AD4 File Offset: 0x00000CD4
		internal static SafeStringMarshal MarshalString(string str)
		{
			return new SafeStringMarshal(str);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002ADC File Offset: 0x00000CDC
		private unsafe static int DecodeBlobSize(IntPtr in_ptr, out IntPtr out_ptr)
		{
			byte* ptr = (byte*)(void*)in_ptr;
			uint num;
			if ((*ptr & 128) == 0)
			{
				num = (uint)(*ptr & 127);
				ptr++;
			}
			else if ((*ptr & 64) == 0)
			{
				num = (uint)(((int)(*ptr & 63) << 8) + (int)ptr[1]);
				ptr += 2;
			}
			else
			{
				num = (uint)(((int)(*ptr & 31) << 24) + ((int)ptr[1] << 16) + ((int)ptr[2] << 8) + (int)ptr[3]);
				ptr += 4;
			}
			out_ptr = (IntPtr)((void*)ptr);
			return (int)num;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002B4C File Offset: 0x00000D4C
		internal static byte[] DecodeBlobArray(IntPtr ptr)
		{
			IntPtr intPtr;
			int num = RuntimeMarshal.DecodeBlobSize(ptr, out intPtr);
			byte[] array = new byte[num];
			Marshal.Copy(intPtr, array, 0, num);
			return array;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002B73 File Offset: 0x00000D73
		internal static int AsciHexDigitValue(int c)
		{
			if (c >= 48 && c <= 57)
			{
				return c - 48;
			}
			if (c >= 97 && c <= 102)
			{
				return c - 97 + 10;
			}
			return c - 65 + 10;
		}

		// Token: 0x06000086 RID: 134
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void FreeAssemblyName(ref MonoAssemblyName name, bool freeStruct);
	}
}
