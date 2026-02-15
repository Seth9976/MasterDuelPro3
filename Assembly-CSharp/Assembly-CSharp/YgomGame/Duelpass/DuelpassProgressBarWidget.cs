using System;
using TMPro;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace YgomGame.Duelpass
{
	// Token: 0x02000C3D RID: 3133
	public class DuelpassProgressBarWidget
	{
		// Token: 0x0600595B RID: 22875 RVA: 0x00002739 File Offset: 0x00000939
		public DuelpassProgressBarWidget(ElementObjectManager eom)
		{
		}

		// Token: 0x0600595C RID: 22876 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateContents()
		{
		}

		// Token: 0x0600595D RID: 22877 RVA: 0x0000216D File Offset: 0x0000036D
		public void UnLockGoldPass()
		{
		}

		// Token: 0x04009520 RID: 38176
		private TMP_Text currentGradeText;

		// Token: 0x04009521 RID: 38177
		private TMP_Text nextGradeText;

		// Token: 0x04009522 RID: 38178
		private Image progressBar;

		// Token: 0x04009523 RID: 38179
		private Image normalpassWallpaper;

		// Token: 0x04009524 RID: 38180
		private Image goldpassWallpaper;

		// Token: 0x04009525 RID: 38181
		private DuelpassProgressBarContext context;
	}
}
