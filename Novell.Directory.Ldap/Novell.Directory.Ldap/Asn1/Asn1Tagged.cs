using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000FC RID: 252
	public class Asn1Tagged : Asn1Object
	{
		// Token: 0x17000191 RID: 401
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x00019383 File Offset: 0x00017583
		[CLSCompliant(false)]
		public virtual Asn1Object TaggedValue
		{
			set
			{
				this.content = value;
				if (!this.explicit_Renamed && value != null)
				{
					value.setIdentifier(this.getIdentifier());
				}
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x000193A3 File Offset: 0x000175A3
		public virtual bool Explicit
		{
			get
			{
				return this.explicit_Renamed;
			}
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x000193AB File Offset: 0x000175AB
		public Asn1Tagged(Asn1Identifier identifier, Asn1Object object_Renamed)
			: this(identifier, object_Renamed, true)
		{
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x000193B6 File Offset: 0x000175B6
		public Asn1Tagged(Asn1Identifier identifier, Asn1Object object_Renamed, bool explicit_Renamed)
			: base(identifier)
		{
			this.content = object_Renamed;
			this.explicit_Renamed = explicit_Renamed;
			if (!explicit_Renamed && this.content != null)
			{
				this.content.setIdentifier(identifier);
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x000193E4 File Offset: 0x000175E4
		[CLSCompliant(false)]
		public Asn1Tagged(Asn1Decoder dec, Stream in_Renamed, int len, Asn1Identifier identifier)
			: base(identifier)
		{
			this.content = new Asn1OctetString(dec, in_Renamed, len);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00013B8B File Offset: 0x00011D8B
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x000193FC File Offset: 0x000175FC
		public Asn1Object taggedValue()
		{
			return this.content;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00019404 File Offset: 0x00017604
		public override string ToString()
		{
			if (this.explicit_Renamed)
			{
				return base.ToString() + this.content.ToString();
			}
			return this.content.ToString();
		}

		// Token: 0x040004FB RID: 1275
		private bool explicit_Renamed;

		// Token: 0x040004FC RID: 1276
		private Asn1Object content;
	}
}
