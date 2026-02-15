using System;

namespace System.Xml
{
	// Token: 0x0200002E RID: 46
	internal interface IDtdEntityInfo
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000186 RID: 390
		string Name { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000187 RID: 391
		bool IsExternal { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000188 RID: 392
		bool IsDeclaredInExternal { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000189 RID: 393
		bool IsUnparsedEntity { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600018A RID: 394
		bool IsParameterEntity { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600018B RID: 395
		string BaseUriString { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600018C RID: 396
		string DeclaredUriString { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600018D RID: 397
		string SystemId { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600018E RID: 398
		string PublicId { get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600018F RID: 399
		string Text { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000190 RID: 400
		int LineNumber { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000191 RID: 401
		int LinePosition { get; }
	}
}
