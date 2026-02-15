using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000EF RID: 239
	public class Asn1Enumerated : Asn1Numeric
	{
		// Token: 0x060005E6 RID: 1510 RVA: 0x000189B9 File Offset: 0x00016BB9
		public Asn1Enumerated(int content)
			: base(Asn1Enumerated.ID, content)
		{
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000189C7 File Offset: 0x00016BC7
		public Asn1Enumerated(long content)
			: base(Asn1Enumerated.ID, content)
		{
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000189D5 File Offset: 0x00016BD5
		[CLSCompliant(false)]
		public Asn1Enumerated(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(Asn1Enumerated.ID, (long)dec.decodeNumeric(in_Renamed, len))
		{
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x000189EF File Offset: 0x00016BEF
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x000189FC File Offset: 0x00016BFC
		public override string ToString()
		{
			return base.ToString() + "ENUMERATED: " + base.longValue().ToString();
		}

		// Token: 0x040004DC RID: 1244
		public const int TAG = 10;

		// Token: 0x040004DD RID: 1245
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, false, 10);
	}
}
