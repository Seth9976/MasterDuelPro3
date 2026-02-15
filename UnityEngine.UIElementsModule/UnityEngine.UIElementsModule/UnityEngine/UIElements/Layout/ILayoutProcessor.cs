using System;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x02000576 RID: 1398
	internal interface ILayoutProcessor
	{
		// Token: 0x060026B3 RID: 9907
		void CalculateLayout(LayoutNode node, float parentWidth, float parentHeight, LayoutDirection parentDirection);
	}
}
