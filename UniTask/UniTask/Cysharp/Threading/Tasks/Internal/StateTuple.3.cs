using System;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x0200023C RID: 572
	internal class StateTuple<T1, T2> : IDisposable
	{
		// Token: 0x06000CFE RID: 3326 RVA: 0x0002D3F1 File Offset: 0x0002B5F1
		public void Deconstruct(out T1 item1, out T2 item2)
		{
			item1 = this.Item1;
			item2 = this.Item2;
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0002D40B File Offset: 0x0002B60B
		public void Dispose()
		{
			StatePool<T1, T2>.Return(this);
		}

		// Token: 0x04000686 RID: 1670
		public T1 Item1;

		// Token: 0x04000687 RID: 1671
		public T2 Item2;
	}
}
