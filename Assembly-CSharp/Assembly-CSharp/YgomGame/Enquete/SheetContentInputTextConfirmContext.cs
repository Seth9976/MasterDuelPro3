using System;
using System.Collections.Generic;

namespace YgomGame.Enquete
{
	// Token: 0x02000C2B RID: 3115
	public class SheetContentInputTextConfirmContext : SheetContentContextBase
	{
		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x060058D1 RID: 22737 RVA: 0x000029CC File Offset: 0x00000BCC
		public override SheetContentType sheetContentType
		{
			get
			{
				return SheetContentType.none;
			}
		}

		// Token: 0x060058D2 RID: 22738 RVA: 0x000F4D25 File Offset: 0x000F2F25
		public SheetContentInputTextConfirmContext(RootContext rootContext)
			: base(null)
		{
		}

		// Token: 0x060058D3 RID: 22739 RVA: 0x000F4D25 File Offset: 0x000F2F25
		public SheetContentInputTextConfirmContext(object jsonData, RootContext rootContext)
			: base(null)
		{
		}

		// Token: 0x060058D4 RID: 22740 RVA: 0x0000216A File Offset: 0x0000036A
		public SheetContentInputTextConfirmContext Copy()
		{
			return null;
		}

		// Token: 0x060058D5 RID: 22741 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(object jsonData)
		{
		}

		// Token: 0x060058D6 RID: 22742 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SearchDependencieTextGroups(List<string> resultList)
		{
		}

		// Token: 0x040094E3 RID: 38115
		public string text;
	}
}
