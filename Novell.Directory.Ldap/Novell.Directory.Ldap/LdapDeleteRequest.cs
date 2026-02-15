using System;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200002E RID: 46
	public class LdapDeleteRequest : LdapMessage
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00005D4D File Offset: 0x00003F4D
		public virtual string DN
		{
			get
			{
				return this.Asn1Object.RequestDN;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00008A3B File Offset: 0x00006C3B
		public LdapDeleteRequest(string dn, LdapControl[] cont)
			: base(10, new RfcDelRequest(dn), cont)
		{
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00004A19 File Offset: 0x00002C19
		public override string ToString()
		{
			return this.Asn1Object.ToString();
		}
	}
}
