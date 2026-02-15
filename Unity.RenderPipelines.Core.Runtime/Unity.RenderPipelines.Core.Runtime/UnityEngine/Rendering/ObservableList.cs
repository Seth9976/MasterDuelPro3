using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x0200005F RID: 95
	public class ObservableList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060004C4 RID: 1220 RVA: 0x00008F98 File Offset: 0x00007198
		// (remove) Token: 0x060004C5 RID: 1221 RVA: 0x00008FD0 File Offset: 0x000071D0
		public event ListChangedEventHandler<T> ItemAdded;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060004C6 RID: 1222 RVA: 0x00009008 File Offset: 0x00007208
		// (remove) Token: 0x060004C7 RID: 1223 RVA: 0x00009040 File Offset: 0x00007240
		public event ListChangedEventHandler<T> ItemRemoved;

		// Token: 0x1700003F RID: 63
		public T this[int index]
		{
			get
			{
				return this.m_List[index];
			}
			set
			{
				this.OnEvent(this.ItemRemoved, index, this.m_List[index]);
				this.m_List[index] = value;
				this.OnEvent(this.ItemAdded, index, value);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x000090B9 File Offset: 0x000072B9
		public int Count
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x000090C6 File Offset: 0x000072C6
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x000090C9 File Offset: 0x000072C9
		public ObservableList()
			: this(0)
		{
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000090D2 File Offset: 0x000072D2
		public ObservableList(int capacity)
		{
			this.m_List = new List<T>(capacity);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000090E6 File Offset: 0x000072E6
		public ObservableList(IEnumerable<T> collection)
		{
			this.m_List = new List<T>(collection);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x000090FA File Offset: 0x000072FA
		private void OnEvent(ListChangedEventHandler<T> e, int index, T item)
		{
			if (e != null)
			{
				e(this, new ListChangedEventArgs<T>(index, item));
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000910D File Offset: 0x0000730D
		public bool Contains(T item)
		{
			return this.m_List.Contains(item);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000911B File Offset: 0x0000731B
		public int IndexOf(T item)
		{
			return this.m_List.IndexOf(item);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00009129 File Offset: 0x00007329
		public void Add(T item)
		{
			this.m_List.Add(item);
			this.OnEvent(this.ItemAdded, this.m_List.IndexOf(item), item);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00009150 File Offset: 0x00007350
		public void Add(params T[] items)
		{
			foreach (T i in items)
			{
				this.Add(i);
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000917C File Offset: 0x0000737C
		public void Insert(int index, T item)
		{
			this.m_List.Insert(index, item);
			this.OnEvent(this.ItemAdded, index, item);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000919C File Offset: 0x0000739C
		public bool Remove(T item)
		{
			int index = this.m_List.IndexOf(item);
			bool flag = this.m_List.Remove(item);
			if (flag)
			{
				this.OnEvent(this.ItemRemoved, index, item);
			}
			return flag;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x000091D4 File Offset: 0x000073D4
		public int Remove(params T[] items)
		{
			if (items == null)
			{
				return 0;
			}
			int count = 0;
			foreach (T i in items)
			{
				count += (this.Remove(i) ? 1 : 0);
			}
			return count;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00009214 File Offset: 0x00007414
		public void RemoveAt(int index)
		{
			T item = this.m_List[index];
			this.m_List.RemoveAt(index);
			this.OnEvent(this.ItemRemoved, index, item);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00009248 File Offset: 0x00007448
		public void Clear()
		{
			while (this.Count > 0)
			{
				this.RemoveAt(this.Count - 1);
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00009263 File Offset: 0x00007463
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.m_List.CopyTo(array, arrayIndex);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00009272 File Offset: 0x00007472
		public IEnumerator<T> GetEnumerator()
		{
			return this.m_List.GetEnumerator();
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000927F File Offset: 0x0000747F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000139 RID: 313
		private IList<T> m_List;
	}
}
