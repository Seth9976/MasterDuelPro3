using System;
using System.Collections;
using System.Collections.Generic;

namespace YgomGame.Stats
{
	// Token: 0x020008E9 RID: 2281
	public class CardStats
	{
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060042D2 RID: 17106 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardStats Instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create()
		{
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Destroy()
		{
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Reload()
		{
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator LoadStatsDataAsync()
		{
			return null;
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsAvailable()
		{
			return false;
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsUseRawData()
		{
			return false;
		}

		// Token: 0x060042DB RID: 17115 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetItemString(int Item)
		{
			return null;
		}

		// Token: 0x060042DC RID: 17116 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetItemUnitString(int Item, double fValue)
		{
			return null;
		}

		// Token: 0x060042DD RID: 17117 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetItemSortType(int Item)
		{
			return null;
		}

		// Token: 0x060042DE RID: 17118 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<double> GetItemUnitThreshold(int Item)
		{
			return null;
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<CardStatsData> GetCardStatsData(int mrk)
		{
			return null;
		}

		// Token: 0x060042E0 RID: 17120 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DebugString(int mrk)
		{
		}

		// Token: 0x04008144 RID: 33092
		private static CardStats s_instance;

		// Token: 0x04008145 RID: 33093
		private const string CARD_STATS_DATA_PATH = "Stats/CardStats";
	}
}
