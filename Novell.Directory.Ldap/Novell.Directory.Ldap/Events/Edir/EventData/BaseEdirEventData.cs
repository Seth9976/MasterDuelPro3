using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000D7 RID: 215
	public class BaseEdirEventData
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x000167E2 File Offset: 0x000149E2
		public EdirEventDataType EventDataType
		{
			get
			{
				return this.event_data_type;
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000167EC File Offset: 0x000149EC
		public BaseEdirEventData(EdirEventDataType eventDataType, Asn1Object message)
		{
			this.event_data_type = eventDataType;
			byte[] array = SupportClass.ToByteArray(((Asn1OctetString)message).byteValue());
			this.decodedData = new MemoryStream(array);
			this.decoder = new LBERDecoder();
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001682E File Offset: 0x00014A2E
		protected void DataInitDone()
		{
			this.decodedData = null;
			this.decoder = null;
		}

		// Token: 0x0400046C RID: 1132
		protected MemoryStream decodedData;

		// Token: 0x0400046D RID: 1133
		protected LBERDecoder decoder;

		// Token: 0x0400046E RID: 1134
		protected EdirEventDataType event_data_type;
	}
}
