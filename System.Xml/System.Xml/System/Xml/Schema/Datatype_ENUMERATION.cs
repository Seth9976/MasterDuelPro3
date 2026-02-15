using System;

namespace System.Xml.Schema
{
	// Token: 0x02000271 RID: 625
	internal class Datatype_ENUMERATION : Datatype_NMTOKEN
	{
		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001C90 RID: 7312 RVA: 0x0003BB5A File Offset: 0x00039D5A
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.ENUMERATION;
			}
		}
	}
}
