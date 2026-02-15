using System;

namespace System.Xml.Schema
{
	// Token: 0x0200025B RID: 603
	internal class Datatype_Name : Datatype_token
	{
		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001C16 RID: 7190 RVA: 0x0009ED18 File Offset: 0x0009CF18
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Name;
			}
		}
	}
}
