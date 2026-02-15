using System;

namespace Spine.Unity
{
	// Token: 0x0200005D RID: 93
	public class DoubleBuffered<T> where T : new()
	{
		// Token: 0x060002EE RID: 750 RVA: 0x0000F55F File Offset: 0x0000D75F
		public T GetCurrent()
		{
			if (!this.usingA)
			{
				return this.b;
			}
			return this.a;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000F576 File Offset: 0x0000D776
		public T GetNext()
		{
			this.usingA = !this.usingA;
			if (!this.usingA)
			{
				return this.b;
			}
			return this.a;
		}

		// Token: 0x040001D3 RID: 467
		private readonly T a = new T();

		// Token: 0x040001D4 RID: 468
		private readonly T b = new T();

		// Token: 0x040001D5 RID: 469
		private bool usingA;
	}
}
