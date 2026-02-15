using System;

namespace UnityEngine.Pool
{
	// Token: 0x020002ED RID: 749
	public struct PooledObject<T> : IDisposable where T : class
	{
		// Token: 0x060014F8 RID: 5368 RVA: 0x0002C556 File Offset: 0x0002A756
		public PooledObject(T value, IObjectPool<T> pool)
		{
			this.m_ToReturn = value;
			this.m_Pool = pool;
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x0002C567 File Offset: 0x0002A767
		void IDisposable.Dispose()
		{
			this.m_Pool.Release(this.m_ToReturn);
		}

		// Token: 0x040007DD RID: 2013
		private readonly T m_ToReturn;

		// Token: 0x040007DE RID: 2014
		private readonly IObjectPool<T> m_Pool;
	}
}
