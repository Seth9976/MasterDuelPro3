using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000DC RID: 220
	public class DebugParameter
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x00016F36 File Offset: 0x00015136
		public DebugParameterType DebugType
		{
			get
			{
				return this.debug_type;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00016F3E File Offset: 0x0001513E
		public object Data
		{
			get
			{
				return this.objData;
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00016F48 File Offset: 0x00015148
		public DebugParameter(Asn1Tagged dseObject)
		{
			switch (dseObject.getIdentifier().Tag)
			{
			case 1:
			case 4:
				this.objData = this.getTaggedIntValue(dseObject);
				break;
			case 2:
				this.objData = ((Asn1OctetString)dseObject.taggedValue()).stringValue();
				break;
			case 3:
				this.objData = ((Asn1OctetString)dseObject.taggedValue()).byteValue();
				break;
			case 5:
				this.objData = new ReferralAddress(this.getTaggedSequence(dseObject));
				break;
			case 6:
				this.objData = new DSETimeStamp(this.getTaggedSequence(dseObject));
				break;
			case 7:
			{
				ArrayList arrayList = new ArrayList();
				Asn1Sequence taggedSequence = this.getTaggedSequence(dseObject);
				int num = ((Asn1Integer)taggedSequence.get_Renamed(0)).intValue();
				if (num > 0)
				{
					Asn1Sequence asn1Sequence = (Asn1Sequence)taggedSequence.get_Renamed(1);
					for (int i = 0; i < num; i++)
					{
						arrayList.Add(new DSETimeStamp((Asn1Sequence)asn1Sequence.get_Renamed(i)));
					}
				}
				this.objData = arrayList;
				break;
			}
			default:
				throw new IOException("Unknown Tag in DebugParameter..");
			}
			this.debug_type = (DebugParameterType)dseObject.getIdentifier().Tag;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001708C File Offset: 0x0001528C
		protected int getTaggedIntValue(Asn1Tagged tagVal)
		{
			byte[] array = SupportClass.ToByteArray(((Asn1OctetString)tagVal.taggedValue()).byteValue());
			MemoryStream memoryStream = new MemoryStream(array);
			return (int)new LBERDecoder().decodeNumeric(memoryStream, array.Length);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000170CC File Offset: 0x000152CC
		protected Asn1Sequence getTaggedSequence(Asn1Tagged tagVal)
		{
			byte[] array = SupportClass.ToByteArray(((Asn1OctetString)tagVal.taggedValue()).byteValue());
			MemoryStream memoryStream = new MemoryStream(array);
			return new Asn1Sequence(new LBERDecoder(), memoryStream, array.Length);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00017104 File Offset: 0x00015304
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DebugParameter");
			if (Enum.IsDefined(this.debug_type.GetType(), this.debug_type))
			{
				stringBuilder.AppendFormat("(type={0},", this.debug_type);
				stringBuilder.AppendFormat("value={0})", this.objData);
			}
			else
			{
				stringBuilder.Append("(type=Unknown)");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000485 RID: 1157
		protected DebugParameterType debug_type;

		// Token: 0x04000486 RID: 1158
		protected object objData;
	}
}
