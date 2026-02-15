using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Net
{
	// Token: 0x02000429 RID: 1065
	internal class ResponseStream : Stream
	{
		// Token: 0x06001ACC RID: 6860 RVA: 0x00074983 File Offset: 0x00072B83
		internal ResponseStream(Stream stream, HttpListenerResponse response, bool ignore_errors)
		{
			this.response = response;
			this.ignore_errors = ignore_errors;
			this.stream = stream;
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x000028AE File Offset: 0x00000AAE
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001ACE RID: 6862 RVA: 0x000028AE File Offset: 0x00000AAE
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x00003BCC File Offset: 0x00001DCC
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x00003132 File Offset: 0x00001332
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x00003132 File Offset: 0x00001332
		// (set) Token: 0x06001AD2 RID: 6866 RVA: 0x00003132 File Offset: 0x00001332
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x000749A0 File Offset: 0x00072BA0
		public override void Close()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				MemoryStream headers = this.GetHeaders(true);
				bool sendChunked = this.response.SendChunked;
				if (this.stream.CanWrite)
				{
					try
					{
						if (headers != null)
						{
							long position = headers.Position;
							if (sendChunked && !this.trailer_sent)
							{
								byte[] array = ResponseStream.GetChunkSizeBytes(0, true);
								headers.Position = headers.Length;
								headers.Write(array, 0, array.Length);
							}
							this.InternalWrite(headers.GetBuffer(), (int)position, (int)(headers.Length - position));
							this.trailer_sent = true;
						}
						else if (sendChunked && !this.trailer_sent)
						{
							byte[] array = ResponseStream.GetChunkSizeBytes(0, true);
							this.InternalWrite(array, 0, array.Length);
							this.trailer_sent = true;
						}
					}
					catch (IOException)
					{
					}
				}
				this.response.Close();
			}
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x00074A7C File Offset: 0x00072C7C
		private MemoryStream GetHeaders(bool closing)
		{
			object headers_lock = this.response.headers_lock;
			MemoryStream memoryStream;
			lock (headers_lock)
			{
				if (this.response.HeadersSent)
				{
					memoryStream = null;
				}
				else
				{
					MemoryStream memoryStream2 = new MemoryStream();
					this.response.SendHeaders(closing, memoryStream2);
					memoryStream = memoryStream2;
				}
			}
			return memoryStream;
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x00002FA0 File Offset: 0x000011A0
		public override void Flush()
		{
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00074AE4 File Offset: 0x00072CE4
		private static byte[] GetChunkSizeBytes(int size, bool final)
		{
			string text = string.Format("{0:x}\r\n{1}", size, final ? "\r\n" : "");
			return Encoding.ASCII.GetBytes(text);
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x00074B1C File Offset: 0x00072D1C
		internal void InternalWrite(byte[] buffer, int offset, int count)
		{
			if (this.ignore_errors)
			{
				try
				{
					this.stream.Write(buffer, offset, count);
					return;
				}
				catch
				{
					return;
				}
			}
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00074B64 File Offset: 0x00072D64
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (count == 0)
			{
				return;
			}
			MemoryStream headers = this.GetHeaders(false);
			bool sendChunked = this.response.SendChunked;
			if (headers != null)
			{
				long position = headers.Position;
				headers.Position = headers.Length;
				if (sendChunked)
				{
					byte[] array = ResponseStream.GetChunkSizeBytes(count, false);
					headers.Write(array, 0, array.Length);
				}
				int num = Math.Min(count, 16384 - (int)headers.Position + (int)position);
				headers.Write(buffer, offset, num);
				count -= num;
				offset += num;
				this.InternalWrite(headers.GetBuffer(), (int)position, (int)(headers.Length - position));
				headers.SetLength(0L);
				headers.Capacity = 0;
			}
			else if (sendChunked)
			{
				byte[] array = ResponseStream.GetChunkSizeBytes(count, false);
				this.InternalWrite(array, 0, array.Length);
			}
			if (count > 0)
			{
				this.InternalWrite(buffer, offset, count);
			}
			if (sendChunked)
			{
				this.InternalWrite(ResponseStream.crlf, 0, 2);
			}
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x00074C5C File Offset: 0x00072E5C
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			MemoryStream headers = this.GetHeaders(false);
			bool sendChunked = this.response.SendChunked;
			if (headers != null)
			{
				long position = headers.Position;
				headers.Position = headers.Length;
				if (sendChunked)
				{
					byte[] array = ResponseStream.GetChunkSizeBytes(count, false);
					headers.Write(array, 0, array.Length);
				}
				headers.Write(buffer, offset, count);
				buffer = headers.GetBuffer();
				offset = (int)position;
				count = (int)(headers.Position - position);
			}
			else if (sendChunked)
			{
				byte[] array = ResponseStream.GetChunkSizeBytes(count, false);
				this.InternalWrite(array, 0, array.Length);
			}
			return this.stream.BeginWrite(buffer, offset, count, cback, state);
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x00074D10 File Offset: 0x00072F10
		public override void EndWrite(IAsyncResult ares)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (this.ignore_errors)
			{
				try
				{
					this.stream.EndWrite(ares);
					if (this.response.SendChunked)
					{
						this.stream.Write(ResponseStream.crlf, 0, 2);
					}
					return;
				}
				catch
				{
					return;
				}
			}
			this.stream.EndWrite(ares);
			if (this.response.SendChunked)
			{
				this.stream.Write(ResponseStream.crlf, 0, 2);
			}
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x00003132 File Offset: 0x00001332
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x00003132 File Offset: 0x00001332
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x00003132 File Offset: 0x00001332
		public override int EndRead(IAsyncResult ares)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x00003132 File Offset: 0x00001332
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x00003132 File Offset: 0x00001332
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04001170 RID: 4464
		private HttpListenerResponse response;

		// Token: 0x04001171 RID: 4465
		private bool ignore_errors;

		// Token: 0x04001172 RID: 4466
		private bool disposed;

		// Token: 0x04001173 RID: 4467
		private bool trailer_sent;

		// Token: 0x04001174 RID: 4468
		private Stream stream;

		// Token: 0x04001175 RID: 4469
		private static byte[] crlf = new byte[] { 13, 10 };
	}
}
