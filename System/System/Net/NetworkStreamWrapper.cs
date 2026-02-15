using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x0200039D RID: 925
	internal class NetworkStreamWrapper : Stream
	{
		// Token: 0x06001736 RID: 5942 RVA: 0x00063C9A File Offset: 0x00061E9A
		internal NetworkStreamWrapper(TcpClient client)
		{
			this._client = client;
			this._networkStream = client.GetStream();
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001737 RID: 5943 RVA: 0x00063CB5 File Offset: 0x00061EB5
		protected bool UsingSecureStream
		{
			get
			{
				return this._networkStream is TlsStream;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x00063CC5 File Offset: 0x00061EC5
		internal IPAddress ServerAddress
		{
			get
			{
				return ((IPEndPoint)this.Socket.RemoteEndPoint).Address;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x00063CDC File Offset: 0x00061EDC
		internal Socket Socket
		{
			get
			{
				return this._client.Client;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x00063CE9 File Offset: 0x00061EE9
		// (set) Token: 0x0600173B RID: 5947 RVA: 0x00063CF1 File Offset: 0x00061EF1
		internal NetworkStream NetworkStream
		{
			get
			{
				return this._networkStream;
			}
			set
			{
				this._networkStream = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x00063CFA File Offset: 0x00061EFA
		public override bool CanRead
		{
			get
			{
				return this._networkStream.CanRead;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x00063D07 File Offset: 0x00061F07
		public override bool CanSeek
		{
			get
			{
				return this._networkStream.CanSeek;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x00063D14 File Offset: 0x00061F14
		public override bool CanWrite
		{
			get
			{
				return this._networkStream.CanWrite;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x00063D21 File Offset: 0x00061F21
		public override bool CanTimeout
		{
			get
			{
				return this._networkStream.CanTimeout;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x00063D2E File Offset: 0x00061F2E
		// (set) Token: 0x06001741 RID: 5953 RVA: 0x00063D3B File Offset: 0x00061F3B
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

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x00063D49 File Offset: 0x00061F49
		// (set) Token: 0x06001743 RID: 5955 RVA: 0x00063D56 File Offset: 0x00061F56
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

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x00063D64 File Offset: 0x00061F64
		public override long Length
		{
			get
			{
				return this._networkStream.Length;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x00063D71 File Offset: 0x00061F71
		// (set) Token: 0x06001746 RID: 5958 RVA: 0x00063D7E File Offset: 0x00061F7E
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

		// Token: 0x06001747 RID: 5959 RVA: 0x00063D8C File Offset: 0x00061F8C
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._networkStream.Seek(offset, origin);
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x00063D9B File Offset: 0x00061F9B
		public override int Read(byte[] buffer, int offset, int size)
		{
			return this._networkStream.Read(buffer, offset, size);
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x00063DAB File Offset: 0x00061FAB
		public override void Write(byte[] buffer, int offset, int size)
		{
			this._networkStream.Write(buffer, offset, size);
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x00063DBC File Offset: 0x00061FBC
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.CloseSocket();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x00063DEC File Offset: 0x00061FEC
		internal void CloseSocket()
		{
			this._networkStream.Close();
			this._client.Dispose();
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00063E04 File Offset: 0x00062004
		public void Close(int timeout)
		{
			this._networkStream.Close(timeout);
			this._client.Dispose();
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00063E1D File Offset: 0x0006201D
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this._networkStream.BeginRead(buffer, offset, size, callback, state);
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00063E31 File Offset: 0x00062031
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this._networkStream.EndRead(asyncResult);
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x00063E3F File Offset: 0x0006203F
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._networkStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x00063E51 File Offset: 0x00062051
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this._networkStream.BeginWrite(buffer, offset, size, callback, state);
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x00063E65 File Offset: 0x00062065
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this._networkStream.EndWrite(asyncResult);
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x00063E73 File Offset: 0x00062073
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._networkStream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x00063E85 File Offset: 0x00062085
		public override void Flush()
		{
			this._networkStream.Flush();
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x00063E92 File Offset: 0x00062092
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this._networkStream.FlushAsync(cancellationToken);
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x00063EA0 File Offset: 0x000620A0
		public override void SetLength(long value)
		{
			this._networkStream.SetLength(value);
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x00063EAE File Offset: 0x000620AE
		internal void SetSocketTimeoutOption(int timeout)
		{
			this._networkStream.ReadTimeout = timeout;
			this._networkStream.WriteTimeout = timeout;
		}

		// Token: 0x04000E50 RID: 3664
		private TcpClient _client;

		// Token: 0x04000E51 RID: 3665
		private NetworkStream _networkStream;
	}
}
