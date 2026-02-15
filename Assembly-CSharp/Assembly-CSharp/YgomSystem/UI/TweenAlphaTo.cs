using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x02000618 RID: 1560
	public class TweenAlphaTo : Tween
	{
		// Token: 0x060031B2 RID: 12722 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x04002E1B RID: 11803
		[SerializeField]
		public float to;

		// Token: 0x04002E1C RID: 11804
		public bool isRecusive;

		// Token: 0x04002E1D RID: 11805
		private CanvasGroup canvasGroup;

		// Token: 0x04002E1E RID: 11806
		private float canvasAlpha;

		// Token: 0x04002E1F RID: 11807
		private List<KeyValuePair<Graphic, Color>> childGraps;
	}
}
