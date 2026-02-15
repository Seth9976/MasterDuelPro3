using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x0200060B RID: 1547
	public class TouchParticle : uGuiParticleBase
	{
		// Token: 0x06003157 RID: 12631 RVA: 0x0000216D File Offset: 0x0000036D
		private void CreateTouchParticle(float px, float py, int n, float rs, float ss)
		{
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x0000216D File Offset: 0x0000036D
		public static void EffectDisable()
		{
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x0000216D File Offset: 0x0000036D
		public static void EffectEnable()
		{
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Update()
		{
		}

		// Token: 0x04002DBE RID: 11710
		private static int TouchEnableCount;

		// Token: 0x04002DBF RID: 11711
		public float emissionSpan;

		// Token: 0x04002DC0 RID: 11712
		public float moveEmissionSpan;

		// Token: 0x04002DC1 RID: 11713
		public float lifeTime;

		// Token: 0x04002DC2 RID: 11714
		public float startScaleMin;

		// Token: 0x04002DC3 RID: 11715
		public float startScaleMax;

		// Token: 0x04002DC4 RID: 11716
		public float endScale;

		// Token: 0x04002DC5 RID: 11717
		public int startId;

		// Token: 0x04002DC6 RID: 11718
		public int endId;

		// Token: 0x04002DC7 RID: 11719
		public float rscaleMin;

		// Token: 0x04002DC8 RID: 11720
		public float rscaleMax;

		// Token: 0x04002DC9 RID: 11721
		public float totalscale;

		// Token: 0x04002DCA RID: 11722
		private float emission;

		// Token: 0x04002DCB RID: 11723
		private float rscale;

		// Token: 0x04002DCC RID: 11724
		private Color32[] scolors;

		// Token: 0x04002DCD RID: 11725
		private Color32[] ecolors;

		// Token: 0x04002DCE RID: 11726
		private int pointingBit;

		// Token: 0x04002DCF RID: 11727
		private int pointingTrgBit;

		// Token: 0x04002DD0 RID: 11728
		private int pointingNtrgBit;

		// Token: 0x04002DD1 RID: 11729
		private int pointingMoveBit;

		// Token: 0x04002DD2 RID: 11730
		private Vector2[] pointingPos;
	}
}
