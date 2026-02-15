using System;

namespace System.Xml.Schema
{
	// Token: 0x0200024A RID: 586
	internal class Datatype_time : Datatype_dateTimeBase
	{
		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x0009E9E9 File Offset: 0x0009CBE9
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Time;
			}
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x0009E9E0 File Offset: 0x0009CBE0
		internal Datatype_time()
			: base(XsdDateTimeFlags.Time)
		{
		}
	}
}
