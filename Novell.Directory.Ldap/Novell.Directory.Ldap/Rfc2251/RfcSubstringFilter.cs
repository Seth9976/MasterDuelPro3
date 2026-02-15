using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000096 RID: 150
	public class RfcSubstringFilter : Asn1Sequence
	{
		// Token: 0x06000497 RID: 1175 RVA: 0x000116AF File Offset: 0x0000F8AF
		public RfcSubstringFilter(RfcAttributeDescription type, Asn1SequenceOf substrings)
			: base(2)
		{
			base.add(type);
			base.add(substrings);
		}
	}
}
