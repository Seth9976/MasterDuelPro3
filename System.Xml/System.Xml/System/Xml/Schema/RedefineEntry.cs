using System;

namespace System.Xml.Schema
{
	// Token: 0x0200028D RID: 653
	internal class RedefineEntry
	{
		// Token: 0x06001D67 RID: 7527 RVA: 0x000A7B34 File Offset: 0x000A5D34
		public RedefineEntry(XmlSchemaRedefine external, XmlSchema schema)
		{
			this.redefine = external;
			this.schemaToUpdate = schema;
		}

		// Token: 0x04000CB4 RID: 3252
		internal XmlSchemaRedefine redefine;

		// Token: 0x04000CB5 RID: 3253
		internal XmlSchema schemaToUpdate;
	}
}
