using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace System.Net
{
	// Token: 0x02000381 RID: 897
	internal class TlsStream : NetworkStream
	{
		// Token: 0x0600164D RID: 5709 RVA: 0x0005ECD3 File Offset: 0x0005CED3
		public TlsStream(NetworkStream stream, Socket socket, string host, X509CertificateCollection clientCertificates)
			: base(socket)
		{
			this._sslStream = new SslStream(stream, false, ServicePointManager.ServerCertificateValidationCallback);
			this._host = host;
			this._clientCertificates = clientCertificates;
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x0005ECFD File Offset: 0x0005CEFD
		public void AuthenticateAsClient()
		{
			this._sslStream.AuthenticateAsClient(this._host, this._clientCertificates, (SslProtocols)ServicePointManager.SecurityProtocol, ServicePointManager.CheckCertificateRevocationList);
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x0005ED20 File Offset: 0x0005CF20
		public IAsyncResult BeginAuthenticateAsClient(AsyncCallback asyncCallback, object state)
		{
			return this._sslStream.BeginAuthenticateAsClient(this._host, this._clientCertificates, (SslProtocols)ServicePointManager.SecurityProtocol, ServicePointManager.CheckCertificateRevocationList, asyncCallback, state);
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x0005ED45 File Offset: 0x0005CF45
		public void EndAuthenticateAsClient(IAsyncResult asyncResult)
		{
			this._sslStream.EndAuthenticateAsClient(asyncResult);
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x0005ED53 File Offset: 0x0005CF53
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this._sslStream.BeginWrite(buffer, offset, size, callback, state);
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x0005ED67 File Offset: 0x0005CF67
		public override void EndWrite(IAsyncResult result)
		{
			this._sslStream.EndWrite(result);
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x0005ED75 File Offset: 0x0005CF75
		public override void Write(byte[] buffer, int offset, int size)
		{
			this._sslStream.Write(buffer, offset, size);
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x0005ED85 File Offset: 0x0005CF85
		public override int Read(byte[] buffer, int offset, int size)
		{
			return this._sslStream.Read(buffer, offset, size);
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0005ED95 File Offset: 0x0005CF95
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this._sslStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x0005EDA9 File Offset: 0x0005CFA9
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this._sslStream.EndRead(asyncResult);
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x0005EDB7 File Offset: 0x0005CFB7
		public override void Close()
		{
			base.Close();
			if (this._sslStream != null)
			{
				this._sslStream.Close();
			}
		}

		// Token: 0x04000D52 RID: 3410
		private SslStream _sslStream;

		// Token: 0x04000D53 RID: 3411
		private string _host;

		// Token: 0x04000D54 RID: 3412
		private X509CertificateCollection _clientCertificates;
	}
}
