using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200003B RID: 59
	public class LdapModifyDNRequest : LdapMessage
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00005D4D File Offset: 0x00003F4D
		public virtual string DN
		{
			get
			{
				return this.Asn1Object.RequestDN;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00009A99 File Offset: 0x00007C99
		public virtual string NewRDN
		{
			get
			{
				return ((RfcRelativeLdapDN)((RfcModifyDNRequest)this.Asn1Object.getRequest()).toArray()[1]).stringValue();
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00009ABC File Offset: 0x00007CBC
		public virtual bool DeleteOldRDN
		{
			get
			{
				return ((Asn1Boolean)((RfcModifyDNRequest)this.Asn1Object.getRequest()).toArray()[2]).booleanValue();
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00009AE0 File Offset: 0x00007CE0
		public virtual string ParentDN
		{
			get
			{
				RfcModifyDNRequest rfcModifyDNRequest = (RfcModifyDNRequest)this.Asn1Object.getRequest();
				Asn1Object[] array = rfcModifyDNRequest.toArray();
				if (array.Length < 4 || array[3] == null)
				{
					return null;
				}
				return ((RfcLdapDN)rfcModifyDNRequest.toArray()[3]).stringValue();
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00009B24 File Offset: 0x00007D24
		public LdapModifyDNRequest(string dn, string newRdn, string newParentdn, bool deleteOldRdn, LdapControl[] cont)
			: base(12, new RfcModifyDNRequest(new RfcLdapDN(dn), new RfcRelativeLdapDN(newRdn), new Asn1Boolean(deleteOldRdn), (newParentdn != null) ? new RfcLdapSuperDN(newParentdn) : null), cont)
		{
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00004A19 File Offset: 0x00002C19
		public override string ToString()
		{
			return this.Asn1Object.ToString();
		}
	}
}
