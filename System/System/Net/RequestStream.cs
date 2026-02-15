using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000428 RID: 1064
	internal class RequestStream : Stream
	{
		// Token: 0x06001AB9 RID: 6841 RVA: 0x00074676 File Offset: 0x00072876
		internal RequestStream(Stream stream, byte[] buffer, int offset, int length)
			: this(stream, buffer, offset, length, -1L)
		{
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x00074685 File Offset: 0x00072885
		internal RequestStream(Stream stream, byte[] buffer, int offset, int length, long contentlength)
		{
			this.stream = stream;
			this.buffer = buffer;
			this.offset = offset;
			this.length = length;
			this.remaining_body = contentlength;
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001ABB RID: 6843 RVA: 0x00003BCC File Offset: 0x00001DCC
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001ABC RID: 6844 RVA: 0x000028AE File Offset: 0x00000AAE
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x000028AE File Offset: 0x00000AAE
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001ABE RID: 6846 RVA: 0x00003132 File Offset: 0x00001332
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001ABF RID: 6847 RVA: 0x00003132 File Offset: 0x00001332
		// (set) Token: 0x06001AC0 RID: 6848 RVA: 0x00003132 File Offset: 0x00001332
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

		// Token: 0x06001AC1 RID: 6849 RVA: 0x000746B2 File Offset: 0x000728B2
		public override void Close()
		{
			this.disposed = true;
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x00002FA0 File Offset: 0x000011A0
		public override void Flush()
		{
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x000746BC File Offset: 0x000728BC
		private int FillFromBuffer(byte[] buffer, int off, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (off < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			int num = buffer.Length;
			if (off > num)
			{
				throw new ArgumentException("destination offset is beyond array size");
			}
			if (off > num - count)
			{
				throw new ArgumentException("Reading would overrun buffer");
			}
			if (this.remaining_body == 0L)
			{
				return -1;
			}
			if (this.length == 0)
			{
				return 0;
			}
			int num2 = Math.Min(this.length, count);
			if (this.remaining_body > 0L)
			{
				num2 = (int)Math.Min((long)num2, this.remaining_body);
			}
			if (this.offset > this.buffer.Length - num2)
			{
				num2 = Math.Min(num2, this.buffer.Length - this.offset);
			}
			if (num2 == 0)
			{
				return 0;
			}
			Buffer.BlockCopy(this.buffer, this.offset, buffer, off, num2);
			this.offset += num2;
			this.length -= num2;
			if (this.remaining_body > 0L)
			{
				this.remaining_body -= (long)num2;
			}
			return num2;
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x000747D4 File Offset: 0x000729D4
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(typeof(RequestStream).ToString());
			}
			int num = this.FillFromBuffer(buffer, offset, count);
			if (num == -1)
			{
				return 0;
			}
			if (num > 0)
			{
				return num;
			}
			num = this.stream.Read(buffer, offset, count);
			if (num > 0 && this.remaining_body > 0L)
			{
				this.remaining_body -= (long)num;
			}
			return num;
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x00074844 File Offset: 0x00072A44
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(typeof(RequestStream).ToString());
			}
			int num = this.FillFromBuffer(buffer, offset, count);
			if (num > 0 || num == -1)
			{
				HttpStreamAsyncResult httpStreamAsyncResult = new HttpStreamAsyncResult();
				httpStreamAsyncResult.Buffer = buffer;
				httpStreamAsyncResult.Offset = offset;
				httpStreamAsyncResult.Count = count;
				httpStreamAsyncResult.Callback = cback;
				httpStreamAsyncResult.State = state;
				httpStreamAsyncResult.SynchRead = Math.Max(0, num);
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			if (this.remaining_body >= 0L && (long)count > this.remaining_body)
			{
				count = (int)Math.Min(2147483647L, this.remaining_body);
			}
			return this.stream.BeginRead(buffer, offset, count, cback, state);
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x000748F8 File Offset: 0x00072AF8
		public override int EndRead(IAsyncResult ares)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(typeof(RequestStream).ToString());
			}
			if (ares == null)
			{
				throw new ArgumentNullException("async_result");
			}
			if (ares is HttpStreamAsyncResult)
			{
				HttpStreamAsyncResult httpStreamAsyncResult = (HttpStreamAsyncResult)ares;
				if (!ares.IsCompleted)
				{
					ares.AsyncWaitHandle.WaitOne();
				}
				return httpStreamAsyncResult.SynchRead;
			}
			int num = this.stream.EndRead(ares);
			if (this.remaining_body > 0L && num > 0)
			{
				this.remaining_body -= (long)num;
			}
			return num;
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x00003132 File Offset: 0x00001332
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x00003132 File Offset: 0x00001332
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x00003132 File Offset: 0x00001332
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x00003132 File Offset: 0x00001332
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x00003132 File Offset: 0x00001332
		public override void EndWrite(IAsyncResult async_result)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400116A RID: 4458
		private byte[] buffer;

		// Token: 0x0400116B RID: 4459
		private int offset;

		// Token: 0x0400116C RID: 4460
		private int length;

		// Token: 0x0400116D RID: 4461
		private long remaining_body;

		// Token: 0x0400116E RID: 4462
		private bool disposed;

		// Token: 0x0400116F RID: 4463
		private Stream stream;
	}
}
