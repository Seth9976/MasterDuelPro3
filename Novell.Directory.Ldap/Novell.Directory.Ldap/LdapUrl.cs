using System;
using System.Collections;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200004E RID: 78
	public class LdapUrl : ICloneable
	{
		// Token: 0x060002D3 RID: 723 RVA: 0x0000C111 File Offset: 0x0000A311
		private void InitBlock()
		{
			this.scope = LdapUrl.DEFAULT_SCOPE;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000C11E File Offset: 0x0000A31E
		public virtual string[] AttributeArray
		{
			get
			{
				return this.attrs;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000C128 File Offset: 0x0000A328
		public virtual IEnumerator Attributes
		{
			get
			{
				object[] array = this.attrs;
				return new ArrayEnumeration(array);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000C142 File Offset: 0x0000A342
		public virtual string[] Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000C14A File Offset: 0x0000A34A
		public virtual string Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000C152 File Offset: 0x0000A352
		public virtual string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000C15A File Offset: 0x0000A35A
		public virtual int Port
		{
			get
			{
				if (this.port == 0)
				{
					return 389;
				}
				return this.port;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000C170 File Offset: 0x0000A370
		public virtual int Scope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000C178 File Offset: 0x0000A378
		public virtual bool Secure
		{
			get
			{
				return this.secure;
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000C180 File Offset: 0x0000A380
		public LdapUrl(string url)
		{
			this.InitBlock();
			this.parseURL(url);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000C195 File Offset: 0x0000A395
		public LdapUrl(string host, int port, string dn)
		{
			this.InitBlock();
			this.host = host;
			this.port = port;
			this.dn = dn;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000C1B8 File Offset: 0x0000A3B8
		public LdapUrl(string host, int port, string dn, string[] attrNames, int scope, string filter, string[] extensions)
		{
			this.InitBlock();
			this.host = host;
			this.port = port;
			this.dn = dn;
			this.attrs = new string[attrNames.Length];
			attrNames.CopyTo(this.attrs, 0);
			this.scope = scope;
			this.filter = filter;
			this.extensions = new string[extensions.Length];
			extensions.CopyTo(this.extensions, 0);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000C230 File Offset: 0x0000A430
		public LdapUrl(string host, int port, string dn, string[] attrNames, int scope, string filter, string[] extensions, bool secure)
		{
			this.InitBlock();
			this.host = host;
			this.port = port;
			this.dn = dn;
			this.attrs = attrNames;
			this.scope = scope;
			this.filter = filter;
			this.extensions = new string[extensions.Length];
			extensions.CopyTo(this.extensions, 0);
			this.secure = secure;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000C29C File Offset: 0x0000A49C
		public object Clone()
		{
			object obj;
			try
			{
				obj = base.MemberwiseClone();
			}
			catch (Exception)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			return obj;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000C2D0 File Offset: 0x0000A4D0
		public static string decode(string URLEncoded)
		{
			int num = 0;
			int i = URLEncoded.IndexOf("%", num);
			if (i < 0)
			{
				return URLEncoded;
			}
			int num2 = 0;
			int length = URLEncoded.Length;
			StringBuilder stringBuilder = new StringBuilder(length);
			while (i <= length - 3)
			{
				if (i < 0)
				{
					i = length;
				}
				stringBuilder.Append(URLEncoded.Substring(num2, i - num2));
				i++;
				if (i < length)
				{
					num2 = i + 2;
					try
					{
						stringBuilder.Append((char)Convert.ToInt32(URLEncoded.Substring(i, num2 - i), 16));
					}
					catch (FormatException ex)
					{
						throw new UriFormatException("LdapUrl.decode: error converting hex characters to integer \"" + ex.Message + "\"");
					}
					num = num2;
					if (num != length)
					{
						i = URLEncoded.IndexOf("%", num);
						continue;
					}
				}
				return stringBuilder.ToString();
			}
			throw new UriFormatException("LdapUrl.decode: must be two hex characters following escape character '%'");
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000C3A4 File Offset: 0x0000A5A4
		public static string encode(string toEncode)
		{
			StringBuilder stringBuilder = new StringBuilder(toEncode.Length);
			foreach (char c in toEncode)
			{
				if (c <= '\u001f' || c == '\u007f' || (c >= '\u0080' && c <= 'ÿ') || c == '<' || c == '>' || c == '"' || c == '#' || c == '%' || c == '{' || c == '}' || c == '|' || c == '\\' || c == '^' || c == '~' || c == '[' || c == '\'' || c == ';' || c == '/' || c == '?' || c == ':' || c == '@' || c == '=' || c == '&')
				{
					string text = Convert.ToString((int)c, 16);
					if (text.Length == 1)
					{
						stringBuilder.Append("%0" + text);
					}
					else
					{
						stringBuilder.Append("%" + Convert.ToString((int)c, 16));
					}
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000C4A9 File Offset: 0x0000A6A9
		public virtual string getDN()
		{
			return this.dn;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000C4B1 File Offset: 0x0000A6B1
		internal virtual void setDN(string dn)
		{
			this.dn = dn;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000C4BC File Offset: 0x0000A6BC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			if (this.secure)
			{
				stringBuilder.Append("ldaps://");
			}
			else
			{
				stringBuilder.Append("ldap://");
			}
			if (this.ipV6)
			{
				stringBuilder.Append("[" + this.host + "]");
			}
			else
			{
				stringBuilder.Append(this.host);
			}
			if (this.port != 0)
			{
				stringBuilder.Append(":" + this.port.ToString());
			}
			if (this.dn == null && this.attrs == null && this.scope == LdapUrl.DEFAULT_SCOPE && this.filter == null && this.extensions == null)
			{
				return stringBuilder.ToString();
			}
			stringBuilder.Append("/");
			if (this.dn != null)
			{
				stringBuilder.Append(this.dn);
			}
			if (this.attrs == null && this.scope == LdapUrl.DEFAULT_SCOPE && this.filter == null && this.extensions == null)
			{
				return stringBuilder.ToString();
			}
			stringBuilder.Append("?");
			if (this.attrs != null)
			{
				for (int i = 0; i < this.attrs.Length; i++)
				{
					stringBuilder.Append(this.attrs[i]);
					if (i < this.attrs.Length - 1)
					{
						stringBuilder.Append(",");
					}
				}
			}
			if (this.scope == LdapUrl.DEFAULT_SCOPE && this.filter == null && this.extensions == null)
			{
				return stringBuilder.ToString();
			}
			stringBuilder.Append("?");
			if (this.scope != LdapUrl.DEFAULT_SCOPE)
			{
				if (this.scope == 1)
				{
					stringBuilder.Append("one");
				}
				else
				{
					stringBuilder.Append("sub");
				}
			}
			if (this.filter == null && this.extensions == null)
			{
				return stringBuilder.ToString();
			}
			if (this.filter == null)
			{
				stringBuilder.Append("?");
			}
			else
			{
				stringBuilder.Append("?" + this.Filter);
			}
			if (this.extensions == null)
			{
				return stringBuilder.ToString();
			}
			stringBuilder.Append("?");
			if (this.extensions != null)
			{
				for (int j = 0; j < this.extensions.Length; j++)
				{
					stringBuilder.Append(this.extensions[j]);
					if (j < this.extensions.Length - 1)
					{
						stringBuilder.Append(",");
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000C728 File Offset: 0x0000A928
		private string[] parseList(string listStr, char delimiter, int listStart, int listEnd)
		{
			if (listEnd - listStart < 1)
			{
				return null;
			}
			int i = listStart;
			int num = 0;
			while (i > 0)
			{
				num++;
				int num2 = listStr.IndexOf(delimiter, i);
				if (num2 <= 0 || num2 >= listEnd)
				{
					break;
				}
				i = num2 + 1;
			}
			i = listStart;
			string[] array = new string[num];
			num = 0;
			while (i > 0)
			{
				int num2 = listStr.IndexOf(delimiter, i);
				if (i > listEnd)
				{
					break;
				}
				if (num2 < 0)
				{
					num2 = listEnd;
				}
				if (num2 > listEnd)
				{
					num2 = listEnd;
				}
				array[num] = listStr.Substring(i, num2 - i);
				i = num2 + 1;
				num++;
			}
			return array;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
		private void parseURL(string url)
		{
			int num = 0;
			int num2 = url.Length;
			if (url == null)
			{
				throw new UriFormatException("LdapUrl: URL cannot be null");
			}
			if (url[num] == '<')
			{
				if (url[num2 - 1] != '>')
				{
					throw new UriFormatException("LdapUrl: URL bad enclosure");
				}
				num++;
				num2--;
			}
			if (url.Substring(num, num + 4 - num).ToUpper().Equals("URL:".ToUpper()))
			{
				num += 4;
			}
			if (url.Substring(num, num + 7 - num).ToUpper().Equals("ldap://".ToUpper()))
			{
				num += 7;
				this.port = 389;
			}
			else
			{
				if (!url.Substring(num, num + 8 - num).ToUpper().Equals("ldaps://".ToUpper()))
				{
					throw new UriFormatException("LdapUrl: URL scheme is not ldap");
				}
				this.secure = true;
				num += 8;
				this.port = 636;
			}
			int num3 = url.IndexOf("/", num);
			int num4 = num2;
			bool flag = false;
			if (num3 < 0)
			{
				num3 = url.IndexOf("?", num);
				if (num3 > 0)
				{
					if (url[num3 + 1] == '?')
					{
						num4 = num3;
						num3++;
						flag = true;
					}
					else
					{
						num3 = -1;
					}
				}
			}
			else
			{
				num4 = num3;
			}
			if (url[num] == '[')
			{
				int num5 = url.IndexOf(']', num + 1);
				if (num5 >= num4 || num5 == -1)
				{
					throw new UriFormatException("LdapUrl: \"]\" is missing on IPV6 host name");
				}
				this.host = url.Substring(num + 1, num5 - (num + 1));
				int num6 = url.IndexOf(":", num5);
				if (num6 < num4 && num6 != -1)
				{
					this.port = int.Parse(url.Substring(num6 + 1, num4 - (num6 + 1)));
				}
			}
			else
			{
				int num6 = url.IndexOf(":", num);
				if (num6 < 0 || num6 > num4)
				{
					this.host = url.Substring(num, num4 - num);
				}
				else
				{
					this.host = url.Substring(num, num6 - num);
					this.port = int.Parse(url.Substring(num6 + 1, num4 - (num6 + 1)));
				}
			}
			num = num4 + 1;
			if (num >= num2 || num3 < 0)
			{
				return;
			}
			num = num3 + 1;
			int num7 = url.IndexOf('?', num);
			if (num7 < 0)
			{
				this.dn = url.Substring(num, num2 - num);
			}
			else
			{
				this.dn = url.Substring(num, num7 - num);
			}
			num = num7 + 1;
			if (num >= num2 || num7 < 0 || flag)
			{
				return;
			}
			int num8 = url.IndexOf('?', num);
			if (num8 < 0)
			{
				num8 = num2 - 1;
			}
			this.attrs = this.parseList(url, ',', num7 + 1, num8);
			num = num8 + 1;
			if (num >= num2)
			{
				return;
			}
			int num9 = url.IndexOf('?', num);
			string text;
			if (num9 < 0)
			{
				text = url.Substring(num, num2 - num);
			}
			else
			{
				text = url.Substring(num, num9 - num);
			}
			if (text.ToUpper().Equals("".ToUpper()))
			{
				this.scope = 0;
			}
			else if (text.ToUpper().Equals("base".ToUpper()))
			{
				this.scope = 0;
			}
			else if (text.ToUpper().Equals("one".ToUpper()))
			{
				this.scope = 1;
			}
			else
			{
				if (!text.ToUpper().Equals("sub".ToUpper()))
				{
					throw new UriFormatException("LdapUrl: URL invalid scope");
				}
				this.scope = 2;
			}
			num = num9 + 1;
			if (num >= num2 || num9 < 0)
			{
				return;
			}
			num = num9 + 1;
			int num10 = url.IndexOf('?', num);
			string text2;
			if (num10 < 0)
			{
				text2 = url.Substring(num, num2 - num);
			}
			else
			{
				text2 = url.Substring(num, num10 - num);
			}
			if (!text2.Equals(""))
			{
				this.filter = text2;
			}
			num = num10 + 1;
			if (num >= num2 || num10 < 0)
			{
				return;
			}
			if (url.IndexOf('?', num) > 0)
			{
				throw new UriFormatException("LdapUrl: URL has too many ? fields");
			}
			this.extensions = this.parseList(url, ',', num, num2);
		}

		// Token: 0x04000195 RID: 405
		private static readonly int DEFAULT_SCOPE;

		// Token: 0x04000196 RID: 406
		private bool secure;

		// Token: 0x04000197 RID: 407
		private bool ipV6;

		// Token: 0x04000198 RID: 408
		private string host;

		// Token: 0x04000199 RID: 409
		private int port;

		// Token: 0x0400019A RID: 410
		private string dn;

		// Token: 0x0400019B RID: 411
		private string[] attrs;

		// Token: 0x0400019C RID: 412
		private string filter;

		// Token: 0x0400019D RID: 413
		private int scope;

		// Token: 0x0400019E RID: 414
		private string[] extensions;
	}
}
