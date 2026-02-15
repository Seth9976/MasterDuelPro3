using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;

namespace Mono.Net
{
	// Token: 0x02000059 RID: 89
	internal static class CFNetwork
	{
		// Token: 0x060000F7 RID: 247
		[DllImport("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork", EntryPoint = "CFNetworkCopyProxiesForAutoConfigurationScript")]
		private static extern IntPtr CFNetworkCopyProxiesForAutoConfigurationScriptSequential(IntPtr proxyAutoConfigurationScript, IntPtr targetURL, out IntPtr error);

		// Token: 0x060000F8 RID: 248
		[DllImport("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork")]
		private static extern IntPtr CFNetworkExecuteProxyAutoConfigurationURL(IntPtr proxyAutoConfigURL, IntPtr targetURL, CFNetwork.CFProxyAutoConfigurationResultCallback cb, ref CFStreamClientContext clientContext);

		// Token: 0x060000F9 RID: 249 RVA: 0x000045E8 File Offset: 0x000027E8
		private static void CFNetworkCopyProxiesForAutoConfigurationScriptThread()
		{
			bool flag = true;
			for (;;)
			{
				CFNetwork.proxy_event.WaitOne();
				do
				{
					object obj = CFNetwork.lock_obj;
					CFNetwork.GetProxyData getProxyData;
					lock (obj)
					{
						if (CFNetwork.get_proxy_queue.Count == 0)
						{
							break;
						}
						getProxyData = CFNetwork.get_proxy_queue.Dequeue();
						flag = CFNetwork.get_proxy_queue.Count > 0;
					}
					getProxyData.result = CFNetwork.CFNetworkCopyProxiesForAutoConfigurationScriptSequential(getProxyData.script, getProxyData.targetUri, out getProxyData.error);
					getProxyData.evt.Set();
				}
				while (flag);
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004684 File Offset: 0x00002884
		private static IntPtr CFNetworkCopyProxiesForAutoConfigurationScript(IntPtr proxyAutoConfigurationScript, IntPtr targetURL, out IntPtr error)
		{
			IntPtr result;
			using (CFNetwork.GetProxyData getProxyData = new CFNetwork.GetProxyData())
			{
				getProxyData.script = proxyAutoConfigurationScript;
				getProxyData.targetUri = targetURL;
				object obj = CFNetwork.lock_obj;
				lock (obj)
				{
					if (CFNetwork.get_proxy_queue == null)
					{
						CFNetwork.get_proxy_queue = new Queue<CFNetwork.GetProxyData>();
						CFNetwork.proxy_event = new AutoResetEvent(false);
						new Thread(new ThreadStart(CFNetwork.CFNetworkCopyProxiesForAutoConfigurationScriptThread))
						{
							IsBackground = true
						}.Start();
					}
					CFNetwork.get_proxy_queue.Enqueue(getProxyData);
					CFNetwork.proxy_event.Set();
				}
				getProxyData.evt.WaitOne();
				error = getProxyData.error;
				result = getProxyData.result;
			}
			return result;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004754 File Offset: 0x00002954
		private static CFArray CopyProxiesForAutoConfigurationScript(IntPtr proxyAutoConfigurationScript, CFUrl targetURL)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr = CFNetwork.CFNetworkCopyProxiesForAutoConfigurationScript(proxyAutoConfigurationScript, targetURL.Handle, out zero);
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new CFArray(intPtr, true);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000478C File Offset: 0x0000298C
		public static CFProxy[] GetProxiesForAutoConfigurationScript(IntPtr proxyAutoConfigurationScript, CFUrl targetURL)
		{
			if (proxyAutoConfigurationScript == IntPtr.Zero)
			{
				throw new ArgumentNullException("proxyAutoConfigurationScript");
			}
			if (targetURL == null)
			{
				throw new ArgumentNullException("targetURL");
			}
			CFArray cfarray = CFNetwork.CopyProxiesForAutoConfigurationScript(proxyAutoConfigurationScript, targetURL);
			if (cfarray == null)
			{
				return null;
			}
			CFProxy[] array = new CFProxy[cfarray.Count];
			for (int i = 0; i < array.Length; i++)
			{
				CFDictionary cfdictionary = new CFDictionary(cfarray[i], false);
				array[i] = new CFProxy(cfdictionary);
			}
			cfarray.Dispose();
			return array;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004804 File Offset: 0x00002A04
		public static CFProxy[] GetProxiesForAutoConfigurationScript(IntPtr proxyAutoConfigurationScript, Uri targetUri)
		{
			if (proxyAutoConfigurationScript == IntPtr.Zero)
			{
				throw new ArgumentNullException("proxyAutoConfigurationScript");
			}
			if (targetUri == null)
			{
				throw new ArgumentNullException("targetUri");
			}
			CFUrl cfurl = CFUrl.Create(targetUri.AbsoluteUri);
			CFProxy[] proxiesForAutoConfigurationScript = CFNetwork.GetProxiesForAutoConfigurationScript(proxyAutoConfigurationScript, cfurl);
			cfurl.Dispose();
			return proxiesForAutoConfigurationScript;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004858 File Offset: 0x00002A58
		public static CFProxy[] ExecuteProxyAutoConfigurationURL(IntPtr proxyAutoConfigURL, Uri targetURL)
		{
			CFUrl cfurl = CFUrl.Create(targetURL.AbsoluteUri);
			if (cfurl == null)
			{
				return null;
			}
			CFProxy[] proxies = null;
			CFRunLoop runLoop = CFRunLoop.CurrentRunLoop;
			CFNetwork.CFProxyAutoConfigurationResultCallback cfproxyAutoConfigurationResultCallback = delegate(IntPtr client, IntPtr proxyList, IntPtr error)
			{
				if (proxyList != IntPtr.Zero)
				{
					CFArray cfarray = new CFArray(proxyList, false);
					proxies = new CFProxy[cfarray.Count];
					for (int i = 0; i < proxies.Length; i++)
					{
						CFDictionary cfdictionary = new CFDictionary(cfarray[i], false);
						proxies[i] = new CFProxy(cfdictionary);
					}
					cfarray.Dispose();
				}
				runLoop.Stop();
			};
			CFStreamClientContext cfstreamClientContext = default(CFStreamClientContext);
			IntPtr intPtr = CFNetwork.CFNetworkExecuteProxyAutoConfigurationURL(proxyAutoConfigURL, cfurl.Handle, cfproxyAutoConfigurationResultCallback, ref cfstreamClientContext);
			CFString cfstring = CFString.Create("Mono.MacProxy");
			runLoop.AddSource(intPtr, cfstring);
			runLoop.RunInMode(cfstring, double.MaxValue, false);
			runLoop.RemoveSource(intPtr, cfstring);
			return proxies;
		}

		// Token: 0x060000FF RID: 255
		[DllImport("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork")]
		private static extern IntPtr CFNetworkCopyProxiesForURL(IntPtr url, IntPtr proxySettings);

		// Token: 0x06000100 RID: 256 RVA: 0x000048FC File Offset: 0x00002AFC
		private static CFArray CopyProxiesForURL(CFUrl url, CFDictionary proxySettings)
		{
			IntPtr intPtr = CFNetwork.CFNetworkCopyProxiesForURL(url.Handle, (proxySettings != null) ? proxySettings.Handle : IntPtr.Zero);
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new CFArray(intPtr, true);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000493C File Offset: 0x00002B3C
		public static CFProxy[] GetProxiesForURL(CFUrl url, CFProxySettings proxySettings)
		{
			if (url == null || url.Handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("url");
			}
			if (proxySettings == null)
			{
				proxySettings = CFNetwork.GetSystemProxySettings();
			}
			CFArray cfarray = CFNetwork.CopyProxiesForURL(url, proxySettings.Dictionary);
			if (cfarray == null)
			{
				return null;
			}
			CFProxy[] array = new CFProxy[cfarray.Count];
			for (int i = 0; i < array.Length; i++)
			{
				CFDictionary cfdictionary = new CFDictionary(cfarray[i], false);
				array[i] = new CFProxy(cfdictionary);
			}
			cfarray.Dispose();
			return array;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000049C0 File Offset: 0x00002BC0
		public static CFProxy[] GetProxiesForUri(Uri uri, CFProxySettings proxySettings)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			CFUrl cfurl = CFUrl.Create(uri.AbsoluteUri);
			if (cfurl == null)
			{
				return null;
			}
			CFProxy[] proxiesForURL = CFNetwork.GetProxiesForURL(cfurl, proxySettings);
			cfurl.Dispose();
			return proxiesForURL;
		}

		// Token: 0x06000103 RID: 259
		[DllImport("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork")]
		private static extern IntPtr CFNetworkCopySystemProxySettings();

		// Token: 0x06000104 RID: 260 RVA: 0x00004A04 File Offset: 0x00002C04
		public static CFProxySettings GetSystemProxySettings()
		{
			IntPtr intPtr = CFNetwork.CFNetworkCopySystemProxySettings();
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new CFProxySettings(new CFDictionary(intPtr, true));
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004A32 File Offset: 0x00002C32
		public static IWebProxy GetDefaultProxy()
		{
			return new CFNetwork.CFWebProxy();
		}

		// Token: 0x040000D6 RID: 214
		private static object lock_obj = new object();

		// Token: 0x040000D7 RID: 215
		private static Queue<CFNetwork.GetProxyData> get_proxy_queue;

		// Token: 0x040000D8 RID: 216
		private static AutoResetEvent proxy_event;

		// Token: 0x0200005A RID: 90
		private class GetProxyData : IDisposable
		{
			// Token: 0x06000107 RID: 263 RVA: 0x00004A45 File Offset: 0x00002C45
			public void Dispose()
			{
				this.evt.Close();
			}

			// Token: 0x040000D9 RID: 217
			public IntPtr script;

			// Token: 0x040000DA RID: 218
			public IntPtr targetUri;

			// Token: 0x040000DB RID: 219
			public IntPtr error;

			// Token: 0x040000DC RID: 220
			public IntPtr result;

			// Token: 0x040000DD RID: 221
			public ManualResetEvent evt = new ManualResetEvent(false);
		}

		// Token: 0x0200005B RID: 91
		// (Invoke) Token: 0x0600010A RID: 266
		private delegate void CFProxyAutoConfigurationResultCallback(IntPtr client, IntPtr proxyList, IntPtr error);

		// Token: 0x0200005C RID: 92
		private class CFWebProxy : IWebProxy
		{
			// Token: 0x17000027 RID: 39
			// (get) Token: 0x0600010C RID: 268 RVA: 0x00004A66 File Offset: 0x00002C66
			public ICredentials Credentials
			{
				get
				{
					return this.credentials;
				}
			}

			// Token: 0x0600010D RID: 269 RVA: 0x00004A70 File Offset: 0x00002C70
			private static Uri GetProxyUri(CFProxy proxy, out NetworkCredential credentials)
			{
				CFProxyType proxyType = proxy.ProxyType;
				string text;
				if (proxyType != CFProxyType.FTP)
				{
					if (proxyType - CFProxyType.HTTP > 1)
					{
						credentials = null;
						return null;
					}
					text = "http://";
				}
				else
				{
					text = "ftp://";
				}
				string username = proxy.Username;
				string password = proxy.Password;
				string hostName = proxy.HostName;
				int port = proxy.Port;
				if (username != null)
				{
					credentials = new NetworkCredential(username, password);
				}
				else
				{
					credentials = null;
				}
				return new Uri(text + hostName + ((port != 0) ? (":" + port.ToString()) : string.Empty), UriKind.Absolute);
			}

			// Token: 0x0600010E RID: 270 RVA: 0x00004AFF File Offset: 0x00002CFF
			private static Uri GetProxyUriFromScript(IntPtr script, Uri targetUri, out NetworkCredential credentials)
			{
				return CFNetwork.CFWebProxy.SelectProxy(CFNetwork.GetProxiesForAutoConfigurationScript(script, targetUri), targetUri, out credentials);
			}

			// Token: 0x0600010F RID: 271 RVA: 0x00004B0F File Offset: 0x00002D0F
			private static Uri ExecuteProxyAutoConfigurationURL(IntPtr proxyAutoConfigURL, Uri targetUri, out NetworkCredential credentials)
			{
				return CFNetwork.CFWebProxy.SelectProxy(CFNetwork.ExecuteProxyAutoConfigurationURL(proxyAutoConfigURL, targetUri), targetUri, out credentials);
			}

			// Token: 0x06000110 RID: 272 RVA: 0x00004B20 File Offset: 0x00002D20
			private static Uri SelectProxy(CFProxy[] proxies, Uri targetUri, out NetworkCredential credentials)
			{
				if (proxies == null)
				{
					credentials = null;
					return targetUri;
				}
				for (int i = 0; i < proxies.Length; i++)
				{
					switch (proxies[i].ProxyType)
					{
					case CFProxyType.None:
						credentials = null;
						return targetUri;
					case CFProxyType.FTP:
					case CFProxyType.HTTP:
					case CFProxyType.HTTPS:
						return CFNetwork.CFWebProxy.GetProxyUri(proxies[i], out credentials);
					}
				}
				credentials = null;
				return null;
			}

			// Token: 0x06000111 RID: 273 RVA: 0x00004B84 File Offset: 0x00002D84
			public Uri GetProxy(Uri targetUri)
			{
				NetworkCredential networkCredential = null;
				Uri uri = null;
				if (targetUri == null)
				{
					throw new ArgumentNullException("targetUri");
				}
				try
				{
					CFProxySettings systemProxySettings = CFNetwork.GetSystemProxySettings();
					CFProxy[] proxiesForUri = CFNetwork.GetProxiesForUri(targetUri, systemProxySettings);
					if (proxiesForUri != null)
					{
						int num = 0;
						while (num < proxiesForUri.Length && uri == null)
						{
							switch (proxiesForUri[num].ProxyType)
							{
							case CFProxyType.None:
								uri = targetUri;
								break;
							case CFProxyType.AutoConfigurationUrl:
								uri = CFNetwork.CFWebProxy.ExecuteProxyAutoConfigurationURL(proxiesForUri[num].AutoConfigurationUrl, targetUri, out networkCredential);
								break;
							case CFProxyType.AutoConfigurationJavaScript:
								uri = CFNetwork.CFWebProxy.GetProxyUriFromScript(proxiesForUri[num].AutoConfigurationJavaScript, targetUri, out networkCredential);
								break;
							case CFProxyType.FTP:
							case CFProxyType.HTTP:
							case CFProxyType.HTTPS:
								uri = CFNetwork.CFWebProxy.GetProxyUri(proxiesForUri[num], out networkCredential);
								break;
							}
							num++;
						}
						if (uri == null)
						{
							uri = targetUri;
						}
					}
					else
					{
						uri = targetUri;
					}
				}
				catch
				{
					uri = targetUri;
				}
				if (!this.userSpecified)
				{
					this.credentials = networkCredential;
				}
				return uri;
			}

			// Token: 0x06000112 RID: 274 RVA: 0x00004C78 File Offset: 0x00002E78
			public bool IsBypassed(Uri targetUri)
			{
				if (targetUri == null)
				{
					throw new ArgumentNullException("targetUri");
				}
				return this.GetProxy(targetUri) == targetUri;
			}

			// Token: 0x040000DE RID: 222
			private ICredentials credentials;

			// Token: 0x040000DF RID: 223
			private bool userSpecified;
		}
	}
}
