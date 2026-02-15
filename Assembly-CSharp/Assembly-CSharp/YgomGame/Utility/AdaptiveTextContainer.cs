using System;
using UnityEngine.UI;

namespace YgomGame.Utility
{
	// Token: 0x02000819 RID: 2073
	public class AdaptiveTextContainer : ContentSizeFitter
	{
		// Token: 0x06004019 RID: 16409 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Awake()
		{
		}

		// Token: 0x0600401A RID: 16410 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SetLayoutHorizontal()
		{
		}

		// Token: 0x0600401B RID: 16411 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SetLayoutVertical()
		{
		}

		// Token: 0x04003931 RID: 14641
		public int maxWidth;

		// Token: 0x04003932 RID: 14642
		public int maxHeight;

		// Token: 0x04003933 RID: 14643
		public AdaptiveTextContainer.Mode mode;

		// Token: 0x0200081A RID: 2074
		public enum Mode
		{
			// Token: 0x04003935 RID: 14645
			HorizontalEx,
			// Token: 0x04003936 RID: 14646
			VerticalEx
		}
	}
}
