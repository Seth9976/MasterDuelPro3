using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Pool
{
	// Token: 0x020002EC RID: 748
	public class ObjectPool<T> : IDisposable, IObjectPool<T> where T : class
	{
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x0002C275 File Offset: 0x0002A475
		// (set) Token: 0x060014F0 RID: 5360 RVA: 0x0002C27D File Offset: 0x0002A47D
		public int CountAll { get; private set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x0002C288 File Offset: 0x0002A488
		public int CountInactive
		{
			get
			{
				return this.m_List.Count + ((this.m_FreshlyReleased != null) ? 1 : 0);
			}
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0002C2B8 File Offset: 0x0002A4B8
		public ObjectPool(Func<T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnRelease = null, Action<T> actionOnDestroy = null, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000)
		{
			bool flag = createFunc == null;
			if (flag)
			{
				throw new ArgumentNullException("createFunc");
			}
			bool flag2 = maxSize <= 0;
			if (flag2)
			{
				throw new ArgumentException("Max Size must be greater than 0", "maxSize");
			}
			this.m_List = new List<T>(defaultCapacity);
			this.m_CreateFunc = createFunc;
			this.m_MaxSize = maxSize;
			this.m_ActionOnGet = actionOnGet;
			this.m_ActionOnRelease = actionOnRelease;
			this.m_ActionOnDestroy = actionOnDestroy;
			this.m_CollectionCheck = collectionCheck;
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0002C338 File Offset: 0x0002A538
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T Get()
		{
			bool flag = this.m_FreshlyReleased != null;
			T element;
			if (flag)
			{
				element = this.m_FreshlyReleased;
				this.m_FreshlyReleased = default(T);
			}
			else
			{
				bool flag2 = this.m_List.Count == 0;
				if (flag2)
				{
					element = this.m_CreateFunc();
					int countAll = this.CountAll;
					this.CountAll = countAll + 1;
				}
				else
				{
					int idx = this.m_List.Count - 1;
					element = this.m_List[idx];
					this.m_List.RemoveAt(idx);
				}
			}
			Action<T> actionOnGet = this.m_ActionOnGet;
			if (actionOnGet != null)
			{
				actionOnGet(element);
			}
			return element;
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0002C3EC File Offset: 0x0002A5EC
		public PooledObject<T> Get(out T v)
		{
			return new PooledObject<T>(v = this.Get(), this);
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0002C410 File Offset: 0x0002A610
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Release(T element)
		{
			Action<T> actionOnRelease = this.m_ActionOnRelease;
			if (actionOnRelease != null)
			{
				actionOnRelease(element);
			}
			bool flag = this.m_FreshlyReleased == null;
			if (flag)
			{
				this.m_FreshlyReleased = element;
			}
			else
			{
				bool flag2 = this.CountInactive < this.m_MaxSize;
				if (flag2)
				{
					this.m_List.Add(element);
				}
				else
				{
					int countAll = this.CountAll;
					this.CountAll = countAll - 1;
					Action<T> actionOnDestroy = this.m_ActionOnDestroy;
					if (actionOnDestroy != null)
					{
						actionOnDestroy(element);
					}
				}
			}
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x0002C498 File Offset: 0x0002A698
		public void Clear()
		{
			bool flag = this.m_ActionOnDestroy != null;
			if (flag)
			{
				foreach (T item in this.m_List)
				{
					this.m_ActionOnDestroy(item);
				}
				bool flag2 = this.m_FreshlyReleased != null;
				if (flag2)
				{
					this.m_ActionOnDestroy(this.m_FreshlyReleased);
				}
			}
			this.m_FreshlyReleased = default(T);
			this.m_List.Clear();
			this.CountAll = 0;
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0002C54C File Offset: 0x0002A74C
		public void Dispose()
		{
			this.Clear();
		}

		// Token: 0x040007D4 RID: 2004
		internal readonly List<T> m_List;

		// Token: 0x040007D5 RID: 2005
		private readonly Func<T> m_CreateFunc;

		// Token: 0x040007D6 RID: 2006
		private readonly Action<T> m_ActionOnGet;

		// Token: 0x040007D7 RID: 2007
		private readonly Action<T> m_ActionOnRelease;

		// Token: 0x040007D8 RID: 2008
		private readonly Action<T> m_ActionOnDestroy;

		// Token: 0x040007D9 RID: 2009
		private readonly int m_MaxSize;

		// Token: 0x040007DA RID: 2010
		internal bool m_CollectionCheck;

		// Token: 0x040007DB RID: 2011
		private T m_FreshlyReleased;
	}
}
