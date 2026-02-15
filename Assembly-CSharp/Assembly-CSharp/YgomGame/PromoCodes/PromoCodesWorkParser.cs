using System;
using System.Collections.Generic;

namespace YgomGame.PromoCodes
{
	// Token: 0x02000A16 RID: 2582
	public static class PromoCodesWorkParser
	{
		// Token: 0x06004AEA RID: 19178 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ParsePromoCodesId(this Dictionary<string, object> data)
		{
			return 0;
		}

		// Token: 0x06004AEB RID: 19179 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ParseInputLength(this Dictionary<string, object> data)
		{
			return 0;
		}

		// Token: 0x06004AEC RID: 19180 RVA: 0x000029CC File Offset: 0x00000BCC
		public static PromoCodeFormat ParseInputFormat(this Dictionary<string, object> data)
		{
			return PromoCodeFormat.Standard;
		}

		// Token: 0x06004AED RID: 19181 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ParseCompleted(this Dictionary<string, object> data)
		{
			return false;
		}

		// Token: 0x06004AEE RID: 19182 RVA: 0x0000216A File Offset: 0x0000036A
		public static string ParseC(this Dictionary<string, object> data)
		{
			return null;
		}

		// Token: 0x06004AEF RID: 19183 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> ParseResult_Rewards(this Dictionary<string, object> result)
		{
			return null;
		}

		// Token: 0x06004AF0 RID: 19184 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ParseResult_IsSendPresent(this Dictionary<string, object> result)
		{
			return false;
		}

		// Token: 0x06004AF1 RID: 19185 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ParseResult_Reward_IsPeriod(this Dictionary<string, object> result)
		{
			return false;
		}

		// Token: 0x06004AF2 RID: 19186 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ParseResult_Reward_ItemCategory(this Dictionary<string, object> result)
		{
			return 0;
		}

		// Token: 0x06004AF3 RID: 19187 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ParseResult_Reward_ItemId(this Dictionary<string, object> result)
		{
			return 0;
		}

		// Token: 0x06004AF4 RID: 19188 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ParseResult_Reward_Num(this Dictionary<string, object> result)
		{
			return 0;
		}

		// Token: 0x06004AF5 RID: 19189 RVA: 0x000029CC File Offset: 0x00000BCC
		public static OnErrorBehaviour ParseResult_OnErrorBehaviour(this Dictionary<string, object> result)
		{
			return OnErrorBehaviour.None;
		}
	}
}
