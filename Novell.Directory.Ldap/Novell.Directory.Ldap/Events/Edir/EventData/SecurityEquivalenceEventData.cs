using System;
using System.Collections;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000E2 RID: 226
	public class SecurityEquivalenceEventData : BaseEdirEventData
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x000179F6 File Offset: 0x00015BF6
		public string EntryDN
		{
			get
			{
				return this.strEntryDN;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x000179FE File Offset: 0x00015BFE
		public int RetryCount
		{
			get
			{
				return this.retry_count;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00017A06 File Offset: 0x00015C06
		public string ValueDN
		{
			get
			{
				return this.strValueDN;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x00017A0E File Offset: 0x00015C0E
		public int ReferralCount
		{
			get
			{
				return this.referral_count;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00017A16 File Offset: 0x00015C16
		public ArrayList ReferralList
		{
			get
			{
				return this.referral_list;
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00017A20 File Offset: 0x00015C20
		public SecurityEquivalenceEventData(EdirEventDataType eventDataType, Asn1Object message)
			: base(eventDataType, message)
		{
			int[] array = new int[1];
			this.strEntryDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.retry_count = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.strValueDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			Asn1Sequence asn1Sequence = (Asn1Sequence)this.decoder.decode(this.decodedData, array);
			this.referral_count = ((Asn1Integer)asn1Sequence.get_Renamed(0)).intValue();
			this.referral_list = new ArrayList();
			if (this.referral_count > 0)
			{
				Asn1Sequence asn1Sequence2 = (Asn1Sequence)asn1Sequence.get_Renamed(1);
				for (int i = 0; i < this.referral_count; i++)
				{
					this.referral_list.Add(new ReferralAddress((Asn1Sequence)asn1Sequence2.get_Renamed(i)));
				}
			}
			base.DataInitDone();
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00017B28 File Offset: 0x00015D28
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SecurityEquivalenceEventData");
			stringBuilder.AppendFormat("(EntryDN={0})", this.strEntryDN);
			stringBuilder.AppendFormat("(RetryCount={0})", this.retry_count);
			stringBuilder.AppendFormat("(valueDN={0})", this.strValueDN);
			stringBuilder.AppendFormat("(referralCount={0})", this.referral_count);
			stringBuilder.AppendFormat("(Referral Lists={0})", this.referral_list);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0400049E RID: 1182
		protected string strEntryDN;

		// Token: 0x0400049F RID: 1183
		protected int retry_count;

		// Token: 0x040004A0 RID: 1184
		protected string strValueDN;

		// Token: 0x040004A1 RID: 1185
		protected int referral_count;

		// Token: 0x040004A2 RID: 1186
		protected ArrayList referral_list;
	}
}
