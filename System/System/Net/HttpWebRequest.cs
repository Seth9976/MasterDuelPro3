using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Net.Cache;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mono.Net.Security;
using Mono.Security.Interface;
using Unity;

namespace System.Net
{
	/// <summary>Provides an HTTP-specific implementation of the <see cref="T:System.Net.WebRequest" /> class.</summary>
	// Token: 0x02000415 RID: 1045
	[Serializable]
	public class HttpWebRequest : WebRequest, ISerializable
	{
		// Token: 0x06001A1E RID: 6686 RVA: 0x00070B60 File Offset: 0x0006ED60
		static HttpWebRequest()
		{
			NetConfig netConfig = ConfigurationSettings.GetConfig("system.net/settings") as NetConfig;
			if (netConfig != null)
			{
				HttpWebRequest.defaultMaxResponseHeadersLength = netConfig.MaxResponseHeadersLength;
			}
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x00070BA4 File Offset: 0x0006EDA4
		internal HttpWebRequest(Uri uri)
		{
			this.allowAutoRedirect = true;
			this.allowBuffering = true;
			this.contentLength = -1L;
			this.keepAlive = true;
			this.maxAutoRedirect = 50;
			this.mediaType = string.Empty;
			this.method = "GET";
			this.initialMethod = "GET";
			this.pipelined = true;
			this.version = HttpVersion.Version11;
			this.timeout = 100000;
			this.continueTimeout = 350;
			this.locker = new object();
			this.readWriteTimeout = 300000;
			base..ctor();
			this.requestUri = uri;
			this.actualUri = uri;
			this.proxy = WebRequest.InternalDefaultWebProxy;
			this.webHeaders = new WebHeaderCollection(WebHeaderCollectionType.HttpWebRequest);
			this.ThrowOnError = true;
			this.ResetAuthorization();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpWebRequest" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the new <see cref="T:System.Net.HttpWebRequest" /> object. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the new <see cref="T:System.Net.HttpWebRequest" /> object. </param>
		// Token: 0x06001A20 RID: 6688 RVA: 0x00070C70 File Offset: 0x0006EE70
		[Obsolete("Serialization is obsoleted for this type.  http://go.microsoft.com/fwlink/?linkid=14202")]
		protected HttpWebRequest(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.allowAutoRedirect = true;
			this.allowBuffering = true;
			this.contentLength = -1L;
			this.keepAlive = true;
			this.maxAutoRedirect = 50;
			this.mediaType = string.Empty;
			this.method = "GET";
			this.initialMethod = "GET";
			this.pipelined = true;
			this.version = HttpVersion.Version11;
			this.timeout = 100000;
			this.continueTimeout = 350;
			this.locker = new object();
			this.readWriteTimeout = 300000;
			base..ctor();
			throw new SerializationException();
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x00070D0C File Offset: 0x0006EF0C
		private void ResetAuthorization()
		{
			this.auth_state = new HttpWebRequest.AuthorizationState(this, false);
			this.proxy_auth_state = new HttpWebRequest.AuthorizationState(this, true);
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the Internet resource that actually responds to the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that identifies the Internet resource that actually responds to the request. The default is the URI used by the <see cref="M:System.Net.WebRequest.Create(System.String)" /> method to initialize the request.</returns>
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001A22 RID: 6690 RVA: 0x00070D28 File Offset: 0x0006EF28
		public Uri Address
		{
			get
			{
				return this.actualUri;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to buffer the data sent to the Internet resource.</summary>
		/// <returns>true to enable buffering of the data sent to the Internet resource; false to disable buffering. The default is true.</returns>
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001A23 RID: 6691 RVA: 0x00070D30 File Offset: 0x0006EF30
		public virtual bool AllowWriteStreamBuffering
		{
			get
			{
				return this.allowBuffering;
			}
		}

		/// <summary>Gets or sets the type of decompression that is used.</summary>
		/// <returns>A T:System.Net.DecompressionMethods object that indicates the type of decompression that is used. </returns>
		/// <exception cref="T:System.InvalidOperationException">The object's current state does not allow this property to be set.</exception>
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001A24 RID: 6692 RVA: 0x00070D38 File Offset: 0x0006EF38
		public DecompressionMethods AutomaticDecompression
		{
			get
			{
				return this.auto_decomp;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x00070D40 File Offset: 0x0006EF40
		internal bool InternalAllowBuffering
		{
			get
			{
				return this.allowBuffering && this.MethodWithBuffer;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001A26 RID: 6694 RVA: 0x00070D54 File Offset: 0x0006EF54
		private bool MethodWithBuffer
		{
			get
			{
				return this.method != "HEAD" && this.method != "GET" && this.method != "MKCOL" && this.method != "CONNECT" && this.method != "TRACE";
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001A27 RID: 6695 RVA: 0x00070DBB File Offset: 0x0006EFBB
		internal MobileTlsProvider TlsProvider
		{
			get
			{
				return this.tlsProvider;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001A28 RID: 6696 RVA: 0x00070DC3 File Offset: 0x0006EFC3
		internal MonoTlsSettings TlsSettings
		{
			get
			{
				return this.tlsSettings;
			}
		}

		/// <summary>Gets or sets the collection of security certificates that are associated with this request.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> that contains the security certificates associated with this request.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null. </exception>
		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001A29 RID: 6697 RVA: 0x00070DCB File Offset: 0x0006EFCB
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				if (this.certificates == null)
				{
					this.certificates = new X509CertificateCollection();
				}
				return this.certificates;
			}
		}

		/// <summary>Gets or sets the Content-length HTTP header.</summary>
		/// <returns>The number of bytes of data to send to the Internet resource. The default is -1, which indicates the property has not been set and that there is no request data to send.</returns>
		/// <exception cref="T:System.InvalidOperationException">The request has been started by calling the <see cref="M:System.Net.HttpWebRequest.GetRequestStream" />, <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />, <see cref="M:System.Net.HttpWebRequest.GetResponse" />, or <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The new <see cref="P:System.Net.HttpWebRequest.ContentLength" /> value is less than 0. </exception>
		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001A2A RID: 6698 RVA: 0x00070DE6 File Offset: 0x0006EFE6
		public override long ContentLength
		{
			get
			{
				return this.contentLength;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (set) Token: 0x06001A2B RID: 6699 RVA: 0x00070DEE File Offset: 0x0006EFEE
		internal long InternalContentLength
		{
			set
			{
				this.contentLength = value;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001A2C RID: 6700 RVA: 0x00070DF7 File Offset: 0x0006EFF7
		// (set) Token: 0x06001A2D RID: 6701 RVA: 0x00070DFF File Offset: 0x0006EFFF
		internal bool ThrowOnError { get; set; }

		/// <summary>Gets or sets authentication information for the request.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> that contains the authentication credentials associated with the request. The default is null.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001A2E RID: 6702 RVA: 0x00070E08 File Offset: 0x0006F008
		// (set) Token: 0x06001A2F RID: 6703 RVA: 0x00070E10 File Offset: 0x0006F010
		public override ICredentials Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.credentials = value;
			}
		}

		/// <summary>Gets or sets the default maximum length of an HTTP error response.</summary>
		/// <returns>An integer that represents the default maximum length of an HTTP error response.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0 and is not equal to -1. </exception>
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001A30 RID: 6704 RVA: 0x00070E19 File Offset: 0x0006F019
		[MonoTODO]
		public static int DefaultMaximumErrorResponseLength
		{
			get
			{
				return HttpWebRequest.defaultMaximumErrorResponseLength;
			}
		}

		/// <summary>Specifies a collection of the name/value pairs that make up the HTTP headers.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains the name/value pairs that make up the headers for the HTTP request.</returns>
		/// <exception cref="T:System.InvalidOperationException">The request has been started by calling the <see cref="M:System.Net.HttpWebRequest.GetRequestStream" />, <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />, <see cref="M:System.Net.HttpWebRequest.GetResponse" />, or <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> method. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001A31 RID: 6705 RVA: 0x00070E20 File Offset: 0x0006F020
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.webHeaders;
			}
		}

		/// <summary>Get or set the Host header value to use in an HTTP request independent from the request URI.</summary>
		/// <returns>The Host header value in the HTTP request.</returns>
		/// <exception cref="T:System.ArgumentNullException">The Host header cannot be set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The Host header cannot be set to an invalid value. </exception>
		/// <exception cref="T:System.InvalidOperationException">The Host header cannot be set after the <see cref="T:System.Net.HttpWebRequest" /> has already started to be sent. </exception>
		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001A32 RID: 6706 RVA: 0x00070E28 File Offset: 0x0006F028
		public string Host
		{
			get
			{
				Uri uri = this.hostUri ?? this.Address;
				if ((!(this.hostUri == null) && this.hostHasPort) || !this.Address.IsDefaultPort)
				{
					return uri.Host + ":" + uri.Port.ToString();
				}
				return uri.Host;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to make a persistent connection to the Internet resource.</summary>
		/// <returns>true if the request to the Internet resource should contain a Connection HTTP header with the value Keep-alive; otherwise, false. The default is true.</returns>
		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x00070E8E File Offset: 0x0006F08E
		public bool KeepAlive
		{
			get
			{
				return this.keepAlive;
			}
		}

		/// <summary>Gets or sets a time-out in milliseconds when writing to or reading from a stream.</summary>
		/// <returns>The number of milliseconds before the writing or reading times out. The default value is 300,000 milliseconds (5 minutes).</returns>
		/// <exception cref="T:System.InvalidOperationException">The request has already been sent. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than or equal to zero and is not equal to <see cref="F:System.Threading.Timeout.Infinite" /></exception>
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001A34 RID: 6708 RVA: 0x00070E96 File Offset: 0x0006F096
		public int ReadWriteTimeout
		{
			get
			{
				return this.readWriteTimeout;
			}
		}

		/// <summary>Gets or sets the method for the request.</summary>
		/// <returns>The request method to use to contact the Internet resource. The default value is GET.</returns>
		/// <exception cref="T:System.ArgumentException">No method is supplied.-or- The method string contains invalid characters. </exception>
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001A35 RID: 6709 RVA: 0x00070E9E File Offset: 0x0006F09E
		// (set) Token: 0x06001A36 RID: 6710 RVA: 0x00070EA8 File Offset: 0x0006F0A8
		public override string Method
		{
			get
			{
				return this.method;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("Cannot set null or blank methods on request.", "value");
				}
				if (HttpValidationHelpers.IsInvalidMethodOrHeaderString(value))
				{
					throw new ArgumentException("Cannot set null or blank methods on request.", "value");
				}
				this.method = value.ToUpperInvariant();
				if (this.method != "HEAD" && this.method != "GET" && this.method != "POST" && this.method != "PUT" && this.method != "DELETE" && this.method != "CONNECT" && this.method != "TRACE" && this.method != "MKCOL")
				{
					this.method = value;
				}
			}
		}

		/// <summary>Gets or sets the version of HTTP to use for the request.</summary>
		/// <returns>The HTTP version to use for the request. The default is <see cref="F:System.Net.HttpVersion.Version11" />.</returns>
		/// <exception cref="T:System.ArgumentException">The HTTP version is set to a value other than 1.0 or 1.1. </exception>
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001A37 RID: 6711 RVA: 0x00070F8B File Offset: 0x0006F18B
		public Version ProtocolVersion
		{
			get
			{
				return this.version;
			}
		}

		/// <summary>Gets or sets proxy information for the request.</summary>
		/// <returns>The <see cref="T:System.Net.IWebProxy" /> object to use to proxy the request. The default value is set by calling the <see cref="P:System.Net.GlobalProxySelection.Select" /> property.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Net.HttpWebRequest.Proxy" /> is set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The request has been started by calling <see cref="M:System.Net.HttpWebRequest.GetRequestStream" />, <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />, <see cref="M:System.Net.HttpWebRequest.GetResponse" />, or <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have permission for the requested operation. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001A38 RID: 6712 RVA: 0x00070F93 File Offset: 0x0006F193
		// (set) Token: 0x06001A39 RID: 6713 RVA: 0x00070F9B File Offset: 0x0006F19B
		public override IWebProxy Proxy
		{
			get
			{
				return this.proxy;
			}
			set
			{
				this.CheckRequestStarted();
				this.proxy = value;
				this.servicePoint = null;
				this.GetServicePoint();
			}
		}

		/// <summary>Gets the original Uniform Resource Identifier (URI) of the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that contains the URI of the Internet resource passed to the <see cref="M:System.Net.WebRequest.Create(System.String)" /> method.</returns>
		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x00070FB8 File Offset: 0x0006F1B8
		public override Uri RequestUri
		{
			get
			{
				return this.requestUri;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to send data in segments to the Internet resource.</summary>
		/// <returns>true to send data to the Internet resource in segments; otherwise, false. The default value is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The request has been started by calling the <see cref="M:System.Net.HttpWebRequest.GetRequestStream" />, <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />, <see cref="M:System.Net.HttpWebRequest.GetResponse" />, or <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> method. </exception>
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x00070FC0 File Offset: 0x0006F1C0
		public bool SendChunked
		{
			get
			{
				return this.sendChunked;
			}
		}

		/// <summary>Gets the service point to use for the request.</summary>
		/// <returns>A <see cref="T:System.Net.ServicePoint" /> that represents the network connection to the Internet resource.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x00070FC8 File Offset: 0x0006F1C8
		public ServicePoint ServicePoint
		{
			get
			{
				return this.GetServicePoint();
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x00070FD0 File Offset: 0x0006F1D0
		internal ServicePoint ServicePointNoLock
		{
			get
			{
				return this.servicePoint;
			}
		}

		/// <summary>Gets or sets the time-out value in milliseconds for the <see cref="M:System.Net.HttpWebRequest.GetResponse" /> and <see cref="M:System.Net.HttpWebRequest.GetRequestStream" /> methods.</summary>
		/// <returns>The number of milliseconds to wait before the request times out. The default value is 100,000 milliseconds (100 seconds).</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is less than zero and is not <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x00070FD8 File Offset: 0x0006F1D8
		public override int Timeout
		{
			get
			{
				return this.timeout;
			}
		}

		/// <summary>Gets or sets the value of the Transfer-encoding HTTP header.</summary>
		/// <returns>The value of the Transfer-encoding HTTP header. The default value is null.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set when <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set to the value "Chunked". </exception>
		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x00070FE0 File Offset: 0x0006F1E0
		public string TransferEncoding
		{
			get
			{
				return this.webHeaders["Transfer-Encoding"];
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether default credentials are sent with requests.</summary>
		/// <returns>true if the default credentials are used; otherwise false. The default value is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">You attempted to set this property after the request was sent.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="USERNAME" />
		/// </PermissionSet>
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x00070FF2 File Offset: 0x0006F1F2
		public override bool UseDefaultCredentials
		{
			get
			{
				return CredentialCache.DefaultCredentials == this.Credentials;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to allow high-speed NTLM-authenticated connection sharing.</summary>
		/// <returns>true to keep the authenticated connection open; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x00071001 File Offset: 0x0006F201
		public bool UnsafeAuthenticatedConnectionSharing
		{
			get
			{
				return this.unsafe_auth_blah;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x00071009 File Offset: 0x0006F209
		// (set) Token: 0x06001A43 RID: 6723 RVA: 0x00071011 File Offset: 0x0006F211
		internal bool ExpectContinue
		{
			get
			{
				return this.expectContinue;
			}
			set
			{
				this.expectContinue = value;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x0007101A File Offset: 0x0006F21A
		internal bool ProxyQuery
		{
			get
			{
				return this.servicePoint.UsesProxy && !this.servicePoint.UseConnect;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001A45 RID: 6725 RVA: 0x00071039 File Offset: 0x0006F239
		internal ServerCertValidationCallback ServerCertValidationCallback
		{
			get
			{
				return this.certValidationCallback;
			}
		}

		/// <summary>Gets or sets a callback function to validate the server certificate.</summary>
		/// <returns>Returns <see cref="T:System.Net.Security.RemoteCertificateValidationCallback" />.A callback function to validate the server certificate.</returns>
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x00071041 File Offset: 0x0006F241
		public RemoteCertificateValidationCallback ServerCertificateValidationCallback
		{
			get
			{
				if (this.certValidationCallback == null)
				{
					return null;
				}
				return this.certValidationCallback.ValidationCallback;
			}
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x00071058 File Offset: 0x0006F258
		internal ServicePoint GetServicePoint()
		{
			object obj = this.locker;
			lock (obj)
			{
				if (this.hostChanged || this.servicePoint == null)
				{
					this.servicePoint = ServicePointManager.FindServicePoint(this.actualUri, this.proxy);
					this.hostChanged = false;
				}
			}
			return this.servicePoint;
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x000710C8 File Offset: 0x0006F2C8
		private WebOperation SendRequest(bool redirecting, BufferOffsetSize writeBuffer, CancellationToken cancellationToken)
		{
			object obj = this.locker;
			WebOperation webOperation2;
			lock (obj)
			{
				if (!redirecting && this.requestSent)
				{
					WebOperation webOperation = this.currentOperation;
					if (webOperation == null)
					{
						throw new InvalidOperationException("Should never happen!");
					}
					webOperation2 = webOperation;
				}
				else
				{
					WebOperation webOperation = new WebOperation(this, writeBuffer, false, cancellationToken);
					if (Interlocked.CompareExchange<WebOperation>(ref this.currentOperation, webOperation, null) != null)
					{
						throw new InvalidOperationException("Invalid nested call.");
					}
					this.requestSent = true;
					if (!redirecting)
					{
						this.redirects = 0;
					}
					this.servicePoint = this.GetServicePoint();
					this.servicePoint.SendRequest(webOperation, this.connectionGroup);
					webOperation2 = webOperation;
				}
			}
			return webOperation2;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x0007117C File Offset: 0x0006F37C
		internal static Task<T> RunWithTimeout<T>(Func<CancellationToken, Task<T>> func, int timeout, Action abort, Func<bool> aborted, CancellationToken cancellationToken)
		{
			CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			return HttpWebRequest.RunWithTimeoutWorker<T>(func(cancellationTokenSource.Token), timeout, abort, aborted, cancellationTokenSource);
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x000711A8 File Offset: 0x0006F3A8
		private static async Task<T> RunWithTimeoutWorker<T>(Task<T> workerTask, int timeout, Action abort, Func<bool> aborted, CancellationTokenSource cts)
		{
			T result;
			try
			{
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = ServicePointScheduler.WaitAsync(workerTask, timeout).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (!configuredTaskAwaiter.GetResult())
				{
					try
					{
						cts.Cancel();
						abort();
					}
					catch
					{
					}
					workerTask.ContinueWith<int?>(delegate(Task<T> t)
					{
						AggregateException exception = t.Exception;
						if (exception == null)
						{
							return null;
						}
						return new int?(exception.GetHashCode());
					}, TaskContinuationOptions.OnlyOnFaulted);
					throw new WebException("The operation has timed out.", WebExceptionStatus.Timeout);
				}
				result = workerTask.Result;
			}
			catch (Exception ex)
			{
				throw HttpWebRequest.GetWebException(ex, aborted());
			}
			finally
			{
				cts.Dispose();
			}
			return result;
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x0007120C File Offset: 0x0006F40C
		private Task<T> RunWithTimeout<T>(Func<CancellationToken, Task<T>> func)
		{
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			return HttpWebRequest.RunWithTimeoutWorker<T>(func(cancellationTokenSource.Token), this.timeout, new Action(this.Abort), () => this.Aborted, cancellationTokenSource);
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x00071250 File Offset: 0x0006F450
		private async Task<HttpWebResponse> MyGetResponseAsync(CancellationToken cancellationToken)
		{
			if (this.Aborted)
			{
				throw HttpWebRequest.CreateRequestAbortedException();
			}
			WebCompletionSource completion = new WebCompletionSource();
			object obj = this.locker;
			WebOperation operation;
			lock (obj)
			{
				this.getResponseCalled = true;
				WebCompletionSource webCompletionSource = Interlocked.CompareExchange<WebCompletionSource>(ref this.responseTask, completion, null);
				if (webCompletionSource != null)
				{
					webCompletionSource.ThrowOnError();
					if (this.haveResponse && webCompletionSource.Task.IsCompleted)
					{
						return this.webResponse;
					}
					throw new InvalidOperationException("Cannot re-call start of asynchronous method while a previous call is still in progress.");
				}
				else
				{
					operation = this.currentOperation;
					if (this.currentOperation != null)
					{
						this.writeStream = this.currentOperation.WriteStream;
					}
					this.initialMethod = this.method;
					operation = this.SendRequest(false, null, cancellationToken);
				}
			}
			HttpWebResponse httpWebResponse;
			for (;;)
			{
				WebException throwMe = null;
				HttpWebResponse response = null;
				WebResponseStream stream = null;
				bool redirect = false;
				bool mustReadAll = false;
				WebOperation ntlm = null;
				BufferOffsetSize writeBuffer = null;
				try
				{
					cancellationToken.ThrowIfCancellationRequested();
					WebRequestStream webRequestStream = await operation.GetRequestStreamInternal().ConfigureAwait(false);
					this.writeStream = webRequestStream;
					await this.writeStream.WriteRequestAsync(cancellationToken).ConfigureAwait(false);
					stream = await operation.GetResponseStream();
					ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation> valueTuple = await this.GetResponseFromData(stream, cancellationToken).ConfigureAwait(false);
					response = valueTuple.Item1;
					redirect = valueTuple.Item2;
					mustReadAll = valueTuple.Item3;
					writeBuffer = valueTuple.Item4;
					ntlm = valueTuple.Item5;
				}
				catch (Exception ex)
				{
					throwMe = this.GetWebException(ex);
				}
				obj = this.locker;
				lock (obj)
				{
					if (throwMe != null)
					{
						this.haveResponse = true;
						completion.TrySetException(throwMe);
						throw throwMe;
					}
					if (!redirect)
					{
						this.haveResponse = true;
						this.webResponse = response;
						completion.TrySetCompleted();
						httpWebResponse = response;
						break;
					}
					this.finished_reading = false;
					this.haveResponse = false;
					this.webResponse = null;
					this.currentOperation = ntlm;
				}
				try
				{
					if (mustReadAll)
					{
						await stream.ReadAllAsync(redirect || ntlm != null, cancellationToken).ConfigureAwait(false);
					}
					operation.Finish(true, null);
					response.Close();
				}
				catch (Exception ex2)
				{
					throwMe = this.GetWebException(ex2);
				}
				obj = this.locker;
				lock (obj)
				{
					if (throwMe != null)
					{
						this.haveResponse = true;
						WebResponseStream webResponseStream = stream;
						if (webResponseStream != null)
						{
							webResponseStream.Close();
						}
						completion.TrySetException(throwMe);
						throw throwMe;
					}
					if (ntlm == null)
					{
						operation = this.SendRequest(true, writeBuffer, cancellationToken);
					}
					else
					{
						operation = ntlm;
					}
				}
				throwMe = null;
				response = null;
				stream = null;
				ntlm = null;
				writeBuffer = null;
			}
			return httpWebResponse;
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x0007129C File Offset: 0x0006F49C
		[return: TupleElementNames(new string[] { "response", "redirect", "mustReadAll", "writeBuffer", "ntlm" })]
		private async Task<ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation>> GetResponseFromData(WebResponseStream stream, CancellationToken cancellationToken)
		{
			HttpWebResponse response = new HttpWebResponse(this.actualUri, this.method, stream, this.cookieContainer);
			WebException throwMe = null;
			bool redirect = false;
			bool mustReadAll = false;
			WebOperation webOperation = null;
			Task<BufferOffsetSize> task = null;
			BufferOffsetSize bufferOffsetSize = null;
			object obj = this.locker;
			lock (obj)
			{
				ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException> valueTuple = this.CheckFinalStatus(response);
				redirect = valueTuple.Item1;
				mustReadAll = valueTuple.Item2;
				task = valueTuple.Item3;
				throwMe = valueTuple.Item4;
			}
			if (throwMe != null)
			{
				if (mustReadAll)
				{
					await stream.ReadAllAsync(false, cancellationToken).ConfigureAwait(false);
				}
				throw throwMe;
			}
			if (task != null)
			{
				bufferOffsetSize = await task.ConfigureAwait(false);
			}
			obj = this.locker;
			lock (obj)
			{
				bool flag2 = this.ProxyQuery && this.proxy != null && !this.proxy.IsBypassed(this.actualUri);
				if (!redirect)
				{
					if ((flag2 ? this.proxy_auth_state : this.auth_state).IsNtlmAuthenticated && response.StatusCode < HttpStatusCode.BadRequest)
					{
						stream.Connection.NtlmAuthenticated = true;
					}
					if (this.writeStream != null)
					{
						this.writeStream.KillBuffer();
					}
					return new ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation>(response, false, false, bufferOffsetSize, null);
				}
				if (this.sendChunked)
				{
					this.sendChunked = false;
					this.webHeaders.RemoveInternal("Transfer-Encoding");
				}
				webOperation = this.HandleNtlmAuth(stream, response, bufferOffsetSize, cancellationToken).Item1;
			}
			return new ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation>(response, true, mustReadAll, bufferOffsetSize, webOperation);
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x000712F0 File Offset: 0x0006F4F0
		internal static Exception FlattenException(Exception e)
		{
			AggregateException ex = e as AggregateException;
			if (ex != null)
			{
				ex = ex.Flatten();
				if (ex.InnerExceptions.Count == 1)
				{
					return ex.InnerException;
				}
			}
			return e;
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x00071324 File Offset: 0x0006F524
		private WebException GetWebException(Exception e)
		{
			return HttpWebRequest.GetWebException(e, this.Aborted);
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x00071334 File Offset: 0x0006F534
		private static WebException GetWebException(Exception e, bool aborted)
		{
			e = HttpWebRequest.FlattenException(e);
			WebException ex = e as WebException;
			if (ex != null && (!aborted || ex.Status == WebExceptionStatus.RequestCanceled || ex.Status == WebExceptionStatus.Timeout))
			{
				return ex;
			}
			if (aborted || e is OperationCanceledException || e is ObjectDisposedException)
			{
				return HttpWebRequest.CreateRequestAbortedException();
			}
			return new WebException(e.Message, e, WebExceptionStatus.UnknownError, null);
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x00071393 File Offset: 0x0006F593
		internal static WebException CreateRequestAbortedException()
		{
			return new WebException(SR.Format("The request was aborted: The request was canceled.", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
		}

		/// <summary>Begins an asynchronous request to an Internet resource.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request for a response.</returns>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate </param>
		/// <param name="state">The state object for this request. </param>
		/// <exception cref="T:System.InvalidOperationException">The stream is already in use by a previous call to <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />-or- <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set to a value and <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false.-or- The thread pool is running out of threads. </exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">
		///   <see cref="P:System.Net.HttpWebRequest.Method" /> is GET or HEAD, and either <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is greater than zero or <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is true.-or- <see cref="P:System.Net.HttpWebRequest.KeepAlive" /> is true, <see cref="P:System.Net.HttpWebRequest.AllowWriteStreamBuffering" /> is false, and either <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is -1, <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false and <see cref="P:System.Net.HttpWebRequest.Method" /> is POST or PUT.-or- The <see cref="T:System.Net.HttpWebRequest" /> has an entity body but the <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> method is called without calling the <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" /> method. -or- The <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is greater than zero, but the application does not write all of the promised data.</exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001A52 RID: 6738 RVA: 0x000713AC File Offset: 0x0006F5AC
		public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			if (this.Aborted)
			{
				throw HttpWebRequest.CreateRequestAbortedException();
			}
			string transferEncoding = this.TransferEncoding;
			if (!this.sendChunked && transferEncoding != null && transferEncoding.Trim() != "")
			{
				throw new InvalidOperationException("TransferEncoding requires the SendChunked property to be set to true.");
			}
			return TaskToApm.Begin(this.RunWithTimeout<HttpWebResponse>(new Func<CancellationToken, Task<HttpWebResponse>>(this.MyGetResponseAsync)), callback, state);
		}

		/// <summary>Ends an asynchronous request to an Internet resource.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> that contains the response from the Internet resource.</returns>
		/// <param name="asyncResult">The pending request for a response. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This method was called previously using <paramref name="asyncResult." />-or- The <see cref="P:System.Net.HttpWebRequest.ContentLength" /> property is greater than 0 but the data has not been written to the request stream. </exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called.-or- An error occurred while processing the request. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by the current instance from a call to <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001A53 RID: 6739 RVA: 0x00071410 File Offset: 0x0006F610
		public override WebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			WebResponse webResponse;
			try
			{
				webResponse = TaskToApm.End<HttpWebResponse>(asyncResult);
			}
			catch (Exception ex)
			{
				throw this.GetWebException(ex);
			}
			return webResponse;
		}

		/// <summary>Returns a response from an Internet resource.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> that contains the response from the Internet resource.</returns>
		/// <exception cref="T:System.InvalidOperationException">The stream is already in use by a previous call to <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />.-or- <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set to a value and <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false. </exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">
		///   <see cref="P:System.Net.HttpWebRequest.Method" /> is GET or HEAD, and either <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is greater or equal to zero or <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is true.-or- <see cref="P:System.Net.HttpWebRequest.KeepAlive" /> is true, <see cref="P:System.Net.HttpWebRequest.AllowWriteStreamBuffering" /> is false, <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is -1, <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false, and <see cref="P:System.Net.HttpWebRequest.Method" /> is POST or PUT. -or- The <see cref="T:System.Net.HttpWebRequest" /> has an entity body but the <see cref="M:System.Net.HttpWebRequest.GetResponse" /> method is called without calling the <see cref="M:System.Net.HttpWebRequest.GetRequestStream" /> method. -or- The <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is greater than zero, but the application does not write all of the promised data.</exception>
		/// <exception cref="T:System.NotSupportedException">The request cache validator indicated that the response for this request can be served from the cache; however, this request includes data to be sent to the server. Requests that send data must not use the cache. This exception can occur if you are using a custom cache validator that is incorrectly implemented. </exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called.-or- The time-out period for the request expired.-or- An error occurred while processing the request. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001A54 RID: 6740 RVA: 0x00071450 File Offset: 0x0006F650
		public override WebResponse GetResponse()
		{
			WebResponse result;
			try
			{
				result = this.GetResponseAsync().Result;
			}
			catch (Exception ex)
			{
				throw this.GetWebException(ex);
			}
			return result;
		}

		// Token: 0x170005C9 RID: 1481
		// (set) Token: 0x06001A55 RID: 6741 RVA: 0x00071488 File Offset: 0x0006F688
		internal bool FinishedReading
		{
			set
			{
				this.finished_reading = value;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x00071491 File Offset: 0x0006F691
		internal bool Aborted
		{
			get
			{
				return Interlocked.CompareExchange(ref this.aborted, 0, 0) == 1;
			}
		}

		/// <summary>Cancels a request to an Internet resource.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001A57 RID: 6743 RVA: 0x000714A4 File Offset: 0x0006F6A4
		public override void Abort()
		{
			if (Interlocked.CompareExchange(ref this.aborted, 1, 0) == 1)
			{
				return;
			}
			this.haveResponse = true;
			WebOperation webOperation = this.currentOperation;
			if (webOperation != null)
			{
				webOperation.Abort();
			}
			WebCompletionSource webCompletionSource = this.responseTask;
			if (webCompletionSource != null)
			{
				webCompletionSource.TrySetCanceled();
			}
			if (this.webResponse != null)
			{
				try
				{
					this.webResponse.Close();
					this.webResponse = null;
				}
				catch
				{
				}
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06001A58 RID: 6744 RVA: 0x0007151C File Offset: 0x0006F71C
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw new SerializationException();
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data required to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06001A59 RID: 6745 RVA: 0x0007151C File Offset: 0x0006F71C
		protected override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw new SerializationException();
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x00071523 File Offset: 0x0006F723
		private void CheckRequestStarted()
		{
			if (this.requestSent)
			{
				throw new InvalidOperationException("request started");
			}
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x00071538 File Offset: 0x0006F738
		internal void DoContinueDelegate(int statusCode, WebHeaderCollection headers)
		{
			if (this.continueDelegate != null)
			{
				this.continueDelegate(statusCode, headers);
			}
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x0007154F File Offset: 0x0006F74F
		private void RewriteRedirectToGet()
		{
			this.method = "GET";
			this.webHeaders.RemoveInternal("Transfer-Encoding");
			this.sendChunked = false;
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00071574 File Offset: 0x0006F774
		private bool Redirect(HttpStatusCode code, WebResponse response)
		{
			this.redirects++;
			Exception ex = null;
			string text = null;
			switch (code)
			{
			case HttpStatusCode.MultipleChoices:
				ex = new WebException("Ambiguous redirect.");
				goto IL_0097;
			case HttpStatusCode.MovedPermanently:
			case HttpStatusCode.Found:
				if (this.method == "POST")
				{
					this.RewriteRedirectToGet();
					goto IL_0097;
				}
				goto IL_0097;
			case HttpStatusCode.SeeOther:
				this.RewriteRedirectToGet();
				goto IL_0097;
			case HttpStatusCode.NotModified:
				return false;
			case HttpStatusCode.UseProxy:
				ex = new NotImplementedException("Proxy support not available.");
				goto IL_0097;
			case HttpStatusCode.TemporaryRedirect:
				goto IL_0097;
			}
			string text2 = "Invalid status code: ";
			int num = (int)code;
			ex = new ProtocolViolationException(text2 + num.ToString());
			IL_0097:
			if (this.method != "GET" && !this.InternalAllowBuffering && this.ResendContentFactory == null && (this.writeStream.WriteBufferLength > 0 || this.contentLength > 0L))
			{
				ex = new WebException("The request requires buffering data to succeed.", null, WebExceptionStatus.ProtocolError, response);
			}
			if (ex != null)
			{
				throw ex;
			}
			if (this.AllowWriteStreamBuffering || this.method == "GET")
			{
				this.contentLength = -1L;
			}
			text = response.Headers["Location"];
			if (text == null)
			{
				throw new WebException(string.Format("No Location header found for {0}", (int)code), null, WebExceptionStatus.ProtocolError, response);
			}
			Uri uri = this.actualUri;
			try
			{
				this.actualUri = new Uri(this.actualUri, text);
			}
			catch (Exception)
			{
				throw new WebException(string.Format("Invalid URL ({0}) for {1}", text, (int)code), null, WebExceptionStatus.ProtocolError, response);
			}
			this.hostChanged = this.actualUri.Scheme != uri.Scheme || this.Host != uri.Authority;
			return true;
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x00071730 File Offset: 0x0006F930
		private string GetHeaders()
		{
			bool flag = false;
			if (this.sendChunked)
			{
				flag = true;
				this.webHeaders.ChangeInternal("Transfer-Encoding", "chunked");
				this.webHeaders.RemoveInternal("Content-Length");
			}
			else if (this.contentLength != -1L)
			{
				if (this.auth_state.NtlmAuthState == HttpWebRequest.NtlmAuthState.Challenge || this.proxy_auth_state.NtlmAuthState == HttpWebRequest.NtlmAuthState.Challenge)
				{
					if (this.haveContentLength || this.gotRequestStream || this.contentLength > 0L)
					{
						this.webHeaders.SetInternal("Content-Length", "0");
					}
					else
					{
						this.webHeaders.RemoveInternal("Content-Length");
					}
				}
				else
				{
					if (this.contentLength > 0L)
					{
						flag = true;
					}
					if (this.haveContentLength || this.gotRequestStream || this.contentLength > 0L)
					{
						this.webHeaders.SetInternal("Content-Length", this.contentLength.ToString());
					}
				}
				this.webHeaders.RemoveInternal("Transfer-Encoding");
			}
			else
			{
				this.webHeaders.RemoveInternal("Content-Length");
			}
			if (this.actualVersion == HttpVersion.Version11 && flag && this.servicePoint.SendContinue)
			{
				this.webHeaders.ChangeInternal("Expect", "100-continue");
				this.expectContinue = true;
			}
			else
			{
				this.webHeaders.RemoveInternal("Expect");
				this.expectContinue = false;
			}
			bool proxyQuery = this.ProxyQuery;
			string text = (proxyQuery ? "Proxy-Connection" : "Connection");
			this.webHeaders.RemoveInternal((!proxyQuery) ? "Proxy-Connection" : "Connection");
			Version protocolVersion = this.servicePoint.ProtocolVersion;
			bool flag2 = protocolVersion == null || protocolVersion == HttpVersion.Version10;
			if (this.keepAlive && (this.version == HttpVersion.Version10 || flag2))
			{
				if (this.webHeaders[text] == null || this.webHeaders[text].IndexOf("keep-alive", StringComparison.OrdinalIgnoreCase) == -1)
				{
					this.webHeaders.ChangeInternal(text, "keep-alive");
				}
			}
			else if (!this.keepAlive && this.version == HttpVersion.Version11)
			{
				this.webHeaders.ChangeInternal(text, "close");
			}
			string text2;
			if (this.hostUri != null)
			{
				if (this.hostHasPort)
				{
					text2 = this.hostUri.GetComponents(UriComponents.HostAndPort, UriFormat.Unescaped);
				}
				else
				{
					text2 = this.hostUri.GetComponents(UriComponents.Host, UriFormat.Unescaped);
				}
			}
			else if (this.Address.IsDefaultPort)
			{
				text2 = this.Address.GetComponents(UriComponents.Host, UriFormat.Unescaped);
			}
			else
			{
				text2 = this.Address.GetComponents(UriComponents.HostAndPort, UriFormat.Unescaped);
			}
			this.webHeaders.SetInternal("Host", text2);
			if (this.cookieContainer != null)
			{
				string cookieHeader = this.cookieContainer.GetCookieHeader(this.actualUri);
				if (cookieHeader != "")
				{
					this.webHeaders.ChangeInternal("Cookie", cookieHeader);
				}
				else
				{
					this.webHeaders.RemoveInternal("Cookie");
				}
			}
			string text3 = null;
			if ((this.auto_decomp & DecompressionMethods.GZip) != DecompressionMethods.None)
			{
				text3 = "gzip";
			}
			if ((this.auto_decomp & DecompressionMethods.Deflate) != DecompressionMethods.None)
			{
				text3 = ((text3 != null) ? "gzip, deflate" : "deflate");
			}
			if (text3 != null)
			{
				this.webHeaders.ChangeInternal("Accept-Encoding", text3);
			}
			if (!this.usedPreAuth && this.preAuthenticate)
			{
				this.DoPreAuthenticate();
			}
			return this.webHeaders.ToString();
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00071AA4 File Offset: 0x0006FCA4
		private void DoPreAuthenticate()
		{
			bool flag = this.proxy != null && !this.proxy.IsBypassed(this.actualUri);
			ICredentials credentials = ((!flag || this.credentials != null) ? this.credentials : this.proxy.Credentials);
			Authorization authorization = AuthenticationManager.PreAuthenticate(this, credentials);
			if (authorization == null)
			{
				return;
			}
			this.webHeaders.RemoveInternal("Proxy-Authorization");
			this.webHeaders.RemoveInternal("Authorization");
			string text = ((flag && this.credentials == null) ? "Proxy-Authorization" : "Authorization");
			this.webHeaders[text] = authorization.Message;
			this.usedPreAuth = true;
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00071B50 File Offset: 0x0006FD50
		internal byte[] GetRequestHeaders()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text;
			if (!this.ProxyQuery)
			{
				text = this.actualUri.PathAndQuery;
			}
			else
			{
				text = string.Format("{0}://{1}{2}", this.actualUri.Scheme, this.Host, this.actualUri.PathAndQuery);
			}
			if (!this.force_version && this.servicePoint.ProtocolVersion != null && this.servicePoint.ProtocolVersion < this.version)
			{
				this.actualVersion = this.servicePoint.ProtocolVersion;
			}
			else
			{
				this.actualVersion = this.version;
			}
			stringBuilder.AppendFormat("{0} {1} HTTP/{2}.{3}\r\n", new object[]
			{
				this.method,
				text,
				this.actualVersion.Major,
				this.actualVersion.Minor
			});
			stringBuilder.Append(this.GetHeaders());
			string text2 = stringBuilder.ToString();
			return Encoding.UTF8.GetBytes(text2);
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00071C58 File Offset: 0x0006FE58
		private ValueTuple<WebOperation, bool> HandleNtlmAuth(WebResponseStream stream, HttpWebResponse response, BufferOffsetSize writeBuffer, CancellationToken cancellationToken)
		{
			bool flag = response.StatusCode == HttpStatusCode.ProxyAuthenticationRequired;
			if ((flag ? this.proxy_auth_state : this.auth_state).NtlmAuthState == HttpWebRequest.NtlmAuthState.None)
			{
				return new ValueTuple<WebOperation, bool>(null, false);
			}
			bool flag2 = this.auth_state.NtlmAuthState == HttpWebRequest.NtlmAuthState.Challenge || this.proxy_auth_state.NtlmAuthState == HttpWebRequest.NtlmAuthState.Challenge;
			WebOperation webOperation = new WebOperation(this, writeBuffer, flag2, cancellationToken);
			stream.Operation.SetPriorityRequest(webOperation);
			ICredentials credentials = ((!flag || this.proxy == null) ? this.credentials : this.proxy.Credentials);
			if (credentials != null)
			{
				stream.Connection.NtlmCredential = credentials.GetCredential(this.requestUri, "NTLM");
				stream.Connection.UnsafeAuthenticatedConnectionSharing = this.unsafe_auth_blah;
			}
			return new ValueTuple<WebOperation, bool>(webOperation, flag2);
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00071D24 File Offset: 0x0006FF24
		private bool CheckAuthorization(WebResponse response, HttpStatusCode code)
		{
			if (code != HttpStatusCode.ProxyAuthenticationRequired)
			{
				return this.auth_state.CheckAuthorization(response, code);
			}
			return this.proxy_auth_state.CheckAuthorization(response, code);
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00071D4C File Offset: 0x0006FF4C
		[return: TupleElementNames(new string[] { "task", "throwMe" })]
		private ValueTuple<Task<BufferOffsetSize>, WebException> GetRewriteHandler(HttpWebResponse response, bool redirect)
		{
			if (redirect)
			{
				if (!this.MethodWithBuffer)
				{
					return new ValueTuple<Task<BufferOffsetSize>, WebException>(null, null);
				}
				if (this.writeStream.WriteBufferLength == 0 || this.contentLength == 0L)
				{
					return new ValueTuple<Task<BufferOffsetSize>, WebException>(null, null);
				}
			}
			if (this.AllowWriteStreamBuffering)
			{
				return new ValueTuple<Task<BufferOffsetSize>, WebException>(Task.FromResult<BufferOffsetSize>(this.writeStream.GetWriteBuffer()), null);
			}
			if (this.ResendContentFactory == null)
			{
				return new ValueTuple<Task<BufferOffsetSize>, WebException>(null, new WebException("The request requires buffering data to succeed.", null, WebExceptionStatus.ProtocolError, response));
			}
			return new ValueTuple<Task<BufferOffsetSize>, WebException>(async delegate
			{
				BufferOffsetSize bufferOffsetSize;
				using (MemoryStream ms = new MemoryStream())
				{
					await this.ResendContentFactory(ms).ConfigureAwait(false);
					byte[] array = ms.ToArray();
					bufferOffsetSize = new BufferOffsetSize(array, 0, array.Length, false);
				}
				return bufferOffsetSize;
			}(), null);
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00071DDC File Offset: 0x0006FFDC
		[return: TupleElementNames(new string[] { "redirect", "mustReadAll", "writeBuffer", "throwMe" })]
		private ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException> CheckFinalStatus(HttpWebResponse response)
		{
			WebException ex = null;
			bool flag = false;
			Task<BufferOffsetSize> task = null;
			HttpStatusCode statusCode = response.StatusCode;
			if (((!this.auth_state.IsCompleted && statusCode == HttpStatusCode.Unauthorized && this.credentials != null) || (this.ProxyQuery && !this.proxy_auth_state.IsCompleted && statusCode == HttpStatusCode.ProxyAuthenticationRequired)) && !this.usedPreAuth && this.CheckAuthorization(response, statusCode))
			{
				flag = true;
				if (!this.MethodWithBuffer)
				{
					return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(true, flag, null, null);
				}
				ValueTuple<Task<BufferOffsetSize>, WebException> rewriteHandler = this.GetRewriteHandler(response, false);
				task = rewriteHandler.Item1;
				ex = rewriteHandler.Item2;
				if (ex == null)
				{
					return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(true, flag, task, null);
				}
				if (!this.ThrowOnError)
				{
					return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(false, flag, null, null);
				}
				this.writeStream.InternalClose();
				this.writeStream = null;
				response.Close();
				return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(false, flag, null, ex);
			}
			else
			{
				if (statusCode >= HttpStatusCode.BadRequest)
				{
					ex = new WebException(string.Format("The remote server returned an error: ({0}) {1}.", (int)statusCode, response.StatusDescription), null, WebExceptionStatus.ProtocolError, response);
					flag = true;
				}
				else if (statusCode == HttpStatusCode.NotModified && this.allowAutoRedirect)
				{
					ex = new WebException(string.Format("The remote server returned an error: ({0}) {1}.", (int)statusCode, response.StatusDescription), null, WebExceptionStatus.ProtocolError, response);
				}
				else if (statusCode >= HttpStatusCode.MultipleChoices && this.allowAutoRedirect && this.redirects >= this.maxAutoRedirect)
				{
					ex = new WebException("Max. redirections exceeded.", null, WebExceptionStatus.ProtocolError, response);
					flag = true;
				}
				if (ex == null)
				{
					int num = (int)statusCode;
					bool flag2 = false;
					if (this.allowAutoRedirect && num >= 300)
					{
						flag2 = this.Redirect(statusCode, response);
						ValueTuple<Task<BufferOffsetSize>, WebException> rewriteHandler2 = this.GetRewriteHandler(response, true);
						task = rewriteHandler2.Item1;
						ex = rewriteHandler2.Item2;
						if (flag2 && !this.unsafe_auth_blah)
						{
							this.auth_state.Reset();
							this.proxy_auth_state.Reset();
						}
					}
					if (num >= 300 && num != 304)
					{
						flag = true;
					}
					if (ex == null)
					{
						return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(flag2, flag, task, null);
					}
				}
				if (!this.ThrowOnError)
				{
					return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(false, flag, null, null);
				}
				if (this.writeStream != null)
				{
					this.writeStream.InternalClose();
					this.writeStream = null;
				}
				return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(false, flag, null, ex);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpWebRequest" /> class.</summary>
		// Token: 0x06001A67 RID: 6759 RVA: 0x0001C8B6 File Offset: 0x0001AAB6
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public HttpWebRequest()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040010BB RID: 4283
		private Uri requestUri;

		// Token: 0x040010BC RID: 4284
		private Uri actualUri;

		// Token: 0x040010BD RID: 4285
		private bool hostChanged;

		// Token: 0x040010BE RID: 4286
		private bool allowAutoRedirect;

		// Token: 0x040010BF RID: 4287
		private bool allowBuffering;

		// Token: 0x040010C0 RID: 4288
		private X509CertificateCollection certificates;

		// Token: 0x040010C1 RID: 4289
		private string connectionGroup;

		// Token: 0x040010C2 RID: 4290
		private bool haveContentLength;

		// Token: 0x040010C3 RID: 4291
		private long contentLength;

		// Token: 0x040010C4 RID: 4292
		private HttpContinueDelegate continueDelegate;

		// Token: 0x040010C5 RID: 4293
		private CookieContainer cookieContainer;

		// Token: 0x040010C6 RID: 4294
		private ICredentials credentials;

		// Token: 0x040010C7 RID: 4295
		private bool haveResponse;

		// Token: 0x040010C8 RID: 4296
		private bool requestSent;

		// Token: 0x040010C9 RID: 4297
		private WebHeaderCollection webHeaders;

		// Token: 0x040010CA RID: 4298
		private bool keepAlive;

		// Token: 0x040010CB RID: 4299
		private int maxAutoRedirect;

		// Token: 0x040010CC RID: 4300
		private string mediaType;

		// Token: 0x040010CD RID: 4301
		private string method;

		// Token: 0x040010CE RID: 4302
		private string initialMethod;

		// Token: 0x040010CF RID: 4303
		private bool pipelined;

		// Token: 0x040010D0 RID: 4304
		private bool preAuthenticate;

		// Token: 0x040010D1 RID: 4305
		private bool usedPreAuth;

		// Token: 0x040010D2 RID: 4306
		private Version version;

		// Token: 0x040010D3 RID: 4307
		private bool force_version;

		// Token: 0x040010D4 RID: 4308
		private Version actualVersion;

		// Token: 0x040010D5 RID: 4309
		private IWebProxy proxy;

		// Token: 0x040010D6 RID: 4310
		private bool sendChunked;

		// Token: 0x040010D7 RID: 4311
		private ServicePoint servicePoint;

		// Token: 0x040010D8 RID: 4312
		private int timeout;

		// Token: 0x040010D9 RID: 4313
		private int continueTimeout;

		// Token: 0x040010DA RID: 4314
		private WebRequestStream writeStream;

		// Token: 0x040010DB RID: 4315
		private HttpWebResponse webResponse;

		// Token: 0x040010DC RID: 4316
		private WebCompletionSource responseTask;

		// Token: 0x040010DD RID: 4317
		private WebOperation currentOperation;

		// Token: 0x040010DE RID: 4318
		private int aborted;

		// Token: 0x040010DF RID: 4319
		private bool gotRequestStream;

		// Token: 0x040010E0 RID: 4320
		private int redirects;

		// Token: 0x040010E1 RID: 4321
		private bool expectContinue;

		// Token: 0x040010E2 RID: 4322
		private bool getResponseCalled;

		// Token: 0x040010E3 RID: 4323
		private object locker;

		// Token: 0x040010E4 RID: 4324
		private bool finished_reading;

		// Token: 0x040010E5 RID: 4325
		private DecompressionMethods auto_decomp;

		// Token: 0x040010E6 RID: 4326
		private static int defaultMaxResponseHeadersLength = 64;

		// Token: 0x040010E7 RID: 4327
		private static int defaultMaximumErrorResponseLength = 64;

		// Token: 0x040010E8 RID: 4328
		private static RequestCachePolicy defaultCachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);

		// Token: 0x040010E9 RID: 4329
		private int readWriteTimeout;

		// Token: 0x040010EA RID: 4330
		private MobileTlsProvider tlsProvider;

		// Token: 0x040010EB RID: 4331
		private MonoTlsSettings tlsSettings;

		// Token: 0x040010EC RID: 4332
		private ServerCertValidationCallback certValidationCallback;

		// Token: 0x040010ED RID: 4333
		private bool hostHasPort;

		// Token: 0x040010EE RID: 4334
		private Uri hostUri;

		// Token: 0x040010EF RID: 4335
		private HttpWebRequest.AuthorizationState auth_state;

		// Token: 0x040010F0 RID: 4336
		private HttpWebRequest.AuthorizationState proxy_auth_state;

		// Token: 0x040010F1 RID: 4337
		[NonSerialized]
		internal Func<Stream, Task> ResendContentFactory;

		// Token: 0x040010F3 RID: 4339
		private bool unsafe_auth_blah;

		// Token: 0x02000416 RID: 1046
		private enum NtlmAuthState
		{
			// Token: 0x040010F5 RID: 4341
			None,
			// Token: 0x040010F6 RID: 4342
			Challenge,
			// Token: 0x040010F7 RID: 4343
			Response
		}

		// Token: 0x02000417 RID: 1047
		private struct AuthorizationState
		{
			// Token: 0x170005CB RID: 1483
			// (get) Token: 0x06001A68 RID: 6760 RVA: 0x0007203B File Offset: 0x0007023B
			public bool IsCompleted
			{
				get
				{
					return this.isCompleted;
				}
			}

			// Token: 0x170005CC RID: 1484
			// (get) Token: 0x06001A69 RID: 6761 RVA: 0x00072043 File Offset: 0x00070243
			public HttpWebRequest.NtlmAuthState NtlmAuthState
			{
				get
				{
					return this.ntlm_auth_state;
				}
			}

			// Token: 0x170005CD RID: 1485
			// (get) Token: 0x06001A6A RID: 6762 RVA: 0x0007204B File Offset: 0x0007024B
			public bool IsNtlmAuthenticated
			{
				get
				{
					return this.isCompleted && this.ntlm_auth_state > HttpWebRequest.NtlmAuthState.None;
				}
			}

			// Token: 0x06001A6B RID: 6763 RVA: 0x00072060 File Offset: 0x00070260
			public AuthorizationState(HttpWebRequest request, bool isProxy)
			{
				this.request = request;
				this.isProxy = isProxy;
				this.isCompleted = false;
				this.ntlm_auth_state = HttpWebRequest.NtlmAuthState.None;
			}

			// Token: 0x06001A6C RID: 6764 RVA: 0x00072080 File Offset: 0x00070280
			public bool CheckAuthorization(WebResponse response, HttpStatusCode code)
			{
				this.isCompleted = false;
				if (code == HttpStatusCode.Unauthorized && this.request.credentials == null)
				{
					return false;
				}
				if (this.isProxy != (code == HttpStatusCode.ProxyAuthenticationRequired))
				{
					return false;
				}
				if (this.isProxy && (this.request.proxy == null || this.request.proxy.Credentials == null))
				{
					return false;
				}
				string[] values = response.Headers.GetValues(this.isProxy ? "Proxy-Authenticate" : "WWW-Authenticate");
				if (values == null || values.Length == 0)
				{
					return false;
				}
				ICredentials credentials = ((!this.isProxy) ? this.request.credentials : this.request.proxy.Credentials);
				Authorization authorization = null;
				string[] array = values;
				for (int i = 0; i < array.Length; i++)
				{
					authorization = AuthenticationManager.Authenticate(array[i], this.request, credentials);
					if (authorization != null)
					{
						break;
					}
				}
				if (authorization == null)
				{
					return false;
				}
				this.request.webHeaders[this.isProxy ? "Proxy-Authorization" : "Authorization"] = authorization.Message;
				this.isCompleted = authorization.Complete;
				if (authorization.ModuleAuthenticationType == "NTLM")
				{
					this.ntlm_auth_state++;
				}
				return true;
			}

			// Token: 0x06001A6D RID: 6765 RVA: 0x000721BB File Offset: 0x000703BB
			public void Reset()
			{
				this.isCompleted = false;
				this.ntlm_auth_state = HttpWebRequest.NtlmAuthState.None;
				this.request.webHeaders.RemoveInternal(this.isProxy ? "Proxy-Authorization" : "Authorization");
			}

			// Token: 0x06001A6E RID: 6766 RVA: 0x000721EF File Offset: 0x000703EF
			public override string ToString()
			{
				return string.Format("{0}AuthState [{1}:{2}]", this.isProxy ? "Proxy" : "", this.isCompleted, this.ntlm_auth_state);
			}

			// Token: 0x040010F8 RID: 4344
			private readonly HttpWebRequest request;

			// Token: 0x040010F9 RID: 4345
			private readonly bool isProxy;

			// Token: 0x040010FA RID: 4346
			private bool isCompleted;

			// Token: 0x040010FB RID: 4347
			private HttpWebRequest.NtlmAuthState ntlm_auth_state;
		}
	}
}
