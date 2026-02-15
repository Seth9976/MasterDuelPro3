using System;

namespace System.Xml.Schema
{
	// Token: 0x0200025D RID: 605
	internal class Datatype_ID : Datatype_NCName
	{
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001C1B RID: 7195 RVA: 0x0009ED69 File Offset: 0x0009CF69
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Id;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001C1C RID: 7196 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.ID;
			}
		}
	}
}
