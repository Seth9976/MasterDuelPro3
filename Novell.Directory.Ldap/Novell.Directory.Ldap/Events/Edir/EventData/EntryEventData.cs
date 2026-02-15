using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000DD RID: 221
	public class EntryEventData : BaseEdirEventData
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x0001718E File Offset: 0x0001538E
		public string PerpetratorDN
		{
			get
			{
				return this.strPerpetratorDN;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00017196 File Offset: 0x00015396
		public string Entry
		{
			get
			{
				return this.strEntry;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0001719E File Offset: 0x0001539E
		public string NewDN
		{
			get
			{
				return this.strNewDN;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x000171A6 File Offset: 0x000153A6
		public string ClassId
		{
			get
			{
				return this.strClassId;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x000171AE File Offset: 0x000153AE
		public int Verb
		{
			get
			{
				return this.nVerb;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x000171B6 File Offset: 0x000153B6
		public int Flags
		{
			get
			{
				return this.nFlags;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x000171BE File Offset: 0x000153BE
		public DSETimeStamp TimeStamp
		{
			get
			{
				return this.timeStampObj;
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x000171C8 File Offset: 0x000153C8
		public EntryEventData(EdirEventDataType eventDataType, Asn1Object message)
			: base(eventDataType, message)
		{
			int[] array = new int[1];
			this.strPerpetratorDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.strEntry = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.strClassId = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.timeStampObj = new DSETimeStamp((Asn1Sequence)this.decoder.decode(this.decodedData, array));
			this.nVerb = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.nFlags = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.strNewDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			base.DataInitDone();
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000172D8 File Offset: 0x000154D8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("EntryEventData[");
			stringBuilder.AppendFormat("(Entry={0})", this.strEntry);
			stringBuilder.AppendFormat("(Prepetrator={0})", this.strPerpetratorDN);
			stringBuilder.AppendFormat("(ClassId={0})", this.strClassId);
			stringBuilder.AppendFormat("(Verb={0})", this.nVerb);
			stringBuilder.AppendFormat("(Flags={0})", this.nFlags);
			stringBuilder.AppendFormat("(NewDN={0})", this.strNewDN);
			stringBuilder.AppendFormat("(TimeStamp={0})", this.timeStampObj);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000487 RID: 1159
		protected string strPerpetratorDN;

		// Token: 0x04000488 RID: 1160
		protected string strEntry;

		// Token: 0x04000489 RID: 1161
		protected string strNewDN;

		// Token: 0x0400048A RID: 1162
		protected string strClassId;

		// Token: 0x0400048B RID: 1163
		protected int nVerb;

		// Token: 0x0400048C RID: 1164
		protected int nFlags;

		// Token: 0x0400048D RID: 1165
		protected DSETimeStamp timeStampObj;
	}
}
