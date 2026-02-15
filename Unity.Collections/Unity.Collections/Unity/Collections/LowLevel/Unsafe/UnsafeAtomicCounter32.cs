using System;
using System.Threading;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000106 RID: 262
	[GenerateTestsForBurstCompatibility]
	public struct UnsafeAtomicCounter32
	{
		// Token: 0x06000AFE RID: 2814 RVA: 0x00021B7A File Offset: 0x0001FD7A
		public unsafe UnsafeAtomicCounter32(void* ptr)
		{
			this.Counter = (int*)ptr;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00021B83 File Offset: 0x0001FD83
		public unsafe void Reset(int value = 0)
		{
			*this.Counter = value;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00021B8D File Offset: 0x0001FD8D
		public unsafe int Add(int value)
		{
			return Interlocked.Add(UnsafeUtility.AsRef<int>((void*)this.Counter), value) - value;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00021BA2 File Offset: 0x0001FDA2
		public int Sub(int value)
		{
			return this.Add(-value);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00021BAC File Offset: 0x0001FDAC
		public unsafe int AddSat(int value, int max = 2147483647)
		{
			int newVal = *this.Counter;
			int oldVal;
			do
			{
				oldVal = newVal;
				newVal = ((newVal >= max) ? max : math.min(max, newVal + value));
				newVal = Interlocked.CompareExchange(UnsafeUtility.AsRef<int>((void*)this.Counter), newVal, oldVal);
			}
			while (oldVal != newVal && oldVal != max);
			return oldVal;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00021BF0 File Offset: 0x0001FDF0
		public unsafe int SubSat(int value, int min = -2147483648)
		{
			int newVal = *this.Counter;
			int oldVal;
			do
			{
				oldVal = newVal;
				newVal = ((newVal <= min) ? min : math.max(min, newVal - value));
				newVal = Interlocked.CompareExchange(UnsafeUtility.AsRef<int>((void*)this.Counter), newVal, oldVal);
			}
			while (oldVal != newVal && oldVal != min);
			return oldVal;
		}

		// Token: 0x040004A5 RID: 1189
		public unsafe int* Counter;
	}
}
