using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000F8 RID: 248
	public class Asn1SequenceOf : Asn1Structured
	{
		// Token: 0x06000623 RID: 1571 RVA: 0x0001907E File Offset: 0x0001727E
		public Asn1SequenceOf()
			: base(Asn1SequenceOf.ID)
		{
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001908B File Offset: 0x0001728B
		public Asn1SequenceOf(int size)
			: base(Asn1SequenceOf.ID, size)
		{
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00019099 File Offset: 0x00017299
		public Asn1SequenceOf(Asn1Sequence sequence)
			: base(Asn1SequenceOf.ID, sequence.toArray(), sequence.size())
		{
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x000190B2 File Offset: 0x000172B2
		[CLSCompliant(false)]
		public Asn1SequenceOf(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(Asn1SequenceOf.ID)
		{
			base.decodeStructured(dec, in_Renamed, len);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x000190C8 File Offset: 0x000172C8
		[CLSCompliant(false)]
		public override string ToString()
		{
			return base.toString("SEQUENCE OF: { ");
		}

		// Token: 0x040004F3 RID: 1267
		public const int TAG = 16;

		// Token: 0x040004F4 RID: 1268
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, true, 16);
	}
}
