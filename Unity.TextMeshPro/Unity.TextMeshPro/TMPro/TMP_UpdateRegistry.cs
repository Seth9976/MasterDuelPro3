using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x020000A4 RID: 164
	public class TMP_UpdateRegistry
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0002E138 File Offset: 0x0002C338
		public static TMP_UpdateRegistry instance
		{
			get
			{
				if (TMP_UpdateRegistry.s_Instance == null)
				{
					TMP_UpdateRegistry.s_Instance = new TMP_UpdateRegistry();
				}
				return TMP_UpdateRegistry.s_Instance;
			}
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0002E150 File Offset: 0x0002C350
		protected TMP_UpdateRegistry()
		{
			Canvas.willRenderCanvases += this.PerformUpdateForCanvasRendererObjects;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0002E1A0 File Offset: 0x0002C3A0
		public static void RegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			TMP_UpdateRegistry.instance.InternalRegisterCanvasElementForLayoutRebuild(element);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0002E1B0 File Offset: 0x0002C3B0
		private bool InternalRegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			int id = (element as global::UnityEngine.Object).GetInstanceID();
			if (this.m_LayoutQueueLookup.Contains(id))
			{
				return false;
			}
			this.m_LayoutQueueLookup.Add(id);
			this.m_LayoutRebuildQueue.Add(element);
			return true;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0002E1F3 File Offset: 0x0002C3F3
		public static void RegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			TMP_UpdateRegistry.instance.InternalRegisterCanvasElementForGraphicRebuild(element);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0002E204 File Offset: 0x0002C404
		private bool InternalRegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			int id = (element as global::UnityEngine.Object).GetInstanceID();
			if (this.m_GraphicQueueLookup.Contains(id))
			{
				return false;
			}
			this.m_GraphicQueueLookup.Add(id);
			this.m_GraphicRebuildQueue.Add(element);
			return true;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0002E248 File Offset: 0x0002C448
		private void PerformUpdateForCanvasRendererObjects()
		{
			for (int index = 0; index < this.m_LayoutRebuildQueue.Count; index++)
			{
				TMP_UpdateRegistry.instance.m_LayoutRebuildQueue[index].Rebuild(CanvasUpdate.Prelayout);
			}
			if (this.m_LayoutRebuildQueue.Count > 0)
			{
				this.m_LayoutRebuildQueue.Clear();
				this.m_LayoutQueueLookup.Clear();
			}
			for (int index2 = 0; index2 < this.m_GraphicRebuildQueue.Count; index2++)
			{
				TMP_UpdateRegistry.instance.m_GraphicRebuildQueue[index2].Rebuild(CanvasUpdate.PreRender);
			}
			if (this.m_GraphicRebuildQueue.Count > 0)
			{
				this.m_GraphicRebuildQueue.Clear();
				this.m_GraphicQueueLookup.Clear();
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0002E2F5 File Offset: 0x0002C4F5
		private void PerformUpdateForMeshRendererObjects()
		{
			Debug.Log("Perform update of MeshRenderer objects.");
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0002E301 File Offset: 0x0002C501
		public static void UnRegisterCanvasElementForRebuild(ICanvasElement element)
		{
			TMP_UpdateRegistry.instance.InternalUnRegisterCanvasElementForLayoutRebuild(element);
			TMP_UpdateRegistry.instance.InternalUnRegisterCanvasElementForGraphicRebuild(element);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0002E31C File Offset: 0x0002C51C
		private void InternalUnRegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			int id = (element as global::UnityEngine.Object).GetInstanceID();
			TMP_UpdateRegistry.instance.m_LayoutRebuildQueue.Remove(element);
			this.m_GraphicQueueLookup.Remove(id);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0002E354 File Offset: 0x0002C554
		private void InternalUnRegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			int id = (element as global::UnityEngine.Object).GetInstanceID();
			TMP_UpdateRegistry.instance.m_GraphicRebuildQueue.Remove(element);
			this.m_LayoutQueueLookup.Remove(id);
		}

		// Token: 0x04000597 RID: 1431
		private static TMP_UpdateRegistry s_Instance;

		// Token: 0x04000598 RID: 1432
		private readonly List<ICanvasElement> m_LayoutRebuildQueue = new List<ICanvasElement>();

		// Token: 0x04000599 RID: 1433
		private HashSet<int> m_LayoutQueueLookup = new HashSet<int>();

		// Token: 0x0400059A RID: 1434
		private readonly List<ICanvasElement> m_GraphicRebuildQueue = new List<ICanvasElement>();

		// Token: 0x0400059B RID: 1435
		private HashSet<int> m_GraphicQueueLookup = new HashSet<int>();
	}
}
