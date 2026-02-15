using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x0200007A RID: 122
	[GenerateTestsForBurstCompatibility]
	public static class FixedString
	{
		// Token: 0x0600056E RID: 1390 RVA: 0x0000F1F0 File Offset: 0x0000D3F0
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, int arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0000F260 File Offset: 0x0000D460
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, int arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000F2D0 File Offset: 0x0000D4D0
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, int arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000F340 File Offset: 0x0000D540
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, int arg1, int arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000F39C File Offset: 0x0000D59C
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, int arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000F40C File Offset: 0x0000D60C
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, int arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000F480 File Offset: 0x0000D680
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, int arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0000F4F0 File Offset: 0x0000D6F0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, float arg1, int arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0000F550 File Offset: 0x0000D750
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, int arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000F5C0 File Offset: 0x0000D7C0
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, int arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000F630 File Offset: 0x0000D830
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, int arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000F6A0 File Offset: 0x0000D8A0
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, string arg1, int arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0000F6FC File Offset: 0x0000D8FC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, T1 arg1, int arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0000F758 File Offset: 0x0000D958
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, T1 arg1, int arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0000F7B8 File Offset: 0x0000D9B8
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, T1 arg1, int arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0000F814 File Offset: 0x0000DA14
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, int arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in arg1, in carg2, in carg3);
			return result;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0000F860 File Offset: 0x0000DA60
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, float arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000F8D0 File Offset: 0x0000DAD0
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, float arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000F944 File Offset: 0x0000DB44
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, float arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000F9B4 File Offset: 0x0000DBB4
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, int arg1, float arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0000FA14 File Offset: 0x0000DC14
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, float arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000FA88 File Offset: 0x0000DC88
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, float arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0000FAFC File Offset: 0x0000DCFC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, float arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000FB70 File Offset: 0x0000DD70
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, float arg1, float arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0000FBD0 File Offset: 0x0000DDD0
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, float arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0000FC40 File Offset: 0x0000DE40
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, float arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0000FCB4 File Offset: 0x0000DEB4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, float arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0000FD24 File Offset: 0x0000DF24
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, string arg1, float arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0000FD84 File Offset: 0x0000DF84
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, T1 arg1, float arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0000FDE4 File Offset: 0x0000DFE4
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, T1 arg1, float arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0000FE44 File Offset: 0x0000E044
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, T1 arg1, float arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000FEA4 File Offset: 0x0000E0A4
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, float arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in arg1, in carg2, in carg3);
			return result;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0000FEF4 File Offset: 0x0000E0F4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, string arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0000FF64 File Offset: 0x0000E164
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, string arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0000FFD4 File Offset: 0x0000E1D4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, string arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00010044 File Offset: 0x0000E244
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, int arg1, string arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000100A0 File Offset: 0x0000E2A0
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, string arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00010110 File Offset: 0x0000E310
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, string arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00010184 File Offset: 0x0000E384
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, string arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x000101F4 File Offset: 0x0000E3F4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, float arg1, string arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00010254 File Offset: 0x0000E454
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, string arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x000102C4 File Offset: 0x0000E4C4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, string arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00010334 File Offset: 0x0000E534
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, string arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000103A4 File Offset: 0x0000E5A4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, string arg1, string arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00010400 File Offset: 0x0000E600
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, T1 arg1, string arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001045C File Offset: 0x0000E65C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, T1 arg1, string arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x000104BC File Offset: 0x0000E6BC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, T1 arg1, string arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00010518 File Offset: 0x0000E718
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, string arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in arg1, in carg2, in carg3);
			return result;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00010564 File Offset: 0x0000E764
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, int arg1, T1 arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000105C0 File Offset: 0x0000E7C0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, int arg1, T1 arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00010620 File Offset: 0x0000E820
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, int arg1, T1 arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001067C File Offset: 0x0000E87C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, int arg1, T2 arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x000106C8 File Offset: 0x0000E8C8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, float arg1, T1 arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00010728 File Offset: 0x0000E928
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, float arg1, T1 arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00010788 File Offset: 0x0000E988
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, float arg1, T1 arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x000107E8 File Offset: 0x0000E9E8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, float arg1, T2 arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00010838 File Offset: 0x0000EA38
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, string arg1, T1 arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00010894 File Offset: 0x0000EA94
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, string arg1, T1 arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x000108F4 File Offset: 0x0000EAF4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, string arg1, T1 arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00010950 File Offset: 0x0000EB50
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, string arg1, T2 arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001099C File Offset: 0x0000EB9C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, int arg0, T1 arg1, T2 arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in arg2, in carg);
			return result;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x000109E8 File Offset: 0x0000EBE8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, float arg0, T1 arg1, T2 arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in arg2, in carg);
			return result;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00010A38 File Offset: 0x0000EC38
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, string arg0, T1 arg1, T2 arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in arg2, in carg);
			return result;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00010A84 File Offset: 0x0000EC84
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3>(FixedString512Bytes formatString, T1 arg0, T2 arg1, T3 arg2, int arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T3 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in arg1, in arg2, in carg3);
			return result;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00010AC0 File Offset: 0x0000ECC0
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, int arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00010B30 File Offset: 0x0000ED30
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, int arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00010BA4 File Offset: 0x0000EDA4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, int arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00010C14 File Offset: 0x0000EE14
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, int arg1, int arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00010C74 File Offset: 0x0000EE74
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, int arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00010CE8 File Offset: 0x0000EEE8
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, int arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00010D5C File Offset: 0x0000EF5C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, int arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00010DD0 File Offset: 0x0000EFD0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, float arg1, int arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00010E30 File Offset: 0x0000F030
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, int arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00010EA0 File Offset: 0x0000F0A0
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, int arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00010F14 File Offset: 0x0000F114
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, int arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00010F84 File Offset: 0x0000F184
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, string arg1, int arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00010FE4 File Offset: 0x0000F1E4
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, T1 arg1, int arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00011044 File Offset: 0x0000F244
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, T1 arg1, int arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000110A4 File Offset: 0x0000F2A4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, T1 arg1, int arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00011104 File Offset: 0x0000F304
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, int arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in arg0, in arg1, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00011154 File Offset: 0x0000F354
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, float arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x000111C8 File Offset: 0x0000F3C8
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, float arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001123C File Offset: 0x0000F43C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, float arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000112B0 File Offset: 0x0000F4B0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, int arg1, float arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00011310 File Offset: 0x0000F510
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, float arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00011384 File Offset: 0x0000F584
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, float arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000113FC File Offset: 0x0000F5FC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, float arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00011470 File Offset: 0x0000F670
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, float arg1, float arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000114D4 File Offset: 0x0000F6D4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, float arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00011548 File Offset: 0x0000F748
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, float arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000115BC File Offset: 0x0000F7BC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, float arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00011630 File Offset: 0x0000F830
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, string arg1, float arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00011690 File Offset: 0x0000F890
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, T1 arg1, float arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000116F0 File Offset: 0x0000F8F0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, T1 arg1, float arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00011754 File Offset: 0x0000F954
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, T1 arg1, float arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000117B4 File Offset: 0x0000F9B4
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, float arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in arg0, in arg1, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00011804 File Offset: 0x0000FA04
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, string arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00011874 File Offset: 0x0000FA74
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, string arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000118E8 File Offset: 0x0000FAE8
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, string arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00011958 File Offset: 0x0000FB58
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, int arg1, string arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000119B8 File Offset: 0x0000FBB8
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, string arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00011A2C File Offset: 0x0000FC2C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, string arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00011AA0 File Offset: 0x0000FCA0
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, string arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00011B14 File Offset: 0x0000FD14
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, float arg1, string arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00011B74 File Offset: 0x0000FD74
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, string arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00011BE4 File Offset: 0x0000FDE4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, string arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00011C58 File Offset: 0x0000FE58
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, string arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00011CC8 File Offset: 0x0000FEC8
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, string arg1, string arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00011D28 File Offset: 0x0000FF28
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, T1 arg1, string arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00011D88 File Offset: 0x0000FF88
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, T1 arg1, string arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00011DE8 File Offset: 0x0000FFE8
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, T1 arg1, string arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00011E48 File Offset: 0x00010048
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, string arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in arg0, in arg1, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00011E98 File Offset: 0x00010098
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, int arg1, T1 arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00011EF8 File Offset: 0x000100F8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, int arg1, T1 arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00011F58 File Offset: 0x00010158
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, int arg1, T1 arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00011FB8 File Offset: 0x000101B8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, int arg1, T2 arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in arg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00012008 File Offset: 0x00010208
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, float arg1, T1 arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00012068 File Offset: 0x00010268
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, float arg1, T1 arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000120CC File Offset: 0x000102CC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, float arg1, T1 arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001212C File Offset: 0x0001032C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, float arg1, T2 arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in arg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001217C File Offset: 0x0001037C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, string arg1, T1 arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000121DC File Offset: 0x000103DC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, string arg1, T1 arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001223C File Offset: 0x0001043C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, string arg1, T1 arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001229C File Offset: 0x0001049C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, string arg1, T2 arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in arg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x000122EC File Offset: 0x000104EC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, int arg0, T1 arg1, T2 arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in arg2, in carg);
			return result;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001233C File Offset: 0x0001053C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, float arg0, T1 arg1, T2 arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in arg2, in carg);
			return result;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001238C File Offset: 0x0001058C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, string arg0, T1 arg1, T2 arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in arg2, in carg);
			return result;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000123DC File Offset: 0x000105DC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3>(FixedString512Bytes formatString, T1 arg0, T2 arg1, T3 arg2, float arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T3 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3, '.');
			(ref result).AppendFormat(in formatString, in arg0, in arg1, in arg2, in carg3);
			return result;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00012418 File Offset: 0x00010618
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, int arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00012488 File Offset: 0x00010688
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, int arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000124F8 File Offset: 0x000106F8
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, int arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00012568 File Offset: 0x00010768
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, int arg1, int arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000125C4 File Offset: 0x000107C4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, int arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00012634 File Offset: 0x00010834
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, int arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x000126A8 File Offset: 0x000108A8
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, int arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00012718 File Offset: 0x00010918
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, float arg1, int arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00012778 File Offset: 0x00010978
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, int arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x000127E8 File Offset: 0x000109E8
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, int arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00012858 File Offset: 0x00010A58
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, int arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x000128C8 File Offset: 0x00010AC8
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, string arg1, int arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00012924 File Offset: 0x00010B24
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, T1 arg1, int arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00012980 File Offset: 0x00010B80
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, T1 arg1, int arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000129E0 File Offset: 0x00010BE0
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, T1 arg1, int arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00012A3C File Offset: 0x00010C3C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, int arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in arg1, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00012A88 File Offset: 0x00010C88
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, float arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00012AF8 File Offset: 0x00010CF8
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, float arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00012B6C File Offset: 0x00010D6C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, float arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00012BDC File Offset: 0x00010DDC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, int arg1, float arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00012C3C File Offset: 0x00010E3C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, float arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00012CB0 File Offset: 0x00010EB0
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, float arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00012D24 File Offset: 0x00010F24
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, float arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00012D98 File Offset: 0x00010F98
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, float arg1, float arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00012DF8 File Offset: 0x00010FF8
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, float arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00012E68 File Offset: 0x00011068
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, float arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00012EDC File Offset: 0x000110DC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, float arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00012F4C File Offset: 0x0001114C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, string arg1, float arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00012FAC File Offset: 0x000111AC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, T1 arg1, float arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001300C File Offset: 0x0001120C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, T1 arg1, float arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001306C File Offset: 0x0001126C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, T1 arg1, float arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x000130CC File Offset: 0x000112CC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, float arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in arg1, in carg2, in carg3);
			return result;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001311C File Offset: 0x0001131C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, string arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001318C File Offset: 0x0001138C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, string arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x000131FC File Offset: 0x000113FC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, string arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001326C File Offset: 0x0001146C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, int arg1, string arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x000132C8 File Offset: 0x000114C8
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, string arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00013338 File Offset: 0x00011538
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, string arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x000133AC File Offset: 0x000115AC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, string arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001341C File Offset: 0x0001161C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, float arg1, string arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0001347C File Offset: 0x0001167C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, string arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x000134EC File Offset: 0x000116EC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, string arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001355C File Offset: 0x0001175C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, string arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000135CC File Offset: 0x000117CC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, T1 arg0, string arg1, string arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in carg3);
			return result;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00013628 File Offset: 0x00011828
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, T1 arg1, string arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00013684 File Offset: 0x00011884
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, T1 arg1, string arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000136E4 File Offset: 0x000118E4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, T1 arg1, string arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in carg2);
			return result;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00013740 File Offset: 0x00011940
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, string arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in arg1, in carg2, in carg3);
			return result;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001378C File Offset: 0x0001198C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, int arg1, T1 arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000137E8 File Offset: 0x000119E8
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, int arg1, T1 arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00013848 File Offset: 0x00011A48
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, int arg1, T1 arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x000138A4 File Offset: 0x00011AA4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, int arg1, T2 arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x000138F0 File Offset: 0x00011AF0
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, float arg1, T1 arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00013950 File Offset: 0x00011B50
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, float arg1, T1 arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x000139B0 File Offset: 0x00011BB0
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, float arg1, T1 arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00013A10 File Offset: 0x00011C10
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, float arg1, T2 arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00013A60 File Offset: 0x00011C60
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, string arg1, T1 arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00013ABC File Offset: 0x00011CBC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, string arg1, T1 arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00013B1C File Offset: 0x00011D1C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, string arg1, T1 arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00013B78 File Offset: 0x00011D78
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, string arg1, T2 arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in arg2, in carg2);
			return result;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00013BC4 File Offset: 0x00011DC4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, int arg0, T1 arg1, T2 arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in arg2, in carg);
			return result;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00013C10 File Offset: 0x00011E10
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, float arg0, T1 arg1, T2 arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in arg2, in carg);
			return result;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00013C60 File Offset: 0x00011E60
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, string arg0, T1 arg1, T2 arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg3);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in arg2, in carg);
			return result;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00013CAC File Offset: 0x00011EAC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3>(FixedString512Bytes formatString, T1 arg0, T2 arg1, T3 arg2, string arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T3 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg3 = default(FixedString32Bytes);
			(ref carg3).Append(arg3);
			(ref result).AppendFormat(in formatString, in arg0, in arg1, in arg2, in carg3);
			return result;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00013CE8 File Offset: 0x00011EE8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, int arg1, int arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00013D44 File Offset: 0x00011F44
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, int arg1, int arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00013DA4 File Offset: 0x00011FA4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, int arg1, int arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00013E00 File Offset: 0x00012000
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, int arg1, int arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00013E4C File Offset: 0x0001204C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, float arg1, int arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00013EAC File Offset: 0x000120AC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, float arg1, int arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00013F0C File Offset: 0x0001210C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, float arg1, int arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00013F6C File Offset: 0x0001216C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, float arg1, int arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00013FB8 File Offset: 0x000121B8
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, string arg1, int arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00014014 File Offset: 0x00012214
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, string arg1, int arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00014074 File Offset: 0x00012274
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, string arg1, int arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x000140D0 File Offset: 0x000122D0
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, string arg1, int arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001411C File Offset: 0x0001231C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, int arg0, T1 arg1, int arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in arg3);
			return result;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00014168 File Offset: 0x00012368
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, float arg0, T1 arg1, int arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in arg3);
			return result;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x000141B4 File Offset: 0x000123B4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, string arg0, T1 arg1, int arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in arg3);
			return result;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00014200 File Offset: 0x00012400
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3>(FixedString512Bytes formatString, T1 arg0, T2 arg1, int arg2, T3 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T3 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in arg0, in arg1, in carg2, in arg3);
			return result;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001423C File Offset: 0x0001243C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, int arg1, float arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001429C File Offset: 0x0001249C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, int arg1, float arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x000142FC File Offset: 0x000124FC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, int arg1, float arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001435C File Offset: 0x0001255C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, int arg1, float arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x000143A8 File Offset: 0x000125A8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, float arg1, float arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00014408 File Offset: 0x00012608
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, float arg1, float arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001446C File Offset: 0x0001266C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, float arg1, float arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x000144CC File Offset: 0x000126CC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, float arg1, float arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001451C File Offset: 0x0001271C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, string arg1, float arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001457C File Offset: 0x0001277C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, string arg1, float arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x000145DC File Offset: 0x000127DC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, string arg1, float arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001463C File Offset: 0x0001283C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, string arg1, float arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00014688 File Offset: 0x00012888
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, int arg0, T1 arg1, float arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in arg3);
			return result;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x000146D4 File Offset: 0x000128D4
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, float arg0, T1 arg1, float arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in arg3);
			return result;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00014724 File Offset: 0x00012924
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, string arg0, T1 arg1, float arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in arg3);
			return result;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00014770 File Offset: 0x00012970
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3>(FixedString512Bytes formatString, T1 arg0, T2 arg1, float arg2, T3 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T3 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in arg0, in arg1, in carg2, in arg3);
			return result;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x000147AC File Offset: 0x000129AC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, int arg1, string arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00014808 File Offset: 0x00012A08
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, int arg1, string arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00014868 File Offset: 0x00012A68
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, int arg1, string arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x000148C4 File Offset: 0x00012AC4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, int arg1, string arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00014910 File Offset: 0x00012B10
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, float arg1, string arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00014970 File Offset: 0x00012B70
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, float arg1, string arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000149D0 File Offset: 0x00012BD0
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, float arg1, string arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00014A30 File Offset: 0x00012C30
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, float arg1, string arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00014A7C File Offset: 0x00012C7C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, int arg0, string arg1, string arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00014AD8 File Offset: 0x00012CD8
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, float arg0, string arg1, string arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00014B38 File Offset: 0x00012D38
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString512Bytes formatString, string arg0, string arg1, string arg2, T1 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00014B94 File Offset: 0x00012D94
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, T1 arg0, string arg1, string arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2, in arg3);
			return result;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00014BE0 File Offset: 0x00012DE0
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, int arg0, T1 arg1, string arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in arg3);
			return result;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00014C2C File Offset: 0x00012E2C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, float arg0, T1 arg1, string arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in arg3);
			return result;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00014C78 File Offset: 0x00012E78
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, string arg0, T1 arg1, string arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg, in arg3);
			return result;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00014CC4 File Offset: 0x00012EC4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3>(FixedString512Bytes formatString, T1 arg0, T2 arg1, string arg2, T3 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T3 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in arg0, in arg1, in carg2, in arg3);
			return result;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00014D00 File Offset: 0x00012F00
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, int arg0, int arg1, T1 arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in arg3);
			return result;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00014D4C File Offset: 0x00012F4C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, float arg0, int arg1, T1 arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in arg3);
			return result;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00014D98 File Offset: 0x00012F98
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, string arg0, int arg1, T1 arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in arg3);
			return result;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00014DE4 File Offset: 0x00012FE4
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3>(FixedString512Bytes formatString, T1 arg0, int arg1, T2 arg2, T3 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T3 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in arg2, in arg3);
			return result;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00014E20 File Offset: 0x00013020
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, int arg0, float arg1, T1 arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in arg3);
			return result;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00014E6C File Offset: 0x0001306C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, float arg0, float arg1, T1 arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in arg3);
			return result;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00014EBC File Offset: 0x000130BC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, string arg0, float arg1, T1 arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in arg3);
			return result;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00014F08 File Offset: 0x00013108
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3>(FixedString512Bytes formatString, T1 arg0, float arg1, T2 arg2, T3 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T3 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			(ref result).AppendFormat(in formatString, in arg0, in carg, in arg2, in arg3);
			return result;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00014F44 File Offset: 0x00013144
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, int arg0, string arg1, T1 arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in arg3);
			return result;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00014F90 File Offset: 0x00013190
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, float arg0, string arg1, T1 arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in arg3);
			return result;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00014FDC File Offset: 0x000131DC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString512Bytes formatString, string arg0, string arg1, T1 arg2, T2 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2, in arg3);
			return result;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00015028 File Offset: 0x00013228
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3>(FixedString512Bytes formatString, T1 arg0, string arg1, T2 arg2, T3 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T3 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in arg2, in arg3);
			return result;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00015064 File Offset: 0x00013264
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3>(FixedString512Bytes formatString, int arg0, T1 arg1, T2 arg2, T3 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T3 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in arg2, in arg3);
			return result;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x000150A0 File Offset: 0x000132A0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3>(FixedString512Bytes formatString, float arg0, T1 arg1, T2 arg2, T3 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T3 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in arg2, in arg3);
			return result;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x000150DC File Offset: 0x000132DC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3>(FixedString512Bytes formatString, string arg0, T1 arg1, T2 arg2, T3 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T3 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in arg2, in arg3);
			return result;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00015118 File Offset: 0x00013318
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3, [global::System.Runtime.CompilerServices.IsUnmanaged] T4>(FixedString512Bytes formatString, T1 arg0, T2 arg1, T3 arg2, T4 arg3) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T3 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T4 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			(ref result).AppendFormat(in formatString, in arg0, in arg1, in arg2, in arg3);
			return result;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00015140 File Offset: 0x00013340
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, int arg1, int arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001519C File Offset: 0x0001339C
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, int arg1, int arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000151F8 File Offset: 0x000133F8
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, int arg1, int arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00015254 File Offset: 0x00013454
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, T1 arg0, int arg1, int arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001529C File Offset: 0x0001349C
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, float arg1, int arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x000152F8 File Offset: 0x000134F8
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, float arg1, int arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00015358 File Offset: 0x00013558
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, float arg1, int arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x000153B4 File Offset: 0x000135B4
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, T1 arg0, float arg1, int arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00015400 File Offset: 0x00013600
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, string arg1, int arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001545C File Offset: 0x0001365C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, string arg1, int arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x000154B8 File Offset: 0x000136B8
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, string arg1, int arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00015514 File Offset: 0x00013714
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, T1 arg0, string arg1, int arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2);
			return result;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001555C File Offset: 0x0001375C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, int arg0, T1 arg1, int arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg);
			return result;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x000155A4 File Offset: 0x000137A4
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, float arg0, T1 arg1, int arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg);
			return result;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x000155F0 File Offset: 0x000137F0
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, string arg0, T1 arg1, int arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg);
			return result;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00015638 File Offset: 0x00013838
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString128Bytes formatString, T1 arg0, T2 arg1, int arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in arg0, in arg1, in carg2);
			return result;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00015670 File Offset: 0x00013870
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, int arg1, float arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x000156CC File Offset: 0x000138CC
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, int arg1, float arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001572C File Offset: 0x0001392C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, int arg1, float arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00015788 File Offset: 0x00013988
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, T1 arg0, int arg1, float arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x000157D4 File Offset: 0x000139D4
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, float arg1, float arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00015834 File Offset: 0x00013A34
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, float arg1, float arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00015894 File Offset: 0x00013A94
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, float arg1, float arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x000158F4 File Offset: 0x00013AF4
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, T1 arg0, float arg1, float arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00015940 File Offset: 0x00013B40
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, string arg1, float arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001599C File Offset: 0x00013B9C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, string arg1, float arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x000159FC File Offset: 0x00013BFC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, string arg1, float arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00015A58 File Offset: 0x00013C58
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, T1 arg0, string arg1, float arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2);
			return result;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00015AA4 File Offset: 0x00013CA4
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, int arg0, T1 arg1, float arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg);
			return result;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00015AF0 File Offset: 0x00013CF0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, float arg0, T1 arg1, float arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg);
			return result;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00015B3C File Offset: 0x00013D3C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, string arg0, T1 arg1, float arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg);
			return result;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00015B88 File Offset: 0x00013D88
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString128Bytes formatString, T1 arg0, T2 arg1, float arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2, '.');
			(ref result).AppendFormat(in formatString, in arg0, in arg1, in carg2);
			return result;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00015BC4 File Offset: 0x00013DC4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, int arg1, string arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00015C20 File Offset: 0x00013E20
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, int arg1, string arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00015C7C File Offset: 0x00013E7C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, int arg1, string arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00015CD8 File Offset: 0x00013ED8
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, T1 arg0, int arg1, string arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00015D20 File Offset: 0x00013F20
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, float arg1, string arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00015D7C File Offset: 0x00013F7C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, float arg1, string arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00015DDC File Offset: 0x00013FDC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, float arg1, string arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00015E38 File Offset: 0x00014038
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, T1 arg0, float arg1, string arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00015E84 File Offset: 0x00014084
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, string arg1, string arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00015EE0 File Offset: 0x000140E0
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, string arg1, string arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00015F3C File Offset: 0x0001413C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, string arg1, string arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in carg2);
			return result;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00015F98 File Offset: 0x00014198
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, T1 arg0, string arg1, string arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in carg2);
			return result;
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00015FE0 File Offset: 0x000141E0
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, int arg0, T1 arg1, string arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg);
			return result;
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00016028 File Offset: 0x00014228
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, float arg0, T1 arg1, string arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg);
			return result;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00016074 File Offset: 0x00014274
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, string arg0, T1 arg1, string arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg2);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in carg);
			return result;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x000160BC File Offset: 0x000142BC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString128Bytes formatString, T1 arg0, T2 arg1, string arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg2 = default(FixedString32Bytes);
			(ref carg2).Append(arg2);
			(ref result).AppendFormat(in formatString, in arg0, in arg1, in carg2);
			return result;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x000160F4 File Offset: 0x000142F4
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, int arg0, int arg1, T1 arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2);
			return result;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001613C File Offset: 0x0001433C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, float arg0, int arg1, T1 arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2);
			return result;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00016188 File Offset: 0x00014388
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, string arg0, int arg1, T1 arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2);
			return result;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x000161D0 File Offset: 0x000143D0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString128Bytes formatString, T1 arg0, int arg1, T2 arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in arg2);
			return result;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00016208 File Offset: 0x00014408
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, int arg0, float arg1, T1 arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2);
			return result;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00016254 File Offset: 0x00014454
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, float arg0, float arg1, T1 arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2);
			return result;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x000162A0 File Offset: 0x000144A0
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, string arg0, float arg1, T1 arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2);
			return result;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x000162EC File Offset: 0x000144EC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString128Bytes formatString, T1 arg0, float arg1, T2 arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			(ref result).AppendFormat(in formatString, in arg0, in carg, in arg2);
			return result;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00016328 File Offset: 0x00014528
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, int arg0, string arg1, T1 arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2);
			return result;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00016370 File Offset: 0x00014570
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, float arg0, string arg1, T1 arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2);
			return result;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x000163BC File Offset: 0x000145BC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, string arg0, string arg1, T1 arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in carg0, in carg, in arg2);
			return result;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00016404 File Offset: 0x00014604
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString128Bytes formatString, T1 arg0, string arg1, T2 arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in arg0, in carg, in arg2);
			return result;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001643C File Offset: 0x0001463C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString128Bytes formatString, int arg0, T1 arg1, T2 arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in arg2);
			return result;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00016474 File Offset: 0x00014674
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString128Bytes formatString, float arg0, T1 arg1, T2 arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in arg2);
			return result;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x000164B0 File Offset: 0x000146B0
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString128Bytes formatString, string arg0, T1 arg1, T2 arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			(ref result).AppendFormat(in formatString, in carg0, in arg1, in arg2);
			return result;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x000164E8 File Offset: 0x000146E8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3>(FixedString128Bytes formatString, T1 arg0, T2 arg1, T3 arg2) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T3 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			(ref result).AppendFormat(in formatString, in arg0, in arg1, in arg2);
			return result;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00016510 File Offset: 0x00014710
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, int arg1)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in carg0, in carg);
			return result;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00016558 File Offset: 0x00014758
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, int arg1)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in carg0, in carg);
			return result;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x000165A0 File Offset: 0x000147A0
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, int arg1)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in carg0, in carg);
			return result;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x000165E8 File Offset: 0x000147E8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, T1 arg0, int arg1) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in arg0, in carg);
			return result;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00016620 File Offset: 0x00014820
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, float arg1)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg);
			return result;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00016668 File Offset: 0x00014868
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, float arg1)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg);
			return result;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x000166B4 File Offset: 0x000148B4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, float arg1)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			(ref result).AppendFormat(in formatString, in carg0, in carg);
			return result;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x000166FC File Offset: 0x000148FC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, T1 arg0, float arg1) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1, '.');
			(ref result).AppendFormat(in formatString, in arg0, in carg);
			return result;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00016734 File Offset: 0x00014934
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, string arg1)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in carg0, in carg);
			return result;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001677C File Offset: 0x0001497C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, string arg1)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in carg0, in carg);
			return result;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x000167C4 File Offset: 0x000149C4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, string arg1)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in carg0, in carg);
			return result;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001680C File Offset: 0x00014A0C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, T1 arg0, string arg1) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg = default(FixedString32Bytes);
			(ref carg).Append(arg1);
			(ref result).AppendFormat(in formatString, in arg0, in carg);
			return result;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00016844 File Offset: 0x00014A44
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, int arg0, T1 arg1) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			(ref result).AppendFormat(in formatString, in carg0, in arg1);
			return result;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001687C File Offset: 0x00014A7C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, float arg0, T1 arg1) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			(ref result).AppendFormat(in formatString, in carg0, in arg1);
			return result;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x000168B4 File Offset: 0x00014AB4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, string arg0, T1 arg1) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			(ref result).AppendFormat(in formatString, in carg0, in arg1);
			return result;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x000168EC File Offset: 0x00014AEC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(FixedString128Bytes formatString, T1 arg0, T2 arg1) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			(ref result).AppendFormat(in formatString, in arg0, in arg1);
			return result;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00016910 File Offset: 0x00014B10
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			(ref result).AppendFormat(in formatString, in carg0);
			return result;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00016944 File Offset: 0x00014B44
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0, '.');
			(ref result).AppendFormat(in formatString, in carg0);
			return result;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001697C File Offset: 0x00014B7C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes carg0 = default(FixedString32Bytes);
			(ref carg0).Append(arg0);
			(ref result).AppendFormat(in formatString, in carg0);
			return result;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x000169B0 File Offset: 0x00014BB0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<[global::System.Runtime.CompilerServices.IsUnmanaged] T1>(FixedString128Bytes formatString, T1 arg0) where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			(ref result).AppendFormat(in formatString, in arg0);
			return result;
		}
	}
}
