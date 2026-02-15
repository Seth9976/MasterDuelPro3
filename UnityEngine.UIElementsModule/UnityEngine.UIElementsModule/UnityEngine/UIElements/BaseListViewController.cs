using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Properties;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004F RID: 79
	public abstract class BaseListViewController : CollectionViewController
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600025F RID: 607 RVA: 0x0000BB34 File Offset: 0x00009D34
		// (remove) Token: 0x06000260 RID: 608 RVA: 0x0000BB6C File Offset: 0x00009D6C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action itemsSourceSizeChanged;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000261 RID: 609 RVA: 0x0000BBA4 File Offset: 0x00009DA4
		// (remove) Token: 0x06000262 RID: 610 RVA: 0x0000BBDC File Offset: 0x00009DDC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<IEnumerable<int>> itemsAdded;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000263 RID: 611 RVA: 0x0000BC14 File Offset: 0x00009E14
		// (remove) Token: 0x06000264 RID: 612 RVA: 0x0000BC4C File Offset: 0x00009E4C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<IEnumerable<int>> itemsRemoved;

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000BC81 File Offset: 0x00009E81
		protected BaseListView baseListView
		{
			get
			{
				return base.view as BaseListView;
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000BC90 File Offset: 0x00009E90
		internal override void InvokeMakeItem(ReusableCollectionItem reusableItem)
		{
			ReusableListViewItem listItem = reusableItem as ReusableListViewItem;
			bool flag = listItem != null;
			if (flag)
			{
				listItem.Init(this.MakeItem(), this.baseListView.reorderable && this.baseListView.reorderMode == ListViewReorderMode.Animated);
				this.PostInitRegistration(listItem);
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000BCE4 File Offset: 0x00009EE4
		internal void PostInitRegistration(ReusableListViewItem listItem)
		{
			listItem.bindableElement.style.position = Position.Relative;
			listItem.bindableElement.style.flexBasis = StyleKeyword.Initial;
			listItem.bindableElement.style.marginTop = 0f;
			listItem.bindableElement.style.marginBottom = 0f;
			listItem.bindableElement.style.paddingTop = 0f;
			listItem.bindableElement.style.flexGrow = 0f;
			listItem.bindableElement.style.flexShrink = 0f;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000BDA8 File Offset: 0x00009FA8
		internal override void SetBindingContext(ReusableCollectionItem reusableItem, int index)
		{
			base.SetBindingContext(reusableItem, index);
			bool autoAssignSource = this.baseListView.autoAssignSource;
			if (autoAssignSource)
			{
				reusableItem.rootElement.dataSource = this.itemsSource;
				reusableItem.rootElement.dataSourcePath = PropertyPath.FromIndex(index);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000BDF4 File Offset: 0x00009FF4
		internal override void InvokeBindItem(ReusableCollectionItem reusableItem, int index)
		{
			ReusableListViewItem listItem = reusableItem as ReusableListViewItem;
			bool flag = listItem != null;
			if (flag)
			{
				bool usesAnimatedDragger = this.baseListView.reorderable && this.baseListView.reorderMode == ListViewReorderMode.Animated;
				listItem.UpdateDragHandle(usesAnimatedDragger && this.NeedsDragHandle(index));
			}
			base.InvokeBindItem(reusableItem, index);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000BE50 File Offset: 0x0000A050
		public virtual bool NeedsDragHandle(int index)
		{
			return true;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000BE64 File Offset: 0x0000A064
		public virtual void AddItems(int itemCount)
		{
			bool flag = itemCount <= 0;
			if (!flag)
			{
				this.EnsureItemSourceCanBeResized();
				int previousCount = this.GetItemsCount();
				List<int> indices = CollectionPool<List<int>, int>.Get();
				try
				{
					bool isFixedSize = this.itemsSource.IsFixedSize;
					if (isFixedSize)
					{
						this.itemsSource = BaseListViewController.AddToArray((Array)this.itemsSource, itemCount);
						for (int i = 0; i < itemCount; i++)
						{
							indices.Add(previousCount + i);
						}
					}
					else
					{
						Type sourceType = this.itemsSource.GetType();
						Type listType = sourceType.GetInterfaces().FirstOrDefault(new Func<Type, bool>(BaseListViewController.<AddItems>g__IsGenericList|17_0));
						bool flag2 = listType != null && listType.GetGenericArguments()[0].IsValueType;
						if (flag2)
						{
							Type elementValueType = listType.GetGenericArguments()[0];
							for (int j = 0; j < itemCount; j++)
							{
								indices.Add(previousCount + j);
								this.itemsSource.Add(Activator.CreateInstance(elementValueType));
							}
						}
						else
						{
							for (int k = 0; k < itemCount; k++)
							{
								indices.Add(previousCount + k);
								this.itemsSource.Add(null);
							}
						}
					}
					this.RaiseItemsAdded(indices);
				}
				finally
				{
					CollectionPool<List<int>, int>.Release(indices);
				}
				this.RaiseOnSizeChanged();
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000BFE4 File Offset: 0x0000A1E4
		public virtual void Move(int index, int newIndex)
		{
			bool flag = this.itemsSource == null;
			if (!flag)
			{
				bool flag2 = index == newIndex;
				if (!flag2)
				{
					int minIndex = Mathf.Min(index, newIndex);
					int maxIndex = Mathf.Max(index, newIndex);
					bool flag3 = minIndex < 0 || maxIndex >= this.itemsSource.Count;
					if (!flag3)
					{
						int destinationIndex = newIndex;
						int direction = ((newIndex < index) ? 1 : (-1));
						while (Mathf.Min(index, newIndex) < Mathf.Max(index, newIndex))
						{
							this.Swap(index, newIndex);
							newIndex += direction;
						}
						base.RaiseItemIndexChanged(index, destinationIndex);
					}
				}
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000C07C File Offset: 0x0000A27C
		public virtual void RemoveItem(int index)
		{
			List<int> indices;
			using (CollectionPool<List<int>, int>.Get(out indices))
			{
				indices.Add(index);
				this.RemoveItems(indices);
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public virtual void RemoveItems(List<int> indices)
		{
			this.EnsureItemSourceCanBeResized();
			bool flag = indices == null;
			if (!flag)
			{
				indices.Sort();
				this.RaiseItemsRemoved(indices);
				bool isFixedSize = this.itemsSource.IsFixedSize;
				if (isFixedSize)
				{
					this.itemsSource = BaseListViewController.RemoveFromArray((Array)this.itemsSource, indices);
				}
				else
				{
					for (int i = indices.Count - 1; i >= 0; i--)
					{
						this.itemsSource.RemoveAt(indices[i]);
					}
				}
				this.RaiseOnSizeChanged();
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000C15C File Offset: 0x0000A35C
		internal virtual void RemoveItems(int itemCount)
		{
			bool flag = itemCount <= 0;
			if (!flag)
			{
				int previousCount = this.GetItemsCount();
				List<int> indices = CollectionPool<List<int>, int>.Get();
				try
				{
					int newItemCount = previousCount - itemCount;
					for (int i = newItemCount; i < previousCount; i++)
					{
						indices.Add(i);
					}
					this.RemoveItems(indices);
				}
				finally
				{
					CollectionPool<List<int>, int>.Release(indices);
				}
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000C1D0 File Offset: 0x0000A3D0
		public virtual void ClearItems()
		{
			bool flag = this.itemsSource == null;
			if (!flag)
			{
				this.EnsureItemSourceCanBeResized();
				IEnumerable<int> itemsSourceIndices = Enumerable.Range(0, this.itemsSource.Count - 1);
				this.itemsSource.Clear();
				this.RaiseItemsRemoved(itemsSourceIndices);
				this.RaiseOnSizeChanged();
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000C223 File Offset: 0x0000A423
		protected void RaiseOnSizeChanged()
		{
			Action action = this.itemsSourceSizeChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000C238 File Offset: 0x0000A438
		protected void RaiseItemsAdded(IEnumerable<int> indices)
		{
			Action<IEnumerable<int>> action = this.itemsAdded;
			if (action != null)
			{
				action(indices);
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000C24E File Offset: 0x0000A44E
		protected void RaiseItemsRemoved(IEnumerable<int> indices)
		{
			Action<IEnumerable<int>> action = this.itemsRemoved;
			if (action != null)
			{
				action(indices);
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000C264 File Offset: 0x0000A464
		private static Array AddToArray(Array source, int itemCount)
		{
			Type elementType = source.GetType().GetElementType();
			bool flag = elementType == null;
			if (flag)
			{
				throw new InvalidOperationException("Cannot resize source, because its size is fixed.");
			}
			Array newItemsSource = Array.CreateInstance(elementType, source.Length + itemCount);
			Array.Copy(source, newItemsSource, source.Length);
			return newItemsSource;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000C2B8 File Offset: 0x0000A4B8
		private static Array RemoveFromArray(Array source, List<int> indicesToRemove)
		{
			int count = source.Length;
			int newCount = count - indicesToRemove.Count;
			bool flag = newCount < 0;
			if (flag)
			{
				throw new InvalidOperationException("Cannot remove more items than the current count from source.");
			}
			Type elementType = source.GetType().GetElementType();
			bool flag2 = newCount == 0;
			Array array;
			if (flag2)
			{
				array = Array.CreateInstance(elementType, 0);
			}
			else
			{
				Array newSource = Array.CreateInstance(elementType, newCount);
				int newSourceIndex = 0;
				int toRemove = 0;
				for (int index = 0; index < source.Length; index++)
				{
					bool flag3 = toRemove < indicesToRemove.Count && indicesToRemove[toRemove] == index;
					if (flag3)
					{
						toRemove++;
					}
					else
					{
						newSource.SetValue(source.GetValue(index), newSourceIndex);
						newSourceIndex++;
					}
				}
				array = newSource;
			}
			return array;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000C380 File Offset: 0x0000A580
		private void Swap(int lhs, int rhs)
		{
			IList itemsSource = this.itemsSource;
			IList itemsSource2 = this.itemsSource;
			object obj = this.itemsSource[rhs];
			object obj2 = this.itemsSource[lhs];
			itemsSource[lhs] = obj;
			itemsSource2[rhs] = obj2;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000C3D4 File Offset: 0x0000A5D4
		private void EnsureItemSourceCanBeResized()
		{
			IList itemsSource = this.itemsSource;
			Type itemsSourceType = ((itemsSource != null) ? itemsSource.GetType() : null);
			bool itemsSourceIsArray = itemsSourceType != null && itemsSourceType.IsArray;
			bool flag = this.itemsSource == null || (this.itemsSource.IsFixedSize && !itemsSourceIsArray);
			if (flag)
			{
				throw new InvalidOperationException("Cannot add or remove items from source, because it is null or its size is fixed.");
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000C439 File Offset: 0x0000A639
		[CompilerGenerated]
		internal static bool <AddItems>g__IsGenericList|17_0(Type t)
		{
			return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IList<>);
		}
	}
}
