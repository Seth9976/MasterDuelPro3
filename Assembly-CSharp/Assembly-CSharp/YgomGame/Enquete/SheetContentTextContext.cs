using System;
using System.Collections.Generic;
using YgomGame.Utility;

namespace YgomGame.Enquete
{
	// Token: 0x02000C2E RID: 3118
	public class SheetContentTextContext : SheetContentContextBase
	{
		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x060058E3 RID: 22755 RVA: 0x000029CC File Offset: 0x00000BCC
		public override SheetContentType sheetContentType
		{
			get
			{
				return SheetContentType.none;
			}
		}

		// Token: 0x060058E4 RID: 22756 RVA: 0x000F4D25 File Offset: 0x000F2F25
		public SheetContentTextContext(RootContext rootContext)
			: base(null)
		{
		}

		// Token: 0x060058E5 RID: 22757 RVA: 0x000F4D25 File Offset: 0x000F2F25
		public SheetContentTextContext(object jsonData, RootContext rootContext)
			: base(null)
		{
		}

		// Token: 0x060058E6 RID: 22758 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(object jsonData)
		{
		}

		// Token: 0x060058E7 RID: 22759 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SearchDependencieTextGroups(List<string> resultList)
		{
		}

		// Token: 0x040094E4 RID: 38116
		public readonly GlobalTextData text;

		// Token: 0x040094E5 RID: 38117
		public bool isLabel;
	}
}
