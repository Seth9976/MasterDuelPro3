using System;
using System.IO;
using Microsoft.Win32.SafeHandles;

namespace YgomSystem.LocalFileSystem
{
	// Token: 0x02000755 RID: 1877
	public class WindowsFileHandler : IFileHandler
	{
		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06003A9E RID: 15006 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isValid
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Open(string nativePath, StreamOpenMode openMode)
		{
			return false;
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x0000216D File Offset: 0x0000036D
		public void Close()
		{
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x000029CC File Offset: 0x00000BCC
		public int Write(byte[] data, int offset, int count)
		{
			return 0;
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x000029CC File Offset: 0x00000BCC
		public int Read(byte[] buffer, int offset, int count)
		{
			return 0;
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x000F1669 File Offset: 0x000EF869
		public long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		// Token: 0x06003AA5 RID: 15013 RVA: 0x000F1669 File Offset: 0x000EF869
		public long GetSeek()
		{
			return 0L;
		}

		// Token: 0x06003AA6 RID: 15014 RVA: 0x000F1669 File Offset: 0x000EF869
		public long GetSize()
		{
			return 0L;
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x000F1669 File Offset: 0x000EF869
		public long SetSize(long size)
		{
			return 0L;
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Flush()
		{
			return false;
		}

		// Token: 0x0400345C RID: 13404
		private static SafeFileHandle nullHandle;

		// Token: 0x0400345D RID: 13405
		private SafeFileHandle m_handle;
	}
}
