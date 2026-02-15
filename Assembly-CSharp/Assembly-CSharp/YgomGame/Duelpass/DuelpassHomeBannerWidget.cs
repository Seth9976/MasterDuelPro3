using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace YgomGame.Duelpass
{
	// Token: 0x02000C38 RID: 3128
	public class DuelpassHomeBannerWidget
	{
		// Token: 0x0600592A RID: 22826 RVA: 0x00002739 File Offset: 0x00000939
		public DuelpassHomeBannerWidget(ElementObjectManager eom)
		{
		}

		// Token: 0x0600592B RID: 22827 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeComponents()
		{
		}

		// Token: 0x0600592C RID: 22828 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnLimitGettingCloser()
		{
		}

		// Token: 0x0600592D RID: 22829 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnLimitNotClose()
		{
		}

		// Token: 0x0400950F RID: 38159
		private ElementObjectManager eom;

		// Token: 0x04009510 RID: 38160
		private TMP_Text currentGradeText;

		// Token: 0x04009511 RID: 38161
		private TMP_Text nextGradeText;

		// Token: 0x04009512 RID: 38162
		private Image progressBar;

		// Token: 0x04009513 RID: 38163
		private Image normalpassWallpaper;

		// Token: 0x04009514 RID: 38164
		private Image goldpassWallpaper;

		// Token: 0x04009515 RID: 38165
		private GameObject clockIcon;

		// Token: 0x04009516 RID: 38166
		private GameObject limitDate;

		// Token: 0x04009517 RID: 38167
		private DuelpassProgressBarContext context;
	}
}
