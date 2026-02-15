using System;

namespace System.Xml.Schema
{
	// Token: 0x0200024E RID: 590
	internal class Datatype_monthDay : Datatype_dateTimeBase
	{
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x0009EA15 File Offset: 0x0009CC15
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GMonthDay;
			}
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x0009EA19 File Offset: 0x0009CC19
		internal Datatype_monthDay()
			: base(XsdDateTimeFlags.GMonthDay)
		{
		}
	}
}
