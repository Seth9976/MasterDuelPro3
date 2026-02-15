using System;

namespace System.Xml.Schema
{
	// Token: 0x0200024B RID: 587
	internal class Datatype_date : Datatype_dateTimeBase
	{
		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x0009E9ED File Offset: 0x0009CBED
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Date;
			}
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x0009E9F1 File Offset: 0x0009CBF1
		internal Datatype_date()
			: base(XsdDateTimeFlags.Date)
		{
		}
	}
}
