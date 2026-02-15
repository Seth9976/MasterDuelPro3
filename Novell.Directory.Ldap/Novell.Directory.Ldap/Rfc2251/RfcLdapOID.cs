using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000082 RID: 130
	public class RfcLdapOID : Asn1OctetString
	{
		// Token: 0x06000453 RID: 1107 RVA: 0x00011793 File Offset: 0x0000F993
		public RfcLdapOID(string s)
			: base(s)
		{
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00011730 File Offset: 0x0000F930
		[CLSCompliant(false)]
		public RfcLdapOID(sbyte[] s)
			: base(s)
		{
		}
	}
}
