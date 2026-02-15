using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Collections.Generic
{
	/// <summary>Represents a collection of objects that is maintained in sorted order.</summary>
	/// <typeparam name="T">The type of elements in the set.</typeparam>
	// Token: 0x0200032F RID: 815
	[DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class SortedSet<T> : ISet<T>, ICollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>, ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class. </summary>
		// Token: 0x06001460 RID: 5216 RVA: 0x00057FD6 File Offset: 0x000561D6
		public SortedSet()
		{
			this.comparer = Comparer<T>.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class that uses a specified comparer.</summary>
		/// <param name="comparer">The default comparer to use for comparing objects. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="comparer" /> is null.</exception>
		// Token: 0x06001461 RID: 5217 RVA: 0x00057FE9 File Offset: 0x000561E9
		public SortedSet(IComparer<T> comparer)
		{
			this.comparer = comparer ?? Comparer<T>.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class that contains serialized data.</summary>
		/// <param name="info">The object that contains the information that is required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <param name="context">The structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		// Token: 0x06001462 RID: 5218 RVA: 0x00058001 File Offset: 0x00056201
		protected SortedSet(SerializationInfo info, StreamingContext context)
		{
			this.siInfo = info;
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x00058010 File Offset: 0x00056210
		internal virtual bool InOrderTreeWalk(TreeWalkPredicate<T> action)
		{
			if (this.root == null)
			{
				return true;
			}
			Stack<SortedSet<T>.Node> stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.Log2(this.Count + 1));
			for (SortedSet<T>.Node node = this.root; node != null; node = node.Left)
			{
				stack.Push(node);
			}
			while (stack.Count != 0)
			{
				SortedSet<T>.Node node = stack.Pop();
				if (!action(node))
				{
					return false;
				}
				for (SortedSet<T>.Node node2 = node.Right; node2 != null; node2 = node2.Left)
				{
					stack.Push(node2);
				}
			}
			return true;
		}

		/// <summary>Gets the number of elements in the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>The number of elements in the <see cref="T:System.Collections.Generic.SortedSet`1" />.</returns>
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x0005808D File Offset: 0x0005628D
		public int Count
		{
			get
			{
				this.VersionCheck();
				return this.count;
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Collections.ICollection" /> is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x000028AE File Offset: 0x00000AAE
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized; otherwise, false.</returns>
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x000028AE File Offset: 0x00000AAE
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />. In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />, this property always returns the current instance.</returns>
		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x0005809B File Offset: 0x0005629B
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x00002FA0 File Offset: 0x000011A0
		internal virtual void VersionCheck()
		{
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00003BCC File Offset: 0x00001DCC
		internal virtual bool IsWithinRange(T item)
		{
			return true;
		}

		/// <summary>Adds an element to the set and returns a value that indicates if it was successfully added.</summary>
		/// <returns>true if <paramref name="item" /> is added to the set; otherwise, false. </returns>
		/// <param name="item">The element to add to the set.</param>
		// Token: 0x0600146A RID: 5226 RVA: 0x000580BD File Offset: 0x000562BD
		public bool Add(T item)
		{
			return this.AddIfNotPresent(item);
		}

		/// <summary>Adds an item to an <see cref="T:System.Collections.Generic.ICollection`1" /> object.</summary>
		/// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" /> object.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</exception>
		// Token: 0x0600146B RID: 5227 RVA: 0x000580C6 File Offset: 0x000562C6
		void ICollection<T>.Add(T item)
		{
			this.Add(item);
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x000580D0 File Offset: 0x000562D0
		internal virtual bool AddIfNotPresent(T item)
		{
			if (this.root == null)
			{
				this.root = new SortedSet<T>.Node(item, NodeColor.Black);
				this.count = 1;
				this.version++;
				return true;
			}
			SortedSet<T>.Node node = this.root;
			SortedSet<T>.Node node2 = null;
			SortedSet<T>.Node node3 = null;
			SortedSet<T>.Node node4 = null;
			this.version++;
			int num = 0;
			while (node != null)
			{
				num = this.comparer.Compare(item, node.Item);
				if (num == 0)
				{
					this.root.ColorBlack();
					return false;
				}
				if (node.Is4Node)
				{
					node.Split4Node();
					if (SortedSet<T>.Node.IsNonNullRed(node2))
					{
						this.InsertionBalance(node, ref node2, node3, node4);
					}
				}
				node4 = node3;
				node3 = node2;
				node2 = node;
				node = ((num < 0) ? node.Left : node.Right);
			}
			SortedSet<T>.Node node5 = new SortedSet<T>.Node(item, NodeColor.Red);
			if (num > 0)
			{
				node2.Right = node5;
			}
			else
			{
				node2.Left = node5;
			}
			if (node2.IsRed)
			{
				this.InsertionBalance(node5, ref node2, node3, node4);
			}
			this.root.ColorBlack();
			this.count++;
			return true;
		}

		/// <summary>Removes a specified item from the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>true if the element is found and successfully removed; otherwise, false. </returns>
		/// <param name="item">The element to remove.</param>
		// Token: 0x0600146D RID: 5229 RVA: 0x000581DA File Offset: 0x000563DA
		public bool Remove(T item)
		{
			return this.DoRemove(item);
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x000581E4 File Offset: 0x000563E4
		internal virtual bool DoRemove(T item)
		{
			if (this.root == null)
			{
				return false;
			}
			this.version++;
			SortedSet<T>.Node node = this.root;
			SortedSet<T>.Node node2 = null;
			SortedSet<T>.Node node3 = null;
			SortedSet<T>.Node node4 = null;
			SortedSet<T>.Node node5 = null;
			bool flag = false;
			while (node != null)
			{
				if (node.Is2Node)
				{
					if (node2 == null)
					{
						node.ColorRed();
					}
					else
					{
						SortedSet<T>.Node node6 = node2.GetSibling(node);
						if (node6.IsRed)
						{
							if (node2.Right == node6)
							{
								node2.RotateLeft();
							}
							else
							{
								node2.RotateRight();
							}
							node2.ColorRed();
							node6.ColorBlack();
							this.ReplaceChildOrRoot(node3, node2, node6);
							node3 = node6;
							if (node2 == node4)
							{
								node5 = node6;
							}
							node6 = node2.GetSibling(node);
						}
						if (node6.Is2Node)
						{
							node2.Merge2Nodes();
						}
						else
						{
							SortedSet<T>.Node node7 = node2.Rotate(node2.GetRotation(node, node6));
							node7.Color = node2.Color;
							node2.ColorBlack();
							node.ColorRed();
							this.ReplaceChildOrRoot(node3, node2, node7);
							if (node2 == node4)
							{
								node5 = node7;
							}
						}
					}
				}
				int num = (flag ? (-1) : this.comparer.Compare(item, node.Item));
				if (num == 0)
				{
					flag = true;
					node4 = node;
					node5 = node2;
				}
				node3 = node2;
				node2 = node;
				node = ((num < 0) ? node.Left : node.Right);
			}
			if (node4 != null)
			{
				this.ReplaceNode(node4, node5, node2, node3);
				this.count--;
			}
			SortedSet<T>.Node node8 = this.root;
			if (node8 != null)
			{
				node8.ColorBlack();
			}
			return flag;
		}

		/// <summary>Removes all elements from the set.</summary>
		// Token: 0x0600146F RID: 5231 RVA: 0x00058350 File Offset: 0x00056550
		public virtual void Clear()
		{
			this.root = null;
			this.count = 0;
			this.version++;
		}

		/// <summary>Determines whether the set contains a specific element.</summary>
		/// <returns>true if the set contains <paramref name="item" />; otherwise, false.</returns>
		/// <param name="item">The element to locate in the set.</param>
		// Token: 0x06001470 RID: 5232 RVA: 0x0005836E File Offset: 0x0005656E
		public virtual bool Contains(T item)
		{
			return this.FindNode(item) != null;
		}

		/// <summary>Copies the complete <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the specified array index.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source array is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		// Token: 0x06001471 RID: 5233 RVA: 0x0005837A File Offset: 0x0005657A
		public void CopyTo(T[] array, int index)
		{
			this.CopyTo(array, index, this.Count);
		}

		/// <summary>Copies a specified number of elements from <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the specified array index.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <param name="count">The number of elements to copy.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source array is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="count" /> is less than zero.</exception>
		// Token: 0x06001472 RID: 5234 RVA: 0x0005838C File Offset: 0x0005658C
		public void CopyTo(T[] array, int index, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (count > array.Length - index)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			count += index;
			this.InOrderTreeWalk(delegate(SortedSet<T>.Node node)
			{
				if (index >= count)
				{
					return false;
				}
				T[] array2 = array;
				int index2 = index;
				index = index2 + 1;
				array2[index2] = node.Item;
				return true;
			});
		}

		/// <summary>Copies the complete <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the specified array index.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source array is greater than the available space from <paramref name="index" /> to the end of the destination array. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		// Token: 0x06001473 RID: 5235 RVA: 0x0005844C File Offset: 0x0005664C
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			if (array.GetLowerBound(0) != 0)
			{
				throw new ArgumentException("The lower bound of target array must be zero.", "array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			object[] objects = array as object[];
			if (objects == null)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
			try
			{
				this.InOrderTreeWalk(delegate(SortedSet<T>.Node node)
				{
					object[] objects2 = objects;
					int index2 = index;
					index = index2 + 1;
					objects2[index2] = node.Item;
					return true;
				});
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>An enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedSet`1" /> in sorted order.</returns>
		// Token: 0x06001474 RID: 5236 RVA: 0x00058560 File Offset: 0x00056760
		public SortedSet<T>.Enumerator GetEnumerator()
		{
			return new SortedSet<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06001475 RID: 5237 RVA: 0x00058568 File Offset: 0x00056768
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06001476 RID: 5238 RVA: 0x00058568 File Offset: 0x00056768
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x00058578 File Offset: 0x00056778
		private void InsertionBalance(SortedSet<T>.Node current, ref SortedSet<T>.Node parent, SortedSet<T>.Node grandParent, SortedSet<T>.Node greatGrandParent)
		{
			bool flag = grandParent.Right == parent;
			bool flag2 = parent.Right == current;
			SortedSet<T>.Node node;
			if (flag == flag2)
			{
				node = (flag2 ? grandParent.RotateLeft() : grandParent.RotateRight());
			}
			else
			{
				node = (flag2 ? grandParent.RotateLeftRight() : grandParent.RotateRightLeft());
				parent = greatGrandParent;
			}
			grandParent.ColorRed();
			node.ColorBlack();
			this.ReplaceChildOrRoot(greatGrandParent, grandParent, node);
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x000585DD File Offset: 0x000567DD
		private void ReplaceChildOrRoot(SortedSet<T>.Node parent, SortedSet<T>.Node child, SortedSet<T>.Node newChild)
		{
			if (parent != null)
			{
				parent.ReplaceChild(child, newChild);
				return;
			}
			this.root = newChild;
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x000585F4 File Offset: 0x000567F4
		private void ReplaceNode(SortedSet<T>.Node match, SortedSet<T>.Node parentOfMatch, SortedSet<T>.Node successor, SortedSet<T>.Node parentOfSuccessor)
		{
			if (successor == match)
			{
				successor = match.Left;
			}
			else
			{
				SortedSet<T>.Node right = successor.Right;
				if (right != null)
				{
					right.ColorBlack();
				}
				if (parentOfSuccessor != match)
				{
					parentOfSuccessor.Left = successor.Right;
					successor.Right = match.Right;
				}
				successor.Left = match.Left;
			}
			if (successor != null)
			{
				successor.Color = match.Color;
			}
			this.ReplaceChildOrRoot(parentOfMatch, match, successor);
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x00058664 File Offset: 0x00056864
		internal virtual SortedSet<T>.Node FindNode(T item)
		{
			int num;
			for (SortedSet<T>.Node node = this.root; node != null; node = ((num < 0) ? node.Left : node.Right))
			{
				num = this.comparer.Compare(item, node.Item);
				if (num == 0)
				{
					return node;
				}
			}
			return null;
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x000586A9 File Offset: 0x000568A9
		internal void UpdateVersion()
		{
			this.version++;
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface, and returns the data that you need to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information that is required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x0600147C RID: 5244 RVA: 0x000586B9 File Offset: 0x000568B9
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.GetObjectData(info, context);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data that you must have to serialize a <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information that is required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x0600147D RID: 5245 RVA: 0x000586C4 File Offset: 0x000568C4
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Count", this.count);
			info.AddValue("Comparer", this.comparer, typeof(IComparer<T>));
			info.AddValue("Version", this.version);
			if (this.root != null)
			{
				T[] array = new T[this.Count];
				this.CopyTo(array, 0);
				info.AddValue("Items", array, typeof(T[]));
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.IDeserializationCallback" /> interface, and raises the deserialization event when the deserialization is completed.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.SortedSet`1" /> instance is invalid.</exception>
		// Token: 0x0600147E RID: 5246 RVA: 0x0005874E File Offset: 0x0005694E
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialization(sender);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface, and raises the deserialization event when the deserialization is completed.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object is invalid.</exception>
		// Token: 0x0600147F RID: 5247 RVA: 0x00058758 File Offset: 0x00056958
		protected virtual void OnDeserialization(object sender)
		{
			if (this.comparer != null)
			{
				return;
			}
			if (this.siInfo == null)
			{
				throw new SerializationException("OnDeserialization method was called while the object was not being deserialized.");
			}
			this.comparer = (IComparer<T>)this.siInfo.GetValue("Comparer", typeof(IComparer<T>));
			int @int = this.siInfo.GetInt32("Count");
			if (@int != 0)
			{
				T[] array = (T[])this.siInfo.GetValue("Items", typeof(T[]));
				if (array == null)
				{
					throw new SerializationException("The values for this dictionary are missing.");
				}
				for (int i = 0; i < array.Length; i++)
				{
					this.Add(array[i]);
				}
			}
			this.version = this.siInfo.GetInt32("Version");
			if (this.count != @int)
			{
				throw new SerializationException("The serialized Count information doesn't match the number of items.");
			}
			this.siInfo = null;
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x00058838 File Offset: 0x00056A38
		private static int Log2(int value)
		{
			int num = 0;
			while (value > 0)
			{
				num++;
				value >>= 1;
			}
			return num;
		}

		// Token: 0x04000BE1 RID: 3041
		private SortedSet<T>.Node root;

		// Token: 0x04000BE2 RID: 3042
		private IComparer<T> comparer;

		// Token: 0x04000BE3 RID: 3043
		private int count;

		// Token: 0x04000BE4 RID: 3044
		private int version;

		// Token: 0x04000BE5 RID: 3045
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04000BE6 RID: 3046
		private SerializationInfo siInfo;

		// Token: 0x04000BE7 RID: 3047
		private const string ComparerName = "Comparer";

		// Token: 0x04000BE8 RID: 3048
		private const string CountName = "Count";

		// Token: 0x04000BE9 RID: 3049
		private const string ItemsName = "Items";

		// Token: 0x04000BEA RID: 3050
		private const string VersionName = "Version";

		// Token: 0x04000BEB RID: 3051
		private const string TreeName = "Tree";

		// Token: 0x04000BEC RID: 3052
		private const string NodeValueName = "Item";

		// Token: 0x04000BED RID: 3053
		private const string EnumStartName = "EnumStarted";

		// Token: 0x04000BEE RID: 3054
		private const string ReverseName = "Reverse";

		// Token: 0x04000BEF RID: 3055
		private const string EnumVersionName = "EnumVersion";

		// Token: 0x04000BF0 RID: 3056
		private const string MinName = "Min";

		// Token: 0x04000BF1 RID: 3057
		private const string MaxName = "Max";

		// Token: 0x04000BF2 RID: 3058
		private const string LowerBoundActiveName = "lBoundActive";

		// Token: 0x04000BF3 RID: 3059
		private const string UpperBoundActiveName = "uBoundActive";

		// Token: 0x04000BF4 RID: 3060
		internal const int StackAllocThreshold = 100;

		// Token: 0x02000330 RID: 816
		[Serializable]
		internal sealed class Node
		{
			// Token: 0x06001481 RID: 5249 RVA: 0x00058857 File Offset: 0x00056A57
			public Node(T item, NodeColor color)
			{
				this.Item = item;
				this.Color = color;
			}

			// Token: 0x06001482 RID: 5250 RVA: 0x0005886D File Offset: 0x00056A6D
			public static bool IsNonNullRed(SortedSet<T>.Node node)
			{
				return node != null && node.IsRed;
			}

			// Token: 0x06001483 RID: 5251 RVA: 0x0005887A File Offset: 0x00056A7A
			public static bool IsNullOrBlack(SortedSet<T>.Node node)
			{
				return node == null || node.IsBlack;
			}

			// Token: 0x17000475 RID: 1141
			// (get) Token: 0x06001484 RID: 5252 RVA: 0x00058887 File Offset: 0x00056A87
			// (set) Token: 0x06001485 RID: 5253 RVA: 0x0005888F File Offset: 0x00056A8F
			public T Item { get; set; }

			// Token: 0x17000476 RID: 1142
			// (get) Token: 0x06001486 RID: 5254 RVA: 0x00058898 File Offset: 0x00056A98
			// (set) Token: 0x06001487 RID: 5255 RVA: 0x000588A0 File Offset: 0x00056AA0
			public SortedSet<T>.Node Left { get; set; }

			// Token: 0x17000477 RID: 1143
			// (get) Token: 0x06001488 RID: 5256 RVA: 0x000588A9 File Offset: 0x00056AA9
			// (set) Token: 0x06001489 RID: 5257 RVA: 0x000588B1 File Offset: 0x00056AB1
			public SortedSet<T>.Node Right { get; set; }

			// Token: 0x17000478 RID: 1144
			// (get) Token: 0x0600148A RID: 5258 RVA: 0x000588BA File Offset: 0x00056ABA
			// (set) Token: 0x0600148B RID: 5259 RVA: 0x000588C2 File Offset: 0x00056AC2
			public NodeColor Color { get; set; }

			// Token: 0x17000479 RID: 1145
			// (get) Token: 0x0600148C RID: 5260 RVA: 0x000588CB File Offset: 0x00056ACB
			public bool IsBlack
			{
				get
				{
					return this.Color == NodeColor.Black;
				}
			}

			// Token: 0x1700047A RID: 1146
			// (get) Token: 0x0600148D RID: 5261 RVA: 0x000588D6 File Offset: 0x00056AD6
			public bool IsRed
			{
				get
				{
					return this.Color == NodeColor.Red;
				}
			}

			// Token: 0x1700047B RID: 1147
			// (get) Token: 0x0600148E RID: 5262 RVA: 0x000588E1 File Offset: 0x00056AE1
			public bool Is2Node
			{
				get
				{
					return this.IsBlack && SortedSet<T>.Node.IsNullOrBlack(this.Left) && SortedSet<T>.Node.IsNullOrBlack(this.Right);
				}
			}

			// Token: 0x1700047C RID: 1148
			// (get) Token: 0x0600148F RID: 5263 RVA: 0x00058905 File Offset: 0x00056B05
			public bool Is4Node
			{
				get
				{
					return SortedSet<T>.Node.IsNonNullRed(this.Left) && SortedSet<T>.Node.IsNonNullRed(this.Right);
				}
			}

			// Token: 0x06001490 RID: 5264 RVA: 0x00058921 File Offset: 0x00056B21
			public void ColorBlack()
			{
				this.Color = NodeColor.Black;
			}

			// Token: 0x06001491 RID: 5265 RVA: 0x0005892A File Offset: 0x00056B2A
			public void ColorRed()
			{
				this.Color = NodeColor.Red;
			}

			// Token: 0x06001492 RID: 5266 RVA: 0x00058934 File Offset: 0x00056B34
			public TreeRotation GetRotation(SortedSet<T>.Node current, SortedSet<T>.Node sibling)
			{
				bool flag = this.Left == current;
				if (!SortedSet<T>.Node.IsNonNullRed(sibling.Left))
				{
					if (!flag)
					{
						return TreeRotation.LeftRight;
					}
					return TreeRotation.Left;
				}
				else
				{
					if (!flag)
					{
						return TreeRotation.Right;
					}
					return TreeRotation.RightLeft;
				}
			}

			// Token: 0x06001493 RID: 5267 RVA: 0x00058965 File Offset: 0x00056B65
			public SortedSet<T>.Node GetSibling(SortedSet<T>.Node node)
			{
				if (node != this.Left)
				{
					return this.Left;
				}
				return this.Right;
			}

			// Token: 0x06001494 RID: 5268 RVA: 0x0005897D File Offset: 0x00056B7D
			public void Split4Node()
			{
				this.ColorRed();
				this.Left.ColorBlack();
				this.Right.ColorBlack();
			}

			// Token: 0x06001495 RID: 5269 RVA: 0x0005899C File Offset: 0x00056B9C
			public SortedSet<T>.Node Rotate(TreeRotation rotation)
			{
				switch (rotation)
				{
				case TreeRotation.Left:
					this.Right.Right.ColorBlack();
					return this.RotateLeft();
				case TreeRotation.LeftRight:
					return this.RotateLeftRight();
				case TreeRotation.Right:
					this.Left.Left.ColorBlack();
					return this.RotateRight();
				case TreeRotation.RightLeft:
					return this.RotateRightLeft();
				default:
					return null;
				}
			}

			// Token: 0x06001496 RID: 5270 RVA: 0x00058A00 File Offset: 0x00056C00
			public SortedSet<T>.Node RotateLeft()
			{
				SortedSet<T>.Node right = this.Right;
				this.Right = right.Left;
				right.Left = this;
				return right;
			}

			// Token: 0x06001497 RID: 5271 RVA: 0x00058A28 File Offset: 0x00056C28
			public SortedSet<T>.Node RotateLeftRight()
			{
				SortedSet<T>.Node left = this.Left;
				SortedSet<T>.Node right = left.Right;
				this.Left = right.Right;
				right.Right = this;
				left.Right = right.Left;
				right.Left = left;
				return right;
			}

			// Token: 0x06001498 RID: 5272 RVA: 0x00058A6C File Offset: 0x00056C6C
			public SortedSet<T>.Node RotateRight()
			{
				SortedSet<T>.Node left = this.Left;
				this.Left = left.Right;
				left.Right = this;
				return left;
			}

			// Token: 0x06001499 RID: 5273 RVA: 0x00058A94 File Offset: 0x00056C94
			public SortedSet<T>.Node RotateRightLeft()
			{
				SortedSet<T>.Node right = this.Right;
				SortedSet<T>.Node left = right.Left;
				this.Right = left.Left;
				left.Left = this;
				right.Left = left.Right;
				left.Right = right;
				return left;
			}

			// Token: 0x0600149A RID: 5274 RVA: 0x00058AD6 File Offset: 0x00056CD6
			public void Merge2Nodes()
			{
				this.ColorBlack();
				this.Left.ColorRed();
				this.Right.ColorRed();
			}

			// Token: 0x0600149B RID: 5275 RVA: 0x00058AF4 File Offset: 0x00056CF4
			public void ReplaceChild(SortedSet<T>.Node child, SortedSet<T>.Node newChild)
			{
				if (this.Left == child)
				{
					this.Left = newChild;
					return;
				}
				this.Right = newChild;
			}
		}

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</summary>
		// Token: 0x02000331 RID: 817
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator, ISerializable, IDeserializationCallback
		{
			// Token: 0x0600149C RID: 5276 RVA: 0x00058B0E File Offset: 0x00056D0E
			internal Enumerator(SortedSet<T> set)
			{
				this = new SortedSet<T>.Enumerator(set, false);
			}

			// Token: 0x0600149D RID: 5277 RVA: 0x00058B18 File Offset: 0x00056D18
			internal Enumerator(SortedSet<T> set, bool reverse)
			{
				this._tree = set;
				set.VersionCheck();
				this._version = set.version;
				this._stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.Log2(set.Count + 1));
				this._current = null;
				this._reverse = reverse;
				this.Initialize();
			}

			/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</summary>
			/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
			/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="info" /> is null.</exception>
			// Token: 0x0600149E RID: 5278 RVA: 0x0000D54C File Offset: 0x0000B74C
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new PlatformNotSupportedException();
			}

			/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
			/// <param name="sender">The source of the deserialization event.</param>
			/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.SortedSet`1" /> instance is invalid.</exception>
			// Token: 0x0600149F RID: 5279 RVA: 0x0000D54C File Offset: 0x0000B74C
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x060014A0 RID: 5280 RVA: 0x00058B6C File Offset: 0x00056D6C
			private void Initialize()
			{
				this._current = null;
				SortedSet<T>.Node node = this._tree.root;
				while (node != null)
				{
					SortedSet<T>.Node node2 = (this._reverse ? node.Right : node.Left);
					SortedSet<T>.Node node3 = (this._reverse ? node.Left : node.Right);
					if (this._tree.IsWithinRange(node.Item))
					{
						this._stack.Push(node);
						node = node2;
					}
					else if (node2 == null || !this._tree.IsWithinRange(node2.Item))
					{
						node = node3;
					}
					else
					{
						node = node2;
					}
				}
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.SortedSet`1" /> collection.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x060014A1 RID: 5281 RVA: 0x00058C04 File Offset: 0x00056E04
			public bool MoveNext()
			{
				this._tree.VersionCheck();
				if (this._version != this._tree.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._stack.Count == 0)
				{
					this._current = null;
					return false;
				}
				this._current = this._stack.Pop();
				SortedSet<T>.Node node = (this._reverse ? this._current.Left : this._current.Right);
				while (node != null)
				{
					SortedSet<T>.Node node2 = (this._reverse ? node.Right : node.Left);
					SortedSet<T>.Node node3 = (this._reverse ? node.Left : node.Right);
					if (this._tree.IsWithinRange(node.Item))
					{
						this._stack.Push(node);
						node = node2;
					}
					else if (node3 == null || !this._tree.IsWithinRange(node3.Item))
					{
						node = node2;
					}
					else
					{
						node = node3;
					}
				}
				return true;
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.SortedSet`1.Enumerator" />. </summary>
			// Token: 0x060014A2 RID: 5282 RVA: 0x00002FA0 File Offset: 0x000011A0
			public void Dispose()
			{
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x1700047D RID: 1149
			// (get) Token: 0x060014A3 RID: 5283 RVA: 0x00058CFC File Offset: 0x00056EFC
			public T Current
			{
				get
				{
					if (this._current != null)
					{
						return this._current.Item;
					}
					return default(T);
				}
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x1700047E RID: 1150
			// (get) Token: 0x060014A4 RID: 5284 RVA: 0x00058D26 File Offset: 0x00056F26
			object IEnumerator.Current
			{
				get
				{
					if (this._current == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._current.Item;
				}
			}

			// Token: 0x1700047F RID: 1151
			// (get) Token: 0x060014A5 RID: 5285 RVA: 0x00058D4B File Offset: 0x00056F4B
			internal bool NotStartedOrEnded
			{
				get
				{
					return this._current == null;
				}
			}

			// Token: 0x060014A6 RID: 5286 RVA: 0x00058D56 File Offset: 0x00056F56
			internal void Reset()
			{
				if (this._version != this._tree.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._stack.Clear();
				this.Initialize();
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x060014A7 RID: 5287 RVA: 0x00058D87 File Offset: 0x00056F87
			void IEnumerator.Reset()
			{
				this.Reset();
			}

			// Token: 0x04000BF9 RID: 3065
			private static readonly SortedSet<T>.Node s_dummyNode = new SortedSet<T>.Node(default(T), NodeColor.Red);

			// Token: 0x04000BFA RID: 3066
			private SortedSet<T> _tree;

			// Token: 0x04000BFB RID: 3067
			private int _version;

			// Token: 0x04000BFC RID: 3068
			private Stack<SortedSet<T>.Node> _stack;

			// Token: 0x04000BFD RID: 3069
			private SortedSet<T>.Node _current;

			// Token: 0x04000BFE RID: 3070
			private bool _reverse;
		}
	}
}
