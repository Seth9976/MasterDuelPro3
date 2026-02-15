using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000F1 RID: 241
	public class Asn1Integer : Asn1Numeric
	{
		// Token: 0x060005FA RID: 1530 RVA: 0x00018BFC File Offset: 0x00016DFC
		public Asn1Integer(int content)
			: base(Asn1Integer.ID, content)
		{
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00018C0A File Offset: 0x00016E0A
		public Asn1Integer(long content)
			: base(Asn1Integer.ID, content)
		{
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00018C18 File Offset: 0x00016E18
		[CLSCompliant(false)]
		public Asn1Integer(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(Asn1Integer.ID, (long)dec.decodeNumeric(in_Renamed, len))
		{
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000189EF File Offset: 0x00016BEF
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00018C34 File Offset: 0x00016E34
		public override string ToString()
		{
			return base.ToString() + "INTEGER: " + base.longValue().ToString();
		}

		// Token: 0x040004E6 RID: 1254
		public const int TAG = 2;

		// Token: 0x040004E7 RID: 1255
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, false, 2);
	}
}
