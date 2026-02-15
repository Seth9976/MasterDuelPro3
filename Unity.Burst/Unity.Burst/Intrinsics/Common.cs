using System;

namespace Unity.Burst.Intrinsics
{
	// Token: 0x02000031 RID: 49
	public static class Common
	{
		// Token: 0x06000A52 RID: 2642 RVA: 0x000024D5 File Offset: 0x000006D5
		public static void Pause()
		{
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00006550 File Offset: 0x00004750
		public static ulong umul128(ulong x, ulong y, out ulong high)
		{
			ulong xLo = (ulong)((uint)x);
			ulong num = x >> 32;
			ulong yLo = (ulong)((uint)y);
			ulong yHi = y >> 32;
			ulong hi = num * yHi;
			ulong m = num * yLo;
			ulong m2 = yHi * xLo;
			ulong num2 = xLo * yLo;
			ulong m1Lo = (ulong)((uint)m);
			ulong loHi = num2 >> 32;
			ulong m1Hi = m >> 32;
			high = hi + m1Hi + (loHi + m1Lo + m2 >> 32);
			return x * y;
		}
	}
}
