using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x0200009D RID: 157
	public class GetBindDNResponse : LdapExtendedResponse
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00014146 File Offset: 0x00012346
		public virtual string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00014150 File Offset: 0x00012350
		public GetBindDNResponse(RfcLdapMessage rfcMessage)
			: base(rfcMessage)
		{
			if (this.ResultCode == 0)
			{
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
				Asn1OctetString asn1OctetString = (Asn1OctetString)lberdecoder.decode(value);
				if (asn1OctetString == null)
				{
					throw new IOException("Decoding error");
				}
				this.identity = asn1OctetString.stringValue();
				if (this.identity == null)
				{
					throw new IOException("Decoding error");
				}
			}
			else
			{
				this.identity = "";
			}
		}

		// Token: 0x0400028A RID: 650
		private string identity;
	}
}
