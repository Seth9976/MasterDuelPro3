using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000148 RID: 328
	internal sealed class RegexTree
	{
		// Token: 0x060007A6 RID: 1958 RVA: 0x0002A7B2 File Offset: 0x000289B2
		internal RegexTree(RegexNode root, Hashtable caps, int[] capNumList, int capTop, Hashtable capNames, string[] capsList, RegexOptions options)
		{
			this.Root = root;
			this.Caps = caps;
			this.CapNumList = capNumList;
			this.CapTop = capTop;
			this.CapNames = capNames;
			this.CapsList = capsList;
			this.Options = options;
		}

		// Token: 0x040005FA RID: 1530
		public readonly RegexNode Root;

		// Token: 0x040005FB RID: 1531
		public readonly Hashtable Caps;

		// Token: 0x040005FC RID: 1532
		public readonly int[] CapNumList;

		// Token: 0x040005FD RID: 1533
		public readonly int CapTop;

		// Token: 0x040005FE RID: 1534
		public readonly Hashtable CapNames;

		// Token: 0x040005FF RID: 1535
		public readonly string[] CapsList;

		// Token: 0x04000600 RID: 1536
		public readonly RegexOptions Options;
	}
}
