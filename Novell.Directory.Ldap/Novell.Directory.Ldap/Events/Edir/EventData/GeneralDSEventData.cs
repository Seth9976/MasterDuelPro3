using System;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000DE RID: 222
	public class GeneralDSEventData : BaseEdirEventData
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0001738F File Offset: 0x0001558F
		public int DSTime
		{
			get
			{
				return this.ds_time;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00017397 File Offset: 0x00015597
		public int MilliSeconds
		{
			get
			{
				return this.milli_seconds;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x0001739F File Offset: 0x0001559F
		public int Verb
		{
			get
			{
				return this.nVerb;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x000173A7 File Offset: 0x000155A7
		public int CurrentProcess
		{
			get
			{
				return this.current_process;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x000173AF File Offset: 0x000155AF
		public string PerpetratorDN
		{
			get
			{
				return this.strPerpetratorDN;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x000173B7 File Offset: 0x000155B7
		public int[] IntegerValues
		{
			get
			{
				return this.integer_values;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x000173BF File Offset: 0x000155BF
		public string[] StringValues
		{
			get
			{
				return this.string_values;
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x000173C8 File Offset: 0x000155C8
		public GeneralDSEventData(EdirEventDataType eventDataType, Asn1Object message)
			: base(eventDataType, message)
		{
			int[] array = new int[1];
			this.ds_time = this.getTaggedIntValue((Asn1Tagged)this.decoder.decode(this.decodedData, array), GeneralEventField.EVT_TAG_GEN_DSTIME);
			this.milli_seconds = this.getTaggedIntValue((Asn1Tagged)this.decoder.decode(this.decodedData, array), GeneralEventField.EVT_TAG_GEN_MILLISEC);
			this.nVerb = this.getTaggedIntValue((Asn1Tagged)this.decoder.decode(this.decodedData, array), GeneralEventField.EVT_TAG_GEN_VERB);
			this.current_process = this.getTaggedIntValue((Asn1Tagged)this.decoder.decode(this.decodedData, array), GeneralEventField.EVT_TAG_GEN_CURRPROC);
			this.strPerpetratorDN = this.getTaggedStringValue((Asn1Tagged)this.decoder.decode(this.decodedData, array), GeneralEventField.EVT_TAG_GEN_PERP);
			Asn1Tagged asn1Tagged = (Asn1Tagged)this.decoder.decode(this.decodedData, array);
			if (asn1Tagged.getIdentifier().Tag == 6)
			{
				Asn1Object[] array2 = this.getTaggedSequence(asn1Tagged, GeneralEventField.EVT_TAG_GEN_INTEGERS).toArray();
				this.integer_values = new int[array2.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					this.integer_values[i] = ((Asn1Integer)array2[i]).intValue();
				}
				asn1Tagged = (Asn1Tagged)this.decoder.decode(this.decodedData, array);
			}
			else
			{
				this.integer_values = null;
			}
			if (asn1Tagged.getIdentifier().Tag == 7 && asn1Tagged.getIdentifier().Constructed)
			{
				Asn1Object[] array3 = this.getTaggedSequence(asn1Tagged, GeneralEventField.EVT_TAG_GEN_STRINGS).toArray();
				this.string_values = new string[array3.Length];
				for (int j = 0; j < array3.Length; j++)
				{
					this.string_values[j] = ((Asn1OctetString)array3[j]).stringValue();
				}
			}
			else
			{
				this.string_values = null;
			}
			base.DataInitDone();
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00017594 File Offset: 0x00015794
		protected int getTaggedIntValue(Asn1Tagged tagvalue, GeneralEventField tagid)
		{
			Asn1Object asn1Object = tagvalue.taggedValue();
			if (tagid != (GeneralEventField)tagvalue.getIdentifier().Tag)
			{
				throw new IOException("Unknown Tagged Data");
			}
			byte[] array = SupportClass.ToByteArray(((Asn1OctetString)asn1Object).byteValue());
			MemoryStream memoryStream = new MemoryStream(array);
			LBERDecoder lberdecoder = new LBERDecoder();
			int num = array.Length;
			return (int)lberdecoder.decodeNumeric(memoryStream, num);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x000175EC File Offset: 0x000157EC
		protected string getTaggedStringValue(Asn1Tagged tagvalue, GeneralEventField tagid)
		{
			Asn1Object asn1Object = tagvalue.taggedValue();
			if (tagid != (GeneralEventField)tagvalue.getIdentifier().Tag)
			{
				throw new IOException("Unknown Tagged Data");
			}
			byte[] array = SupportClass.ToByteArray(((Asn1OctetString)asn1Object).byteValue());
			MemoryStream memoryStream = new MemoryStream(array);
			LBERDecoder lberdecoder = new LBERDecoder();
			int num = array.Length;
			return (string)lberdecoder.decodeCharacterString(memoryStream, num);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00017644 File Offset: 0x00015844
		protected Asn1Sequence getTaggedSequence(Asn1Tagged tagvalue, GeneralEventField tagid)
		{
			Asn1Object asn1Object = tagvalue.taggedValue();
			if (tagid != (GeneralEventField)tagvalue.getIdentifier().Tag)
			{
				throw new IOException("Unknown Tagged Data");
			}
			byte[] array = SupportClass.ToByteArray(((Asn1OctetString)asn1Object).byteValue());
			MemoryStream memoryStream = new MemoryStream(array);
			Asn1Decoder asn1Decoder = new LBERDecoder();
			int num = array.Length;
			return new Asn1Sequence(asn1Decoder, memoryStream, num);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00017698 File Offset: 0x00015898
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[GeneralDSEventData");
			stringBuilder.AppendFormat("(DSTime={0})", this.ds_time);
			stringBuilder.AppendFormat("(MilliSeconds={0})", this.milli_seconds);
			stringBuilder.AppendFormat("(verb={0})", this.nVerb);
			stringBuilder.AppendFormat("(currentProcess={0})", this.current_process);
			stringBuilder.AppendFormat("(PerpetartorDN={0})", this.strPerpetratorDN);
			stringBuilder.AppendFormat("(Integer Values={0})", this.integer_values);
			string text = "(String Values={0})";
			object[] array = this.string_values;
			stringBuilder.AppendFormat(text, array);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0400048E RID: 1166
		protected int ds_time;

		// Token: 0x0400048F RID: 1167
		protected int milli_seconds;

		// Token: 0x04000490 RID: 1168
		protected int nVerb;

		// Token: 0x04000491 RID: 1169
		protected int current_process;

		// Token: 0x04000492 RID: 1170
		protected string strPerpetratorDN;

		// Token: 0x04000493 RID: 1171
		protected int[] integer_values;

		// Token: 0x04000494 RID: 1172
		protected string[] string_values;
	}
}
