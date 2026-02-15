using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Hierarchy;
using Unity.Profiling;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000052 RID: 82
	public abstract class BaseTreeViewController : CollectionViewController
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000C5A3 File Offset: 0x0000A7A3
		protected BaseTreeView baseTreeView
		{
			get
			{
				return base.view as BaseTreeView;
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000290 RID: 656 RVA: 0x0000C5B0 File Offset: 0x0000A7B0
		// (remove) Token: 0x06000291 RID: 657 RVA: 0x0000C5E8 File Offset: 0x0000A7E8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<TreeViewExpansionChangedArgs> itemExpandedChanged;

		// Token: 0x06000292 RID: 658 RVA: 0x0000C61D File Offset: 0x0000A81D
		protected BaseTreeViewController()
		{
			this.hierarchy = new Hierarchy();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000C640 File Offset: 0x0000A840
		~BaseTreeViewController()
		{
			this.DisposeHierarchy();
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000C670 File Offset: 0x0000A870
		// (set) Token: 0x06000295 RID: 661 RVA: 0x0000C678 File Offset: 0x0000A878
		private protected Hierarchy hierarchy
		{
			get
			{
				return this.m_Hierarchy;
			}
			set
			{
				bool flag = this.hierarchy == value;
				if (!flag)
				{
					this.DisposeHierarchy();
					bool flag2 = value == null;
					if (!flag2)
					{
						this.m_Hierarchy = value;
						this.m_HierarchyFlattened = new HierarchyFlattened(this.m_Hierarchy);
						this.m_HierarchyViewModel = new HierarchyViewModel(this.m_HierarchyFlattened, HierarchyNodeFlags.None);
						this.m_TreeViewDataProperty = this.m_Hierarchy.GetOrCreatePropertyUnmanaged<int>("TreeViewDataProperty", HierarchyPropertyStorageType.Dense);
					}
				}
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000C6EC File Offset: 0x0000A8EC
		internal void DisposeHierarchy()
		{
			bool flag = this.m_HierarchyViewModel != null;
			if (flag)
			{
				bool isCreated = this.m_HierarchyViewModel.IsCreated;
				if (isCreated)
				{
					this.m_HierarchyViewModel.Dispose();
				}
				this.m_HierarchyViewModel = null;
			}
			bool flag2 = this.m_HierarchyFlattened != null;
			if (flag2)
			{
				bool isCreated2 = this.m_HierarchyFlattened.IsCreated;
				if (isCreated2)
				{
					this.m_HierarchyFlattened.Dispose();
				}
				this.m_HierarchyFlattened = null;
			}
			bool flag3 = this.m_Hierarchy != null;
			if (flag3)
			{
				bool isCreated3 = this.m_Hierarchy.IsCreated;
				if (isCreated3)
				{
					this.m_Hierarchy.Dispose();
				}
				this.m_Hierarchy = null;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000C791 File Offset: 0x0000A991
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000C799 File Offset: 0x0000A999
		public override IList itemsSource
		{
			get
			{
				return base.itemsSource;
			}
			set
			{
				throw new InvalidOperationException("Can't set itemsSource directly. Override this controller to manage tree data.");
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000C7A5 File Offset: 0x0000A9A5
		public virtual IEnumerable<int> GetAllItemIds(IEnumerable<int> rootIds = null)
		{
			bool flag = rootIds == null;
			if (flag)
			{
				foreach (ref HierarchyFlattenedNode ptr in this.m_HierarchyFlattened)
				{
					HierarchyFlattenedNode flattenedNode = ptr;
					HierarchyNode hierarchyNode = flattenedNode.Node;
					bool flag2 = (in hierarchyNode) == this.m_Hierarchy.Root;
					if (!flag2)
					{
						IHierarchyProperty<int> treeViewDataProperty = this.m_TreeViewDataProperty;
						hierarchyNode = flattenedNode.Node;
						yield return treeViewDataProperty.GetValue(in hierarchyNode);
						flattenedNode = default(HierarchyFlattenedNode);
					}
				}
				HierarchyFlattened.Enumerator enumerator = default(HierarchyFlattened.Enumerator);
				yield break;
			}
			foreach (int id in rootIds)
			{
				HierarchyFlattened hierarchyFlattened = this.m_HierarchyFlattened;
				HierarchyNode hierarchyNode = this.m_IdToNodeDictionary[id];
				HierarchyFlattenedNodeChildren flattenedNodeChildren = hierarchyFlattened.EnumerateChildren(in hierarchyNode);
				foreach (ref HierarchyNode ptr2 in flattenedNodeChildren)
				{
					HierarchyNode node = ptr2;
					yield return this.m_TreeViewDataProperty.GetValue(in node);
					node = default(HierarchyNode);
				}
				HierarchyFlattenedNodeChildren.Enumerator enumerator3 = default(HierarchyFlattenedNodeChildren.Enumerator);
				yield return id;
				flattenedNodeChildren = default(HierarchyFlattenedNodeChildren);
			}
			IEnumerator<int> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000C7BC File Offset: 0x0000A9BC
		public virtual int GetParentId(int id)
		{
			HierarchyNode node = this.GetHierarchyNodeById(id);
			bool flag = (in node) == HierarchyNode.Null || !this.m_Hierarchy.Exists(in node);
			int num;
			if (flag)
			{
				num = BaseTreeView.invalidId;
			}
			else
			{
				HierarchyNode parentNode = this.m_Hierarchy.GetParent(in node);
				bool flag2 = (in parentNode) == this.m_Hierarchy.Root;
				if (flag2)
				{
					num = BaseTreeView.invalidId;
				}
				else
				{
					num = this.m_TreeViewDataProperty.GetValue(in parentNode);
				}
			}
			return num;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000C83E File Offset: 0x0000AA3E
		public virtual IEnumerable<int> GetChildrenIds(int id)
		{
			HierarchyNode nodeById = this.GetHierarchyNodeById(id);
			bool flag = (in nodeById) == HierarchyNode.Null || !this.m_Hierarchy.Exists(in nodeById);
			if (flag)
			{
				yield break;
			}
			foreach (ref HierarchyNode ptr in this.m_Hierarchy.EnumerateChildren(in nodeById))
			{
				HierarchyNode node = ptr;
				yield return this.m_TreeViewDataProperty.GetValue(in node);
				node = default(HierarchyNode);
			}
			HierarchyNodeChildren.Enumerator enumerator = default(HierarchyNodeChildren.Enumerator);
			yield break;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000C858 File Offset: 0x0000AA58
		public unsafe virtual void Move(int id, int newParentId, int childIndex = -1, bool rebuildTree = true)
		{
			bool flag = id == newParentId;
			if (!flag)
			{
				bool flag2 = this.IsChildOf(newParentId, id);
				if (!flag2)
				{
					HierarchyNode node;
					bool flag3 = !this.m_IdToNodeDictionary.TryGetValue(id, out node);
					if (!flag3)
					{
						HierarchyNode newParent = ((newParentId == BaseTreeView.invalidId) ? (*this.m_Hierarchy.Root) : this.GetHierarchyNodeById(newParentId));
						HierarchyNode currentParent = this.m_Hierarchy.GetParent(in node);
						bool flag4 = (in currentParent) == (in newParent);
						if (flag4)
						{
							int index = this.GetChildIndexForId(id);
							bool flag5 = index < childIndex;
							if (flag5)
							{
								childIndex--;
							}
						}
						else
						{
							this.m_Hierarchy.SetParent(in node, in newParent);
						}
						this.UpdateSortOrder(in newParent, in node, childIndex);
						if (rebuildTree)
						{
							this.RaiseItemParentChanged(id, newParentId);
						}
					}
				}
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000C930 File Offset: 0x0000AB30
		internal override void InvokeMakeItem(ReusableCollectionItem reusableItem)
		{
			ReusableTreeViewItem treeItem = reusableItem as ReusableTreeViewItem;
			bool flag = treeItem != null;
			if (flag)
			{
				treeItem.Init(this.MakeItem());
				this.PostInitRegistration(treeItem);
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000C964 File Offset: 0x0000AB64
		internal override void InvokeBindItem(ReusableCollectionItem reusableItem, int index)
		{
			ReusableTreeViewItem treeItem = reusableItem as ReusableTreeViewItem;
			bool flag = treeItem != null;
			if (flag)
			{
				treeItem.Indent(this.GetIndentationDepthByIndex(index));
				treeItem.SetExpandedWithoutNotify(this.IsExpandedByIndex(index));
				treeItem.SetToggleVisibility(this.HasChildrenByIndex(index));
			}
			base.InvokeBindItem(reusableItem, index);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000C9B8 File Offset: 0x0000ABB8
		internal override void InvokeDestroyItem(ReusableCollectionItem reusableItem)
		{
			ReusableTreeViewItem treeItem = reusableItem as ReusableTreeViewItem;
			bool flag = treeItem != null;
			if (flag)
			{
				treeItem.onPointerUp -= this.OnItemPointerUp;
				treeItem.onToggleValueChanged -= this.OnToggleValueChanged;
			}
			base.InvokeDestroyItem(reusableItem);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000CA05 File Offset: 0x0000AC05
		internal void PostInitRegistration(ReusableTreeViewItem treeItem)
		{
			treeItem.onPointerUp += this.OnItemPointerUp;
			treeItem.onToggleValueChanged += this.OnToggleValueChanged;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000CA30 File Offset: 0x0000AC30
		private void OnItemPointerUp(PointerUpEvent evt)
		{
			bool flag = (evt.modifiers & EventModifiers.Alt) == EventModifiers.None;
			if (!flag)
			{
				VisualElement target = evt.currentTarget as VisualElement;
				Toggle toggle = target.Q(BaseTreeView.itemToggleUssClassName, null);
				int index = ((ReusableTreeViewItem)toggle.userData).index;
				bool flag2 = !this.HasChildrenByIndex(index);
				if (!flag2)
				{
					bool wasExpanded = this.IsExpandedByIndex(index);
					bool flag3 = this.IsViewDataKeyEnabled();
					if (flag3)
					{
						int id = this.GetIdForIndex(index);
						HashSet<int> hashSet = new HashSet<int>(this.baseTreeView.expandedItemIds);
						bool flag4 = wasExpanded;
						if (flag4)
						{
							hashSet.Remove(id);
						}
						else
						{
							hashSet.Add(id);
						}
						IEnumerable<int> childrenIds = this.GetChildrenIdsByIndex(index);
						foreach (int childId in this.GetAllItemIds(childrenIds))
						{
							bool flag5 = this.HasChildren(childId);
							if (flag5)
							{
								bool flag6 = wasExpanded;
								if (flag6)
								{
									hashSet.Remove(childId);
								}
								else
								{
									hashSet.Add(childId);
								}
							}
						}
						this.baseTreeView.expandedItemIds = new List<int>(hashSet);
					}
					bool flag7 = wasExpanded;
					if (flag7)
					{
						HierarchyViewModel hierarchyViewModel = this.m_HierarchyViewModel;
						HierarchyNode hierarchyNode = this.GetHierarchyNodeByIndex(index);
						hierarchyViewModel.ClearFlags(in hierarchyNode, HierarchyNodeFlags.Expanded, true);
					}
					else
					{
						HierarchyViewModel hierarchyViewModel2 = this.m_HierarchyViewModel;
						HierarchyNode hierarchyNode = this.GetHierarchyNodeByIndex(index);
						hierarchyViewModel2.SetFlags(in hierarchyNode, HierarchyNodeFlags.Expanded, true);
					}
					this.UpdateHierarchy();
					this.baseTreeView.RefreshItems();
					this.RaiseItemExpandedChanged(this.GetIdForIndex(index), !wasExpanded, true);
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000CBDC File Offset: 0x0000ADDC
		private void RaiseItemExpandedChanged(int id, bool isExpanded, bool isAppliedToAllChildren)
		{
			Action<TreeViewExpansionChangedArgs> action = this.itemExpandedChanged;
			if (action != null)
			{
				action(new TreeViewExpansionChangedArgs
				{
					id = id,
					isExpanded = isExpanded,
					isAppliedToAllChildren = isAppliedToAllChildren
				});
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000CC10 File Offset: 0x0000AE10
		private void OnToggleValueChanged(ChangeEvent<bool> evt)
		{
			Toggle toggle = evt.target as Toggle;
			int index = ((ReusableTreeViewItem)toggle.userData).index;
			bool isExpanded = this.IsExpandedByIndex(index);
			bool flag = isExpanded;
			if (flag)
			{
				this.CollapseItemByIndex(index, false, true);
			}
			else
			{
				this.ExpandItemByIndex(index, false, true);
			}
			this.baseTreeView.scrollView.contentContainer.Focus();
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000CC74 File Offset: 0x0000AE74
		public override int GetIndexForId(int id)
		{
			HierarchyNode node;
			return this.m_IdToNodeDictionary.TryGetValue(id, out node) ? this.m_HierarchyViewModel.IndexOf(in node) : BaseTreeView.invalidId;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000CCAC File Offset: 0x0000AEAC
		public override int GetIdForIndex(int index)
		{
			int availableNodeCount = this.m_HierarchyViewModel.Count;
			bool flag = index == availableNodeCount && availableNodeCount > 0;
			int num;
			if (flag)
			{
				IHierarchyProperty<int> treeViewDataProperty = this.m_TreeViewDataProperty;
				HierarchyViewModel hierarchyViewModel = this.m_HierarchyViewModel;
				num = treeViewDataProperty.GetValue(hierarchyViewModel[hierarchyViewModel.Count - 1]);
			}
			else
			{
				num = ((!this.IsIndexValid(index)) ? BaseTreeView.invalidId : this.m_TreeViewDataProperty.GetValue(this.m_HierarchyViewModel[index]));
			}
			return num;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000CD24 File Offset: 0x0000AF24
		public virtual bool HasChildren(int id)
		{
			HierarchyNode node;
			bool flag = this.m_IdToNodeDictionary.TryGetValue(id, out node);
			return flag && this.m_Hierarchy.GetChildrenCount(in node) > 0;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000CD5C File Offset: 0x0000AF5C
		public bool Exists(int id)
		{
			return this.m_IdToNodeDictionary.ContainsKey(id);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000CD7C File Offset: 0x0000AF7C
		public bool HasChildrenByIndex(int index)
		{
			bool flag = !this.IsIndexValid(index);
			return !flag && this.m_HierarchyViewModel.GetChildrenCount(this.m_HierarchyViewModel[index]) > 0;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000CDBA File Offset: 0x0000AFBA
		public IEnumerable<int> GetChildrenIdsByIndex(int index)
		{
			bool flag = !this.IsIndexValid(index);
			if (flag)
			{
				yield break;
			}
			foreach (ref HierarchyNode ptr in this.m_Hierarchy.EnumerateChildren(this.m_HierarchyViewModel[index]))
			{
				HierarchyNode node = ptr;
				yield return this.m_TreeViewDataProperty.GetValue(in node);
				node = default(HierarchyNode);
			}
			HierarchyNodeChildren.Enumerator enumerator = default(HierarchyNodeChildren.Enumerator);
			yield break;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000CDD4 File Offset: 0x0000AFD4
		public int GetChildIndexForId(int id)
		{
			HierarchyNode node;
			bool flag = this.m_IdToNodeDictionary.TryGetValue(id, out node);
			int num;
			if (flag)
			{
				HierarchyNode parent = this.m_Hierarchy.GetParent(in node);
				bool flag2 = (in parent) == HierarchyNode.Null;
				if (flag2)
				{
					num = BaseTreeView.invalidId;
				}
				else
				{
					HierarchyNodeChildren nodes = this.m_Hierarchy.EnumerateChildren(in parent);
					int index = 0;
					foreach (ref HierarchyNode ptr in nodes)
					{
						HierarchyNode i = ptr;
						bool flag3 = (in i) == (in node);
						if (flag3)
						{
							break;
						}
						index++;
					}
					num = index;
				}
			}
			else
			{
				num = BaseTreeView.invalidId;
			}
			return num;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000CE7C File Offset: 0x0000B07C
		public int GetIndentationDepth(int id)
		{
			int depth = 0;
			int parentId = this.GetParentId(id);
			while (parentId != BaseTreeView.invalidId)
			{
				parentId = this.GetParentId(parentId);
				depth++;
			}
			return depth;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000CEB8 File Offset: 0x0000B0B8
		public int GetIndentationDepthByIndex(int index)
		{
			int id = this.GetIdForIndex(index);
			return this.GetIndentationDepth(id);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000CEDC File Offset: 0x0000B0DC
		public virtual bool CanChangeExpandedState(int id)
		{
			return true;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
		public bool IsExpanded(int id)
		{
			bool flag = this.IsViewDataKeyEnabled();
			bool flag2;
			if (flag)
			{
				flag2 = this.baseTreeView.expandedItemIds.Contains(id);
			}
			else
			{
				bool flag3;
				if (this.m_IdToNodeDictionary.ContainsKey(id))
				{
					Hierarchy hierarchy = this.m_Hierarchy;
					HierarchyNode hierarchyNode = this.m_IdToNodeDictionary[id];
					if (hierarchy.Exists(in hierarchyNode))
					{
						HierarchyViewModel hierarchyViewModel = this.m_HierarchyViewModel;
						HierarchyNode hierarchyNode2 = this.m_IdToNodeDictionary[id];
						flag3 = hierarchyViewModel.HasAllFlags(in hierarchyNode2, HierarchyNodeFlags.Expanded);
						goto IL_0067;
					}
				}
				flag3 = false;
				IL_0067:
				flag2 = flag3;
			}
			return flag2;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000CF68 File Offset: 0x0000B168
		public bool IsExpandedByIndex(int index)
		{
			bool flag = !this.IsIndexValid(index);
			return !flag && this.IsExpanded(this.GetIdForIndex(index));
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000CF9C File Offset: 0x0000B19C
		public void ExpandItemByIndex(int index, bool expandAllChildren, bool refresh = true)
		{
			using (BaseTreeViewController.K_ExpandItemByIndex.Auto())
			{
				bool flag = !this.HasChildrenByIndex(index);
				if (!flag)
				{
					HierarchyNode hierarchyNodeById = this.GetHierarchyNodeById(this.GetIdForIndex(index));
					this.ExpandItemByNode(in hierarchyNodeById, expandAllChildren, refresh);
				}
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000D004 File Offset: 0x0000B204
		public void ExpandItem(int id, bool expandAllChildren, bool refresh = true)
		{
			bool flag = !this.HasChildren(id) || !this.CanChangeExpandedState(id);
			if (!flag)
			{
				HierarchyNode node;
				bool flag2 = this.m_IdToNodeDictionary.TryGetValue(id, out node);
				if (flag2)
				{
					this.ExpandItemByNode(in node, expandAllChildren, refresh);
				}
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000D04C File Offset: 0x0000B24C
		public void CollapseItemByIndex(int index, bool collapseAllChildren, bool refresh = true)
		{
			bool flag = !this.HasChildrenByIndex(index);
			if (!flag)
			{
				HierarchyNode hierarchyNodeById = this.GetHierarchyNodeById(this.GetIdForIndex(index));
				this.CollapseItemByNode(in hierarchyNodeById, collapseAllChildren, refresh);
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000D084 File Offset: 0x0000B284
		public void CollapseItem(int id, bool collapseAllChildren, bool refresh = true)
		{
			bool flag = !this.HasChildren(id) || !this.CanChangeExpandedState(id);
			if (!flag)
			{
				HierarchyNode node;
				bool flag2 = this.m_IdToNodeDictionary.TryGetValue(id, out node);
				if (flag2)
				{
					this.CollapseItemByNode(in node, collapseAllChildren, refresh);
				}
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		private void ExpandItemByNode(in HierarchyNode node, bool expandAllChildren, bool refresh)
		{
			int id = this.m_TreeViewDataProperty.GetValue(in node);
			bool flag = !this.CanChangeExpandedState(id);
			if (!flag)
			{
				this.m_HierarchyViewModel.SetFlags(in node, HierarchyNodeFlags.Expanded, expandAllChildren);
				this.m_HierarchyHasPendingChanged = true;
				bool flag2 = this.IsViewDataKeyEnabled();
				if (flag2)
				{
					HashSet<int> hashSet = new HashSet<int>(this.baseTreeView.expandedItemIds) { id };
					if (expandAllChildren)
					{
						this.UpdateHierarchy();
						IEnumerable<int> childrenIds = this.GetChildrenIds(id);
						foreach (int childId in this.GetAllItemIds(childrenIds))
						{
							hashSet.Add(childId);
						}
					}
					this.baseTreeView.expandedItemIds.Clear();
					this.baseTreeView.expandedItemIds.AddRange(hashSet);
					this.baseTreeView.SaveViewData();
				}
				if (refresh)
				{
					this.baseTreeView.RefreshItems();
				}
				this.RaiseItemExpandedChanged(id, true, expandAllChildren);
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000D1EC File Offset: 0x0000B3EC
		private void CollapseItemByNode(in HierarchyNode node, bool collapseAllChildren, bool refresh)
		{
			int id = this.m_TreeViewDataProperty.GetValue(in node);
			bool flag = !this.CanChangeExpandedState(id);
			if (!flag)
			{
				bool flag2 = this.IsViewDataKeyEnabled();
				if (flag2)
				{
					if (collapseAllChildren)
					{
						IEnumerable<int> childrenIds = this.GetChildrenIds(id);
						foreach (int childId in this.GetAllItemIds(childrenIds))
						{
							this.baseTreeView.expandedItemIds.Remove(childId);
						}
					}
					this.baseTreeView.expandedItemIds.Remove(id);
					this.baseTreeView.SaveViewData();
				}
				HierarchyViewModel hierarchyViewModel = this.m_HierarchyViewModel;
				HierarchyNode hierarchyNodeById = this.GetHierarchyNodeById(id);
				hierarchyViewModel.ClearFlags(in hierarchyNodeById, HierarchyNodeFlags.Expanded, collapseAllChildren);
				this.m_HierarchyHasPendingChanged = true;
				if (refresh)
				{
					this.baseTreeView.RefreshItems();
				}
				this.RaiseItemExpandedChanged(id, false, collapseAllChildren);
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000D2E8 File Offset: 0x0000B4E8
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void GetExpandedItemIds(List<int> list)
		{
			bool flag = list.Count > 0;
			if (flag)
			{
				list.Clear();
			}
			bool flag2 = this.IsViewDataKeyEnabled();
			if (flag2)
			{
				list.AddRange(this.baseTreeView.expandedItemIds);
			}
			foreach (ref HierarchyNode ptr in this.m_HierarchyViewModel.EnumerateNodesWithAllFlags(HierarchyNodeFlags.Expanded))
			{
				HierarchyNode node = ptr;
				list.Add(this.m_TreeViewDataProperty.GetValue(in node));
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000D36C File Offset: 0x0000B56C
		internal bool IsViewDataKeyEnabled()
		{
			return this.baseTreeView.enableViewDataPersistence && !string.IsNullOrEmpty(this.baseTreeView.viewDataKey);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000D3A4 File Offset: 0x0000B5A4
		internal override void PreRefresh()
		{
			bool flag = !this.m_HierarchyHasPendingChanged;
			if (!flag)
			{
				this.UpdateHierarchy();
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000D3C8 File Offset: 0x0000B5C8
		private bool IsIndexValid(int index)
		{
			return index >= 0 && index < this.m_HierarchyViewModel.Count;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000D3F0 File Offset: 0x0000B5F0
		private bool IsChildOf(int childId, int id)
		{
			bool flag = childId == BaseTreeView.invalidId || id == BaseTreeView.invalidId;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				HierarchyNode childNode = this.GetHierarchyNodeById(childId);
				HierarchyNode ancestorNode = this.GetHierarchyNodeById(id);
				bool flag3 = (in ancestorNode) == (in childNode);
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					for (;;)
					{
						HierarchyNode parent;
						HierarchyNode parentNode = (parent = this.m_Hierarchy.GetParent(in childNode));
						if (!((in parent) != this.m_Hierarchy.Root))
						{
							goto Block_5;
						}
						bool flag4 = (in ancestorNode) == (in parentNode);
						if (flag4)
						{
							break;
						}
						childNode = parentNode;
					}
					return true;
					Block_5:
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000D488 File Offset: 0x0000B688
		internal void RaiseItemParentChanged(int id, int newParentId)
		{
			base.RaiseItemIndexChanged(id, newParentId);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000D494 File Offset: 0x0000B694
		internal unsafe HierarchyNode CreateNode(in HierarchyNode parent)
		{
			Hierarchy hierarchy = this.m_Hierarchy;
			HierarchyNode hierarchyNode = (((in parent) == HierarchyNode.Null) ? (*this.m_Hierarchy.Root) : parent);
			return hierarchy.Add(in hierarchyNode);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000D4DC File Offset: 0x0000B6DC
		internal void UpdateIdToNodeDictionary(int id, in HierarchyNode node, bool isAdd = true)
		{
			if (isAdd)
			{
				this.m_TreeViewDataProperty.SetValue(in node, id);
				this.m_IdToNodeDictionary[id] = node;
			}
			else
			{
				this.m_IdToNodeDictionary.Remove(id);
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000D520 File Offset: 0x0000B720
		internal void ClearIdToNodeDictionary()
		{
			this.m_IdToNodeDictionary.Clear();
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000D530 File Offset: 0x0000B730
		internal unsafe void UpdateSortOrder(in HierarchyNode newParent, in HierarchyNode insertedNode, int insertedIndex)
		{
			Span<HierarchyNode> existingChildren = this.m_Hierarchy.GetChildren(in newParent);
			bool flag = insertedIndex == -1;
			if (flag)
			{
				insertedIndex = existingChildren.Length;
			}
			int currentSortIndex = 0;
			int i = 0;
			while (i < insertedIndex && i < existingChildren.Length)
			{
				bool flag2 = (in insertedNode) == existingChildren[i];
				if (!flag2)
				{
					this.m_Hierarchy.SetSortIndex(existingChildren[i], currentSortIndex++);
				}
				i++;
			}
			this.m_Hierarchy.SetSortIndex(in insertedNode, insertedIndex);
			bool flag3 = insertedIndex == currentSortIndex;
			if (flag3)
			{
				currentSortIndex++;
			}
			for (int j = insertedIndex; j < existingChildren.Length; j++)
			{
				bool flag4 = (in insertedNode) == existingChildren[j];
				if (!flag4)
				{
					this.m_Hierarchy.SetSortIndex(existingChildren[j], currentSortIndex++);
				}
			}
			this.m_Hierarchy.SortChildren(in newParent, false);
			this.UpdateHierarchy();
			Span<HierarchyNode> newChildren = this.m_Hierarchy.GetChildren(in newParent);
			Span<HierarchyNode> span = newChildren;
			for (int k = 0; k < span.Length; k++)
			{
				HierarchyNode node = *span[k];
				this.m_Hierarchy.SetSortIndex(in node, 0);
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000D688 File Offset: 0x0000B888
		internal void OnViewDataReadyUpdateNodes()
		{
			foreach (int id in this.baseTreeView.expandedItemIds)
			{
				HierarchyNode node;
				bool flag = !this.m_IdToNodeDictionary.TryGetValue(id, out node);
				if (!flag)
				{
					this.m_HierarchyViewModel.SetFlags(in node, HierarchyNodeFlags.Expanded, false);
				}
			}
			this.UpdateHierarchy();
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000D70C File Offset: 0x0000B90C
		internal void UpdateHierarchy()
		{
			bool updateNeeded = this.m_Hierarchy.UpdateNeeded;
			if (updateNeeded)
			{
				this.m_Hierarchy.Update();
			}
			bool updateNeeded2 = this.m_HierarchyFlattened.UpdateNeeded;
			if (updateNeeded2)
			{
				this.m_HierarchyFlattened.Update();
			}
			bool updateNeeded3 = this.m_HierarchyViewModel.UpdateNeeded;
			if (updateNeeded3)
			{
				this.m_HierarchyViewModel.Update();
			}
			this.m_HierarchyHasPendingChanged = false;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000D778 File Offset: 0x0000B978
		internal unsafe HierarchyNode GetHierarchyNodeById(int id)
		{
			HierarchyNode node;
			return this.m_IdToNodeDictionary.TryGetValue(id, out node) ? node : (*HierarchyNode.Null);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000D7A8 File Offset: 0x0000B9A8
		internal unsafe HierarchyNode GetHierarchyNodeByIndex(int index)
		{
			bool flag = !this.IsIndexValid(index);
			HierarchyNode hierarchyNode;
			if (flag)
			{
				hierarchyNode = *HierarchyNode.Null;
			}
			else
			{
				hierarchyNode = *this.m_HierarchyViewModel[index];
			}
			return hierarchyNode;
		}

		// Token: 0x04000186 RID: 390
		private protected Hierarchy m_Hierarchy;

		// Token: 0x04000187 RID: 391
		private protected HierarchyFlattened m_HierarchyFlattened;

		// Token: 0x04000188 RID: 392
		private protected HierarchyViewModel m_HierarchyViewModel;

		// Token: 0x04000189 RID: 393
		private protected Dictionary<int, HierarchyNode> m_IdToNodeDictionary = new Dictionary<int, HierarchyNode>();

		// Token: 0x0400018A RID: 394
		private IHierarchyProperty<int> m_TreeViewDataProperty;

		// Token: 0x0400018B RID: 395
		private bool m_HierarchyHasPendingChanged;

		// Token: 0x0400018D RID: 397
		private static readonly ProfilerMarker K_ExpandItemByIndex = new ProfilerMarker(ProfilerCategory.Scripts, "BaseTreeViewController.ExpandItemByIndex");
	}
}
