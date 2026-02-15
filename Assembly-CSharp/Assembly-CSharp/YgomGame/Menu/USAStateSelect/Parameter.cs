using System;
using System.Collections.Generic;

namespace YgomGame.Menu.USAStateSelect
{
	// Token: 0x02000B07 RID: 2823
	public class Parameter
	{
		// Token: 0x0400907A RID: 36986
		public int defaultState;

		// Token: 0x0400907B RID: 36987
		public IReadOnlyList<int> codeList;

		// Token: 0x0400907C RID: 36988
		public IReadOnlyList<string> nameList;

		// Token: 0x0400907D RID: 36989
		public Action<int> resultCallback;
	}
}
