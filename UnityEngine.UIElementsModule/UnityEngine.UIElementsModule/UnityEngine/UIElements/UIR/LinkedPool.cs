using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200052F RID: 1327
	internal class LinkedPool<T> where T : LinkedPoolItem<T>
	{
		// Token: 0x060024B4 RID: 9396 RVA: 0x0008BE23 File Offset: 0x0008A023
		public LinkedPool(Func<T> createFunc, Action<T> resetAction, int limit = 10000)
		{
			Debug.Assert(createFunc != null);
			this.m_CreateFunc = createFunc;
			this.m_ResetAction = resetAction;
			Debug.Assert(limit > 0);
			this.m_Limit = limit;
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x060024B5 RID: 9397 RVA: 0x0008BE56 File Offset: 0x0008A056
		// (set) Token: 0x060024B6 RID: 9398 RVA: 0x0008BE5E File Offset: 0x0008A05E
		public int Count { get; private set; }

		// Token: 0x060024B7 RID: 9399 RVA: 0x0008BE67 File Offset: 0x0008A067
		public void Clear()
		{
			this.m_PoolFirst = default(T);
			this.Count = 0;
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x0008BE80 File Offset: 0x0008A080
		public T Get()
		{
			T item = this.m_PoolFirst;
			bool flag = this.m_PoolFirst != null;
			if (flag)
			{
				int num = this.Count - 1;
				this.Count = num;
				this.m_PoolFirst = item.poolNext;
				Action<T> resetAction = this.m_ResetAction;
				if (resetAction != null)
				{
					resetAction(item);
				}
			}
			else
			{
				item = this.m_CreateFunc();
			}
			return item;
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x0008BEF4 File Offset: 0x0008A0F4
		public void Return(T item)
		{
			bool flag = this.Count < this.m_Limit;
			if (flag)
			{
				item.poolNext = this.m_PoolFirst;
				this.m_PoolFirst = item;
				int num = this.Count + 1;
				this.Count = num;
			}
		}

		// Token: 0x040011B3 RID: 4531
		private readonly Func<T> m_CreateFunc;

		// Token: 0x040011B4 RID: 4532
		private readonly Action<T> m_ResetAction;

		// Token: 0x040011B5 RID: 4533
		private readonly int m_Limit;

		// Token: 0x040011B6 RID: 4534
		private T m_PoolFirst;
	}
}
