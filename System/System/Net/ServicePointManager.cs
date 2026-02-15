using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Net.Configuration;
using System.Net.Security;

namespace System.Net
{
	/// <summary>Manages the collection of <see cref="T:System.Net.ServicePoint" /> objects.</summary>
	// Token: 0x0200042B RID: 1067
	public class ServicePointManager
	{
		// Token: 0x06001AFC RID: 6908 RVA: 0x00075258 File Offset: 0x00073458
		static ServicePointManager()
		{
			ConnectionManagementSection connectionManagementSection = ConfigurationManager.GetSection("system.net/connectionManagement") as ConnectionManagementSection;
			if (connectionManagementSection != null)
			{
				ServicePointManager.manager = new ConnectionManagementData(null);
				foreach (object obj in connectionManagementSection.ConnectionManagement)
				{
					ConnectionManagementElement connectionManagementElement = (ConnectionManagementElement)obj;
					ServicePointManager.manager.Add(connectionManagementElement.Address, connectionManagementElement.MaxConnection);
				}
				ServicePointManager.defaultConnectionLimit = (int)ServicePointManager.manager.GetMaxConnections("*");
				return;
			}
			ServicePointManager.manager = (ConnectionManagementData)ConfigurationSettings.GetConfig("system.net/connectionManagement");
			if (ServicePointManager.manager != null)
			{
				ServicePointManager.defaultConnectionLimit = (int)ServicePointManager.manager.GetMaxConnections("*");
			}
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x00075360 File Offset: 0x00073560
		internal static ICertificatePolicy GetLegacyCertificatePolicy()
		{
			return ServicePointManager.policy;
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that indicates whether the certificate is checked against the certificate authority revocation list.</summary>
		/// <returns>true if the certificate revocation list is checked; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001AFE RID: 6910 RVA: 0x00075367 File Offset: 0x00073567
		[MonoTODO("CRL checks not implemented")]
		public static bool CheckCertificateRevocationList
		{
			get
			{
				return ServicePointManager._checkCRL;
			}
		}

		/// <summary>Gets or sets a value that indicates how long a Domain Name Service (DNS) resolution is considered valid.</summary>
		/// <returns>The time-out value, in milliseconds. A value of -1 indicates an infinite time-out period. The default value is 120,000 milliseconds (two minutes).</returns>
		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x0007536E File Offset: 0x0007356E
		public static int DnsRefreshTimeout
		{
			get
			{
				return ServicePointManager.dnsRefreshTimeout;
			}
		}

		/// <summary>Gets or sets the security protocol used by the <see cref="T:System.Net.ServicePoint" /> objects managed by the <see cref="T:System.Net.ServicePointManager" /> object.</summary>
		/// <returns>One of the values defined in the <see cref="T:System.Net.SecurityProtocolType" /> enumeration.</returns>
		/// <exception cref="T:System.NotSupportedException">The value specified to set the property is not a valid <see cref="T:System.Net.SecurityProtocolType" /> enumeration value. </exception>
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x00075375 File Offset: 0x00073575
		public static SecurityProtocolType SecurityProtocol
		{
			get
			{
				return ServicePointManager._securityProtocol;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x0007537C File Offset: 0x0007357C
		internal static ServerCertValidationCallback ServerCertValidationCallback
		{
			get
			{
				return ServicePointManager.server_cert_cb;
			}
		}

		/// <summary>Gets or sets the callback to validate a server certificate.</summary>
		/// <returns>A <see cref="T:System.Net.Security.RemoteCertificateValidationCallback" />. The default value is null.</returns>
		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x00075383 File Offset: 0x00073583
		public static RemoteCertificateValidationCallback ServerCertificateValidationCallback
		{
			get
			{
				if (ServicePointManager.server_cert_cb == null)
				{
					return null;
				}
				return ServicePointManager.server_cert_cb.ValidationCallback;
			}
		}

		/// <summary>Finds an existing <see cref="T:System.Net.ServicePoint" /> object or creates a new <see cref="T:System.Net.ServicePoint" /> object to manage communications with the specified <see cref="T:System.Uri" /> object.</summary>
		/// <returns>The <see cref="T:System.Net.ServicePoint" /> object that manages communications for the request.</returns>
		/// <param name="address">A <see cref="T:System.Uri" /> object that contains the address of the Internet resource to contact. </param>
		/// <param name="proxy">The proxy data for this request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of <see cref="T:System.Net.ServicePoint" /> objects defined in <see cref="P:System.Net.ServicePointManager.MaxServicePoints" /> has been reached. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001B03 RID: 6915 RVA: 0x00075398 File Offset: 0x00073598
		public static ServicePoint FindServicePoint(Uri address, IWebProxy proxy)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			Uri uri = new Uri(address.Scheme + "://" + address.Authority);
			bool flag = false;
			bool flag2 = false;
			if (proxy != null && !proxy.IsBypassed(address))
			{
				flag = true;
				bool flag3 = address.Scheme == "https";
				address = proxy.GetProxy(address);
				if (address.Scheme != "http")
				{
					throw new NotSupportedException("Proxy scheme not supported.");
				}
				if (flag3 && address.Scheme == "http")
				{
					flag2 = true;
				}
			}
			address = new Uri(address.Scheme + "://" + address.Authority);
			ServicePointManager.SPKey spkey = new ServicePointManager.SPKey(uri, flag ? address : null, flag2);
			ConcurrentDictionary<ServicePointManager.SPKey, ServicePoint> concurrentDictionary = ServicePointManager.servicePoints;
			ServicePoint servicePoint2;
			lock (concurrentDictionary)
			{
				ServicePoint servicePoint;
				if (ServicePointManager.servicePoints.TryGetValue(spkey, out servicePoint))
				{
					servicePoint2 = servicePoint;
				}
				else
				{
					if (ServicePointManager.maxServicePoints > 0 && ServicePointManager.servicePoints.Count >= ServicePointManager.maxServicePoints)
					{
						throw new InvalidOperationException("maximum number of service points reached");
					}
					string text = address.ToString();
					int maxConnections = (int)ServicePointManager.manager.GetMaxConnections(text);
					servicePoint = new ServicePoint(spkey, address, maxConnections, ServicePointManager.maxServicePointIdleTime);
					servicePoint.Expect100Continue = ServicePointManager.expectContinue;
					servicePoint.UseNagleAlgorithm = ServicePointManager.useNagle;
					servicePoint.UsesProxy = flag;
					servicePoint.UseConnect = flag2;
					servicePoint.SetTcpKeepAlive(ServicePointManager.tcp_keepalive, ServicePointManager.tcp_keepalive_time, ServicePointManager.tcp_keepalive_interval);
					servicePoint2 = ServicePointManager.servicePoints.GetOrAdd(spkey, servicePoint);
				}
			}
			return servicePoint2;
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x0007553C File Offset: 0x0007373C
		internal static void RemoveServicePoint(ServicePoint sp)
		{
			ServicePoint servicePoint;
			ServicePointManager.servicePoints.TryRemove(sp.Key, out servicePoint);
		}

		// Token: 0x0400118C RID: 4492
		private static ConcurrentDictionary<ServicePointManager.SPKey, ServicePoint> servicePoints = new ConcurrentDictionary<ServicePointManager.SPKey, ServicePoint>();

		// Token: 0x0400118D RID: 4493
		private static ICertificatePolicy policy;

		// Token: 0x0400118E RID: 4494
		private static int defaultConnectionLimit = 2;

		// Token: 0x0400118F RID: 4495
		private static int maxServicePointIdleTime = 100000;

		// Token: 0x04001190 RID: 4496
		private static int maxServicePoints = 0;

		// Token: 0x04001191 RID: 4497
		private static int dnsRefreshTimeout = 120000;

		// Token: 0x04001192 RID: 4498
		private static bool _checkCRL = false;

		// Token: 0x04001193 RID: 4499
		private static SecurityProtocolType _securityProtocol = SecurityProtocolType.SystemDefault;

		// Token: 0x04001194 RID: 4500
		private static bool expectContinue = true;

		// Token: 0x04001195 RID: 4501
		private static bool useNagle;

		// Token: 0x04001196 RID: 4502
		private static ServerCertValidationCallback server_cert_cb;

		// Token: 0x04001197 RID: 4503
		private static bool tcp_keepalive;

		// Token: 0x04001198 RID: 4504
		private static int tcp_keepalive_time;

		// Token: 0x04001199 RID: 4505
		private static int tcp_keepalive_interval;

		// Token: 0x0400119A RID: 4506
		private static ConnectionManagementData manager;

		// Token: 0x0200042C RID: 1068
		internal class SPKey
		{
			// Token: 0x06001B05 RID: 6917 RVA: 0x0007555C File Offset: 0x0007375C
			public SPKey(Uri uri, Uri proxy, bool use_connect)
			{
				this.uri = uri;
				this.proxy = proxy;
				this.use_connect = use_connect;
			}

			// Token: 0x170005F9 RID: 1529
			// (get) Token: 0x06001B06 RID: 6918 RVA: 0x00075579 File Offset: 0x00073779
			public bool UsesProxy
			{
				get
				{
					return this.proxy != null;
				}
			}

			// Token: 0x06001B07 RID: 6919 RVA: 0x00075588 File Offset: 0x00073788
			public override int GetHashCode()
			{
				return ((23 * 31 + (this.use_connect ? 1 : 0)) * 31 + this.uri.GetHashCode()) * 31 + ((this.proxy != null) ? this.proxy.GetHashCode() : 0);
			}

			// Token: 0x06001B08 RID: 6920 RVA: 0x000755D8 File Offset: 0x000737D8
			public override bool Equals(object obj)
			{
				ServicePointManager.SPKey spkey = obj as ServicePointManager.SPKey;
				return obj != null && this.uri.Equals(spkey.uri) && this.use_connect == spkey.use_connect && this.UsesProxy == spkey.UsesProxy && (!this.UsesProxy || this.proxy.Equals(spkey.proxy));
			}

			// Token: 0x0400119B RID: 4507
			private Uri uri;

			// Token: 0x0400119C RID: 4508
			private Uri proxy;

			// Token: 0x0400119D RID: 4509
			private bool use_connect;
		}
	}
}
