using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Net
{
	/// <summary>Describes an incoming HTTP request to an <see cref="T:System.Net.HttpListener" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000411 RID: 1041
	public sealed class HttpListenerRequest
	{
		// Token: 0x060019EC RID: 6636 RVA: 0x0006F63E File Offset: 0x0006D83E
		internal HttpListenerRequest(HttpListenerContext context)
		{
			this.context = context;
			this.headers = new WebHeaderCollection();
			this.version = HttpVersion.Version10;
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x0006F664 File Offset: 0x0006D864
		internal void SetRequestLine(string req)
		{
			string[] array = req.Split(HttpListenerRequest.separators, 3);
			if (array.Length != 3)
			{
				this.context.ErrorMessage = "Invalid request line (parts).";
				return;
			}
			this.method = array[0];
			foreach (char c in this.method)
			{
				int num = (int)c;
				if ((num < 65 || num > 90) && (num <= 32 || c >= '\u007f' || c == '(' || c == ')' || c == '<' || c == '<' || c == '>' || c == '@' || c == ',' || c == ';' || c == ':' || c == '\\' || c == '"' || c == '/' || c == '[' || c == ']' || c == '?' || c == '=' || c == '{' || c == '}'))
				{
					this.context.ErrorMessage = "(Invalid verb)";
					return;
				}
			}
			this.raw_url = array[1];
			if (array[2].Length != 8 || !array[2].StartsWith("HTTP/"))
			{
				this.context.ErrorMessage = "Invalid request line (version).";
				return;
			}
			try
			{
				this.version = new Version(array[2].Substring(5));
				if (this.version.Major < 1)
				{
					throw new Exception();
				}
			}
			catch
			{
				this.context.ErrorMessage = "Invalid request line (version).";
			}
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x0006F7CC File Offset: 0x0006D9CC
		private void CreateQueryString(string query)
		{
			if (query == null || query.Length == 0)
			{
				this.query_string = new NameValueCollection(1);
				return;
			}
			this.query_string = new NameValueCollection();
			if (query[0] == '?')
			{
				query = query.Substring(1);
			}
			foreach (string text in query.Split('&', StringSplitOptions.None))
			{
				int num = text.IndexOf('=');
				if (num == -1)
				{
					this.query_string.Add(null, WebUtility.UrlDecode(text));
				}
				else
				{
					string text2 = WebUtility.UrlDecode(text.Substring(0, num));
					string text3 = WebUtility.UrlDecode(text.Substring(num + 1));
					this.query_string.Add(text2, text3);
				}
			}
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x0006F87C File Offset: 0x0006DA7C
		private static bool MaybeUri(string s)
		{
			int num = s.IndexOf(':');
			return num != -1 && num < 10 && HttpListenerRequest.IsPredefinedScheme(s.Substring(0, num));
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x0006F8AC File Offset: 0x0006DAAC
		private static bool IsPredefinedScheme(string scheme)
		{
			if (scheme == null || scheme.Length < 3)
			{
				return false;
			}
			char c = scheme[0];
			if (c == 'h')
			{
				return scheme == "http" || scheme == "https";
			}
			if (c == 'f')
			{
				return scheme == "file" || scheme == "ftp";
			}
			if (c != 'n')
			{
				return (c == 'g' && scheme == "gopher") || (c == 'm' && scheme == "mailto");
			}
			c = scheme[1];
			if (c == 'e')
			{
				return scheme == "news" || scheme == "net.pipe" || scheme == "net.tcp";
			}
			return scheme == "nntp";
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x0006F984 File Offset: 0x0006DB84
		internal bool FinishInitialization()
		{
			string text = this.UserHostName;
			if (this.version > HttpVersion.Version10 && (text == null || text.Length == 0))
			{
				this.context.ErrorMessage = "Invalid host name";
				return true;
			}
			Uri uri = null;
			string pathAndQuery;
			if (HttpListenerRequest.MaybeUri(this.raw_url.ToLowerInvariant()) && Uri.TryCreate(this.raw_url, UriKind.Absolute, out uri))
			{
				pathAndQuery = uri.PathAndQuery;
			}
			else
			{
				pathAndQuery = this.raw_url;
			}
			if (text == null || text.Length == 0)
			{
				text = this.UserHostAddress;
			}
			if (uri != null)
			{
				text = uri.Host;
			}
			int num = text.IndexOf(':');
			if (num >= 0)
			{
				text = text.Substring(0, num);
			}
			string text2 = string.Format("{0}://{1}:{2}", this.IsSecureConnection ? "https" : "http", text, this.LocalEndPoint.Port);
			if (!Uri.TryCreate(text2 + pathAndQuery, UriKind.Absolute, out this.url))
			{
				this.context.ErrorMessage = WebUtility.HtmlEncode("Invalid url: " + text2 + pathAndQuery);
				return true;
			}
			this.CreateQueryString(this.url.Query);
			this.url = HttpListenerRequestUriBuilder.GetRequestUri(this.raw_url, this.url.Scheme, this.url.Authority, this.url.LocalPath, this.url.Query);
			if (this.version >= HttpVersion.Version11)
			{
				string text3 = this.Headers["Transfer-Encoding"];
				this.is_chunked = text3 != null && string.Compare(text3, "chunked", StringComparison.OrdinalIgnoreCase) == 0;
				if (text3 != null && !this.is_chunked)
				{
					this.context.Connection.SendError(null, 501);
					return false;
				}
			}
			if (!this.is_chunked && !this.cl_set && (string.Compare(this.method, "POST", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(this.method, "PUT", StringComparison.OrdinalIgnoreCase) == 0))
			{
				this.context.Connection.SendError(null, 411);
				return false;
			}
			if (string.Compare(this.Headers["Expect"], "100-continue", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.context.Connection.GetResponseStream().InternalWrite(HttpListenerRequest._100continue, 0, HttpListenerRequest._100continue.Length);
			}
			return true;
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x0006FBDC File Offset: 0x0006DDDC
		internal static string Unquote(string str)
		{
			int num = str.IndexOf('"');
			int num2 = str.LastIndexOf('"');
			if (num >= 0 && num2 >= 0)
			{
				str = str.Substring(num + 1, num2 - 1);
			}
			return str.Trim();
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x0006FC18 File Offset: 0x0006DE18
		internal void AddHeader(string header)
		{
			int num = header.IndexOf(':');
			if (num == -1 || num == 0)
			{
				this.context.ErrorMessage = "Bad Request";
				this.context.ErrorStatus = 400;
				return;
			}
			string text = header.Substring(0, num).Trim();
			string text2 = header.Substring(num + 1).Trim();
			string text3 = text.ToLower(CultureInfo.InvariantCulture);
			this.headers.SetInternal(text, text2);
			if (text3 == "accept-language")
			{
				this.user_languages = text2.Split(',', StringSplitOptions.None);
				return;
			}
			if (!(text3 == "accept"))
			{
				if (!(text3 == "content-length"))
				{
					if (!(text3 == "referer"))
					{
						if (!(text3 == "cookie"))
						{
							return;
						}
						goto IL_0142;
					}
				}
				else
				{
					try
					{
						this.content_length = long.Parse(text2.Trim());
						if (this.content_length < 0L)
						{
							this.context.ErrorMessage = "Invalid Content-Length.";
						}
						this.cl_set = true;
						return;
					}
					catch
					{
						this.context.ErrorMessage = "Invalid Content-Length.";
						return;
					}
				}
				try
				{
					this.referrer = new Uri(text2);
					return;
				}
				catch
				{
					this.referrer = new Uri("http://someone.is.screwing.with.the.headers.com/");
					return;
				}
				IL_0142:
				if (this.cookies == null)
				{
					this.cookies = new CookieCollection();
				}
				string[] array = text2.Split(new char[] { ',', ';' });
				Cookie cookie = null;
				int num2 = 0;
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text4 = array2[i].Trim();
					if (text4.Length != 0)
					{
						if (text4.StartsWith("$Version"))
						{
							num2 = int.Parse(HttpListenerRequest.Unquote(text4.Substring(text4.IndexOf('=') + 1)));
						}
						else if (text4.StartsWith("$Path"))
						{
							if (cookie != null)
							{
								cookie.Path = text4.Substring(text4.IndexOf('=') + 1).Trim();
							}
						}
						else if (text4.StartsWith("$Domain"))
						{
							if (cookie != null)
							{
								cookie.Domain = text4.Substring(text4.IndexOf('=') + 1).Trim();
							}
						}
						else if (text4.StartsWith("$Port"))
						{
							if (cookie != null)
							{
								cookie.Port = text4.Substring(text4.IndexOf('=') + 1).Trim();
							}
						}
						else
						{
							if (cookie != null)
							{
								this.cookies.Add(cookie);
							}
							try
							{
								cookie = new Cookie();
								int num3 = text4.IndexOf('=');
								if (num3 > 0)
								{
									cookie.Name = text4.Substring(0, num3).Trim();
									cookie.Value = text4.Substring(num3 + 1).Trim();
								}
								else
								{
									cookie.Name = text4.Trim();
									cookie.Value = string.Empty;
								}
								cookie.Version = num2;
							}
							catch (CookieException)
							{
								cookie = null;
							}
						}
					}
				}
				if (cookie != null)
				{
					this.cookies.Add(cookie);
				}
				return;
			}
			this.accept_types = text2.Split(',', StringSplitOptions.None);
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x0006FF60 File Offset: 0x0006E160
		internal bool FlushInput()
		{
			if (!this.HasEntityBody)
			{
				return true;
			}
			int num = 2048;
			if (this.content_length > 0L)
			{
				num = (int)Math.Min(this.content_length, (long)num);
			}
			byte[] array = new byte[num];
			bool flag;
			for (;;)
			{
				try
				{
					IAsyncResult asyncResult = this.InputStream.BeginRead(array, 0, num, null, null);
					if (!asyncResult.IsCompleted && !asyncResult.AsyncWaitHandle.WaitOne(1000))
					{
						flag = false;
					}
					else
					{
						if (this.InputStream.EndRead(asyncResult) > 0)
						{
							continue;
						}
						flag = true;
					}
				}
				catch (ObjectDisposedException)
				{
					this.input_stream = null;
					flag = true;
				}
				catch
				{
					flag = false;
				}
				break;
			}
			return flag;
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the request has associated body data.</summary>
		/// <returns>true if the request has associated body data; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x00070010 File Offset: 0x0006E210
		public bool HasEntityBody
		{
			get
			{
				return this.content_length > 0L || this.is_chunked;
			}
		}

		/// <summary>Gets the collection of header name/value pairs sent in the request.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains the HTTP headers included in the request.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060019F6 RID: 6646 RVA: 0x00070024 File Offset: 0x0006E224
		public NameValueCollection Headers
		{
			get
			{
				return this.headers;
			}
		}

		/// <summary>Gets a stream that contains the body data sent by the client.</summary>
		/// <returns>A readable <see cref="T:System.IO.Stream" /> object that contains the bytes sent by the client in the body of the request. This property returns <see cref="F:System.IO.Stream.Null" /> if no data is sent with the request.</returns>
		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060019F7 RID: 6647 RVA: 0x0007002C File Offset: 0x0006E22C
		public Stream InputStream
		{
			get
			{
				if (this.input_stream == null)
				{
					if (this.is_chunked || this.content_length > 0L)
					{
						this.input_stream = this.context.Connection.GetRequestStream(this.is_chunked, this.content_length);
					}
					else
					{
						this.input_stream = Stream.Null;
					}
				}
				return this.input_stream;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the TCP connection used to send the request is using the Secure Sockets Layer (SSL) protocol.</summary>
		/// <returns>true if the TCP connection is using SSL; otherwise, false.</returns>
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x00070088 File Offset: 0x0006E288
		public bool IsSecureConnection
		{
			get
			{
				return this.context.Connection.IsSecure;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the client requests a persistent connection.</summary>
		/// <returns>true if the connection should be kept open; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x0007009C File Offset: 0x0006E29C
		public bool KeepAlive
		{
			get
			{
				if (this.ka_set)
				{
					return this.keep_alive;
				}
				this.ka_set = true;
				string text = this.headers["Connection"];
				if (!string.IsNullOrEmpty(text))
				{
					this.keep_alive = string.Compare(text, "keep-alive", StringComparison.OrdinalIgnoreCase) == 0;
				}
				else if (this.version == HttpVersion.Version11)
				{
					this.keep_alive = true;
				}
				else
				{
					text = this.headers["keep-alive"];
					if (!string.IsNullOrEmpty(text))
					{
						this.keep_alive = string.Compare(text, "closed", StringComparison.OrdinalIgnoreCase) != 0;
					}
				}
				return this.keep_alive;
			}
		}

		/// <summary>Get the server IP address and port number to which the request is directed.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> that represents the IP address that the request is sent to.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x0007013E File Offset: 0x0006E33E
		public IPEndPoint LocalEndPoint
		{
			get
			{
				return this.context.Connection.LocalEndPoint;
			}
		}

		/// <summary>Gets the HTTP version used by the requesting client.</summary>
		/// <returns>A <see cref="T:System.Version" /> that identifies the client's version of HTTP.</returns>
		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x00070150 File Offset: 0x0006E350
		public Version ProtocolVersion
		{
			get
			{
				return this.version;
			}
		}

		/// <summary>Gets the URL information (without the host and port) requested by the client.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the raw URL for this request.</returns>
		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x00070158 File Offset: 0x0006E358
		public string RawUrl
		{
			get
			{
				return this.raw_url;
			}
		}

		/// <summary>Gets the <see cref="T:System.Uri" /> object requested by the client.</summary>
		/// <returns>A <see cref="T:System.Uri" /> object that identifies the resource requested by the client.</returns>
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x00070160 File Offset: 0x0006E360
		public Uri Url
		{
			get
			{
				return this.url;
			}
		}

		/// <summary>Gets the server IP address and port number to which the request is directed.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the host address information.</returns>
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x00070168 File Offset: 0x0006E368
		public string UserHostAddress
		{
			get
			{
				return this.LocalEndPoint.ToString();
			}
		}

		/// <summary>Gets the DNS name and, if provided, the port number specified by the client.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the text of the request's Host header.</returns>
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x00070175 File Offset: 0x0006E375
		public string UserHostName
		{
			get
			{
				return this.headers["host"];
			}
		}

		// Token: 0x0400108B RID: 4235
		private string[] accept_types;

		// Token: 0x0400108C RID: 4236
		private long content_length;

		// Token: 0x0400108D RID: 4237
		private bool cl_set;

		// Token: 0x0400108E RID: 4238
		private CookieCollection cookies;

		// Token: 0x0400108F RID: 4239
		private WebHeaderCollection headers;

		// Token: 0x04001090 RID: 4240
		private string method;

		// Token: 0x04001091 RID: 4241
		private Stream input_stream;

		// Token: 0x04001092 RID: 4242
		private Version version;

		// Token: 0x04001093 RID: 4243
		private NameValueCollection query_string;

		// Token: 0x04001094 RID: 4244
		private string raw_url;

		// Token: 0x04001095 RID: 4245
		private Uri url;

		// Token: 0x04001096 RID: 4246
		private Uri referrer;

		// Token: 0x04001097 RID: 4247
		private string[] user_languages;

		// Token: 0x04001098 RID: 4248
		private HttpListenerContext context;

		// Token: 0x04001099 RID: 4249
		private bool is_chunked;

		// Token: 0x0400109A RID: 4250
		private bool ka_set;

		// Token: 0x0400109B RID: 4251
		private bool keep_alive;

		// Token: 0x0400109C RID: 4252
		private static byte[] _100continue = Encoding.ASCII.GetBytes("HTTP/1.1 100 Continue\r\n\r\n");

		// Token: 0x0400109D RID: 4253
		private static char[] separators = new char[] { ' ' };
	}
}
