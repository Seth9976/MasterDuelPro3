using System;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000026 RID: 38
	public class LdapCompareRequest : LdapMessage
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006067 File Offset: 0x00004267
		public virtual string AttributeDescription
		{
			get
			{
				return ((RfcCompareRequest)this.Asn1Object.getRequest()).AttributeValueAssertion.AttributeDescription;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00006083 File Offset: 0x00004283
		[CLSCompliant(false)]
		public virtual sbyte[] AssertionValue
		{
			get
			{
				return ((RfcCompareRequest)this.Asn1Object.getRequest()).AttributeValueAssertion.AssertionValue;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00005D4D File Offset: 0x00003F4D
		public virtual string DN
		{
			get
			{
				return this.Asn1Object.RequestDN;
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000609F File Offset: 0x0000429F
		[CLSCompliant(false)]
		public LdapCompareRequest(string dn, string name, sbyte[] value_Renamed, LdapControl[] cont)
			: base(14, new RfcCompareRequest(new RfcLdapDN(dn), new RfcAttributeValueAssertion(new RfcAttributeDescription(name), new RfcAssertionValue(value_Renamed))), cont)
		{
		}
	}
}
