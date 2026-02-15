using System;

namespace YgomGame.Colosseum
{
	// Token: 0x02001048 RID: 4168
	public class ColosseumPathManager
	{
		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06007D76 RID: 32118 RVA: 0x0000216A File Offset: 0x0000036A
		public string DECK_LIST
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06007D77 RID: 32119 RVA: 0x0000216A File Offset: 0x0000036A
		public string ACCESSORY_BOX
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06007D78 RID: 32120 RVA: 0x0000216A File Offset: 0x0000036A
		public string ACCESSORY_SLEEVE
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06007D79 RID: 32121 RVA: 0x0000216A File Offset: 0x0000036A
		public string PICKCARDS_IDS
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x06007D7A RID: 32122 RVA: 0x0000216A File Offset: 0x0000036A
		public string PICKCARDS_R
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x06007D7B RID: 32123 RVA: 0x0000216A File Offset: 0x0000036A
		public string ARGKEY_ID_NAME
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x06007D7C RID: 32124 RVA: 0x0000216A File Offset: 0x0000036A
		public string DECK_LIST_ACCESSORY
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06007D7D RID: 32125 RVA: 0x0000216A File Offset: 0x0000036A
		public string DECK_LIST_PICKCARDS_IDS
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x06007D7E RID: 32126 RVA: 0x0000216A File Offset: 0x0000036A
		public string DECK_LIST_PICKCARDS_R
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x06007D7F RID: 32127 RVA: 0x0000216A File Offset: 0x0000036A
		public string NAME_REG_ID
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x06007D80 RID: 32128 RVA: 0x0000216A File Offset: 0x0000036A
		public object IDS
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007D81 RID: 32129 RVA: 0x00002739 File Offset: 0x00000939
		public ColosseumPathManager(ColosseumUtil.PlayMode playMode, int identifier)
		{
		}

		// Token: 0x06007D82 RID: 32130 RVA: 0x00002739 File Offset: 0x00000939
		public ColosseumPathManager(ColosseumUtil.PlayMode playMode, bool isRental, int identifier, int identifier2 = 1)
		{
		}

		// Token: 0x0400B57A RID: 46458
		public const int BASE_VERSUS_LOGO_ID = 400;

		// Token: 0x0400B57B RID: 46459
		private ColosseumPathManager.PathBase path;

		// Token: 0x02001049 RID: 4169
		internal class PathBase
		{
			// Token: 0x0400B57C RID: 46460
			internal string deck_list;

			// Token: 0x0400B57D RID: 46461
			internal string accessory_box;

			// Token: 0x0400B57E RID: 46462
			internal string accessory_sleeve;

			// Token: 0x0400B57F RID: 46463
			internal string pickcards_ids;

			// Token: 0x0400B580 RID: 46464
			internal string pickcards_r;

			// Token: 0x0400B581 RID: 46465
			internal string id_name;

			// Token: 0x0400B582 RID: 46466
			internal string deck_list_accessory;

			// Token: 0x0400B583 RID: 46467
			internal string deck_list_pickcards_ids;

			// Token: 0x0400B584 RID: 46468
			internal string deck_list_pickcards_r;

			// Token: 0x0400B585 RID: 46469
			internal string name_reg_id;

			// Token: 0x0400B586 RID: 46470
			internal string ids;
		}

		// Token: 0x0200104A RID: 4170
		internal class PathTournament : ColosseumPathManager.PathBase
		{
			// Token: 0x06007D84 RID: 32132 RVA: 0x000F67F5 File Offset: 0x000F49F5
			internal PathTournament(int id)
			{
			}
		}

		// Token: 0x0200104B RID: 4171
		internal class PathExhibition : ColosseumPathManager.PathBase
		{
			// Token: 0x06007D85 RID: 32133 RVA: 0x000F67F5 File Offset: 0x000F49F5
			internal PathExhibition(int identifier, bool isRental)
			{
			}
		}

		// Token: 0x0200104C RID: 4172
		internal class PathDuelistCup : ColosseumPathManager.PathBase
		{
			// Token: 0x06007D86 RID: 32134 RVA: 0x000F67F5 File Offset: 0x000F49F5
			internal PathDuelistCup()
			{
			}
		}

		// Token: 0x0200104D RID: 4173
		internal class PathRankEvent : ColosseumPathManager.PathBase
		{
			// Token: 0x06007D87 RID: 32135 RVA: 0x000F67F5 File Offset: 0x000F49F5
			internal PathRankEvent(int id)
			{
			}
		}

		// Token: 0x0200104E RID: 4174
		internal class PathDuelTrial : ColosseumPathManager.PathBase
		{
			// Token: 0x06007D88 RID: 32136 RVA: 0x000F67F5 File Offset: 0x000F49F5
			internal PathDuelTrial(int identifier, bool isRental, int deckNo = 1)
			{
			}
		}

		// Token: 0x0200104F RID: 4175
		internal class PathWCS : ColosseumPathManager.PathBase
		{
			// Token: 0x06007D89 RID: 32137 RVA: 0x000F67F5 File Offset: 0x000F49F5
			internal PathWCS()
			{
			}
		}

		// Token: 0x02001050 RID: 4176
		internal class PathVersus : ColosseumPathManager.PathBase
		{
			// Token: 0x06007D8A RID: 32138 RVA: 0x000F67F5 File Offset: 0x000F49F5
			internal PathVersus(int identifier, bool isRental, int deckNo = 1)
			{
			}
		}
	}
}
