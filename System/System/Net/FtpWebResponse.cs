using System;
using System.IO;

namespace System.Net
{
	/// <summary>Encapsulates a File Transfer Protocol (FTP) server's response to a request.</summary>
	// Token: 0x0200039B RID: 923
	public class FtpWebResponse : WebResponse, IDisposable
	{
		// Token: 0x0600172D RID: 5933 RVA: 0x00063B0C File Offset: 0x00061D0C
		internal FtpWebResponse(Stream responseStream, long contentLength, Uri responseUri, FtpStatusCode statusCode, string statusLine, DateTime lastModified, string bannerMessage, string welcomeMessage, string exitMessage)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, contentLength, statusLine);
			}
			this._responseStream = responseStream;
			if (responseStream == null && contentLength < 0L)
			{
				contentLength = 0L;
			}
			this._contentLength = contentLength;
			this._responseUri = responseUri;
			this._statusCode = statusCode;
			this._statusLine = statusLine;
			this._lastModified = lastModified;
			this._bannerMessage = bannerMessage;
			this._welcomeMessage = welcomeMessage;
			this._exitMessage = exitMessage;
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x00063B85 File Offset: 0x00061D85
		internal void UpdateStatus(FtpStatusCode statusCode, string statusLine, string exitMessage)
		{
			this._statusCode = statusCode;
			this._statusLine = statusLine;
			this._exitMessage = exitMessage;
		}

		/// <summary>Retrieves the stream that contains response data sent from an FTP server.</summary>
		/// <returns>A readable <see cref="T:System.IO.Stream" /> instance that contains data returned with the response; otherwise, <see cref="F:System.IO.Stream.Null" /> if no response data was returned by the server.</returns>
		/// <exception cref="T:System.InvalidOperationException">The response did not return a data stream. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600172F RID: 5935 RVA: 0x00063B9C File Offset: 0x00061D9C
		public override Stream GetResponseStream()
		{
			Stream stream;
			if (this._responseStream != null)
			{
				stream = this._responseStream;
			}
			else
			{
				stream = (this._responseStream = new FtpWebResponse.EmptyStream());
			}
			return stream;
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x00063BCC File Offset: 0x00061DCC
		internal void SetResponseStream(Stream stream)
		{
			if (stream == null || stream == Stream.Null || stream is FtpWebResponse.EmptyStream)
			{
				return;
			}
			this._responseStream = stream;
		}

		/// <summary>Frees the resources held by the response.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001731 RID: 5937 RVA: 0x00063BE9 File Offset: 0x00061DE9
		public override void Close()
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, null, "Close");
			}
			Stream responseStream = this._responseStream;
			if (responseStream != null)
			{
				responseStream.Close();
			}
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Exit(this, null, "Close");
			}
		}

		/// <summary>Gets an empty <see cref="T:System.Net.WebHeaderCollection" /> object.</summary>
		/// <returns>An empty <see cref="T:System.Net.WebHeaderCollection" /> object.</returns>
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x00063C24 File Offset: 0x00061E24
		public override WebHeaderCollection Headers
		{
			get
			{
				if (this._ftpRequestHeaders == null)
				{
					lock (this)
					{
						if (this._ftpRequestHeaders == null)
						{
							this._ftpRequestHeaders = new WebHeaderCollection();
						}
					}
				}
				return this._ftpRequestHeaders;
			}
		}

		/// <summary>Gets the URI that sent the response to the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> instance that identifies the resource associated with this response.</returns>
		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x00063C7C File Offset: 0x00061E7C
		public override Uri ResponseUri
		{
			get
			{
				return this._responseUri;
			}
		}

		/// <summary>Gets the most recent status code sent from the FTP server.</summary>
		/// <returns>An <see cref="T:System.Net.FtpStatusCode" /> value that indicates the most recent status code returned with this response.</returns>
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x00063C84 File Offset: 0x00061E84
		public FtpStatusCode StatusCode
		{
			get
			{
				return this._statusCode;
			}
		}

		// Token: 0x04000E46 RID: 3654
		internal Stream _responseStream;

		// Token: 0x04000E47 RID: 3655
		private long _contentLength;

		// Token: 0x04000E48 RID: 3656
		private Uri _responseUri;

		// Token: 0x04000E49 RID: 3657
		private FtpStatusCode _statusCode;

		// Token: 0x04000E4A RID: 3658
		private string _statusLine;

		// Token: 0x04000E4B RID: 3659
		private WebHeaderCollection _ftpRequestHeaders;

		// Token: 0x04000E4C RID: 3660
		private DateTime _lastModified;

		// Token: 0x04000E4D RID: 3661
		private string _bannerMessage;

		// Token: 0x04000E4E RID: 3662
		private string _welcomeMessage;

		// Token: 0x04000E4F RID: 3663
		private string _exitMessage;

		// Token: 0x0200039C RID: 924
		internal sealed class EmptyStream : MemoryStream
		{
			// Token: 0x06001735 RID: 5941 RVA: 0x00063C8C File Offset: 0x00061E8C
			internal EmptyStream()
				: base(Array.Empty<byte>(), false)
			{
			}
		}
	}
}
