using System;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000239 RID: 569
	internal static class StateTuple
	{
		// Token: 0x06000CF5 RID: 3317 RVA: 0x0002D369 File Offset: 0x0002B569
		public static StateTuple<T1> Create<T1>(T1 item1)
		{
			return StatePool<T1>.Create(item1);
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0002D371 File Offset: 0x0002B571
		public static StateTuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return StatePool<T1, T2>.Create(item1, item2);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0002D37A File Offset: 0x0002B57A
		public static StateTuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
		{
			return StatePool<T1, T2, T3>.Create(item1, item2, item3);
		}
	}
}
