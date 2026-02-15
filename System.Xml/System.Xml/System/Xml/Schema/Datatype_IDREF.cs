using System;

namespace System.Xml.Schema
{
	// Token: 0x0200025E RID: 606
	internal class Datatype_IDREF : Datatype_NCName
	{
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001C1E RID: 7198 RVA: 0x0009ED75 File Offset: 0x0009CF75
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Idref;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x0003A73C File Offset: 0x0003893C
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.IDREF;
			}
		}
	}
}
