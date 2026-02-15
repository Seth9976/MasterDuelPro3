using System;

namespace System.Xml.Schema
{
	// Token: 0x0200026D RID: 621
	internal class Datatype_positiveInteger : Datatype_nonNegativeInteger
	{
		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x0009F5C7 File Offset: 0x0009D7C7
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_positiveInteger.numeric10FacetsChecker;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x0009F5CE File Offset: 0x0009D7CE
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.PositiveInteger;
			}
		}

		// Token: 0x04000C53 RID: 3155
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(1m, decimal.MaxValue);
	}
}
