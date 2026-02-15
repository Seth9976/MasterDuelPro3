using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Controls
{
	// Token: 0x020000E6 RID: 230
	public class LdapSortControl : LdapControl
	{
		// Token: 0x060005AA RID: 1450 RVA: 0x0001817C File Offset: 0x0001637C
		public LdapSortControl(LdapSortKey key, bool critical)
			: this(new LdapSortKey[] { key }, critical)
		{
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00018190 File Offset: 0x00016390
		public LdapSortControl(LdapSortKey[] keys, bool critical)
			: base(LdapSortControl.requestOID, critical, null)
		{
			Asn1SequenceOf asn1SequenceOf = new Asn1SequenceOf();
			for (int i = 0; i < keys.Length; i++)
			{
				Asn1Sequence asn1Sequence = new Asn1Sequence();
				asn1Sequence.add(new Asn1OctetString(keys[i].Key));
				if (keys[i].MatchRule != null)
				{
					asn1Sequence.add(new Asn1Tagged(new Asn1Identifier(2, false, LdapSortControl.ORDERING_RULE), new Asn1OctetString(keys[i].MatchRule), false));
				}
				if (keys[i].Reverse)
				{
					asn1Sequence.add(new Asn1Tagged(new Asn1Identifier(2, false, LdapSortControl.REVERSE_ORDER), new Asn1Boolean(true), false));
				}
				asn1SequenceOf.add(asn1Sequence);
			}
			this.setValue(asn1SequenceOf.getEncoding(new LBEREncoder()));
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001824C File Offset: 0x0001644C
		static LdapSortControl()
		{
			try
			{
				LdapControl.register(LdapSortControl.responseOID, Type.GetType("Novell.Directory.Ldap.Controls.LdapSortResponse"));
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x040004C0 RID: 1216
		private static int ORDERING_RULE = 0;

		// Token: 0x040004C1 RID: 1217
		private static int REVERSE_ORDER = 1;

		// Token: 0x040004C2 RID: 1218
		private static string requestOID = "1.2.840.113556.1.4.473";

		// Token: 0x040004C3 RID: 1219
		private static string responseOID = "1.2.840.113556.1.4.474";
	}
}
