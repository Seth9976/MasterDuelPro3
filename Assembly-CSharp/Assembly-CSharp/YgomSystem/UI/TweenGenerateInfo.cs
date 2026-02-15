using System;

namespace YgomSystem.UI
{
	// Token: 0x02000620 RID: 1568
	public abstract class TweenGenerateInfo : TweenInfo
	{
		// Token: 0x04002E3B RID: 11835
		public string label;

		// Token: 0x04002E3C RID: 11836
		public Tween.Easing easing;

		// Token: 0x04002E3D RID: 11837
		public Tween.Style style;

		// Token: 0x04002E3E RID: 11838
		[SecField]
		public float duration;

		// Token: 0x04002E3F RID: 11839
		[SecField]
		public float startDelay;

		// Token: 0x04002E40 RID: 11840
		public bool ignoreTimeScale;
	}
}
