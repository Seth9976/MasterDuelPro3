using System;
using System.Collections.Generic;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B32 RID: 2866
	public interface IAsyncProgressContainer
	{
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x0600539B RID: 21403
		IReadOnlyList<IAsyncProgressContent> asyncProgressContents { get; }
	}
}
