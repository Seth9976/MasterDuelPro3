using System;
using System.Collections.Generic;
using YgomSystem.Network;

namespace YgomGame.Menu
{
	// Token: 0x02000AE3 RID: 2787
	public class PvpMenuMatchingTeam_Leader : PvpMenuMatchingTeam
	{
		// Token: 0x0600513D RID: 20797 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Handle CallAPIMatching(Dictionary<string, object> matchParam)
		{
			return null;
		}

		// Token: 0x0600513E RID: 20798 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCompleteMatchingHandle(Handle e)
		{
		}
	}
}
