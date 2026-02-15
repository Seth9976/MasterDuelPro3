using System;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x0200000E RID: 14
	internal class XmlTextWriterBase64Encoder : Base64Encoder
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002CC1 File Offset: 0x00000EC1
		internal XmlTextWriterBase64Encoder(XmlTextEncoder xmlTextEncoder)
		{
			this.xmlTextEncoder = xmlTextEncoder;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002CD0 File Offset: 0x00000ED0
		internal override void WriteChars(char[] chars, int index, int count)
		{
			this.xmlTextEncoder.WriteRaw(chars, index, count);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002CE0 File Offset: 0x00000EE0
		internal override Task WriteCharsAsync(char[] chars, int index, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000029 RID: 41
		private XmlTextEncoder xmlTextEncoder;
	}
}
