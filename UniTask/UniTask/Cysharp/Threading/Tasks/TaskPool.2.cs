using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000070 RID: 112
	[StructLayout(LayoutKind.Auto)]
	public struct TaskPool<T> where T : class, ITaskPoolNode<T>
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000056DF File Offset: 0x000038DF
		public int Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000056E8 File Offset: 0x000038E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryPop(out T result)
		{
			if (Interlocked.CompareExchange(ref this.gate, 1, 0) == 0)
			{
				T v = this.root;
				if (v != null)
				{
					ref T nextNode = ref v.NextNode;
					this.root = nextNode;
					nextNode = default(T);
					this.size--;
					result = v;
					Volatile.Write(ref this.gate, 0);
					return true;
				}
				Volatile.Write(ref this.gate, 0);
			}
			result = default(T);
			return false;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000576C File Offset: 0x0000396C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe bool TryPush(T item)
		{
			if (Interlocked.CompareExchange(ref this.gate, 1, 0) == 0)
			{
				if (this.size < TaskPool.MaxPoolSize)
				{
					*item.NextNode = this.root;
					this.root = item;
					this.size++;
					Volatile.Write(ref this.gate, 0);
					return true;
				}
				Volatile.Write(ref this.gate, 0);
			}
			return false;
		}

		// Token: 0x040000EE RID: 238
		private int gate;

		// Token: 0x040000EF RID: 239
		private int size;

		// Token: 0x040000F0 RID: 240
		private T root;
	}
}
