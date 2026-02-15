using System;
using System.Collections.Generic;
using YgomGame.TextIDs;

namespace YgomGame.Card
{
	// Token: 0x02001126 RID: 4390
	public class LootSourceInfo
	{
		// Token: 0x060082CC RID: 33484 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> GetLootSource()
		{
			return null;
		}

		// Token: 0x060082CD RID: 33485 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetLootCategoryID(Dictionary<string, object> dic)
		{
			return 0;
		}

		// Token: 0x060082CE RID: 33486 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetLootParam(Dictionary<string, object> dic)
		{
			return 0;
		}

		// Token: 0x060082CF RID: 33487 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetLootIconType(Dictionary<string, object> dic)
		{
			return 0;
		}

		// Token: 0x060082D0 RID: 33488 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetLootIconData(Dictionary<string, object> dic)
		{
			return null;
		}

		// Token: 0x060082D1 RID: 33489 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsLootAvailable(Dictionary<string, object> dic)
		{
			return false;
		}

		// Token: 0x060082D2 RID: 33490 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetLootSourceString(Dictionary<string, object> dic)
		{
			return null;
		}

		// Token: 0x0400BDDD RID: 48605
		public static Dictionary<LootSourceInfo.LootCategory, IDS_DECKEDIT> CategoryTextTbl;

		// Token: 0x0400BDDE RID: 48606
		private const string CLIENTWORK_PATH_ROUTE = "$.Route";

		// Token: 0x0400BDDF RID: 48607
		private const string KEY_CATEGORY = "route_category";

		// Token: 0x0400BDE0 RID: 48608
		private const string KEY_PARAM = "route_param";

		// Token: 0x0400BDE1 RID: 48609
		private const string KEY_OPEN = "route_open";

		// Token: 0x0400BDE2 RID: 48610
		private const string KEY_NAMEID = "route_name_id";

		// Token: 0x0400BDE3 RID: 48611
		private const string KEY_ICONTYPE = "route_icon_type";

		// Token: 0x0400BDE4 RID: 48612
		private const string KEY_ICONDATA = "route_icon_data";

		// Token: 0x02001127 RID: 4391
		public enum LootCategory
		{
			// Token: 0x0400BDE6 RID: 48614
			Pack = 1,
			// Token: 0x0400BDE7 RID: 48615
			Solo,
			// Token: 0x0400BDE8 RID: 48616
			Tournament,
			// Token: 0x0400BDE9 RID: 48617
			Exhibition,
			// Token: 0x0400BDEA RID: 48618
			FreeStruct,
			// Token: 0x0400BDEB RID: 48619
			PaidStruct,
			// Token: 0x0400BDEC RID: 48620
			Mission,
			// Token: 0x0400BDED RID: 48621
			DuelReward,
			// Token: 0x0400BDEE RID: 48622
			Set,
			// Token: 0x0400BDEF RID: 48623
			Etc = 99
		}
	}
}
