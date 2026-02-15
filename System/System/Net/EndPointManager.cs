using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x02000407 RID: 1031
	internal sealed class EndPointManager
	{
		// Token: 0x0600199A RID: 6554 RVA: 0x0006DB94 File Offset: 0x0006BD94
		public static void AddListener(HttpListener listener)
		{
			ArrayList arrayList = new ArrayList();
			try
			{
				Hashtable hashtable = EndPointManager.ip_to_endpoints;
				lock (hashtable)
				{
					foreach (string text in listener.Prefixes)
					{
						EndPointManager.AddPrefixInternal(text, listener);
						arrayList.Add(text);
					}
				}
			}
			catch
			{
				foreach (object obj in arrayList)
				{
					EndPointManager.RemovePrefix((string)obj, listener);
				}
				throw;
			}
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x0006DC74 File Offset: 0x0006BE74
		public static void AddPrefix(string prefix, HttpListener listener)
		{
			Hashtable hashtable = EndPointManager.ip_to_endpoints;
			lock (hashtable)
			{
				EndPointManager.AddPrefixInternal(prefix, listener);
			}
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0006DCB4 File Offset: 0x0006BEB4
		private static void AddPrefixInternal(string p, HttpListener listener)
		{
			ListenerPrefix listenerPrefix = new ListenerPrefix(p);
			if (listenerPrefix.Path.IndexOf('%') != -1)
			{
				throw new HttpListenerException(400, "Invalid path.");
			}
			if (listenerPrefix.Path.IndexOf("//", StringComparison.Ordinal) != -1)
			{
				throw new HttpListenerException(400, "Invalid path.");
			}
			EndPointManager.GetEPListener(listenerPrefix.Host, listenerPrefix.Port, listener, listenerPrefix.Secure).AddPrefix(listenerPrefix, listener);
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x0006DD2C File Offset: 0x0006BF2C
		private static EndPointListener GetEPListener(string host, int port, HttpListener listener, bool secure)
		{
			IPAddress ipaddress;
			if (host == "*")
			{
				ipaddress = IPAddress.Any;
			}
			else if (!IPAddress.TryParse(host, out ipaddress))
			{
				try
				{
					IPHostEntry hostByName = Dns.GetHostByName(host);
					if (hostByName != null)
					{
						ipaddress = hostByName.AddressList[0];
					}
					else
					{
						ipaddress = IPAddress.Any;
					}
				}
				catch
				{
					ipaddress = IPAddress.Any;
				}
			}
			Hashtable hashtable;
			if (EndPointManager.ip_to_endpoints.ContainsKey(ipaddress))
			{
				hashtable = (Hashtable)EndPointManager.ip_to_endpoints[ipaddress];
			}
			else
			{
				hashtable = new Hashtable();
				EndPointManager.ip_to_endpoints[ipaddress] = hashtable;
			}
			EndPointListener endPointListener;
			if (hashtable.ContainsKey(port))
			{
				endPointListener = (EndPointListener)hashtable[port];
			}
			else
			{
				endPointListener = new EndPointListener(listener, ipaddress, port, secure);
				hashtable[port] = endPointListener;
			}
			return endPointListener;
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x0006DE00 File Offset: 0x0006C000
		public static void RemoveEndPoint(EndPointListener epl, IPEndPoint ep)
		{
			Hashtable hashtable = EndPointManager.ip_to_endpoints;
			lock (hashtable)
			{
				Hashtable hashtable2 = (Hashtable)EndPointManager.ip_to_endpoints[ep.Address];
				hashtable2.Remove(ep.Port);
				if (hashtable2.Count == 0)
				{
					EndPointManager.ip_to_endpoints.Remove(ep.Address);
				}
				epl.Close();
			}
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x0006DE7C File Offset: 0x0006C07C
		public static void RemoveListener(HttpListener listener)
		{
			Hashtable hashtable = EndPointManager.ip_to_endpoints;
			lock (hashtable)
			{
				foreach (string text in listener.Prefixes)
				{
					EndPointManager.RemovePrefixInternal(text, listener);
				}
			}
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x0006DEF0 File Offset: 0x0006C0F0
		public static void RemovePrefix(string prefix, HttpListener listener)
		{
			Hashtable hashtable = EndPointManager.ip_to_endpoints;
			lock (hashtable)
			{
				EndPointManager.RemovePrefixInternal(prefix, listener);
			}
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x0006DF30 File Offset: 0x0006C130
		private static void RemovePrefixInternal(string prefix, HttpListener listener)
		{
			ListenerPrefix listenerPrefix = new ListenerPrefix(prefix);
			if (listenerPrefix.Path.IndexOf('%') != -1)
			{
				return;
			}
			if (listenerPrefix.Path.IndexOf("//", StringComparison.Ordinal) != -1)
			{
				return;
			}
			EndPointManager.GetEPListener(listenerPrefix.Host, listenerPrefix.Port, listener, listenerPrefix.Secure).RemovePrefix(listenerPrefix, listener);
		}

		// Token: 0x04001044 RID: 4164
		private static Hashtable ip_to_endpoints = new Hashtable();
	}
}
