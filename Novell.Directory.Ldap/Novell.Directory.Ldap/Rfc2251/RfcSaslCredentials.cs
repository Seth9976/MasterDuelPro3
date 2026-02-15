using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000091 RID: 145
	public class RfcSaslCredentials : Asn1Sequence
	{
		// Token: 0x06000487 RID: 1159 RVA: 0x00013DD2 File Offset: 0x00011FD2
		public RfcSaslCredentials(RfcLdapString mechanism)
			: this(mechanism, null)
		{
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00013DDC File Offset: 0x00011FDC
		public RfcSaslCredentials(RfcLdapString mechanism, Asn1OctetString credentials)
			: base(2)
		{
			base.add(mechanism);
			if (credentials != null)
			{
				base.add(credentials);
			}
		}
	}
}
