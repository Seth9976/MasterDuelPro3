using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200006E RID: 110
	public class RfcAttributeTypeAndValues : Asn1Sequence
	{
		// Token: 0x060003CA RID: 970 RVA: 0x000116AF File Offset: 0x0000F8AF
		public RfcAttributeTypeAndValues(RfcAttributeDescription type, Asn1SetOf vals)
			: base(2)
		{
			base.add(type);
			base.add(vals);
		}
	}
}
