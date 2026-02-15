using System;
using System.Collections.Generic;

namespace UnityEngine.ResourceManagement.Util
{
	// Token: 0x02000039 RID: 57
	public class LinkedListNodeCache<T>
	{
		// Token: 0x06000142 RID: 322 RVA: 0x000065B4 File Offset: 0x000047B4
		public LinkedListNode<T> Acquire(T val)
		{
			if (this.m_NodeCache != null)
			{
				LinkedListNode<T> i = this.m_NodeCache.First;
				if (i != null)
				{
					this.m_NodeCache.RemoveFirst();
					i.Value = val;
					return i;
				}
			}
			this.m_NodesCreated++;
			return new LinkedListNode<T>(val);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006600 File Offset: 0x00004800
		public void Release(LinkedListNode<T> node)
		{
			if (this.m_NodeCache == null)
			{
				this.m_NodeCache = new LinkedList<T>();
			}
			node.Value = default(T);
			this.m_NodeCache.AddLast(node);
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000663B File Offset: 0x0000483B
		internal int CreatedNodeCount
		{
			get
			{
				return this.m_NodesCreated;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00006643 File Offset: 0x00004843
		// (set) Token: 0x06000146 RID: 326 RVA: 0x0000665C File Offset: 0x0000485C
		internal int CachedNodeCount
		{
			get
			{
				if (this.m_NodeCache != null)
				{
					return this.m_NodeCache.Count;
				}
				return 0;
			}
			set
			{
				if (this.m_NodeCache == null)
				{
					this.m_NodeCache = new LinkedList<T>();
				}
				while (value < this.m_NodeCache.Count)
				{
					this.m_NodeCache.RemoveLast();
				}
				while (value > this.m_NodeCache.Count)
				{
					this.m_NodeCache.AddLast(new LinkedListNode<T>(default(T)));
				}
			}
		}

		// Token: 0x0400008B RID: 139
		private int m_NodesCreated;

		// Token: 0x0400008C RID: 140
		private LinkedList<T> m_NodeCache;
	}
}
