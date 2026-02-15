using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000005 RID: 5
internal class ListWithEvents<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
{
	// Token: 0x14000001 RID: 1
	// (add) Token: 0x0600000A RID: 10 RVA: 0x00002260 File Offset: 0x00000460
	// (remove) Token: 0x0600000B RID: 11 RVA: 0x00002298 File Offset: 0x00000498
	public event Action<T> OnElementAdded;

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x0600000C RID: 12 RVA: 0x000022D0 File Offset: 0x000004D0
	// (remove) Token: 0x0600000D RID: 13 RVA: 0x00002308 File Offset: 0x00000508
	public event Action<T> OnElementRemoved;

	// Token: 0x0600000E RID: 14 RVA: 0x0000233D File Offset: 0x0000053D
	private void InvokeAdded(T element)
	{
		Action<T> onElementAdded = this.OnElementAdded;
		if (onElementAdded == null)
		{
			return;
		}
		onElementAdded(element);
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002350 File Offset: 0x00000550
	private void InvokeRemoved(T element)
	{
		Action<T> onElementRemoved = this.OnElementRemoved;
		if (onElementRemoved == null)
		{
			return;
		}
		onElementRemoved(element);
	}

	// Token: 0x17000002 RID: 2
	public T this[int index]
	{
		get
		{
			return this.m_List[index];
		}
		set
		{
			T oldElement = this.m_List[index];
			this.m_List[index] = value;
			this.InvokeRemoved(oldElement);
			this.InvokeAdded(value);
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000012 RID: 18 RVA: 0x000023A9 File Offset: 0x000005A9
	public int Count
	{
		get
		{
			return this.m_List.Count;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000013 RID: 19 RVA: 0x000023B6 File Offset: 0x000005B6
	public bool IsReadOnly
	{
		get
		{
			return ((ICollection<T>)this.m_List).IsReadOnly;
		}
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000023C3 File Offset: 0x000005C3
	public void Add(T item)
	{
		this.m_List.Add(item);
		this.InvokeAdded(item);
	}

	// Token: 0x06000015 RID: 21 RVA: 0x000023D8 File Offset: 0x000005D8
	public void Clear()
	{
		foreach (T obj in this.m_List)
		{
			this.InvokeRemoved(obj);
		}
		this.m_List.Clear();
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002438 File Offset: 0x00000638
	public bool Contains(T item)
	{
		return this.m_List.Contains(item);
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002446 File Offset: 0x00000646
	public void CopyTo(T[] array, int arrayIndex)
	{
		this.m_List.CopyTo(array, arrayIndex);
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00002455 File Offset: 0x00000655
	public IEnumerator<T> GetEnumerator()
	{
		return this.m_List.GetEnumerator();
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002467 File Offset: 0x00000667
	public int IndexOf(T item)
	{
		return this.m_List.IndexOf(item);
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00002475 File Offset: 0x00000675
	public void Insert(int index, T item)
	{
		this.m_List.Insert(index, item);
		this.InvokeAdded(item);
	}

	// Token: 0x0600001B RID: 27 RVA: 0x0000248B File Offset: 0x0000068B
	public bool Remove(T item)
	{
		bool flag = this.m_List.Remove(item);
		if (flag)
		{
			this.InvokeRemoved(item);
		}
		return flag;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x000024A4 File Offset: 0x000006A4
	public void RemoveAt(int index)
	{
		T item = this.m_List[index];
		this.m_List.RemoveAt(index);
		this.InvokeRemoved(item);
	}

	// Token: 0x0600001D RID: 29 RVA: 0x000024D1 File Offset: 0x000006D1
	IEnumerator IEnumerable.GetEnumerator()
	{
		return ((IEnumerable)this.m_List).GetEnumerator();
	}

	// Token: 0x04000005 RID: 5
	private List<T> m_List = new List<T>();
}
