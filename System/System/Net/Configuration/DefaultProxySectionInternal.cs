using System;
using System.Configuration;
using System.Threading;

namespace System.Net.Configuration
{
	// Token: 0x02000480 RID: 1152
	internal sealed class DefaultProxySectionInternal
	{
		// Token: 0x06001C6C RID: 7276 RVA: 0x0007C670 File Offset: 0x0007A870
		private static IWebProxy GetDefaultProxy_UsingOldMonoCode()
		{
			DefaultProxySection defaultProxySection = ConfigurationManager.GetSection("system.net/defaultProxy") as DefaultProxySection;
			if (defaultProxySection == null)
			{
				return DefaultProxySectionInternal.GetSystemWebProxy();
			}
			ProxyElement proxy = defaultProxySection.Proxy;
			WebProxy webProxy;
			if (proxy.UseSystemDefault != ProxyElement.UseSystemDefaultValues.False && proxy.ProxyAddress == null)
			{
				IWebProxy systemWebProxy = DefaultProxySectionInternal.GetSystemWebProxy();
				if (!(systemWebProxy is WebProxy))
				{
					return systemWebProxy;
				}
				webProxy = (WebProxy)systemWebProxy;
			}
			else
			{
				webProxy = new WebProxy();
			}
			if (proxy.ProxyAddress != null)
			{
				webProxy.Address = proxy.ProxyAddress;
			}
			if (proxy.BypassOnLocal != ProxyElement.BypassOnLocalValues.Unspecified)
			{
				webProxy.BypassProxyOnLocal = proxy.BypassOnLocal == ProxyElement.BypassOnLocalValues.True;
			}
			foreach (object obj in defaultProxySection.BypassList)
			{
				BypassElement bypassElement = (BypassElement)obj;
				webProxy.BypassArrayList.Add(bypassElement.Address);
			}
			return webProxy;
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x0007C768 File Offset: 0x0007A968
		private static IWebProxy GetSystemWebProxy()
		{
			return global::System.Net.WebProxy.CreateDefaultProxy();
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x0007C770 File Offset: 0x0007A970
		internal static object ClassSyncObject
		{
			get
			{
				if (DefaultProxySectionInternal.classSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref DefaultProxySectionInternal.classSyncObject, obj, null);
				}
				return DefaultProxySectionInternal.classSyncObject;
			}
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x0007C79C File Offset: 0x0007A99C
		internal static DefaultProxySectionInternal GetSection()
		{
			object obj = DefaultProxySectionInternal.ClassSyncObject;
			DefaultProxySectionInternal defaultProxySectionInternal;
			lock (obj)
			{
				defaultProxySectionInternal = new DefaultProxySectionInternal
				{
					webProxy = DefaultProxySectionInternal.GetDefaultProxy_UsingOldMonoCode()
				};
			}
			return defaultProxySectionInternal;
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001C70 RID: 7280 RVA: 0x0007C7E8 File Offset: 0x0007A9E8
		internal IWebProxy WebProxy
		{
			get
			{
				return this.webProxy;
			}
		}

		// Token: 0x04001381 RID: 4993
		private IWebProxy webProxy;

		// Token: 0x04001382 RID: 4994
		private static object classSyncObject;
	}
}
