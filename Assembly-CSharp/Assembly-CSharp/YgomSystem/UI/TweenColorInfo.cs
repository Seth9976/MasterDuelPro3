using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x0200061B RID: 1563
	public class TweenColorInfo : TweenGenerateInfo
	{
		// Token: 0x04002E29 RID: 11817
		[ColorLabelString]
		public string fromLabel;

		// Token: 0x04002E2A RID: 11818
		public Color from;

		// Token: 0x04002E2B RID: 11819
		[ColorLabelString]
		public string toLabel;

		// Token: 0x04002E2C RID: 11820
		public Color to;

		// Token: 0x04002E2D RID: 11821
		public bool isOverride;

		// Token: 0x04002E2E RID: 11822
		public bool isRecusive;
	}
}
