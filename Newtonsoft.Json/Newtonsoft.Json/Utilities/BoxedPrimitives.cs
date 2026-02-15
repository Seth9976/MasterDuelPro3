using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000099 RID: 153
	[NullableContext(1)]
	[Nullable(0)]
	internal static class BoxedPrimitives
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x0001AEBF File Offset: 0x000190BF
		internal static object Get(bool value)
		{
			if (!value)
			{
				return BoxedPrimitives.BooleanFalse;
			}
			return BoxedPrimitives.BooleanTrue;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001AED0 File Offset: 0x000190D0
		internal static object Get(int value)
		{
			object obj;
			switch (value)
			{
			case -1:
				obj = BoxedPrimitives.Int32_M1;
				break;
			case 0:
				obj = BoxedPrimitives.Int32_0;
				break;
			case 1:
				obj = BoxedPrimitives.Int32_1;
				break;
			case 2:
				obj = BoxedPrimitives.Int32_2;
				break;
			case 3:
				obj = BoxedPrimitives.Int32_3;
				break;
			case 4:
				obj = BoxedPrimitives.Int32_4;
				break;
			case 5:
				obj = BoxedPrimitives.Int32_5;
				break;
			case 6:
				obj = BoxedPrimitives.Int32_6;
				break;
			case 7:
				obj = BoxedPrimitives.Int32_7;
				break;
			case 8:
				obj = BoxedPrimitives.Int32_8;
				break;
			default:
				obj = value;
				break;
			}
			return obj;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001AF68 File Offset: 0x00019168
		internal static object Get(long value)
		{
			long num = value - -1L;
			if (num <= 9L)
			{
				switch ((uint)num)
				{
				case 0U:
					return BoxedPrimitives.Int64_M1;
				case 1U:
					return BoxedPrimitives.Int64_0;
				case 2U:
					return BoxedPrimitives.Int64_1;
				case 3U:
					return BoxedPrimitives.Int64_2;
				case 4U:
					return BoxedPrimitives.Int64_3;
				case 5U:
					return BoxedPrimitives.Int64_4;
				case 6U:
					return BoxedPrimitives.Int64_5;
				case 7U:
					return BoxedPrimitives.Int64_6;
				case 8U:
					return BoxedPrimitives.Int64_7;
				case 9U:
					return BoxedPrimitives.Int64_8;
				}
			}
			return value;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001B00D File Offset: 0x0001920D
		internal static object Get(decimal value)
		{
			if (!(value == 0m))
			{
				return value;
			}
			return BoxedPrimitives.DecimalZero;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001B028 File Offset: 0x00019228
		internal static object Get(double value)
		{
			if (value == 0.0)
			{
				return BoxedPrimitives.DoubleZero;
			}
			if (double.IsInfinity(value))
			{
				if (!double.IsPositiveInfinity(value))
				{
					return BoxedPrimitives.DoubleNegativeInfinity;
				}
				return BoxedPrimitives.DoublePositiveInfinity;
			}
			else
			{
				if (double.IsNaN(value))
				{
					return BoxedPrimitives.DoubleNaN;
				}
				return value;
			}
		}

		// Token: 0x04000382 RID: 898
		internal static readonly object BooleanTrue = true;

		// Token: 0x04000383 RID: 899
		internal static readonly object BooleanFalse = false;

		// Token: 0x04000384 RID: 900
		internal static readonly object Int32_M1 = -1;

		// Token: 0x04000385 RID: 901
		internal static readonly object Int32_0 = 0;

		// Token: 0x04000386 RID: 902
		internal static readonly object Int32_1 = 1;

		// Token: 0x04000387 RID: 903
		internal static readonly object Int32_2 = 2;

		// Token: 0x04000388 RID: 904
		internal static readonly object Int32_3 = 3;

		// Token: 0x04000389 RID: 905
		internal static readonly object Int32_4 = 4;

		// Token: 0x0400038A RID: 906
		internal static readonly object Int32_5 = 5;

		// Token: 0x0400038B RID: 907
		internal static readonly object Int32_6 = 6;

		// Token: 0x0400038C RID: 908
		internal static readonly object Int32_7 = 7;

		// Token: 0x0400038D RID: 909
		internal static readonly object Int32_8 = 8;

		// Token: 0x0400038E RID: 910
		internal static readonly object Int64_M1 = -1L;

		// Token: 0x0400038F RID: 911
		internal static readonly object Int64_0 = 0L;

		// Token: 0x04000390 RID: 912
		internal static readonly object Int64_1 = 1L;

		// Token: 0x04000391 RID: 913
		internal static readonly object Int64_2 = 2L;

		// Token: 0x04000392 RID: 914
		internal static readonly object Int64_3 = 3L;

		// Token: 0x04000393 RID: 915
		internal static readonly object Int64_4 = 4L;

		// Token: 0x04000394 RID: 916
		internal static readonly object Int64_5 = 5L;

		// Token: 0x04000395 RID: 917
		internal static readonly object Int64_6 = 6L;

		// Token: 0x04000396 RID: 918
		internal static readonly object Int64_7 = 7L;

		// Token: 0x04000397 RID: 919
		internal static readonly object Int64_8 = 8L;

		// Token: 0x04000398 RID: 920
		private static readonly object DecimalZero = 0m;

		// Token: 0x04000399 RID: 921
		internal static readonly object DoubleNaN = double.NaN;

		// Token: 0x0400039A RID: 922
		internal static readonly object DoublePositiveInfinity = double.PositiveInfinity;

		// Token: 0x0400039B RID: 923
		internal static readonly object DoubleNegativeInfinity = double.NegativeInfinity;

		// Token: 0x0400039C RID: 924
		internal static readonly object DoubleZero = 0.0;
	}
}
