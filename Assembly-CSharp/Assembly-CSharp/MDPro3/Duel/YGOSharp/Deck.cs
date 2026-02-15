using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MDPro3.Net;

namespace MDPro3.Duel.YGOSharp
{
	// Token: 0x02001525 RID: 5413
	public class Deck
	{
		// Token: 0x06009D93 RID: 40339 RVA: 0x0019A39C File Offset: 0x0019859C
		public Deck()
		{
			this.Main = new List<int>();
			this.Extra = new List<int>();
			this.Side = new List<int>();
			this.Pickup = new List<int> { 0, 0, 0 };
			this.Protector = 1070001;
			this.Case = 1080001;
			this.Field = 1090001;
			this.Grave = 1100001;
			this.Stand = 1110001;
			this.Mate = 1000001;
		}

		// Token: 0x06009D94 RID: 40340 RVA: 0x0019A440 File Offset: 0x00198640
		public Deck(string path)
			: this(File.ReadAllText(path), string.Empty, string.Empty)
		{
			this.type = Path.GetFileName(Path.GetDirectoryName(path));
			if (this.type == "Deck" && this.type == Path.GetDirectoryName(path))
			{
				this.type = string.Empty;
			}
		}

		// Token: 0x06009D95 RID: 40341 RVA: 0x0019A4A4 File Offset: 0x001986A4
		public Deck(string ydk, string deckID = "", string userID = "")
		{
			this.deckId = deckID;
			this.userId = userID;
			this.Main = new List<int>();
			this.Extra = new List<int>();
			this.Side = new List<int>();
			this.Pickup = new List<int>();
			this.Protector = 1070001;
			this.Case = 1080001;
			this.Field = 1090001;
			this.Grave = 1100001;
			this.Stand = 1110001;
			this.Mate = 1000001;
			string[] array = ydk.Replace("\r", string.Empty).Replace("\\r", string.Empty).Replace("\\n", "\n")
				.Split("\n", StringSplitOptions.RemoveEmptyEntries);
			int flag = -1;
			foreach (string line in array)
			{
				if (line.StartsWith("###") && this.userId == string.Empty)
				{
					this.userId = line.Replace("###", string.Empty);
				}
				else if (line.StartsWith("##") && this.deckId == string.Empty)
				{
					this.deckId = line.Replace("##", string.Empty);
				}
				else if (line.StartsWith("#main"))
				{
					flag = 1;
				}
				else if (line.StartsWith("#extra"))
				{
					flag = 2;
				}
				else if (line.StartsWith("!side"))
				{
					flag = 3;
				}
				else if (line.StartsWith("#pickup"))
				{
					flag = 4;
				}
				else if (line.StartsWith("#protector"))
				{
					flag = 5;
				}
				else if (line.StartsWith("#case"))
				{
					flag = 6;
				}
				else if (line.StartsWith("#field"))
				{
					flag = 7;
				}
				else if (line.StartsWith("#grave"))
				{
					flag = 8;
				}
				else if (line.StartsWith("#stand"))
				{
					flag = 9;
				}
				else if (line.StartsWith("#mate"))
				{
					flag = 10;
				}
				else
				{
					int code = 0;
					try
					{
						code = int.Parse(line.Replace("#", ""));
					}
					catch
					{
						goto IL_02E5;
					}
					if (code > 100)
					{
						switch (flag)
						{
						case 1:
							this.Main.Add(code);
							break;
						case 2:
							this.Extra.Add(code);
							break;
						case 3:
							this.Side.Add(code);
							break;
						case 4:
							this.Pickup.Add(code);
							break;
						case 5:
							this.Protector = code;
							break;
						case 6:
							this.Case = code;
							break;
						case 7:
							this.Field = code;
							break;
						case 8:
							this.Grave = code;
							break;
						case 9:
							this.Stand = code;
							break;
						case 10:
							this.Mate = code;
							break;
						}
					}
				}
				IL_02E5:;
			}
			if (this.Pickup.Count < 3)
			{
				this.Pickup.AddRange(Enumerable.Repeat<int>(0, 3 - this.Pickup.Count));
			}
		}

		// Token: 0x06009D96 RID: 40342 RVA: 0x0019A7E0 File Offset: 0x001989E0
		public Deck(List<int> main, List<int> extra, List<int> side)
		{
			this.Main = main;
			this.Extra = extra;
			this.Side = side;
			this.Pickup = new List<int> { 0, 0, 0 };
			this.Protector = 1070001;
			this.Case = 1080001;
			this.Field = 1090001;
			this.Grave = 1100001;
			this.Stand = 1110001;
			this.Mate = 1000001;
		}

		// Token: 0x06009D97 RID: 40343 RVA: 0x0019A878 File Offset: 0x00198A78
		public int Check(Banlist ban, bool ocg, bool tcg)
		{
			if (this.Main.Count < 40 || this.Main.Count > 60 || this.Extra.Count > 15 || this.Side.Count > 15)
			{
				return 1;
			}
			Dictionary<int, int> cards = new Dictionary<int, int>();
			List<int>[] array = new List<int>[] { this.Main, this.Extra, this.Side };
			for (int i = 0; i < array.Length; i++)
			{
				foreach (int id in array[i])
				{
					Card card = CardsManager.Get(id, false);
					Deck.AddToCards(cards, card);
					if ((!ocg && card.Ot == 1) || (!tcg && card.Ot == 2))
					{
						return id;
					}
					if (card.HasType(CardType.Token))
					{
						return id;
					}
				}
			}
			if (ban == null)
			{
				return 0;
			}
			foreach (KeyValuePair<int, int> pair in cards)
			{
				int max = ban.GetQuantity(pair.Key);
				if (pair.Value > max)
				{
					return pair.Key;
				}
			}
			return 0;
		}

		// Token: 0x06009D98 RID: 40344 RVA: 0x0019A9E8 File Offset: 0x00198BE8
		public int GetCardCount(int code)
		{
			try
			{
				int alias = CardsManager.Get(code, false).Alias;
			}
			catch (Exception)
			{
			}
			return 0;
		}

		// Token: 0x06009D99 RID: 40345 RVA: 0x0019AA18 File Offset: 0x00198C18
		public bool Check(Deck deck)
		{
			if (deck.Main.Count != this.Main.Count || deck.Extra.Count != this.Extra.Count)
			{
				return false;
			}
			Dictionary<int, int> cards = new Dictionary<int, int>();
			Dictionary<int, int> ncards = new Dictionary<int, int>();
			List<int>[] array = new List<int>[] { this.Main, this.Extra, this.Side };
			for (int i = 0; i < array.Length; i++)
			{
				foreach (int id in ((IEnumerable<int>)array[i]))
				{
					if (!cards.ContainsKey(id))
					{
						cards.Add(id, 1);
					}
					else
					{
						Dictionary<int, int> dictionary = cards;
						int num = id;
						int num2 = dictionary[num];
						dictionary[num] = num2 + 1;
					}
				}
			}
			array = new List<int>[] { deck.Main, deck.Extra, deck.Side };
			for (int i = 0; i < array.Length; i++)
			{
				foreach (int id2 in array[i])
				{
					if (!ncards.ContainsKey(id2))
					{
						ncards.Add(id2, 1);
					}
					else
					{
						Dictionary<int, int> dictionary2 = ncards;
						int num2 = id2;
						int num = dictionary2[num2];
						dictionary2[num2] = num + 1;
					}
				}
			}
			foreach (KeyValuePair<int, int> pair in cards)
			{
				if (!ncards.ContainsKey(pair.Key))
				{
					return false;
				}
				if (ncards[pair.Key] != pair.Value)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06009D9A RID: 40346 RVA: 0x0019AC04 File Offset: 0x00198E04
		private static void AddToCards(Dictionary<int, int> cards, Card card)
		{
			int id = card.Id;
			if (card.Alias != 0)
			{
				id = card.Alias;
			}
			if (cards.ContainsKey(id))
			{
				int num = id;
				int num2 = cards[num];
				cards[num] = num2 + 1;
				return;
			}
			cards.Add(id, 1);
		}

		// Token: 0x06009D9B RID: 40347 RVA: 0x0019AC50 File Offset: 0x00198E50
		public bool Save(string deckName, DateTime saveTime, bool upload = true, bool showHint = true)
		{
			string ydk = this.GetYDK();
			try
			{
				deckName = Path.GetFileNameWithoutExtension(deckName);
				string text = "Deck/" + ((this.type == string.Empty) ? string.Empty : (this.type + "/")) + deckName + ".ydk";
				string dir = Path.GetDirectoryName(text);
				if (!Directory.Exists(dir))
				{
					Directory.CreateDirectory(dir);
				}
				File.WriteAllText(text, ydk, Encoding.UTF8);
				File.SetLastWriteTimeUtc(text, saveTime);
				if (MyCard.account != null && upload)
				{
					OnlineDeck.SyncDeck(this.deckId, deckName, this, saveTime, showHint);
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x06009D9C RID: 40348 RVA: 0x0019AD04 File Offset: 0x00198F04
		public string GetYDK()
		{
			string value = "#created by MDPro3\r\n#main";
			for (int i = 0; i < this.Main.Count; i++)
			{
				value += string.Format("\r\n{0}", this.Main[i]);
			}
			value += "\r\n#extra";
			for (int j = 0; j < this.Extra.Count; j++)
			{
				value += string.Format("\r\n{0}", this.Extra[j]);
			}
			value += "\r\n!side";
			for (int k = 0; k < this.Side.Count; k++)
			{
				value += string.Format("\r\n{0}", this.Side[k]);
			}
			value += string.Format("\r\n#pickup\r\n#{0}", this.Pickup[0]);
			value += string.Format("\r\n#{0}", this.Pickup[1]);
			value += string.Format("\r\n#{0}", this.Pickup[2]);
			value += string.Format("\r\n#case\r\n#{0}", this.Case);
			value += string.Format("\r\n#protector\r\n#{0}", this.Protector);
			value += string.Format("\r\n#field\r\n#{0}", this.Field);
			value += string.Format("\r\n#grave\r\n#{0}", this.Grave);
			value += string.Format("\r\n#stand\r\n#{0}", this.Stand);
			value += string.Format("\r\n#mate\r\n#{0}", this.Mate);
			if (!string.IsNullOrEmpty(this.deckId))
			{
				value = value + "\r\n##" + this.deckId;
			}
			if (!string.IsNullOrEmpty(this.userId))
			{
				value = value + "\r\n###" + this.userId;
			}
			return value;
		}

		// Token: 0x0400DB83 RID: 56195
		public List<int> Main;

		// Token: 0x0400DB84 RID: 56196
		public List<int> Extra;

		// Token: 0x0400DB85 RID: 56197
		public List<int> Side;

		// Token: 0x0400DB86 RID: 56198
		public List<int> Pickup;

		// Token: 0x0400DB87 RID: 56199
		public int Protector;

		// Token: 0x0400DB88 RID: 56200
		public int Case;

		// Token: 0x0400DB89 RID: 56201
		public int Field;

		// Token: 0x0400DB8A RID: 56202
		public int Grave;

		// Token: 0x0400DB8B RID: 56203
		public int Stand;

		// Token: 0x0400DB8C RID: 56204
		public int Mate;

		// Token: 0x0400DB8D RID: 56205
		public const string deckHint = "#created by MDPro3";

		// Token: 0x0400DB8E RID: 56206
		public string userId;

		// Token: 0x0400DB8F RID: 56207
		public string deckId;

		// Token: 0x0400DB90 RID: 56208
		public string type = string.Empty;
	}
}
