using System;

namespace System.Xml.Schema
{
	// Token: 0x02000257 RID: 599
	internal class Datatype_token : Datatype_normalizedString
	{
		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001C0C RID: 7180 RVA: 0x0009ECF8 File Offset: 0x0009CEF8
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Token;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001C0D RID: 7181 RVA: 0x0003A73C File Offset: 0x0003893C
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}
	}
}
