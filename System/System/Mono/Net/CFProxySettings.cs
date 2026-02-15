using System;

namespace Mono.Net
{
	// Token: 0x02000058 RID: 88
	internal class CFProxySettings
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00004554 File Offset: 0x00002754
		static CFProxySettings()
		{
			IntPtr intPtr = CFObject.dlopen("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork", 0);
			CFProxySettings.kCFNetworkProxiesHTTPEnable = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesHTTPEnable");
			CFProxySettings.kCFNetworkProxiesHTTPPort = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesHTTPPort");
			CFProxySettings.kCFNetworkProxiesHTTPProxy = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesHTTPProxy");
			CFProxySettings.kCFNetworkProxiesProxyAutoConfigEnable = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesProxyAutoConfigEnable");
			CFProxySettings.kCFNetworkProxiesProxyAutoConfigJavaScript = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesProxyAutoConfigJavaScript");
			CFProxySettings.kCFNetworkProxiesProxyAutoConfigURLString = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesProxyAutoConfigURLString");
			CFObject.dlclose(intPtr);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000045D1 File Offset: 0x000027D1
		public CFProxySettings(CFDictionary settings)
		{
			this.settings = settings;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000045E0 File Offset: 0x000027E0
		public CFDictionary Dictionary
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x040000CF RID: 207
		private static IntPtr kCFNetworkProxiesHTTPEnable;

		// Token: 0x040000D0 RID: 208
		private static IntPtr kCFNetworkProxiesHTTPPort;

		// Token: 0x040000D1 RID: 209
		private static IntPtr kCFNetworkProxiesHTTPProxy;

		// Token: 0x040000D2 RID: 210
		private static IntPtr kCFNetworkProxiesProxyAutoConfigEnable;

		// Token: 0x040000D3 RID: 211
		private static IntPtr kCFNetworkProxiesProxyAutoConfigJavaScript;

		// Token: 0x040000D4 RID: 212
		private static IntPtr kCFNetworkProxiesProxyAutoConfigURLString;

		// Token: 0x040000D5 RID: 213
		private CFDictionary settings;
	}
}
