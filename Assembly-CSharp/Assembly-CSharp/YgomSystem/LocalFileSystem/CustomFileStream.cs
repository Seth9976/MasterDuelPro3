using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace YgomSystem.LocalFileSystem
{
	// Token: 0x0200073E RID: 1854
	public class CustomFileStream : Stream
	{
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06003984 RID: 14724 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool isReadable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06003985 RID: 14725 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool isWritable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06003986 RID: 14726 RVA: 0x0000216A File Offset: 0x0000036A
		public string nativePath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06003987 RID: 14727 RVA: 0x000F1669 File Offset: 0x000EF869
		// (set) Token: 0x06003988 RID: 14728 RVA: 0x0000216D File Offset: 0x0000036D
		public override long Position
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06003989 RID: 14729 RVA: 0x000F1669 File Offset: 0x000EF869
		public override long Length
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x0600398A RID: 14730 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x0600398B RID: 14731 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600398C RID: 14732 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600398D RID: 14733 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool CanTimeout
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x0600398E RID: 14734 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600398F RID: 14735 RVA: 0x0000216D File Offset: 0x0000036D
		public override int ReadTimeout
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06003990 RID: 14736 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003991 RID: 14737 RVA: 0x0000216D File Offset: 0x0000036D
		public override int WriteTimeout
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x000F36F2 File Offset: 0x000F18F2
		protected CustomFileStream(string nativePath, StreamOpenMode openMode, IFileHandler fileHandler)
		{
		}

		// Token: 0x06003993 RID: 14739 RVA: 0x000F36FC File Offset: 0x000F18FC
		~CustomFileStream()
		{
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x0000216A File Offset: 0x0000036A
		public static CustomFileStream Create<T>(string nativePath, StreamOpenMode openMode) where T : IFileHandler, new()
		{
			return null;
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Dispose(bool disposing)
		{
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int Read(byte[] buffer, int offset, int count)
		{
			return 0;
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int ReadByte()
		{
			return 0;
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Write(byte[] data, int offset, int count)
		{
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x0000216D File Offset: 0x0000036D
		public override void WriteByte(byte value)
		{
		}

		// Token: 0x0600399A RID: 14746 RVA: 0x000F1669 File Offset: 0x000EF869
		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		// Token: 0x0600399B RID: 14747 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Flush()
		{
		}

		// Token: 0x0600399C RID: 14748 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SetLength(long value)
		{
		}

		// Token: 0x04003408 RID: 13320
		protected IFileHandler m_file;

		// Token: 0x04003409 RID: 13321
		private string m_nativePath;

		// Token: 0x0400340A RID: 13322
		private long m_seek;

		// Token: 0x0400340B RID: 13323
		protected long m_dataLength;

		// Token: 0x0400340C RID: 13324
		private byte[] m_temp1ByteArray;

		// Token: 0x0400340D RID: 13325
		private object m_syncObject;

		// Token: 0x0400340E RID: 13326
		private CustomFileStream.Access m_access;

		// Token: 0x0200073F RID: 1855
		[Flags]
		private enum Access
		{
			// Token: 0x04003410 RID: 13328
			Readable = 1,
			// Token: 0x04003411 RID: 13329
			Writable = 2
		}
	}
}
