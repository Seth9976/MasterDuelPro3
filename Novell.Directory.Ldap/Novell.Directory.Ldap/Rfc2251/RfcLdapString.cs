using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000084 RID: 132
	public class RfcLdapString : Asn1OctetString
	{
		// Token: 0x0600045C RID: 1116 RVA: 0x00011793 File Offset: 0x0000F993
		public RfcLdapString(string s)
			: base(s)
		{
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00011730 File Offset: 0x0000F930
		[CLSCompliant(false)]
		public RfcLdapString(sbyte[] ba)
			: base(ba)
		{
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00013B08 File Offset: 0x00011D08
		[CLSCompliant(false)]
		public RfcLdapString(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}
	}
}
