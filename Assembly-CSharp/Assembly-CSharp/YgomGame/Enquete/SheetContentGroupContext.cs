using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace YgomGame.Enquete
{
	// Token: 0x02000C27 RID: 3111
	public class SheetContentGroupContext : SheetContentContextBase
	{
		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x060058BB RID: 22715 RVA: 0x000029CC File Offset: 0x00000BCC
		public override SheetContentType sheetContentType
		{
			get
			{
				return SheetContentType.none;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x060058BC RID: 22716 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060058BD RID: 22717 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isMust
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x060058BE RID: 22718 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool isInput
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060058BF RID: 22719 RVA: 0x000F4D25 File Offset: 0x000F2F25
		public SheetContentGroupContext(RootContext rootContext)
			: base(null)
		{
		}

		// Token: 0x060058C0 RID: 22720 RVA: 0x000F4D25 File Offset: 0x000F2F25
		public SheetContentGroupContext(object jsonData, RootContext rootContext)
			: base(null)
		{
		}

		// Token: 0x060058C1 RID: 22721 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(object jsonData)
		{
		}

		// Token: 0x060058C2 RID: 22722 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SearchDependencieTextGroups(List<string> resultList)
		{
		}

		// Token: 0x040094DB RID: 38107
		public readonly List<ISheetContentContext> contents;
	}
}
