using System;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x0200000D RID: 13
	internal class XmlRawWriterBase64Encoder : Base64Encoder
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002C92 File Offset: 0x00000E92
		internal XmlRawWriterBase64Encoder(XmlRawWriter rawWriter)
		{
			this.rawWriter = rawWriter;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002CA1 File Offset: 0x00000EA1
		internal override void WriteChars(char[] chars, int index, int count)
		{
			this.rawWriter.WriteRaw(chars, index, count);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002CB1 File Offset: 0x00000EB1
		internal override Task WriteCharsAsync(char[] chars, int index, int count)
		{
			return this.rawWriter.WriteRawAsync(chars, index, count);
		}

		// Token: 0x04000028 RID: 40
		private XmlRawWriter rawWriter;
	}
}
