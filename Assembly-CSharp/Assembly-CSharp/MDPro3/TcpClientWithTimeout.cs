using System;
using System.Net.Sockets;
using System.Threading;

namespace MDPro3
{
	// Token: 0x0200121F RID: 4639
	public class TcpClientWithTimeout
	{
		// Token: 0x06008982 RID: 35202 RVA: 0x0010ABC2 File Offset: 0x00108DC2
		public TcpClientWithTimeout(string hostname, int port, int timeout_milliseconds)
		{
			this._hostname = hostname;
			this._port = port;
			this._timeout_milliseconds = timeout_milliseconds;
		}

		// Token: 0x06008983 RID: 35203 RVA: 0x0010ABE0 File Offset: 0x00108DE0
		public TcpClient Connect()
		{
			this.connected = false;
			this.exception = null;
			Thread thread = new Thread(new ThreadStart(this.BeginConnect));
			thread.IsBackground = true;
			thread.Start();
			thread.Join(this._timeout_milliseconds);
			if (this.connected)
			{
				thread.Abort();
				return this.connection;
			}
			if (this.exception != null)
			{
				thread.Abort();
				TcpHelper.onDisConnected = true;
				throw this.exception;
			}
			thread.Abort();
			string text = string.Format("TcpClient connection to {0}:{1} timed out", this._hostname, this._port);
			TcpHelper.onDisConnected = true;
			throw new TimeoutException(text);
		}

		// Token: 0x06008984 RID: 35204 RVA: 0x0010AC84 File Offset: 0x00108E84
		protected void BeginConnect()
		{
			try
			{
				this.connection = new TcpClient(this._hostname, this._port);
				this.connected = true;
			}
			catch (Exception ex)
			{
				this.exception = ex;
			}
		}

		// Token: 0x0400C484 RID: 50308
		protected string _hostname;

		// Token: 0x0400C485 RID: 50309
		protected int _port;

		// Token: 0x0400C486 RID: 50310
		protected int _timeout_milliseconds;

		// Token: 0x0400C487 RID: 50311
		protected bool connected;

		// Token: 0x0400C488 RID: 50312
		protected TcpClient connection;

		// Token: 0x0400C489 RID: 50313
		protected Exception exception;
	}
}
