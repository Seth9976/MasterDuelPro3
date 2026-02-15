using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000086 RID: 134
	public class RfcMatchingRuleAssertion : Asn1Sequence
	{
		// Token: 0x06000466 RID: 1126 RVA: 0x00013C21 File Offset: 0x00011E21
		public RfcMatchingRuleAssertion(RfcAssertionValue matchValue)
			: this(null, null, matchValue, null)
		{
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00013C30 File Offset: 0x00011E30
		public RfcMatchingRuleAssertion(RfcMatchingRuleId matchingRule, RfcAttributeDescription type, RfcAssertionValue matchValue, Asn1Boolean dnAttributes)
			: base(4)
		{
			if (matchingRule != null)
			{
				base.add(new Asn1Tagged(new Asn1Identifier(2, false, 1), matchingRule, false));
			}
			if (type != null)
			{
				base.add(new Asn1Tagged(new Asn1Identifier(2, false, 2), type, false));
			}
			base.add(new Asn1Tagged(new Asn1Identifier(2, false, 3), matchValue, false));
			if (dnAttributes != null && dnAttributes.booleanValue())
			{
				base.add(new Asn1Tagged(new Asn1Identifier(2, false, 4), dnAttributes, false));
			}
		}
	}
}
