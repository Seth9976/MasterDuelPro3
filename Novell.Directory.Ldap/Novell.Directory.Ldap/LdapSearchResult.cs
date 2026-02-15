using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000048 RID: 72
	public class LdapSearchResult : LdapMessage
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000B8D8 File Offset: 0x00009AD8
		public virtual LdapEntry Entry
		{
			get
			{
				if (this.entry == null)
				{
					LdapAttributeSet ldapAttributeSet = new LdapAttributeSet();
					foreach (Asn1Sequence asn1Sequence in ((RfcSearchResultEntry)this.message.Response).Attributes.toArray())
					{
						LdapAttribute ldapAttribute = new LdapAttribute(((Asn1OctetString)asn1Sequence.get_Renamed(0)).stringValue());
						object[] array2 = ((Asn1Set)asn1Sequence.get_Renamed(1)).toArray();
						object[] array3 = array2;
						for (int j = 0; j < array3.Length; j++)
						{
							ldapAttribute.addValue(((Asn1OctetString)array3[j]).byteValue());
						}
						ldapAttributeSet.Add(ldapAttribute);
					}
					this.entry = new LdapEntry(((RfcSearchResultEntry)this.message.Response).ObjectName.stringValue(), ldapAttributeSet);
				}
				return this.entry;
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000AB50 File Offset: 0x00008D50
		internal LdapSearchResult(RfcLdapMessage message)
			: base(message)
		{
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000B9B0 File Offset: 0x00009BB0
		public LdapSearchResult(LdapEntry entry, LdapControl[] cont)
		{
			if (entry == null)
			{
				throw new ArgumentException("Argument \"entry\" cannot be null");
			}
			this.entry = entry;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000B9D0 File Offset: 0x00009BD0
		public override string ToString()
		{
			string text;
			if (this.entry == null)
			{
				text = base.ToString();
			}
			else
			{
				text = this.entry.ToString();
			}
			return text;
		}

		// Token: 0x04000180 RID: 384
		private LdapEntry entry;
	}
}
