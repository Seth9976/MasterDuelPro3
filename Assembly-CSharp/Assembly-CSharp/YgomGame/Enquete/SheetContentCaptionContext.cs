using System;
using System.Collections.Generic;
using YgomGame.Utility;

namespace YgomGame.Enquete
{
	// Token: 0x02000C25 RID: 3109
	public class SheetContentCaptionContext : SheetContentContextBase
	{
		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x060058AE RID: 22702 RVA: 0x000029CC File Offset: 0x00000BCC
		public override SheetContentType sheetContentType
		{
			get
			{
				return SheetContentType.none;
			}
		}

		// Token: 0x060058AF RID: 22703 RVA: 0x000F4D25 File Offset: 0x000F2F25
		public SheetContentCaptionContext(RootContext rootContext)
			: base(null)
		{
		}

		// Token: 0x060058B0 RID: 22704 RVA: 0x000F4D25 File Offset: 0x000F2F25
		public SheetContentCaptionContext(object jsonData, RootContext rootContext)
			: base(null)
		{
		}

		// Token: 0x060058B1 RID: 22705 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(object jsonData)
		{
		}

		// Token: 0x060058B2 RID: 22706 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SearchDependencieTextGroups(List<string> resultList)
		{
		}

		// Token: 0x040094D9 RID: 38105
		public readonly GlobalTextData text;
	}
}
