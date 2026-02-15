using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000094 RID: 148
	public class RfcSearchResultEntry : Asn1Sequence
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00011A64 File Offset: 0x0000FC64
		public virtual Asn1OctetString ObjectName
		{
			get
			{
				return (Asn1OctetString)base.get_Renamed(0);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x00013EC8 File Offset: 0x000120C8
		public virtual Asn1Sequence Attributes
		{
			get
			{
				return (Asn1Sequence)base.get_Renamed(1);
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00011B70 File Offset: 0x0000FD70
		[CLSCompliant(false)]
		public RfcSearchResultEntry(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00013ED6 File Offset: 0x000120D6
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 4);
		}
	}
}
