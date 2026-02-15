using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000A3 RID: 163
	public class GetReplicationFilterResponse : LdapExtendedResponse
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x000146E0 File Offset: 0x000128E0
		public virtual string[][] ReplicationFilter
		{
			get
			{
				return this.returnedFilter;
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000146E8 File Offset: 0x000128E8
		public GetReplicationFilterResponse(RfcLdapMessage rfcMessage)
			: base(rfcMessage)
		{
			if (this.ResultCode != 0)
			{
				this.returnedFilter = new string[0][];
				for (int i = 0; i < 0; i++)
				{
					this.returnedFilter[i] = new string[0];
				}
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
			Asn1Sequence asn1Sequence = (Asn1Sequence)lberdecoder.decode(value);
			if (asn1Sequence == null)
			{
				throw new IOException("Decoding error");
			}
			int num = asn1Sequence.size();
			this.returnedFilter = new string[num][];
			for (int j = 0; j < num; j++)
			{
				Asn1Sequence asn1Sequence2 = (Asn1Sequence)asn1Sequence.get_Renamed(j);
				if (asn1Sequence2 == null)
				{
					throw new IOException("Decoding error");
				}
				Asn1OctetString asn1OctetString = (Asn1OctetString)asn1Sequence2.get_Renamed(0);
				if (asn1OctetString == null)
				{
					return;
				}
				Asn1Sequence asn1Sequence3 = (Asn1Sequence)asn1Sequence2.get_Renamed(1);
				if (asn1Sequence3 == null)
				{
					throw new IOException("Decoding error");
				}
				int num2 = asn1Sequence3.size();
				this.returnedFilter[j] = new string[num2 + 1];
				this.returnedFilter[j][0] = asn1OctetString.stringValue();
				if (this.returnedFilter[j][0] == null)
				{
					throw new IOException("Decoding error");
				}
				for (int k = 0; k < num2; k++)
				{
					Asn1OctetString asn1OctetString2 = (Asn1OctetString)asn1Sequence3.get_Renamed(k);
					if (asn1OctetString2 == null)
					{
						throw new IOException("Decoding error");
					}
					this.returnedFilter[j][k + 1] = asn1OctetString2.stringValue();
					if (this.returnedFilter[j][k + 1] == null)
					{
						throw new IOException("Decoding error");
					}
				}
			}
		}

		// Token: 0x04000294 RID: 660
		internal string[][] returnedFilter;
	}
}
