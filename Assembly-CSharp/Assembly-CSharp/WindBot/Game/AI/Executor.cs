using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI
{
	// Token: 0x02000240 RID: 576
	public abstract class Executor
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x00036D13 File Offset: 0x00034F13
		// (set) Token: 0x06000CAF RID: 3247 RVA: 0x00036D1B File Offset: 0x00034F1B
		public string Deck { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x00036D24 File Offset: 0x00034F24
		// (set) Token: 0x06000CB1 RID: 3249 RVA: 0x00036D2C File Offset: 0x00034F2C
		public Duel Duel { get; private set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x00036D35 File Offset: 0x00034F35
		// (set) Token: 0x06000CB3 RID: 3251 RVA: 0x00036D3D File Offset: 0x00034F3D
		public IList<CardExecutor> Executors { get; private set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x00036D46 File Offset: 0x00034F46
		// (set) Token: 0x06000CB5 RID: 3253 RVA: 0x00036D4E File Offset: 0x00034F4E
		public GameAI AI { get; private set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x00036D57 File Offset: 0x00034F57
		// (set) Token: 0x06000CB7 RID: 3255 RVA: 0x00036D5F File Offset: 0x00034F5F
		public AIUtil Util { get; private set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x00036D68 File Offset: 0x00034F68
		// (set) Token: 0x06000CB9 RID: 3257 RVA: 0x00036D70 File Offset: 0x00034F70
		private protected ExecutorType Type { protected get; private set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00036D79 File Offset: 0x00034F79
		// (set) Token: 0x06000CBB RID: 3259 RVA: 0x00036D81 File Offset: 0x00034F81
		private protected ClientCard Card { protected get; private set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x00036D8A File Offset: 0x00034F8A
		// (set) Token: 0x06000CBD RID: 3261 RVA: 0x00036D92 File Offset: 0x00034F92
		private protected int ActivateDescription { protected get; private set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00036D9B File Offset: 0x00034F9B
		// (set) Token: 0x06000CBF RID: 3263 RVA: 0x00036DA3 File Offset: 0x00034FA3
		private protected int CurrentTiming { protected get; private set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00036DAC File Offset: 0x00034FAC
		// (set) Token: 0x06000CC1 RID: 3265 RVA: 0x00036DB4 File Offset: 0x00034FB4
		private protected ClientField Bot { protected get; private set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x00036DBD File Offset: 0x00034FBD
		// (set) Token: 0x06000CC3 RID: 3267 RVA: 0x00036DC5 File Offset: 0x00034FC5
		private protected ClientField Enemy { protected get; private set; }

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00036DD0 File Offset: 0x00034FD0
		protected Executor(GameAI ai, Duel duel)
		{
			this.Duel = duel;
			this.AI = ai;
			this.Util = new AIUtil(duel);
			this.Executors = new List<CardExecutor>();
			this.Bot = this.Duel.Fields[0];
			this.Enemy = this.Duel.Fields[1];
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00036E2E File Offset: 0x0003502E
		public virtual int OnRockPaperScissors()
		{
			return Program.Rand.Next(1, 4);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00036E3C File Offset: 0x0003503C
		public virtual bool OnSelectHand()
		{
			return Program.Rand.Next(2) > 0;
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual BattlePhaseAction OnBattle(IList<ClientCard> attackers, IList<ClientCard> defenders)
		{
			return null;
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual ClientCard OnSelectAttacker(IList<ClientCard> attackers, IList<ClientCard> defenders)
		{
			return null;
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual BattlePhaseAction OnSelectAttackTarget(ClientCard attacker, IList<ClientCard> defenders)
		{
			return null;
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0000763C File Offset: 0x0000583C
		public virtual bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
		{
			return true;
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0000763C File Offset: 0x0000583C
		public virtual bool OnPreActivate(ClientCard card)
		{
			return true;
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnChaining(int player, ClientCard card)
		{
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnChainSolved(int chainIndex)
		{
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnChainEnd()
		{
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnReceivingAnnouce(int player, int data)
		{
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnNewPhase()
		{
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnNewTurn()
		{
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnDraw(int player)
		{
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnMove(ClientCard card, int previousControler, int previousLocation, int currentControler, int currentLocation)
		{
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
		{
			return null;
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual IList<ClientCard> OnSelectSum(IList<ClientCard> cards, int sum, int min, int max, int hint, bool mode)
		{
			return null;
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual IList<ClientCard> OnSelectFusionMaterial(IList<ClientCard> cards, int min, int max)
		{
			return null;
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual IList<ClientCard> OnSelectSynchroMaterial(IList<ClientCard> cards, int sum, int min, int max)
		{
			return null;
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual IList<ClientCard> OnSelectXyzMaterial(IList<ClientCard> cards, int min, int max)
		{
			return null;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual IList<ClientCard> OnSelectLinkMaterial(IList<ClientCard> cards, int min, int max)
		{
			return null;
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual IList<ClientCard> OnSelectRitualTribute(IList<ClientCard> cards, int sum, int min, int max)
		{
			return null;
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual IList<ClientCard> OnSelectPendulumSummon(IList<ClientCard> cards, int max)
		{
			return null;
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual IList<ClientCard> OnCardSorting(IList<ClientCard> cards)
		{
			return null;
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnSelectChain(IList<ClientCard> cards)
		{
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0000763C File Offset: 0x0000583C
		public virtual bool OnSelectYesNo(int desc)
		{
			return true;
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00036E4C File Offset: 0x0003504C
		public virtual int OnSelectOption(IList<int> options)
		{
			return -1;
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual int OnSelectPlace(int cardId, int player, CardLocation location, int available)
		{
			return 0;
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
		{
			return (CardPosition)0;
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool OnSelectBattleReplay()
		{
			return false;
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool OnSelectMonsterSummonOrSet(ClientCard card)
		{
			return false;
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual int OnAnnounceCard(IList<int> avail)
		{
			return 0;
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnSpSummoned()
		{
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00036E4F File Offset: 0x0003504F
		public void SetCard(ExecutorType type, ClientCard card, int description, int timing = -1)
		{
			this.Type = type;
			this.Card = card;
			this.ActivateDescription = description;
			this.CurrentTiming = timing;
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00036E6E File Offset: 0x0003506E
		public void AddExecutor(ExecutorType type, int cardId, Func<bool> func)
		{
			this.Executors.Add(new CardExecutor(type, cardId, func));
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00036E83 File Offset: 0x00035083
		public void AddExecutor(ExecutorType type, int cardId)
		{
			this.Executors.Add(new CardExecutor(type, cardId, null));
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00036E98 File Offset: 0x00035098
		public void AddExecutor(ExecutorType type, Func<bool> func)
		{
			this.Executors.Add(new CardExecutor(type, -1, func));
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00036EAD File Offset: 0x000350AD
		public void AddExecutor(ExecutorType type)
		{
			this.Executors.Add(new CardExecutor(type, -1, new Func<bool>(this.DefaultNoExecutor)));
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00036ECD File Offset: 0x000350CD
		private bool DefaultNoExecutor()
		{
			return this.Executors.All((CardExecutor exec) => exec.Type != this.Type || exec.CardId != this.Card.Id);
		}
	}
}
