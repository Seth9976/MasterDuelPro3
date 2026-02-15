using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace System.Net
{
	// Token: 0x020003F1 RID: 1009
	internal class AutoWebProxyScriptEngine
	{
		// Token: 0x06001937 RID: 6455 RVA: 0x000026E5 File Offset: 0x000008E5
		public AutoWebProxyScriptEngine(WebProxy proxy, bool useRegistry)
		{
		}

		// Token: 0x17000573 RID: 1395
		// (set) Token: 0x06001938 RID: 6456 RVA: 0x0006BB63 File Offset: 0x00069D63
		public Uri AutomaticConfigurationScript
		{
			[CompilerGenerated]
			set
			{
				this.<AutomaticConfigurationScript>k__BackingField = value;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (set) Token: 0x06001939 RID: 6457 RVA: 0x0006BB6C File Offset: 0x00069D6C
		public bool AutomaticallyDetectSettings
		{
			[CompilerGenerated]
			set
			{
				this.<AutomaticallyDetectSettings>k__BackingField = value;
			}
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x0006BB78 File Offset: 0x00069D78
		public bool GetProxies(Uri destination, out IList<string> proxyList)
		{
			int num = 0;
			return this.GetProxies(destination, out proxyList, ref num);
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x0006BB91 File Offset: 0x00069D91
		public bool GetProxies(Uri destination, out IList<string> proxyList, ref int syncStatus)
		{
			proxyList = null;
			return false;
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00002FA0 File Offset: 0x000011A0
		public void Close()
		{
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x0006BB98 File Offset: 0x00069D98
		public WebProxyData GetWebProxyData()
		{
			WebProxyData webProxyData;
			if (AutoWebProxyScriptEngine.IsWindows())
			{
				webProxyData = this.InitializeRegistryGlobalProxy();
				if (webProxyData != null)
				{
					return webProxyData;
				}
			}
			webProxyData = this.ReadEnvVariables();
			return webProxyData ?? new WebProxyData();
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x0006BBCC File Offset: 0x00069DCC
		private WebProxyData ReadEnvVariables()
		{
			string text = Environment.GetEnvironmentVariable("http_proxy") ?? Environment.GetEnvironmentVariable("HTTP_PROXY");
			if (text != null)
			{
				try
				{
					if (!text.StartsWith("http://"))
					{
						text = "http://" + text;
					}
					Uri uri = new Uri(text);
					IPAddress ipaddress;
					if (IPAddress.TryParse(uri.Host, out ipaddress))
					{
						if (IPAddress.Any.Equals(ipaddress))
						{
							uri = new UriBuilder(uri)
							{
								Host = "127.0.0.1"
							}.Uri;
						}
						else if (IPAddress.IPv6Any.Equals(ipaddress))
						{
							uri = new UriBuilder(uri)
							{
								Host = "[::1]"
							}.Uri;
						}
					}
					bool flag = false;
					ArrayList arrayList = new ArrayList();
					string text2 = Environment.GetEnvironmentVariable("no_proxy") ?? Environment.GetEnvironmentVariable("NO_PROXY");
					if (text2 != null)
					{
						foreach (string text3 in text2.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
						{
							if (text3 != "*.local")
							{
								arrayList.Add(text3);
							}
							else
							{
								flag = true;
							}
						}
					}
					return new WebProxyData
					{
						proxyAddress = uri,
						bypassOnLocal = flag,
						bypassList = AutoWebProxyScriptEngine.CreateBypassList(arrayList)
					};
				}
				catch (UriFormatException)
				{
				}
			}
			return null;
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x0006BD2C File Offset: 0x00069F2C
		private static bool IsWindows()
		{
			return Environment.OSVersion.Platform < PlatformID.Unix;
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x0006BD3C File Offset: 0x00069F3C
		private WebProxyData InitializeRegistryGlobalProxy()
		{
			if ((int)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", "ProxyEnable", 0) <= 0)
			{
				return null;
			}
			string text = "";
			bool flag = false;
			ArrayList arrayList = new ArrayList();
			string text2 = (string)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", "ProxyServer", null);
			if (text2 == null)
			{
				return null;
			}
			string text3 = (string)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", "ProxyOverride", null);
			if (text2.Contains("="))
			{
				foreach (string text4 in text2.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
				{
					if (text4.StartsWith("http="))
					{
						text = text4.Substring(5);
						break;
					}
				}
			}
			else
			{
				text = text2;
			}
			if (text3 != null)
			{
				foreach (string text5 in text3.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
				{
					if (text5 != "<local>")
					{
						arrayList.Add(text5);
					}
					else
					{
						flag = true;
					}
				}
			}
			return new WebProxyData
			{
				proxyAddress = AutoWebProxyScriptEngine.ToUri(text),
				bypassOnLocal = flag,
				bypassList = AutoWebProxyScriptEngine.CreateBypassList(arrayList)
			};
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0006BE75 File Offset: 0x0006A075
		private static Uri ToUri(string address)
		{
			if (address == null)
			{
				return null;
			}
			if (address.IndexOf("://", StringComparison.Ordinal) == -1)
			{
				address = "http://" + address;
			}
			return new Uri(address);
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0006BEA0 File Offset: 0x0006A0A0
		private static ArrayList CreateBypassList(ArrayList al)
		{
			string[] array = al.ToArray(typeof(string)) as string[];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = "^" + Regex.Escape(array[i]).Replace("\\*", ".*").Replace("\\?", ".") + "$";
			}
			return new ArrayList(array);
		}
	}
}
