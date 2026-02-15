using System;
using System.Collections.Concurrent;
using System.Threading;

namespace K4os.Compression.LZ4.Internal
{
	// Token: 0x02000008 RID: 8
	internal class Pool<T>
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000027BC File Offset: 0x000009BC
		public Pool(Func<T> create, Action<T> reset, Action<T> destroy, int size)
		{
			this._queue = new ConcurrentQueue<T>();
			this._create = create;
			Action<T> action = reset;
			if (reset == null && (action = Pool<T>.<>c.<>9__5_0) == null)
			{
				action = (Pool<T>.<>c.<>9__5_0 = delegate(T _)
				{
				});
			}
			this._reset = action;
			Action<T> action2 = destroy;
			if (destroy == null && (action2 = Pool<T>.<>c.<>9__5_1) == null)
			{
				action2 = (Pool<T>.<>c.<>9__5_1 = delegate(T _)
				{
				});
			}
			this._destroy = action2;
			this._freeSlots = size;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002840 File Offset: 0x00000A40
		public T Borrow()
		{
			T t;
			if (!this._queue.TryDequeue(out t))
			{
				return this._create();
			}
			this._reset(t);
			Interlocked.Increment(ref this._freeSlots);
			return t;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002881 File Offset: 0x00000A81
		public void Return(T resource)
		{
			if (Interlocked.Decrement(ref this._freeSlots) < 0)
			{
				Interlocked.Increment(ref this._freeSlots);
				this._destroy(resource);
				return;
			}
			this._queue.Enqueue(resource);
		}

		// Token: 0x0400001C RID: 28
		private readonly ConcurrentQueue<T> _queue;

		// Token: 0x0400001D RID: 29
		private readonly Func<T> _create;

		// Token: 0x0400001E RID: 30
		private readonly Action<T> _reset;

		// Token: 0x0400001F RID: 31
		private readonly Action<T> _destroy;

		// Token: 0x04000020 RID: 32
		private int _freeSlots;
	}
}
