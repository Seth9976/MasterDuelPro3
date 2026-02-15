using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.TreeNode" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001F8 RID: 504
	[Editor("System.Windows.Forms.Design.TreeNodeCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public class TreeNodeCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06001587 RID: 5511 RVA: 0x0006C185 File Offset: 0x0006A385
		internal TreeNodeCollection(TreeNode owner)
		{
			this.owner = owner;
			this.nodes = new TreeNode[TreeNodeCollection.OrigSize];
		}

		/// <summary>Gets the total number of <see cref="T:System.Windows.Forms.TreeNode" /> objects in the collection.</summary>
		/// <returns>The total number of <see cref="T:System.Windows.Forms.TreeNode" /> objects in the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x0006C1A4 File Offset: 0x0006A3A4
		[Browsable(false)]
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x00002D70 File Offset: 0x00000F70
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600158A RID: 5514 RVA: 0x00002D70 File Offset: 0x00000F70
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.TreeNodeCollection" />.</returns>
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x00002F7A File Offset: 0x0000117A
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether the tree node collection has a fixed size.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x00002D70 File Offset: 0x00000F70
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the tree node at the specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> at the specified index in the <see cref="T:System.Windows.Forms.TreeNodeCollection" />.</returns>
		/// <param name="index">The zero-based index at which to get or set the item.</param>
		/// <exception cref="T:System.ArgumentException">The value set is not a <see cref="T:System.Windows.Forms.TreeNode" />.</exception>
		// Token: 0x170005B7 RID: 1463
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (!(value is TreeNode))
				{
					throw new ArgumentException("Parameter must be of type TreeNode.", "value");
				}
				this[index] = (TreeNode)value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.TreeNode" /> at the specified indexed location in the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> at the specified indexed location in the collection.</returns>
		/// <param name="index">The indexed location of the <see cref="T:System.Windows.Forms.TreeNode" /> in the collection. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> value is less than 0 or is greater than the number of tree nodes in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005B8 RID: 1464
		public virtual TreeNode this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.nodes[index];
			}
			set
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.SetupNode(value);
				this.nodes[index] = value;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x0006C228 File Offset: 0x0006A428
		private bool UsingSorting
		{
			get
			{
				TreeView treeView = ((this.owner == null) ? null : this.owner.TreeView);
				return treeView != null && (treeView.Sorted || treeView.TreeViewNodeSorter != null);
			}
		}

		/// <summary>Adds a previously created tree node to the end of the tree node collection.</summary>
		/// <returns>The zero-based index value of the <see cref="T:System.Windows.Forms.TreeNode" /> added to the tree node collection.</returns>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to add to the collection. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="node" /> is currently assigned to another <see cref="T:System.Windows.Forms.TreeView" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001592 RID: 5522 RVA: 0x0006C264 File Offset: 0x0006A464
		public virtual int Add(TreeNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			TreeView treeView = null;
			if (this.owner != null)
			{
				treeView = this.owner.TreeView;
			}
			int num;
			if (treeView != null && this.UsingSorting)
			{
				num = this.AddSorted(node);
			}
			else
			{
				if (this.count >= this.nodes.Length)
				{
					this.Grow();
				}
				num = this.count;
				this.count++;
				this.nodes[num] = node;
			}
			this.SetupNode(node);
			if (treeView != null)
			{
				treeView.OnUIACollectionChanged(this.owner, new CollectionChangeEventArgs(CollectionChangeAction.Add, node));
			}
			return num;
		}

		/// <summary>Removes all tree nodes from the collection.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001593 RID: 5523 RVA: 0x0006C2FC File Offset: 0x0006A4FC
		public virtual void Clear()
		{
			while (this.count > 0)
			{
				this.RemoveAt(0, false);
			}
			Array.Clear(this.nodes, 0, this.count);
			this.count = 0;
			if (this.owner != null)
			{
				TreeView treeView = this.owner.TreeView;
				if (treeView != null)
				{
					treeView.UpdateBelow(this.owner);
					treeView.RecalculateVisibleOrder(this.owner);
					treeView.UpdateScrollBars(false);
				}
			}
		}

		/// <summary>Determines whether the specified tree node is a member of the collection.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.TreeNode" /> is a member of the collection; otherwise, false.</returns>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to locate in the collection. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001594 RID: 5524 RVA: 0x0006C36D File Offset: 0x0006A56D
		public bool Contains(TreeNode node)
		{
			return Array.IndexOf<TreeNode>(this.nodes, node, 0, this.count) != -1;
		}

		/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
		/// <param name="dest">The destination array. </param>
		/// <param name="index">The index in the destination array at which storing begins. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001595 RID: 5525 RVA: 0x0006C388 File Offset: 0x0006A588
		public void CopyTo(Array dest, int index)
		{
			Array.Copy(this.nodes, index, dest, index, this.count);
		}

		/// <summary>Returns an enumerator that can be used to iterate through the tree node collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the tree node collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001596 RID: 5526 RVA: 0x0006C39E File Offset: 0x0006A59E
		public IEnumerator GetEnumerator()
		{
			return new TreeNodeCollection.TreeNodeEnumerator(this);
		}

		/// <summary>Returns the index of the specified tree node in the collection.</summary>
		/// <returns>The zero-based index of the item found in the tree node collection; otherwise, -1.</returns>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to locate in the collection. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001597 RID: 5527 RVA: 0x0006C3A6 File Offset: 0x0006A5A6
		public int IndexOf(TreeNode node)
		{
			return Array.IndexOf<TreeNode>(this.nodes, node);
		}

		/// <summary>Inserts an existing tree node into the tree node collection at the specified location.</summary>
		/// <param name="index">The indexed location within the collection to insert the tree node. </param>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to insert into the collection. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="node" /> is currently assigned to another <see cref="T:System.Windows.Forms.TreeView" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001598 RID: 5528 RVA: 0x0006C3B4 File Offset: 0x0006A5B4
		public virtual void Insert(int index, TreeNode node)
		{
			if (this.count >= this.nodes.Length)
			{
				this.Grow();
			}
			Array.Copy(this.nodes, index, this.nodes, index + 1, this.count - index);
			this.nodes[index] = node;
			this.count++;
			if (this.UsingSorting)
			{
				this.Sort(this.owner.TreeView.TreeViewNodeSorter);
			}
			this.SetupNode(node);
		}

		/// <summary>Removes the specified tree node from the tree node collection.</summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to remove. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001599 RID: 5529 RVA: 0x0006C430 File Offset: 0x0006A630
		public void Remove(TreeNode node)
		{
			if (node == null)
			{
				throw new NullReferenceException();
			}
			int num = this.IndexOf(node);
			if (num != -1)
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Removes a tree node from the tree node collection at a specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.TreeNode" /> to remove. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600159A RID: 5530 RVA: 0x0006C459 File Offset: 0x0006A659
		public virtual void RemoveAt(int index)
		{
			this.RemoveAt(index, true);
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x0006C464 File Offset: 0x0006A664
		private void RemoveAt(int index, bool update)
		{
			TreeNode treeNode = this.nodes[index];
			TreeNode prevNode = this.GetPrevNode(treeNode);
			TreeNode treeNode2 = null;
			bool flag = false;
			bool isVisible = treeNode.IsVisible;
			TreeView treeView = null;
			if (this.owner != null)
			{
				treeView = this.owner.TreeView;
			}
			if (treeView != null)
			{
				treeView.RecalculateVisibleOrder(prevNode);
				if (treeNode == treeView.SelectedNode)
				{
					if (treeNode.IsExpanded)
					{
						treeNode.Collapse();
					}
					flag = true;
					OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(treeNode);
					if (openTreeNodeEnumerator.MoveNext() && openTreeNodeEnumerator.MoveNext())
					{
						treeNode2 = openTreeNodeEnumerator.CurrentNode;
					}
					else
					{
						openTreeNodeEnumerator = new OpenTreeNodeEnumerator(treeNode);
						openTreeNodeEnumerator.MovePrevious();
						treeNode2 = ((openTreeNodeEnumerator.CurrentNode == treeNode) ? null : openTreeNodeEnumerator.CurrentNode);
					}
				}
			}
			Array.Copy(this.nodes, index + 1, this.nodes, index, this.count - index - 1);
			this.count--;
			this.nodes[this.count] = null;
			if (this.nodes.Length > TreeNodeCollection.OrigSize && this.nodes.Length > this.count * 2)
			{
				this.Shrink();
			}
			if (treeView != null && flag)
			{
				treeView.SelectedNode = treeNode2;
			}
			TreeNode parent = treeNode.parent;
			treeNode.parent = null;
			if (update && treeView != null && isVisible)
			{
				treeView.RecalculateVisibleOrder(prevNode);
				treeView.UpdateScrollBars(false);
				treeView.UpdateBelow(parent);
			}
			if (treeView != null)
			{
				treeView.OnUIACollectionChanged(this.owner, new CollectionChangeEventArgs(CollectionChangeAction.Remove, treeNode));
			}
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x0006C5D8 File Offset: 0x0006A7D8
		private TreeNode GetPrevNode(TreeNode node)
		{
			OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(node);
			if (openTreeNodeEnumerator.MovePrevious() && openTreeNodeEnumerator.MovePrevious())
			{
				return openTreeNodeEnumerator.CurrentNode;
			}
			return null;
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x0006C604 File Offset: 0x0006A804
		private void SetupNode(TreeNode node)
		{
			node.parent = this.owner;
			TreeView treeView = null;
			if (this.owner != null)
			{
				treeView = this.owner.TreeView;
			}
			if (treeView != null)
			{
				TreeNode treeNode = (this.UsingSorting ? this.owner : this.GetPrevNode(node));
				if (treeView.IsHandleCreated && node.ArePreviousNodesExpanded)
				{
					treeView.RecalculateVisibleOrder(treeNode);
				}
				if (this.owner == treeView.root_node || (node.Parent.IsVisible && node.Parent.IsExpanded))
				{
					treeView.UpdateScrollBars(false);
				}
				treeView.UpdateBelow(this.owner);
			}
		}

		/// <summary>Adds an object to the end of the tree node collection.</summary>
		/// <returns>The zero-based index value of the <see cref="T:System.Windows.Forms.TreeNode" /> that was added to the tree node collection.</returns>
		/// <param name="node">The object to add to the tree node collection.</param>
		/// <exception cref="T:System.Exception">
		///   <paramref name="node" /> is currently assigned to another <see cref="T:System.Windows.Forms.TreeView" /> control.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.</exception>
		// Token: 0x0600159E RID: 5534 RVA: 0x0006C6A0 File Offset: 0x0006A8A0
		int IList.Add(object node)
		{
			return this.Add((TreeNode)node);
		}

		/// <summary>Determines whether the specified tree node is a member of the collection.</summary>
		/// <returns>true if <paramref name="node" /> is a member of the collection; otherwise, false.</returns>
		/// <param name="node">The object to find in the collection.</param>
		// Token: 0x0600159F RID: 5535 RVA: 0x0006C6AE File Offset: 0x0006A8AE
		bool IList.Contains(object node)
		{
			return this.Contains((TreeNode)node);
		}

		/// <summary>Returns the index of the specified tree node in the collection.</summary>
		/// <returns>The zero-based index of the item found in the tree node collection; otherwise, -1.</returns>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to locate in the collection.</param>
		// Token: 0x060015A0 RID: 5536 RVA: 0x0006C6BC File Offset: 0x0006A8BC
		int IList.IndexOf(object node)
		{
			return this.IndexOf((TreeNode)node);
		}

		/// <summary>Inserts an existing tree node in the tree node collection at the specified location.</summary>
		/// <param name="index">The indexed location within the collection to insert the tree node. </param>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to insert into the collection.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="node" /> is currently assigned to another <see cref="T:System.Windows.Forms.TreeView" />.-or-<paramref name="node" /> is not a <see cref="T:System.Windows.Forms.TreeNode" />.</exception>
		// Token: 0x060015A1 RID: 5537 RVA: 0x0006C6CA File Offset: 0x0006A8CA
		void IList.Insert(int index, object node)
		{
			this.Insert(index, (TreeNode)node);
		}

		/// <summary>Removes the specified tree node from the tree node collection.</summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to remove from the collection.</param>
		// Token: 0x060015A2 RID: 5538 RVA: 0x0006C6D9 File Offset: 0x0006A8D9
		void IList.Remove(object node)
		{
			this.Remove((TreeNode)node);
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0006C6E8 File Offset: 0x0006A8E8
		private int AddSorted(TreeNode node)
		{
			if (this.count >= this.nodes.Length)
			{
				this.Grow();
			}
			TreeView treeView = this.owner.TreeView;
			if (treeView.TreeViewNodeSorter != null)
			{
				TreeNode[] array = this.nodes;
				int num = this.count;
				this.count = num + 1;
				array[num] = node;
				this.Sort(treeView.TreeViewNodeSorter);
				return this.count - 1;
			}
			CompareInfo compareInfo = Application.CurrentCulture.CompareInfo;
			int num2 = 0;
			bool flag = false;
			for (int i = 0; i < this.count; i++)
			{
				num2 = i;
				if (compareInfo.Compare(node.Text, this.nodes[i].Text) < 0)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				num2 = this.count;
			}
			for (int j = this.count - 1; j >= num2; j--)
			{
				this.nodes[j + 1] = this.nodes[j];
			}
			this.count++;
			this.nodes[num2] = node;
			return num2;
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0006C7E8 File Offset: 0x0006A9E8
		internal void Sort(IComparer sorter)
		{
			Array array = this.nodes;
			int num = 0;
			int num2 = this.count;
			IComparer comparer;
			if (sorter != null)
			{
				comparer = sorter;
			}
			else
			{
				IComparer comparer2 = new TreeNodeCollection.TreeNodeComparer(Application.CurrentCulture.CompareInfo);
				comparer = comparer2;
			}
			Array.Sort(array, num, num2, comparer);
			for (int i = 0; i < this.count; i++)
			{
				this.nodes[i].Nodes.Sort(sorter);
			}
			TreeView treeView = ((this.owner == null) ? null : this.owner.TreeView);
			if (treeView != null)
			{
				treeView.sorted = true;
			}
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x0006C864 File Offset: 0x0006AA64
		private void Grow()
		{
			TreeNode[] array = new TreeNode[this.nodes.Length + 50];
			Array.Copy(this.nodes, array, this.nodes.Length);
			this.nodes = array;
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x0006C8A0 File Offset: 0x0006AAA0
		private void Shrink()
		{
			TreeNode[] array = new TreeNode[(this.count + 1 > TreeNodeCollection.OrigSize) ? (this.count + 1) : TreeNodeCollection.OrigSize];
			Array.Copy(this.nodes, array, this.count);
			this.nodes = array;
		}

		// Token: 0x04000CF4 RID: 3316
		private static readonly int OrigSize = 50;

		// Token: 0x04000CF5 RID: 3317
		private TreeNode owner;

		// Token: 0x04000CF6 RID: 3318
		private int count;

		// Token: 0x04000CF7 RID: 3319
		private TreeNode[] nodes;

		// Token: 0x020001F9 RID: 505
		internal class TreeNodeEnumerator : IEnumerator
		{
			// Token: 0x060015A8 RID: 5544 RVA: 0x0006C8F3 File Offset: 0x0006AAF3
			public TreeNodeEnumerator(TreeNodeCollection collection)
			{
				this.collection = collection;
			}

			// Token: 0x170005BA RID: 1466
			// (get) Token: 0x060015A9 RID: 5545 RVA: 0x0006C909 File Offset: 0x0006AB09
			public object Current
			{
				get
				{
					if (this.index == -1)
					{
						return null;
					}
					return this.collection[this.index];
				}
			}

			// Token: 0x060015AA RID: 5546 RVA: 0x0006C927 File Offset: 0x0006AB27
			public bool MoveNext()
			{
				if (this.index + 1 >= this.collection.Count)
				{
					return false;
				}
				this.index++;
				return true;
			}

			// Token: 0x060015AB RID: 5547 RVA: 0x0006C94F File Offset: 0x0006AB4F
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x04000CF8 RID: 3320
			private TreeNodeCollection collection;

			// Token: 0x04000CF9 RID: 3321
			private int index = -1;
		}

		// Token: 0x020001FA RID: 506
		private class TreeNodeComparer : IComparer
		{
			// Token: 0x060015AC RID: 5548 RVA: 0x0006C958 File Offset: 0x0006AB58
			public TreeNodeComparer(CompareInfo compare)
			{
				this.compare = compare;
			}

			// Token: 0x060015AD RID: 5549 RVA: 0x0006C968 File Offset: 0x0006AB68
			public int Compare(object x, object y)
			{
				TreeNode treeNode = (TreeNode)x;
				TreeNode treeNode2 = (TreeNode)y;
				int num = this.compare.Compare(treeNode.Text, treeNode2.Text);
				if (num != 0)
				{
					return num;
				}
				return treeNode.Index - treeNode2.Index;
			}

			// Token: 0x04000CFA RID: 3322
			private CompareInfo compare;
		}
	}
}
