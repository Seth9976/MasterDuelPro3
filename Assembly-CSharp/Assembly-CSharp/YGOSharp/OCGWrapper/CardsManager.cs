using System;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;

namespace YGOSharp.OCGWrapper
{
	// Token: 0x020001C9 RID: 457
	internal static class CardsManager
	{
		// Token: 0x06000803 RID: 2051 RVA: 0x00025C30 File Offset: 0x00023E30
		internal static void Init(string databaseFullPath)
		{
			CardsManager._cards = new Dictionary<int, Card>();
			using (SqliteConnection connection = new SqliteConnection("Data Source=" + databaseFullPath))
			{
				connection.Open();
				using (IDbCommand command = new SqliteCommand("SELECT id, ot, alias, setcode, type, level, race, attribute, atk, def FROM datas", connection))
				{
					using (IDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							CardsManager.LoadCard(reader);
						}
					}
				}
			}
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00025CCC File Offset: 0x00023ECC
		internal static Card GetCard(int id)
		{
			if (CardsManager._cards.ContainsKey(id))
			{
				return CardsManager._cards[id];
			}
			return null;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00025CE8 File Offset: 0x00023EE8
		private static void LoadCard(IDataRecord reader)
		{
			Card card = new Card(reader);
			CardsManager._cards.Add(card.Id, card);
		}

		// Token: 0x04000BC1 RID: 3009
		private static IDictionary<int, Card> _cards;
	}
}
