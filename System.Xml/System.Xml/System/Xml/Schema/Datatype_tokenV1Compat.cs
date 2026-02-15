using System;

namespace System.Xml.Schema
{
	// Token: 0x02000258 RID: 600
	internal class Datatype_tokenV1Compat : Datatype_normalizedStringV1Compat
	{
		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001C0F RID: 7183 RVA: 0x0009ECF8 File Offset: 0x0009CEF8
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Token;
			}
		}
	}
}
