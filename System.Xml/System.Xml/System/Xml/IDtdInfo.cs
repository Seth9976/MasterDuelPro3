using System;

namespace System.Xml
{
	// Token: 0x0200002A RID: 42
	internal interface IDtdInfo
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000172 RID: 370
		XmlQualifiedName Name { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000173 RID: 371
		string InternalDtdSubset { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000174 RID: 372
		bool HasDefaultAttributes { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000175 RID: 373
		bool HasNonCDataAttributes { get; }

		// Token: 0x06000176 RID: 374
		IDtdAttributeListInfo LookupAttributeList(string prefix, string localName);

		// Token: 0x06000177 RID: 375
		IDtdEntityInfo LookupEntity(string name);
	}
}
