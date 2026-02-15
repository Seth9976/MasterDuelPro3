using System;
using System.Collections;
using System.Globalization;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000058 RID: 88
	public class DN
	{
		// Token: 0x06000343 RID: 835 RVA: 0x0000E17B File Offset: 0x0000C37B
		private void InitBlock()
		{
			this.rdnList = new ArrayList();
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000E188 File Offset: 0x0000C388
		public virtual ArrayList RDNs
		{
			get
			{
				int count = this.rdnList.Count;
				ArrayList arrayList = new ArrayList(count);
				for (int i = 0; i < count; i++)
				{
					arrayList.Add(this.rdnList[i]);
				}
				return arrayList;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000E1C8 File Offset: 0x0000C3C8
		public virtual DN Parent
		{
			get
			{
				DN dn = new DN();
				dn.rdnList = (ArrayList)this.rdnList.Clone();
				if (dn.rdnList.Count >= 1)
				{
					dn.rdnList.Remove(this.rdnList[0]);
				}
				return dn;
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000E217 File Offset: 0x0000C417
		public DN()
		{
			this.InitBlock();
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000E228 File Offset: 0x0000C428
		public DN(string dnString)
		{
			this.InitBlock();
			if (dnString.Length == 0)
			{
				return;
			}
			char[] array = new char[dnString.Length];
			int num = 0;
			string text = "";
			int num2 = 0;
			RDN rdn = new RDN();
			int num3 = 0;
			int i = 0;
			int num4 = 0;
			int num5 = 1;
			int num6 = dnString.Length - 1;
			while (i <= num6)
			{
				char c = dnString[i];
				switch (num5)
				{
				case 1:
					while (c == ' ' && i < num6)
					{
						c = dnString[++i];
					}
					if (this.isAlpha(c))
					{
						if (dnString.Substring(i).StartsWith("oid.") || dnString.Substring(i).StartsWith("OID."))
						{
							i += 4;
							if (i > num6)
							{
								throw new ArgumentException(dnString);
							}
							c = dnString[i];
							if (!this.isDigit(c))
							{
								throw new ArgumentException(dnString);
							}
							array[num3++] = c;
							num5 = 3;
						}
						else
						{
							array[num3++] = c;
							num5 = 2;
						}
					}
					else if (this.isDigit(c))
					{
						i--;
						num5 = 3;
					}
					else if (char.GetUnicodeCategory(c) != UnicodeCategory.SpaceSeparator)
					{
						throw new ArgumentException(dnString);
					}
					break;
				case 2:
					if (this.isAlpha(c) || this.isDigit(c) || c == '-')
					{
						array[num3++] = c;
					}
					else
					{
						while (c == ' ' && i < num6)
						{
							c = dnString[++i];
						}
						if (c != '=')
						{
							throw new ArgumentException(dnString);
						}
						text = new string(array, 0, num3);
						num3 = 0;
						num5 = 4;
					}
					break;
				case 3:
				{
					if (!this.isDigit(c))
					{
						throw new ArgumentException(dnString);
					}
					bool flag = c == '0';
					array[num3++] = c;
					c = dnString[++i];
					if ((this.isDigit(c) && flag) || (c == '.' && flag))
					{
						throw new ArgumentException(dnString);
					}
					while (this.isDigit(c) && i < num6)
					{
						array[num3++] = c;
						c = dnString[++i];
					}
					if (c == '.')
					{
						array[num3++] = c;
					}
					else
					{
						while (c == ' ' && i < num6)
						{
							c = dnString[++i];
						}
						if (c != '=')
						{
							throw new ArgumentException(dnString);
						}
						text = new string(array, 0, num3);
						num3 = 0;
						num5 = 4;
					}
					break;
				}
				case 4:
					while (c == ' ')
					{
						if (i >= num6)
						{
							throw new ArgumentException(dnString);
						}
						c = dnString[++i];
					}
					if (c == '"')
					{
						num5 = 5;
						num4 = i;
					}
					else if (c == '#')
					{
						num2 = 0;
						array[num3++] = c;
						num4 = i;
						num5 = 6;
					}
					else
					{
						num4 = i;
						i--;
						num5 = 7;
					}
					break;
				case 5:
					if (c == '"')
					{
						string text2 = dnString.Substring(num4, i + 1 - num4);
						if (i < num6)
						{
							c = dnString[++i];
						}
						while (c == ' ' && i < num6)
						{
							c = dnString[++i];
						}
						if (c != ',' && c != ';' && c != '+' && i != num6)
						{
							throw new ArgumentException(dnString);
						}
						string text3 = new string(array, 0, num3);
						rdn.add(text, text3, text2);
						if (c != '+')
						{
							this.rdnList.Add(rdn);
							rdn = new RDN();
						}
						num = 0;
						num3 = 0;
						num5 = 1;
					}
					else if (c == '\\')
					{
						c = dnString[++i];
						if (DN.isHexDigit(c))
						{
							char c2 = dnString[++i];
							if (!DN.isHexDigit(c2))
							{
								throw new ArgumentException(dnString);
							}
							array[num3++] = DN.hexToChar(c, c2);
							num = 0;
						}
						else
						{
							if (!this.needsEscape(c) && c != '#' && c != '=' && c != ' ')
							{
								throw new ArgumentException(dnString);
							}
							array[num3++] = c;
							num = 0;
						}
					}
					else
					{
						array[num3++] = c;
					}
					break;
				case 6:
					if (!DN.isHexDigit(c) || i > num6)
					{
						if (num2 % 2 != 0 || num2 == 0)
						{
							throw new ArgumentException(dnString);
						}
						string text2 = dnString.Substring(num4, i - num4);
						while (c == ' ' && i < num6)
						{
							c = dnString[++i];
						}
						if (c != ',' && c != ';' && c != '+' && i != num6)
						{
							throw new ArgumentException(dnString);
						}
						string text3 = new string(array, 0, num3);
						rdn.add(text, text3, text2);
						if (c != '+')
						{
							this.rdnList.Add(rdn);
							rdn = new RDN();
						}
						num3 = 0;
						num5 = 1;
					}
					else
					{
						array[num3++] = c;
						num2++;
					}
					break;
				case 7:
					if (c == '\\')
					{
						if (i >= num6)
						{
							throw new ArgumentException(dnString);
						}
						c = dnString[++i];
						if (DN.isHexDigit(c))
						{
							if (i >= num6)
							{
								throw new ArgumentException(dnString);
							}
							char c2 = dnString[++i];
							if (!DN.isHexDigit(c2))
							{
								throw new ArgumentException(dnString);
							}
							array[num3++] = DN.hexToChar(c, c2);
							num = 0;
						}
						else
						{
							if (!this.needsEscape(c) && c != '#' && c != '=' && c != ' ')
							{
								throw new ArgumentException(dnString);
							}
							array[num3++] = c;
							num = 0;
						}
					}
					else if (c == ' ')
					{
						num++;
						array[num3++] = c;
					}
					else if (c == ',' || c == ';' || c == '+')
					{
						string text3 = new string(array, 0, num3 - num);
						string text2 = dnString.Substring(num4, i - num - num4);
						rdn.add(text, text3, text2);
						if (c != '+')
						{
							this.rdnList.Add(rdn);
							rdn = new RDN();
						}
						num = 0;
						num3 = 0;
						num5 = 1;
					}
					else
					{
						if (this.needsEscape(c))
						{
							throw new ArgumentException(dnString);
						}
						num = 0;
						array[num3++] = c;
					}
					break;
				}
				i++;
			}
			if (num5 == 7 || (num5 == 6 && num2 % 2 == 0 && num2 != 0))
			{
				string text3 = new string(array, 0, num3 - num);
				string text2 = dnString.Substring(num4, i - num - num4);
				rdn.add(text, text3, text2);
				this.rdnList.Add(rdn);
				return;
			}
			if (num5 == 4)
			{
				string text3 = "";
				string text2 = dnString.Substring(num4);
				rdn.add(text, text3, text2);
				this.rdnList.Add(rdn);
				return;
			}
			throw new ArgumentException(dnString);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000E896 File Offset: 0x0000CA96
		private bool isAlpha(char ch)
		{
			return (ch < '[' && ch > '@') || (ch < '{' && ch > '`');
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000E8AF File Offset: 0x0000CAAF
		private bool isDigit(char ch)
		{
			return ch < ':' && ch > '/';
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000E8BE File Offset: 0x0000CABE
		private static bool isHexDigit(char ch)
		{
			return (ch < ':' && ch > '/') || (ch < 'G' && ch > '@') || (ch < 'g' && ch > '`');
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000E8E1 File Offset: 0x0000CAE1
		private bool needsEscape(char ch)
		{
			return ch == ',' || ch == '+' || ch == '"' || ch == ';' || ch == '<' || ch == '>' || ch == '\\';
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000E90C File Offset: 0x0000CB0C
		private static char hexToChar(char hex1, char hex0)
		{
			int num;
			if (hex1 < ':' && hex1 > '/')
			{
				num = (int)((hex1 - '0') * '\u0010');
			}
			else if (hex1 < 'G' && hex1 > '@')
			{
				num = (int)((hex1 - '7') * '\u0010');
			}
			else
			{
				if (hex1 >= 'g' || hex1 <= '`')
				{
					throw new ArgumentException("Not hex digit");
				}
				num = (int)((hex1 - 'W') * '\u0010');
			}
			if (hex0 < ':' && hex0 > '/')
			{
				num += (int)(hex0 - '0');
			}
			else if (hex0 < 'G' && hex0 > '@')
			{
				num += (int)(hex0 - '7');
			}
			else
			{
				if (hex0 >= 'g' || hex0 <= '`')
				{
					throw new ArgumentException("Not hex digit");
				}
				num += (int)(hex0 - 'W');
			}
			return (char)num;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000E9A8 File Offset: 0x0000CBA8
		public override string ToString()
		{
			int count = this.rdnList.Count;
			if (count < 1)
			{
				return null;
			}
			string text = this.rdnList[0].ToString();
			for (int i = 1; i < count; i++)
			{
				text = text + "," + this.rdnList[i].ToString();
			}
			return text;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000EA09 File Offset: 0x0000CC09
		public ArrayList getrdnList()
		{
			return this.rdnList;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000EA11 File Offset: 0x0000CC11
		public override bool Equals(object toDN)
		{
			return this.Equals((DN)toDN);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000EA20 File Offset: 0x0000CC20
		public bool Equals(DN toDN)
		{
			int count = toDN.getrdnList().Count;
			if (this.rdnList.Count != count)
			{
				return false;
			}
			for (int i = 0; i < count; i++)
			{
				if (!((RDN)this.rdnList[i]).equals((RDN)toDN.getrdnList()[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000EA84 File Offset: 0x0000CC84
		public virtual string[] explodeDN(bool noTypes)
		{
			int count = this.rdnList.Count;
			string[] array = new string[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = ((RDN)this.rdnList[i]).toString(noTypes);
			}
			return array;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000EACB File Offset: 0x0000CCCB
		public virtual int countRDNs()
		{
			return this.rdnList.Count;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000EAD8 File Offset: 0x0000CCD8
		public virtual bool isDescendantOf(DN containerDN)
		{
			int num = containerDN.rdnList.Count - 1;
			int num2 = this.rdnList.Count - 1;
			while (!((RDN)this.rdnList[num2]).equals((RDN)containerDN.rdnList[num]))
			{
				num2--;
				if (num2 <= 0)
				{
					return false;
				}
			}
			num--;
			num2--;
			while (num >= 0 && num2 >= 0)
			{
				if (!((RDN)this.rdnList[num2]).equals((RDN)containerDN.rdnList[num]))
				{
					return false;
				}
				num--;
				num2--;
			}
			return num2 != 0 || num != 0;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000EB84 File Offset: 0x0000CD84
		public virtual void addRDN(RDN rdn)
		{
			this.rdnList.Insert(0, rdn);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000EB84 File Offset: 0x0000CD84
		public virtual void addRDNToFront(RDN rdn)
		{
			this.rdnList.Insert(0, rdn);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000EB93 File Offset: 0x0000CD93
		public virtual void addRDNToBack(RDN rdn)
		{
			this.rdnList.Add(rdn);
		}

		// Token: 0x040001CB RID: 459
		private const int LOOK_FOR_RDN_ATTR_TYPE = 1;

		// Token: 0x040001CC RID: 460
		private const int ALPHA_ATTR_TYPE = 2;

		// Token: 0x040001CD RID: 461
		private const int OID_ATTR_TYPE = 3;

		// Token: 0x040001CE RID: 462
		private const int LOOK_FOR_RDN_VALUE = 4;

		// Token: 0x040001CF RID: 463
		private const int QUOTED_RDN_VALUE = 5;

		// Token: 0x040001D0 RID: 464
		private const int HEX_RDN_VALUE = 6;

		// Token: 0x040001D1 RID: 465
		private const int UNQUOTED_RDN_VALUE = 7;

		// Token: 0x040001D2 RID: 466
		private ArrayList rdnList;
	}
}
