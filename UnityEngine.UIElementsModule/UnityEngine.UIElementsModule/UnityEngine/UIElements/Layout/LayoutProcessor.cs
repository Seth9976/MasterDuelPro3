using System;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x02000577 RID: 1399
	internal static class LayoutProcessor
	{
		// Token: 0x060026B4 RID: 9908 RVA: 0x0009A1CF File Offset: 0x000983CF
		public static void CalculateLayout(LayoutNode node, float parentWidth, float parentHeight, LayoutDirection parentDirection)
		{
			LayoutProcessor.s_Processor.CalculateLayout(node, parentWidth, parentHeight, parentDirection);
		}

		// Token: 0x0400137B RID: 4987
		private static ILayoutProcessor s_Processor = new LayoutProcessorNative();
	}
}
