using System;
using System.Collections.Generic;
using Unity.Hierarchy;

namespace UnityEngine.UIElements
{
	// Token: 0x0200005C RID: 92
	internal sealed class TreeDataController<T>
	{
		// Token: 0x0600032B RID: 811 RVA: 0x0000E97C File Offset: 0x0000CB7C
		public T GetDataForNode(HierarchyNode node)
		{
			TreeViewItemData<T> item;
			bool flag = this.m_NodeToItemDataDictionary.TryGetValue(node, out item);
			T t;
			if (flag)
			{
				t = item.data;
			}
			else
			{
				t = default(T);
			}
			return t;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000E9B4 File Offset: 0x0000CBB4
		internal unsafe void ConvertTreeViewItemDataToHierarchy(IEnumerable<TreeViewItemData<T>> list, Func<HierarchyNode, HierarchyNode> createNode, Action<int, HierarchyNode> updateDictionary)
		{
			bool flag = list == null;
			if (!flag)
			{
				this.m_ItemStack.Clear();
				this.m_NodeStack.Clear();
				IEnumerator<TreeViewItemData<T>> currentIterator = list.GetEnumerator();
				HierarchyNode parentNode = *HierarchyNode.Null;
				for (;;)
				{
					bool hasNext = currentIterator.MoveNext();
					bool flag2 = !hasNext;
					if (flag2)
					{
						bool flag3 = this.m_ItemStack.Count > 0;
						if (!flag3)
						{
							break;
						}
						parentNode = this.m_NodeStack.Pop();
						currentIterator = this.m_ItemStack.Pop();
					}
					else
					{
						TreeViewItemData<T> item = currentIterator.Current;
						HierarchyNode node = createNode(parentNode);
						this.UpdateNodeToDataDictionary(node, item);
						updateDictionary(item.id, node);
						bool flag4 = item.children != null && ((IList<TreeViewItemData<T>>)item.children).Count > 0;
						if (flag4)
						{
							this.m_NodeStack.Push(parentNode);
							parentNode = node;
							this.m_ItemStack.Push(currentIterator);
							currentIterator = item.children.GetEnumerator();
						}
					}
				}
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000EACF File Offset: 0x0000CCCF
		internal void UpdateNodeToDataDictionary(HierarchyNode node, TreeViewItemData<T> item)
		{
			this.m_NodeToItemDataDictionary.TryAdd(node, item);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000EAE0 File Offset: 0x0000CCE0
		internal void ClearNodeToDataDictionary()
		{
			this.m_NodeToItemDataDictionary.Clear();
		}

		// Token: 0x040001B6 RID: 438
		private Dictionary<HierarchyNode, TreeViewItemData<T>> m_NodeToItemDataDictionary = new Dictionary<HierarchyNode, TreeViewItemData<T>>();

		// Token: 0x040001B7 RID: 439
		private Stack<IEnumerator<TreeViewItemData<T>>> m_ItemStack = new Stack<IEnumerator<TreeViewItemData<T>>>();

		// Token: 0x040001B8 RID: 440
		private Stack<HierarchyNode> m_NodeStack = new Stack<HierarchyNode>();
	}
}
