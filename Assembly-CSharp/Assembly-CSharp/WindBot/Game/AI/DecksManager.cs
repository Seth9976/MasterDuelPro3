using System;
using System.Collections.Generic;
using System.Reflection;

namespace WindBot.Game.AI
{
	// Token: 0x0200022E RID: 558
	public static class DecksManager
	{
		// Token: 0x06000BA5 RID: 2981 RVA: 0x00032D40 File Offset: 0x00030F40
		public static void Init()
		{
			DecksManager._decks = new Dictionary<string, DecksManager.DeckInstance>();
			DecksManager._rand = new Random();
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
			{
				foreach (object attribute in type.GetCustomAttributes(false))
				{
					if (attribute is DeckAttribute)
					{
						DeckAttribute deck = (DeckAttribute)attribute;
						DecksManager._decks.Add(deck.Name, new DecksManager.DeckInstance(deck.File, type, deck.Level));
					}
				}
			}
			DecksManager._list = new List<DecksManager.DeckInstance>();
			DecksManager._list.AddRange(DecksManager._decks.Values);
			Logger.WriteLine("Decks initialized, " + DecksManager._decks.Count.ToString() + " found.");
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00032E1C File Offset: 0x0003101C
		public static Executor Instantiate(GameAI ai, Duel duel)
		{
			string deck = ai.Game.Deck;
			DecksManager.DeckInstance infos;
			if (deck != null && DecksManager._decks.ContainsKey(deck))
			{
				infos = DecksManager._decks[deck];
			}
			else
			{
				do
				{
					infos = DecksManager._list[DecksManager._rand.Next(DecksManager._list.Count)];
				}
				while (infos.Level != "Normal");
			}
			Executor executor = (Executor)Activator.CreateInstance(infos.Type, new object[] { ai, duel });
			executor.Deck = infos.Deck;
			return executor;
		}

		// Token: 0x04000E4A RID: 3658
		private static Dictionary<string, DecksManager.DeckInstance> _decks;

		// Token: 0x04000E4B RID: 3659
		private static List<DecksManager.DeckInstance> _list;

		// Token: 0x04000E4C RID: 3660
		private static Random _rand;

		// Token: 0x0200022F RID: 559
		private class DeckInstance
		{
			// Token: 0x17000167 RID: 359
			// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x00032EAE File Offset: 0x000310AE
			// (set) Token: 0x06000BA8 RID: 2984 RVA: 0x00032EB6 File Offset: 0x000310B6
			public string Deck { get; private set; }

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x00032EBF File Offset: 0x000310BF
			// (set) Token: 0x06000BAA RID: 2986 RVA: 0x00032EC7 File Offset: 0x000310C7
			public Type Type { get; private set; }

			// Token: 0x17000169 RID: 361
			// (get) Token: 0x06000BAB RID: 2987 RVA: 0x00032ED0 File Offset: 0x000310D0
			// (set) Token: 0x06000BAC RID: 2988 RVA: 0x00032ED8 File Offset: 0x000310D8
			public string Level { get; private set; }

			// Token: 0x06000BAD RID: 2989 RVA: 0x00032EE1 File Offset: 0x000310E1
			public DeckInstance(string deck, Type type, string level)
			{
				this.Deck = deck;
				this.Type = type;
				this.Level = level;
			}
		}
	}
}
