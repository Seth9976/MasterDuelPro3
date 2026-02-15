using System;
using System.Collections.Generic;

namespace YgomGame.Enquete
{
	// Token: 0x02000C31 RID: 3121
	public class SheetContext : ContextBase
	{
		// Token: 0x060058ED RID: 22765 RVA: 0x000F4D1D File Offset: 0x000F2F1D
		public SheetContext()
		{
		}

		// Token: 0x060058EE RID: 22766 RVA: 0x000F4D1D File Offset: 0x000F2F1D
		public SheetContext(object jsonData, RootContext rootContext)
		{
		}

		// Token: 0x060058EF RID: 22767 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(object jsonData)
		{
		}

		// Token: 0x060058F0 RID: 22768 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SearchDependencieTextGroups(List<string> resultList)
		{
		}

		// Token: 0x060058F1 RID: 22769 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool ContainsMust()
		{
			return false;
		}

		// Token: 0x040094F1 RID: 38129
		public readonly List<ISheetContentContext> contents;

		// Token: 0x040094F2 RID: 38130
		public readonly RootContext rootContext;
	}
}
