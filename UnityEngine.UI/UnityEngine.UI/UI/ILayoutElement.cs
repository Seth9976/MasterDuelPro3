using System;

namespace UnityEngine.UI
{
	// Token: 0x02000048 RID: 72
	public interface ILayoutElement
	{
		// Token: 0x060002AF RID: 687
		void CalculateLayoutInputHorizontal();

		// Token: 0x060002B0 RID: 688
		void CalculateLayoutInputVertical();

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002B1 RID: 689
		float minWidth { get; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002B2 RID: 690
		float preferredWidth { get; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002B3 RID: 691
		float flexibleWidth { get; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002B4 RID: 692
		float minHeight { get; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002B5 RID: 693
		float preferredHeight { get; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002B6 RID: 694
		float flexibleHeight { get; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002B7 RID: 695
		int layoutPriority { get; }
	}
}
