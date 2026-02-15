using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020003FD RID: 1021
	internal class ChunkedInputStream : RequestStream
	{
		// Token: 0x0600195A RID: 6490 RVA: 0x0006C884 File Offset: 0x0006AA84
		public ChunkedInputStream(HttpListenerContext context, Stream stream, byte[] buffer, int offset, int length)
			: base(stream, buffer, offset, length)
		{
			this.context = context;
			WebHeaderCollection webHeaderCollection = (WebHeaderCollection)context.Request.Headers;
			this.decoder = new MonoChunkParser(webHeaderCollection);
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0006C8C4 File Offset: 0x0006AAC4
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			IAsyncResult asyncResult = this.BeginRead(buffer, offset, count, null, null);
			return this.EndRead(asyncResult);
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0006C8E4 File Offset: 0x0006AAE4
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || offset > num)
			{
				throw new ArgumentOutOfRangeException("offset exceeds the size of buffer");
			}
			if (count < 0 || offset > num - count)
			{
				throw new ArgumentOutOfRangeException("offset+size exceeds the size of buffer");
			}
			HttpStreamAsyncResult httpStreamAsyncResult = new HttpStreamAsyncResult();
			httpStreamAsyncResult.Callback = cback;
			httpStreamAsyncResult.State = state;
			if (this.no_more_data)
			{
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			int num2 = this.decoder.Read(buffer, offset, count);
			offset += num2;
			count -= num2;
			if (count == 0)
			{
				httpStreamAsyncResult.Count = num2;
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			if (!this.decoder.WantMore)
			{
				this.no_more_data = num2 == 0;
				httpStreamAsyncResult.Count = num2;
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			httpStreamAsyncResult.Buffer = new byte[8192];
			httpStreamAsyncResult.Offset = 0;
			httpStreamAsyncResult.Count = 8192;
			ChunkedInputStream.ReadBufferState readBufferState = new ChunkedInputStream.ReadBufferState(buffer, offset, count, httpStreamAsyncResult);
			readBufferState.InitialCount += num2;
			base.BeginRead(httpStreamAsyncResult.Buffer, httpStreamAsyncResult.Offset, httpStreamAsyncResult.Count, new AsyncCallback(this.OnRead), readBufferState);
			return httpStreamAsyncResult;
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0006CA1C File Offset: 0x0006AC1C
		private void OnRead(IAsyncResult base_ares)
		{
			ChunkedInputStream.ReadBufferState readBufferState = (ChunkedInputStream.ReadBufferState)base_ares.AsyncState;
			HttpStreamAsyncResult ares = readBufferState.Ares;
			try
			{
				int num = base.EndRead(base_ares);
				this.decoder.Write(ares.Buffer, ares.Offset, num);
				num = this.decoder.Read(readBufferState.Buffer, readBufferState.Offset, readBufferState.Count);
				readBufferState.Offset += num;
				readBufferState.Count -= num;
				if (readBufferState.Count == 0 || !this.decoder.WantMore || num == 0)
				{
					this.no_more_data = !this.decoder.WantMore && num == 0;
					ares.Count = readBufferState.InitialCount - readBufferState.Count;
					ares.Complete();
				}
				else
				{
					ares.Offset = 0;
					ares.Count = Math.Min(8192, this.decoder.ChunkLeft + 6);
					base.BeginRead(ares.Buffer, ares.Offset, ares.Count, new AsyncCallback(this.OnRead), readBufferState);
				}
			}
			catch (Exception ex)
			{
				this.context.Connection.SendError(ex.Message, 400);
				ares.Complete(ex);
			}
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0006CB64 File Offset: 0x0006AD64
		public override int EndRead(IAsyncResult ares)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			HttpStreamAsyncResult httpStreamAsyncResult = ares as HttpStreamAsyncResult;
			if (ares == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "ares");
			}
			if (!ares.IsCompleted)
			{
				ares.AsyncWaitHandle.WaitOne();
			}
			if (httpStreamAsyncResult.Error != null)
			{
				throw new HttpListenerException(400, "I/O operation aborted: " + httpStreamAsyncResult.Error.Message);
			}
			return httpStreamAsyncResult.Count;
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0006CBE6 File Offset: 0x0006ADE6
		public override void Close()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				base.Close();
			}
		}

		// Token: 0x04001020 RID: 4128
		private bool disposed;

		// Token: 0x04001021 RID: 4129
		private MonoChunkParser decoder;

		// Token: 0x04001022 RID: 4130
		private HttpListenerContext context;

		// Token: 0x04001023 RID: 4131
		private bool no_more_data;

		// Token: 0x020003FE RID: 1022
		private class ReadBufferState
		{
			// Token: 0x06001960 RID: 6496 RVA: 0x0006CBFD File Offset: 0x0006ADFD
			public ReadBufferState(byte[] buffer, int offset, int count, HttpStreamAsyncResult ares)
			{
				this.Buffer = buffer;
				this.Offset = offset;
				this.Count = count;
				this.InitialCount = count;
				this.Ares = ares;
			}

			// Token: 0x04001024 RID: 4132
			public byte[] Buffer;

			// Token: 0x04001025 RID: 4133
			public int Offset;

			// Token: 0x04001026 RID: 4134
			public int Count;

			// Token: 0x04001027 RID: 4135
			public int InitialCount;

			// Token: 0x04001028 RID: 4136
			public HttpStreamAsyncResult Ares;
		}
	}
}
