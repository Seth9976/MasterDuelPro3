using System;

namespace YgomGame.Enquete
{
	// Token: 0x02000C1D RID: 3101
	public interface ISheetContentContext : IContext
	{
		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06005880 RID: 22656
		SheetContentType sheetContentType { get; }

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06005881 RID: 22657
		RootContext rootContext { get; }

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06005882 RID: 22658
		// (set) Token: 0x06005883 RID: 22659
		SheetContentGroupContext groupContext { get; set; }

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06005884 RID: 22660
		bool isInput { get; }
	}
}
