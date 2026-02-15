using System;
using System.IO;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Mono.Net.Security;
using Mono.Net.Security.Private;
using Mono.Security.Interface;

namespace System.Net.Security
{
	/// <summary>Provides a stream used for client-server communication that uses the Secure Socket Layer (SSL) security protocol to authenticate the server and optionally the client.</summary>
	// Token: 0x020004F5 RID: 1269
	public class SslStream : AuthenticatedStream
	{
		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001EEC RID: 7916 RVA: 0x00087580 File Offset: 0x00085780
		internal MobileAuthenticatedStream Impl
		{
			get
			{
				this.CheckDisposed();
				return this.impl;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001EED RID: 7917 RVA: 0x0008758E File Offset: 0x0008578E
		internal string InternalTargetHost
		{
			get
			{
				this.CheckDisposed();
				return this.impl.TargetHost;
			}
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x000875A1 File Offset: 0x000857A1
		private static MobileTlsProvider GetProvider()
		{
			return (MobileTlsProvider)Mono.Security.Interface.MonoTlsProviderFactory.GetProvider();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Security.SslStream" /> class using the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="innerStream">A <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.SslStream" /> for sending and receiving data.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="innerStream" /> is not readable.-or-<paramref name="innerStream" /> is not writable.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="innerStream" /> is null.-or-<paramref name="innerStream" /> is equal to <see cref="F:System.IO.Stream.Null" />.</exception>
		// Token: 0x06001EEF RID: 7919 RVA: 0x000875AD File Offset: 0x000857AD
		public SslStream(Stream innerStream)
			: this(innerStream, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Security.SslStream" /> class using the specified <see cref="T:System.IO.Stream" /> and stream closure behavior.</summary>
		/// <param name="innerStream">A <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.SslStream" /> for sending and receiving data.</param>
		/// <param name="leaveInnerStreamOpen">A Boolean value that indicates the closure behavior of the <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.SslStream" /> for sending and receiving data. This parameter indicates if the inner stream is left open.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="innerStream" /> is not readable.-or-<paramref name="innerStream" /> is not writable.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="innerStream" /> is null.-or-<paramref name="innerStream" /> is equal to <see cref="F:System.IO.Stream.Null" />.</exception>
		// Token: 0x06001EF0 RID: 7920 RVA: 0x000875B7 File Offset: 0x000857B7
		public SslStream(Stream innerStream, bool leaveInnerStreamOpen)
			: base(innerStream, leaveInnerStreamOpen)
		{
			this.provider = SslStream.GetProvider();
			this.settings = MonoTlsSettings.CopyDefaultSettings();
			this.impl = this.provider.CreateSslStream(this, innerStream, leaveInnerStreamOpen, this.settings);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Security.SslStream" /> class using the specified <see cref="T:System.IO.Stream" />, stream closure behavior and certificate validation delegate.</summary>
		/// <param name="innerStream">A <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.SslStream" /> for sending and receiving data.</param>
		/// <param name="leaveInnerStreamOpen">A Boolean value that indicates the closure behavior of the <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.SslStream" /> for sending and receiving data. This parameter indicates if the inner stream is left open.</param>
		/// <param name="userCertificateValidationCallback">A <see cref="T:System.Net.Security.RemoteCertificateValidationCallback" /> delegate responsible for validating the certificate supplied by the remote party.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="innerStream" /> is not readable.-or-<paramref name="innerStream" /> is not writable.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="innerStream" /> is null.-or-<paramref name="innerStream" /> is equal to <see cref="F:System.IO.Stream.Null" />.</exception>
		// Token: 0x06001EF1 RID: 7921 RVA: 0x000875F1 File Offset: 0x000857F1
		public SslStream(Stream innerStream, bool leaveInnerStreamOpen, RemoteCertificateValidationCallback userCertificateValidationCallback)
			: this(innerStream, leaveInnerStreamOpen, userCertificateValidationCallback, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Security.SslStream" /> class using the specified <see cref="T:System.IO.Stream" />, stream closure behavior, certificate validation delegate and certificate selection delegate.</summary>
		/// <param name="innerStream">A <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.SslStream" /> for sending and receiving data.</param>
		/// <param name="leaveInnerStreamOpen">A Boolean value that indicates the closure behavior of the <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.SslStream" /> for sending and receiving data. This parameter indicates if the inner stream is left open.</param>
		/// <param name="userCertificateValidationCallback">A <see cref="T:System.Net.Security.RemoteCertificateValidationCallback" /> delegate responsible for validating the certificate supplied by the remote party.</param>
		/// <param name="userCertificateSelectionCallback">A <see cref="T:System.Net.Security.LocalCertificateSelectionCallback" /> delegate responsible for selecting the certificate used for authentication.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="innerStream" /> is not readable.-or-<paramref name="innerStream" /> is not writable.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="innerStream" /> is null.-or-<paramref name="innerStream" /> is equal to <see cref="F:System.IO.Stream.Null" />.</exception>
		// Token: 0x06001EF2 RID: 7922 RVA: 0x00087600 File Offset: 0x00085800
		public SslStream(Stream innerStream, bool leaveInnerStreamOpen, RemoteCertificateValidationCallback userCertificateValidationCallback, LocalCertificateSelectionCallback userCertificateSelectionCallback)
			: base(innerStream, leaveInnerStreamOpen)
		{
			this.provider = SslStream.GetProvider();
			this.settings = MonoTlsSettings.CopyDefaultSettings();
			this.SetAndVerifyValidationCallback(userCertificateValidationCallback);
			this.SetAndVerifySelectionCallback(userCertificateSelectionCallback);
			this.impl = this.provider.CreateSslStream(this, innerStream, leaveInnerStreamOpen, this.settings);
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x00087654 File Offset: 0x00085854
		internal SslStream(Stream innerStream, bool leaveInnerStreamOpen, MonoTlsProvider provider, MonoTlsSettings settings)
			: base(innerStream, leaveInnerStreamOpen)
		{
			this.provider = (MobileTlsProvider)provider;
			this.settings = settings.Clone();
			this.explicitSettings = true;
			this.impl = this.provider.CreateSslStream(this, innerStream, leaveInnerStreamOpen, settings);
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x00087694 File Offset: 0x00085894
		private void SetAndVerifyValidationCallback(RemoteCertificateValidationCallback callback)
		{
			if (this.validationCallback == null)
			{
				this.validationCallback = callback;
				this.settings.RemoteCertificateValidationCallback = CallbackHelpers.PublicToMono(callback);
				return;
			}
			if ((callback != null && this.validationCallback != callback) || (this.explicitSettings & (this.settings.RemoteCertificateValidationCallback != null)))
			{
				throw new InvalidOperationException(SR.Format("The '{0}' option was already set in the SslStream constructor.", "RemoteCertificateValidationCallback"));
			}
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x00087700 File Offset: 0x00085900
		private void SetAndVerifySelectionCallback(LocalCertificateSelectionCallback callback)
		{
			if (this.selectionCallback == null)
			{
				this.selectionCallback = callback;
				if (callback == null)
				{
					this.settings.ClientCertificateSelectionCallback = null;
					return;
				}
				this.settings.ClientCertificateSelectionCallback = (string t, X509CertificateCollection lc, X509Certificate rc, string[] ai) => callback(this, t, lc, rc, ai);
				return;
			}
			else
			{
				if ((callback != null && this.selectionCallback != callback) || (this.explicitSettings && this.settings.ClientCertificateSelectionCallback != null))
				{
					throw new InvalidOperationException(SR.Format("The '{0}' option was already set in the SslStream constructor.", "LocalCertificateSelectionCallback"));
				}
				return;
			}
		}

		/// <summary>Called by clients to authenticate the server and optionally the client in a client-server connection.</summary>
		/// <param name="targetHost">The name of the server that shares this <see cref="T:System.Net.Security.SslStream" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetHost" /> is null.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed and left this object in an unusable state.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.-or-Server authentication using this <see cref="T:System.Net.Security.SslStream" /> was tried previously.-or- Authentication is already in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x06001EF6 RID: 7926 RVA: 0x000877A7 File Offset: 0x000859A7
		public virtual void AuthenticateAsClient(string targetHost)
		{
			this.AuthenticateAsClient(targetHost, new X509CertificateCollection(), SslProtocols.None, false);
		}

		/// <summary>Called by clients to authenticate the server and optionally the client in a client-server connection. The authentication process uses the specified certificate collection and SSL protocol.</summary>
		/// <param name="targetHost">The name of the server that will share this <see cref="T:System.Net.Security.SslStream" />.</param>
		/// <param name="clientCertificates">The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> that contains client certificates.</param>
		/// <param name="enabledSslProtocols">The <see cref="T:System.Security.Authentication.SslProtocols" /> value that represents the protocol used for authentication.</param>
		/// <param name="checkCertificateRevocation">A <see cref="T:System.Boolean" /> value that specifies whether the certificate revocation list is checked during authentication.</param>
		// Token: 0x06001EF7 RID: 7927 RVA: 0x000877B7 File Offset: 0x000859B7
		public virtual void AuthenticateAsClient(string targetHost, X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			this.Impl.AuthenticateAsClient(targetHost, clientCertificates, enabledSslProtocols, checkCertificateRevocation);
		}

		/// <summary>Called by clients to begin an asynchronous operation to authenticate the server and optionally the client using the specified certificates and security protocol.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that indicates the status of the asynchronous operation. </returns>
		/// <param name="targetHost">The name of the server that shares this <see cref="T:System.Net.Security.SslStream" />.</param>
		/// <param name="clientCertificates">The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> containing client certificates.</param>
		/// <param name="enabledSslProtocols">The <see cref="T:System.Security.Authentication.SslProtocols" /> value that represents the protocol used for authentication.</param>
		/// <param name="checkCertificateRevocation">A <see cref="T:System.Boolean" /> value that specifies whether the certificate revocation list is checked during authentication.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete. </param>
		/// <param name="asyncState">A user-defined object that contains information about the operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetHost" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enabledSslProtocols" /> is not a valid <see cref="T:System.Security.Authentication.SslProtocols" />  value.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed and left this object in an unusable state.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.-or-Server authentication using this <see cref="T:System.Net.Security.SslStream" /> was tried previously.-or- Authentication is already in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x06001EF8 RID: 7928 RVA: 0x000877C9 File Offset: 0x000859C9
		public virtual IAsyncResult BeginAuthenticateAsClient(string targetHost, X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation, AsyncCallback asyncCallback, object asyncState)
		{
			return TaskToApm.Begin(this.Impl.AuthenticateAsClientAsync(targetHost, clientCertificates, enabledSslProtocols, checkCertificateRevocation), asyncCallback, asyncState);
		}

		/// <summary>Ends a pending asynchronous server authentication operation started with a previous call to <see cref="Overload:System.Net.Security.SslStream.BeginAuthenticateAsServer" />.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to <see cref="Overload:System.Net.Security.SslStream.BeginAuthenticateAsServer" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not created by a call to <see cref="Overload:System.Net.Security.SslStream.BeginAuthenticateAsServer" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed and left this object in an unusable state.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no pending server authentication to complete.</exception>
		// Token: 0x06001EF9 RID: 7929 RVA: 0x0005D466 File Offset: 0x0005B666
		public virtual void EndAuthenticateAsClient(IAsyncResult asyncResult)
		{
			TaskToApm.End(asyncResult);
		}

		/// <summary>Called by servers to authenticate the server and optionally the client in a client-server connection using the specified certificates, requirements and security protocol.</summary>
		/// <param name="serverCertificate">The X509Certificate used to authenticate the server.</param>
		/// <param name="clientCertificateRequired">A <see cref="T:System.Boolean" /> value that specifies whether the client must supply a certificate for authentication.</param>
		/// <param name="enabledSslProtocols">The <see cref="T:System.Security.Authentication.SslProtocols" />  value that represents the protocol used for authentication.</param>
		/// <param name="checkCertificateRevocation">A <see cref="T:System.Boolean" /> value that specifies whether the certificate revocation list is checked during authentication.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serverCertificate" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enabledSslProtocols" /> is not a valid <see cref="T:System.Security.Authentication.SslProtocols" /> value.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed and left this object in an unusable state.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.-or-Client authentication using this <see cref="T:System.Net.Security.SslStream" /> was tried previously.-or- Authentication is already in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <see cref="Overload:System.Net.Security.SslStream.AuthenticateAsServer" /> method is not supported on Windows 95, Windows 98, or Windows Millennium.</exception>
		// Token: 0x06001EFA RID: 7930 RVA: 0x000877E4 File Offset: 0x000859E4
		public virtual void AuthenticateAsServer(X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			this.Impl.AuthenticateAsServer(serverCertificate, clientCertificateRequired, enabledSslProtocols, checkCertificateRevocation);
		}

		/// <summary>Called by clients to authenticate the server and optionally the client in a client-server connection as an asynchronous operation. The authentication process uses the specified certificate collection and SSL protocol.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />The task object representing the asynchronous operation.</returns>
		/// <param name="targetHost">The name of the server that will share this <see cref="T:System.Net.Security.SslStream" />.</param>
		/// <param name="clientCertificates">The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> that contains client certificates.</param>
		/// <param name="enabledSslProtocols">The <see cref="T:System.Security.Authentication.SslProtocols" /> value that represents the protocol used for authentication.</param>
		/// <param name="checkCertificateRevocation">A <see cref="T:System.Boolean" /> value that specifies whether the certificate revocation list is checked during authentication.</param>
		// Token: 0x06001EFB RID: 7931 RVA: 0x000877F6 File Offset: 0x000859F6
		public virtual Task AuthenticateAsClientAsync(string targetHost, X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			return this.Impl.AuthenticateAsClientAsync(targetHost, clientCertificates, enabledSslProtocols, checkCertificateRevocation);
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether authentication was successful.</summary>
		/// <returns>true if successful authentication occurred; otherwise, false.</returns>
		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001EFC RID: 7932 RVA: 0x00087808 File Offset: 0x00085A08
		public override bool IsAuthenticated
		{
			get
			{
				return this.Impl.IsAuthenticated;
			}
		}

		/// <summary>Gets the certificate used to authenticate the local endpoint.</summary>
		/// <returns>An X509Certificate object that represents the certificate supplied for authentication or null if no certificate was supplied.</returns>
		/// <exception cref="T:System.InvalidOperationException">Authentication failed or has not occurred.</exception>
		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001EFD RID: 7933 RVA: 0x00087815 File Offset: 0x00085A15
		public virtual X509Certificate LocalCertificate
		{
			get
			{
				return this.Impl.LocalCertificate;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the underlying stream is seekable.</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x000028AE File Offset: 0x00000AAE
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the underlying stream is readable.</summary>
		/// <returns>true if authentication has occurred and the underlying stream is readable; otherwise false.</returns>
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001EFF RID: 7935 RVA: 0x00087822 File Offset: 0x00085A22
		public override bool CanRead
		{
			get
			{
				return this.impl != null && this.impl.CanRead;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the underlying stream supports time-outs.</summary>
		/// <returns>true if the underlying stream supports time-outs; otherwise, false.</returns>
		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001F00 RID: 7936 RVA: 0x00006695 File Offset: 0x00004895
		public override bool CanTimeout
		{
			get
			{
				return base.InnerStream.CanTimeout;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the underlying stream is writable.</summary>
		/// <returns>true if authentication has occurred and the underlying stream is writable; otherwise false.</returns>
		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001F01 RID: 7937 RVA: 0x00087839 File Offset: 0x00085A39
		public override bool CanWrite
		{
			get
			{
				return this.impl != null && this.impl.CanWrite;
			}
		}

		/// <summary>Gets or sets the amount of time a read operation blocks waiting for data.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the amount of time that elapses before a synchronous read operation fails.</returns>
		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001F02 RID: 7938 RVA: 0x00087850 File Offset: 0x00085A50
		// (set) Token: 0x06001F03 RID: 7939 RVA: 0x0008785D File Offset: 0x00085A5D
		public override int ReadTimeout
		{
			get
			{
				return this.Impl.ReadTimeout;
			}
			set
			{
				this.Impl.ReadTimeout = value;
			}
		}

		/// <summary>Gets or sets the amount of time a write operation blocks waiting for data.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the amount of time that elapses before a synchronous write operation fails. </returns>
		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001F04 RID: 7940 RVA: 0x0008786B File Offset: 0x00085A6B
		// (set) Token: 0x06001F05 RID: 7941 RVA: 0x00087878 File Offset: 0x00085A78
		public override int WriteTimeout
		{
			get
			{
				return this.Impl.WriteTimeout;
			}
			set
			{
				this.Impl.WriteTimeout = value;
			}
		}

		/// <summary>Gets the length of the underlying stream.</summary>
		/// <returns>A <see cref="T:System.Int64" />.The length of the underlying stream.</returns>
		/// <exception cref="T:System.NotSupportedException">Getting the value of this property is not supported when the underlying stream is a <see cref="T:System.Net.Sockets.NetworkStream" />.</exception>
		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001F06 RID: 7942 RVA: 0x00087886 File Offset: 0x00085A86
		public override long Length
		{
			get
			{
				return this.Impl.Length;
			}
		}

		/// <summary>Gets or sets the current position in the underlying stream.</summary>
		/// <returns>A <see cref="T:System.Int64" />.The current position in the underlying stream.</returns>
		/// <exception cref="T:System.NotSupportedException">Setting this property is not supported.-or-Getting the value of this property is not supported when the underlying stream is a <see cref="T:System.Net.Sockets.NetworkStream" />.</exception>
		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x00087893 File Offset: 0x00085A93
		// (set) Token: 0x06001F08 RID: 7944 RVA: 0x000878A0 File Offset: 0x00085AA0
		public override long Position
		{
			get
			{
				return this.Impl.Position;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("This stream does not support seek operations."));
			}
		}

		/// <summary>Sets the length of the underlying stream.</summary>
		/// <param name="value">An <see cref="T:System.Int64" /> value that specifies the length of the stream.</param>
		// Token: 0x06001F09 RID: 7945 RVA: 0x000878B1 File Offset: 0x00085AB1
		public override void SetLength(long value)
		{
			this.Impl.SetLength(value);
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="offset">This value is ignored.</param>
		/// <param name="origin">This value is ignored.</param>
		/// <exception cref="T:System.NotSupportedException">Seeking is not supported by <see cref="T:System.Net.Security.SslStream" /> objects.</exception>
		// Token: 0x06001F0A RID: 7946 RVA: 0x000878A0 File Offset: 0x00085AA0
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("This stream does not support seek operations."));
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x000878BF File Offset: 0x00085ABF
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return base.InnerStream.FlushAsync(cancellationToken);
		}

		/// <summary>Causes any buffered data to be written to the underlying device.</summary>
		// Token: 0x06001F0C RID: 7948 RVA: 0x0000659C File Offset: 0x0000479C
		public override void Flush()
		{
			base.InnerStream.Flush();
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x000878CD File Offset: 0x00085ACD
		private void CheckDisposed()
		{
			if (this.impl == null)
			{
				throw new ObjectDisposedException("SslStream");
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Security.SslStream" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001F0E RID: 7950 RVA: 0x000878E4 File Offset: 0x00085AE4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this.impl != null && disposing)
				{
					this.impl.Dispose();
					this.impl = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Reads data from this stream and stores it in the specified array.</summary>
		/// <returns>A <see cref="T:System.Int32" /> value that specifies the number of bytes read. When there is no more data to be read, returns 0.</returns>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that receives the bytes read from this stream.</param>
		/// <param name="offset">A <see cref="T:System.Int32" /> that contains the zero-based location in <paramref name="buffer" /> at which to begin storing the data read from this stream.</param>
		/// <param name="count">A <see cref="T:System.Int32" /> that contains the maximum number of bytes to read from this stream.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" />
		///   <paramref name="&lt;" />
		///   <paramref name="0" />.<paramref name="-or-" /><paramref name="offset" /> &gt; the length of <paramref name="buffer" />.-or-<paramref name="offset" /> + count &gt; the length of <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.IO.IOException">The read operation failed. Check the inner exception, if present to determine the cause of the failure.</exception>
		/// <exception cref="T:System.NotSupportedException">There is already a read operation in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		// Token: 0x06001F0F RID: 7951 RVA: 0x0008792C File Offset: 0x00085B2C
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.Impl.Read(buffer, offset, count);
		}

		/// <summary>Write the specified number of <see cref="T:System.Byte" />s to the underlying stream using the specified buffer and offset.</summary>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that supplies the bytes written to the stream.</param>
		/// <param name="offset">A <see cref="T:System.Int32" /> that contains the zero-based location in <paramref name="buffer" /> at which to begin reading bytes to be written to the stream.</param>
		/// <param name="count">A <see cref="T:System.Int32" /> that contains the number of bytes to read from <paramref name="buffer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" />
		///   <paramref name="&lt;" />
		///   <paramref name="0" />.<paramref name="-or-" /><paramref name="offset" /> &gt; the length of <paramref name="buffer" />.-or-<paramref name="offset" /> + count &gt; the length of <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.IO.IOException">The write operation failed.</exception>
		/// <exception cref="T:System.NotSupportedException">There is already a write operation in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		// Token: 0x06001F10 RID: 7952 RVA: 0x0008793C File Offset: 0x00085B3C
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.Impl.Write(buffer, offset, count);
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x0008794C File Offset: 0x00085B4C
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this.Impl.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x0008795E File Offset: 0x00085B5E
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this.Impl.WriteAsync(buffer, offset, count, cancellationToken);
		}

		/// <summary>Begins an asynchronous read operation that reads data from the stream and stores it in the specified array.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that indicates the status of the asynchronous operation. </returns>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that receives the bytes read from the stream.</param>
		/// <param name="offset">The zero-based location in <paramref name="buffer" /> at which to begin storing the data read from this stream.</param>
		/// <param name="count">The maximum number of bytes to read from the stream.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the read operation is complete. </param>
		/// <param name="asyncState">A user-defined object that contains information about the read operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" />
		///   <paramref name="&lt;" />
		///   <paramref name="0" />.<paramref name="-or-" /><paramref name="offset" /> &gt; the length of <paramref name="buffer" />.-or-<paramref name="offset" /> + count &gt; the length of <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.IO.IOException">The read operation failed.-or-Encryption is in use, but the data could not be decrypted.</exception>
		/// <exception cref="T:System.NotSupportedException">There is already a read operation in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		// Token: 0x06001F13 RID: 7955 RVA: 0x00087970 File Offset: 0x00085B70
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return TaskToApm.Begin(this.Impl.ReadAsync(buffer, offset, count), callback, state);
		}

		/// <summary>Ends an asynchronous read operation started with a previous call to <see cref="M:System.Net.Security.SslStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.</summary>
		/// <returns>A <see cref="T:System.Int32" /> value that specifies the number of bytes read from the underlying stream.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to <see cref="M:System.Net.Security.SslStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /></param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not created by a call to <see cref="M:System.Net.Security.SslStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no pending read operation to complete.</exception>
		/// <exception cref="T:System.IO.IOException">The read operation failed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		// Token: 0x06001F14 RID: 7956 RVA: 0x0005D3FC File Offset: 0x0005B5FC
		public override int EndRead(IAsyncResult asyncResult)
		{
			return TaskToApm.End<int>(asyncResult);
		}

		/// <summary>Begins an asynchronous write operation that writes <see cref="T:System.Byte" />s from the specified buffer to the stream.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation. </returns>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that supplies the bytes to be written to the stream.</param>
		/// <param name="offset">The zero-based location in <paramref name="buffer" /> at which to begin reading bytes to be written to the stream.</param>
		/// <param name="count">An <see cref="T:System.Int32" /> value that specifies the number of bytes to read from <paramref name="buffer" />.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the write operation is complete. </param>
		/// <param name="asyncState">A user-defined object that contains information about the write operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" />
		///   <paramref name="&lt;" />
		///   <paramref name="0" />.<paramref name="-or-" /><paramref name="offset" /> &gt; the length of <paramref name="buffer" />.-or-<paramref name="offset" /> + count &gt; the length of <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.IO.IOException">The write operation failed.</exception>
		/// <exception cref="T:System.NotSupportedException">There is already a write operation in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		// Token: 0x06001F15 RID: 7957 RVA: 0x00087989 File Offset: 0x00085B89
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return TaskToApm.Begin(this.Impl.WriteAsync(buffer, offset, count), callback, state);
		}

		/// <summary>Ends an asynchronous write operation started with a previous call to <see cref="M:System.Net.Security.SslStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to <see cref="M:System.Net.Security.SslStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /></param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not created by a call to <see cref="M:System.Net.Security.SslStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no pending write operation to complete.</exception>
		/// <exception cref="T:System.IO.IOException">The write operation failed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		// Token: 0x06001F16 RID: 7958 RVA: 0x0005D466 File Offset: 0x0005B666
		public override void EndWrite(IAsyncResult asyncResult)
		{
			TaskToApm.End(asyncResult);
		}

		// Token: 0x0400167D RID: 5757
		private MobileTlsProvider provider;

		// Token: 0x0400167E RID: 5758
		private MonoTlsSettings settings;

		// Token: 0x0400167F RID: 5759
		private RemoteCertificateValidationCallback validationCallback;

		// Token: 0x04001680 RID: 5760
		private LocalCertificateSelectionCallback selectionCallback;

		// Token: 0x04001681 RID: 5761
		private MobileAuthenticatedStream impl;

		// Token: 0x04001682 RID: 5762
		private bool explicitSettings;
	}
}
