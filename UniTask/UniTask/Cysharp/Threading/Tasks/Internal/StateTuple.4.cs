using System;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x0200023E RID: 574
	internal class StateTuple<T1, T2, T3> : IDisposable
	{
		// Token: 0x06000D04 RID: 3332 RVA: 0x0002D483 File Offset: 0x0002B683
		public void Deconstruct(out T1 item1, out T2 item2, out T3 item3)
		{
			item1 = this.Item1;
			item2 = this.Item2;
			item3 = this.Item3;
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0002D4A9 File Offset: 0x0002B6A9
		public void Dispose()
		{
			StatePool<T1, T2, T3>.Return(this);
		}

		// Token: 0x04000689 RID: 1673
		public T1 Item1;

		// Token: 0x0400068A RID: 1674
		public T2 Item2;

		// Token: 0x0400068B RID: 1675
		public T3 Item3;
	}
}
