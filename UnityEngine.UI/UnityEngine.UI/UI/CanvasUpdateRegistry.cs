using System;
using UnityEngine.UI.Collections;

namespace UnityEngine.UI
{
	// Token: 0x0200000A RID: 10
	public class CanvasUpdateRegistry
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000022AC File Offset: 0x000004AC
		protected CanvasUpdateRegistry()
		{
			Canvas.willRenderCanvases += this.PerformUpdate;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000231A File Offset: 0x0000051A
		public static CanvasUpdateRegistry instance
		{
			get
			{
				if (CanvasUpdateRegistry.s_Instance == null)
				{
					CanvasUpdateRegistry.s_Instance = new CanvasUpdateRegistry();
				}
				return CanvasUpdateRegistry.s_Instance;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002334 File Offset: 0x00000534
		private bool ObjectValidForUpdate(ICanvasElement element)
		{
			bool valid = element != null;
			if (element is Object)
			{
				valid = element as Object != null;
			}
			return valid;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002360 File Offset: 0x00000560
		private void CleanInvalidItems()
		{
			for (int i = this.m_LayoutRebuildQueue.Count - 1; i >= 0; i--)
			{
				ICanvasElement item = this.m_LayoutRebuildQueue[i];
				if (item == null)
				{
					this.m_LayoutRebuildQueue.RemoveAt(i);
				}
				else if (item.IsDestroyed())
				{
					this.m_LayoutRebuildQueue.RemoveAt(i);
					item.LayoutComplete();
				}
			}
			for (int j = this.m_GraphicRebuildQueue.Count - 1; j >= 0; j--)
			{
				ICanvasElement item2 = this.m_GraphicRebuildQueue[j];
				if (item2 == null)
				{
					this.m_GraphicRebuildQueue.RemoveAt(j);
				}
				else if (item2.IsDestroyed())
				{
					this.m_GraphicRebuildQueue.RemoveAt(j);
					item2.GraphicUpdateComplete();
				}
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002410 File Offset: 0x00000610
		private void PerformUpdate()
		{
			UISystemProfilerApi.BeginSample(UISystemProfilerApi.SampleType.Layout);
			this.CleanInvalidItems();
			this.m_PerformingLayoutUpdate = true;
			this.m_LayoutRebuildQueue.Sort(CanvasUpdateRegistry.s_SortLayoutFunction);
			for (int i = 0; i <= 2; i++)
			{
				for (int j = 0; j < this.m_LayoutRebuildQueue.Count; j++)
				{
					ICanvasElement rebuild = this.m_LayoutRebuildQueue[j];
					try
					{
						if (this.ObjectValidForUpdate(rebuild))
						{
							rebuild.Rebuild((CanvasUpdate)i);
						}
					}
					catch (Exception ex)
					{
						Debug.LogException(ex, rebuild.transform);
					}
				}
			}
			for (int k = 0; k < this.m_LayoutRebuildQueue.Count; k++)
			{
				this.m_LayoutRebuildQueue[k].LayoutComplete();
			}
			this.m_LayoutRebuildQueue.Clear();
			this.m_PerformingLayoutUpdate = false;
			UISystemProfilerApi.EndSample(UISystemProfilerApi.SampleType.Layout);
			UISystemProfilerApi.BeginSample(UISystemProfilerApi.SampleType.Render);
			ClipperRegistry.instance.Cull();
			this.m_PerformingGraphicUpdate = true;
			for (int l = 3; l < 5; l++)
			{
				for (int m = 0; m < this.m_GraphicRebuildQueue.Count; m++)
				{
					try
					{
						ICanvasElement element = this.m_GraphicRebuildQueue[m];
						if (this.ObjectValidForUpdate(element))
						{
							element.Rebuild((CanvasUpdate)l);
						}
					}
					catch (Exception ex2)
					{
						Debug.LogException(ex2, this.m_GraphicRebuildQueue[m].transform);
					}
				}
			}
			for (int n = 0; n < this.m_GraphicRebuildQueue.Count; n++)
			{
				this.m_GraphicRebuildQueue[n].GraphicUpdateComplete();
			}
			this.m_GraphicRebuildQueue.Clear();
			this.m_PerformingGraphicUpdate = false;
			UISystemProfilerApi.EndSample(UISystemProfilerApi.SampleType.Render);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025AC File Offset: 0x000007AC
		private static int ParentCount(Transform child)
		{
			if (child == null)
			{
				return 0;
			}
			Transform parent = child.parent;
			int count = 0;
			while (parent != null)
			{
				count++;
				parent = parent.parent;
			}
			return count;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025E4 File Offset: 0x000007E4
		private static int SortLayoutList(ICanvasElement x, ICanvasElement y)
		{
			Transform transform = x.transform;
			Transform t2 = y.transform;
			return CanvasUpdateRegistry.ParentCount(transform) - CanvasUpdateRegistry.ParentCount(t2);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000260A File Offset: 0x0000080A
		public static void RegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			CanvasUpdateRegistry.instance.InternalRegisterCanvasElementForLayoutRebuild(element);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002618 File Offset: 0x00000818
		public static bool TryRegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			return CanvasUpdateRegistry.instance.InternalRegisterCanvasElementForLayoutRebuild(element);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002625 File Offset: 0x00000825
		private bool InternalRegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			return !this.m_LayoutRebuildQueue.Contains(element) && this.m_LayoutRebuildQueue.AddUnique(element, true);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002644 File Offset: 0x00000844
		public static void RegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			CanvasUpdateRegistry.instance.InternalRegisterCanvasElementForGraphicRebuild(element);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002652 File Offset: 0x00000852
		public static bool TryRegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			return CanvasUpdateRegistry.instance.InternalRegisterCanvasElementForGraphicRebuild(element);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000265F File Offset: 0x0000085F
		private bool InternalRegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			if (this.m_PerformingGraphicUpdate)
			{
				Debug.LogError(string.Format("Trying to add {0} for graphic rebuild while we are already inside a graphic rebuild loop. This is not supported.", element));
				return false;
			}
			return this.m_GraphicRebuildQueue.AddUnique(element, true);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002688 File Offset: 0x00000888
		public static void UnRegisterCanvasElementForRebuild(ICanvasElement element)
		{
			CanvasUpdateRegistry.instance.InternalUnRegisterCanvasElementForLayoutRebuild(element);
			CanvasUpdateRegistry.instance.InternalUnRegisterCanvasElementForGraphicRebuild(element);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026A0 File Offset: 0x000008A0
		public static void DisableCanvasElementForRebuild(ICanvasElement element)
		{
			CanvasUpdateRegistry.instance.InternalDisableCanvasElementForLayoutRebuild(element);
			CanvasUpdateRegistry.instance.InternalDisableCanvasElementForGraphicRebuild(element);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026B8 File Offset: 0x000008B8
		private void InternalUnRegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			if (this.m_PerformingLayoutUpdate)
			{
				Debug.LogError(string.Format("Trying to remove {0} from rebuild list while we are already inside a rebuild loop. This is not supported.", element));
				return;
			}
			element.LayoutComplete();
			CanvasUpdateRegistry.instance.m_LayoutRebuildQueue.Remove(element);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000026EA File Offset: 0x000008EA
		private void InternalUnRegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			if (this.m_PerformingGraphicUpdate)
			{
				Debug.LogError(string.Format("Trying to remove {0} from rebuild list while we are already inside a rebuild loop. This is not supported.", element));
				return;
			}
			element.GraphicUpdateComplete();
			CanvasUpdateRegistry.instance.m_GraphicRebuildQueue.Remove(element);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000271C File Offset: 0x0000091C
		private void InternalDisableCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			if (this.m_PerformingLayoutUpdate)
			{
				Debug.LogError(string.Format("Trying to remove {0} from rebuild list while we are already inside a rebuild loop. This is not supported.", element));
				return;
			}
			element.LayoutComplete();
			CanvasUpdateRegistry.instance.m_LayoutRebuildQueue.DisableItem(element);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000274E File Offset: 0x0000094E
		private void InternalDisableCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			if (this.m_PerformingGraphicUpdate)
			{
				Debug.LogError(string.Format("Trying to remove {0} from rebuild list while we are already inside a rebuild loop. This is not supported.", element));
				return;
			}
			element.GraphicUpdateComplete();
			CanvasUpdateRegistry.instance.m_GraphicRebuildQueue.DisableItem(element);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002780 File Offset: 0x00000980
		public static bool IsRebuildingLayout()
		{
			return CanvasUpdateRegistry.instance.m_PerformingLayoutUpdate;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000278C File Offset: 0x0000098C
		public static bool IsRebuildingGraphics()
		{
			return CanvasUpdateRegistry.instance.m_PerformingGraphicUpdate;
		}

		// Token: 0x0400001D RID: 29
		private static CanvasUpdateRegistry s_Instance;

		// Token: 0x0400001E RID: 30
		private bool m_PerformingLayoutUpdate;

		// Token: 0x0400001F RID: 31
		private bool m_PerformingGraphicUpdate;

		// Token: 0x04000020 RID: 32
		private string[] m_CanvasUpdateProfilerStrings = new string[] { "CanvasUpdate.Prelayout", "CanvasUpdate.Layout", "CanvasUpdate.PostLayout", "CanvasUpdate.PreRender", "CanvasUpdate.LatePreRender" };

		// Token: 0x04000021 RID: 33
		private const string m_CullingUpdateProfilerString = "ClipperRegistry.Cull";

		// Token: 0x04000022 RID: 34
		private readonly IndexedSet<ICanvasElement> m_LayoutRebuildQueue = new IndexedSet<ICanvasElement>();

		// Token: 0x04000023 RID: 35
		private readonly IndexedSet<ICanvasElement> m_GraphicRebuildQueue = new IndexedSet<ICanvasElement>();

		// Token: 0x04000024 RID: 36
		private static readonly Comparison<ICanvasElement> s_SortLayoutFunction = new Comparison<ICanvasElement>(CanvasUpdateRegistry.SortLayoutList);
	}
}
