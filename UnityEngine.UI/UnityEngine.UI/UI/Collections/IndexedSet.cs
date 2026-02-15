using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UI.Collections
{
	// Token: 0x02000089 RID: 137
	internal class IndexedSet<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000544 RID: 1348 RVA: 0x00017411 File Offset: 0x00015611
		public void Add(T item)
		{
			this.Add(item, true);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001741B File Offset: 0x0001561B
		public void Add(T item, bool isActive)
		{
			this.m_List.Add(item);
			this.m_Dictionary.Add(item, this.m_List.Count - 1);
			if (isActive)
			{
				this.EnableItem(item);
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001744D File Offset: 0x0001564D
		public bool AddUnique(T item, bool isActive = true)
		{
			if (this.m_Dictionary.ContainsKey(item))
			{
				if (isActive)
				{
					this.EnableItem(item);
				}
				else
				{
					this.DisableItem(item);
				}
				return false;
			}
			this.Add(item, isActive);
			return true;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00017480 File Offset: 0x00015680
		public bool EnableItem(T item)
		{
			int index;
			if (!this.m_Dictionary.TryGetValue(item, out index))
			{
				return false;
			}
			if (index < this.m_EnabledObjectCount)
			{
				return true;
			}
			if (index > this.m_EnabledObjectCount)
			{
				this.Swap(this.m_EnabledObjectCount, index);
			}
			this.m_EnabledObjectCount++;
			return true;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x000174D0 File Offset: 0x000156D0
		public bool DisableItem(T item)
		{
			int index;
			if (!this.m_Dictionary.TryGetValue(item, out index))
			{
				return false;
			}
			if (index >= this.m_EnabledObjectCount)
			{
				return true;
			}
			if (index < this.m_EnabledObjectCount - 1)
			{
				this.Swap(index, this.m_EnabledObjectCount - 1);
			}
			this.m_EnabledObjectCount--;
			return true;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00017524 File Offset: 0x00015724
		public bool Remove(T item)
		{
			int index = -1;
			if (!this.m_Dictionary.TryGetValue(item, out index))
			{
				return false;
			}
			this.RemoveAt(index);
			return true;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001754D File Offset: 0x0001574D
		public IEnumerator<T> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00017554 File Offset: 0x00015754
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001755C File Offset: 0x0001575C
		public void Clear()
		{
			this.m_List.Clear();
			this.m_Dictionary.Clear();
			this.m_EnabledObjectCount = 0;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001757B File Offset: 0x0001577B
		public bool Contains(T item)
		{
			return this.m_Dictionary.ContainsKey(item);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00017589 File Offset: 0x00015789
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.m_List.CopyTo(array, arrayIndex);
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00017598 File Offset: 0x00015798
		public int Count
		{
			get
			{
				return this.m_EnabledObjectCount;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x000175A0 File Offset: 0x000157A0
		public int Capacity
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x000093DE File Offset: 0x000075DE
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x000175B0 File Offset: 0x000157B0
		public int IndexOf(T item)
		{
			int index = -1;
			if (this.m_Dictionary.TryGetValue(item, out index))
			{
				return index;
			}
			return -1;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000175D2 File Offset: 0x000157D2
		public void Insert(int index, T item)
		{
			throw new NotSupportedException("Random Insertion is semantically invalid, since this structure does not guarantee ordering.");
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000175E0 File Offset: 0x000157E0
		public void RemoveAt(int index)
		{
			T item = this.m_List[index];
			if (index == this.m_List.Count - 1)
			{
				if (this.m_EnabledObjectCount == this.m_List.Count)
				{
					this.m_EnabledObjectCount--;
				}
				this.m_List.RemoveAt(index);
			}
			else
			{
				int replaceItemIndex = this.m_List.Count - 1;
				if (index < this.m_EnabledObjectCount - 1)
				{
					int num = this.m_EnabledObjectCount - 1;
					this.m_EnabledObjectCount = num;
					this.Swap(num, index);
					index = this.m_EnabledObjectCount;
				}
				else if (index == this.m_EnabledObjectCount - 1)
				{
					this.m_EnabledObjectCount--;
				}
				this.Swap(replaceItemIndex, index);
				this.m_List.RemoveAt(replaceItemIndex);
			}
			this.m_Dictionary.Remove(item);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000176B0 File Offset: 0x000158B0
		private void Swap(int index1, int index2)
		{
			if (index1 == index2)
			{
				return;
			}
			T item = this.m_List[index1];
			T item2 = this.m_List[index2];
			this.m_List[index1] = item2;
			this.m_List[index2] = item;
			this.m_Dictionary[item2] = index1;
			this.m_Dictionary[item] = index2;
		}

		// Token: 0x1700015B RID: 347
		public T this[int index]
		{
			get
			{
				if (index >= this.m_EnabledObjectCount)
				{
					throw new IndexOutOfRangeException();
				}
				return this.m_List[index];
			}
			set
			{
				T item = this.m_List[index];
				this.m_Dictionary.Remove(item);
				this.m_List[index] = value;
				this.m_Dictionary.Add(value, index);
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00017774 File Offset: 0x00015974
		public void RemoveAll(Predicate<T> match)
		{
			int i = 0;
			while (i < this.m_List.Count)
			{
				T item = this.m_List[i];
				if (match(item))
				{
					this.Remove(item);
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000177B8 File Offset: 0x000159B8
		public void Sort(Comparison<T> sortLayoutFunction)
		{
			this.m_List.Sort(sortLayoutFunction);
			for (int i = 0; i < this.m_List.Count; i++)
			{
				T item = this.m_List[i];
				this.m_Dictionary[item] = i;
			}
		}

		// Token: 0x04000273 RID: 627
		private readonly List<T> m_List = new List<T>();

		// Token: 0x04000274 RID: 628
		private Dictionary<T, int> m_Dictionary = new Dictionary<T, int>();

		// Token: 0x04000275 RID: 629
		private int m_EnabledObjectCount;
	}
}
