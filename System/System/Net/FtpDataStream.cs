using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace System.Net
{
	// Token: 0x02000392 RID: 914
	internal class FtpDataStream : Stream, ICloseEx
	{
		// Token: 0x060016D7 RID: 5847 RVA: 0x00061D18 File Offset: 0x0005FF18
		internal FtpDataStream(NetworkStream networkStream, FtpWebRequest request, TriState writeOnly)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, ".ctor");
			}
			this._readable = true;
			this._writeable = true;
			if (writeOnly == TriState.True)
			{
				this._readable = false;
			}
			else if (writeOnly == TriState.False)
			{
				this._writeable = false;
			}
			this._networkStream = networkStream;
			this._request = request;
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x00061D74 File Offset: 0x0005FF74
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					((ICloseEx)this).CloseEx(CloseExState.Normal);
				}
				else
				{
					((ICloseEx)this).CloseEx(CloseExState.Abort | CloseExState.Silent);
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x00061DB0 File Offset: 0x0005FFB0
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("state = {0}", new object[] { closeState }), "CloseEx");
			}
			lock (this)
			{
				if (this._closing)
				{
					return;
				}
				this._closing = true;
				this._writeable = false;
				this._readable = false;
			}
			try
			{
				try
				{
					if ((closeState & CloseExState.Abort) == CloseExState.Normal)
					{
						this._networkStream.Close(-1);
					}
					else
					{
						this._networkStream.Close(0);
					}
				}
				finally
				{
					this._request.DataStreamClosed(closeState);
				}
			}
			catch (Exception ex)
			{
				bool flag2 = true;
				WebException ex2 = ex as WebException;
				if (ex2 != null)
				{
					FtpWebResponse ftpWebResponse = ex2.Response as FtpWebResponse;
					if (ftpWebResponse != null && !this._isFullyRead && ftpWebResponse.StatusCode == FtpStatusCode.ConnectionClosed)
					{
						flag2 = false;
					}
				}
				if (flag2 && (closeState & CloseExState.Silent) == CloseExState.Normal)
				{
					throw;
				}
			}
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x00061EBC File Offset: 0x000600BC
		private void CheckError()
		{
			if (this._request.Aborted)
			{
				throw ExceptionHelper.RequestAbortedException;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x00061ED1 File Offset: 0x000600D1
		public override bool CanRead
		{
			get
			{
				return this._readable;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x00061ED9 File Offset: 0x000600D9
		public override bool CanSeek
		{
			get
			{
				return this._networkStream.CanSeek;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x00061EE6 File Offset: 0x000600E6
		public override bool CanWrite
		{
			get
			{
				return this._writeable;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x00061EEE File Offset: 0x000600EE
		public override long Length
		{
			get
			{
				return this._networkStream.Length;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x00061EFB File Offset: 0x000600FB
		// (set) Token: 0x060016E0 RID: 5856 RVA: 0x00061F08 File Offset: 0x00060108
		public override long Position
		{
			get
			{
				return this._networkStream.Position;
			}
			set
			{
				this._networkStream.Position = value;
			}
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x00061F18 File Offset: 0x00060118
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckError();
			long num;
			try
			{
				num = this._networkStream.Seek(offset, origin);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return num;
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00061F58 File Offset: 0x00060158
		public override int Read(byte[] buffer, int offset, int size)
		{
			this.CheckError();
			int num;
			try
			{
				num = this._networkStream.Read(buffer, offset, size);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			if (num == 0)
			{
				this._isFullyRead = true;
				this.Close();
			}
			return num;
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x00061FA8 File Offset: 0x000601A8
		public override void Write(byte[] buffer, int offset, int size)
		{
			this.CheckError();
			try
			{
				this._networkStream.Write(buffer, offset, size);
			}
			catch
			{
				this.CheckError();
				throw;
			}
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00061FE4 File Offset: 0x000601E4
		private void AsyncReadCallback(IAsyncResult ar)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)ar.AsyncState;
			try
			{
				try
				{
					int num = this._networkStream.EndRead(ar);
					if (num == 0)
					{
						this._isFullyRead = true;
						this.Close();
					}
					lazyAsyncResult.InvokeCallback(num);
				}
				catch (Exception ex)
				{
					if (!lazyAsyncResult.IsCompleted)
					{
						lazyAsyncResult.InvokeCallback(ex);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0006205C File Offset: 0x0006025C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckError();
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this, state, callback);
			try
			{
				this._networkStream.BeginRead(buffer, offset, size, new AsyncCallback(this.AsyncReadCallback), lazyAsyncResult);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return lazyAsyncResult;
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x000620B4 File Offset: 0x000602B4
		public override int EndRead(IAsyncResult ar)
		{
			int num;
			try
			{
				object obj = ((LazyAsyncResult)ar).InternalWaitForCompletion();
				Exception ex = obj as Exception;
				if (ex != null)
				{
					ExceptionDispatchInfo.Throw(ex);
				}
				num = (int)obj;
			}
			finally
			{
				this.CheckError();
			}
			return num;
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x000620FC File Offset: 0x000602FC
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckError();
			IAsyncResult asyncResult;
			try
			{
				asyncResult = this._networkStream.BeginWrite(buffer, offset, size, callback, state);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return asyncResult;
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00062140 File Offset: 0x00060340
		public override void EndWrite(IAsyncResult asyncResult)
		{
			try
			{
				this._networkStream.EndWrite(asyncResult);
			}
			finally
			{
				this.CheckError();
			}
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x00062174 File Offset: 0x00060374
		public override void Flush()
		{
			this._networkStream.Flush();
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x00062181 File Offset: 0x00060381
		public override void SetLength(long value)
		{
			this._networkStream.SetLength(value);
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x0006218F File Offset: 0x0006038F
		public override bool CanTimeout
		{
			get
			{
				return this._networkStream.CanTimeout;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x0006219C File Offset: 0x0006039C
		// (set) Token: 0x060016ED RID: 5869 RVA: 0x000621A9 File Offset: 0x000603A9
		public override int ReadTimeout
		{
			get
			{
				return this._networkStream.ReadTimeout;
			}
			set
			{
				this._networkStream.ReadTimeout = value;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x000621B7 File Offset: 0x000603B7
		// (set) Token: 0x060016EF RID: 5871 RVA: 0x000621C4 File Offset: 0x000603C4
		public override int WriteTimeout
		{
			get
			{
				return this._networkStream.WriteTimeout;
			}
			set
			{
				this._networkStream.WriteTimeout = value;
			}
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x000621D2 File Offset: 0x000603D2
		internal void SetSocketTimeoutOption(int timeout)
		{
			this._networkStream.ReadTimeout = timeout;
			this._networkStream.WriteTimeout = timeout;
		}

		// Token: 0x04000DF2 RID: 3570
		private FtpWebRequest _request;

		// Token: 0x04000DF3 RID: 3571
		private NetworkStream _networkStream;

		// Token: 0x04000DF4 RID: 3572
		private bool _writeable;

		// Token: 0x04000DF5 RID: 3573
		private bool _readable;

		// Token: 0x04000DF6 RID: 3574
		private bool _isFullyRead;

		// Token: 0x04000DF7 RID: 3575
		private bool _closing;
	}
}
