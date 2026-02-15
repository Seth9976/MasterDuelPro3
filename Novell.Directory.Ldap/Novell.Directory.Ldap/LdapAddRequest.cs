using System;
using System.Collections;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200001C RID: 28
	public class LdapAddRequest : LdapMessage
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000048BC File Offset: 0x00002ABC
		public virtual LdapEntry Entry
		{
			get
			{
				RfcAddRequest rfcAddRequest = (RfcAddRequest)this.Asn1Object.getRequest();
				LdapAttributeSet ldapAttributeSet = new LdapAttributeSet();
				foreach (RfcAttributeTypeAndValues rfcAttributeTypeAndValues in rfcAddRequest.Attributes.toArray())
				{
					LdapAttribute ldapAttribute = new LdapAttribute(((Asn1OctetString)rfcAttributeTypeAndValues.get_Renamed(0)).stringValue());
					object[] array2 = ((Asn1SetOf)rfcAttributeTypeAndValues.get_Renamed(1)).toArray();
					object[] array3 = array2;
					for (int j = 0; j < array3.Length; j++)
					{
						ldapAttribute.addValue(((Asn1OctetString)array3[j]).byteValue());
					}
					ldapAttributeSet.Add(ldapAttribute);
				}
				return new LdapEntry(this.Asn1Object.RequestDN, ldapAttributeSet);
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000496E File Offset: 0x00002B6E
		public LdapAddRequest(LdapEntry entry, LdapControl[] cont)
			: base(8, new RfcAddRequest(new RfcLdapDN(entry.DN), LdapAddRequest.makeRfcAttrList(entry)), cont)
		{
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004990 File Offset: 0x00002B90
		private static RfcAttributeList makeRfcAttrList(LdapEntry entry)
		{
			LdapAttributeSet attributeSet = entry.getAttributeSet();
			RfcAttributeList rfcAttributeList = new RfcAttributeList(attributeSet.Count);
			foreach (object obj in attributeSet)
			{
				LdapAttribute ldapAttribute = (LdapAttribute)obj;
				Asn1SetOf asn1SetOf = new Asn1SetOf(ldapAttribute.size());
				IEnumerator byteValues = ldapAttribute.ByteValues;
				while (byteValues.MoveNext())
				{
					object obj2 = byteValues.Current;
					asn1SetOf.add(new RfcAttributeValue((sbyte[])obj2));
				}
				rfcAttributeList.add(new RfcAttributeTypeAndValues(new RfcAttributeDescription(ldapAttribute.Name), asn1SetOf));
			}
			return rfcAttributeList;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004A19 File Offset: 0x00002C19
		public override string ToString()
		{
			return this.Asn1Object.ToString();
		}
	}
}
