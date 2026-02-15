using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000AC RID: 172
	public class PartitionEntryCountResponse : LdapExtendedResponse
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00015184 File Offset: 0x00013384
		public virtual int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001518C File Offset: 0x0001338C
		public PartitionEntryCountResponse(RfcLdapMessage rfcMessage)
			: base(rfcMessage)
		{
			if (this.ResultCode != 0)
			{
				this.count = -1;
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
			this.count = asn1Integer.intValue();
		}

		// Token: 0x040002E5 RID: 741
		private int count;
	}
}
