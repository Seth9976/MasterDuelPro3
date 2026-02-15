using System;

namespace System.Xml
{
	// Token: 0x0200002C RID: 44
	internal interface IDtdAttributeInfo
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600017B RID: 379
		string Prefix { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600017C RID: 380
		string LocalName { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600017D RID: 381
		int LineNumber { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600017E RID: 382
		int LinePosition { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600017F RID: 383
		bool IsNonCDataType { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000180 RID: 384
		bool IsDeclaredInExternal { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000181 RID: 385
		bool IsXmlAttribute { get; }
	}
}
