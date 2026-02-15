using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Mono.Net;

namespace System.Net
{
	/// <summary>Contains HTTP proxy settings for the <see cref="T:System.Net.WebRequest" /> class.</summary>
	// Token: 0x020003F0 RID: 1008
	[Serializable]
	public class WebProxy : IWebProxy, ISerializable
	{
		/// <summary>Initializes an empty instance of the <see cref="T:System.Net.WebProxy" /> class.</summary>
		// Token: 0x0600191A RID: 6426 RVA: 0x0006B484 File Offset: 0x00069684
		public WebProxy()
			: this(null, false, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified <see cref="T:System.Uri" /> instance, bypass setting, list of URIs to bypass, and credentials.</summary>
		/// <param name="Address">A <see cref="T:System.Uri" /> instance that contains the address of the proxy server. </param>
		/// <param name="BypassOnLocal">true to bypass the proxy for local addresses; otherwise, false. </param>
		/// <param name="BypassList">An array of regular expression strings that contains the URIs of the servers to bypass. </param>
		/// <param name="Credentials">An <see cref="T:System.Net.ICredentials" /> instance to submit to the proxy server for authentication. </param>
		// Token: 0x0600191B RID: 6427 RVA: 0x0006B490 File Offset: 0x00069690
		public WebProxy(Uri Address, bool BypassOnLocal, string[] BypassList, ICredentials Credentials)
		{
			this._ProxyAddress = Address;
			this._BypassOnLocal = BypassOnLocal;
			if (BypassList != null)
			{
				this._BypassList = new ArrayList(BypassList);
				this.UpdateRegExList(true);
			}
			this._Credentials = Credentials;
			this.m_EnableAutoproxy = true;
		}

		/// <summary>Gets or sets the address of the proxy server.</summary>
		/// <returns>A <see cref="T:System.Uri" /> instance that contains the address of the proxy server.</returns>
		// Token: 0x1700056D RID: 1389
		// (set) Token: 0x0600191C RID: 6428 RVA: 0x0006B4CB File Offset: 0x000696CB
		public Uri Address
		{
			set
			{
				this._UseRegistry = false;
				this.DeleteScriptEngine();
				this._ProxyHostAddresses = null;
				this._ProxyAddress = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to bypass the proxy server for local addresses.</summary>
		/// <returns>true to bypass the proxy server for local addresses; otherwise, false. The default value is false.</returns>
		// Token: 0x1700056E RID: 1390
		// (set) Token: 0x0600191D RID: 6429 RVA: 0x0006B4E8 File Offset: 0x000696E8
		public bool BypassProxyOnLocal
		{
			set
			{
				this._UseRegistry = false;
				this.DeleteScriptEngine();
				this._BypassOnLocal = value;
			}
		}

		/// <summary>Gets or sets the credentials to submit to the proxy server for authentication.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> instance that contains the credentials to submit to the proxy server for authentication.</returns>
		/// <exception cref="T:System.InvalidOperationException">You attempted to set this property when the <see cref="P:System.Net.WebProxy.UseDefaultCredentials" />  property was set to true. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x0600191E RID: 6430 RVA: 0x0006B4FE File Offset: 0x000696FE
		public ICredentials Credentials
		{
			get
			{
				return this._Credentials;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether the <see cref="P:System.Net.CredentialCache.DefaultCredentials" /> are sent with requests.</summary>
		/// <returns>true if the default credentials are used; otherwise, false. The default value is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">You attempted to set this property when the <see cref="P:System.Net.WebProxy.Credentials" /> property contains credentials other than the default credentials. For more information, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="USERNAME" />
		/// </PermissionSet>
		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x0006B506 File Offset: 0x00069706
		// (set) Token: 0x06001920 RID: 6432 RVA: 0x0006B518 File Offset: 0x00069718
		public bool UseDefaultCredentials
		{
			get
			{
				return this.Credentials is SystemNetworkCredential;
			}
			set
			{
				this._Credentials = (value ? CredentialCache.DefaultCredentials : null);
			}
		}

		/// <summary>Gets a list of addresses that do not use the proxy server.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> that contains a list of <see cref="P:System.Net.WebProxy.BypassList" /> arrays that represents URIs that do not use the proxy server when accessed.</returns>
		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x0006B52B File Offset: 0x0006972B
		public ArrayList BypassArrayList
		{
			get
			{
				if (this._BypassList == null)
				{
					this._BypassList = new ArrayList();
				}
				return this._BypassList;
			}
		}

		/// <summary>Returns the proxied URI for a request.</summary>
		/// <returns>The <see cref="T:System.Uri" /> instance of the Internet resource, if the resource is on the bypass list; otherwise, the <see cref="T:System.Uri" /> instance of the proxy.</returns>
		/// <param name="destination">The <see cref="T:System.Uri" /> instance of the requested Internet resource. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destination" /> parameter is null. </exception>
		// Token: 0x06001922 RID: 6434 RVA: 0x0006B548 File Offset: 0x00069748
		public Uri GetProxy(Uri destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			Uri uri;
			if (this.GetProxyAuto(destination, out uri))
			{
				return uri;
			}
			if (this.IsBypassedManual(destination))
			{
				return destination;
			}
			Hashtable proxyHostAddresses = this._ProxyHostAddresses;
			Uri uri2 = ((proxyHostAddresses != null) ? (proxyHostAddresses[destination.Scheme] as Uri) : this._ProxyAddress);
			if (!(uri2 != null))
			{
				return destination;
			}
			return uri2;
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x0006B5B4 File Offset: 0x000697B4
		private void UpdateRegExList(bool canThrow)
		{
			Regex[] array = null;
			ArrayList bypassList = this._BypassList;
			try
			{
				if (bypassList != null && bypassList.Count > 0)
				{
					array = new Regex[bypassList.Count];
					for (int i = 0; i < bypassList.Count; i++)
					{
						array[i] = new Regex((string)bypassList[i], RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
					}
				}
			}
			catch
			{
				if (!canThrow)
				{
					this._RegExBypassList = null;
					return;
				}
				throw;
			}
			this._RegExBypassList = array;
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x0006B634 File Offset: 0x00069834
		private bool IsMatchInBypassList(Uri input)
		{
			this.UpdateRegExList(false);
			if (this._RegExBypassList == null)
			{
				return false;
			}
			string text = input.Scheme + "://" + input.Host + ((!input.IsDefaultPort) ? (":" + input.Port.ToString()) : "");
			for (int i = 0; i < this._BypassList.Count; i++)
			{
				if (this._RegExBypassList[i].IsMatch(text))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x0006B6BC File Offset: 0x000698BC
		private bool IsLocal(Uri host)
		{
			string host2 = host.Host;
			IPAddress ipaddress;
			if (IPAddress.TryParse(host2, out ipaddress))
			{
				return IPAddress.IsLoopback(ipaddress) || NclUtilities.IsAddressLocal(ipaddress);
			}
			int num = host2.IndexOf('.');
			if (num == -1)
			{
				return true;
			}
			string text = "." + IPGlobalProperties.InternalGetIPGlobalProperties().DomainName;
			return text != null && text.Length == host2.Length - num && string.Compare(text, 0, host2, num, text.Length, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x0006B738 File Offset: 0x00069938
		private bool IsLocalInProxyHash(Uri host)
		{
			Hashtable proxyHostAddresses = this._ProxyHostAddresses;
			return proxyHostAddresses != null && (Uri)proxyHostAddresses[host.Scheme] == null;
		}

		/// <summary>Indicates whether to use the proxy server for the specified host.</summary>
		/// <returns>true if the proxy server should not be used for <paramref name="host" />; otherwise, false.</returns>
		/// <param name="host">The <see cref="T:System.Uri" /> instance of the host to check for proxy use. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="host" /> parameter is null. </exception>
		// Token: 0x06001927 RID: 6439 RVA: 0x0006B76C File Offset: 0x0006996C
		public bool IsBypassed(Uri host)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			bool flag;
			if (this.IsBypassedAuto(host, out flag))
			{
				return flag;
			}
			return this.IsBypassedManual(host);
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x0006B7A4 File Offset: 0x000699A4
		private bool IsBypassedManual(Uri host)
		{
			return host.IsLoopback || (this._ProxyAddress == null && this._ProxyHostAddresses == null) || (this._BypassOnLocal && this.IsLocal(host)) || this.IsMatchInBypassList(host) || this.IsLocalInProxyHash(host);
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Net.WebProxy" /> class using previously serialized content.</summary>
		/// <param name="serializationInfo">The serialization data. </param>
		/// <param name="streamingContext">The context for the serialized data. </param>
		// Token: 0x06001929 RID: 6441 RVA: 0x0006B7F4 File Offset: 0x000699F4
		protected WebProxy(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			bool flag = false;
			try
			{
				flag = serializationInfo.GetBoolean("_UseRegistry");
			}
			catch
			{
			}
			if (flag)
			{
				this.UnsafeUpdateFromRegistry();
				return;
			}
			this._ProxyAddress = (Uri)serializationInfo.GetValue("_ProxyAddress", typeof(Uri));
			this._BypassOnLocal = serializationInfo.GetBoolean("_BypassOnLocal");
			this._BypassList = (ArrayList)serializationInfo.GetValue("_BypassList", typeof(ArrayList));
			try
			{
				this.UseDefaultCredentials = serializationInfo.GetBoolean("_UseDefaultCredentials");
			}
			catch
			{
			}
		}

		/// <summary>Creates the serialization data and context that are used by the system to serialize a <see cref="T:System.Net.WebProxy" /> object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that indicates the destination for this serialization. </param>
		// Token: 0x0600192A RID: 6442 RVA: 0x0006B8A8 File Offset: 0x00069AA8
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data that is needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x0600192B RID: 6443 RVA: 0x0006B8B4 File Offset: 0x00069AB4
		protected virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("_BypassOnLocal", this._BypassOnLocal);
			serializationInfo.AddValue("_ProxyAddress", this._ProxyAddress);
			serializationInfo.AddValue("_BypassList", this._BypassList);
			serializationInfo.AddValue("_UseDefaultCredentials", this.UseDefaultCredentials);
			if (this._UseRegistry)
			{
				serializationInfo.AddValue("_UseRegistry", true);
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x0006B919 File Offset: 0x00069B19
		// (set) Token: 0x0600192D RID: 6445 RVA: 0x0006B921 File Offset: 0x00069B21
		internal AutoWebProxyScriptEngine ScriptEngine
		{
			get
			{
				return this.m_ScriptEngine;
			}
			set
			{
				this.m_ScriptEngine = value;
			}
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x0006B92C File Offset: 0x00069B2C
		public static IWebProxy CreateDefaultProxy()
		{
			if (Platform.IsMacOS)
			{
				IWebProxy defaultProxy = CFNetwork.GetDefaultProxy();
				if (defaultProxy != null)
				{
					return defaultProxy;
				}
			}
			return new WebProxy(true);
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x0006B951 File Offset: 0x00069B51
		internal WebProxy(bool enableAutoproxy)
		{
			this.m_EnableAutoproxy = enableAutoproxy;
			this.UnsafeUpdateFromRegistry();
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x0006B966 File Offset: 0x00069B66
		internal void DeleteScriptEngine()
		{
			if (this.ScriptEngine != null)
			{
				this.ScriptEngine.Close();
				this.ScriptEngine = null;
			}
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x0006B984 File Offset: 0x00069B84
		internal void UnsafeUpdateFromRegistry()
		{
			this._UseRegistry = true;
			this.ScriptEngine = new AutoWebProxyScriptEngine(this, true);
			WebProxyData webProxyData = this.ScriptEngine.GetWebProxyData();
			this.Update(webProxyData);
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x0006B9B8 File Offset: 0x00069BB8
		internal void Update(WebProxyData webProxyData)
		{
			lock (this)
			{
				this._BypassOnLocal = webProxyData.bypassOnLocal;
				this._ProxyAddress = webProxyData.proxyAddress;
				this._ProxyHostAddresses = webProxyData.proxyHostAddresses;
				this._BypassList = webProxyData.bypassList;
				this.ScriptEngine.AutomaticallyDetectSettings = this.m_EnableAutoproxy && webProxyData.automaticallyDetectSettings;
				this.ScriptEngine.AutomaticConfigurationScript = (this.m_EnableAutoproxy ? webProxyData.scriptLocation : null);
			}
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x0006BA58 File Offset: 0x00069C58
		private bool GetProxyAuto(Uri destination, out Uri proxyUri)
		{
			proxyUri = null;
			if (this.ScriptEngine == null)
			{
				return false;
			}
			IList<string> list = null;
			if (!this.ScriptEngine.GetProxies(destination, out list))
			{
				return false;
			}
			if (list.Count > 0)
			{
				if (WebProxy.AreAllBypassed(list, true))
				{
					proxyUri = destination;
				}
				else
				{
					proxyUri = WebProxy.ProxyUri(list[0]);
				}
			}
			return true;
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x0006BAAC File Offset: 0x00069CAC
		private bool IsBypassedAuto(Uri destination, out bool isBypassed)
		{
			isBypassed = true;
			if (this.ScriptEngine == null)
			{
				return false;
			}
			IList<string> list;
			if (!this.ScriptEngine.GetProxies(destination, out list))
			{
				return false;
			}
			if (list.Count == 0)
			{
				isBypassed = false;
			}
			else
			{
				isBypassed = WebProxy.AreAllBypassed(list, true);
			}
			return true;
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x0006BAF0 File Offset: 0x00069CF0
		private static bool AreAllBypassed(IEnumerable<string> proxies, bool checkFirstOnly)
		{
			bool flag = true;
			foreach (string text in proxies)
			{
				flag = string.IsNullOrEmpty(text);
				if (checkFirstOnly)
				{
					break;
				}
				if (!flag)
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x0006BB44 File Offset: 0x00069D44
		private static Uri ProxyUri(string proxyName)
		{
			if (proxyName != null && proxyName.Length != 0)
			{
				return new Uri("http://" + proxyName);
			}
			return null;
		}

		// Token: 0x04001000 RID: 4096
		private bool _UseRegistry;

		// Token: 0x04001001 RID: 4097
		private bool _BypassOnLocal;

		// Token: 0x04001002 RID: 4098
		private bool m_EnableAutoproxy;

		// Token: 0x04001003 RID: 4099
		private Uri _ProxyAddress;

		// Token: 0x04001004 RID: 4100
		private ArrayList _BypassList;

		// Token: 0x04001005 RID: 4101
		private ICredentials _Credentials;

		// Token: 0x04001006 RID: 4102
		private Regex[] _RegExBypassList;

		// Token: 0x04001007 RID: 4103
		private Hashtable _ProxyHostAddresses;

		// Token: 0x04001008 RID: 4104
		private AutoWebProxyScriptEngine m_ScriptEngine;
	}
}
