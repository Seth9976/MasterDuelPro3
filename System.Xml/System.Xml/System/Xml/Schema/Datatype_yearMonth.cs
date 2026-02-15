using System;

namespace System.Xml.Schema
{
	// Token: 0x0200024C RID: 588
	internal class Datatype_yearMonth : Datatype_dateTimeBase
	{
		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x0009E9FA File Offset: 0x0009CBFA
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GYearMonth;
			}
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x0009E9FE File Offset: 0x0009CBFE
		internal Datatype_yearMonth()
			: base(XsdDateTimeFlags.GYearMonth)
		{
		}
	}
}
