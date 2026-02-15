using System;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000235 RID: 565
	internal sealed class PooledDelegate<T> : ITaskPoolNode<PooledDelegate<T>>
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0002D13C File Offset: 0x0002B33C
		public ref PooledDelegate<T> NextNode
		{
			get
			{
				return ref this.nextNode;
			}
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0002D144 File Offset: 0x0002B344
		static PooledDelegate()
		{
			TaskPool.RegisterSizeGetter(typeof(PooledDelegate<T>), () => PooledDelegate<T>.pool.Size);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0002D165 File Offset: 0x0002B365
		private PooledDelegate()
		{
			this.runDelegate = new Action<T>(this.Run);
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0002D180 File Offset: 0x0002B380
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Action<T> Create(Action continuation)
		{
			PooledDelegate<T> item;
			if (!PooledDelegate<T>.pool.TryPop(out item))
			{
				item = new PooledDelegate<T>();
			}
			item.continuation = continuation;
			return item.runDelegate;
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0002D1B0 File Offset: 0x0002B3B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Run(T _)
		{
			Action call = this.continuation;
			this.continuation = null;
			if (call != null)
			{
				PooledDelegate<T>.pool.TryPush(this);
				call();
			}
		}

		// Token: 0x0400067E RID: 1662
		private static TaskPool<PooledDelegate<T>> pool;

		// Token: 0x0400067F RID: 1663
		private PooledDelegate<T> nextNode;

		// Token: 0x04000680 RID: 1664
		private readonly Action<T> runDelegate;

		// Token: 0x04000681 RID: 1665
		private Action continuation;
	}
}
