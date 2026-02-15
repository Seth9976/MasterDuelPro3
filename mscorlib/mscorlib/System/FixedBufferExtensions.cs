using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200016E RID: 366
	internal static class FixedBufferExtensions
	{
		// Token: 0x06000D5E RID: 3422 RVA: 0x00039648 File Offset: 0x00037848
		internal unsafe static string GetStringFromFixedBuffer(this ReadOnlySpan<char> span)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(span))
			{
				return new string(reference, 0, span.GetFixedBufferStringLength());
			}
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0003966C File Offset: 0x0003786C
		internal static int GetFixedBufferStringLength(this ReadOnlySpan<char> span)
		{
			int num = span.IndexOf('\0');
			if (num >= 0)
			{
				return num;
			}
			return span.Length;
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00039690 File Offset: 0x00037890
		internal unsafe static bool FixedBufferEqualsString(this ReadOnlySpan<char> span, string value)
		{
			if (value == null || value.Length > span.Length)
			{
				return false;
			}
			int i;
			for (i = 0; i < value.Length; i++)
			{
				if (value[i] == '\0' || value[i] != (char)(*span[i]))
				{
					return false;
				}
			}
			return i == span.Length || *span[i] == 0;
		}
	}
}
