using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000565 RID: 1381
	internal class GradientRemap : LinkedPoolItem<GradientRemap>
	{
		// Token: 0x060025F1 RID: 9713 RVA: 0x00096EB8 File Offset: 0x000950B8
		public void Reset()
		{
			this.origIndex = 0;
			this.destIndex = 0;
			this.location = default(RectInt);
			this.atlas = TextureId.invalid;
		}

		// Token: 0x0400131F RID: 4895
		public int origIndex;

		// Token: 0x04001320 RID: 4896
		public int destIndex;

		// Token: 0x04001321 RID: 4897
		public RectInt location;

		// Token: 0x04001322 RID: 4898
		public GradientRemap next;

		// Token: 0x04001323 RID: 4899
		public TextureId atlas;
	}
}
