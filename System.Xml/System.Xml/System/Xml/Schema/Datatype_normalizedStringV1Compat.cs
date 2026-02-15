using System;

namespace System.Xml.Schema
{
	// Token: 0x02000256 RID: 598
	internal class Datatype_normalizedStringV1Compat : Datatype_string
	{
		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001C09 RID: 7177 RVA: 0x0009ECEC File Offset: 0x0009CEEC
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NormalizedString;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}
	}
}
