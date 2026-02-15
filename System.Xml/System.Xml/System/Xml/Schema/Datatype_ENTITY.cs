using System;

namespace System.Xml.Schema
{
	// Token: 0x0200025F RID: 607
	internal class Datatype_ENTITY : Datatype_NCName
	{
		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x0009ED79 File Offset: 0x0009CF79
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Entity;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001C22 RID: 7202 RVA: 0x0003B0B7 File Offset: 0x000392B7
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.ENTITY;
			}
		}
	}
}
