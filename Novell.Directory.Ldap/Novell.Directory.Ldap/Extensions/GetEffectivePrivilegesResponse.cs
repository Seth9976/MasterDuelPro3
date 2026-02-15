using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x0200009F RID: 159
	public class GetEffectivePrivilegesResponse : LdapExtendedResponse
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x000142B0 File Offset: 0x000124B0
		public virtual int Privileges
		{
			get
			{
				return this.privileges;
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x000142B8 File Offset: 0x000124B8
		public GetEffectivePrivilegesResponse(RfcLdapMessage rfcMessage)
			: base(rfcMessage)
		{
			if (this.ResultCode != 0)
			{
				this.privileges = 0;
				return;
			}
			sbyte[] value = this.Value;
			if (value == null)
			{
				throw new IOException("No returned value");
			}
			LBERDecoder lberdecoder = new LBERDecoder();
			if (lberdecoder == null)
			{
				throw new IOException("Decoding error");
			}
			Asn1Integer asn1Integer = (Asn1Integer)lberdecoder.decode(value);
			if (asn1Integer == null)
			{
				throw new IOException("Decoding error");
			}
			this.privileges = asn1Integer.intValue();
		}

		// Token: 0x0400028B RID: 651
		private int privileges;
	}
}
