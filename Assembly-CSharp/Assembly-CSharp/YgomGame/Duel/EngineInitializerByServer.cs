using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace YgomGame.Duel
{
	// Token: 0x02000E88 RID: 3720
	public class EngineInitializerByServer : EngineInitializer
	{
		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06006BDF RID: 27615 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int myPlayerNum
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06006BE0 RID: 27616 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int firstPlayer
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06006BE1 RID: 27617 RVA: 0x0000216A File Offset: 0x0000036A
		public override int[][] deck0
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06006BE2 RID: 27618 RVA: 0x0000216A File Offset: 0x0000036A
		public override int[][] deck1
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06006BE3 RID: 27619 RVA: 0x0000216A File Offset: 0x0000036A
		public override int[][] deck2
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06006BE4 RID: 27620 RVA: 0x0000216A File Offset: 0x0000036A
		public override int[][] deck3
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06006BE5 RID: 27621 RVA: 0x000029CC File Offset: 0x00000BCC
		public override uint randSeed
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06006BE6 RID: 27622 RVA: 0x000029CC File Offset: 0x00000BCC
		public override Engine.PlayerType myselfType
		{
			get
			{
				return Engine.PlayerType.Human;
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06006BE7 RID: 27623 RVA: 0x000029CC File Offset: 0x00000BCC
		public override Engine.PlayerType rivalType
		{
			get
			{
				return Engine.PlayerType.Human;
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06006BE8 RID: 27624 RVA: 0x000029CC File Offset: 0x00000BCC
		public override Engine.PlayerType myselfPartnerType
		{
			get
			{
				return Engine.PlayerType.Human;
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06006BE9 RID: 27625 RVA: 0x000029CC File Offset: 0x00000BCC
		public override Engine.PlayerType rivalPartnerType
		{
			get
			{
				return Engine.PlayerType.Human;
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06006BEA RID: 27626 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int fDuelType
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06006BEB RID: 27627 RVA: 0x000029CC File Offset: 0x00000BCC
		public override Engine.LimitedType limitedType
		{
			get
			{
				return Engine.LimitedType.None;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06006BEC RID: 27628 RVA: 0x0000216A File Offset: 0x0000036A
		public override int[] rare0
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06006BED RID: 27629 RVA: 0x0000216A File Offset: 0x0000036A
		public override int[] rare1
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06006BEE RID: 27630 RVA: 0x0000216A File Offset: 0x0000036A
		public override int[] rare2
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06006BEF RID: 27631 RVA: 0x0000216A File Offset: 0x0000036A
		public override int[] rare3
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06006BF0 RID: 27632 RVA: 0x0000216A File Offset: 0x0000036A
		public override byte[] replayData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06006BF1 RID: 27633 RVA: 0x0000216A File Offset: 0x0000036A
		public override int[] repFinish
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06006BF2 RID: 27634 RVA: 0x0000216A File Offset: 0x0000036A
		public override byte[] packedReplay
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06006BF3 RID: 27635 RVA: 0x000029CC File Offset: 0x00000BCC
		public override uint cpuParam
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06006BF4 RID: 27636 RVA: 0x000029CC File Offset: 0x00000BCC
		public override Engine.CpuParam cpuFlag
		{
			get
			{
				return Engine.CpuParam.None;
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06006BF5 RID: 27637 RVA: 0x0000216A File Offset: 0x0000036A
		public override uint[] cpuParams
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06006BF6 RID: 27638 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int challenge
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06006BF7 RID: 27639 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int challenge0
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06006BF8 RID: 27640 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int challenge1
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06006BF9 RID: 27641 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int duelId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06006BFA RID: 27642 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool noshuffle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06006BFB RID: 27643 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool inputTimer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06006BFC RID: 27644 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool match
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06006BFD RID: 27645 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool isTagDuel
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06006BFE RID: 27646 RVA: 0x0000216A File Offset: 0x0000036A
		public override int[] life
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06006BFF RID: 27647 RVA: 0x0000216A File Offset: 0x0000036A
		public override int[] hand
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06006C00 RID: 27648 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int item
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06006C01 RID: 27649 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006C02 RID: 27650 RVA: 0x0000216D File Offset: 0x0000036D
		public Dictionary<string, object> duelSettings
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x06006C03 RID: 27651 RVA: 0x000F5F7E File Offset: 0x000F417E
		private void SetupDeckAndRare(List<object> decks, int player, out int[][] dstDeck, out int[] dstRare)
		{
			dstDeck = null;
			dstRare = null;
		}

		// Token: 0x06006C04 RID: 27652 RVA: 0x0000216A File Offset: 0x0000036A
		private int[] GetRejectedArray(int[] arr, List<int> rejectIndices)
		{
			return null;
		}

		// Token: 0x06006C05 RID: 27653 RVA: 0x000F5CA6 File Offset: 0x000F3EA6
		private int[] objectListToIntArray(List<object> src, bool rejectZero, out List<int> rejectIdxs)
		{
			rejectIdxs = null;
			return null;
		}

		// Token: 0x06006C06 RID: 27654 RVA: 0x000F5CAC File Offset: 0x000F3EAC
		private int[] dicDeckToIntArray(Dictionary<string, object> dic, string key1, string key2, bool rejectZero, out List<int> rejectIdxs)
		{
			rejectIdxs = null;
			return null;
		}

		// Token: 0x0400A774 RID: 42868
		private int[][][] m_decks;

		// Token: 0x0400A775 RID: 42869
		private int[][] m_rares;

		// Token: 0x0400A776 RID: 42870
		private const int maxDecks = 4;

		// Token: 0x0400A777 RID: 42871
		private int[] repfin;
	}
}
