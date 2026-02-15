using System;
using System.Collections.Generic;
using YgomSystem.Network;

namespace YgomGame.Menu
{
	// Token: 0x02000AE4 RID: 2788
	public class PvpMenuMatchingTeam_Member : PvpMenuMatchingTeam
	{
		// Token: 0x06005140 RID: 20800 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Handle CallAPIMatching(Dictionary<string, object> matchParam)
		{
			return null;
		}

		// Token: 0x06005141 RID: 20801 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCompleteMatchingHandle(Handle e)
		{
		}
	}
}
