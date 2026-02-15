using System;
using System.Collections.Generic;

namespace Spine
{
	// Token: 0x0200003B RID: 59
	internal class Pool<T> where T : class, new()
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600017C RID: 380 RVA: 0x000092DB File Offset: 0x000074DB
		public int Count
		{
			get
			{
				return this.freeObjects.Count;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000092E8 File Offset: 0x000074E8
		// (set) Token: 0x0600017E RID: 382 RVA: 0x000092F0 File Offset: 0x000074F0
		public int Peak { get; private set; }

		// Token: 0x0600017F RID: 383 RVA: 0x000092F9 File Offset: 0x000074F9
		public Pool(int initialCapacity = 16, int max = 2147483647)
		{
			this.freeObjects = new Stack<T>(initialCapacity);
			this.max = max;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00009314 File Offset: 0x00007514
		public T Obtain()
		{
			if (this.freeObjects.Count != 0)
			{
				return this.freeObjects.Pop();
			}
			return new T();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00009334 File Offset: 0x00007534
		public void Free(T obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj", "obj cannot be null");
			}
			if (this.freeObjects.Count < this.max)
			{
				this.freeObjects.Push(obj);
				this.Peak = Math.Max(this.Peak, this.freeObjects.Count);
			}
			this.Reset(obj);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000939B File Offset: 0x0000759B
		public void Clear()
		{
			this.freeObjects.Clear();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000093A8 File Offset: 0x000075A8
		protected void Reset(T obj)
		{
			Pool<T>.IPoolable poolable = obj as Pool<T>.IPoolable;
			if (poolable != null)
			{
				poolable.Reset();
			}
		}

		// Token: 0x040000F0 RID: 240
		public readonly int max;

		// Token: 0x040000F1 RID: 241
		private readonly Stack<T> freeObjects;

		// Token: 0x0200003C RID: 60
		public interface IPoolable
		{
			// Token: 0x06000184 RID: 388
			void Reset();
		}
	}
}
