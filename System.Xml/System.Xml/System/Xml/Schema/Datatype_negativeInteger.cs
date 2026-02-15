using System;

namespace System.Xml.Schema
{
	// Token: 0x02000263 RID: 611
	internal class Datatype_negativeInteger : Datatype_nonPositiveInteger
	{
		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x0009EF42 File Offset: 0x0009D142
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_negativeInteger.numeric10FacetsChecker;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001C39 RID: 7225 RVA: 0x0009EF49 File Offset: 0x0009D149
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NegativeInteger;
			}
		}

		// Token: 0x04000C39 RID: 3129
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(decimal.MinValue, -1m);
	}
}
