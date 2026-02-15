using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.Pool;

namespace UnityEngine.UI
{
	// Token: 0x02000050 RID: 80
	public class LayoutRebuilder : ICanvasElement
	{
		// Token: 0x060002FF RID: 767 RVA: 0x0000F217 File Offset: 0x0000D417
		private void Initialize(RectTransform controller)
		{
			this.m_ToRebuild = controller;
			this.m_CachedHashFromTransform = controller.GetHashCode();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000F22C File Offset: 0x0000D42C
		private void Clear()
		{
			this.m_ToRebuild = null;
			this.m_CachedHashFromTransform = 0;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000F23C File Offset: 0x0000D43C
		static LayoutRebuilder()
		{
			RectTransform.reapplyDrivenProperties += LayoutRebuilder.ReapplyDrivenProperties;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000F28E File Offset: 0x0000D48E
		private static void ReapplyDrivenProperties(RectTransform driven)
		{
			LayoutRebuilder.MarkLayoutForRebuild(driven);
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000F296 File Offset: 0x0000D496
		public Transform transform
		{
			get
			{
				return this.m_ToRebuild;
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000F29E File Offset: 0x0000D49E
		public bool IsDestroyed()
		{
			return this.m_ToRebuild == null;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000F2AC File Offset: 0x0000D4AC
		private static void StripDisabledBehavioursFromList(List<Component> components)
		{
			components.RemoveAll((Component e) => e is Behaviour && !((Behaviour)e).isActiveAndEnabled);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000F2D4 File Offset: 0x0000D4D4
		public static void ForceRebuildLayoutImmediate(RectTransform layoutRoot)
		{
			LayoutRebuilder rebuilder = LayoutRebuilder.s_Rebuilders.Get();
			rebuilder.Initialize(layoutRoot);
			rebuilder.Rebuild(CanvasUpdate.Layout);
			LayoutRebuilder.s_Rebuilders.Release(rebuilder);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000F308 File Offset: 0x0000D508
		public void Rebuild(CanvasUpdate executing)
		{
			if (executing == CanvasUpdate.Layout)
			{
				this.PerformLayoutCalculation(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutElement).CalculateLayoutInputHorizontal();
				});
				this.PerformLayoutControl(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutController).SetLayoutHorizontal();
				});
				this.PerformLayoutCalculation(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutElement).CalculateLayoutInputVertical();
				});
				this.PerformLayoutControl(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutController).SetLayoutVertical();
				});
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000F3C8 File Offset: 0x0000D5C8
		private void PerformLayoutControl(RectTransform rect, UnityAction<Component> action)
		{
			if (rect == null)
			{
				return;
			}
			List<Component> components = CollectionPool<List<Component>, Component>.Get();
			rect.GetComponents(typeof(ILayoutController), components);
			LayoutRebuilder.StripDisabledBehavioursFromList(components);
			if (components.Count > 0)
			{
				for (int i = 0; i < components.Count; i++)
				{
					if (components[i] is ILayoutSelfController)
					{
						action(components[i]);
					}
				}
				for (int j = 0; j < components.Count; j++)
				{
					if (!(components[j] is ILayoutSelfController))
					{
						Component scrollRect = components[j];
						if (scrollRect && scrollRect is ScrollRect)
						{
							if (((ScrollRect)scrollRect).content != rect)
							{
								action(components[j]);
							}
						}
						else
						{
							action(components[j]);
						}
					}
				}
				for (int k = 0; k < rect.childCount; k++)
				{
					this.PerformLayoutControl(rect.GetChild(k) as RectTransform, action);
				}
			}
			CollectionPool<List<Component>, Component>.Release(components);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000F4CC File Offset: 0x0000D6CC
		private void PerformLayoutCalculation(RectTransform rect, UnityAction<Component> action)
		{
			if (rect == null)
			{
				return;
			}
			List<Component> components = CollectionPool<List<Component>, Component>.Get();
			rect.GetComponents(typeof(ILayoutElement), components);
			LayoutRebuilder.StripDisabledBehavioursFromList(components);
			Component component;
			if (components.Count > 0 || rect.TryGetComponent(typeof(ILayoutGroup), out component))
			{
				for (int i = 0; i < rect.childCount; i++)
				{
					this.PerformLayoutCalculation(rect.GetChild(i) as RectTransform, action);
				}
				for (int j = 0; j < components.Count; j++)
				{
					action(components[j]);
				}
			}
			CollectionPool<List<Component>, Component>.Release(components);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000F568 File Offset: 0x0000D768
		public static void MarkLayoutForRebuild(RectTransform rect)
		{
			if (rect == null || rect.gameObject == null)
			{
				return;
			}
			List<Component> comps = CollectionPool<List<Component>, Component>.Get();
			bool validLayoutGroup = true;
			RectTransform layoutRoot = rect;
			RectTransform parent = layoutRoot.parent as RectTransform;
			while (validLayoutGroup && !(parent == null) && !(parent.gameObject == null))
			{
				validLayoutGroup = false;
				parent.GetComponents(typeof(ILayoutGroup), comps);
				for (int i = 0; i < comps.Count; i++)
				{
					Component cur = comps[i];
					if (cur != null && cur is Behaviour && ((Behaviour)cur).isActiveAndEnabled)
					{
						validLayoutGroup = true;
						layoutRoot = parent;
						break;
					}
				}
				parent = parent.parent as RectTransform;
			}
			if (layoutRoot == rect && !LayoutRebuilder.ValidController(layoutRoot, comps))
			{
				CollectionPool<List<Component>, Component>.Release(comps);
				return;
			}
			LayoutRebuilder.MarkLayoutRootForRebuild(layoutRoot);
			CollectionPool<List<Component>, Component>.Release(comps);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000F64C File Offset: 0x0000D84C
		private static bool ValidController(RectTransform layoutRoot, List<Component> comps)
		{
			if (layoutRoot == null || layoutRoot.gameObject == null)
			{
				return false;
			}
			layoutRoot.GetComponents(typeof(ILayoutController), comps);
			for (int i = 0; i < comps.Count; i++)
			{
				Component cur = comps[i];
				if (cur != null && cur is Behaviour && ((Behaviour)cur).isActiveAndEnabled)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000F6C0 File Offset: 0x0000D8C0
		private static void MarkLayoutRootForRebuild(RectTransform controller)
		{
			if (controller == null)
			{
				return;
			}
			LayoutRebuilder rebuilder = LayoutRebuilder.s_Rebuilders.Get();
			rebuilder.Initialize(controller);
			if (!CanvasUpdateRegistry.TryRegisterCanvasElementForLayoutRebuild(rebuilder))
			{
				LayoutRebuilder.s_Rebuilders.Release(rebuilder);
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000F6FC File Offset: 0x0000D8FC
		public void LayoutComplete()
		{
			LayoutRebuilder.s_Rebuilders.Release(this);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00002209 File Offset: 0x00000409
		public void GraphicUpdateComplete()
		{
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000F709 File Offset: 0x0000D909
		public override int GetHashCode()
		{
			return this.m_CachedHashFromTransform;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000F711 File Offset: 0x0000D911
		public override bool Equals(object obj)
		{
			return obj.GetHashCode() == this.GetHashCode();
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000F721 File Offset: 0x0000D921
		public override string ToString()
		{
			string text = "(Layout Rebuilder for) ";
			RectTransform toRebuild = this.m_ToRebuild;
			return text + ((toRebuild != null) ? toRebuild.ToString() : null);
		}

		// Token: 0x04000184 RID: 388
		private RectTransform m_ToRebuild;

		// Token: 0x04000185 RID: 389
		private int m_CachedHashFromTransform;

		// Token: 0x04000186 RID: 390
		private static ObjectPool<LayoutRebuilder> s_Rebuilders = new ObjectPool<LayoutRebuilder>(() => new LayoutRebuilder(), null, delegate(LayoutRebuilder x)
		{
			x.Clear();
		}, null, true, 10, 10000);
	}
}
