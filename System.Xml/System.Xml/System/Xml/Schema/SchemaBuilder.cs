using System;

namespace System.Xml.Schema
{
	// Token: 0x02000291 RID: 657
	internal abstract class SchemaBuilder
	{
		// Token: 0x06001DBB RID: 7611
		internal abstract bool ProcessElement(string prefix, string name, string ns);

		// Token: 0x06001DBC RID: 7612
		internal abstract void ProcessAttribute(string prefix, string name, string ns, string value);

		// Token: 0x06001DBD RID: 7613
		internal abstract bool IsContentParsed();

		// Token: 0x06001DBE RID: 7614
		internal abstract void ProcessMarkup(XmlNode[] markup);

		// Token: 0x06001DBF RID: 7615
		internal abstract void ProcessCData(string value);

		// Token: 0x06001DC0 RID: 7616
		internal abstract void StartChildren();

		// Token: 0x06001DC1 RID: 7617
		internal abstract void EndChildren();
	}
}
