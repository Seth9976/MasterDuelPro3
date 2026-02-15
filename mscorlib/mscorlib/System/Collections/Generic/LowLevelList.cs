using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000768 RID: 1896
	[DebuggerDisplay("Count = {Count}")]
	internal class LowLevelList<T>
	{
		// Token: 0x06003C4B RID: 15435 RVA: 0x000E8C9F File Offset: 0x000E6E9F
		public LowLevelList()
		{
			this._items = LowLevelList<T>.s_emptyArray;
		}

		// Token: 0x06003C4C RID: 15436 RVA: 0x000E8CB2 File Offset: 0x000E6EB2
		public LowLevelList(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			if (capacity == 0)
			{
				this._items = LowLevelList<T>.s_emptyArray;
				return;
			}
			this._items = new T[capacity];
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06003C4D RID: 15437 RVA: 0x000E8CE4 File Offset: 0x000E6EE4
		// (set) Token: 0x06003C4E RID: 15438 RVA: 0x000E8CF0 File Offset: 0x000E6EF0
		public int Capacity
		{
			get
			{
				return this._items.Length;
			}
			set
			{
				if (value < this._size)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value != this._items.Length)
				{
					if (value > 0)
					{
						T[] array = new T[value];
						Array.Copy(this._items, 0, array, 0, this._size);
						this._items = array;
						return;
					}
					this._items = LowLevelList<T>.s_emptyArray;
				}
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06003C4F RID: 15439 RVA: 0x000E8D4E File Offset: 0x000E6F4E
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x170009D6 RID: 2518
		public T this[int index]
		{
			get
			{
				if (index >= this._size)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this._items[index];
			}
			set
			{
				if (index >= this._size)
				{
					throw new ArgumentOutOfRangeException();
				}
				this._items[index] = value;
				this._version++;
			}
		}

		// Token: 0x06003C52 RID: 15442 RVA: 0x000E8DA0 File Offset: 0x000E6FA0
		public void Add(T item)
		{
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			T[] items = this._items;
			int size = this._size;
			this._size = size + 1;
			items[size] = item;
			this._version++;
		}

		// Token: 0x06003C53 RID: 15443 RVA: 0x000E8DF8 File Offset: 0x000E6FF8
		private void EnsureCapacity(int min)
		{
			if (this._items.Length < min)
			{
				int num = ((this._items.Length == 0) ? 4 : (this._items.Length * 2));
				if (num < min)
				{
					num = min;
				}
				this.Capacity = num;
			}
		}

		// Token: 0x06003C54 RID: 15444 RVA: 0x000E8E34 File Offset: 0x000E7034
		public void AddRange(IEnumerable<T> collection)
		{
			this.InsertRange(this._size, collection);
		}

		// Token: 0x06003C55 RID: 15445 RVA: 0x000E8E43 File Offset: 0x000E7043
		public void Clear()
		{
			if (this._size > 0)
			{
				Array.Clear(this._items, 0, this._size);
				this._size = 0;
			}
			this._version++;
		}

		// Token: 0x06003C56 RID: 15446 RVA: 0x000E8E78 File Offset: 0x000E7078
		public bool Contains(T item)
		{
			if (item == null)
			{
				for (int i = 0; i < this._size; i++)
				{
					if (this._items[i] == null)
					{
						return true;
					}
				}
				return false;
			}
			return this.IndexOf(item) >= 0;
		}

		// Token: 0x06003C57 RID: 15447 RVA: 0x000E8EC2 File Offset: 0x000E70C2
		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(this._items, 0, array, arrayIndex, this._size);
		}

		// Token: 0x06003C58 RID: 15448 RVA: 0x000E8ED8 File Offset: 0x000E70D8
		public int IndexOf(T item)
		{
			return Array.IndexOf<T>(this._items, item, 0, this._size);
		}

		// Token: 0x06003C59 RID: 15449 RVA: 0x000E8EF0 File Offset: 0x000E70F0
		public void Insert(int index, T item)
		{
			if (index > this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this._items, index, this._items, index + 1, this._size - index);
			}
			this._items[index] = item;
			this._size++;
			this._version++;
		}

		// Token: 0x06003C5A RID: 15450 RVA: 0x000E8F80 File Offset: 0x000E7180
		public void InsertRange(int index, IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (index > this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			ICollection<T> collection2 = collection as ICollection<T>;
			if (collection2 != null)
			{
				int count = collection2.Count;
				if (count > 0)
				{
					this.EnsureCapacity(this._size + count);
					if (index < this._size)
					{
						Array.Copy(this._items, index, this._items, index + count, this._size - index);
					}
					if (this == collection2)
					{
						Array.Copy(this._items, 0, this._items, index, index);
						Array.Copy(this._items, index + count, this._items, index * 2, this._size - index);
					}
					else
					{
						T[] array = new T[count];
						collection2.CopyTo(array, 0);
						Array.Copy(array, 0, this._items, index, count);
					}
					this._size += count;
				}
			}
			else
			{
				foreach (T t in collection)
				{
					this.Insert(index++, t);
				}
			}
			this._version++;
		}

		// Token: 0x06003C5B RID: 15451 RVA: 0x000E90B4 File Offset: 0x000E72B4
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x06003C5C RID: 15452 RVA: 0x000E90D8 File Offset: 0x000E72D8
		public int RemoveAll(Predicate<T> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			int num = 0;
			while (num < this._size && !match(this._items[num]))
			{
				num++;
			}
			if (num >= this._size)
			{
				return 0;
			}
			int i = num + 1;
			while (i < this._size)
			{
				while (i < this._size && match(this._items[i]))
				{
					i++;
				}
				if (i < this._size)
				{
					this._items[num++] = this._items[i++];
				}
			}
			Array.Clear(this._items, num, this._size - num);
			int num2 = this._size - num;
			this._size = num;
			this._version++;
			return num2;
		}

		// Token: 0x06003C5D RID: 15453 RVA: 0x000E91B0 File Offset: 0x000E73B0
		public void RemoveAt(int index)
		{
			if (index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this._items, index + 1, this._items, index, this._size - index);
			}
			this._items[this._size] = default(T);
			this._version++;
		}

		// Token: 0x04001F49 RID: 8009
		protected T[] _items;

		// Token: 0x04001F4A RID: 8010
		protected int _size;

		// Token: 0x04001F4B RID: 8011
		protected int _version;

		// Token: 0x04001F4C RID: 8012
		private static readonly T[] s_emptyArray = new T[0];
	}
}
