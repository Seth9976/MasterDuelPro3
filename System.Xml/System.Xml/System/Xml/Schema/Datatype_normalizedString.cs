using System;

namespace System.Xml.Schema
{
	// Token: 0x02000255 RID: 597
	internal class Datatype_normalizedString : Datatype_string
	{
		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x0009ECEC File Offset: 0x0009CEEC
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NormalizedString;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Replace;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001C07 RID: 7175 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}
	}
}
