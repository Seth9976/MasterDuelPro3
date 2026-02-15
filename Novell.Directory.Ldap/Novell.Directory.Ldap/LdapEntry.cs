using System;
using System.Text;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200002F RID: 47
	public class LdapEntry : IComparable
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00008A4C File Offset: 0x00006C4C
		[CLSCompliant(false)]
		public virtual string DN
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00008A54 File Offset: 0x00006C54
		public LdapEntry()
			: this(null, null)
		{
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00008A5E File Offset: 0x00006C5E
		public LdapEntry(string dn)
			: this(dn, null)
		{
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00008A68 File Offset: 0x00006C68
		public LdapEntry(string dn, LdapAttributeSet attrs)
		{
			if (dn == null)
			{
				dn = "";
			}
			if (attrs == null)
			{
				attrs = new LdapAttributeSet();
			}
			this.dn = dn;
			this.attrs = attrs;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00008A92 File Offset: 0x00006C92
		public virtual LdapAttribute getAttribute(string attrName)
		{
			return this.attrs.getAttribute(attrName);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00008AA0 File Offset: 0x00006CA0
		public virtual LdapAttributeSet getAttributeSet()
		{
			return this.attrs;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00008AA8 File Offset: 0x00006CA8
		public virtual LdapAttributeSet getAttributeSet(string subtype)
		{
			return this.attrs.getSubset(subtype);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00008AB6 File Offset: 0x00006CB6
		public virtual int CompareTo(object entry)
		{
			return LdapDN.normalize(this.dn).CompareTo(LdapDN.normalize(((LdapEntry)entry).dn));
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00008AD8 File Offset: 0x00006CD8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("LdapEntry: ");
			if (this.dn != null)
			{
				stringBuilder.Append(this.dn + "; ");
			}
			if (this.attrs != null)
			{
				stringBuilder.Append(this.attrs.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000DA RID: 218
		protected internal string dn;

		// Token: 0x040000DB RID: 219
		protected internal LdapAttributeSet attrs;
	}
}
