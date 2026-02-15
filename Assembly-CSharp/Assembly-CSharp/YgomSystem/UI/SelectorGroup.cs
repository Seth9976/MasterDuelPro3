using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x020005E7 RID: 1511
	public class SelectorGroup
	{
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x0600303D RID: 12349 RVA: 0x000029CC File Offset: 0x00000BCC
		public int selectorNum
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600303E RID: 12350 RVA: 0x000029CC File Offset: 0x00000BCC
		public int groupPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600303F RID: 12351 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem currentItem
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06003040 RID: 12352 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003041 RID: 12353 RVA: 0x0000216D File Offset: 0x0000036D
		public int priorityInCluster
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x00002739 File Offset: 0x00000939
		public SelectorGroup(string group_label)
		{
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddSelector(Selector selector)
		{
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveSelector(Selector selector)
		{
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x000029CC File Offset: 0x00000BCC
		public SelectionItem.UpdateItemStatus UpdateAllSelectors()
		{
			return SelectionItem.UpdateItemStatus.Unknown;
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x000F21D4 File Offset: 0x000F03D4
		public ValueTuple<SelectionItem, float> GetSelectionItem(Vector2 position, Vector2 normalized_direction, float angle, SelectionItem ignore_item = null)
		{
			return default(ValueTuple<SelectionItem, float>);
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem GetHiestPrioritySelectableItem()
		{
			return null;
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x000F21EC File Offset: 0x000F03EC
		public ValueTuple<SelectionItem, float> GetSelectionItem(Vector2 view_position)
		{
			return default(ValueTuple<SelectionItem, float>);
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDefaultItem(SelectionItem item)
		{
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem GetDefaultItem()
		{
			return null;
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SelectItem(SelectionItem item, bool initializeSelection = false, bool force = false)
		{
			return false;
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetGroupPriority(int group_priority, bool refresh_active_group)
		{
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPriorityInCluster(int priority_in_cluster)
		{
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateSelectorDepth()
		{
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x0000216D File Offset: 0x0000036D
		public void InvokeClusterActivateCallback()
		{
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x0000216D File Offset: 0x0000036D
		public void InvokeClusterDeactivateCallback()
		{
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x0000216A File Offset: 0x0000036A
		public List<Selector> GetSelectors()
		{
			return null;
		}

		// Token: 0x04002CD0 RID: 11472
		public string groupLabel;

		// Token: 0x04002CD1 RID: 11473
		public List<Selector> selectors;

		// Token: 0x04002CD2 RID: 11474
		private bool isDirty;

		// Token: 0x020005E8 RID: 1512
		public enum Direction
		{
			// Token: 0x04002CD4 RID: 11476
			None,
			// Token: 0x04002CD5 RID: 11477
			Up,
			// Token: 0x04002CD6 RID: 11478
			Right,
			// Token: 0x04002CD7 RID: 11479
			Down,
			// Token: 0x04002CD8 RID: 11480
			Left
		}
	}
}
