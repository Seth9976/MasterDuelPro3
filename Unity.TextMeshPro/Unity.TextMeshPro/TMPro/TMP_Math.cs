using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x020000A9 RID: 169
	public static class TMP_Math
	{
		// Token: 0x0600063E RID: 1598 RVA: 0x0002EAFA File Offset: 0x0002CCFA
		public static bool Approximately(float a, float b)
		{
			return b - 0.0001f < a && a < b + 0.0001f;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0002EB14 File Offset: 0x0002CD14
		public static int Mod(int a, int b)
		{
			int r = a % b;
			if (r >= 0)
			{
				return r;
			}
			return r + b;
		}

		// Token: 0x040005AE RID: 1454
		public const float FLOAT_MAX = 32767f;

		// Token: 0x040005AF RID: 1455
		public const float FLOAT_MIN = -32767f;

		// Token: 0x040005B0 RID: 1456
		public const int INT_MAX = 2147483647;

		// Token: 0x040005B1 RID: 1457
		public const int INT_MIN = -2147483647;

		// Token: 0x040005B2 RID: 1458
		public const float FLOAT_UNSET = -32767f;

		// Token: 0x040005B3 RID: 1459
		public const int INT_UNSET = -32767;

		// Token: 0x040005B4 RID: 1460
		public static Vector2 MAX_16BIT = new Vector2(32767f, 32767f);

		// Token: 0x040005B5 RID: 1461
		public static Vector2 MIN_16BIT = new Vector2(-32767f, -32767f);
	}
}
