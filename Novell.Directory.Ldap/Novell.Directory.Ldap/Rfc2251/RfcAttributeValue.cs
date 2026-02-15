using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200006F RID: 111
	public class RfcAttributeValue : Asn1OctetString
	{
		// Token: 0x060003CB RID: 971 RVA: 0x00011793 File Offset: 0x0000F993
		public RfcAttributeValue(string value_Renamed)
			: base(value_Renamed)
		{
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00011730 File Offset: 0x0000F930
		[CLSCompliant(false)]
		public RfcAttributeValue(sbyte[] value_Renamed)
			: base(value_Renamed)
		{
		}
	}
}
