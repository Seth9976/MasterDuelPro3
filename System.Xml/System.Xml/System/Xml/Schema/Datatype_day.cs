using System;

namespace System.Xml.Schema
{
	// Token: 0x0200024F RID: 591
	internal class Datatype_day : Datatype_dateTimeBase
	{
		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x0009E0F3 File Offset: 0x0009C2F3
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GDay;
			}
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x0009EA23 File Offset: 0x0009CC23
		internal Datatype_day()
			: base(XsdDateTimeFlags.GDay)
		{
		}
	}
}
