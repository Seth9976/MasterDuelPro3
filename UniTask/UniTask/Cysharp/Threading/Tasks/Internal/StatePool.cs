using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x0200023B RID: 571
	internal static class StatePool<T1>
	{
		// Token: 0x06000CFB RID: 3323 RVA: 0x0002D39C File Offset: 0x0002B59C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static StateTuple<T1> Create(T1 item1)
		{
			StateTuple<T1> value;
			if (StatePool<T1>.queue.TryDequeue(out value))
			{
				value.Item1 = item1;
				return value;
			}
			return new StateTuple<T1>
			{
				Item1 = item1
			};
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0002D3CC File Offset: 0x0002B5CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Return(StateTuple<T1> tuple)
		{
			tuple.Item1 = default(T1);
			StatePool<T1>.queue.Enqueue(tuple);
		}

		// Token: 0x04000685 RID: 1669
		private static readonly ConcurrentQueue<StateTuple<T1>> queue = new ConcurrentQueue<StateTuple<T1>>();
	}
}
