using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000032 RID: 50
	public class LdapExtendedRequest : LdapMessage
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00008E48 File Offset: 0x00007048
		public virtual LdapExtendedOperation ExtendedOperation
		{
			get
			{
				RfcExtendedRequest rfcExtendedRequest = (RfcExtendedRequest)this.Asn1Object.get_Renamed(1);
				string text = ((RfcLdapOID)((Asn1Tagged)rfcExtendedRequest.get_Renamed(0)).taggedValue()).stringValue();
				sbyte[] array = null;
				if (rfcExtendedRequest.size() >= 2)
				{
					array = ((Asn1OctetString)((Asn1Tagged)rfcExtendedRequest.get_Renamed(1)).taggedValue()).byteValue();
				}
				return new LdapExtendedOperation(text, array);
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00008EAF File Offset: 0x000070AF
		public LdapExtendedRequest(LdapExtendedOperation op, LdapControl[] cont)
			: base(23, new RfcExtendedRequest(new RfcLdapOID(op.getID()), (op.getValue() != null) ? new Asn1OctetString(op.getValue()) : null), cont)
		{
		}
	}
}
