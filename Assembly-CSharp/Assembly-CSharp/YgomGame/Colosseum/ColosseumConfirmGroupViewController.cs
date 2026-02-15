using System;
using System.Collections.Generic;
using YgomSystem.UI;

namespace YgomGame.Colosseum
{
	// Token: 0x02001025 RID: 4133
	public class ColosseumConfirmGroupViewController : ColosseumConfirmViewControllerBase
	{
		// Token: 0x06007C1D RID: 31773 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PushView(ViewControllerManager vcm, int logoId, int identifier = 0, Action onSuccess = null)
		{
		}

		// Token: 0x06007C1E RID: 31774 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void UpdateView()
		{
		}

		// Token: 0x06007C1F RID: 31775 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPIWcsSetRegion(int regionId)
		{
		}

		// Token: 0x0400B3E7 RID: 46055
		private int currentIndex;

		// Token: 0x0400B3E8 RID: 46056
		private List<ValueTuple<int, string>> regionList;
	}
}
