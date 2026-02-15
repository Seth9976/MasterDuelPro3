using System;
using System.IO;

namespace System.Net
{
	// Token: 0x020003EC RID: 1004
	internal sealed class FileWebStream : FileStream, ICloseEx
	{
		// Token: 0x06001901 RID: 6401 RVA: 0x0006B004 File Offset: 0x00069204
		public FileWebStream(FileWebRequest request, string path, FileMode mode, FileAccess access, FileShare sharing)
			: base(path, mode, access, sharing)
		{
			this.m_request = request;
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0006B019 File Offset: 0x00069219
		public FileWebStream(FileWebRequest request, string path, FileMode mode, FileAccess access, FileShare sharing, int length, bool async)
			: base(path, mode, access, sharing, length, async)
		{
			this.m_request = request;
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x0006B034 File Offset: 0x00069234
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.m_request != null)
				{
					this.m_request.UnblockReader();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0006B074 File Offset: 0x00069274
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			if ((closeState & CloseExState.Abort) != CloseExState.Normal)
			{
				this.SafeFileHandle.Close();
				return;
			}
			this.Close();
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0006B090 File Offset: 0x00069290
		public override int Read(byte[] buffer, int offset, int size)
		{
			this.CheckError();
			int num;
			try
			{
				num = base.Read(buffer, offset, size);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return num;
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x0006B0CC File Offset: 0x000692CC
		public override void Write(byte[] buffer, int offset, int size)
		{
			this.CheckError();
			try
			{
				base.Write(buffer, offset, size);
			}
			catch
			{
				this.CheckError();
				throw;
			}
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x0006B104 File Offset: 0x00069304
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckError();
			IAsyncResult asyncResult;
			try
			{
				asyncResult = base.BeginRead(buffer, offset, size, callback, state);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return asyncResult;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x0006B144 File Offset: 0x00069344
		public override int EndRead(IAsyncResult ar)
		{
			int num;
			try
			{
				num = base.EndRead(ar);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return num;
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x0006B178 File Offset: 0x00069378
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckError();
			IAsyncResult asyncResult;
			try
			{
				asyncResult = base.BeginWrite(buffer, offset, size, callback, state);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return asyncResult;
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x0006B1B8 File Offset: 0x000693B8
		public override void EndWrite(IAsyncResult ar)
		{
			try
			{
				base.EndWrite(ar);
			}
			catch
			{
				this.CheckError();
				throw;
			}
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x0006B1E8 File Offset: 0x000693E8
		private void CheckError()
		{
			if (this.m_request.Aborted)
			{
				throw new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
			}
		}

		// Token: 0x04000FF3 RID: 4083
		private FileWebRequest m_request;
	}
}
