using System;
using System.Threading;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000107 RID: 263
	[GenerateTestsForBurstCompatibility]
	public struct UnsafeAtomicCounter64
	{
		// Token: 0x06000B04 RID: 2820 RVA: 0x00021C34 File Offset: 0x0001FE34
		public unsafe UnsafeAtomicCounter64(void* ptr)
		{
			this.Counter = (long*)ptr;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00021C3D File Offset: 0x0001FE3D
		public unsafe void Reset(long value = 0L)
		{
			*this.Counter = value;
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00021C47 File Offset: 0x0001FE47
		public unsafe long Add(long value)
		{
			return Interlocked.Add(UnsafeUtility.AsRef<long>((void*)this.Counter), value) - value;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00021C5C File Offset: 0x0001FE5C
		public long Sub(long value)
		{
			return this.Add(-value);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00021C68 File Offset: 0x0001FE68
		public unsafe long AddSat(long value, long max = 9223372036854775807L)
		{
			long newVal = *this.Counter;
			long oldVal;
			do
			{
				oldVal = newVal;
				newVal = ((newVal >= max) ? max : math.min(max, newVal + value));
				newVal = Interlocked.CompareExchange(UnsafeUtility.AsRef<long>((void*)this.Counter), newVal, oldVal);
			}
			while (oldVal != newVal && oldVal != max);
			return oldVal;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00021CAC File Offset: 0x0001FEAC
		public unsafe long SubSat(long value, long min = -9223372036854775808L)
		{
			long newVal = *this.Counter;
			long oldVal;
			do
			{
				oldVal = newVal;
				newVal = ((newVal <= min) ? min : math.max(min, newVal - value));
				newVal = Interlocked.CompareExchange(UnsafeUtility.AsRef<long>((void*)this.Counter), newVal, oldVal);
			}
			while (oldVal != newVal && oldVal != min);
			return oldVal;
		}

		// Token: 0x040004A6 RID: 1190
		public unsafe long* Counter;
	}
}
