using System;

namespace System.Xml.Schema
{
	// Token: 0x0200025A RID: 602
	internal class Datatype_NMTOKEN : Datatype_token
	{
		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001C13 RID: 7187 RVA: 0x0001544D File Offset: 0x0001364D
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NmToken;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001C14 RID: 7188 RVA: 0x0003D904 File Offset: 0x0003BB04
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.NMTOKEN;
			}
		}
	}
}
