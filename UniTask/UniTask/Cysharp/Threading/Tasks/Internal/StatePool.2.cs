using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x0200023D RID: 573
	internal static class StatePool<T1, T2>
	{
		// Token: 0x06000D01 RID: 3329 RVA: 0x0002D414 File Offset: 0x0002B614
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static StateTuple<T1, T2> Create(T1 item1, T2 item2)
		{
			StateTuple<T1, T2> value;
			if (StatePool<T1, T2>.queue.TryDequeue(out value))
			{
				value.Item1 = item1;
				value.Item2 = item2;
				return value;
			}
			return new StateTuple<T1, T2>
			{
				Item1 = item1,
				Item2 = item2
			};
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0002D452 File Offset: 0x0002B652
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Return(StateTuple<T1, T2> tuple)
		{
			tuple.Item1 = default(T1);
			tuple.Item2 = default(T2);
			StatePool<T1, T2>.queue.Enqueue(tuple);
		}

		// Token: 0x04000688 RID: 1672
		private static readonly ConcurrentQueue<StateTuple<T1, T2>> queue = new ConcurrentQueue<StateTuple<T1, T2>>();
	}
}
