using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200006C RID: 108
	public class RfcAttributeDescriptionList : Asn1SequenceOf
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x0001174D File Offset: 0x0000F94D
		public RfcAttributeDescriptionList(int size)
			: base(size)
		{
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00011758 File Offset: 0x0000F958
		public RfcAttributeDescriptionList(string[] attrs)
			: base((attrs == null) ? 0 : attrs.Length)
		{
			if (attrs != null)
			{
				for (int i = 0; i < attrs.Length; i++)
				{
					base.add(new RfcAttributeDescription(attrs[i]));
				}
			}
		}
	}
}
