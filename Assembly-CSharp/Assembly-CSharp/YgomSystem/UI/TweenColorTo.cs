using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x0200061C RID: 1564
	public class TweenColorTo : Tween
	{
		// Token: 0x060031BA RID: 12730 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x04002E2F RID: 11823
		[SerializeField]
		public ColorLabelProperty toLabel;

		// Token: 0x04002E30 RID: 11824
		[SerializeField]
		public Color to;

		// Token: 0x04002E31 RID: 11825
		public bool isRecusive;

		// Token: 0x04002E32 RID: 11826
		private List<KeyValuePair<Graphic, Color>> childGraps;
	}
}
