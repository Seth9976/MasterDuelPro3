using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000095 RID: 149
	public class RfcSearchResultReference : Asn1SequenceOf
	{
		// Token: 0x06000495 RID: 1173 RVA: 0x00013DC7 File Offset: 0x00011FC7
		[CLSCompliant(false)]
		public RfcSearchResultReference(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00013EE0 File Offset: 0x000120E0
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 19);
		}
	}
}
