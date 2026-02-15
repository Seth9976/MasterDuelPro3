using System;
using System.Xml;

namespace System.Runtime.Diagnostics
{
	// Token: 0x0200003B RID: 59
	internal class StringTraceRecord : TraceRecord
	{
		// Token: 0x0600014C RID: 332 RVA: 0x0000697C File Offset: 0x00004B7C
		internal StringTraceRecord(string elementName, string content)
		{
			this.elementName = elementName;
			this.content = content;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006992 File Offset: 0x00004B92
		internal override void WriteTo(XmlWriter writer)
		{
			writer.WriteElementString(this.elementName, this.content);
		}

		// Token: 0x04000093 RID: 147
		private string elementName;

		// Token: 0x04000094 RID: 148
		private string content;
	}
}
