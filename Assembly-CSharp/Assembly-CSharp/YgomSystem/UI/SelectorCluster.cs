using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x020005E6 RID: 1510
	public class SelectorCluster
	{
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600301F RID: 12319 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool goThrough
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06003020 RID: 12320 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003021 RID: 12321 RVA: 0x0000216D File Offset: 0x0000036D
		public int goThroughCounter
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

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06003022 RID: 12322 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003023 RID: 12323 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectionItem currentItem
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06003024 RID: 12324 RVA: 0x0000216A File Offset: 0x0000036A
		public Selector currentSelector
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06003025 RID: 12325 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003026 RID: 12326 RVA: 0x0000216D File Offset: 0x0000036D
		public bool activated
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06003027 RID: 12327 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003028 RID: 12328 RVA: 0x0000216D File Offset: 0x0000036D
		public bool protectActivation
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x00002739 File Offset: 0x00000939
		public SelectorCluster(int priority)
		{
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddGroup(SelectorGroup group)
		{
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveGroup(SelectorGroup group)
		{
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x000029CC File Offset: 0x00000BCC
		public SelectionItem.UpdateItemStatus UpdateAllGroups()
		{
			return SelectionItem.UpdateItemStatus.Unknown;
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectorGroup GetGroup(string label)
		{
			return null;
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SelectItem(SelectionItem item, bool initializeSelection = false, bool force = false)
		{
			return false;
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x0000216D File Offset: 0x0000036D
		public void DeselectCurrentItem(bool savePreSelectedItem)
		{
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem GetSelectionItem(Vector2 view_position)
		{
			return null;
		}

		// Token: 0x06003031 RID: 12337 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem GetHighestPrioritySelectableItem()
		{
			return null;
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SelectHighestPriorityItem()
		{
			return false;
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x0000216D File Offset: 0x0000036D
		public void ChangeSelectionItem(Vector2 position, Vector2 normalized_direction, bool check_loop, float angle, bool igonre_current_item)
		{
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x000F21A4 File Offset: 0x000F03A4
		public ValueTuple<SelectionItem, float> GetSelectionItem(Vector2 position, Vector2 normalized_direction, bool check_loop, float angle, SelectionItem ignore_item = null)
		{
			return default(ValueTuple<SelectionItem, float>);
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem GetDefaultItem()
		{
			return null;
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SelectDefaultItem()
		{
			return false;
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem GetPreSelectedItem()
		{
			return null;
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SelectPreSelectedItem()
		{
			return false;
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x000F21BC File Offset: 0x000F03BC
		private Vector2 GetScreenEdge(Vector2 position, Vector2 ray_direction)
		{
			return default(Vector2);
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x0000216D File Offset: 0x0000036D
		public void Activate()
		{
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x0000216D File Offset: 0x0000036D
		public void Deactivate()
		{
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x0000216D File Offset: 0x0000036D
		public void Refresh()
		{
		}

		// Token: 0x04002CC3 RID: 11459
		public int priority;

		// Token: 0x04002CC4 RID: 11460
		public List<SelectorGroup> groups;

		// Token: 0x04002CC5 RID: 11461
		private SelectionItem preSelectedItem;

		// Token: 0x04002CC6 RID: 11462
		protected PadInputDirection currentPadDirection;

		// Token: 0x04002CC7 RID: 11463
		protected float padInputContinueTime;

		// Token: 0x04002CC8 RID: 11464
		protected static Vector2 directionUp;

		// Token: 0x04002CC9 RID: 11465
		protected static Vector2 directionUpRight;

		// Token: 0x04002CCA RID: 11466
		protected static Vector2 directionRight;

		// Token: 0x04002CCB RID: 11467
		protected static Vector2 directionDownRight;

		// Token: 0x04002CCC RID: 11468
		protected static Vector2 directionDown;

		// Token: 0x04002CCD RID: 11469
		protected static Vector2 directionDownLeft;

		// Token: 0x04002CCE RID: 11470
		protected static Vector2 directionLeft;

		// Token: 0x04002CCF RID: 11471
		protected static Vector2 directionUpLeft;
	}
}
