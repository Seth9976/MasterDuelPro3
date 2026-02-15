using System;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace UnityEngine.UI
{
	// Token: 0x02000056 RID: 86
	public class MaskUtilities
	{
		// Token: 0x06000341 RID: 833 RVA: 0x0000FDCC File Offset: 0x0000DFCC
		public static void Notify2DMaskStateChanged(Component mask)
		{
			List<Component> components = CollectionPool<List<Component>, Component>.Get();
			mask.GetComponentsInChildren<Component>(components);
			for (int i = 0; i < components.Count; i++)
			{
				if (!(components[i] == null) && !(components[i].gameObject == mask.gameObject))
				{
					IClippable toNotify = components[i] as IClippable;
					if (toNotify != null)
					{
						toNotify.RecalculateClipping();
					}
				}
			}
			CollectionPool<List<Component>, Component>.Release(components);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000FE3C File Offset: 0x0000E03C
		public static void NotifyStencilStateChanged(Component mask)
		{
			List<Component> components = CollectionPool<List<Component>, Component>.Get();
			mask.GetComponentsInChildren<Component>(components);
			for (int i = 0; i < components.Count; i++)
			{
				if (!(components[i] == null) && !(components[i].gameObject == mask.gameObject))
				{
					IMaskable toNotify = components[i] as IMaskable;
					if (toNotify != null)
					{
						toNotify.RecalculateMasking();
					}
				}
			}
			CollectionPool<List<Component>, Component>.Release(components);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000FEAC File Offset: 0x0000E0AC
		public static Transform FindRootSortOverrideCanvas(Transform start)
		{
			List<Canvas> canvasList = CollectionPool<List<Canvas>, Canvas>.Get();
			start.GetComponentsInParent<Canvas>(false, canvasList);
			Canvas canvas = null;
			for (int i = 0; i < canvasList.Count; i++)
			{
				canvas = canvasList[i];
				if (canvas.overrideSorting)
				{
					break;
				}
			}
			CollectionPool<List<Canvas>, Canvas>.Release(canvasList);
			if (!(canvas != null))
			{
				return null;
			}
			return canvas.transform;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000FF04 File Offset: 0x0000E104
		public static int GetStencilDepth(Transform transform, Transform stopAfter)
		{
			int depth = 0;
			if (transform == stopAfter)
			{
				return depth;
			}
			Transform t = transform.parent;
			List<Mask> components = CollectionPool<List<Mask>, Mask>.Get();
			while (t != null)
			{
				t.GetComponents<Mask>(components);
				for (int i = 0; i < components.Count; i++)
				{
					if (components[i] != null && components[i].MaskEnabled() && components[i].graphic.IsActive())
					{
						depth++;
						break;
					}
				}
				if (t == stopAfter)
				{
					break;
				}
				t = t.parent;
			}
			CollectionPool<List<Mask>, Mask>.Release(components);
			return depth;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000FF9C File Offset: 0x0000E19C
		public static bool IsDescendantOrSelf(Transform father, Transform child)
		{
			if (father == null || child == null)
			{
				return false;
			}
			if (father == child)
			{
				return true;
			}
			while (child.parent != null)
			{
				if (child.parent == father)
				{
					return true;
				}
				child = child.parent;
			}
			return false;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000FFF0 File Offset: 0x0000E1F0
		public static RectMask2D GetRectMaskForClippable(IClippable clippable)
		{
			List<RectMask2D> rectMaskComponents = CollectionPool<List<RectMask2D>, RectMask2D>.Get();
			List<Canvas> canvasComponents = CollectionPool<List<Canvas>, Canvas>.Get();
			RectMask2D componentToReturn = null;
			clippable.gameObject.GetComponentsInParent<RectMask2D>(false, rectMaskComponents);
			if (rectMaskComponents.Count > 0)
			{
				for (int rmi = 0; rmi < rectMaskComponents.Count; rmi++)
				{
					componentToReturn = rectMaskComponents[rmi];
					if (componentToReturn.gameObject == clippable.gameObject)
					{
						componentToReturn = null;
					}
					else
					{
						if (componentToReturn.isActiveAndEnabled)
						{
							clippable.gameObject.GetComponentsInParent<Canvas>(false, canvasComponents);
							for (int i = canvasComponents.Count - 1; i >= 0; i--)
							{
								if (!MaskUtilities.IsDescendantOrSelf(canvasComponents[i].transform, componentToReturn.transform) && canvasComponents[i].overrideSorting)
								{
									componentToReturn = null;
									break;
								}
							}
							break;
						}
						componentToReturn = null;
					}
				}
			}
			CollectionPool<List<RectMask2D>, RectMask2D>.Release(rectMaskComponents);
			CollectionPool<List<Canvas>, Canvas>.Release(canvasComponents);
			return componentToReturn;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000100C8 File Offset: 0x0000E2C8
		public static void GetRectMasksForClip(RectMask2D clipper, List<RectMask2D> masks)
		{
			masks.Clear();
			List<Canvas> canvasComponents = CollectionPool<List<Canvas>, Canvas>.Get();
			List<RectMask2D> rectMaskComponents = CollectionPool<List<RectMask2D>, RectMask2D>.Get();
			clipper.transform.GetComponentsInParent<RectMask2D>(false, rectMaskComponents);
			if (rectMaskComponents.Count > 0)
			{
				clipper.transform.GetComponentsInParent<Canvas>(false, canvasComponents);
				for (int i = rectMaskComponents.Count - 1; i >= 0; i--)
				{
					if (rectMaskComponents[i].IsActive())
					{
						bool shouldAdd = true;
						for (int j = canvasComponents.Count - 1; j >= 0; j--)
						{
							if (!MaskUtilities.IsDescendantOrSelf(canvasComponents[j].transform, rectMaskComponents[i].transform) && canvasComponents[j].overrideSorting)
							{
								shouldAdd = false;
								break;
							}
						}
						if (shouldAdd)
						{
							masks.Add(rectMaskComponents[i]);
						}
					}
				}
			}
			CollectionPool<List<RectMask2D>, RectMask2D>.Release(rectMaskComponents);
			CollectionPool<List<Canvas>, Canvas>.Release(canvasComponents);
		}
	}
}
