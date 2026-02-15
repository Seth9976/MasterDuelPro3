using System;
using System.Collections.Generic;

namespace UnityEngine.ResourceManagement.Util
{
	// Token: 0x0200003A RID: 58
	internal static class GlobalLinkedListNodeCache<T>
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000066C0 File Offset: 0x000048C0
		public static bool CacheExists
		{
			get
			{
				return GlobalLinkedListNodeCache<T>.m_globalCache != null;
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000066CA File Offset: 0x000048CA
		public static void SetCacheSize(int length)
		{
			if (GlobalLinkedListNodeCache<T>.m_globalCache == null)
			{
				GlobalLinkedListNodeCache<T>.m_globalCache = new LinkedListNodeCache<T>();
			}
			GlobalLinkedListNodeCache<T>.m_globalCache.CachedNodeCount = length;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000066E8 File Offset: 0x000048E8
		public static LinkedListNode<T> Acquire(T val)
		{
			if (GlobalLinkedListNodeCache<T>.m_globalCache == null)
			{
				GlobalLinkedListNodeCache<T>.m_globalCache = new LinkedListNodeCache<T>();
			}
			return GlobalLinkedListNodeCache<T>.m_globalCache.Acquire(val);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006706 File Offset: 0x00004906
		public static void Release(LinkedListNode<T> node)
		{
			if (GlobalLinkedListNodeCache<T>.m_globalCache == null)
			{
				GlobalLinkedListNodeCache<T>.m_globalCache = new LinkedListNodeCache<T>();
			}
			GlobalLinkedListNodeCache<T>.m_globalCache.Release(node);
		}

		// Token: 0x0400008D RID: 141
		private static LinkedListNodeCache<T> m_globalCache;
	}
}
