using System;

namespace System.Xml.Schema
{
	// Token: 0x0200023B RID: 571
	internal class Datatype_untypedAtomicType : Datatype_anyAtomicType
	{
		// Token: 0x06001B71 RID: 7025 RVA: 0x0009E358 File Offset: 0x0009C558
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlUntypedConverter.Untyped;
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001B72 RID: 7026 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Preserve;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x0003CDFD File Offset: 0x0003AFFD
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UntypedAtomic;
			}
		}
	}
}
