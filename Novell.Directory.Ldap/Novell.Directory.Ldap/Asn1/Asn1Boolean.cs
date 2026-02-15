using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000EB RID: 235
	public class Asn1Boolean : Asn1Object
	{
		// Token: 0x060005CA RID: 1482 RVA: 0x000188DF File Offset: 0x00016ADF
		public Asn1Boolean(bool content)
			: base(Asn1Boolean.ID)
		{
			this.content = content;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000188F3 File Offset: 0x00016AF3
		[CLSCompliant(false)]
		public Asn1Boolean(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(Asn1Boolean.ID)
		{
			this.content = (bool)dec.decodeBoolean(in_Renamed, len);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00018913 File Offset: 0x00016B13
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001891D File Offset: 0x00016B1D
		public bool booleanValue()
		{
			return this.content;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00018925 File Offset: 0x00016B25
		public override string ToString()
		{
			return base.ToString() + "BOOLEAN: " + this.content.ToString();
		}

		// Token: 0x040004D8 RID: 1240
		private bool content;

		// Token: 0x040004D9 RID: 1241
		public const int TAG = 1;

		// Token: 0x040004DA RID: 1242
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, false, 1);
	}
}
