using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Net
{
	/// <summary>Represents a response to a request being handled by an <see cref="T:System.Net.HttpListener" /> object.</summary>
	// Token: 0x02000412 RID: 1042
	public sealed class HttpListenerResponse : IDisposable
	{
		// Token: 0x06001A01 RID: 6657 RVA: 0x000701B0 File Offset: 0x0006E3B0
		internal HttpListenerResponse(HttpListenerContext context)
		{
			this.context = context;
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x00070208 File Offset: 0x0006E408
		internal bool ForceCloseChunked
		{
			get
			{
				return this.force_close_chunked;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Text.Encoding" /> for this response's <see cref="P:System.Net.HttpListenerResponse.OutputStream" />.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object suitable for use with the data in the <see cref="P:System.Net.HttpListenerResponse.OutputStream" /> property, or null if no encoding is specified.</returns>
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x00070210 File Offset: 0x0006E410
		public Encoding ContentEncoding
		{
			get
			{
				if (this.content_encoding == null)
				{
					this.content_encoding = Encoding.Default;
				}
				return this.content_encoding;
			}
		}

		/// <summary>Gets or sets the number of bytes in the body data included in the response.</summary>
		/// <returns>The value of the response's Content-Length header.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The response is already being sent.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x1700059F RID: 1439
		// (set) Token: 0x06001A04 RID: 6660 RVA: 0x0007022C File Offset: 0x0006E42C
		public long ContentLength64
		{
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("Must be >= 0", "value");
				}
				this.cl_set = true;
				this.content_length = value;
			}
		}

		/// <summary>Gets or sets the MIME type of the content returned.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the text of the response's Content-Type header.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is an empty string ("").</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170005A0 RID: 1440
		// (set) Token: 0x06001A05 RID: 6661 RVA: 0x00070288 File Offset: 0x0006E488
		public string ContentType
		{
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				this.content_type = value;
			}
		}

		/// <summary>Gets or sets the collection of header name/value pairs returned by the server.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> instance that contains all the explicitly set HTTP headers to be included in the response.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.WebHeaderCollection" /> instance specified for a set operation is not valid for a response.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x000702BD File Offset: 0x0006E4BD
		public WebHeaderCollection Headers
		{
			get
			{
				return this.headers;
			}
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object to which a response can be written.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object to which a response can be written.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x000702C5 File Offset: 0x0006E4C5
		public Stream OutputStream
		{
			get
			{
				if (this.output_stream == null)
				{
					this.output_stream = this.context.Connection.GetResponseStream();
				}
				return this.output_stream;
			}
		}

		/// <summary>Gets or sets whether the response uses chunked transfer encoding.</summary>
		/// <returns>true if the response is set to use chunked transfer encoding; otherwise, false. The default is false.</returns>
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x000702EB File Offset: 0x0006E4EB
		// (set) Token: 0x06001A09 RID: 6665 RVA: 0x000702F3 File Offset: 0x0006E4F3
		public bool SendChunked
		{
			get
			{
				return this.chunked;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				this.chunked = value;
			}
		}

		/// <summary>Gets or sets the HTTP status code to be returned to the client.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that specifies the HTTP status code for the requested resource. The default is <see cref="F:System.Net.HttpStatusCode.OK" />, indicating that the server successfully processed the client's request and included the requested resource in the response body.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">The value specified for a set operation is not valid. Valid values are between 100 and 999 inclusive.</exception>
		// Token: 0x170005A4 RID: 1444
		// (set) Token: 0x06001A0A RID: 6666 RVA: 0x00070328 File Offset: 0x0006E528
		public int StatusCode
		{
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				if (value < 100 || value > 999)
				{
					throw new ProtocolViolationException("StatusCode must be between 100 and 999.");
				}
				this.status_code = value;
				this.status_description = HttpStatusDescription.Get(value);
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.HttpListenerResponse" />.</summary>
		// Token: 0x06001A0B RID: 6667 RVA: 0x0007038C File Offset: 0x0006E58C
		void IDisposable.Dispose()
		{
			this.Close(true);
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00070395 File Offset: 0x0006E595
		private void Close(bool force)
		{
			this.disposed = true;
			this.context.Connection.Close(force);
		}

		/// <summary>Sends the response to the client and releases the resources held by this <see cref="T:System.Net.HttpListenerResponse" /> instance.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001A0D RID: 6669 RVA: 0x000703AF File Offset: 0x0006E5AF
		public void Close()
		{
			if (this.disposed)
			{
				return;
			}
			this.Close(false);
		}

		/// <summary>Returns the specified byte array to the client and releases the resources held by this <see cref="T:System.Net.HttpListenerResponse" /> instance.</summary>
		/// <param name="responseEntity">A <see cref="T:System.Byte" /> array that contains the response to send to the client.</param>
		/// <param name="willBlock">true to block execution while flushing the stream to the client; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="responseEntity" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001A0E RID: 6670 RVA: 0x000703C1 File Offset: 0x0006E5C1
		public void Close(byte[] responseEntity, bool willBlock)
		{
			if (this.disposed)
			{
				return;
			}
			if (responseEntity == null)
			{
				throw new ArgumentNullException("responseEntity");
			}
			this.ContentLength64 = (long)responseEntity.Length;
			this.OutputStream.Write(responseEntity, 0, (int)this.content_length);
			this.Close(false);
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x00070400 File Offset: 0x0006E600
		internal void SendHeaders(bool closing, MemoryStream ms)
		{
			Encoding @default = this.content_encoding;
			if (@default == null)
			{
				@default = Encoding.Default;
			}
			if (this.content_type != null)
			{
				if (this.content_encoding != null && this.content_type.IndexOf("charset=", StringComparison.Ordinal) == -1)
				{
					string webName = this.content_encoding.WebName;
					this.headers.SetInternal("Content-Type", this.content_type + "; charset=" + webName);
				}
				else
				{
					this.headers.SetInternal("Content-Type", this.content_type);
				}
			}
			if (this.headers["Server"] == null)
			{
				this.headers.SetInternal("Server", "Mono-HTTPAPI/1.0");
			}
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			if (this.headers["Date"] == null)
			{
				this.headers.SetInternal("Date", DateTime.UtcNow.ToString("r", invariantCulture));
			}
			if (!this.chunked)
			{
				if (!this.cl_set && closing)
				{
					this.cl_set = true;
					this.content_length = 0L;
				}
				if (this.cl_set)
				{
					this.headers.SetInternal("Content-Length", this.content_length.ToString(invariantCulture));
				}
			}
			Version protocolVersion = this.context.Request.ProtocolVersion;
			if (!this.cl_set && !this.chunked && protocolVersion >= HttpVersion.Version11)
			{
				this.chunked = true;
			}
			bool flag = this.status_code == 400 || this.status_code == 408 || this.status_code == 411 || this.status_code == 413 || this.status_code == 414 || this.status_code == 500 || this.status_code == 503;
			if (!flag)
			{
				flag = !this.context.Request.KeepAlive;
			}
			if (!this.keep_alive || flag)
			{
				this.headers.SetInternal("Connection", "close");
				flag = true;
			}
			if (this.chunked)
			{
				this.headers.SetInternal("Transfer-Encoding", "chunked");
			}
			int reuses = this.context.Connection.Reuses;
			if (reuses >= 100)
			{
				this.force_close_chunked = true;
				if (!flag)
				{
					this.headers.SetInternal("Connection", "close");
					flag = true;
				}
			}
			if (!flag)
			{
				this.headers.SetInternal("Keep-Alive", string.Format("timeout=15,max={0}", 100 - reuses));
				if (this.context.Request.ProtocolVersion <= HttpVersion.Version10)
				{
					this.headers.SetInternal("Connection", "keep-alive");
				}
			}
			if (this.location != null)
			{
				this.headers.SetInternal("Location", this.location);
			}
			if (this.cookies != null)
			{
				foreach (object obj in this.cookies)
				{
					Cookie cookie = (Cookie)obj;
					this.headers.SetInternal("Set-Cookie", HttpListenerResponse.CookieToClientString(cookie));
				}
			}
			StreamWriter streamWriter = new StreamWriter(ms, @default, 256);
			streamWriter.Write("HTTP/{0} {1} {2}\r\n", this.version, this.status_code, this.status_description);
			string text = HttpListenerResponse.FormatHeaders(this.headers);
			streamWriter.Write(text);
			streamWriter.Flush();
			int num = @default.GetPreamble().Length;
			if (this.output_stream == null)
			{
				this.output_stream = this.context.Connection.GetResponseStream();
			}
			ms.Position = (long)num;
			this.HeadersSent = true;
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x000707BC File Offset: 0x0006E9BC
		private static string FormatHeaders(WebHeaderCollection headers)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < headers.Count; i++)
			{
				string key = headers.GetKey(i);
				if (WebHeaderCollection.AllowMultiValues(key))
				{
					foreach (string text in headers.GetValues(i))
					{
						stringBuilder.Append(key).Append(": ").Append(text)
							.Append("\r\n");
					}
				}
				else
				{
					stringBuilder.Append(key).Append(": ").Append(headers.Get(i))
						.Append("\r\n");
				}
			}
			return stringBuilder.Append("\r\n").ToString();
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x00070874 File Offset: 0x0006EA74
		private static string CookieToClientString(Cookie cookie)
		{
			if (cookie.Name.Length == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(64);
			if (cookie.Version > 0)
			{
				stringBuilder.Append("Version=").Append(cookie.Version).Append(";");
			}
			stringBuilder.Append(cookie.Name).Append("=").Append(cookie.Value);
			if (cookie.Path != null && cookie.Path.Length != 0)
			{
				stringBuilder.Append(";Path=").Append(HttpListenerResponse.QuotedString(cookie, cookie.Path));
			}
			if (cookie.Domain != null && cookie.Domain.Length != 0)
			{
				stringBuilder.Append(";Domain=").Append(HttpListenerResponse.QuotedString(cookie, cookie.Domain));
			}
			if (cookie.Port != null && cookie.Port.Length != 0)
			{
				stringBuilder.Append(";Port=").Append(cookie.Port);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x0007097E File Offset: 0x0006EB7E
		private static string QuotedString(Cookie cookie, string value)
		{
			if (cookie.Version == 0 || HttpListenerResponse.IsToken(value))
			{
				return value;
			}
			return "\"" + value.Replace("\"", "\\\"") + "\"";
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x000709B4 File Offset: 0x0006EBB4
		private static bool IsToken(string value)
		{
			int length = value.Length;
			for (int i = 0; i < length; i++)
			{
				char c = value[i];
				if (c < ' ' || c >= '\u007f' || HttpListenerResponse.tspecials.IndexOf(c) != -1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400109E RID: 4254
		private bool disposed;

		// Token: 0x0400109F RID: 4255
		private Encoding content_encoding;

		// Token: 0x040010A0 RID: 4256
		private long content_length;

		// Token: 0x040010A1 RID: 4257
		private bool cl_set;

		// Token: 0x040010A2 RID: 4258
		private string content_type;

		// Token: 0x040010A3 RID: 4259
		private CookieCollection cookies;

		// Token: 0x040010A4 RID: 4260
		private WebHeaderCollection headers = new WebHeaderCollection();

		// Token: 0x040010A5 RID: 4261
		private bool keep_alive = true;

		// Token: 0x040010A6 RID: 4262
		private ResponseStream output_stream;

		// Token: 0x040010A7 RID: 4263
		private Version version = HttpVersion.Version11;

		// Token: 0x040010A8 RID: 4264
		private string location;

		// Token: 0x040010A9 RID: 4265
		private int status_code = 200;

		// Token: 0x040010AA RID: 4266
		private string status_description = "OK";

		// Token: 0x040010AB RID: 4267
		private bool chunked;

		// Token: 0x040010AC RID: 4268
		private HttpListenerContext context;

		// Token: 0x040010AD RID: 4269
		internal bool HeadersSent;

		// Token: 0x040010AE RID: 4270
		internal object headers_lock = new object();

		// Token: 0x040010AF RID: 4271
		private bool force_close_chunked;

		// Token: 0x040010B0 RID: 4272
		private static string tspecials = "()<>@,;:\\\"/[]?={} \t";
	}
}
