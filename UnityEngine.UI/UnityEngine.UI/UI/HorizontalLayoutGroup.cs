using System;

namespace UnityEngine.UI
{
	// Token: 0x02000046 RID: 70
	[AddComponentMenu("Layout/Horizontal Layout Group", 150)]
	public class HorizontalLayoutGroup : HorizontalOrVerticalLayoutGroup
	{
		// Token: 0x06000296 RID: 662 RVA: 0x0000E4A5 File Offset: 0x0000C6A5
		protected HorizontalLayoutGroup()
		{
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000E4AD File Offset: 0x0000C6AD
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			base.CalcAlongAxis(0, false);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000E4BD File Offset: 0x0000C6BD
		public override void CalculateLayoutInputVertical()
		{
			base.CalcAlongAxis(1, false);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000E4C7 File Offset: 0x0000C6C7
		public override void SetLayoutHorizontal()
		{
			base.SetChildrenAlongAxis(0, false);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000E4D1 File Offset: 0x0000C6D1
		public override void SetLayoutVertical()
		{
			base.SetChildrenAlongAxis(1, false);
		}
	}
}
