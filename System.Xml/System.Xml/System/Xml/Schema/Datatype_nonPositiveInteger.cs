using System;

namespace System.Xml.Schema
{
	// Token: 0x02000262 RID: 610
	internal class Datatype_nonPositiveInteger : Datatype_integer
	{
		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001C33 RID: 7219 RVA: 0x0009EF14 File Offset: 0x0009D114
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_nonPositiveInteger.numeric10FacetsChecker;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001C34 RID: 7220 RVA: 0x0009EF1B File Offset: 0x0009D11B
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NonPositiveInteger;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001C35 RID: 7221 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000C38 RID: 3128
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(decimal.MinValue, 0m);
	}
}
