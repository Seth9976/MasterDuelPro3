using System;

namespace System.Xml.Schema
{
	// Token: 0x0200023A RID: 570
	internal class Datatype_anyAtomicType : Datatype_anySimpleType
	{
		// Token: 0x06001B6D RID: 7021 RVA: 0x0009E3B5 File Offset: 0x0009C5B5
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlAnyConverter.AnyAtomic;
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Preserve;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x0003CFC9 File Offset: 0x0003B1C9
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyAtomicType;
			}
		}
	}
}
