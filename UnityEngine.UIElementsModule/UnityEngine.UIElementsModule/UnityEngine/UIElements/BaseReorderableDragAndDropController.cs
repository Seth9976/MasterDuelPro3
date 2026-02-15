using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000189 RID: 393
	internal abstract class BaseReorderableDragAndDropController : ICollectionDragAndDropController, IDragAndDropController<IListDragAndDropArgs>, IReorderable
	{
		// Token: 0x06000B9F RID: 2975 RVA: 0x0003810F File Offset: 0x0003630F
		public IEnumerable<int> GetSortedSelectedIds()
		{
			return this.m_SortedSelectedIds;
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00038117 File Offset: 0x00036317
		protected BaseReorderableDragAndDropController(BaseVerticalCollectionView view)
		{
			this.m_View = view;
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0003813A File Offset: 0x0003633A
		// (set) Token: 0x06000BA2 RID: 2978 RVA: 0x00038142 File Offset: 0x00036342
		public virtual bool enableReordering { get; set; } = true;

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0003814C File Offset: 0x0003634C
		public virtual bool CanStartDrag(IEnumerable<int> itemIds)
		{
			return true;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00038160 File Offset: 0x00036360
		public virtual StartDragArgs SetupDragAndDrop(IEnumerable<int> itemIds, bool skipText = false)
		{
			this.m_SortedSelectedIds.Clear();
			string title = string.Empty;
			bool flag = itemIds != null;
			if (flag)
			{
				foreach (int id in itemIds)
				{
					this.m_SortedSelectedIds.Add(id);
					bool flag2 = skipText;
					if (!flag2)
					{
						bool flag3 = string.IsNullOrEmpty(title);
						if (flag3)
						{
							ReusableCollectionItem recycledItemFromId = this.m_View.GetRecycledItemFromId(id);
							Label label = ((recycledItemFromId != null) ? recycledItemFromId.rootElement.Q(null, null) : null);
							title = ((label != null) ? label.text : string.Format("Item {0}", id));
						}
						else
						{
							title = "<Multiple>";
							skipText = true;
						}
					}
				}
			}
			this.m_SortedSelectedIds.Sort(new Comparison<int>(this.CompareId));
			return new StartDragArgs(title, DragVisualMode.Move);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00038260 File Offset: 0x00036460
		protected virtual int CompareId(int id1, int id2)
		{
			return id1.CompareTo(id2);
		}

		// Token: 0x06000BA6 RID: 2982
		public abstract DragVisualMode HandleDragAndDrop(IListDragAndDropArgs args);

		// Token: 0x06000BA7 RID: 2983
		public abstract void OnDrop(IListDragAndDropArgs args);

		// Token: 0x06000BA8 RID: 2984 RVA: 0x000020EA File Offset: 0x000002EA
		public virtual void DragCleanup()
		{
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x000020EA File Offset: 0x000002EA
		public virtual void HandleAutoExpand(ReusableCollectionItem item, Vector2 pointerPosition)
		{
		}

		// Token: 0x04000768 RID: 1896
		protected readonly BaseVerticalCollectionView m_View;

		// Token: 0x04000769 RID: 1897
		protected List<int> m_SortedSelectedIds = new List<int>();
	}
}
