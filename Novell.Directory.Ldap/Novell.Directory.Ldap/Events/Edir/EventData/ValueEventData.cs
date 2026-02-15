using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000E3 RID: 227
	public class ValueEventData : BaseEdirEventData
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x00017BBB File Offset: 0x00015DBB
		public string Attribute
		{
			get
			{
				return this.strAttribute;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00017BC3 File Offset: 0x00015DC3
		public string ClassId
		{
			get
			{
				return this.strClassId;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00017BCB File Offset: 0x00015DCB
		public string Data
		{
			get
			{
				return this.strData;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00017BD3 File Offset: 0x00015DD3
		public byte[] BinaryData
		{
			get
			{
				return this.binData;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00017BDB File Offset: 0x00015DDB
		public string Entry
		{
			get
			{
				return this.strEntry;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x00017BE3 File Offset: 0x00015DE3
		public string PerpetratorDN
		{
			get
			{
				return this.strPerpetratorDN;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00017BEB File Offset: 0x00015DEB
		public string Syntax
		{
			get
			{
				return this.strSyntax;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x00017BF3 File Offset: 0x00015DF3
		public DSETimeStamp TimeStamp
		{
			get
			{
				return this.timeStampObj;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x00017BFB File Offset: 0x00015DFB
		public int Verb
		{
			get
			{
				return this.nVerb;
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00017C04 File Offset: 0x00015E04
		public ValueEventData(EdirEventDataType eventDataType, Asn1Object message)
			: base(eventDataType, message)
		{
			int[] array = new int[1];
			this.strPerpetratorDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.strEntry = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.strAttribute = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.strSyntax = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.strClassId = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.timeStampObj = new DSETimeStamp((Asn1Sequence)this.decoder.decode(this.decodedData, array));
			Asn1OctetString asn1OctetString = (Asn1OctetString)this.decoder.decode(this.decodedData, array);
			this.strData = asn1OctetString.stringValue();
			this.binData = SupportClass.ToByteArray(asn1OctetString.byteValue());
			this.nVerb = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			base.DataInitDone();
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00017D4C File Offset: 0x00015F4C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ValueEventData");
			stringBuilder.AppendFormat("(Attribute={0})", this.strAttribute);
			stringBuilder.AppendFormat("(Classid={0})", this.strClassId);
			stringBuilder.AppendFormat("(Data={0})", this.strData);
			stringBuilder.AppendFormat("(Data={0})", this.binData);
			stringBuilder.AppendFormat("(Entry={0})", this.strEntry);
			stringBuilder.AppendFormat("(Perpetrator={0})", this.strPerpetratorDN);
			stringBuilder.AppendFormat("(Syntax={0})", this.strSyntax);
			stringBuilder.AppendFormat("(TimeStamp={0})", this.timeStampObj);
			stringBuilder.AppendFormat("(Verb={0})", this.nVerb);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x040004A3 RID: 1187
		protected string strAttribute;

		// Token: 0x040004A4 RID: 1188
		protected string strClassId;

		// Token: 0x040004A5 RID: 1189
		protected string strData;

		// Token: 0x040004A6 RID: 1190
		protected byte[] binData;

		// Token: 0x040004A7 RID: 1191
		protected string strEntry;

		// Token: 0x040004A8 RID: 1192
		protected string strPerpetratorDN;

		// Token: 0x040004A9 RID: 1193
		protected string strSyntax;

		// Token: 0x040004AA RID: 1194
		protected DSETimeStamp timeStampObj;

		// Token: 0x040004AB RID: 1195
		protected int nVerb;
	}
}
