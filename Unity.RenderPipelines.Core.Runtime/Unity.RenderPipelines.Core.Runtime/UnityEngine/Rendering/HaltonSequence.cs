using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020001D7 RID: 471
	public static class HaltonSequence
	{
		// Token: 0x06000D63 RID: 3427 RVA: 0x00031580 File Offset: 0x0002F780
		public static float Get(int index, int radix)
		{
			float result = 0f;
			float fraction = 1f / (float)radix;
			while (index > 0)
			{
				result += (float)(index % radix) * fraction;
				index /= radix;
				fraction /= (float)radix;
			}
			return result;
		}
	}
}
