using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000049 RID: 73
	public class LdapSearchResultReference : LdapMessage
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000B9FC File Offset: 0x00009BFC
		public virtual string[] Referrals
		{
			get
			{
				Asn1Object[] array = ((RfcSearchResultReference)this.message.Response).toArray();
				this.srefs = new string[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.srefs[i] = ((Asn1OctetString)array[i]).stringValue();
				}
				return this.srefs;
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000AB50 File Offset: 0x00008D50
		internal LdapSearchResultReference(RfcLdapMessage message)
			: base(message)
		{
		}

		// Token: 0x04000181 RID: 385
		private string[] srefs;

		// Token: 0x04000182 RID: 386
		private static object nameLock = new object();

		// Token: 0x04000183 RID: 387
		private static int refNum;

		// Token: 0x04000184 RID: 388
		private string name;
	}
}
