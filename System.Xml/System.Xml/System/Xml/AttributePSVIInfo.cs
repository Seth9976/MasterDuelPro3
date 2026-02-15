using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000C9 RID: 201
	internal class AttributePSVIInfo
	{
		// Token: 0x060009D3 RID: 2515 RVA: 0x00035A0F File Offset: 0x00033C0F
		internal AttributePSVIInfo()
		{
			this.attributeSchemaInfo = new XmlSchemaInfo();
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00035A22 File Offset: 0x00033C22
		internal void Reset()
		{
			this.typedAttributeValue = null;
			this.localName = string.Empty;
			this.namespaceUri = string.Empty;
			this.attributeSchemaInfo.Clear();
		}

		// Token: 0x040005A3 RID: 1443
		internal string localName;

		// Token: 0x040005A4 RID: 1444
		internal string namespaceUri;

		// Token: 0x040005A5 RID: 1445
		internal object typedAttributeValue;

		// Token: 0x040005A6 RID: 1446
		internal XmlSchemaInfo attributeSchemaInfo;
	}
}
