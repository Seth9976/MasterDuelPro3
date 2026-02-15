using System;

namespace System.Xml.Schema
{
	// Token: 0x0200024D RID: 589
	internal class Datatype_year : Datatype_dateTimeBase
	{
		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x0009EA07 File Offset: 0x0009CC07
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GYear;
			}
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x0009EA0B File Offset: 0x0009CC0B
		internal Datatype_year()
			: base(XsdDateTimeFlags.GYear)
		{
		}
	}
}
