using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000F9 RID: 249
	public class Asn1Set : Asn1Structured
	{
		// Token: 0x06000629 RID: 1577 RVA: 0x000190E5 File Offset: 0x000172E5
		public Asn1Set()
			: base(Asn1Set.ID)
		{
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000190F2 File Offset: 0x000172F2
		public Asn1Set(int size)
			: base(Asn1Set.ID, size)
		{
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00019100 File Offset: 0x00017300
		[CLSCompliant(false)]
		public Asn1Set(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(Asn1Set.ID)
		{
			base.decodeStructured(dec, in_Renamed, len);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00019116 File Offset: 0x00017316
		[CLSCompliant(false)]
		public override string ToString()
		{
			return base.toString("SET: { ");
		}

		// Token: 0x040004F5 RID: 1269
		public const int TAG = 17;

		// Token: 0x040004F6 RID: 1270
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, true, 17);
	}
}
