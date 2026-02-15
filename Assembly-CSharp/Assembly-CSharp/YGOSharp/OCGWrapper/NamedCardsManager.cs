using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;

namespace YGOSharp.OCGWrapper
{
	// Token: 0x020001CD RID: 461
	public static class NamedCardsManager
	{
		// Token: 0x0600082A RID: 2090 RVA: 0x0002633C File Offset: 0x0002453C
		public static void Init(string databaseFullPath)
		{
			try
			{
				if (!File.Exists(databaseFullPath))
				{
					throw new Exception("Could not find the cards database.");
				}
				NamedCardsManager._cards = new Dictionary<int, NamedCard>();
				using (SqliteConnection connection = new SqliteConnection("Data Source=" + databaseFullPath))
				{
					connection.Open();
					using (IDbCommand command = new SqliteCommand("SELECT datas.id, ot, alias, setcode, type, level, race, attribute, atk, def, texts.name, texts.desc FROM datas INNER JOIN texts ON datas.id = texts.id", connection))
					{
						using (IDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								NamedCardsManager.LoadCard(reader);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Could not initialize the cards database. Check the inner exception for more details.", ex);
			}
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00026408 File Offset: 0x00024608
		public static void InitForMulti(List<string> databaseFullPaths)
		{
			try
			{
				NamedCardsManager._cards = new Dictionary<int, NamedCard>();
				foreach (string databaseFullPath in databaseFullPaths)
				{
					if (File.Exists(databaseFullPath))
					{
						using (SqliteConnection connection = new SqliteConnection("Data Source=" + databaseFullPath))
						{
							connection.Open();
							using (IDbCommand command = new SqliteCommand("SELECT datas.id, ot, alias, setcode, type, level, race, attribute, atk, def, texts.name, texts.desc FROM datas INNER JOIN texts ON datas.id = texts.id", connection))
							{
								using (IDataReader reader = command.ExecuteReader())
								{
									while (reader.Read())
									{
										NamedCardsManager.LoadCard(reader);
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Could not initialize the cards database. Check the inner exception for more details.", ex);
			}
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00026508 File Offset: 0x00024708
		internal static NamedCard GetCard(int id)
		{
			if (NamedCardsManager._cards.ContainsKey(id))
			{
				return NamedCardsManager._cards[id];
			}
			return null;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00026524 File Offset: 0x00024724
		internal static IList<NamedCard> GetAllCards()
		{
			List<NamedCard> returnValue = new List<NamedCard>();
			foreach (NamedCard card in NamedCardsManager._cards.Values)
			{
				returnValue.Add(card);
			}
			return returnValue;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0002657C File Offset: 0x0002477C
		private static void LoadCard(IDataRecord reader)
		{
			NamedCard card = new NamedCard(reader);
			NamedCardsManager._cards[card.Id] = card;
		}

		// Token: 0x04000BCE RID: 3022
		private static IDictionary<int, NamedCard> _cards;
	}
}
