using System;

namespace System.Net
{
	// Token: 0x02000420 RID: 1056
	internal sealed class ListenerPrefix
	{
		// Token: 0x06001A95 RID: 6805 RVA: 0x0007382F File Offset: 0x00071A2F
		public ListenerPrefix(string prefix)
		{
			this.original = prefix;
			this.Parse(prefix);
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x00073845 File Offset: 0x00071A45
		public override string ToString()
		{
			return this.original;
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x0007384D File Offset: 0x00071A4D
		public bool Secure
		{
			get
			{
				return this.secure;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001A98 RID: 6808 RVA: 0x00073855 File Offset: 0x00071A55
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x0007385D File Offset: 0x00071A5D
		public int Port
		{
			get
			{
				return (int)this.port;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x00073865 File Offset: 0x00071A65
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x00073870 File Offset: 0x00071A70
		public override bool Equals(object o)
		{
			ListenerPrefix listenerPrefix = o as ListenerPrefix;
			return listenerPrefix != null && this.original == listenerPrefix.original;
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x0007389A File Offset: 0x00071A9A
		public override int GetHashCode()
		{
			return this.original.GetHashCode();
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x000738A8 File Offset: 0x00071AA8
		private void Parse(string uri)
		{
			ushort num = 80;
			if (uri.StartsWith("https://"))
			{
				num = 443;
				this.secure = true;
			}
			int length = uri.Length;
			int num2 = uri.IndexOf(':') + 3;
			if (num2 >= length)
			{
				throw new ArgumentException("No host specified.");
			}
			int num3 = uri.IndexOf(':', num2, length - num2);
			if (uri[num2] == '[')
			{
				num3 = uri.IndexOf("]:") + 1;
			}
			if (num2 == num3)
			{
				throw new ArgumentException("No host specified.");
			}
			int num4 = uri.IndexOf('/', num2, length - num2);
			if (num4 == -1)
			{
				throw new ArgumentException("No path specified.");
			}
			if (num3 > 0)
			{
				this.host = uri.Substring(num2, num3 - num2).Trim(new char[] { '[', ']' });
				this.port = ushort.Parse(uri.Substring(num3 + 1, num4 - num3 - 1));
			}
			else
			{
				this.host = uri.Substring(num2, num4 - num2).Trim(new char[] { '[', ']' });
				this.port = num;
			}
			this.path = uri.Substring(num4);
			if (this.path.Length != 1)
			{
				this.path = this.path.Substring(0, this.path.Length - 1);
			}
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x000739F4 File Offset: 0x00071BF4
		public static void CheckUri(string uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uriPrefix");
			}
			if (!uri.StartsWith("http://") && !uri.StartsWith("https://"))
			{
				throw new ArgumentException("Only 'http' and 'https' schemes are supported.");
			}
			int length = uri.Length;
			int num = uri.IndexOf(':') + 3;
			if (num >= length)
			{
				throw new ArgumentException("No host specified.");
			}
			int num2 = uri.IndexOf(':', num, length - num);
			if (uri[num] == '[')
			{
				num2 = uri.IndexOf("]:") + 1;
			}
			if (num == num2)
			{
				throw new ArgumentException("No host specified.");
			}
			int num3 = uri.IndexOf('/', num, length - num);
			if (num3 == -1)
			{
				throw new ArgumentException("No path specified.");
			}
			if (num2 > 0)
			{
				try
				{
					int num4 = int.Parse(uri.Substring(num2 + 1, num3 - num2 - 1));
					if (num4 <= 0 || num4 >= 65536)
					{
						throw new Exception();
					}
				}
				catch
				{
					throw new ArgumentException("Invalid port.");
				}
			}
			if (uri[uri.Length - 1] != '/')
			{
				throw new ArgumentException("The prefix must end with '/'");
			}
		}

		// Token: 0x0400113F RID: 4415
		private string original;

		// Token: 0x04001140 RID: 4416
		private string host;

		// Token: 0x04001141 RID: 4417
		private ushort port;

		// Token: 0x04001142 RID: 4418
		private string path;

		// Token: 0x04001143 RID: 4419
		private bool secure;

		// Token: 0x04001144 RID: 4420
		public HttpListener Listener;
	}
}
