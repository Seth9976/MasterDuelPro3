using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000F3 RID: 243
	public class Asn1Null : Asn1Object
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x00018DB7 File Offset: 0x00016FB7
		public Asn1Null()
			: base(Asn1Null.ID)
		{
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00018DC4 File Offset: 0x00016FC4
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00018DCE File Offset: 0x00016FCE
		public override string ToString()
		{
			return base.ToString() + "NULL: \"\"";
		}

		// Token: 0x040004EA RID: 1258
		public const int TAG = 5;

		// Token: 0x040004EB RID: 1259
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, false, 5);
	}
}
