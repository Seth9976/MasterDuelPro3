using System;

namespace WindBot.Game.AI
{
	// Token: 0x02000228 RID: 552
	public class CardExecutor
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x000326EF File Offset: 0x000308EF
		// (set) Token: 0x06000B81 RID: 2945 RVA: 0x000326F7 File Offset: 0x000308F7
		public int CardId { get; private set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x00032700 File Offset: 0x00030900
		// (set) Token: 0x06000B83 RID: 2947 RVA: 0x00032708 File Offset: 0x00030908
		public ExecutorType Type { get; private set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x00032711 File Offset: 0x00030911
		// (set) Token: 0x06000B85 RID: 2949 RVA: 0x00032719 File Offset: 0x00030919
		public Func<bool> Func { get; private set; }

		// Token: 0x06000B86 RID: 2950 RVA: 0x00032722 File Offset: 0x00030922
		public CardExecutor(ExecutorType type, int cardId, Func<bool> func)
		{
			this.CardId = cardId;
			this.Type = type;
			this.Func = func;
		}
	}
}
