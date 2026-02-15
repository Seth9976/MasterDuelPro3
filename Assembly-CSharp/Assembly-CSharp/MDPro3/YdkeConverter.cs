using System;
using System.Collections.Generic;
using MDPro3.Duel.YGOSharp;

namespace MDPro3
{
	// Token: 0x02001257 RID: 4695
	public static class YdkeConverter
	{
		// Token: 0x06008A4B RID: 35403 RVA: 0x001125B8 File Offset: 0x001107B8
		public static Deck Ydke2Deck(string ydkeString)
		{
			ydkeString = ydkeString.Replace(YdkeConverter.ydkeHeader, string.Empty);
			string[] sections = ydkeString.Split('!', StringSplitOptions.None);
			if (sections.Length < 3)
			{
				return null;
			}
			return new Deck
			{
				Main = YdkeConverter.DecodeSection(sections[0]),
				Extra = YdkeConverter.DecodeSection(sections[1]),
				Side = YdkeConverter.DecodeSection(sections[2])
			};
		}

		// Token: 0x06008A4C RID: 35404 RVA: 0x00112618 File Offset: 0x00110818
		private static List<int> DecodeSection(string base64Section)
		{
			if (string.IsNullOrEmpty(base64Section))
			{
				return new List<int>();
			}
			byte[] decodedBytes = Convert.FromBase64String(base64Section);
			List<int> cardIds = new List<int>();
			int i = 0;
			while (i < decodedBytes.Length && i + 4 <= decodedBytes.Length)
			{
				uint cardId = BitConverter.ToUInt32(decodedBytes, i);
				cardIds.Add((int)cardId);
				i += 4;
			}
			return cardIds;
		}

		// Token: 0x06008A4D RID: 35405 RVA: 0x00112668 File Offset: 0x00110868
		public static string DeckToYdke(Deck deck)
		{
			if (deck.Main == null)
			{
				deck.Main = new List<int>();
			}
			if (deck.Extra == null)
			{
				deck.Extra = new List<int>();
			}
			if (deck.Side == null)
			{
				deck.Side = new List<int>();
			}
			string main = YdkeConverter.EncodeSection(deck.Main);
			string extra = YdkeConverter.EncodeSection(deck.Extra);
			string side = YdkeConverter.EncodeSection(deck.Side);
			return string.Concat(new string[]
			{
				YdkeConverter.ydkeHeader,
				main,
				"!",
				extra,
				"!",
				side
			});
		}

		// Token: 0x06008A4E RID: 35406 RVA: 0x00112708 File Offset: 0x00110908
		private static string EncodeSection(List<int> deck)
		{
			List<byte> bytes = new List<byte>();
			foreach (int cardId in deck)
			{
				bytes.Add((byte)(cardId & 255));
				bytes.Add((byte)((cardId >> 8) & 255));
				bytes.Add((byte)((cardId >> 16) & 255));
				bytes.Add((byte)(cardId >> 24));
			}
			return Convert.ToBase64String(bytes.ToArray());
		}

		// Token: 0x0400C5E6 RID: 50662
		public static string ydkeHeader = "ydke://";
	}
}
