using System;
using System.Collections.Generic;

namespace YgomGame.Enquete
{
	// Token: 0x02000C2D RID: 3117
	public class SheetContentSpacerContext : SheetContentContextBase
	{
		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x060058DE RID: 22750 RVA: 0x000029CC File Offset: 0x00000BCC
		public override SheetContentType sheetContentType
		{
			get
			{
				return SheetContentType.none;
			}
		}

		// Token: 0x060058DF RID: 22751 RVA: 0x000F4D25 File Offset: 0x000F2F25
		public SheetContentSpacerContext(RootContext rootContext)
			: base(null)
		{
		}

		// Token: 0x060058E0 RID: 22752 RVA: 0x000F4D25 File Offset: 0x000F2F25
		public SheetContentSpacerContext(object jsonData, RootContext rootContext)
			: base(null)
		{
		}

		// Token: 0x060058E1 RID: 22753 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(object jsonData)
		{
		}

		// Token: 0x060058E2 RID: 22754 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SearchDependencieTextGroups(List<string> resultList)
		{
		}
	}
}
