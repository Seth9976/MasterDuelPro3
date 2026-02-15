using System;
using System.Threading;

namespace System.Collections.Specialized
{
	/// <summary>Implements IDictionary using a singly linked list. Recommended for collections that typically include fewer than 10 items.</summary>
	// Token: 0x020002FC RID: 764
	[Serializable]
	public class ListDictionary : IDictionary, ICollection, IEnumerable
	{
		/// <summary>Creates an empty <see cref="T:System.Collections.Specialized.ListDictionary" /> using the default comparer.</summary>
		// Token: 0x06001276 RID: 4726 RVA: 0x000026E5 File Offset: 0x000008E5
		public ListDictionary()
		{
		}

		/// <summary>Creates an empty <see cref="T:System.Collections.Specialized.ListDictionary" /> using the specified comparer.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> to use to determine whether two keys are equal.-or- null to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />. </param>
		// Token: 0x06001277 RID: 4727 RVA: 0x00053545 File Offset: 0x00051745
		public ListDictionary(IComparer comparer)
		{
			this.comparer = comparer;
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns null, and attempting to set it creates a new entry using the specified key.</returns>
		/// <param name="key">The key whose value to get or set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x170003DA RID: 986
		public object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				ListDictionary.DictionaryNode dictionaryNode = this.head;
				if (this.comparer == null)
				{
					while (dictionaryNode != null)
					{
						if (dictionaryNode.key.Equals(key))
						{
							return dictionaryNode.value;
						}
						dictionaryNode = dictionaryNode.next;
					}
				}
				else
				{
					while (dictionaryNode != null)
					{
						object key2 = dictionaryNode.key;
						if (this.comparer.Compare(key2, key) == 0)
						{
							return dictionaryNode.value;
						}
						dictionaryNode = dictionaryNode.next;
					}
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this.version++;
				ListDictionary.DictionaryNode dictionaryNode = null;
				ListDictionary.DictionaryNode next;
				for (next = this.head; next != null; next = next.next)
				{
					object key2 = next.key;
					if ((this.comparer == null) ? key2.Equals(key) : (this.comparer.Compare(key2, key) == 0))
					{
						break;
					}
					dictionaryNode = next;
				}
				if (next != null)
				{
					next.value = value;
					return;
				}
				ListDictionary.DictionaryNode dictionaryNode2 = new ListDictionary.DictionaryNode();
				dictionaryNode2.key = key;
				dictionaryNode2.value = value;
				if (dictionaryNode != null)
				{
					dictionaryNode.next = dictionaryNode2;
				}
				else
				{
					this.head = dictionaryNode2;
				}
				this.count++;
			}
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x00053676 File Offset: 0x00051876
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x0005367E File Offset: 0x0005187E
		public ICollection Keys
		{
			get
			{
				return new ListDictionary.NodeKeyValueCollection(this, true);
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.ListDictionary" /> is read-only.</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x000028AE File Offset: 0x00000AAE
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.ListDictionary" /> has a fixed size.</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x000028AE File Offset: 0x00000AAE
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.ListDictionary" /> is synchronized (thread safe).</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x000028AE File Offset: 0x00000AAE
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x00053687 File Offset: 0x00051887
		public object SyncRoot
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

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x000536A9 File Offset: 0x000518A9
		public ICollection Values
		{
			get
			{
				return new ListDictionary.NodeKeyValueCollection(this, false);
			}
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <param name="key">The key of the entry to add. </param>
		/// <param name="value">The value of the entry to add. The value can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An entry with the same key already exists in the <see cref="T:System.Collections.Specialized.ListDictionary" />. </exception>
		// Token: 0x06001281 RID: 4737 RVA: 0x000536B4 File Offset: 0x000518B4
		public void Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.version++;
			ListDictionary.DictionaryNode dictionaryNode = null;
			for (ListDictionary.DictionaryNode next = this.head; next != null; next = next.next)
			{
				object key2 = next.key;
				if ((this.comparer == null) ? key2.Equals(key) : (this.comparer.Compare(key2, key) == 0))
				{
					throw new ArgumentException(SR.Format("An item with the same key has already been added. Key: {0}", key));
				}
				dictionaryNode = next;
			}
			ListDictionary.DictionaryNode dictionaryNode2 = new ListDictionary.DictionaryNode();
			dictionaryNode2.key = key;
			dictionaryNode2.value = value;
			if (dictionaryNode != null)
			{
				dictionaryNode.next = dictionaryNode2;
			}
			else
			{
				this.head = dictionaryNode2;
			}
			this.count++;
		}

		/// <summary>Removes all entries from the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		// Token: 0x06001282 RID: 4738 RVA: 0x00053764 File Offset: 0x00051964
		public void Clear()
		{
			this.count = 0;
			this.head = null;
			this.version++;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Specialized.ListDictionary" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.ListDictionary" /> contains an entry with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Specialized.ListDictionary" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x06001283 RID: 4739 RVA: 0x00053784 File Offset: 0x00051984
		public bool Contains(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			for (ListDictionary.DictionaryNode next = this.head; next != null; next = next.next)
			{
				object key2 = next.key;
				if ((this.comparer == null) ? key2.Equals(key) : (this.comparer.Compare(key2, key) == 0))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Specialized.ListDictionary" /> entries to a one-dimensional <see cref="T:System.Array" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.Specialized.ListDictionary" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Specialized.ListDictionary" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.ListDictionary" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x06001284 RID: 4740 RVA: 0x000537E0 File Offset: 0x000519E0
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
			}
			if (array.Length - index < this.count)
			{
				throw new ArgumentException("Insufficient space in the target location to copy the information.");
			}
			for (ListDictionary.DictionaryNode next = this.head; next != null; next = next.next)
			{
				array.SetValue(new DictionaryEntry(next.key, next.value), index);
				index++;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x06001285 RID: 4741 RVA: 0x00053865 File Offset: 0x00051A65
		public IDictionaryEnumerator GetEnumerator()
		{
			return new ListDictionary.NodeEnumerator(this);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x06001286 RID: 4742 RVA: 0x00053865 File Offset: 0x00051A65
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ListDictionary.NodeEnumerator(this);
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <param name="key">The key of the entry to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x06001287 RID: 4743 RVA: 0x00053870 File Offset: 0x00051A70
		public void Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.version++;
			ListDictionary.DictionaryNode dictionaryNode = null;
			ListDictionary.DictionaryNode next;
			for (next = this.head; next != null; next = next.next)
			{
				object key2 = next.key;
				if ((this.comparer == null) ? key2.Equals(key) : (this.comparer.Compare(key2, key) == 0))
				{
					break;
				}
				dictionaryNode = next;
			}
			if (next == null)
			{
				return;
			}
			if (next == this.head)
			{
				this.head = next.next;
			}
			else
			{
				dictionaryNode.next = next.next;
			}
			this.count--;
		}

		// Token: 0x04000B53 RID: 2899
		private ListDictionary.DictionaryNode head;

		// Token: 0x04000B54 RID: 2900
		private int version;

		// Token: 0x04000B55 RID: 2901
		private int count;

		// Token: 0x04000B56 RID: 2902
		private readonly IComparer comparer;

		// Token: 0x04000B57 RID: 2903
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x020002FD RID: 765
		private class NodeEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06001288 RID: 4744 RVA: 0x0005390F File Offset: 0x00051B0F
			public NodeEnumerator(ListDictionary list)
			{
				this._list = list;
				this._version = list.version;
				this._start = true;
				this._current = null;
			}

			// Token: 0x170003E2 RID: 994
			// (get) Token: 0x06001289 RID: 4745 RVA: 0x00053938 File Offset: 0x00051B38
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x170003E3 RID: 995
			// (get) Token: 0x0600128A RID: 4746 RVA: 0x00053945 File Offset: 0x00051B45
			public DictionaryEntry Entry
			{
				get
				{
					if (this._current == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return new DictionaryEntry(this._current.key, this._current.value);
				}
			}

			// Token: 0x170003E4 RID: 996
			// (get) Token: 0x0600128B RID: 4747 RVA: 0x00053975 File Offset: 0x00051B75
			public object Key
			{
				get
				{
					if (this._current == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._current.key;
				}
			}

			// Token: 0x170003E5 RID: 997
			// (get) Token: 0x0600128C RID: 4748 RVA: 0x00053995 File Offset: 0x00051B95
			public object Value
			{
				get
				{
					if (this._current == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._current.value;
				}
			}

			// Token: 0x0600128D RID: 4749 RVA: 0x000539B8 File Offset: 0x00051BB8
			public bool MoveNext()
			{
				if (this._version != this._list.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._start)
				{
					this._current = this._list.head;
					this._start = false;
				}
				else if (this._current != null)
				{
					this._current = this._current.next;
				}
				return this._current != null;
			}

			// Token: 0x0600128E RID: 4750 RVA: 0x00053A27 File Offset: 0x00051C27
			public void Reset()
			{
				if (this._version != this._list.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._start = true;
				this._current = null;
			}

			// Token: 0x04000B58 RID: 2904
			private ListDictionary _list;

			// Token: 0x04000B59 RID: 2905
			private ListDictionary.DictionaryNode _current;

			// Token: 0x04000B5A RID: 2906
			private int _version;

			// Token: 0x04000B5B RID: 2907
			private bool _start;
		}

		// Token: 0x020002FE RID: 766
		private class NodeKeyValueCollection : ICollection, IEnumerable
		{
			// Token: 0x0600128F RID: 4751 RVA: 0x00053A55 File Offset: 0x00051C55
			public NodeKeyValueCollection(ListDictionary list, bool isKeys)
			{
				this._list = list;
				this._isKeys = isKeys;
			}

			// Token: 0x06001290 RID: 4752 RVA: 0x00053A6C File Offset: 0x00051C6C
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
				}
				for (ListDictionary.DictionaryNode dictionaryNode = this._list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
				{
					array.SetValue(this._isKeys ? dictionaryNode.key : dictionaryNode.value, index);
					index++;
				}
			}

			// Token: 0x170003E6 RID: 998
			// (get) Token: 0x06001291 RID: 4753 RVA: 0x00053ADC File Offset: 0x00051CDC
			int ICollection.Count
			{
				get
				{
					int num = 0;
					for (ListDictionary.DictionaryNode dictionaryNode = this._list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
					{
						num++;
					}
					return num;
				}
			}

			// Token: 0x170003E7 RID: 999
			// (get) Token: 0x06001292 RID: 4754 RVA: 0x000028AE File Offset: 0x00000AAE
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170003E8 RID: 1000
			// (get) Token: 0x06001293 RID: 4755 RVA: 0x00053B08 File Offset: 0x00051D08
			object ICollection.SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06001294 RID: 4756 RVA: 0x00053B15 File Offset: 0x00051D15
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new ListDictionary.NodeKeyValueCollection.NodeKeyValueEnumerator(this._list, this._isKeys);
			}

			// Token: 0x04000B5C RID: 2908
			private ListDictionary _list;

			// Token: 0x04000B5D RID: 2909
			private bool _isKeys;

			// Token: 0x020002FF RID: 767
			private class NodeKeyValueEnumerator : IEnumerator
			{
				// Token: 0x06001295 RID: 4757 RVA: 0x00053B28 File Offset: 0x00051D28
				public NodeKeyValueEnumerator(ListDictionary list, bool isKeys)
				{
					this._list = list;
					this._isKeys = isKeys;
					this._version = list.version;
					this._start = true;
					this._current = null;
				}

				// Token: 0x170003E9 RID: 1001
				// (get) Token: 0x06001296 RID: 4758 RVA: 0x00053B58 File Offset: 0x00051D58
				public object Current
				{
					get
					{
						if (this._current == null)
						{
							throw new InvalidOperationException("Enumeration has either not started or has already finished.");
						}
						if (!this._isKeys)
						{
							return this._current.value;
						}
						return this._current.key;
					}
				}

				// Token: 0x06001297 RID: 4759 RVA: 0x00053B8C File Offset: 0x00051D8C
				public bool MoveNext()
				{
					if (this._version != this._list.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					if (this._start)
					{
						this._current = this._list.head;
						this._start = false;
					}
					else if (this._current != null)
					{
						this._current = this._current.next;
					}
					return this._current != null;
				}

				// Token: 0x06001298 RID: 4760 RVA: 0x00053BFB File Offset: 0x00051DFB
				public void Reset()
				{
					if (this._version != this._list.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					this._start = true;
					this._current = null;
				}

				// Token: 0x04000B5E RID: 2910
				private ListDictionary _list;

				// Token: 0x04000B5F RID: 2911
				private ListDictionary.DictionaryNode _current;

				// Token: 0x04000B60 RID: 2912
				private int _version;

				// Token: 0x04000B61 RID: 2913
				private bool _isKeys;

				// Token: 0x04000B62 RID: 2914
				private bool _start;
			}
		}

		// Token: 0x02000300 RID: 768
		[Serializable]
		public class DictionaryNode
		{
			// Token: 0x04000B63 RID: 2915
			public object key;

			// Token: 0x04000B64 RID: 2916
			public object value;

			// Token: 0x04000B65 RID: 2917
			public ListDictionary.DictionaryNode next;
		}
	}
}
