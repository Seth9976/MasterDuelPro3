using System;

namespace YgomGame.Duel
{
	// Token: 0x02000E87 RID: 3719
	public abstract class EngineInitializer
	{
		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06006BB7 RID: 27575
		public abstract int myPlayerNum { get; }

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06006BB8 RID: 27576 RVA: 0x000029CC File Offset: 0x00000BCC
		public int rivalPlayerNum
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06006BB9 RID: 27577
		public abstract int firstPlayer { get; }

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06006BBA RID: 27578
		public abstract int[][] deck0 { get; }

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06006BBB RID: 27579
		public abstract int[][] deck1 { get; }

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06006BBC RID: 27580
		public abstract uint randSeed { get; }

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06006BBD RID: 27581
		public abstract Engine.PlayerType myselfType { get; }

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06006BBE RID: 27582
		public abstract Engine.PlayerType rivalType { get; }

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06006BBF RID: 27583
		public abstract Engine.PlayerType myselfPartnerType { get; }

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06006BC0 RID: 27584
		public abstract Engine.PlayerType rivalPartnerType { get; }

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06006BC1 RID: 27585
		public abstract int fDuelType { get; }

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06006BC2 RID: 27586
		public abstract Engine.LimitedType limitedType { get; }

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06006BC3 RID: 27587
		public abstract int[] rare0 { get; }

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06006BC4 RID: 27588
		public abstract int[] rare1 { get; }

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06006BC5 RID: 27589
		public abstract byte[] replayData { get; }

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06006BC6 RID: 27590
		public abstract uint cpuParam { get; }

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06006BC7 RID: 27591
		public abstract Engine.CpuParam cpuFlag { get; }

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06006BC8 RID: 27592 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual int challenge
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06006BC9 RID: 27593 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual int challenge0
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06006BCA RID: 27594 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual int challenge1
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06006BCB RID: 27595 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual int duelId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06006BCC RID: 27596 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool noshuffle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06006BCD RID: 27597 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool inputTimer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06006BCE RID: 27598 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual byte[] packedReplay
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06006BCF RID: 27599 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual int[] repFinish
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06006BD0 RID: 27600 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool match
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06006BD1 RID: 27601 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool isTagDuel
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06006BD2 RID: 27602 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual int[][] deck2
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06006BD3 RID: 27603 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual int[][] deck3
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06006BD4 RID: 27604 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual int[] life
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06006BD5 RID: 27605 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual uint[] cpuParams
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06006BD6 RID: 27606 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual int[] rare2
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06006BD7 RID: 27607 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual int[] rare3
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06006BD8 RID: 27608 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual int[] hand
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06006BD9 RID: 27609 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual int item
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06006BDA RID: 27610 RVA: 0x0000216A File Offset: 0x0000036A
		private int[][] deck(int player)
		{
			return null;
		}

		// Token: 0x06006BDB RID: 27611 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void LoadResources()
		{
		}

		// Token: 0x06006BDC RID: 27612 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool WaitLoad()
		{
			return false;
		}

		// Token: 0x06006BDD RID: 27613 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitEngine(Engine.RunEffect runEffect, Engine.IsBusyEffect isBusyEffect, ref RecordManager recmanref)
		{
		}
	}
}
