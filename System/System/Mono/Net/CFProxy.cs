using System;

namespace Mono.Net
{
	// Token: 0x02000057 RID: 87
	internal class CFProxy
	{
		// Token: 0x060000EA RID: 234 RVA: 0x0000426C File Offset: 0x0000246C
		static CFProxy()
		{
			IntPtr intPtr = CFObject.dlopen("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork", 0);
			CFProxy.kCFProxyAutoConfigurationJavaScriptKey = CFObject.GetCFObjectHandle(intPtr, "kCFProxyAutoConfigurationJavaScriptKey");
			CFProxy.kCFProxyAutoConfigurationURLKey = CFObject.GetCFObjectHandle(intPtr, "kCFProxyAutoConfigurationURLKey");
			CFProxy.kCFProxyHostNameKey = CFObject.GetCFObjectHandle(intPtr, "kCFProxyHostNameKey");
			CFProxy.kCFProxyPasswordKey = CFObject.GetCFObjectHandle(intPtr, "kCFProxyPasswordKey");
			CFProxy.kCFProxyPortNumberKey = CFObject.GetCFObjectHandle(intPtr, "kCFProxyPortNumberKey");
			CFProxy.kCFProxyTypeKey = CFObject.GetCFObjectHandle(intPtr, "kCFProxyTypeKey");
			CFProxy.kCFProxyUsernameKey = CFObject.GetCFObjectHandle(intPtr, "kCFProxyUsernameKey");
			CFProxy.kCFProxyTypeAutoConfigurationURL = CFObject.GetCFObjectHandle(intPtr, "kCFProxyTypeAutoConfigurationURL");
			CFProxy.kCFProxyTypeAutoConfigurationJavaScript = CFObject.GetCFObjectHandle(intPtr, "kCFProxyTypeAutoConfigurationJavaScript");
			CFProxy.kCFProxyTypeFTP = CFObject.GetCFObjectHandle(intPtr, "kCFProxyTypeFTP");
			CFProxy.kCFProxyTypeHTTP = CFObject.GetCFObjectHandle(intPtr, "kCFProxyTypeHTTP");
			CFProxy.kCFProxyTypeHTTPS = CFObject.GetCFObjectHandle(intPtr, "kCFProxyTypeHTTPS");
			CFProxy.kCFProxyTypeSOCKS = CFObject.GetCFObjectHandle(intPtr, "kCFProxyTypeSOCKS");
			CFObject.dlclose(intPtr);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004359 File Offset: 0x00002559
		internal CFProxy(CFDictionary settings)
		{
			this.settings = settings;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004368 File Offset: 0x00002568
		private static CFProxyType CFProxyTypeToEnum(IntPtr type)
		{
			if (type == CFProxy.kCFProxyTypeAutoConfigurationJavaScript)
			{
				return CFProxyType.AutoConfigurationJavaScript;
			}
			if (type == CFProxy.kCFProxyTypeAutoConfigurationURL)
			{
				return CFProxyType.AutoConfigurationUrl;
			}
			if (type == CFProxy.kCFProxyTypeFTP)
			{
				return CFProxyType.FTP;
			}
			if (type == CFProxy.kCFProxyTypeHTTP)
			{
				return CFProxyType.HTTP;
			}
			if (type == CFProxy.kCFProxyTypeHTTPS)
			{
				return CFProxyType.HTTPS;
			}
			if (type == CFProxy.kCFProxyTypeSOCKS)
			{
				return CFProxyType.SOCKS;
			}
			if (CFString.Compare(type, CFProxy.kCFProxyTypeAutoConfigurationJavaScript, 0) == 0)
			{
				return CFProxyType.AutoConfigurationJavaScript;
			}
			if (CFString.Compare(type, CFProxy.kCFProxyTypeAutoConfigurationURL, 0) == 0)
			{
				return CFProxyType.AutoConfigurationUrl;
			}
			if (CFString.Compare(type, CFProxy.kCFProxyTypeFTP, 0) == 0)
			{
				return CFProxyType.FTP;
			}
			if (CFString.Compare(type, CFProxy.kCFProxyTypeHTTP, 0) == 0)
			{
				return CFProxyType.HTTP;
			}
			if (CFString.Compare(type, CFProxy.kCFProxyTypeHTTPS, 0) == 0)
			{
				return CFProxyType.HTTPS;
			}
			if (CFString.Compare(type, CFProxy.kCFProxyTypeSOCKS, 0) == 0)
			{
				return CFProxyType.SOCKS;
			}
			return CFProxyType.None;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004430 File Offset: 0x00002630
		public IntPtr AutoConfigurationJavaScript
		{
			get
			{
				if (CFProxy.kCFProxyAutoConfigurationJavaScriptKey == IntPtr.Zero)
				{
					return IntPtr.Zero;
				}
				return this.settings[CFProxy.kCFProxyAutoConfigurationJavaScriptKey];
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004459 File Offset: 0x00002659
		public IntPtr AutoConfigurationUrl
		{
			get
			{
				if (CFProxy.kCFProxyAutoConfigurationURLKey == IntPtr.Zero)
				{
					return IntPtr.Zero;
				}
				return this.settings[CFProxy.kCFProxyAutoConfigurationURLKey];
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004482 File Offset: 0x00002682
		public string HostName
		{
			get
			{
				if (CFProxy.kCFProxyHostNameKey == IntPtr.Zero)
				{
					return null;
				}
				return CFString.AsString(this.settings[CFProxy.kCFProxyHostNameKey]);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000044AC File Offset: 0x000026AC
		public string Password
		{
			get
			{
				if (CFProxy.kCFProxyPasswordKey == IntPtr.Zero)
				{
					return null;
				}
				return CFString.AsString(this.settings[CFProxy.kCFProxyPasswordKey]);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000044D6 File Offset: 0x000026D6
		public int Port
		{
			get
			{
				if (CFProxy.kCFProxyPortNumberKey == IntPtr.Zero)
				{
					return 0;
				}
				return CFNumber.AsInt32(this.settings[CFProxy.kCFProxyPortNumberKey]);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004500 File Offset: 0x00002700
		public CFProxyType ProxyType
		{
			get
			{
				if (CFProxy.kCFProxyTypeKey == IntPtr.Zero)
				{
					return CFProxyType.None;
				}
				return CFProxy.CFProxyTypeToEnum(this.settings[CFProxy.kCFProxyTypeKey]);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000452A File Offset: 0x0000272A
		public string Username
		{
			get
			{
				if (CFProxy.kCFProxyUsernameKey == IntPtr.Zero)
				{
					return null;
				}
				return CFString.AsString(this.settings[CFProxy.kCFProxyUsernameKey]);
			}
		}

		// Token: 0x040000C1 RID: 193
		private static IntPtr kCFProxyAutoConfigurationJavaScriptKey;

		// Token: 0x040000C2 RID: 194
		private static IntPtr kCFProxyAutoConfigurationURLKey;

		// Token: 0x040000C3 RID: 195
		private static IntPtr kCFProxyHostNameKey;

		// Token: 0x040000C4 RID: 196
		private static IntPtr kCFProxyPasswordKey;

		// Token: 0x040000C5 RID: 197
		private static IntPtr kCFProxyPortNumberKey;

		// Token: 0x040000C6 RID: 198
		private static IntPtr kCFProxyTypeKey;

		// Token: 0x040000C7 RID: 199
		private static IntPtr kCFProxyUsernameKey;

		// Token: 0x040000C8 RID: 200
		private static IntPtr kCFProxyTypeAutoConfigurationURL;

		// Token: 0x040000C9 RID: 201
		private static IntPtr kCFProxyTypeAutoConfigurationJavaScript;

		// Token: 0x040000CA RID: 202
		private static IntPtr kCFProxyTypeFTP;

		// Token: 0x040000CB RID: 203
		private static IntPtr kCFProxyTypeHTTP;

		// Token: 0x040000CC RID: 204
		private static IntPtr kCFProxyTypeHTTPS;

		// Token: 0x040000CD RID: 205
		private static IntPtr kCFProxyTypeSOCKS;

		// Token: 0x040000CE RID: 206
		private CFDictionary settings;
	}
}
