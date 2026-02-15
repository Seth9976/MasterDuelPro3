using System;
using System.IO;
using System.Runtime.Serialization;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000ED RID: 237
	[CLSCompliant(false)]
	public interface Asn1Decoder : ISerializable
	{
		// Token: 0x060005D8 RID: 1496
		Asn1Object decode(sbyte[] value_Renamed);

		// Token: 0x060005D9 RID: 1497
		Asn1Object decode(Stream in_Renamed);

		// Token: 0x060005DA RID: 1498
		Asn1Object decode(Stream in_Renamed, int[] length);

		// Token: 0x060005DB RID: 1499
		object decodeBoolean(Stream in_Renamed, int len);

		// Token: 0x060005DC RID: 1500
		object decodeNumeric(Stream in_Renamed, int len);

		// Token: 0x060005DD RID: 1501
		object decodeOctetString(Stream in_Renamed, int len);

		// Token: 0x060005DE RID: 1502
		object decodeCharacterString(Stream in_Renamed, int len);
	}
}
