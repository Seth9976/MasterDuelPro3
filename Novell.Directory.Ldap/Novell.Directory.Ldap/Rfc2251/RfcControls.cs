using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000077 RID: 119
	public class RfcControls : Asn1SequenceOf
	{
		// Token: 0x060003F8 RID: 1016 RVA: 0x00011BB0 File Offset: 0x0000FDB0
		public RfcControls()
			: base(5)
		{
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00011BBC File Offset: 0x0000FDBC
		[CLSCompliant(false)]
		public RfcControls(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
			for (int i = 0; i < base.size(); i++)
			{
				RfcControl rfcControl = new RfcControl((Asn1Sequence)base.get_Renamed(i));
				this.set_Renamed(i, rfcControl);
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00011BFD File Offset: 0x0000FDFD
		public void add(RfcControl control)
		{
			base.add(control);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00011C06 File Offset: 0x0000FE06
		public void set_Renamed(int index, RfcControl control)
		{
			base.set_Renamed(index, control);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00011C10 File Offset: 0x0000FE10
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(2, true, 0);
		}

		// Token: 0x04000256 RID: 598
		public const int CONTROLS = 0;
	}
}
