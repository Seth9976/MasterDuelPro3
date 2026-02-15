using System;

namespace System.Xml.Schema
{
	// Token: 0x020002C0 RID: 704
	internal sealed class XmlSchemaCollectionNode
	{
		// Token: 0x170007B2 RID: 1970
		// (set) Token: 0x0600205B RID: 8283 RVA: 0x000BEA95 File Offset: 0x000BCC95
		internal string NamespaceURI
		{
			set
			{
				this.namespaceUri = value;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x0600205C RID: 8284 RVA: 0x000BEA9E File Offset: 0x000BCC9E
		// (set) Token: 0x0600205D RID: 8285 RVA: 0x000BEAA6 File Offset: 0x000BCCA6
		internal SchemaInfo SchemaInfo
		{
			get
			{
				return this.schemaInfo;
			}
			set
			{
				this.schemaInfo = value;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x0600205E RID: 8286 RVA: 0x000BEAAF File Offset: 0x000BCCAF
		// (set) Token: 0x0600205F RID: 8287 RVA: 0x000BEAB7 File Offset: 0x000BCCB7
		internal XmlSchema Schema
		{
			get
			{
				return this.schema;
			}
			set
			{
				this.schema = value;
			}
		}

		// Token: 0x04000F1F RID: 3871
		private string namespaceUri;

		// Token: 0x04000F20 RID: 3872
		private SchemaInfo schemaInfo;

		// Token: 0x04000F21 RID: 3873
		private XmlSchema schema;
	}
}
