using System;
using System.Collections.Generic;
using YgomGame.Utility;

namespace YgomGame.Enquete
{
	// Token: 0x02000C28 RID: 3112
	public class SheetContentInputAmountContext : SheetContentContextBase
	{
		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x060058C3 RID: 22723 RVA: 0x000029CC File Offset: 0x00000BCC
		public override SheetContentType sheetContentType
		{
			get
			{
				return SheetContentType.none;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x060058C4 RID: 22724 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool isInput
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060058C5 RID: 22725 RVA: 0x000F4D25 File Offset: 0x000F2F25
		public SheetContentInputAmountContext(object jsonData, RootContext rootContext)
			: base(null)
		{
		}

		// Token: 0x060058C6 RID: 22726 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(object jsonData)
		{
		}

		// Token: 0x060058C7 RID: 22727 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SearchDependencieTextGroups(List<string> resultList)
		{
		}

		// Token: 0x040094DC RID: 38108
		public readonly GlobalTextData minText;

		// Token: 0x040094DD RID: 38109
		public readonly GlobalTextData maxText;

		// Token: 0x040094DE RID: 38110
		public int amountLength;
	}
}
