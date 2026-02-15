using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace YgomGame.Duel
{
	// Token: 0x02000ECA RID: 3786
	public class ManaSet : MonoBehaviour
	{
		// Token: 0x06006E67 RID: 28263 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x06006E68 RID: 28264 RVA: 0x0000216D File Offset: 0x0000036D
		public void Play(Engine.CounterType type, int beforeNum, int afterNum, Vector3 targetAnchor)
		{
		}

		// Token: 0x06006E69 RID: 28265 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06006E6A RID: 28266 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdatePosition()
		{
		}

		// Token: 0x0400A948 RID: 43336
		private ElementObjectManager ui;

		// Token: 0x0400A949 RID: 43337
		private Image counterIcon;

		// Token: 0x0400A94A RID: 43338
		private TextMeshProUGUI text;

		// Token: 0x0400A94B RID: 43339
		private Vector3 targetAnchor;

		// Token: 0x0400A94C RID: 43340
		private int afterNum;

		// Token: 0x0400A94D RID: 43341
		private int currentNum;

		// Token: 0x0400A94E RID: 43342
		private int deltaNum;

		// Token: 0x0400A94F RID: 43343
		private float timer;

		// Token: 0x0400A950 RID: 43344
		private const float timeCounterChange = 1f;

		// Token: 0x0400A951 RID: 43345
		private const float timeAfter = 0.3f;

		// Token: 0x0400A952 RID: 43346
		private ManaSet.Step step;

		// Token: 0x02000ECB RID: 3787
		private enum Step
		{
			// Token: 0x0400A954 RID: 43348
			None,
			// Token: 0x0400A955 RID: 43349
			BeforeNum,
			// Token: 0x0400A956 RID: 43350
			ChangeNum,
			// Token: 0x0400A957 RID: 43351
			AfterNum,
			// Token: 0x0400A958 RID: 43352
			Out
		}
	}
}
