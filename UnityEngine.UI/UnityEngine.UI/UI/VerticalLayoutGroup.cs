using System;

namespace UnityEngine.UI
{
	// Token: 0x02000054 RID: 84
	[AddComponentMenu("Layout/Vertical Layout Group", 151)]
	public class VerticalLayoutGroup : HorizontalOrVerticalLayoutGroup
	{
		// Token: 0x06000331 RID: 817 RVA: 0x0000E4A5 File Offset: 0x0000C6A5
		protected VerticalLayoutGroup()
		{
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000FA74 File Offset: 0x0000DC74
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			base.CalcAlongAxis(0, true);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000FA84 File Offset: 0x0000DC84
		public override void CalculateLayoutInputVertical()
		{
			base.CalcAlongAxis(1, true);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000FA8E File Offset: 0x0000DC8E
		public override void SetLayoutHorizontal()
		{
			base.SetChildrenAlongAxis(0, true);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000FA98 File Offset: 0x0000DC98
		public override void SetLayoutVertical()
		{
			base.SetChildrenAlongAxis(1, true);
		}
	}
}
