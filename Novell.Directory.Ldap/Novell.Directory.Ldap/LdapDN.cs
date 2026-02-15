using System;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200002C RID: 44
	public class LdapDN
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x000086A4 File Offset: 0x000068A4
		[CLSCompliant(false)]
		public static bool equals(string dn1, string dn2)
		{
			DN dn3 = new DN(dn1);
			DN dn4 = new DN(dn2);
			return dn3.Equals(dn4);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000086C4 File Offset: 0x000068C4
		public static string escapeRDN(string rdn)
		{
			StringBuilder stringBuilder = new StringBuilder(rdn);
			int i = 0;
			while (i < stringBuilder.Length && stringBuilder[i] != '=')
			{
				i++;
			}
			if (i == stringBuilder.Length)
			{
				throw new ArgumentException("Could not parse RDN: Attribute type and name must be separated by an equal symbol, '='");
			}
			i++;
			if (stringBuilder[i] == ' ' || stringBuilder[i] == '#')
			{
				stringBuilder.Insert(i++, '\\');
			}
			while (i < stringBuilder.Length)
			{
				if (stringBuilder[i] == ',' || stringBuilder[i] == '+' || stringBuilder[i] == '"' || stringBuilder[i] == '\\' || stringBuilder[i] == '<' || stringBuilder[i] == '>' || stringBuilder[i] == ';')
				{
					stringBuilder.Insert(i++, '\\');
				}
				i++;
			}
			if (stringBuilder[stringBuilder.Length - 1] == ' ')
			{
				stringBuilder.Insert(stringBuilder.Length - 1, '\\');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000087C3 File Offset: 0x000069C3
		public static string[] explodeDN(string dn, bool noTypes)
		{
			return new DN(dn).explodeDN(noTypes);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000087D1 File Offset: 0x000069D1
		public static string[] explodeRDN(string rdn, bool noTypes)
		{
			return new RDN(rdn).explodeRDN(noTypes);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000087E0 File Offset: 0x000069E0
		public static bool isValid(string dn)
		{
			try
			{
				new DN(dn);
			}
			catch (ArgumentException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00008810 File Offset: 0x00006A10
		public static string normalize(string dn)
		{
			return new DN(dn).ToString();
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00008820 File Offset: 0x00006A20
		public static string unescapeRDN(string rdn)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while (i < rdn.Length && rdn[i] != '=')
			{
				i++;
			}
			if (i == rdn.Length)
			{
				throw new ArgumentException("Could not parse rdn: Attribute type and name must be separated by an equal symbol, '='");
			}
			i++;
			if (rdn[i] == '\\' && i + 1 < rdn.Length - 1 && (rdn[i + 1] == ' ' || rdn[i + 1] == '#'))
			{
				i++;
			}
			while (i < rdn.Length)
			{
				if (rdn[i] != '\\' || i == rdn.Length - 1 || (rdn[i + 1] != ',' && rdn[i + 1] != '+' && rdn[i + 1] != '"' && rdn[i + 1] != '\\' && rdn[i + 1] != '<' && rdn[i + 1] != '>' && rdn[i + 1] != ';' && (rdn[i + 1] != ' ' || i + 2 != rdn.Length)))
				{
					stringBuilder.Append(rdn[i]);
				}
				i++;
			}
			return stringBuilder.ToString();
		}
	}
}
