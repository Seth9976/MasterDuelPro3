using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000068 RID: 104
	public class RfcAddRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003BB RID: 955 RVA: 0x000116A1 File Offset: 0x0000F8A1
		public virtual RfcAttributeList Attributes
		{
			get
			{
				return (RfcAttributeList)base.get_Renamed(1);
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000116AF File Offset: 0x0000F8AF
		public RfcAddRequest(RfcLdapDN entry, RfcAttributeList attributes)
			: base(2)
		{
			base.add(entry);
			base.add(attributes);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000116C6 File Offset: 0x0000F8C6
		internal RfcAddRequest(Asn1Object[] origRequest, string base_Renamed)
			: base(origRequest, origRequest.Length)
		{
			if (base_Renamed != null)
			{
				base.set_Renamed(0, new RfcLdapDN(base_Renamed));
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000116E2 File Offset: 0x0000F8E2
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 8);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000116EC File Offset: 0x0000F8EC
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcAddRequest(base.toArray(), base_Renamed);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000116FA File Offset: 0x0000F8FA
		public string getRequestDN()
		{
			return ((RfcLdapDN)base.get_Renamed(0)).stringValue();
		}
	}
}
