using System;

namespace System.Linq.Expressions
{
	// Token: 0x020000DD RID: 221
	internal static class Utils
	{
		// Token: 0x0600075C RID: 1884 RVA: 0x00017E80 File Offset: 0x00016080
		public static ConstantExpression Constant(bool value)
		{
			if (!value)
			{
				return Utils.s_false;
			}
			return Utils.s_true;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00017E90 File Offset: 0x00016090
		public static ConstantExpression Constant(int value)
		{
			switch (value)
			{
			case -1:
				return Utils.s_m1;
			case 0:
				return Utils.s_0;
			case 1:
				return Utils.s_1;
			case 2:
				return Utils.s_2;
			case 3:
				return Utils.s_3;
			default:
				return Expression.Constant(value);
			}
		}

		// Token: 0x0400021A RID: 538
		public static readonly object BoxedFalse = false;

		// Token: 0x0400021B RID: 539
		public static readonly object BoxedTrue = true;

		// Token: 0x0400021C RID: 540
		public static readonly object BoxedIntM1 = -1;

		// Token: 0x0400021D RID: 541
		public static readonly object BoxedInt0 = 0;

		// Token: 0x0400021E RID: 542
		public static readonly object BoxedInt1 = 1;

		// Token: 0x0400021F RID: 543
		public static readonly object BoxedInt2 = 2;

		// Token: 0x04000220 RID: 544
		public static readonly object BoxedInt3 = 3;

		// Token: 0x04000221 RID: 545
		public static readonly object BoxedDefaultSByte = 0;

		// Token: 0x04000222 RID: 546
		public static readonly object BoxedDefaultChar = '\0';

		// Token: 0x04000223 RID: 547
		public static readonly object BoxedDefaultInt16 = 0;

		// Token: 0x04000224 RID: 548
		public static readonly object BoxedDefaultInt64 = 0L;

		// Token: 0x04000225 RID: 549
		public static readonly object BoxedDefaultByte = 0;

		// Token: 0x04000226 RID: 550
		public static readonly object BoxedDefaultUInt16 = 0;

		// Token: 0x04000227 RID: 551
		public static readonly object BoxedDefaultUInt32 = 0U;

		// Token: 0x04000228 RID: 552
		public static readonly object BoxedDefaultUInt64 = 0UL;

		// Token: 0x04000229 RID: 553
		public static readonly object BoxedDefaultSingle = 0f;

		// Token: 0x0400022A RID: 554
		public static readonly object BoxedDefaultDouble = 0.0;

		// Token: 0x0400022B RID: 555
		public static readonly object BoxedDefaultDecimal = 0m;

		// Token: 0x0400022C RID: 556
		public static readonly object BoxedDefaultDateTime = default(DateTime);

		// Token: 0x0400022D RID: 557
		private static readonly ConstantExpression s_true = Expression.Constant(Utils.BoxedTrue);

		// Token: 0x0400022E RID: 558
		private static readonly ConstantExpression s_false = Expression.Constant(Utils.BoxedFalse);

		// Token: 0x0400022F RID: 559
		private static readonly ConstantExpression s_m1 = Expression.Constant(Utils.BoxedIntM1);

		// Token: 0x04000230 RID: 560
		private static readonly ConstantExpression s_0 = Expression.Constant(Utils.BoxedInt0);

		// Token: 0x04000231 RID: 561
		private static readonly ConstantExpression s_1 = Expression.Constant(Utils.BoxedInt1);

		// Token: 0x04000232 RID: 562
		private static readonly ConstantExpression s_2 = Expression.Constant(Utils.BoxedInt2);

		// Token: 0x04000233 RID: 563
		private static readonly ConstantExpression s_3 = Expression.Constant(Utils.BoxedInt3);

		// Token: 0x04000234 RID: 564
		public static readonly DefaultExpression Empty = Expression.Empty();

		// Token: 0x04000235 RID: 565
		public static readonly ConstantExpression Null = Expression.Constant(null);
	}
}
