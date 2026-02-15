using System;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace UnityEngine.UI
{
	// Token: 0x02000052 RID: 82
	public static class LayoutUtility
	{
		// Token: 0x0600031C RID: 796 RVA: 0x0000F7A8 File Offset: 0x0000D9A8
		public static float GetMinSize(RectTransform rect, int axis)
		{
			if (axis != 0)
			{
				return LayoutUtility.GetMinHeight(rect);
			}
			return LayoutUtility.GetMinWidth(rect);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000F7BA File Offset: 0x0000D9BA
		public static float GetPreferredSize(RectTransform rect, int axis)
		{
			if (axis != 0)
			{
				return LayoutUtility.GetPreferredHeight(rect);
			}
			return LayoutUtility.GetPreferredWidth(rect);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000F7CC File Offset: 0x0000D9CC
		public static float GetFlexibleSize(RectTransform rect, int axis)
		{
			if (axis != 0)
			{
				return LayoutUtility.GetFlexibleHeight(rect);
			}
			return LayoutUtility.GetFlexibleWidth(rect);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000F7DE File Offset: 0x0000D9DE
		public static float GetMinWidth(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minWidth, 0f);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000F80C File Offset: 0x0000DA0C
		public static float GetPreferredWidth(RectTransform rect)
		{
			return Mathf.Max(LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minWidth, 0f), LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.preferredWidth, 0f));
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000F872 File Offset: 0x0000DA72
		public static float GetFlexibleWidth(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.flexibleWidth, 0f);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000F89E File Offset: 0x0000DA9E
		public static float GetMinHeight(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minHeight, 0f);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000F8CC File Offset: 0x0000DACC
		public static float GetPreferredHeight(RectTransform rect)
		{
			return Mathf.Max(LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minHeight, 0f), LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.preferredHeight, 0f));
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000F932 File Offset: 0x0000DB32
		public static float GetFlexibleHeight(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.flexibleHeight, 0f);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000F960 File Offset: 0x0000DB60
		public static float GetLayoutProperty(RectTransform rect, Func<ILayoutElement, float> property, float defaultValue)
		{
			ILayoutElement dummy;
			return LayoutUtility.GetLayoutProperty(rect, property, defaultValue, out dummy);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000F978 File Offset: 0x0000DB78
		public static float GetLayoutProperty(RectTransform rect, Func<ILayoutElement, float> property, float defaultValue, out ILayoutElement source)
		{
			source = null;
			if (rect == null)
			{
				return 0f;
			}
			float min = defaultValue;
			int maxPriority = int.MinValue;
			List<Component> components = CollectionPool<List<Component>, Component>.Get();
			rect.GetComponents(typeof(ILayoutElement), components);
			int componentsCount = components.Count;
			for (int i = 0; i < componentsCount; i++)
			{
				ILayoutElement layoutComp = components[i] as ILayoutElement;
				if (!(layoutComp is Behaviour) || ((Behaviour)layoutComp).isActiveAndEnabled)
				{
					int priority = layoutComp.layoutPriority;
					if (priority >= maxPriority)
					{
						float prop = property(layoutComp);
						if (prop >= 0f)
						{
							if (priority > maxPriority)
							{
								min = prop;
								maxPriority = priority;
								source = layoutComp;
							}
							else if (prop > min)
							{
								min = prop;
								source = layoutComp;
							}
						}
					}
				}
			}
			CollectionPool<List<Component>, Component>.Release(components);
			return min;
		}
	}
}
