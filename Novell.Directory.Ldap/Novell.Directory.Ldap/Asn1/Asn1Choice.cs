using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000EC RID: 236
	public class Asn1Choice : Asn1Object
	{
		// Token: 0x17000186 RID: 390
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x00018951 File Offset: 0x00016B51
		[CLSCompliant(false)]
		protected internal virtual Asn1Object ChoiceValue
		{
			set
			{
				this.content = value;
			}
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0001895A File Offset: 0x00016B5A
		public Asn1Choice(Asn1Object content)
			: base(null)
		{
			this.content = content;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001896A File Offset: 0x00016B6A
		protected internal Asn1Choice()
			: base(null)
		{
			this.content = null;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0001897A File Offset: 0x00016B7A
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			this.content.encode(enc, out_Renamed);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00018989 File Offset: 0x00016B89
		public Asn1Object choiceValue()
		{
			return this.content;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00018991 File Offset: 0x00016B91
		public override Asn1Identifier getIdentifier()
		{
			return this.content.getIdentifier();
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001899E File Offset: 0x00016B9E
		public override void setIdentifier(Asn1Identifier id)
		{
			this.content.setIdentifier(id);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000189AC File Offset: 0x00016BAC
		public override string ToString()
		{
			return this.content.ToString();
		}

		// Token: 0x040004DB RID: 1243
		private Asn1Object content;
	}
}
