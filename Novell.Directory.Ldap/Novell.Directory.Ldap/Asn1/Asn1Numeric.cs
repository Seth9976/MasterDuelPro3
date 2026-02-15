using System;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000F4 RID: 244
	public abstract class Asn1Numeric : Asn1Object
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x00018DEF File Offset: 0x00016FEF
		internal Asn1Numeric(Asn1Identifier id, int value_Renamed)
			: base(id)
		{
			this.content = (long)value_Renamed;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00018E00 File Offset: 0x00017000
		internal Asn1Numeric(Asn1Identifier id, long value_Renamed)
			: base(id)
		{
			this.content = value_Renamed;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00018E10 File Offset: 0x00017010
		public int intValue()
		{
			return (int)this.content;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00018E19 File Offset: 0x00017019
		public long longValue()
		{
			return this.content;
		}

		// Token: 0x040004EC RID: 1260
		private long content;
	}
}
