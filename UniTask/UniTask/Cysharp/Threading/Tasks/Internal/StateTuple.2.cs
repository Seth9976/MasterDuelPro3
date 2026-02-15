using System;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x0200023A RID: 570
	internal class StateTuple<T1> : IDisposable
	{
		// Token: 0x06000CF8 RID: 3320 RVA: 0x0002D384 File Offset: 0x0002B584
		public void Deconstruct(out T1 item1)
		{
			item1 = this.Item1;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0002D392 File Offset: 0x0002B592
		public void Dispose()
		{
			StatePool<T1>.Return(this);
		}

		// Token: 0x04000684 RID: 1668
		public T1 Item1;
	}
}
