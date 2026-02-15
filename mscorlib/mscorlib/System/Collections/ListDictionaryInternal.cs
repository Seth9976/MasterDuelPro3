using System;
using System.Threading;

namespace System.Collections
{
	// Token: 0x02000700 RID: 1792
	[Serializable]
	internal class ListDictionaryInternal : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x170008AB RID: 2219
		public object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", "Key cannot be null.");
				}
				for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
				{
					if (next.key.Equals(key))
					{
						return next.value;
					}
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", "Key cannot be null.");
				}
				this.version++;
				ListDictionaryInternal.DictionaryNode dictionaryNode = null;
				ListDictionaryInternal.DictionaryNode next = this.head;
				while (next != null && !next.key.Equals(key))
				{
					dictionaryNode = next;
					next = next.next;
				}
				if (next != null)
				{
					next.value = value;
					return;
				}
				ListDictionaryInternal.DictionaryNode dictionaryNode2 = new ListDictionaryInternal.DictionaryNode();
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

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06003813 RID: 14355 RVA: 0x000DBA9F File Offset: 0x000D9C9F
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06003814 RID: 14356 RVA: 0x000DBAA7 File Offset: 0x000D9CA7
		public ICollection Keys
		{
			get
			{
				return new ListDictionaryInternal.NodeKeyValueCollection(this, true);
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06003815 RID: 14357 RVA: 0x00033991 File Offset: 0x00031B91
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06003816 RID: 14358 RVA: 0x00033991 File Offset: 0x00031B91
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06003817 RID: 14359 RVA: 0x00033991 File Offset: 0x00031B91
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06003818 RID: 14360 RVA: 0x000DBAB0 File Offset: 0x000D9CB0
		public object SyncRoot
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

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06003819 RID: 14361 RVA: 0x000DBAD2 File Offset: 0x000D9CD2
		public ICollection Values
		{
			get
			{
				return new ListDictionaryInternal.NodeKeyValueCollection(this, false);
			}
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x000DBADC File Offset: 0x000D9CDC
		public void Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			this.version++;
			ListDictionaryInternal.DictionaryNode dictionaryNode = null;
			ListDictionaryInternal.DictionaryNode next;
			for (next = this.head; next != null; next = next.next)
			{
				if (next.key.Equals(key))
				{
					throw new ArgumentException(SR.Format("Item has already been added. Key in dictionary: '{0}'  Key being added: '{1}'", next.key, key));
				}
				dictionaryNode = next;
			}
			if (next != null)
			{
				next.value = value;
				return;
			}
			ListDictionaryInternal.DictionaryNode dictionaryNode2 = new ListDictionaryInternal.DictionaryNode();
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

		// Token: 0x0600381B RID: 14363 RVA: 0x000DBB86 File Offset: 0x000D9D86
		public void Clear()
		{
			this.count = 0;
			this.head = null;
			this.version++;
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x000DBBA4 File Offset: 0x000D9DA4
		public bool Contains(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
			{
				if (next.key.Equals(key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x000DBBE8 File Offset: 0x000D9DE8
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException("Index was out of range. Must be non-negative and less than the size of the collection.", "index");
			}
			for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
			{
				array.SetValue(new DictionaryEntry(next.key, next.value), index);
				index++;
			}
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x000DBC80 File Offset: 0x000D9E80
		public IDictionaryEnumerator GetEnumerator()
		{
			return new ListDictionaryInternal.NodeEnumerator(this);
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x000DBC80 File Offset: 0x000D9E80
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ListDictionaryInternal.NodeEnumerator(this);
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x000DBC88 File Offset: 0x000D9E88
		public void Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			this.version++;
			ListDictionaryInternal.DictionaryNode dictionaryNode = null;
			ListDictionaryInternal.DictionaryNode next = this.head;
			while (next != null && !next.key.Equals(key))
			{
				dictionaryNode = next;
				next = next.next;
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

		// Token: 0x04001E46 RID: 7750
		private ListDictionaryInternal.DictionaryNode head;

		// Token: 0x04001E47 RID: 7751
		private int version;

		// Token: 0x04001E48 RID: 7752
		private int count;

		// Token: 0x04001E49 RID: 7753
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x02000701 RID: 1793
		private class NodeEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06003821 RID: 14369 RVA: 0x000DBD10 File Offset: 0x000D9F10
			public NodeEnumerator(ListDictionaryInternal list)
			{
				this.list = list;
				this.version = list.version;
				this.start = true;
				this.current = null;
			}

			// Token: 0x170008B3 RID: 2227
			// (get) Token: 0x06003822 RID: 14370 RVA: 0x000DBD39 File Offset: 0x000D9F39
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x170008B4 RID: 2228
			// (get) Token: 0x06003823 RID: 14371 RVA: 0x000DBD46 File Offset: 0x000D9F46
			public DictionaryEntry Entry
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return new DictionaryEntry(this.current.key, this.current.value);
				}
			}

			// Token: 0x170008B5 RID: 2229
			// (get) Token: 0x06003824 RID: 14372 RVA: 0x000DBD76 File Offset: 0x000D9F76
			public object Key
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this.current.key;
				}
			}

			// Token: 0x170008B6 RID: 2230
			// (get) Token: 0x06003825 RID: 14373 RVA: 0x000DBD96 File Offset: 0x000D9F96
			public object Value
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this.current.value;
				}
			}

			// Token: 0x06003826 RID: 14374 RVA: 0x000DBDB8 File Offset: 0x000D9FB8
			public bool MoveNext()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this.start)
				{
					this.current = this.list.head;
					this.start = false;
				}
				else if (this.current != null)
				{
					this.current = this.current.next;
				}
				return this.current != null;
			}

			// Token: 0x06003827 RID: 14375 RVA: 0x000DBE27 File Offset: 0x000DA027
			public void Reset()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this.start = true;
				this.current = null;
			}

			// Token: 0x04001E4A RID: 7754
			private ListDictionaryInternal list;

			// Token: 0x04001E4B RID: 7755
			private ListDictionaryInternal.DictionaryNode current;

			// Token: 0x04001E4C RID: 7756
			private int version;

			// Token: 0x04001E4D RID: 7757
			private bool start;
		}

		// Token: 0x02000702 RID: 1794
		private class NodeKeyValueCollection : ICollection, IEnumerable
		{
			// Token: 0x06003828 RID: 14376 RVA: 0x000DBE55 File Offset: 0x000DA055
			public NodeKeyValueCollection(ListDictionaryInternal list, bool isKeys)
			{
				this.list = list;
				this.isKeys = isKeys;
			}

			// Token: 0x06003829 RID: 14377 RVA: 0x000DBE6C File Offset: 0x000DA06C
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
				}
				if (array.Length - index < this.list.Count)
				{
					throw new ArgumentException("Index was out of range. Must be non-negative and less than the size of the collection.", "index");
				}
				for (ListDictionaryInternal.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
				{
					array.SetValue(this.isKeys ? dictionaryNode.key : dictionaryNode.value, index);
					index++;
				}
			}

			// Token: 0x170008B7 RID: 2231
			// (get) Token: 0x0600382A RID: 14378 RVA: 0x000DBF10 File Offset: 0x000DA110
			int ICollection.Count
			{
				get
				{
					int num = 0;
					for (ListDictionaryInternal.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
					{
						num++;
					}
					return num;
				}
			}

			// Token: 0x170008B8 RID: 2232
			// (get) Token: 0x0600382B RID: 14379 RVA: 0x00033991 File Offset: 0x00031B91
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170008B9 RID: 2233
			// (get) Token: 0x0600382C RID: 14380 RVA: 0x000DBF3C File Offset: 0x000DA13C
			object ICollection.SyncRoot
			{
				get
				{
					return this.list.SyncRoot;
				}
			}

			// Token: 0x0600382D RID: 14381 RVA: 0x000DBF49 File Offset: 0x000DA149
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new ListDictionaryInternal.NodeKeyValueCollection.NodeKeyValueEnumerator(this.list, this.isKeys);
			}

			// Token: 0x04001E4E RID: 7758
			private ListDictionaryInternal list;

			// Token: 0x04001E4F RID: 7759
			private bool isKeys;

			// Token: 0x02000703 RID: 1795
			private class NodeKeyValueEnumerator : IEnumerator
			{
				// Token: 0x0600382E RID: 14382 RVA: 0x000DBF5C File Offset: 0x000DA15C
				public NodeKeyValueEnumerator(ListDictionaryInternal list, bool isKeys)
				{
					this.list = list;
					this.isKeys = isKeys;
					this.version = list.version;
					this.start = true;
					this.current = null;
				}

				// Token: 0x170008BA RID: 2234
				// (get) Token: 0x0600382F RID: 14383 RVA: 0x000DBF8C File Offset: 0x000DA18C
				public object Current
				{
					get
					{
						if (this.current == null)
						{
							throw new InvalidOperationException("Enumeration has either not started or has already finished.");
						}
						if (!this.isKeys)
						{
							return this.current.value;
						}
						return this.current.key;
					}
				}

				// Token: 0x06003830 RID: 14384 RVA: 0x000DBFC0 File Offset: 0x000DA1C0
				public bool MoveNext()
				{
					if (this.version != this.list.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					if (this.start)
					{
						this.current = this.list.head;
						this.start = false;
					}
					else if (this.current != null)
					{
						this.current = this.current.next;
					}
					return this.current != null;
				}

				// Token: 0x06003831 RID: 14385 RVA: 0x000DC02F File Offset: 0x000DA22F
				public void Reset()
				{
					if (this.version != this.list.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					this.start = true;
					this.current = null;
				}

				// Token: 0x04001E50 RID: 7760
				private ListDictionaryInternal list;

				// Token: 0x04001E51 RID: 7761
				private ListDictionaryInternal.DictionaryNode current;

				// Token: 0x04001E52 RID: 7762
				private int version;

				// Token: 0x04001E53 RID: 7763
				private bool isKeys;

				// Token: 0x04001E54 RID: 7764
				private bool start;
			}
		}

		// Token: 0x02000704 RID: 1796
		[Serializable]
		private class DictionaryNode
		{
			// Token: 0x04001E55 RID: 7765
			public object key;

			// Token: 0x04001E56 RID: 7766
			public object value;

			// Token: 0x04001E57 RID: 7767
			public ListDictionaryInternal.DictionaryNode next;
		}
	}
}
