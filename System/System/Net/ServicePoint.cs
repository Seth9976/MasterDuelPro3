using System;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace System.Net
{
	/// <summary>Provides connection management for HTTP connections.</summary>
	// Token: 0x0200042A RID: 1066
	public class ServicePoint
	{
		// Token: 0x06001AE1 RID: 6881 RVA: 0x00074DC4 File Offset: 0x00072FC4
		internal ServicePoint(ServicePointManager.SPKey key, Uri uri, int connectionLimit, int maxIdleTime)
		{
			this.Key = key;
			this.uri = uri;
			this.connectionLimit = connectionLimit;
			this.maxIdleTime = maxIdleTime;
			this.Scheduler = new ServicePointScheduler(this, connectionLimit, maxIdleTime);
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x00074E23 File Offset: 0x00073023
		internal ServicePointManager.SPKey Key { get; }

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x00074E2B File Offset: 0x0007302B
		// (set) Token: 0x06001AE4 RID: 6884 RVA: 0x00074E33 File Offset: 0x00073033
		private ServicePointScheduler Scheduler { get; set; }

		/// <summary>Gets the Uniform Resource Identifier (URI) of the server that this <see cref="T:System.Net.ServicePoint" /> object connects to.</summary>
		/// <returns>An instance of the <see cref="T:System.Uri" /> class that contains the URI of the Internet server that this <see cref="T:System.Net.ServicePoint" /> object connects to.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Net.ServicePoint" /> is in host mode.</exception>
		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x00074E3C File Offset: 0x0007303C
		public Uri Address
		{
			get
			{
				return this.uri;
			}
		}

		/// <summary>Gets or sets the maximum number of connections allowed on this <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>The maximum number of connections allowed on this <see cref="T:System.Net.ServicePoint" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The connection limit is equal to or less than 0. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x00074E44 File Offset: 0x00073044
		public int ConnectionLimit
		{
			get
			{
				return this.connectionLimit;
			}
		}

		/// <summary>Gets the version of the HTTP protocol that the <see cref="T:System.Net.ServicePoint" /> object uses.</summary>
		/// <returns>A <see cref="T:System.Version" /> object that contains the HTTP protocol version that the <see cref="T:System.Net.ServicePoint" /> object uses.</returns>
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x00074E4C File Offset: 0x0007304C
		public virtual Version ProtocolVersion
		{
			get
			{
				return this.protocolVersion;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that determines whether 100-Continue behavior is used.</summary>
		/// <returns>true to expect 100-Continue responses for POST requests; otherwise, false. The default value is true.</returns>
		// Token: 0x170005ED RID: 1517
		// (set) Token: 0x06001AE8 RID: 6888 RVA: 0x00074E54 File Offset: 0x00073054
		public bool Expect100Continue
		{
			set
			{
				this.SendContinue = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that determines whether the Nagle algorithm is used on connections managed by this <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>true to use the Nagle algorithm; otherwise, false. The default value is true.</returns>
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x00074E5D File Offset: 0x0007305D
		// (set) Token: 0x06001AEA RID: 6890 RVA: 0x00074E65 File Offset: 0x00073065
		public bool UseNagleAlgorithm
		{
			get
			{
				return this.useNagle;
			}
			set
			{
				this.useNagle = value;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x00074E6E File Offset: 0x0007306E
		// (set) Token: 0x06001AEC RID: 6892 RVA: 0x00074E9A File Offset: 0x0007309A
		internal bool SendContinue
		{
			get
			{
				return this.sendContinue && (this.protocolVersion == null || this.protocolVersion == HttpVersion.Version11);
			}
			set
			{
				this.sendContinue = value;
			}
		}

		/// <summary>Enables or disables the keep-alive option on a TCP connection.</summary>
		/// <param name="enabled">If set to true, then the TCP keep-alive option on a TCP connection will be enabled using the specified <paramref name="keepAliveTime " />and <paramref name="keepAliveInterval" /> values. If set to false, then the TCP keep-alive option is disabled and the remaining parameters are ignored.The default value is false.</param>
		/// <param name="keepAliveTime">Specifies the timeout, in milliseconds, with no activity until the first keep-alive packet is sent. The value must be greater than 0.  If a value of less than or equal to zero is passed an <see cref="T:System.ArgumentOutOfRangeException" /> is thrown.</param>
		/// <param name="keepAliveInterval">Specifies the interval, in milliseconds, between when successive keep-alive packets are sent if no acknowledgement is received.The value must be greater than 0.  If a value of less than or equal to zero is passed an <see cref="T:System.ArgumentOutOfRangeException" /> is thrown.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for <paramref name="keepAliveTime" /> or <paramref name="keepAliveInterval" /> parameter is less than or equal to 0.</exception>
		// Token: 0x06001AED RID: 6893 RVA: 0x00074EA4 File Offset: 0x000730A4
		public void SetTcpKeepAlive(bool enabled, int keepAliveTime, int keepAliveInterval)
		{
			if (enabled)
			{
				if (keepAliveTime <= 0)
				{
					throw new ArgumentOutOfRangeException("keepAliveTime", "Must be greater than 0");
				}
				if (keepAliveInterval <= 0)
				{
					throw new ArgumentOutOfRangeException("keepAliveInterval", "Must be greater than 0");
				}
			}
			this.tcp_keepalive = enabled;
			this.tcp_keepalive_time = keepAliveTime;
			this.tcp_keepalive_interval = keepAliveInterval;
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x00074EF4 File Offset: 0x000730F4
		internal void KeepAliveSetup(Socket socket)
		{
			if (!this.tcp_keepalive)
			{
				return;
			}
			byte[] array = new byte[12];
			ServicePoint.PutBytes(array, this.tcp_keepalive ? 1U : 0U, 0);
			ServicePoint.PutBytes(array, (uint)this.tcp_keepalive_time, 4);
			ServicePoint.PutBytes(array, (uint)this.tcp_keepalive_interval, 8);
			socket.IOControl((IOControlCode)((ulong)(-1744830460)), array, null);
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x00074F50 File Offset: 0x00073150
		private static void PutBytes(byte[] bytes, uint v, int offset)
		{
			if (BitConverter.IsLittleEndian)
			{
				bytes[offset] = (byte)(v & 255U);
				bytes[offset + 1] = (byte)((v & 65280U) >> 8);
				bytes[offset + 2] = (byte)((v & 16711680U) >> 16);
				bytes[offset + 3] = (byte)((v & 4278190080U) >> 24);
				return;
			}
			bytes[offset + 3] = (byte)(v & 255U);
			bytes[offset + 2] = (byte)((v & 65280U) >> 8);
			bytes[offset + 1] = (byte)((v & 16711680U) >> 16);
			bytes[offset] = (byte)((v & 4278190080U) >> 24);
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x00074FD9 File Offset: 0x000731D9
		// (set) Token: 0x06001AF1 RID: 6897 RVA: 0x00074FE1 File Offset: 0x000731E1
		internal bool UsesProxy
		{
			get
			{
				return this.usesProxy;
			}
			set
			{
				this.usesProxy = value;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x00074FEA File Offset: 0x000731EA
		// (set) Token: 0x06001AF3 RID: 6899 RVA: 0x00074FF2 File Offset: 0x000731F2
		internal bool UseConnect
		{
			get
			{
				return this.useConnect;
			}
			set
			{
				this.useConnect = value;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x00074FFC File Offset: 0x000731FC
		private bool HasTimedOut
		{
			get
			{
				int dnsRefreshTimeout = ServicePointManager.DnsRefreshTimeout;
				return dnsRefreshTimeout != -1 && this.lastDnsResolve + TimeSpan.FromMilliseconds((double)dnsRefreshTimeout) < DateTime.UtcNow;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x00075034 File Offset: 0x00073234
		internal IPHostEntry HostEntry
		{
			get
			{
				object obj = this.hostE;
				lock (obj)
				{
					string text = this.uri.Host;
					if (this.uri.HostNameType == UriHostNameType.IPv6 || this.uri.HostNameType == UriHostNameType.IPv4)
					{
						if (this.host != null)
						{
							return this.host;
						}
						if (this.uri.HostNameType == UriHostNameType.IPv6)
						{
							text = text.Substring(1, text.Length - 2);
						}
						this.host = new IPHostEntry();
						this.host.AddressList = new IPAddress[] { IPAddress.Parse(text) };
						return this.host;
					}
					else
					{
						if (!this.HasTimedOut && this.host != null)
						{
							return this.host;
						}
						this.lastDnsResolve = DateTime.UtcNow;
						try
						{
							this.host = Dns.GetHostEntry(text);
						}
						catch
						{
							return null;
						}
					}
				}
				return this.host;
			}
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x00075140 File Offset: 0x00073340
		internal void SetVersion(Version version)
		{
			this.protocolVersion = version;
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x0007514C File Offset: 0x0007334C
		internal void SendRequest(WebOperation operation, string groupName)
		{
			lock (this)
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(typeof(ServicePoint).FullName);
				}
				this.Scheduler.SendRequest(operation, groupName);
			}
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x000751AC File Offset: 0x000733AC
		internal void FreeServicePoint()
		{
			this.disposed = true;
			this.Scheduler = null;
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x000751BC File Offset: 0x000733BC
		internal void UpdateServerCertificate(X509Certificate certificate)
		{
			if (certificate != null)
			{
				this.m_ServerCertificateOrBytes = certificate.GetRawCertData();
				return;
			}
			this.m_ServerCertificateOrBytes = null;
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x000751D5 File Offset: 0x000733D5
		internal void UpdateClientCertificate(X509Certificate certificate)
		{
			if (certificate != null)
			{
				this.m_ClientCertificateOrBytes = certificate.GetRawCertData();
				return;
			}
			this.m_ClientCertificateOrBytes = null;
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x000751F0 File Offset: 0x000733F0
		internal bool CallEndPointDelegate(Socket sock, IPEndPoint remote)
		{
			if (this.endPointCallback == null)
			{
				return true;
			}
			int num = 0;
			checked
			{
				for (;;)
				{
					IPEndPoint ipendPoint = null;
					try
					{
						ipendPoint = this.endPointCallback(this, remote, num);
					}
					catch
					{
						return false;
					}
					if (ipendPoint == null)
					{
						break;
					}
					try
					{
						sock.Bind(ipendPoint);
					}
					catch (SocketException)
					{
						num++;
						continue;
					}
					return true;
				}
				return true;
			}
		}

		// Token: 0x04001176 RID: 4470
		private readonly Uri uri;

		// Token: 0x04001177 RID: 4471
		private DateTime lastDnsResolve;

		// Token: 0x04001178 RID: 4472
		private Version protocolVersion;

		// Token: 0x04001179 RID: 4473
		private IPHostEntry host;

		// Token: 0x0400117A RID: 4474
		private bool usesProxy;

		// Token: 0x0400117B RID: 4475
		private bool sendContinue = true;

		// Token: 0x0400117C RID: 4476
		private bool useConnect;

		// Token: 0x0400117D RID: 4477
		private object hostE = new object();

		// Token: 0x0400117E RID: 4478
		private bool useNagle;

		// Token: 0x0400117F RID: 4479
		private BindIPEndPoint endPointCallback;

		// Token: 0x04001180 RID: 4480
		private bool tcp_keepalive;

		// Token: 0x04001181 RID: 4481
		private int tcp_keepalive_time;

		// Token: 0x04001182 RID: 4482
		private int tcp_keepalive_interval;

		// Token: 0x04001183 RID: 4483
		private bool disposed;

		// Token: 0x04001184 RID: 4484
		private int connectionLeaseTimeout = -1;

		// Token: 0x04001185 RID: 4485
		private int receiveBufferSize = -1;

		// Token: 0x04001188 RID: 4488
		private int connectionLimit;

		// Token: 0x04001189 RID: 4489
		private int maxIdleTime;

		// Token: 0x0400118A RID: 4490
		private object m_ServerCertificateOrBytes;

		// Token: 0x0400118B RID: 4491
		private object m_ClientCertificateOrBytes;
	}
}
