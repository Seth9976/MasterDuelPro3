using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x0200043F RID: 1087
	internal class WebConnectionTunnel
	{
		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x00077B2E File Offset: 0x00075D2E
		public HttpWebRequest Request { get; }

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x00077B36 File Offset: 0x00075D36
		public Uri ConnectUri { get; }

		// Token: 0x06001B88 RID: 7048 RVA: 0x00077B3E File Offset: 0x00075D3E
		public WebConnectionTunnel(HttpWebRequest request, Uri connectUri)
		{
			this.Request = request;
			this.ConnectUri = connectUri;
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x00077B54 File Offset: 0x00075D54
		// (set) Token: 0x06001B8A RID: 7050 RVA: 0x00077B5C File Offset: 0x00075D5C
		public bool Success { get; private set; }

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x00077B65 File Offset: 0x00075D65
		// (set) Token: 0x06001B8C RID: 7052 RVA: 0x00077B6D File Offset: 0x00075D6D
		public bool CloseConnection { get; private set; }

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x00077B76 File Offset: 0x00075D76
		// (set) Token: 0x06001B8E RID: 7054 RVA: 0x00077B7E File Offset: 0x00075D7E
		public int StatusCode { get; private set; }

		// Token: 0x17000618 RID: 1560
		// (set) Token: 0x06001B8F RID: 7055 RVA: 0x00077B87 File Offset: 0x00075D87
		private string StatusDescription
		{
			[CompilerGenerated]
			set
			{
				this.<StatusDescription>k__BackingField = value;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x00077B90 File Offset: 0x00075D90
		// (set) Token: 0x06001B91 RID: 7057 RVA: 0x00077B98 File Offset: 0x00075D98
		public string[] Challenge { get; private set; }

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001B92 RID: 7058 RVA: 0x00077BA1 File Offset: 0x00075DA1
		// (set) Token: 0x06001B93 RID: 7059 RVA: 0x00077BA9 File Offset: 0x00075DA9
		public WebHeaderCollection Headers { get; private set; }

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x00077BB2 File Offset: 0x00075DB2
		// (set) Token: 0x06001B95 RID: 7061 RVA: 0x00077BBA File Offset: 0x00075DBA
		public Version ProxyVersion { get; private set; }

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001B96 RID: 7062 RVA: 0x00077BC3 File Offset: 0x00075DC3
		// (set) Token: 0x06001B97 RID: 7063 RVA: 0x00077BCB File Offset: 0x00075DCB
		public byte[] Data { get; private set; }

		// Token: 0x06001B98 RID: 7064 RVA: 0x00077BD4 File Offset: 0x00075DD4
		internal async Task Initialize(Stream stream, CancellationToken cancellationToken)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CONNECT ");
			stringBuilder.Append(this.Request.Address.Host);
			stringBuilder.Append(':');
			stringBuilder.Append(this.Request.Address.Port);
			stringBuilder.Append(" HTTP/");
			if (this.Request.ProtocolVersion == HttpVersion.Version11)
			{
				stringBuilder.Append("1.1");
			}
			else
			{
				stringBuilder.Append("1.0");
			}
			stringBuilder.Append("\r\nHost: ");
			stringBuilder.Append(this.Request.Address.Authority);
			bool flag = false;
			string[] challenge = this.Challenge;
			this.Challenge = null;
			string text = this.Request.Headers["Proxy-Authorization"];
			bool have_auth = text != null;
			if (have_auth)
			{
				stringBuilder.Append("\r\nProxy-Authorization: ");
				stringBuilder.Append(text);
				flag = text.ToUpper().Contains("NTLM");
			}
			else if (challenge != null && this.StatusCode == 407)
			{
				ICredentials credentials = this.Request.Proxy.Credentials;
				have_auth = true;
				if (this.connectRequest == null)
				{
					this.connectRequest = (HttpWebRequest)WebRequest.Create(string.Concat(new string[]
					{
						this.ConnectUri.Scheme,
						"://",
						this.ConnectUri.Host,
						":",
						this.ConnectUri.Port.ToString(),
						"/"
					}));
					this.connectRequest.Method = "CONNECT";
					this.connectRequest.Credentials = credentials;
				}
				if (credentials != null)
				{
					for (int i = 0; i < challenge.Length; i++)
					{
						Authorization authorization = AuthenticationManager.Authenticate(challenge[i], this.connectRequest, credentials);
						if (authorization != null)
						{
							flag = authorization.ModuleAuthenticationType == "NTLM";
							stringBuilder.Append("\r\nProxy-Authorization: ");
							stringBuilder.Append(authorization.Message);
							break;
						}
					}
				}
			}
			if (flag)
			{
				stringBuilder.Append("\r\nProxy-Connection: keep-alive");
				this.ntlmAuthState++;
			}
			stringBuilder.Append("\r\n\r\n");
			this.StatusCode = 0;
			byte[] bytes = Encoding.Default.GetBytes(stringBuilder.ToString());
			await stream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
			ValueTuple<WebHeaderCollection, byte[], int> valueTuple = await this.ReadHeaders(stream, cancellationToken).ConfigureAwait(false);
			this.Headers = valueTuple.Item1;
			this.Data = valueTuple.Item2;
			this.StatusCode = valueTuple.Item3;
			if ((!have_auth || this.ntlmAuthState == WebConnectionTunnel.NtlmAuthState.Challenge) && this.Headers != null && this.StatusCode == 407)
			{
				string text2 = this.Headers["Connection"];
				if (!string.IsNullOrEmpty(text2) && text2.ToLower() == "close")
				{
					this.CloseConnection = true;
				}
				this.Challenge = this.Headers.GetValues("Proxy-Authenticate");
				this.Success = false;
			}
			else
			{
				this.Success = this.StatusCode == 200 && this.Headers != null;
			}
			if (this.Challenge == null && (this.StatusCode == 401 || this.StatusCode == 407))
			{
				HttpWebResponse httpWebResponse = new HttpWebResponse(this.ConnectUri, "CONNECT", (HttpStatusCode)this.StatusCode, this.Headers);
				throw new WebException((this.StatusCode == 407) ? "(407) Proxy Authentication Required" : "(401) Unauthorized", null, WebExceptionStatus.ProtocolError, httpWebResponse);
			}
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x00077C28 File Offset: 0x00075E28
		private async Task<ValueTuple<WebHeaderCollection, byte[], int>> ReadHeaders(Stream stream, CancellationToken cancellationToken)
		{
			byte[] retBuffer = null;
			int status = 200;
			byte[] buffer = new byte[1024];
			MemoryStream ms = new MemoryStream();
			int num2;
			WebHeaderCollection webHeaderCollection;
			for (;;)
			{
				cancellationToken.ThrowIfCancellationRequested();
				int num = await stream.ReadAsync(buffer, 0, 1024, cancellationToken).ConfigureAwait(false);
				if (num == 0)
				{
					break;
				}
				ms.Write(buffer, 0, num);
				num2 = 0;
				string text = null;
				bool flag = false;
				webHeaderCollection = new WebHeaderCollection();
				while (WebConnection.ReadLine(ms.GetBuffer(), ref num2, (int)ms.Length, ref text))
				{
					if (text == null)
					{
						goto Block_2;
					}
					if (flag)
					{
						webHeaderCollection.Add(text);
					}
					else
					{
						string[] array = text.Split(' ', StringSplitOptions.None);
						if (array.Length < 2)
						{
							goto Block_6;
						}
						if (string.Compare(array[0], "HTTP/1.1", true) == 0)
						{
							this.ProxyVersion = HttpVersion.Version11;
						}
						else
						{
							if (string.Compare(array[0], "HTTP/1.0", true) != 0)
							{
								goto IL_022A;
							}
							this.ProxyVersion = HttpVersion.Version10;
						}
						status = (int)uint.Parse(array[1]);
						if (array.Length >= 3)
						{
							this.StatusDescription = string.Join(" ", array, 2, array.Length - 2);
						}
						flag = true;
					}
				}
			}
			throw WebConnection.GetException(WebExceptionStatus.ServerProtocolViolation, null);
			Block_2:
			string text2 = webHeaderCollection["Content-Length"];
			int num3;
			if (string.IsNullOrEmpty(text2) || !int.TryParse(text2, out num3))
			{
				num3 = 0;
			}
			if (ms.Length - (long)num2 - (long)num3 > 0L)
			{
				retBuffer = new byte[ms.Length - (long)num2 - (long)num3];
				Buffer.BlockCopy(ms.GetBuffer(), num2 + num3, retBuffer, 0, retBuffer.Length);
			}
			else
			{
				this.FlushContents(stream, num3 - (int)(ms.Length - (long)num2));
			}
			return new ValueTuple<WebHeaderCollection, byte[], int>(webHeaderCollection, retBuffer, status);
			Block_6:
			throw WebConnection.GetException(WebExceptionStatus.ServerProtocolViolation, null);
			IL_022A:
			throw WebConnection.GetException(WebExceptionStatus.ServerProtocolViolation, null);
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x00077C7C File Offset: 0x00075E7C
		private void FlushContents(Stream stream, int contentLength)
		{
			while (contentLength > 0)
			{
				byte[] array = new byte[contentLength];
				int num = stream.Read(array, 0, contentLength);
				if (num <= 0)
				{
					break;
				}
				contentLength -= num;
			}
		}

		// Token: 0x0400120A RID: 4618
		private HttpWebRequest connectRequest;

		// Token: 0x0400120B RID: 4619
		private WebConnectionTunnel.NtlmAuthState ntlmAuthState;

		// Token: 0x02000440 RID: 1088
		private enum NtlmAuthState
		{
			// Token: 0x04001215 RID: 4629
			None,
			// Token: 0x04001216 RID: 4630
			Challenge,
			// Token: 0x04001217 RID: 4631
			Response
		}
	}
}
