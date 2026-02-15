using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000074 RID: 116
	public class RfcCompareRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00011A08 File Offset: 0x0000FC08
		public virtual RfcAttributeValueAssertion AttributeValueAssertion
		{
			get
			{
				return (RfcAttributeValueAssertion)base.get_Renamed(1);
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00011A16 File Offset: 0x0000FC16
		public RfcCompareRequest(RfcLdapDN entry, RfcAttributeValueAssertion ava)
			: base(2)
		{
			base.add(entry);
			base.add(ava);
			if (ava.AssertionValue == null)
			{
				throw new ArgumentException("compare: Attribute must have an assertion value");
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000116C6 File Offset: 0x0000F8C6
		internal RfcCompareRequest(Asn1Object[] origRequest, string base_Renamed)
			: base(origRequest, origRequest.Length)
		{
			if (base_Renamed != null)
			{
				base.set_Renamed(0, new RfcLdapDN(base_Renamed));
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00011A40 File Offset: 0x0000FC40
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 14);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00011A4B File Offset: 0x0000FC4B
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcCompareRequest(base.toArray(), base_Renamed);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000116FA File Offset: 0x0000F8FA
		public string getRequestDN()
		{
			return ((RfcLdapDN)base.get_Renamed(0)).stringValue();
		}
	}
}
