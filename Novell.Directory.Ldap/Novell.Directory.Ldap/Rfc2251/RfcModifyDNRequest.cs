using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000089 RID: 137
	public class RfcModifyDNRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x0600046D RID: 1133 RVA: 0x00013D2E File Offset: 0x00011F2E
		public RfcModifyDNRequest(RfcLdapDN entry, RfcRelativeLdapDN newrdn, Asn1Boolean deleteoldrdn)
			: this(entry, newrdn, deleteoldrdn, null)
		{
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00013D3A File Offset: 0x00011F3A
		public RfcModifyDNRequest(RfcLdapDN entry, RfcRelativeLdapDN newrdn, Asn1Boolean deleteoldrdn, RfcLdapSuperDN newSuperior)
			: base(4)
		{
			base.add(entry);
			base.add(newrdn);
			base.add(deleteoldrdn);
			if (newSuperior != null)
			{
				newSuperior.setIdentifier(new Asn1Identifier(2, false, 0));
				base.add(newSuperior);
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x000116C6 File Offset: 0x0000F8C6
		internal RfcModifyDNRequest(Asn1Object[] origRequest, string base_Renamed)
			: base(origRequest, origRequest.Length)
		{
			if (base_Renamed != null)
			{
				base.set_Renamed(0, new RfcLdapDN(base_Renamed));
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00013D73 File Offset: 0x00011F73
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 12);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00013D7E File Offset: 0x00011F7E
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcModifyDNRequest(base.toArray(), base_Renamed);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000116FA File Offset: 0x0000F8FA
		public string getRequestDN()
		{
			return ((RfcLdapDN)base.get_Renamed(0)).stringValue();
		}
	}
}
