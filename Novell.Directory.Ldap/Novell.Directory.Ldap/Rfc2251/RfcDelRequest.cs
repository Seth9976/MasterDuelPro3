using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000078 RID: 120
	public class RfcDelRequest : RfcLdapDN, RfcRequest
	{
		// Token: 0x060003FD RID: 1021 RVA: 0x00011C1A File Offset: 0x0000FE1A
		public RfcDelRequest(string dn)
			: base(dn)
		{
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00011C23 File Offset: 0x0000FE23
		[CLSCompliant(false)]
		public RfcDelRequest(sbyte[] dn)
			: base(dn)
		{
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00011C2C File Offset: 0x0000FE2C
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, false, 10);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00011C37 File Offset: 0x0000FE37
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			if (base_Renamed == null)
			{
				return new RfcDelRequest(base.byteValue());
			}
			return new RfcDelRequest(base_Renamed);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00011C4E File Offset: 0x0000FE4E
		public string getRequestDN()
		{
			return base.stringValue();
		}
	}
}
