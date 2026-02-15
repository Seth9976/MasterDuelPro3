using System;

namespace System.Xml.Schema
{
	// Token: 0x02000268 RID: 616
	internal class Datatype_nonNegativeInteger : Datatype_integer
	{
		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x0009F296 File Offset: 0x0009D496
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_nonNegativeInteger.numeric10FacetsChecker;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x0009F29D File Offset: 0x0009D49D
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NonNegativeInteger;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000C46 RID: 3142
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, decimal.MaxValue);
	}
}
