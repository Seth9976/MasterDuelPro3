using System;
using System.Collections.Generic;

namespace YgomGame.Menu.CountrySelect
{
	// Token: 0x02000B0C RID: 2828
	public class Parameter
	{
		// Token: 0x0400908E RID: 37006
		public int defaultCountry;

		// Token: 0x0400908F RID: 37007
		public IReadOnlyList<int> codeList;

		// Token: 0x04009090 RID: 37008
		public IReadOnlyList<string> nameList;

		// Token: 0x04009091 RID: 37009
		public Action<int> resultCallback;
	}
}
