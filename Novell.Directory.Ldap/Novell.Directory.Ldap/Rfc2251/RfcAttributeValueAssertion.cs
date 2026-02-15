using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000070 RID: 112
	public class RfcAttributeValueAssertion : Asn1Sequence
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0001179C File Offset: 0x0000F99C
		public virtual string AttributeDescription
		{
			get
			{
				return ((RfcAttributeDescription)base.get_Renamed(0)).stringValue();
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003CE RID: 974 RVA: 0x000117AF File Offset: 0x0000F9AF
		[CLSCompliant(false)]
		public virtual sbyte[] AssertionValue
		{
			get
			{
				return ((RfcAssertionValue)base.get_Renamed(1)).byteValue();
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x000116AF File Offset: 0x0000F8AF
		public RfcAttributeValueAssertion(RfcAttributeDescription ad, RfcAssertionValue av)
			: base(2)
		{
			base.add(ad);
			base.add(av);
		}
	}
}
