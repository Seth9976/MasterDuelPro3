using System;
using System.Collections.Generic;
using MDPro3.Duel.YGOSharp;

namespace MDPro3.Net
{
	// Token: 0x02001310 RID: 4880
	public class DeckShareURL
	{
		// Token: 0x06008ED1 RID: 36561 RVA: 0x001320FC File Offset: 0x001302FC
		public static Uri DeckToUri(List<int> main, List<int> extra, List<int> side, Dictionary<string, string> parameters = null)
		{
			UriBuilder builder = new UriBuilder(DeckShareURL.URL_SCHEME_HTTP, DeckShareURL.URL_HOST_DECK);
			if (parameters != null)
			{
				foreach (KeyValuePair<string, string> entry in parameters)
				{
					builder.Query = ((builder.Query.Length > 1) ? string.Concat(new string[]
					{
						builder.Query.Substring(1),
						"&",
						entry.Key,
						"=",
						Uri.EscapeDataString(entry.Value)
					}) : (entry.Key + "=" + entry.Value));
				}
			}
			UriBuilder uriBuilder = builder;
			uriBuilder.Query = uriBuilder.Query + DeckShareURL.QUERY_YGO_TYPE + "=" + DeckShareURL.ARG_DECK;
			UriBuilder uriBuilder2 = builder;
			uriBuilder2.Query = uriBuilder2.Query + "&" + DeckShareURL.QUERY_VERSION + "=1";
			int mNum = DeckShareURL.GetTypeNum(main);
			int eNum = DeckShareURL.GetTypeNum(extra);
			int sNum = DeckShareURL.GetTypeNum(side);
			string deck = DeckShareURL.ToBit(main, extra, side, mNum, eNum, sNum);
			string text = Convert.ToString(mNum, 2);
			string e = Convert.ToString(eNum, 2);
			string s = Convert.ToString(sNum, 2);
			string text2 = DeckShareURL.ToNumLength(text, 8);
			e = DeckShareURL.ToNumLength(e, 4);
			s = DeckShareURL.ToNumLength(s, 4);
			deck = text2 + e + s + deck;
			string message = Convert.ToBase64String(DeckShareURL.ToBytes(deck)).Replace('+', '-').Replace('/', '_')
				.TrimEnd('=');
			UriBuilder uriBuilder3 = builder;
			uriBuilder3.Query = string.Concat(new string[]
			{
				uriBuilder3.Query,
				"&",
				DeckShareURL.QUERY_DECK,
				"=",
				Uri.EscapeDataString(message)
			});
			return builder.Uri;
		}

		// Token: 0x06008ED2 RID: 36562 RVA: 0x001322E0 File Offset: 0x001304E0
		private static string ToBit(List<int> main, List<int> extra, List<int> side, int mNum, int eNum, int sNum)
		{
			string text = DeckShareURL.ToByte(main, mNum);
			string extras = DeckShareURL.ToByte(extra, eNum);
			string sides = DeckShareURL.ToByte(side, sNum);
			return text + extras + sides;
		}

		// Token: 0x06008ED3 RID: 36563 RVA: 0x00132310 File Offset: 0x00130510
		private static string ToByte(List<int> ids, int typeNum)
		{
			string bytes = string.Empty;
			if (ids == null)
			{
				return bytes;
			}
			for (int i = 0; i < ids.Count; i++)
			{
				int id = ids[i];
				if (id > 0)
				{
					string idB = DeckShareURL.ToB(id);
					if (i != ids.Count - 1)
					{
						int num = ids[i + 1];
						int tNum = 1;
						if (num == id)
						{
							tNum++;
							if (i != ids.Count - 2 && ids[i + 2] == id)
							{
								tNum++;
								i++;
							}
							i++;
						}
						switch (Math.Min(3, tNum))
						{
						case 1:
							idB = "01" + idB;
							break;
						case 2:
							idB = "10" + idB;
							break;
						case 3:
							idB = "11" + idB;
							break;
						}
					}
					else
					{
						idB = "01" + idB;
					}
					bytes += idB;
				}
			}
			return bytes;
		}

		// Token: 0x06008ED4 RID: 36564 RVA: 0x001323FD File Offset: 0x001305FD
		private static string ToB(int id)
		{
			return DeckShareURL.ToNumLength(Convert.ToString(id, 2), 27);
		}

		// Token: 0x06008ED5 RID: 36565 RVA: 0x0013240D File Offset: 0x0013060D
		private static string ToNumLength(string message, int num)
		{
			while (message.Length < num)
			{
				message = "0" + message;
			}
			return message;
		}

		// Token: 0x06008ED6 RID: 36566 RVA: 0x00132428 File Offset: 0x00130628
		private static byte[] ToBytes(string bits)
		{
			int y = bits.Length % 8;
			if (y != 0)
			{
				bits = DeckShareURL.ToNumLengthLast(bits, bits.Length + 8 - y);
			}
			byte[] bytes = new byte[bits.Length / 8];
			for (int i = 0; i < bits.Length / 8; i++)
			{
				bytes[i] = (byte)Convert.ToInt32(bits.Substring(i * 8, 8), 2);
			}
			return bytes;
		}

		// Token: 0x06008ED7 RID: 36567 RVA: 0x00132489 File Offset: 0x00130689
		private static string ToNumLengthLast(string message, int num)
		{
			while (message.Length < num)
			{
				message += "0";
			}
			return message;
		}

		// Token: 0x06008ED8 RID: 36568 RVA: 0x001324A4 File Offset: 0x001306A4
		private static int GetTypeNum(List<int> ids)
		{
			int num = 0;
			for (int i = 0; i < ids.Count; i++)
			{
				int id = ids[i];
				if (id > 0)
				{
					num++;
					if (i != ids.Count - 1 && ids[i + 1] == id)
					{
						if (i != ids.Count - 2 && ids[i + 2] == id)
						{
							i++;
						}
						i++;
					}
				}
			}
			return num;
		}

		// Token: 0x06008ED9 RID: 36569 RVA: 0x0013250C File Offset: 0x0013070C
		public static Deck UriToDeck(Uri uri)
		{
			if (!uri.Host.Equals(DeckShareURL.URL_HOST_DECK) || !uri.Query.Contains(DeckShareURL.QUERY_DECK + "="))
			{
				throw new ArgumentException("Invalid URI format or host");
			}
			string encodedDeck = DeckShareURL.GetValueFromQuery(DeckShareURL.ParseQueryParameters(uri.Query), DeckShareURL.QUERY_DECK, string.Empty);
			encodedDeck = encodedDeck.Replace('-', '+').Replace('_', '/');
			byte[] deckBytes = Convert.FromBase64String(encodedDeck.PadRight(encodedDeck.Length + (4 - encodedDeck.Length % 4) % 4, '='));
			string deckBits = "";
			for (int i = 0; i < deckBytes.Length; i++)
			{
				deckBits += Convert.ToString(deckBytes[i], 2).PadLeft(8, '0');
			}
			int totalMainLength = 29 * DeckShareURL.GetBinaryIntValue(deckBits.Substring(0, 8));
			int totalExtraLength = 29 * DeckShareURL.GetBinaryIntValue(deckBits.Substring(8, 4));
			int totalSideLength = 29 * DeckShareURL.GetBinaryIntValue(deckBits.Substring(12, 4));
			int startIndex = 16;
			List<int> list = DeckShareURL.DecodeCardList(deckBits.Substring(startIndex, totalMainLength));
			startIndex += totalMainLength;
			List<int> extra = DeckShareURL.DecodeCardList(deckBits.Substring(startIndex, totalExtraLength));
			startIndex += totalExtraLength;
			List<int> side = DeckShareURL.DecodeCardList(deckBits.Substring(startIndex, totalSideLength));
			return new Deck(list, extra, side);
		}

		// Token: 0x06008EDA RID: 36570 RVA: 0x00132654 File Offset: 0x00130854
		private static Dictionary<string, string> ParseQueryParameters(string query)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();
			string[] array = query.Split('&', StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				string[] keyValue = array[i].Split('=', StringSplitOptions.None);
				if (keyValue.Length == 2)
				{
					result[keyValue[0]] = Uri.UnescapeDataString(keyValue[1]);
				}
			}
			return result;
		}

		// Token: 0x06008EDB RID: 36571 RVA: 0x001326A3 File Offset: 0x001308A3
		private static int GetBinaryIntValue(string binaryString)
		{
			return Convert.ToInt32(binaryString, 2);
		}

		// Token: 0x06008EDC RID: 36572 RVA: 0x001326AC File Offset: 0x001308AC
		private static List<int> DecodeCardList(string bitString)
		{
			List<int> cardIds = new List<int>();
			int currentIndex = 0;
			while (currentIndex < bitString.Length)
			{
				int count;
				int currentId = DeckShareURL.GetCardId(bitString, ref currentIndex, out count);
				for (int i = 0; i < count; i++)
				{
					cardIds.Add(currentId);
				}
			}
			return cardIds;
		}

		// Token: 0x06008EDD RID: 36573 RVA: 0x001326F0 File Offset: 0x001308F0
		private static int GetCardId(string bitString, ref int currentIndex, out int count)
		{
			count = 1;
			if (bitString.Length - currentIndex >= 2)
			{
				string text = bitString.Substring(currentIndex, 2);
				if (!(text == "01"))
				{
					if (!(text == "10"))
					{
						if (text == "11")
						{
							count = 3;
						}
					}
					else
					{
						count = 2;
					}
				}
				else
				{
					count = 1;
				}
				currentIndex += 2;
			}
			int num = Convert.ToInt32(bitString.Substring(currentIndex, 27), 2);
			currentIndex += 27;
			return num;
		}

		// Token: 0x06008EDE RID: 36574 RVA: 0x0013276C File Offset: 0x0013096C
		private static string GetValueFromQuery(Dictionary<string, string> queryParameters, string key, string defaultValue = "")
		{
			string value;
			if (queryParameters.TryGetValue(key, out value))
			{
				return value;
			}
			return defaultValue;
		}

		// Token: 0x0400CC85 RID: 52357
		private static readonly string URL_SCHEME_HTTP = "http";

		// Token: 0x0400CC86 RID: 52358
		private static readonly string URL_HOST_DECK = "deck.ourygo.top";

		// Token: 0x0400CC87 RID: 52359
		private static readonly string ARG_DECK = "deck";

		// Token: 0x0400CC88 RID: 52360
		private static readonly string QUERY_VERSION = "v";

		// Token: 0x0400CC89 RID: 52361
		private static readonly string QUERY_YGO_TYPE = "ygotype";

		// Token: 0x0400CC8A RID: 52362
		private static readonly string QUERY_DECK = "d";
	}
}
