using System;

namespace Spine
{
	// Token: 0x02000065 RID: 101
	public static class MathUtils
	{
		// Token: 0x06000350 RID: 848 RVA: 0x0000E82C File Offset: 0x0000CA2C
		public static float Sin(float radians)
		{
			return (float)Math.Sin((double)radians);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000E836 File Offset: 0x0000CA36
		public static float Cos(float radians)
		{
			return (float)Math.Cos((double)radians);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000E840 File Offset: 0x0000CA40
		public static float SinDeg(float degrees)
		{
			return (float)Math.Sin((double)(degrees * 0.017453292f));
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000E850 File Offset: 0x0000CA50
		public static float CosDeg(float degrees)
		{
			return (float)Math.Cos((double)(degrees * 0.017453292f));
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000E860 File Offset: 0x0000CA60
		public static float Atan2Deg(float y, float x)
		{
			return (float)Math.Atan2((double)y, (double)x) * 57.295776f;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000E872 File Offset: 0x0000CA72
		public static float Atan2(float y, float x)
		{
			return (float)Math.Atan2((double)y, (double)x);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000E87E File Offset: 0x0000CA7E
		public static float Clamp(float value, float min, float max)
		{
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000E88D File Offset: 0x0000CA8D
		public static float RandomTriangle(float min, float max)
		{
			return MathUtils.RandomTriangle(min, max, (min + max) * 0.5f);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000E8A0 File Offset: 0x0000CAA0
		public static float RandomTriangle(float min, float max, float mode)
		{
			float u = (float)MathUtils.random.NextDouble();
			float d = max - min;
			if (u <= (mode - min) / d)
			{
				return min + (float)Math.Sqrt((double)(u * d * (mode - min)));
			}
			return max - (float)Math.Sqrt((double)((1f - u) * d * (max - mode)));
		}

		// Token: 0x040001D9 RID: 473
		public const float PI = 3.1415927f;

		// Token: 0x040001DA RID: 474
		public const float PI2 = 6.2831855f;

		// Token: 0x040001DB RID: 475
		public const float InvPI2 = 0.15915494f;

		// Token: 0x040001DC RID: 476
		public const float RadDeg = 57.295776f;

		// Token: 0x040001DD RID: 477
		public const float DegRad = 0.017453292f;

		// Token: 0x040001DE RID: 478
		private static Random random = new Random();
	}
}
