using System;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000FA RID: 250
	public class Asn1SetOf : Asn1Structured
	{
		// Token: 0x0600062E RID: 1582 RVA: 0x00019133 File Offset: 0x00017333
		public Asn1SetOf()
			: base(Asn1SetOf.ID)
		{
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00019140 File Offset: 0x00017340
		public Asn1SetOf(int size)
			: base(Asn1SetOf.ID, size)
		{
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001914E File Offset: 0x0001734E
		public Asn1SetOf(Asn1Set set_Renamed)
			: base(Asn1SetOf.ID, set_Renamed.toArray(), set_Renamed.size())
		{
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00019167 File Offset: 0x00017367
		[CLSCompliant(false)]
		public override string ToString()
		{
			return base.toString("SET OF: { ");
		}

		// Token: 0x040004F7 RID: 1271
		public const int TAG = 17;

		// Token: 0x040004F8 RID: 1272
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, true, 17);
	}
}
