using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200007A RID: 122
	public class RfcExtendedRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x06000405 RID: 1029 RVA: 0x00011C61 File Offset: 0x0000FE61
		public RfcExtendedRequest(RfcLdapOID requestName)
			: this(requestName, null)
		{
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00011C6B File Offset: 0x0000FE6B
		public RfcExtendedRequest(RfcLdapOID requestName, Asn1OctetString requestValue)
			: base(2)
		{
			base.add(new Asn1Tagged(new Asn1Identifier(2, false, 0), requestName, false));
			if (requestValue != null)
			{
				base.add(new Asn1Tagged(new Asn1Identifier(2, false, 1), requestValue, false));
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00011CA1 File Offset: 0x0000FEA1
		public RfcExtendedRequest(Asn1Object[] origRequest)
			: base(origRequest, origRequest.Length)
		{
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00011CAD File Offset: 0x0000FEAD
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 23);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00011CB8 File Offset: 0x0000FEB8
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcExtendedRequest(base.toArray());
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001169E File Offset: 0x0000F89E
		public string getRequestDN()
		{
			return null;
		}

		// Token: 0x04000257 RID: 599
		public const int REQUEST_NAME = 0;

		// Token: 0x04000258 RID: 600
		public const int REQUEST_VALUE = 1;
	}
}
