using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000F7 RID: 247
	public class Asn1Sequence : Asn1Structured
	{
		// Token: 0x0600061D RID: 1565 RVA: 0x0001901F File Offset: 0x0001721F
		public Asn1Sequence()
			: base(Asn1Sequence.ID, 10)
		{
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001902E File Offset: 0x0001722E
		public Asn1Sequence(int size)
			: base(Asn1Sequence.ID, size)
		{
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001903C File Offset: 0x0001723C
		public Asn1Sequence(Asn1Object[] newContent, int size)
			: base(Asn1Sequence.ID, newContent, size)
		{
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001904B File Offset: 0x0001724B
		[CLSCompliant(false)]
		public Asn1Sequence(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(Asn1Sequence.ID)
		{
			base.decodeStructured(dec, in_Renamed, len);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00019061 File Offset: 0x00017261
		[CLSCompliant(false)]
		public override string ToString()
		{
			return base.toString("SEQUENCE: { ");
		}

		// Token: 0x040004F1 RID: 1265
		public const int TAG = 16;

		// Token: 0x040004F2 RID: 1266
		private static readonly Asn1Identifier ID = new Asn1Identifier(0, true, 16);
	}
}
