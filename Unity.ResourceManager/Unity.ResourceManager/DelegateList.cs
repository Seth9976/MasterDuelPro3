using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.Util;

// Token: 0x02000004 RID: 4
internal class DelegateList<T>
{
	// Token: 0x06000003 RID: 3 RVA: 0x00002058 File Offset: 0x00000258
	public DelegateList(Func<Action<T>, LinkedListNode<Action<T>>> acquireFunc, Action<LinkedListNode<Action<T>>> releaseFunc)
	{
		if (acquireFunc == null)
		{
			throw new ArgumentNullException("acquireFunc");
		}
		if (releaseFunc == null)
		{
			throw new ArgumentNullException("releaseFunc");
		}
		this.m_acquireFunc = acquireFunc;
		this.m_releaseFunc = releaseFunc;
	}

	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000004 RID: 4 RVA: 0x0000208A File Offset: 0x0000028A
	public int Count
	{
		get
		{
			if (this.m_callbacks != null)
			{
				return this.m_callbacks.Count;
			}
			return 0;
		}
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000020A4 File Offset: 0x000002A4
	public void Add(Action<T> action)
	{
		LinkedListNode<Action<T>> node = this.m_acquireFunc(action);
		if (this.m_callbacks == null)
		{
			this.m_callbacks = new LinkedList<Action<T>>();
		}
		this.m_callbacks.AddLast(node);
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000020E0 File Offset: 0x000002E0
	public void Remove(Action<T> action)
	{
		if (this.m_callbacks == null)
		{
			return;
		}
		LinkedListNode<Action<T>> node = this.m_callbacks.First;
		while (node != null)
		{
			if (node.Value == action)
			{
				if (this.m_invoking)
				{
					node.Value = null;
					return;
				}
				this.m_callbacks.Remove(node);
				this.m_releaseFunc(node);
				return;
			}
			else
			{
				node = node.Next;
			}
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002148 File Offset: 0x00000348
	public void Invoke(T res)
	{
		if (this.m_callbacks == null)
		{
			return;
		}
		this.m_invoking = true;
		for (LinkedListNode<Action<T>> node = this.m_callbacks.First; node != null; node = node.Next)
		{
			if (node.Value != null)
			{
				try
				{
					node.Value(res);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
			}
		}
		this.m_invoking = false;
		LinkedListNode<Action<T>> next;
		for (LinkedListNode<Action<T>> r = this.m_callbacks.First; r != null; r = next)
		{
			next = r.Next;
			if (r.Value == null)
			{
				this.m_callbacks.Remove(r);
				this.m_releaseFunc(r);
			}
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x000021EC File Offset: 0x000003EC
	public void Clear()
	{
		if (this.m_callbacks == null)
		{
			return;
		}
		LinkedListNode<Action<T>> next;
		for (LinkedListNode<Action<T>> node = this.m_callbacks.First; node != null; node = next)
		{
			next = node.Next;
			this.m_callbacks.Remove(node);
			this.m_releaseFunc(node);
		}
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002232 File Offset: 0x00000432
	public static DelegateList<T> CreateWithGlobalCache()
	{
		if (!GlobalLinkedListNodeCache<Action<T>>.CacheExists)
		{
			GlobalLinkedListNodeCache<Action<T>>.SetCacheSize(32);
		}
		return new DelegateList<T>(new Func<Action<T>, LinkedListNode<Action<T>>>(GlobalLinkedListNodeCache<Action<T>>.Acquire), new Action<LinkedListNode<Action<T>>>(GlobalLinkedListNodeCache<Action<T>>.Release));
	}

	// Token: 0x04000001 RID: 1
	private Func<Action<T>, LinkedListNode<Action<T>>> m_acquireFunc;

	// Token: 0x04000002 RID: 2
	private Action<LinkedListNode<Action<T>>> m_releaseFunc;

	// Token: 0x04000003 RID: 3
	private LinkedList<Action<T>> m_callbacks;

	// Token: 0x04000004 RID: 4
	private bool m_invoking;
}
