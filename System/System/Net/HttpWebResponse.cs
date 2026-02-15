using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Net
{
	/// <summary>Provides an HTTP-specific implementation of the <see cref="T:System.Net.WebResponse" /> class.</summary>
	// Token: 0x0200041D RID: 1053
	[Serializable]
	public class HttpWebResponse : WebResponse, ISerializable, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpWebResponse" /> class.</summary>
		// Token: 0x06001A7A RID: 6778 RVA: 0x00072F3A File Offset: 0x0007113A
		public HttpWebResponse()
		{
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x00072F44 File Offset: 0x00071144
		internal HttpWebResponse(Uri uri, string method, HttpStatusCode status, WebHeaderCollection headers)
		{
			this.uri = uri;
			this.method = method;
			this.statusCode = status;
			this.statusDescription = HttpStatusDescription.Get(status);
			this.webHeaders = headers;
			this.version = HttpVersion.Version10;
			this.contentLength = -1L;
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x00072F94 File Offset: 0x00071194
		internal HttpWebResponse(Uri uri, string method, WebResponseStream stream, CookieContainer container)
		{
			this.uri = uri;
			this.method = method;
			this.stream = stream;
			this.webHeaders = stream.Headers ?? new WebHeaderCollection();
			this.version = stream.Version;
			this.statusCode = stream.StatusCode;
			this.statusDescription = stream.StatusDescription ?? HttpStatusDescription.Get(this.statusCode);
			this.contentLength = -1L;
			try
			{
				string text = this.webHeaders["Content-Length"];
				if (string.IsNullOrEmpty(text) || !long.TryParse(text, out this.contentLength))
				{
					this.contentLength = -1L;
				}
			}
			catch (Exception)
			{
				this.contentLength = -1L;
			}
			if (container != null)
			{
				this.cookie_container = container;
				this.FillCookies();
			}
			string text2 = this.webHeaders["Content-Encoding"];
			if (text2 == "gzip" && (stream.Request.AutomaticDecompression & DecompressionMethods.GZip) != DecompressionMethods.None)
			{
				this.stream = new GZipStream(stream, CompressionMode.Decompress);
				this.webHeaders.Remove(HttpRequestHeader.ContentEncoding);
				return;
			}
			if (text2 == "deflate" && (stream.Request.AutomaticDecompression & DecompressionMethods.Deflate) != DecompressionMethods.None)
			{
				this.stream = new DeflateStream(stream, CompressionMode.Decompress);
				this.webHeaders.Remove(HttpRequestHeader.ContentEncoding);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpWebResponse" /> class from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.HttpWebRequest" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.HttpWebRequest" />. </param>
		// Token: 0x06001A7D RID: 6781 RVA: 0x000730E8 File Offset: 0x000712E8
		[Obsolete("Serialization is obsoleted for this type", false)]
		protected HttpWebResponse(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.uri = (Uri)serializationInfo.GetValue("uri", typeof(Uri));
			this.contentLength = serializationInfo.GetInt64("contentLength");
			this.contentType = serializationInfo.GetString("contentType");
			this.method = serializationInfo.GetString("method");
			this.statusDescription = serializationInfo.GetString("statusDescription");
			this.cookieCollection = (CookieCollection)serializationInfo.GetValue("cookieCollection", typeof(CookieCollection));
			this.version = (Version)serializationInfo.GetValue("version", typeof(Version));
			this.statusCode = (HttpStatusCode)serializationInfo.GetValue("statusCode", typeof(HttpStatusCode));
		}

		/// <summary>Gets the headers that are associated with this response from the server.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains the header information returned with the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001A7E RID: 6782 RVA: 0x000731C1 File Offset: 0x000713C1
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.webHeaders;
			}
		}

		/// <summary>Gets the URI of the Internet resource that responded to the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that contains the URI of the Internet resource that responded to the request.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x000731C9 File Offset: 0x000713C9
		public override Uri ResponseUri
		{
			get
			{
				this.CheckDisposed();
				return this.uri;
			}
		}

		/// <summary>Gets the status of the response.</summary>
		/// <returns>One of the <see cref="T:System.Net.HttpStatusCode" /> values.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x000731D7 File Offset: 0x000713D7
		public virtual HttpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		/// <summary>Gets the status description returned with the response.</summary>
		/// <returns>A string that describes the status of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x000731DF File Offset: 0x000713DF
		public virtual string StatusDescription
		{
			get
			{
				this.CheckDisposed();
				return this.statusDescription;
			}
		}

		/// <summary>Gets the stream that is used to read the body of the response from the server.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> containing the body of the response.</returns>
		/// <exception cref="T:System.Net.ProtocolViolationException">There is no response stream. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001A82 RID: 6786 RVA: 0x000731ED File Offset: 0x000713ED
		public override Stream GetResponseStream()
		{
			this.CheckDisposed();
			if (this.stream == null)
			{
				return Stream.Null;
			}
			if (string.Equals(this.method, "HEAD", StringComparison.OrdinalIgnoreCase))
			{
				return Stream.Null;
			}
			return this.stream;
		}

		/// <summary>Serializes this instance into the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</summary>
		/// <param name="serializationInfo">The object into which this <see cref="T:System.Net.HttpWebResponse" /> will be serialized. </param>
		/// <param name="streamingContext">The destination of the serialization. </param>
		// Token: 0x06001A83 RID: 6787 RVA: 0x00066340 File Offset: 0x00064540
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06001A84 RID: 6788 RVA: 0x00073224 File Offset: 0x00071424
		protected override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("uri", this.uri);
			serializationInfo.AddValue("contentLength", this.contentLength);
			serializationInfo.AddValue("contentType", this.contentType);
			serializationInfo.AddValue("method", this.method);
			serializationInfo.AddValue("statusDescription", this.statusDescription);
			serializationInfo.AddValue("cookieCollection", this.cookieCollection);
			serializationInfo.AddValue("version", this.version);
			serializationInfo.AddValue("statusCode", this.statusCode);
		}

		/// <summary>Closes the response stream.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001A85 RID: 6789 RVA: 0x000732C0 File Offset: 0x000714C0
		public override void Close()
		{
			Stream stream = Interlocked.Exchange<Stream>(ref this.stream, null);
			if (stream != null)
			{
				stream.Close();
			}
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x000732E3 File Offset: 0x000714E3
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.HttpWebResponse" />, and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to releases only unmanaged resources. </param>
		// Token: 0x06001A87 RID: 6791 RVA: 0x000732EC File Offset: 0x000714EC
		protected override void Dispose(bool disposing)
		{
			this.disposed = true;
			base.Dispose(true);
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x000732FC File Offset: 0x000714FC
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x00073318 File Offset: 0x00071518
		private void FillCookies()
		{
			if (this.webHeaders == null)
			{
				return;
			}
			CookieCollection cookieCollection = null;
			try
			{
				string text = this.webHeaders.Get("Set-Cookie");
				if (text != null)
				{
					cookieCollection = this.cookie_container.CookieCutter(this.uri, "Set-Cookie", text, false);
				}
			}
			catch
			{
			}
			try
			{
				string text = this.webHeaders.Get("Set-Cookie2");
				if (text != null)
				{
					CookieCollection cookieCollection2 = this.cookie_container.CookieCutter(this.uri, "Set-Cookie2", text, false);
					if (cookieCollection != null && cookieCollection.Count != 0)
					{
						cookieCollection.Add(cookieCollection2);
					}
					else
					{
						cookieCollection = cookieCollection2;
					}
				}
			}
			catch
			{
			}
			this.cookieCollection = cookieCollection;
		}

		// Token: 0x04001127 RID: 4391
		private Uri uri;

		// Token: 0x04001128 RID: 4392
		private WebHeaderCollection webHeaders;

		// Token: 0x04001129 RID: 4393
		private CookieCollection cookieCollection;

		// Token: 0x0400112A RID: 4394
		private string method;

		// Token: 0x0400112B RID: 4395
		private Version version;

		// Token: 0x0400112C RID: 4396
		private HttpStatusCode statusCode;

		// Token: 0x0400112D RID: 4397
		private string statusDescription;

		// Token: 0x0400112E RID: 4398
		private long contentLength;

		// Token: 0x0400112F RID: 4399
		private string contentType;

		// Token: 0x04001130 RID: 4400
		private CookieContainer cookie_container;

		// Token: 0x04001131 RID: 4401
		private bool disposed;

		// Token: 0x04001132 RID: 4402
		private Stream stream;
	}
}
