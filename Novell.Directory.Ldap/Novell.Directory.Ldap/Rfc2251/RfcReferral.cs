using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200008D RID: 141
	public class RfcReferral : Asn1SequenceOf
	{
		// Token: 0x0600047F RID: 1151 RVA: 0x00013DC7 File Offset: 0x00011FC7
		[CLSCompliant(false)]
		public RfcReferral(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}
	}
}
