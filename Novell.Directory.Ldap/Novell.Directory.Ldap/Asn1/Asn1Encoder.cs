using System;
using System.IO;
using System.Runtime.Serialization;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000EE RID: 238
	public interface Asn1Encoder : ISerializable
	{
		// Token: 0x060005DF RID: 1503
		void encode(Asn1Boolean b, Stream out_Renamed);

		// Token: 0x060005E0 RID: 1504
		void encode(Asn1Numeric n, Stream out_Renamed);

		// Token: 0x060005E1 RID: 1505
		void encode(Asn1Null n, Stream out_Renamed);

		// Token: 0x060005E2 RID: 1506
		void encode(Asn1OctetString os, Stream out_Renamed);

		// Token: 0x060005E3 RID: 1507
		void encode(Asn1Structured c, Stream out_Renamed);

		// Token: 0x060005E4 RID: 1508
		void encode(Asn1Tagged t, Stream out_Renamed);

		// Token: 0x060005E5 RID: 1509
		void encode(Asn1Identifier id, Stream out_Renamed);
	}
}
