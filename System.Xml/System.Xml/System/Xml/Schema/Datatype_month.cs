using System;

namespace System.Xml.Schema
{
	// Token: 0x02000250 RID: 592
	internal class Datatype_month : Datatype_dateTimeBase
	{
		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x0009EA2D File Offset: 0x0009CC2D
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GMonth;
			}
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x0009EA31 File Offset: 0x0009CC31
		internal Datatype_month()
			: base(XsdDateTimeFlags.GMonth)
		{
		}
	}
}
