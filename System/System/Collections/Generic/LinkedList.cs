using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Collections.Generic
{
	/// <summary>Represents a doubly linked list.</summary>
	/// <typeparam name="T">Specifies the element type of the linked list.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000318 RID: 792
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
	[Serializable]
	public class LinkedList<T> : ICollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>, ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.LinkedList`1" /> class that is empty.</summary>
		// Token: 0x06001359 RID: 4953 RVA: 0x000026E5 File Offset: 0x000008E5
		public LinkedList()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.LinkedList`1" /> class that is serializable with the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		// Token: 0x0600135A RID: 4954 RVA: 0x000557D4 File Offset: 0x000539D4
		protected LinkedList(SerializationInfo info, StreamingContext context)
		{
			this._siInfo = info;
		}

		/// <summary>Gets the number of nodes actually contained in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>The number of nodes actually contained in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x000557E3 File Offset: 0x000539E3
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		/// <summary>Gets the first node of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>The first <see cref="T:System.Collections.Generic.LinkedListNode`1" /> of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600135C RID: 4956 RVA: 0x000557EB File Offset: 0x000539EB
		public LinkedListNode<T> First
		{
			get
			{
				return this.head;
			}
		}

		/// <summary>Gets the last node of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>The last <see cref="T:System.Collections.Generic.LinkedListNode`1" /> of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x000557F3 File Offset: 0x000539F3
		public LinkedListNode<T> Last
		{
			get
			{
				if (this.head != null)
				{
					return this.head.prev;
				}
				return null;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.LinkedList`1" />, this property always returns false.</returns>
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x000028AE File Offset: 0x00000AAE
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds an item at the end of the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
		/// <param name="value">The value to add at the end of the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		// Token: 0x0600135F RID: 4959 RVA: 0x0005580A File Offset: 0x00053A0A
		void ICollection<T>.Add(T value)
		{
			this.AddLast(value);
		}

		/// <summary>Adds the specified new node before the specified existing node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <param name="node">The <see cref="T:System.Collections.Generic.LinkedListNode`1" /> before which to insert <paramref name="newNode" />.</param>
		/// <param name="newNode">The new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> to add to the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.-or-<paramref name="newNode" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="node" /> is not in the current <see cref="T:System.Collections.Generic.LinkedList`1" />.-or-<paramref name="newNode" /> belongs to another <see cref="T:System.Collections.Generic.LinkedList`1" />.</exception>
		// Token: 0x06001360 RID: 4960 RVA: 0x00055814 File Offset: 0x00053A14
		public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
		{
			this.ValidateNode(node);
			this.ValidateNewNode(newNode);
			this.InternalInsertNodeBefore(node, newNode);
			newNode.list = this;
			if (node == this.head)
			{
				this.head = newNode;
			}
		}

		/// <summary>Adds a new node containing the specified value at the start of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>The new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> containing <paramref name="value" />.</returns>
		/// <param name="value">The value to add at the start of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		// Token: 0x06001361 RID: 4961 RVA: 0x00055844 File Offset: 0x00053A44
		public LinkedListNode<T> AddFirst(T value)
		{
			LinkedListNode<T> linkedListNode = new LinkedListNode<T>(this, value);
			if (this.head == null)
			{
				this.InternalInsertNodeToEmptyList(linkedListNode);
			}
			else
			{
				this.InternalInsertNodeBefore(this.head, linkedListNode);
				this.head = linkedListNode;
			}
			return linkedListNode;
		}

		/// <summary>Adds the specified new node at the start of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <param name="node">The new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> to add at the start of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="node" /> belongs to another <see cref="T:System.Collections.Generic.LinkedList`1" />.</exception>
		// Token: 0x06001362 RID: 4962 RVA: 0x0005587F File Offset: 0x00053A7F
		public void AddFirst(LinkedListNode<T> node)
		{
			this.ValidateNewNode(node);
			if (this.head == null)
			{
				this.InternalInsertNodeToEmptyList(node);
			}
			else
			{
				this.InternalInsertNodeBefore(this.head, node);
				this.head = node;
			}
			node.list = this;
		}

		/// <summary>Adds a new node containing the specified value at the end of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>The new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> containing <paramref name="value" />.</returns>
		/// <param name="value">The value to add at the end of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		// Token: 0x06001363 RID: 4963 RVA: 0x000558B4 File Offset: 0x00053AB4
		public LinkedListNode<T> AddLast(T value)
		{
			LinkedListNode<T> linkedListNode = new LinkedListNode<T>(this, value);
			if (this.head == null)
			{
				this.InternalInsertNodeToEmptyList(linkedListNode);
			}
			else
			{
				this.InternalInsertNodeBefore(this.head, linkedListNode);
			}
			return linkedListNode;
		}

		/// <summary>Adds the specified new node at the end of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <param name="node">The new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> to add at the end of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="node" /> belongs to another <see cref="T:System.Collections.Generic.LinkedList`1" />.</exception>
		// Token: 0x06001364 RID: 4964 RVA: 0x000558E8 File Offset: 0x00053AE8
		public void AddLast(LinkedListNode<T> node)
		{
			this.ValidateNewNode(node);
			if (this.head == null)
			{
				this.InternalInsertNodeToEmptyList(node);
			}
			else
			{
				this.InternalInsertNodeBefore(this.head, node);
			}
			node.list = this;
		}

		/// <summary>Removes all nodes from the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		// Token: 0x06001365 RID: 4965 RVA: 0x00055918 File Offset: 0x00053B18
		public void Clear()
		{
			LinkedListNode<T> next = this.head;
			while (next != null)
			{
				LinkedListNode<T> linkedListNode = next;
				next = next.Next;
				linkedListNode.Invalidate();
			}
			this.head = null;
			this.count = 0;
			this.version++;
		}

		/// <summary>Determines whether a value is in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>true if <paramref name="value" /> is found in the <see cref="T:System.Collections.Generic.LinkedList`1" />; otherwise, false.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.LinkedList`1" />. The value can be null for reference types.</param>
		// Token: 0x06001366 RID: 4966 RVA: 0x0005595C File Offset: 0x00053B5C
		public bool Contains(T value)
		{
			return this.Find(value) != null;
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.Generic.LinkedList`1" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.LinkedList`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.LinkedList`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x06001367 RID: 4967 RVA: 0x00055968 File Offset: 0x00053B68
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
			}
			if (index > array.Length)
			{
				throw new ArgumentOutOfRangeException("index", index, "Must be less than or equal to the size of the collection.");
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException("Insufficient space in the target location to copy the information.");
			}
			LinkedListNode<T> next = this.head;
			if (next != null)
			{
				do
				{
					array[index++] = next.item;
					next = next.next;
				}
				while (next != this.head);
			}
		}

		/// <summary>Finds the first node that contains the specified value.</summary>
		/// <returns>The first <see cref="T:System.Collections.Generic.LinkedListNode`1" /> that contains the specified value, if found; otherwise, null.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		// Token: 0x06001368 RID: 4968 RVA: 0x00055A00 File Offset: 0x00053C00
		public LinkedListNode<T> Find(T value)
		{
			LinkedListNode<T> linkedListNode = this.head;
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			if (linkedListNode != null)
			{
				if (value != null)
				{
					while (!@default.Equals(linkedListNode.item, value))
					{
						linkedListNode = linkedListNode.next;
						if (linkedListNode == this.head)
						{
							goto IL_005A;
						}
					}
					return linkedListNode;
				}
				while (linkedListNode.item != null)
				{
					linkedListNode = linkedListNode.next;
					if (linkedListNode == this.head)
					{
						goto IL_005A;
					}
				}
				return linkedListNode;
			}
			IL_005A:
			return null;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.LinkedList`1.Enumerator" /> for the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x06001369 RID: 4969 RVA: 0x00055A68 File Offset: 0x00053C68
		public LinkedList<T>.Enumerator GetEnumerator()
		{
			return new LinkedList<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
		// Token: 0x0600136A RID: 4970 RVA: 0x00055A70 File Offset: 0x00053C70
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Removes the first occurrence of the specified value from the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>true if the element containing <paramref name="value" /> is successfully removed; otherwise, false.  This method also returns false if <paramref name="value" /> was not found in the original <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		/// <param name="value">The value to remove from the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		// Token: 0x0600136B RID: 4971 RVA: 0x00055A80 File Offset: 0x00053C80
		public bool Remove(T value)
		{
			LinkedListNode<T> linkedListNode = this.Find(value);
			if (linkedListNode != null)
			{
				this.InternalRemoveNode(linkedListNode);
				return true;
			}
			return false;
		}

		/// <summary>Removes the specified node from the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <param name="node">The <see cref="T:System.Collections.Generic.LinkedListNode`1" /> to remove from the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="node" /> is not in the current <see cref="T:System.Collections.Generic.LinkedList`1" />.</exception>
		// Token: 0x0600136C RID: 4972 RVA: 0x00055AA2 File Offset: 0x00053CA2
		public void Remove(LinkedListNode<T> node)
		{
			this.ValidateNode(node);
			this.InternalRemoveNode(node);
		}

		/// <summary>Removes the node at the start of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Generic.LinkedList`1" /> is empty.</exception>
		// Token: 0x0600136D RID: 4973 RVA: 0x00055AB2 File Offset: 0x00053CB2
		public void RemoveFirst()
		{
			if (this.head == null)
			{
				throw new InvalidOperationException("The LinkedList is empty.");
			}
			this.InternalRemoveNode(this.head);
		}

		/// <summary>Removes the node at the end of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Generic.LinkedList`1" /> is empty.</exception>
		// Token: 0x0600136E RID: 4974 RVA: 0x00055AD3 File Offset: 0x00053CD3
		public void RemoveLast()
		{
			if (this.head == null)
			{
				throw new InvalidOperationException("The LinkedList is empty.");
			}
			this.InternalRemoveNode(this.head.prev);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Generic.LinkedList`1" /> instance.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.LinkedList`1" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.LinkedList`1" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x0600136F RID: 4975 RVA: 0x00055AFC File Offset: 0x00053CFC
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Version", this.version);
			info.AddValue("Count", this.count);
			if (this.count != 0)
			{
				T[] array = new T[this.count];
				this.CopyTo(array, 0);
				info.AddValue("Data", array, typeof(T[]));
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.LinkedList`1" /> instance is invalid.</exception>
		// Token: 0x06001370 RID: 4976 RVA: 0x00055B6C File Offset: 0x00053D6C
		public virtual void OnDeserialization(object sender)
		{
			if (this._siInfo == null)
			{
				return;
			}
			int @int = this._siInfo.GetInt32("Version");
			if (this._siInfo.GetInt32("Count") != 0)
			{
				T[] array = (T[])this._siInfo.GetValue("Data", typeof(T[]));
				if (array == null)
				{
					throw new SerializationException("The values for this dictionary are missing.");
				}
				for (int i = 0; i < array.Length; i++)
				{
					this.AddLast(array[i]);
				}
			}
			else
			{
				this.head = null;
			}
			this.version = @int;
			this._siInfo = null;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00055C08 File Offset: 0x00053E08
		private void InternalInsertNodeBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
		{
			newNode.next = node;
			newNode.prev = node.prev;
			node.prev.next = newNode;
			node.prev = newNode;
			this.version++;
			this.count++;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00055C57 File Offset: 0x00053E57
		private void InternalInsertNodeToEmptyList(LinkedListNode<T> newNode)
		{
			newNode.next = newNode;
			newNode.prev = newNode;
			this.head = newNode;
			this.version++;
			this.count++;
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00055C8C File Offset: 0x00053E8C
		internal void InternalRemoveNode(LinkedListNode<T> node)
		{
			if (node.next == node)
			{
				this.head = null;
			}
			else
			{
				node.next.prev = node.prev;
				node.prev.next = node.next;
				if (this.head == node)
				{
					this.head = node.next;
				}
			}
			node.Invalidate();
			this.count--;
			this.version++;
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00055D04 File Offset: 0x00053F04
		internal void ValidateNewNode(LinkedListNode<T> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node.list != null)
			{
				throw new InvalidOperationException("The LinkedList node already belongs to a LinkedList.");
			}
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00055D27 File Offset: 0x00053F27
		internal void ValidateNode(LinkedListNode<T> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node.list != this)
			{
				throw new InvalidOperationException("The LinkedList node does not belong to current LinkedList.");
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.LinkedList`1" />, this property always returns false.</returns>
		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x000028AE File Offset: 0x00000AAE
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.LinkedList`1" />, this property always returns the current instance.</returns>
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001377 RID: 4983 RVA: 0x00055D4B File Offset: 0x00053F4B
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06001378 RID: 4984 RVA: 0x00055D70 File Offset: 0x00053F70
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
				throw new ArgumentException("Insufficient space in the target location to copy the information.");
			}
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			object[] array3 = array as object[];
			if (array3 == null)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
			LinkedListNode<T> next = this.head;
			try
			{
				if (next != null)
				{
					do
					{
						array3[index++] = next.item;
						next = next.next;
					}
					while (next != this.head);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
		}

		/// <summary>Returns an enumerator that iterates through the linked list as a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the linked list as a collection.</returns>
		// Token: 0x06001379 RID: 4985 RVA: 0x00055A70 File Offset: 0x00053C70
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000BA2 RID: 2978
		internal LinkedListNode<T> head;

		// Token: 0x04000BA3 RID: 2979
		internal int count;

		// Token: 0x04000BA4 RID: 2980
		internal int version;

		// Token: 0x04000BA5 RID: 2981
		private object _syncRoot;

		// Token: 0x04000BA6 RID: 2982
		private SerializationInfo _siInfo;

		// Token: 0x04000BA7 RID: 2983
		private const string VersionName = "Version";

		// Token: 0x04000BA8 RID: 2984
		private const string CountName = "Count";

		// Token: 0x04000BA9 RID: 2985
		private const string ValuesName = "Data";

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		// Token: 0x02000319 RID: 793
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator, ISerializable, IDeserializationCallback
		{
			// Token: 0x0600137A RID: 4986 RVA: 0x00055E70 File Offset: 0x00054070
			internal Enumerator(LinkedList<T> list)
			{
				this._list = list;
				this._version = list.version;
				this._node = list.head;
				this._current = default(T);
				this._index = 0;
			}

			// Token: 0x0600137B RID: 4987 RVA: 0x0000D54C File Offset: 0x0000B74C
			private Enumerator(SerializationInfo info, StreamingContext context)
			{
				throw new PlatformNotSupportedException();
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the <see cref="T:System.Collections.Generic.LinkedList`1" /> at the current position of the enumerator.</returns>
			// Token: 0x17000425 RID: 1061
			// (get) Token: 0x0600137C RID: 4988 RVA: 0x00055EA4 File Offset: 0x000540A4
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000426 RID: 1062
			// (get) Token: 0x0600137D RID: 4989 RVA: 0x00055EAC File Offset: 0x000540AC
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._list.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._current;
				}
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x0600137E RID: 4990 RVA: 0x00055EE4 File Offset: 0x000540E4
			public bool MoveNext()
			{
				if (this._version != this._list.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._node == null)
				{
					this._index = this._list.Count + 1;
					return false;
				}
				this._index++;
				this._current = this._node.item;
				this._node = this._node.next;
				if (this._node == this._list.head)
				{
					this._node = null;
				}
				return true;
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection. This class cannot be inherited.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x0600137F RID: 4991 RVA: 0x00055F78 File Offset: 0x00054178
			void IEnumerator.Reset()
			{
				if (this._version != this._list.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._current = default(T);
				this._node = this._list.head;
				this._index = 0;
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.LinkedList`1.Enumerator" />.</summary>
			// Token: 0x06001380 RID: 4992 RVA: 0x00002FA0 File Offset: 0x000011A0
			public void Dispose()
			{
			}

			/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Generic.LinkedList`1" /> instance.</summary>
			/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.LinkedList`1" /> instance.</param>
			/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.LinkedList`1" /> instance.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="info" /> is null.</exception>
			// Token: 0x06001381 RID: 4993 RVA: 0x0000D54C File Offset: 0x0000B74C
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new PlatformNotSupportedException();
			}

			/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
			/// <param name="sender">The source of the deserialization event.</param>
			/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.LinkedList`1" /> instance is invalid.</exception>
			// Token: 0x06001382 RID: 4994 RVA: 0x0000D54C File Offset: 0x0000B74C
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x04000BAA RID: 2986
			private LinkedList<T> _list;

			// Token: 0x04000BAB RID: 2987
			private LinkedListNode<T> _node;

			// Token: 0x04000BAC RID: 2988
			private int _version;

			// Token: 0x04000BAD RID: 2989
			private T _current;

			// Token: 0x04000BAE RID: 2990
			private int _index;
		}
	}
}
