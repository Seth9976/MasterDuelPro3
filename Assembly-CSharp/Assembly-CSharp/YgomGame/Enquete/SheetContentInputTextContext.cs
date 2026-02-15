using System;
using System.Collections.Generic;

namespace YgomGame.Enquete
{
	// Token: 0x02000C2C RID: 3116
	public class SheetContentInputTextContext : SheetContentContextBase
	{
		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x060058D7 RID: 22743 RVA: 0x000029CC File Offset: 0x00000BCC
		public override SheetContentType sheetContentType
		{
			get
			{
				return SheetContentType.none;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x060058D8 RID: 22744 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool isInput
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060058D9 RID: 22745 RVA: 0x000F4D25 File Offset: 0x000F2F25
		public SheetContentInputTextContext(RootContext rootContext)
			: base(null)
		{
		}

		// Token: 0x060058DA RID: 22746 RVA: 0x000F4D25 File Offset: 0x000F2F25
		public SheetContentInputTextContext(object jsonData, RootContext rootContext)
			: base(null)
		{
		}

		// Token: 0x060058DB RID: 22747 RVA: 0x0000216A File Offset: 0x0000036A
		public SheetContentInputTextContext Copy()
		{
			return null;
		}

		// Token: 0x060058DC RID: 22748 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(object jsonData)
		{
		}

		// Token: 0x060058DD RID: 22749 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SearchDependencieTextGroups(List<string> resultList)
		{
		}
	}
}
