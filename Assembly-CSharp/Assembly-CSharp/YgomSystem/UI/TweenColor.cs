using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x0200061A RID: 1562
	public class TweenColor : Tween
	{
		// Token: 0x060031B6 RID: 12726 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x04002E22 RID: 11810
		[SerializeField]
		[ColorLabelString]
		public string fromLabel;

		// Token: 0x04002E23 RID: 11811
		[SerializeField]
		public Color from;

		// Token: 0x04002E24 RID: 11812
		[ColorLabelString]
		[SerializeField]
		public string toLabel;

		// Token: 0x04002E25 RID: 11813
		[SerializeField]
		public Color to;

		// Token: 0x04002E26 RID: 11814
		public bool isOverride;

		// Token: 0x04002E27 RID: 11815
		public bool isRecusive;

		// Token: 0x04002E28 RID: 11816
		private List<KeyValuePair<Graphic, Color>> childGraps;
	}
}
