using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x0200023F RID: 575
	internal static class StatePool<T1, T2, T3>
	{
		// Token: 0x06000D07 RID: 3335 RVA: 0x0002D4B4 File Offset: 0x0002B6B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static StateTuple<T1, T2, T3> Create(T1 item1, T2 item2, T3 item3)
		{
			StateTuple<T1, T2, T3> value;
			if (StatePool<T1, T2, T3>.queue.TryDequeue(out value))
			{
				value.Item1 = item1;
				value.Item2 = item2;
				value.Item3 = item3;
				return value;
			}
			return new StateTuple<T1, T2, T3>
			{
				Item1 = item1,
				Item2 = item2,
				Item3 = item3
			};
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0002D500 File Offset: 0x0002B700
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Return(StateTuple<T1, T2, T3> tuple)
		{
			tuple.Item1 = default(T1);
			tuple.Item2 = default(T2);
			tuple.Item3 = default(T3);
			StatePool<T1, T2, T3>.queue.Enqueue(tuple);
		}

		// Token: 0x0400068C RID: 1676
		private static readonly ConcurrentQueue<StateTuple<T1, T2, T3>> queue = new ConcurrentQueue<StateTuple<T1, T2, T3>>();
	}
}
