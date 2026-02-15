using System;
using System.IO;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001AA RID: 426
	internal static class ManagedStreamHelpers
	{
		// Token: 0x060010C7 RID: 4295 RVA: 0x00023A5C File Offset: 0x00021C5C
		internal static void ValidateLoadFromStream(Stream stream)
		{
			bool flag = stream == null;
			if (flag)
			{
				throw new ArgumentNullException("ManagedStream object must be non-null", "stream");
			}
			bool flag2 = !stream.CanRead;
			if (flag2)
			{
				throw new ArgumentException("ManagedStream object must be readable (stream.CanRead must return true)", "stream");
			}
			bool flag3 = !stream.CanSeek;
			if (flag3)
			{
				throw new ArgumentException("ManagedStream object must be seekable (stream.CanSeek must return true)", "stream");
			}
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00023ABC File Offset: 0x00021CBC
		[RequiredByNativeCode]
		internal unsafe static void ManagedStreamRead(byte[] buffer, int offset, int count, Stream stream, IntPtr returnValueAddress)
		{
			bool flag = returnValueAddress == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Return value address cannot be 0.", "returnValueAddress");
			}
			ManagedStreamHelpers.ValidateLoadFromStream(stream);
			*(int*)(void*)returnValueAddress = stream.Read(buffer, offset, count);
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00023B04 File Offset: 0x00021D04
		[RequiredByNativeCode]
		internal unsafe static void ManagedStreamSeek(long offset, uint origin, Stream stream, IntPtr returnValueAddress)
		{
			bool flag = returnValueAddress == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Return value address cannot be 0.", "returnValueAddress");
			}
			ManagedStreamHelpers.ValidateLoadFromStream(stream);
			*(long*)(void*)returnValueAddress = stream.Seek(offset, (SeekOrigin)origin);
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00023B48 File Offset: 0x00021D48
		[RequiredByNativeCode]
		internal unsafe static void ManagedStreamLength(Stream stream, IntPtr returnValueAddress)
		{
			bool flag = returnValueAddress == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Return value address cannot be 0.", "returnValueAddress");
			}
			ManagedStreamHelpers.ValidateLoadFromStream(stream);
			*(long*)(void*)returnValueAddress = stream.Length;
		}
	}
}
