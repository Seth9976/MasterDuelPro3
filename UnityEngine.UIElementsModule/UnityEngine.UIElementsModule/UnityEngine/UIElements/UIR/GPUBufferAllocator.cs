using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000514 RID: 1300
	internal class GPUBufferAllocator
	{
		// Token: 0x0600242D RID: 9261 RVA: 0x000882EC File Offset: 0x000864EC
		public GPUBufferAllocator(uint maxSize)
		{
			this.m_Low = new BestFitAllocator(maxSize);
			this.m_High = new BestFitAllocator(maxSize);
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x00088310 File Offset: 0x00086510
		public Alloc Allocate(uint size, bool shortLived)
		{
			bool flag = !shortLived;
			Alloc alloc;
			if (flag)
			{
				alloc = this.m_Low.Allocate(size);
			}
			else
			{
				alloc = this.m_High.Allocate(size);
				alloc.start = this.m_High.totalSize - alloc.start - alloc.size;
			}
			alloc.shortLived = shortLived;
			bool flag2 = this.HighLowCollide() && alloc.size > 0U;
			Alloc alloc2;
			if (flag2)
			{
				this.Free(alloc);
				alloc2 = default(Alloc);
			}
			else
			{
				alloc2 = alloc;
			}
			return alloc2;
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x000883A4 File Offset: 0x000865A4
		public void Free(Alloc alloc)
		{
			bool flag = !alloc.shortLived;
			if (flag)
			{
				this.m_Low.Free(alloc);
			}
			else
			{
				alloc.start = this.m_High.totalSize - alloc.start - alloc.size;
				this.m_High.Free(alloc);
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002430 RID: 9264 RVA: 0x00088400 File Offset: 0x00086600
		public bool isEmpty
		{
			get
			{
				return this.m_Low.highWatermark == 0U && this.m_High.highWatermark == 0U;
			}
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x00088430 File Offset: 0x00086630
		private bool HighLowCollide()
		{
			return this.m_Low.highWatermark + this.m_High.highWatermark > this.m_Low.totalSize;
		}

		// Token: 0x040010CF RID: 4303
		private BestFitAllocator m_Low;

		// Token: 0x040010D0 RID: 4304
		private BestFitAllocator m_High;
	}
}
