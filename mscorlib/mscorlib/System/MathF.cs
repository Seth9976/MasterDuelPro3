using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200011B RID: 283
	public static class MathF
	{
		// Token: 0x06000972 RID: 2418 RVA: 0x00028949 File Offset: 0x00026B49
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Abs(float x)
		{
			return Math.Abs(x);
		}

		// Token: 0x06000973 RID: 2419
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Ceiling(float x);

		// Token: 0x04000448 RID: 1096
		private static float[] roundPower10Single = new float[] { 1f, 10f, 100f, 1000f, 10000f, 100000f, 1000000f };

		// Token: 0x04000449 RID: 1097
		private static float singleRoundLimit = 100000000f;
	}
}
