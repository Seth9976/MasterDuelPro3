using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200014D RID: 333
	internal class TabLayout
	{
		// Token: 0x060009FD RID: 2557 RVA: 0x00030E05 File Offset: 0x0002F005
		public TabLayout(TabView tabView, bool isVertical)
		{
			this.m_TabView = tabView;
			this.m_TabHeaders = tabView.tabHeaders;
			this.m_IsVertical = isVertical;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00030E2C File Offset: 0x0002F02C
		public static float GetHeight(VisualElement t)
		{
			return t.boundingBox.height;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00030E4C File Offset: 0x0002F04C
		public static float GetWidth(VisualElement t)
		{
			return t.boundingBox.width;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00030E6C File Offset: 0x0002F06C
		public float GetTabOffset(VisualElement tab)
		{
			bool flag = !tab.visible;
			float num;
			if (flag)
			{
				num = float.NaN;
			}
			else
			{
				float pos = 0f;
				int visibleIndex = this.m_TabHeaders.IndexOf(tab);
				for (int i = 0; i < visibleIndex; i++)
				{
					VisualElement otherTab = this.m_TabHeaders[i];
					float size = (this.m_IsVertical ? TabLayout.GetHeight(otherTab) : TabLayout.GetWidth(otherTab));
					bool flag2 = float.IsNaN(size);
					if (!flag2)
					{
						pos += size;
					}
				}
				num = pos;
			}
			return num;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00030EFC File Offset: 0x0002F0FC
		private void InitOrderTabs()
		{
			if (this.m_TabHeaders == null)
			{
				this.m_TabHeaders = new List<VisualElement>();
			}
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00030F12 File Offset: 0x0002F112
		public void ReorderDisplay(int from, int to)
		{
			this.InitOrderTabs();
			this.m_TabView.ReorderTab(from, to);
		}

		// Token: 0x04000689 RID: 1673
		private TabView m_TabView;

		// Token: 0x0400068A RID: 1674
		private List<VisualElement> m_TabHeaders;

		// Token: 0x0400068B RID: 1675
		private bool m_IsVertical;
	}
}
