using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x0200011A RID: 282
	internal class XmlRegisteredNonCachedStream : Stream
	{
		// Token: 0x06000EBB RID: 3771 RVA: 0x0004AFE6 File Offset: 0x000491E6
		internal XmlRegisteredNonCachedStream(Stream stream, XmlDownloadManager downloadManager, string host)
		{
			this.stream = stream;
			this.downloadManager = downloadManager;
			this.host = host;
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0004B004 File Offset: 0x00049204
		~XmlRegisteredNonCachedStream()
		{
			if (this.downloadManager != null)
			{
				this.downloadManager.Remove(this.host);
			}
			this.stream = null;
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0004B04C File Offset: 0x0004924C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.stream != null)
				{
					if (this.downloadManager != null)
					{
						this.downloadManager.Remove(this.host);
					}
					this.stream.Close();
				}
				this.stream = null;
				GC.SuppressFinalize(this);
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0004B0B0 File Offset: 0x000492B0
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.stream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0004B0C4 File Offset: 0x000492C4
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.stream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0004B0D8 File Offset: 0x000492D8
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.stream.EndRead(asyncResult);
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0004B0E6 File Offset: 0x000492E6
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.stream.EndWrite(asyncResult);
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0004B0F4 File Offset: 0x000492F4
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0004B101 File Offset: 0x00049301
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.stream.Read(buffer, offset, count);
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0004B111 File Offset: 0x00049311
		public override int ReadByte()
		{
			return this.stream.ReadByte();
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0004B11E File Offset: 0x0004931E
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0004B12D File Offset: 0x0004932D
		public override void SetLength(long value)
		{
			this.stream.SetLength(value);
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0004B13B File Offset: 0x0004933B
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0004B14B File Offset: 0x0004934B
		public override void WriteByte(byte value)
		{
			this.stream.WriteByte(value);
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x0004B159 File Offset: 0x00049359
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x0004B166 File Offset: 0x00049366
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x0004B173 File Offset: 0x00049373
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0004B180 File Offset: 0x00049380
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0004B18D File Offset: 0x0004938D
		// (set) Token: 0x06000ECE RID: 3790 RVA: 0x0004B19A File Offset: 0x0004939A
		public override long Position
		{
			get
			{
				return this.stream.Position;
			}
			set
			{
				this.stream.Position = value;
			}
		}

		// Token: 0x04000740 RID: 1856
		protected Stream stream;

		// Token: 0x04000741 RID: 1857
		private XmlDownloadManager downloadManager;

		// Token: 0x04000742 RID: 1858
		private string host;
	}
}
