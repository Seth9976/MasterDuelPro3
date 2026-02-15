using System;
using System.Collections.Generic;
using YgomGame.Utility;

namespace YgomGame.Enquete
{
	// Token: 0x02000C24 RID: 3108
	public class RootContext : ContextBase
	{
		// Token: 0x060058AB RID: 22699 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(object jsonData)
		{
		}

		// Token: 0x060058AC RID: 22700 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SearchDependencieTextGroups(List<string> resultList)
		{
		}

		// Token: 0x040094D6 RID: 38102
		public readonly GlobalTextData title;

		// Token: 0x040094D7 RID: 38103
		public readonly List<SheetContext> sheets;

		// Token: 0x040094D8 RID: 38104
		public bool isGuideMust;
	}
}
