using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.WebSockets
{
	/// <summary>Options to use with a  <see cref="T:System.Net.WebSockets.ClientWebSocket" /> object.</summary>
	// Token: 0x020004DC RID: 1244
	public sealed class ClientWebSocketOptions
	{
		// Token: 0x06001E81 RID: 7809 RVA: 0x00085CF6 File Offset: 0x00083EF6
		internal ClientWebSocketOptions()
		{
			this._requestedSubProtocols = new List<string>();
			this._requestHeaders = new WebHeaderCollection();
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001E82 RID: 7810 RVA: 0x00085D35 File Offset: 0x00083F35
		internal WebHeaderCollection RequestHeaders
		{
			get
			{
				return this._requestHeaders;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001E83 RID: 7811 RVA: 0x00085D3D File Offset: 0x00083F3D
		internal List<string> RequestedSubProtocols
		{
			get
			{
				return this._requestedSubProtocols;
			}
		}

		/// <summary>Gets or sets the proxy for WebSocket requests.</summary>
		/// <returns>Returns <see cref="T:System.Net.IWebProxy" />.The proxy for WebSocket requests.</returns>
		// Token: 0x170006AA RID: 1706
		// (set) Token: 0x06001E84 RID: 7812 RVA: 0x00085D45 File Offset: 0x00083F45
		public IWebProxy Proxy
		{
			set
			{
				this.ThrowIfReadOnly();
				this._proxy = value;
			}
		}

		/// <summary>Gets or sets a collection of client side certificates.</summary>
		/// <returns>Returns <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.A collection of client side certificates.</returns>
		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001E85 RID: 7813 RVA: 0x00085D54 File Offset: 0x00083F54
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				if (this._clientCertificates == null)
				{
					this._clientCertificates = new X509CertificateCollection();
				}
				return this._clientCertificates;
			}
		}

		/// <summary>Gets or sets the cookies associated with the request.</summary>
		/// <returns>Returns <see cref="T:System.Net.CookieContainer" />.The cookies associated with the request.</returns>
		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001E86 RID: 7814 RVA: 0x00085D6F File Offset: 0x00083F6F
		public CookieContainer Cookies
		{
			get
			{
				return this._cookies;
			}
		}

		/// <summary>Adds a sub-protocol to be negotiated during the WebSocket connection handshake.</summary>
		/// <param name="subProtocol">The WebSocket sub-protocol to add.</param>
		// Token: 0x06001E87 RID: 7815 RVA: 0x00085D78 File Offset: 0x00083F78
		public void AddSubProtocol(string subProtocol)
		{
			this.ThrowIfReadOnly();
			WebSocketValidate.ValidateSubprotocol(subProtocol);
			using (List<string>.Enumerator enumerator = this._requestedSubProtocols.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (string.Equals(enumerator.Current, subProtocol, StringComparison.OrdinalIgnoreCase))
					{
						throw new ArgumentException(SR.Format("Duplicate protocols are not allowed: '{0}'.", subProtocol), "subProtocol");
					}
				}
			}
			this._requestedSubProtocols.Add(subProtocol);
		}

		/// <summary>Gets or sets the WebSocket protocol keep-alive interval in milliseconds.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The WebSocket protocol keep-alive interval in milliseconds.</returns>
		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001E88 RID: 7816 RVA: 0x00085DFC File Offset: 0x00083FFC
		public TimeSpan KeepAliveInterval
		{
			get
			{
				return this._keepAliveInterval;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x00085E04 File Offset: 0x00084004
		internal int ReceiveBufferSize
		{
			get
			{
				return this._receiveBufferSize;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001E8A RID: 7818 RVA: 0x00085E0C File Offset: 0x0008400C
		internal int SendBufferSize
		{
			get
			{
				return this._sendBufferSize;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x00085E14 File Offset: 0x00084014
		internal ArraySegment<byte>? Buffer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x00085E1C File Offset: 0x0008401C
		internal void SetToReadOnly()
		{
			this._isReadOnly = true;
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x00085E25 File Offset: 0x00084025
		private void ThrowIfReadOnly()
		{
			if (this._isReadOnly)
			{
				throw new InvalidOperationException("The WebSocket has already been started.");
			}
		}

		// Token: 0x040015F4 RID: 5620
		private bool _isReadOnly;

		// Token: 0x040015F5 RID: 5621
		private readonly List<string> _requestedSubProtocols;

		// Token: 0x040015F6 RID: 5622
		private readonly WebHeaderCollection _requestHeaders;

		// Token: 0x040015F7 RID: 5623
		private TimeSpan _keepAliveInterval = WebSocket.DefaultKeepAliveInterval;

		// Token: 0x040015F8 RID: 5624
		private bool _useDefaultCredentials;

		// Token: 0x040015F9 RID: 5625
		private ICredentials _credentials;

		// Token: 0x040015FA RID: 5626
		private IWebProxy _proxy;

		// Token: 0x040015FB RID: 5627
		private X509CertificateCollection _clientCertificates;

		// Token: 0x040015FC RID: 5628
		private CookieContainer _cookies;

		// Token: 0x040015FD RID: 5629
		private int _receiveBufferSize = 4096;

		// Token: 0x040015FE RID: 5630
		private int _sendBufferSize = 4096;

		// Token: 0x040015FF RID: 5631
		private ArraySegment<byte>? _buffer;

		// Token: 0x04001600 RID: 5632
		private RemoteCertificateValidationCallback _remoteCertificateValidationCallback;
	}
}
