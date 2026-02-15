using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x0200007E RID: 126
	public abstract class BaseTreeView : BaseVerticalCollectionView
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00017AED File Offset: 0x00015CED
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x00017B01 File Offset: 0x00015D01
		[CreateProperty(ReadOnly = true)]
		public new IList itemsSource
		{
			get
			{
				BaseTreeViewController viewController = this.viewController;
				return (viewController != null) ? viewController.itemsSource : null;
			}
			internal set
			{
				base.GetOrCreateViewController().itemsSource = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00017B10 File Offset: 0x00015D10
		public new BaseTreeViewController viewController
		{
			get
			{
				return base.viewController as BaseTreeViewController;
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00017B1D File Offset: 0x00015D1D
		private protected override void CreateVirtualizationController()
		{
			base.CreateVirtualizationController<ReusableTreeViewItem>();
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00017B28 File Offset: 0x00015D28
		public override void SetViewController(CollectionViewController controller)
		{
			bool flag = this.viewController != null;
			if (flag)
			{
				this.viewController.itemIndexChanged -= this.OnItemIndexChanged;
				this.viewController.itemExpandedChanged -= this.OnItemExpandedChanged;
			}
			base.SetViewController(controller);
			bool flag2 = this.viewController != null;
			if (flag2)
			{
				this.viewController.itemIndexChanged += this.OnItemIndexChanged;
				this.viewController.itemExpandedChanged += this.OnItemExpandedChanged;
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00017BBC File Offset: 0x00015DBC
		private void OnItemIndexChanged(int srcIndex, int dstIndex)
		{
			base.RefreshItems();
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00017BC6 File Offset: 0x00015DC6
		private void OnItemExpandedChanged(TreeViewExpansionChangedArgs arg)
		{
			Action<TreeViewExpansionChangedArgs> action = this.itemExpandedChanged;
			if (action != null)
			{
				action(arg);
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00017BDC File Offset: 0x00015DDC
		internal override ICollectionDragAndDropController CreateDragAndDropController()
		{
			return new TreeViewReorderableDragAndDropController(this);
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00017BE4 File Offset: 0x00015DE4
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x00017BEC File Offset: 0x00015DEC
		[CreateProperty]
		public bool autoExpand
		{
			get
			{
				return this.m_AutoExpand;
			}
			set
			{
				bool flag = this.m_AutoExpand == value;
				if (!flag)
				{
					this.m_AutoExpand = value;
					base.RefreshItems();
					base.NotifyPropertyChanged(in BaseTreeView.autoExpandProperty);
				}
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00017C23 File Offset: 0x00015E23
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x00017C2B File Offset: 0x00015E2B
		internal List<int> expandedItemIds
		{
			get
			{
				return this.m_ExpandedItemIds;
			}
			set
			{
				this.m_ExpandedItemIds = value;
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00017C34 File Offset: 0x00015E34
		public BaseTreeView()
			: this(-1)
		{
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00017C3F File Offset: 0x00015E3F
		public BaseTreeView(int itemHeight)
			: base(null, (float)itemHeight)
		{
			this.m_ExpandedItemIds = new List<int>();
			base.AddToClassList(BaseTreeView.ussClassName);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00017C64 File Offset: 0x00015E64
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			bool flag = this.viewController != null;
			if (flag)
			{
				this.viewController.OnViewDataReadyUpdateNodes();
				base.RefreshItems();
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00017C9C File Offset: 0x00015E9C
		private protected override bool HandleItemNavigation(bool moveIn, bool altPressed)
		{
			int selectionIncrement = 1;
			bool hasChanges = false;
			foreach (int selectedId in base.selectedIds)
			{
				int id = this.viewController.GetIndexForId(selectedId);
				bool flag = !this.viewController.HasChildrenByIndex(id);
				if (flag)
				{
					break;
				}
				bool flag2 = moveIn && !this.IsExpandedByIndex(id);
				if (flag2)
				{
					this.ExpandItemByIndex(id, altPressed);
					hasChanges = true;
				}
				else
				{
					bool flag3 = !moveIn && this.IsExpandedByIndex(id);
					if (flag3)
					{
						this.CollapseItemByIndex(id, altPressed);
						hasChanges = true;
					}
				}
			}
			bool flag4 = hasChanges;
			bool flag5;
			if (flag4)
			{
				flag5 = true;
			}
			else
			{
				bool flag6 = !moveIn;
				if (flag6)
				{
					int id2 = this.viewController.GetIdForIndex(base.selectedIndex);
					int ancestorId = this.viewController.GetParentId(id2);
					bool flag7 = ancestorId != -1;
					if (flag7)
					{
						this.SetSelectionById(ancestorId);
						base.ScrollToItemById(ancestorId);
						return true;
					}
					selectionIncrement = -1;
				}
				int selectionIndex = base.selectedIndex;
				bool hasChildren;
				do
				{
					selectionIndex += selectionIncrement;
					hasChildren = this.viewController.HasChildrenByIndex(selectionIndex);
				}
				while (!hasChildren && selectionIndex >= 0 && selectionIndex < this.itemsSource.Count);
				bool flag8 = hasChildren;
				if (flag8)
				{
					base.SetSelection(selectionIndex);
					base.ScrollToItem(selectionIndex);
					flag5 = true;
				}
				else
				{
					flag5 = false;
				}
			}
			return flag5;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00017E28 File Offset: 0x00016028
		public void SetSelectionById(int id)
		{
			this.SetSelectionById(new int[] { id });
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00017E3C File Offset: 0x0001603C
		public void SetSelectionById(IEnumerable<int> ids)
		{
			this.SetSelectionInternalById(ids, true);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00017E48 File Offset: 0x00016048
		internal void SetSelectionInternalById(IEnumerable<int> ids, bool sendNotification)
		{
			bool flag = ids == null;
			if (!flag)
			{
				List<int> selectedIndexes = ids.Select((int id) => this.GetItemIndex(id, true)).ToList<int>();
				base.SetSelectionInternal(selectedIndexes, sendNotification);
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00017E84 File Offset: 0x00016084
		private int GetItemIndex(int id, bool expand = false)
		{
			if (expand)
			{
				int parentId = this.viewController.GetParentId(id);
				List<int> list = CollectionPool<List<int>, int>.Get();
				this.viewController.GetExpandedItemIds(list);
				while (parentId != -1)
				{
					bool flag = !list.Contains(parentId);
					if (flag)
					{
						this.viewController.ExpandItem(parentId, false, true);
					}
					parentId = this.viewController.GetParentId(parentId);
				}
				CollectionPool<List<int>, int>.Release(list);
			}
			return this.viewController.GetIndexForId(id);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00017F10 File Offset: 0x00016110
		public bool IsExpanded(int id)
		{
			return this.viewController.IsExpanded(id);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00017F2E File Offset: 0x0001612E
		public void CollapseItem(int id, bool collapseAllChildren = false, bool refresh = true)
		{
			this.viewController.CollapseItem(id, collapseAllChildren, refresh);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00017F40 File Offset: 0x00016140
		public void ExpandItem(int id, bool expandAllChildren = false, bool refresh = true)
		{
			this.viewController.ExpandItem(id, expandAllChildren, refresh);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00017F54 File Offset: 0x00016154
		private bool IsExpandedByIndex(int index)
		{
			return this.viewController.IsExpandedByIndex(index);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00017F74 File Offset: 0x00016174
		private void CollapseItemByIndex(int index, bool collapseAll)
		{
			bool flag = !this.viewController.HasChildrenByIndex(index);
			if (!flag)
			{
				this.viewController.CollapseItemByIndex(index, collapseAll, true);
				base.RefreshItems();
				base.SaveViewData();
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00017FB4 File Offset: 0x000161B4
		private void ExpandItemByIndex(int index, bool expandAll)
		{
			bool flag = !this.viewController.HasChildrenByIndex(index);
			if (!flag)
			{
				this.viewController.ExpandItemByIndex(index, expandAll, true);
				base.RefreshItems();
				base.SaveViewData();
			}
		}

		// Token: 0x040002B8 RID: 696
		internal static readonly BindingId autoExpandProperty = "autoExpand";

		// Token: 0x040002B9 RID: 697
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal static readonly int invalidId = -1;

		// Token: 0x040002BA RID: 698
		public new static readonly string ussClassName = "unity-tree-view";

		// Token: 0x040002BB RID: 699
		public new static readonly string itemUssClassName = BaseTreeView.ussClassName + "__item";

		// Token: 0x040002BC RID: 700
		public static readonly string itemToggleUssClassName = BaseTreeView.ussClassName + "__item-toggle";

		// Token: 0x040002BD RID: 701
		[Obsolete("Individual item indents are no longer used, see itemIndentUssClassName instead", false)]
		public static readonly string itemIndentsContainerUssClassName = BaseTreeView.ussClassName + "__item-indents";

		// Token: 0x040002BE RID: 702
		public static readonly string itemIndentUssClassName = BaseTreeView.ussClassName + "__item-indent";

		// Token: 0x040002BF RID: 703
		public static readonly string itemContentContainerUssClassName = BaseTreeView.ussClassName + "__item-content";

		// Token: 0x040002C0 RID: 704
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<TreeViewExpansionChangedArgs> itemExpandedChanged;

		// Token: 0x040002C1 RID: 705
		private bool m_AutoExpand;

		// Token: 0x040002C2 RID: 706
		[SerializeField]
		[DontCreateProperty]
		private List<int> m_ExpandedItemIds;

		// Token: 0x0200007F RID: 127
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseVerticalCollectionView.UxmlTraits
		{
			// Token: 0x060004D4 RID: 1236 RVA: 0x00018090 File Offset: 0x00016290
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				BaseTreeView treeView = (BaseTreeView)ve;
				treeView.autoExpand = this.m_AutoExpand.GetValueFromBag(bag, cc);
			}

			// Token: 0x040002C3 RID: 707
			private readonly UxmlBoolAttributeDescription m_AutoExpand = new UxmlBoolAttributeDescription
			{
				name = "auto-expand",
				defaultValue = false
			};
		}
	}
}
