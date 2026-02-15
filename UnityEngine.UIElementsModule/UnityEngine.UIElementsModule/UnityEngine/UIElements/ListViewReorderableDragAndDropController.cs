using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200019F RID: 415
	internal class ListViewReorderableDragAndDropController : BaseReorderableDragAndDropController
	{
		// Token: 0x06000C24 RID: 3108 RVA: 0x0003AA32 File Offset: 0x00038C32
		public ListViewReorderableDragAndDropController(BaseListView view)
			: base(view)
		{
			this.m_ListView = view;
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0003AA44 File Offset: 0x00038C44
		public override DragVisualMode HandleDragAndDrop(IListDragAndDropArgs args)
		{
			bool flag = args.dragAndDropPosition == DragAndDropPosition.OverItem;
			DragVisualMode dragVisualMode;
			if (flag)
			{
				dragVisualMode = DragVisualMode.Rejected;
			}
			else
			{
				dragVisualMode = ((args.dragAndDropData.source == this.m_ListView) ? DragVisualMode.Move : DragVisualMode.Rejected);
			}
			return dragVisualMode;
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0003AA80 File Offset: 0x00038C80
		public override void OnDrop(IListDragAndDropArgs args)
		{
			int insertIndex = args.insertAtIndex;
			int insertIndexShift = 0;
			int srcIndexShift = 0;
			for (int i = this.m_SortedSelectedIds.Count - 1; i >= 0; i--)
			{
				int id = this.m_SortedSelectedIds[i];
				int index = this.m_View.viewController.GetIndexForId(id);
				bool flag = index < 0;
				if (!flag)
				{
					int newIndex = insertIndex - insertIndexShift;
					bool flag2 = index >= insertIndex;
					if (flag2)
					{
						index += srcIndexShift;
						srcIndexShift++;
					}
					else
					{
						bool flag3 = index < newIndex;
						if (flag3)
						{
							insertIndexShift++;
							newIndex--;
						}
					}
					this.m_ListView.viewController.Move(index, newIndex);
				}
			}
			bool flag4 = this.m_ListView.selectionType > SelectionType.None;
			if (flag4)
			{
				List<int> newSelection = new List<int>();
				for (int j = 0; j < this.m_SortedSelectedIds.Count; j++)
				{
					newSelection.Add(insertIndex - insertIndexShift + j);
				}
				this.m_ListView.SetSelectionWithoutNotify(newSelection);
			}
			else
			{
				this.m_ListView.ClearSelection();
			}
		}

		// Token: 0x040007AC RID: 1964
		protected readonly BaseListView m_ListView;
	}
}
