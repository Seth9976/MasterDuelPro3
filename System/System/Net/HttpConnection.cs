using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200040A RID: 1034
	internal sealed class HttpConnection
	{
		// Token: 0x060019A8 RID: 6568 RVA: 0x0006E154 File Offset: 0x0006C354
		public HttpConnection(Socket sock, EndPointListener epl, bool secure, X509Certificate cert)
		{
			this.sock = sock;
			this.epl = epl;
			this.secure = secure;
			this.cert = cert;
			if (!secure)
			{
				this.stream = new NetworkStream(sock, false);
			}
			else
			{
				this.ssl_stream = epl.Listener.CreateSslStream(new NetworkStream(sock, false), false, delegate(object t, X509Certificate c, X509Chain ch, SslPolicyErrors e)
				{
					if (c == null)
					{
						return true;
					}
					X509Certificate2 x509Certificate = c as X509Certificate2;
					if (x509Certificate == null)
					{
						x509Certificate = new X509Certificate2(c.GetRawCertData());
					}
					this.client_cert = x509Certificate;
					this.client_cert_errors = new int[] { (int)e };
					return true;
				});
				this.stream = this.ssl_stream;
			}
			this.timer = new Timer(new TimerCallback(this.OnTimeout), null, -1, -1);
			if (this.ssl_stream != null)
			{
				this.ssl_stream.AuthenticateAsServer(cert, true, (SslProtocols)ServicePointManager.SecurityProtocol, false);
			}
			this.Init();
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x0006E210 File Offset: 0x0006C410
		private void Init()
		{
			this.context_bound = false;
			this.i_stream = null;
			this.o_stream = null;
			this.prefix = null;
			this.chunked = false;
			this.ms = new MemoryStream();
			this.position = 0;
			this.input_state = HttpConnection.InputState.RequestLine;
			this.line_state = HttpConnection.LineState.None;
			this.context = new HttpListenerContext(this);
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x0006E26C File Offset: 0x0006C46C
		public int Reuses
		{
			get
			{
				return this.reuses;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x0006E274 File Offset: 0x0006C474
		public IPEndPoint LocalEndPoint
		{
			get
			{
				if (this.local_ep != null)
				{
					return this.local_ep;
				}
				this.local_ep = (IPEndPoint)this.sock.LocalEndPoint;
				return this.local_ep;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060019AC RID: 6572 RVA: 0x0006E2A1 File Offset: 0x0006C4A1
		public bool IsSecure
		{
			get
			{
				return this.secure;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (set) Token: 0x060019AD RID: 6573 RVA: 0x0006E2A9 File Offset: 0x0006C4A9
		public ListenerPrefix Prefix
		{
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x0006E2B2 File Offset: 0x0006C4B2
		private void OnTimeout(object unused)
		{
			this.CloseSocket();
			this.Unbind();
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x0006E2C0 File Offset: 0x0006C4C0
		public void BeginReadRequest()
		{
			if (this.buffer == null)
			{
				this.buffer = new byte[8192];
			}
			try
			{
				if (this.reuses == 1)
				{
					this.s_timeout = 15000;
				}
				this.timer.Change(this.s_timeout, -1);
				this.stream.BeginRead(this.buffer, 0, 8192, HttpConnection.onread_cb, this);
			}
			catch
			{
				this.timer.Change(-1, -1);
				this.CloseSocket();
				this.Unbind();
			}
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x0006E35C File Offset: 0x0006C55C
		public RequestStream GetRequestStream(bool chunked, long contentlength)
		{
			if (this.i_stream == null)
			{
				byte[] array = this.ms.GetBuffer();
				int num = (int)this.ms.Length;
				this.ms = null;
				if (chunked)
				{
					this.chunked = true;
					this.context.Response.SendChunked = true;
					this.i_stream = new ChunkedInputStream(this.context, this.stream, array, this.position, num - this.position);
				}
				else
				{
					this.i_stream = new RequestStream(this.stream, array, this.position, num - this.position, contentlength);
				}
			}
			return this.i_stream;
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0006E400 File Offset: 0x0006C600
		public ResponseStream GetResponseStream()
		{
			if (this.o_stream == null)
			{
				HttpListener listener = this.context.Listener;
				if (listener == null)
				{
					return new ResponseStream(this.stream, this.context.Response, true);
				}
				this.o_stream = new ResponseStream(this.stream, this.context.Response, listener.IgnoreWriteExceptions);
			}
			return this.o_stream;
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x0006E464 File Offset: 0x0006C664
		private static void OnRead(IAsyncResult ares)
		{
			((HttpConnection)ares.AsyncState).OnReadInternal(ares);
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x0006E478 File Offset: 0x0006C678
		private void OnReadInternal(IAsyncResult ares)
		{
			this.timer.Change(-1, -1);
			int num = -1;
			try
			{
				num = this.stream.EndRead(ares);
				this.ms.Write(this.buffer, 0, num);
				if (this.ms.Length > 32768L)
				{
					this.SendError("Bad request", 400);
					this.Close(true);
					return;
				}
			}
			catch
			{
				if (this.ms != null && this.ms.Length > 0L)
				{
					this.SendError();
				}
				if (this.sock != null)
				{
					this.CloseSocket();
					this.Unbind();
				}
				return;
			}
			if (num == 0)
			{
				this.CloseSocket();
				this.Unbind();
				return;
			}
			if (this.ProcessInput(this.ms))
			{
				if (!this.context.HaveError && !this.context.Request.FinishInitialization())
				{
					this.Close(true);
					return;
				}
				if (this.context.HaveError)
				{
					this.SendError();
					this.Close(true);
					return;
				}
				if (!this.epl.BindContext(this.context))
				{
					this.SendError("Invalid host", 400);
					this.Close(true);
					return;
				}
				HttpListener listener = this.context.Listener;
				if (this.last_listener != listener)
				{
					this.RemoveConnection();
					listener.AddConnection(this);
					this.last_listener = listener;
				}
				this.context_bound = true;
				listener.RegisterContext(this.context);
				return;
			}
			else
			{
				this.stream.BeginRead(this.buffer, 0, 8192, HttpConnection.onread_cb, this);
			}
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x0006E618 File Offset: 0x0006C818
		private void RemoveConnection()
		{
			if (this.last_listener == null)
			{
				this.epl.RemoveConnection(this);
				return;
			}
			this.last_listener.RemoveConnection(this);
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x0006E63C File Offset: 0x0006C83C
		private bool ProcessInput(MemoryStream ms)
		{
			byte[] array = ms.GetBuffer();
			int num = (int)ms.Length;
			int num2 = 0;
			while (!this.context.HaveError)
			{
				if (this.position < num)
				{
					string text;
					try
					{
						text = this.ReadLine(array, this.position, num - this.position, ref num2);
						this.position += num2;
					}
					catch
					{
						this.context.ErrorMessage = "Bad request";
						this.context.ErrorStatus = 400;
						return true;
					}
					if (text == null)
					{
						goto IL_010D;
					}
					if (text == "")
					{
						if (this.input_state != HttpConnection.InputState.RequestLine)
						{
							this.current_line = null;
							ms = null;
							return true;
						}
						continue;
					}
					else
					{
						if (this.input_state == HttpConnection.InputState.RequestLine)
						{
							this.context.Request.SetRequestLine(text);
							this.input_state = HttpConnection.InputState.Headers;
							continue;
						}
						try
						{
							this.context.Request.AddHeader(text);
							continue;
						}
						catch (Exception ex)
						{
							this.context.ErrorMessage = ex.Message;
							this.context.ErrorStatus = 400;
							return true;
						}
						goto IL_010D;
					}
					bool flag;
					return flag;
				}
				IL_010D:
				if (num2 == num)
				{
					ms.SetLength(0L);
					this.position = 0;
				}
				return false;
			}
			return true;
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x0006E78C File Offset: 0x0006C98C
		private string ReadLine(byte[] buffer, int offset, int len, ref int used)
		{
			if (this.current_line == null)
			{
				this.current_line = new StringBuilder(128);
			}
			int num = offset + len;
			used = 0;
			int num2 = offset;
			while (num2 < num && this.line_state != HttpConnection.LineState.LF)
			{
				used++;
				byte b = buffer[num2];
				if (b == 13)
				{
					this.line_state = HttpConnection.LineState.CR;
				}
				else if (b == 10)
				{
					this.line_state = HttpConnection.LineState.LF;
				}
				else
				{
					this.current_line.Append((char)b);
				}
				num2++;
			}
			string text = null;
			if (this.line_state == HttpConnection.LineState.LF)
			{
				this.line_state = HttpConnection.LineState.None;
				text = this.current_line.ToString();
				this.current_line.Length = 0;
			}
			return text;
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x0006E830 File Offset: 0x0006CA30
		public void SendError(string msg, int status)
		{
			try
			{
				HttpListenerResponse response = this.context.Response;
				response.StatusCode = status;
				response.ContentType = "text/html";
				string text = HttpStatusDescription.Get(status);
				string text2;
				if (msg != null)
				{
					text2 = string.Format("<h1>{0} ({1})</h1>", text, msg);
				}
				else
				{
					text2 = string.Format("<h1>{0}</h1>", text);
				}
				byte[] bytes = this.context.Response.ContentEncoding.GetBytes(text2);
				response.Close(bytes, false);
			}
			catch
			{
			}
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x0006E8B4 File Offset: 0x0006CAB4
		public void SendError()
		{
			this.SendError(this.context.ErrorMessage, this.context.ErrorStatus);
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x0006E8D2 File Offset: 0x0006CAD2
		private void Unbind()
		{
			if (this.context_bound)
			{
				this.epl.UnbindContext(this.context);
				this.context_bound = false;
			}
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x0006E8F4 File Offset: 0x0006CAF4
		private void CloseSocket()
		{
			if (this.sock == null)
			{
				return;
			}
			try
			{
				this.sock.Close();
			}
			catch
			{
			}
			finally
			{
				this.sock = null;
			}
			this.RemoveConnection();
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x0006E948 File Offset: 0x0006CB48
		internal void Close(bool force_close)
		{
			if (this.sock != null)
			{
				Stream responseStream = this.GetResponseStream();
				if (responseStream != null)
				{
					responseStream.Close();
				}
				this.o_stream = null;
			}
			if (this.sock == null)
			{
				return;
			}
			force_close |= !this.context.Request.KeepAlive;
			if (!force_close)
			{
				force_close = this.context.Response.Headers["connection"] == "close";
			}
			if (force_close || !this.context.Request.FlushInput())
			{
				Socket socket = this.sock;
				this.sock = null;
				try
				{
					if (socket != null)
					{
						socket.Shutdown(SocketShutdown.Both);
					}
				}
				catch
				{
				}
				finally
				{
					if (socket != null)
					{
						socket.Close();
					}
				}
				this.Unbind();
				this.RemoveConnection();
				return;
			}
			if (this.chunked && !this.context.Response.ForceCloseChunked)
			{
				this.reuses++;
				this.Unbind();
				this.Init();
				this.BeginReadRequest();
				return;
			}
			this.reuses++;
			this.Unbind();
			this.Init();
			this.BeginReadRequest();
		}

		// Token: 0x0400104F RID: 4175
		private static AsyncCallback onread_cb = new AsyncCallback(HttpConnection.OnRead);

		// Token: 0x04001050 RID: 4176
		private Socket sock;

		// Token: 0x04001051 RID: 4177
		private Stream stream;

		// Token: 0x04001052 RID: 4178
		private EndPointListener epl;

		// Token: 0x04001053 RID: 4179
		private MemoryStream ms;

		// Token: 0x04001054 RID: 4180
		private byte[] buffer;

		// Token: 0x04001055 RID: 4181
		private HttpListenerContext context;

		// Token: 0x04001056 RID: 4182
		private StringBuilder current_line;

		// Token: 0x04001057 RID: 4183
		private ListenerPrefix prefix;

		// Token: 0x04001058 RID: 4184
		private RequestStream i_stream;

		// Token: 0x04001059 RID: 4185
		private ResponseStream o_stream;

		// Token: 0x0400105A RID: 4186
		private bool chunked;

		// Token: 0x0400105B RID: 4187
		private int reuses;

		// Token: 0x0400105C RID: 4188
		private bool context_bound;

		// Token: 0x0400105D RID: 4189
		private bool secure;

		// Token: 0x0400105E RID: 4190
		private X509Certificate cert;

		// Token: 0x0400105F RID: 4191
		private int s_timeout = 90000;

		// Token: 0x04001060 RID: 4192
		private Timer timer;

		// Token: 0x04001061 RID: 4193
		private IPEndPoint local_ep;

		// Token: 0x04001062 RID: 4194
		private HttpListener last_listener;

		// Token: 0x04001063 RID: 4195
		private int[] client_cert_errors;

		// Token: 0x04001064 RID: 4196
		private X509Certificate2 client_cert;

		// Token: 0x04001065 RID: 4197
		private SslStream ssl_stream;

		// Token: 0x04001066 RID: 4198
		private HttpConnection.InputState input_state;

		// Token: 0x04001067 RID: 4199
		private HttpConnection.LineState line_state;

		// Token: 0x04001068 RID: 4200
		private int position;

		// Token: 0x0200040B RID: 1035
		private enum InputState
		{
			// Token: 0x0400106A RID: 4202
			RequestLine,
			// Token: 0x0400106B RID: 4203
			Headers
		}

		// Token: 0x0200040C RID: 1036
		private enum LineState
		{
			// Token: 0x0400106D RID: 4205
			None,
			// Token: 0x0400106E RID: 4206
			CR,
			// Token: 0x0400106F RID: 4207
			LF
		}
	}
}
