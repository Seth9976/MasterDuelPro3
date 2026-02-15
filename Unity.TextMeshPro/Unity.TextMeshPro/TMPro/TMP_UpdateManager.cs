using System;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x020000A3 RID: 163
	public class TMP_UpdateManager
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x0002DD3C File Offset: 0x0002BF3C
		private static TMP_UpdateManager instance
		{
			get
			{
				if (TMP_UpdateManager.s_Instance == null)
				{
					TMP_UpdateManager.s_Instance = new TMP_UpdateManager();
				}
				return TMP_UpdateManager.s_Instance;
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0002DD54 File Offset: 0x0002BF54
		private TMP_UpdateManager()
		{
			Canvas.willRenderCanvases += this.DoRebuilds;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0002DDD0 File Offset: 0x0002BFD0
		internal static void RegisterTextObjectForUpdate(TMP_Text textObject)
		{
			TMP_UpdateManager.instance.InternalRegisterTextObjectForUpdate(textObject);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0002DDE0 File Offset: 0x0002BFE0
		private void InternalRegisterTextObjectForUpdate(TMP_Text textObject)
		{
			int id = textObject.GetInstanceID();
			if (this.m_InternalUpdateLookup.Contains(id))
			{
				return;
			}
			this.m_InternalUpdateLookup.Add(id);
			this.m_InternalUpdateQueue.Add(textObject);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0002DE1C File Offset: 0x0002C01C
		public static void RegisterTextElementForLayoutRebuild(TMP_Text element)
		{
			TMP_UpdateManager.instance.InternalRegisterTextElementForLayoutRebuild(element);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0002DE2C File Offset: 0x0002C02C
		private void InternalRegisterTextElementForLayoutRebuild(TMP_Text element)
		{
			int id = element.GetInstanceID();
			if (this.m_LayoutQueueLookup.Contains(id))
			{
				return;
			}
			this.m_LayoutQueueLookup.Add(id);
			this.m_LayoutRebuildQueue.Add(element);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0002DE68 File Offset: 0x0002C068
		public static void RegisterTextElementForGraphicRebuild(TMP_Text element)
		{
			TMP_UpdateManager.instance.InternalRegisterTextElementForGraphicRebuild(element);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0002DE78 File Offset: 0x0002C078
		private void InternalRegisterTextElementForGraphicRebuild(TMP_Text element)
		{
			int id = element.GetInstanceID();
			if (this.m_GraphicQueueLookup.Contains(id))
			{
				return;
			}
			this.m_GraphicQueueLookup.Add(id);
			this.m_GraphicRebuildQueue.Add(element);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0002DEB4 File Offset: 0x0002C0B4
		public static void RegisterTextElementForCullingUpdate(TMP_Text element)
		{
			TMP_UpdateManager.instance.InternalRegisterTextElementForCullingUpdate(element);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0002DEC4 File Offset: 0x0002C0C4
		private void InternalRegisterTextElementForCullingUpdate(TMP_Text element)
		{
			int id = element.GetInstanceID();
			if (this.m_CullingUpdateLookup.Contains(id))
			{
				return;
			}
			this.m_CullingUpdateLookup.Add(id);
			this.m_CullingUpdateQueue.Add(element);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0002DF00 File Offset: 0x0002C100
		private void OnCameraPreCull()
		{
			this.DoRebuilds();
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0002DF08 File Offset: 0x0002C108
		private void DoRebuilds()
		{
			for (int i = 0; i < this.m_InternalUpdateQueue.Count; i++)
			{
				this.m_InternalUpdateQueue[i].InternalUpdate();
			}
			for (int j = 0; j < this.m_LayoutRebuildQueue.Count; j++)
			{
				this.m_LayoutRebuildQueue[j].Rebuild(CanvasUpdate.Prelayout);
			}
			if (this.m_LayoutRebuildQueue.Count > 0)
			{
				this.m_LayoutRebuildQueue.Clear();
				this.m_LayoutQueueLookup.Clear();
			}
			for (int k = 0; k < this.m_GraphicRebuildQueue.Count; k++)
			{
				this.m_GraphicRebuildQueue[k].Rebuild(CanvasUpdate.PreRender);
			}
			if (this.m_GraphicRebuildQueue.Count > 0)
			{
				this.m_GraphicRebuildQueue.Clear();
				this.m_GraphicQueueLookup.Clear();
			}
			for (int l = 0; l < this.m_CullingUpdateQueue.Count; l++)
			{
				this.m_CullingUpdateQueue[l].UpdateCulling();
			}
			if (this.m_CullingUpdateQueue.Count > 0)
			{
				this.m_CullingUpdateQueue.Clear();
				this.m_CullingUpdateLookup.Clear();
			}
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0002E01F File Offset: 0x0002C21F
		internal static void UnRegisterTextObjectForUpdate(TMP_Text textObject)
		{
			TMP_UpdateManager.instance.InternalUnRegisterTextObjectForUpdate(textObject);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0002E02C File Offset: 0x0002C22C
		public static void UnRegisterTextElementForRebuild(TMP_Text element)
		{
			TMP_UpdateManager.instance.InternalUnRegisterTextElementForGraphicRebuild(element);
			TMP_UpdateManager.instance.InternalUnRegisterTextElementForLayoutRebuild(element);
			TMP_UpdateManager.instance.InternalUnRegisterTextObjectForUpdate(element);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0002E050 File Offset: 0x0002C250
		private void InternalUnRegisterTextElementForGraphicRebuild(TMP_Text element)
		{
			int id = element.GetInstanceID();
			this.m_GraphicRebuildQueue.Remove(element);
			this.m_GraphicQueueLookup.Remove(id);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0002E080 File Offset: 0x0002C280
		private void InternalUnRegisterTextElementForLayoutRebuild(TMP_Text element)
		{
			int id = element.GetInstanceID();
			this.m_LayoutRebuildQueue.Remove(element);
			this.m_LayoutQueueLookup.Remove(id);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0002E0B0 File Offset: 0x0002C2B0
		private void InternalUnRegisterTextObjectForUpdate(TMP_Text textObject)
		{
			int id = textObject.GetInstanceID();
			this.m_InternalUpdateQueue.Remove(textObject);
			this.m_InternalUpdateLookup.Remove(id);
		}

		// Token: 0x04000589 RID: 1417
		private static TMP_UpdateManager s_Instance;

		// Token: 0x0400058A RID: 1418
		private readonly HashSet<int> m_LayoutQueueLookup = new HashSet<int>();

		// Token: 0x0400058B RID: 1419
		private readonly List<TMP_Text> m_LayoutRebuildQueue = new List<TMP_Text>();

		// Token: 0x0400058C RID: 1420
		private readonly HashSet<int> m_GraphicQueueLookup = new HashSet<int>();

		// Token: 0x0400058D RID: 1421
		private readonly List<TMP_Text> m_GraphicRebuildQueue = new List<TMP_Text>();

		// Token: 0x0400058E RID: 1422
		private readonly HashSet<int> m_InternalUpdateLookup = new HashSet<int>();

		// Token: 0x0400058F RID: 1423
		private readonly List<TMP_Text> m_InternalUpdateQueue = new List<TMP_Text>();

		// Token: 0x04000590 RID: 1424
		private readonly HashSet<int> m_CullingUpdateLookup = new HashSet<int>();

		// Token: 0x04000591 RID: 1425
		private readonly List<TMP_Text> m_CullingUpdateQueue = new List<TMP_Text>();

		// Token: 0x04000592 RID: 1426
		private static ProfilerMarker k_RegisterTextObjectForUpdateMarker = new ProfilerMarker("TMP.RegisterTextObjectForUpdate");

		// Token: 0x04000593 RID: 1427
		private static ProfilerMarker k_RegisterTextElementForGraphicRebuildMarker = new ProfilerMarker("TMP.RegisterTextElementForGraphicRebuild");

		// Token: 0x04000594 RID: 1428
		private static ProfilerMarker k_RegisterTextElementForCullingUpdateMarker = new ProfilerMarker("TMP.RegisterTextElementForCullingUpdate");

		// Token: 0x04000595 RID: 1429
		private static ProfilerMarker k_UnregisterTextObjectForUpdateMarker = new ProfilerMarker("TMP.UnregisterTextObjectForUpdate");

		// Token: 0x04000596 RID: 1430
		private static ProfilerMarker k_UnregisterTextElementForGraphicRebuildMarker = new ProfilerMarker("TMP.UnregisterTextElementForGraphicRebuild");
	}
}
