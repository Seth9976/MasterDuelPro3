using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x02000616 RID: 1558
	public class TweenAlpha : Tween
	{
		// Token: 0x060031AE RID: 12718 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x04002E13 RID: 11795
		[SerializeField]
		public float from;

		// Token: 0x04002E14 RID: 11796
		[SerializeField]
		public float to;

		// Token: 0x04002E15 RID: 11797
		public bool isRecusive;

		// Token: 0x04002E16 RID: 11798
		private CanvasGroup canvasGroup;

		// Token: 0x04002E17 RID: 11799
		private List<KeyValuePair<Graphic, Color>> childGraps;
	}
}
