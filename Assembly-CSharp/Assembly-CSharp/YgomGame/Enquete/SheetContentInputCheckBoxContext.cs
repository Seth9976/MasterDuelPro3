using System;
using System.Collections.Generic;
using YgomGame.Utility;

namespace YgomGame.Enquete
{
	// Token: 0x02000C29 RID: 3113
	public class SheetContentInputCheckBoxContext : SheetContentContextBase
	{
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x060058C8 RID: 22728 RVA: 0x000029CC File Offset: 0x00000BCC
		public override SheetContentType sheetContentType
		{
			get
			{
				return SheetContentType.none;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x060058C9 RID: 22729 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool isInput
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060058CA RID: 22730 RVA: 0x000F4D25 File Offset: 0x000F2F25
		public SheetContentInputCheckBoxContext(object jsonData, RootContext rootContext)
			: base(null)
		{
		}

		// Token: 0x060058CB RID: 22731 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(object jsonData)
		{
		}

		// Token: 0x060058CC RID: 22732 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SearchDependencieTextGroups(List<string> resultList)
		{
		}

		// Token: 0x040094DF RID: 38111
		public readonly List<SheetContentInputCheckBoxContext.ContentContext> contents;

		// Token: 0x040094E0 RID: 38112
		public int min;

		// Token: 0x040094E1 RID: 38113
		public int max;

		// Token: 0x02000C2A RID: 3114
		public class ContentContext : ContextBase
		{
			// Token: 0x060058CD RID: 22733 RVA: 0x000F4D1D File Offset: 0x000F2F1D
			public ContentContext()
			{
			}

			// Token: 0x060058CE RID: 22734 RVA: 0x000F4D1D File Offset: 0x000F2F1D
			public ContentContext(object jsonData)
			{
			}

			// Token: 0x060058CF RID: 22735 RVA: 0x0000216D File Offset: 0x0000036D
			public override void Import(object jsonData)
			{
			}

			// Token: 0x060058D0 RID: 22736 RVA: 0x0000216D File Offset: 0x0000036D
			public override void SearchDependencieTextGroups(List<string> resultList)
			{
			}

			// Token: 0x040094E2 RID: 38114
			public readonly GlobalTextData text;
		}
	}
}
