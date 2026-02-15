using System;
using System.IO;

namespace Ionic.Zip
{
	// Token: 0x02000023 RID: 35
	internal class OffsetStream : Stream, IDisposable
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00003ABA File Offset: 0x00001CBA
		public OffsetStream(Stream s)
		{
			this._originalPosition = s.Position;
			this._innerStream = s;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003AD5 File Offset: 0x00001CD5
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this._innerStream.Read(buffer, offset, count);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003AE5 File Offset: 0x00001CE5
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003AEC File Offset: 0x00001CEC
		public override bool CanRead
		{
			get
			{
				return this._innerStream.CanRead;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003AF9 File Offset: 0x00001CF9
		public override bool CanSeek
		{
			get
			{
				return this._innerStream.CanSeek;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003B06 File Offset: 0x00001D06
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003B09 File Offset: 0x00001D09
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003B16 File Offset: 0x00001D16
		public override long Length
		{
			get
			{
				return this._innerStream.Length;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003B23 File Offset: 0x00001D23
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00003B37 File Offset: 0x00001D37
		public override long Position
		{
			get
			{
				return this._innerStream.Position - this._originalPosition;
			}
			set
			{
				this._innerStream.Position = this._originalPosition + value;
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003B4C File Offset: 0x00001D4C
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._innerStream.Seek(this._originalPosition + offset, origin) - this._originalPosition;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003AE5 File Offset: 0x00001CE5
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003B69 File Offset: 0x00001D69
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003B71 File Offset: 0x00001D71
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x0400005B RID: 91
		private long _originalPosition;

		// Token: 0x0400005C RID: 92
		private Stream _innerStream;
	}
}
