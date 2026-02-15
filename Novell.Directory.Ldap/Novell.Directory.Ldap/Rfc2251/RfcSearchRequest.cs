using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000092 RID: 146
	public class RfcSearchRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x06000489 RID: 1161 RVA: 0x00013DF8 File Offset: 0x00011FF8
		public RfcSearchRequest(RfcLdapDN baseObject, Asn1Enumerated scope, Asn1Enumerated derefAliases, Asn1Integer sizeLimit, Asn1Integer timeLimit, Asn1Boolean typesOnly, RfcFilter filter, RfcAttributeDescriptionList attributes)
			: base(8)
		{
			base.add(baseObject);
			base.add(scope);
			base.add(derefAliases);
			base.add(sizeLimit);
			base.add(timeLimit);
			base.add(typesOnly);
			base.add(filter);
			base.add(attributes);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00013E4C File Offset: 0x0001204C
		internal RfcSearchRequest(Asn1Object[] origRequest, string base_Renamed, string filter, bool request)
			: base(origRequest, origRequest.Length)
		{
			if (base_Renamed != null)
			{
				base.set_Renamed(0, new RfcLdapDN(base_Renamed));
			}
			if (request && ((Asn1Enumerated)origRequest[1]).intValue() == 1)
			{
				base.set_Renamed(1, new Asn1Enumerated(0));
			}
			if (filter != null)
			{
				base.set_Renamed(6, new RfcFilter(filter));
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00013EA4 File Offset: 0x000120A4
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 3);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00013EAE File Offset: 0x000120AE
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcSearchRequest(base.toArray(), base_Renamed, filter, request);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000116FA File Offset: 0x0000F8FA
		public string getRequestDN()
		{
			return ((RfcLdapDN)base.get_Renamed(0)).stringValue();
		}
	}
}
