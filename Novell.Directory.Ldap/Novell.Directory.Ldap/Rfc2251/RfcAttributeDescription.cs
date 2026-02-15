using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200006B RID: 107
	public class RfcAttributeDescription : RfcLdapString
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x00011739 File Offset: 0x0000F939
		public RfcAttributeDescription(string s)
			: base(s)
		{
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00011742 File Offset: 0x0000F942
		[CLSCompliant(false)]
		public RfcAttributeDescription(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}
	}
}
