using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace YgomGame.Duelpass
{
	// Token: 0x02000C41 RID: 3137
	public class DuelpassResultProgressBarWidget
	{
		// Token: 0x06005979 RID: 22905 RVA: 0x00002739 File Offset: 0x00000939
		public DuelpassResultProgressBarWidget(ElementObjectManager eom)
		{
		}

		// Token: 0x0600597A RID: 22906 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartGradeProgressAnimation(MonoBehaviour coroutineStarter)
		{
		}

		// Token: 0x0600597B RID: 22907 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator ProgressAnimationCoroutine()
		{
			return null;
		}

		// Token: 0x04009533 RID: 38195
		private TMP_Text currentGradeText;

		// Token: 0x04009534 RID: 38196
		private TMP_Text nextGradeText;

		// Token: 0x04009535 RID: 38197
		private Image progressBar;

		// Token: 0x04009536 RID: 38198
		private Image normalpassWallpaper;

		// Token: 0x04009537 RID: 38199
		private Image goldpassWallpaper;

		// Token: 0x04009538 RID: 38200
		private DuelpassResultProgressBarContext context;

		// Token: 0x04009539 RID: 38201
		private float time100;

		// Token: 0x0400953A RID: 38202
		private float dulationTime;

		// Token: 0x0400953B RID: 38203
		public Action onStartAnimation;

		// Token: 0x0400953C RID: 38204
		public Action onEndAnimation;

		// Token: 0x0400953D RID: 38205
		public Action<int> onGradeUpInAnimation;
	}
}
