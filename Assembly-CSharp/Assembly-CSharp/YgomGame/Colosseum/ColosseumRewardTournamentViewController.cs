using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Colosseum
{
	// Token: 0x02001067 RID: 4199
	public class ColosseumRewardTournamentViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x06007DFE RID: 32254 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007DFF RID: 32255 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007E00 RID: 32256 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x0400B664 RID: 46692
		private readonly string SCROLL_LABEL;

		// Token: 0x0400B665 RID: 46693
		private ColosseumRewardTournamentViewController.ModeBehaviour modeBehaviour;

		// Token: 0x02001068 RID: 4200
		internal abstract class ModeBehaviour
		{
			// Token: 0x06007E02 RID: 32258 RVA: 0x00002739 File Offset: 0x00000939
			protected ModeBehaviour(ColosseumRewardTournamentViewController vc, InfinityScrollView isv, ElementObjectManager eom, int id, string startDateReward, string endDateReward)
			{
			}

			// Token: 0x06007E03 RID: 32259
			internal abstract void CallAPI();

			// Token: 0x06007E04 RID: 32260
			internal abstract void UpdateView();

			// Token: 0x06007E05 RID: 32261
			internal abstract void OnUpdateEntity(GameObject go, int dataIndex);

			// Token: 0x06007E06 RID: 32262
			internal abstract void InitializeScroll();

			// Token: 0x0400B666 RID: 46694
			protected readonly string BTN_LABEL;

			// Token: 0x0400B667 RID: 46695
			protected readonly string BTN_ICON_LABEL;

			// Token: 0x0400B668 RID: 46696
			protected readonly string IMG_LINE_LABEL;

			// Token: 0x0400B669 RID: 46697
			protected readonly string TXT_ORDER_LABEL;

			// Token: 0x0400B66A RID: 46698
			protected readonly string TXT_NAME_LABEL;

			// Token: 0x0400B66B RID: 46699
			protected readonly string TXT_TITLE_LABEL;

			// Token: 0x0400B66C RID: 46700
			protected readonly string TXT_DATE_LABEL;

			// Token: 0x0400B66D RID: 46701
			protected readonly string TXT_EXPLAIN_LABEL;

			// Token: 0x0400B66E RID: 46702
			protected readonly string E_ImageReceived;

			// Token: 0x0400B66F RID: 46703
			protected readonly ColosseumRewardTournamentViewController vc;

			// Token: 0x0400B670 RID: 46704
			protected readonly InfinityScrollView isv;

			// Token: 0x0400B671 RID: 46705
			protected readonly ElementObjectManager eom;

			// Token: 0x0400B672 RID: 46706
			protected readonly int id;

			// Token: 0x0400B673 RID: 46707
			protected readonly string startDateReward;

			// Token: 0x0400B674 RID: 46708
			protected readonly string endDateReward;
		}

		// Token: 0x02001069 RID: 4201
		internal class TournamentBehaviour : ColosseumRewardTournamentViewController.ModeBehaviour
		{
			// Token: 0x06007E07 RID: 32263 RVA: 0x000F682B File Offset: 0x000F4A2B
			public TournamentBehaviour(ColosseumRewardTournamentViewController vc, InfinityScrollView isv, ElementObjectManager eom, int id, string startDateReward, string endDateReward)
				: base(null, null, null, 0, null, null)
			{
			}

			// Token: 0x06007E08 RID: 32264 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007E09 RID: 32265 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void InitializeScroll()
			{
			}

			// Token: 0x06007E0A RID: 32266 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnUpdateEntity(GameObject go, int dataIndex)
			{
			}

			// Token: 0x06007E0B RID: 32267 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateView()
			{
			}

			// Token: 0x06007E0C RID: 32268 RVA: 0x0000216A File Offset: 0x0000036A
			private IReadOnlyList<ValueTuple<SelectionItem, int, int>> CustomCollectionSelectionItems(GameObject rewardEntity)
			{
				return null;
			}

			// Token: 0x0400B675 RID: 46709
			private List<ColosseumRewardTournamentViewController.TournamentBehaviour.Data> dataList;

			// Token: 0x0200106A RID: 4202
			private class Data
			{
				// Token: 0x06007E0D RID: 32269 RVA: 0x00002739 File Offset: 0x00000939
				public Data(int maxRank, int minRank)
				{
				}

				// Token: 0x0400B676 RID: 46710
				internal List<ColosseumRewardTournamentViewController.TournamentBehaviour.ItemData> itemDatas;

				// Token: 0x0400B677 RID: 46711
				internal int maxRank;

				// Token: 0x0400B678 RID: 46712
				internal int minRank;
			}

			// Token: 0x0200106B RID: 4203
			private class ItemData
			{
				// Token: 0x06007E0E RID: 32270 RVA: 0x00002739 File Offset: 0x00000939
				public ItemData(int itemID, int quantity)
				{
				}

				// Token: 0x0400B679 RID: 46713
				internal int itemID;

				// Token: 0x0400B67A RID: 46714
				internal int quantity;
			}
		}

		// Token: 0x0200106C RID: 4204
		internal class ExhibitionBehaviour : ColosseumRewardTournamentViewController.ModeBehaviour
		{
			// Token: 0x06007E0F RID: 32271 RVA: 0x000F682B File Offset: 0x000F4A2B
			public ExhibitionBehaviour(ColosseumRewardTournamentViewController vc, InfinityScrollView isv, ElementObjectManager eom, int id, string startDateReward, string endDateReward)
				: base(null, null, null, 0, null, null)
			{
			}

			// Token: 0x06007E10 RID: 32272 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007E11 RID: 32273 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void InitializeScroll()
			{
			}

			// Token: 0x06007E12 RID: 32274 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnUpdateEntity(GameObject go, int dataIndex)
			{
			}

			// Token: 0x06007E13 RID: 32275 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateView()
			{
			}

			// Token: 0x0400B67B RID: 46715
			private List<ColosseumRewardTournamentViewController.ExhibitionBehaviour.Data> dataList;

			// Token: 0x0200106D RID: 4205
			private class Data
			{
				// Token: 0x06007E14 RID: 32276 RVA: 0x00002739 File Offset: 0x00000939
				public Data(int itemCategory, int itemID, int needToken, int quantity, bool isPeriod, bool received)
				{
				}

				// Token: 0x0400B67C RID: 46716
				internal readonly int itemCategory;

				// Token: 0x0400B67D RID: 46717
				internal readonly int itemID;

				// Token: 0x0400B67E RID: 46718
				internal readonly int needToken;

				// Token: 0x0400B67F RID: 46719
				internal readonly int quantity;

				// Token: 0x0400B680 RID: 46720
				internal bool isPeriod;

				// Token: 0x0400B681 RID: 46721
				internal bool received;
			}
		}

		// Token: 0x0200106E RID: 4206
		internal class DuelTrialBehaviour : ColosseumRewardTournamentViewController.ModeBehaviour
		{
			// Token: 0x06007E15 RID: 32277 RVA: 0x000F682B File Offset: 0x000F4A2B
			public DuelTrialBehaviour(ColosseumRewardTournamentViewController vc, InfinityScrollView isv, ElementObjectManager eom, int id, string startDateReward, string endDateReward)
				: base(null, null, null, 0, null, null)
			{
			}

			// Token: 0x06007E16 RID: 32278 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007E17 RID: 32279 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void InitializeScroll()
			{
			}

			// Token: 0x06007E18 RID: 32280 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnUpdateEntity(GameObject go, int dataIndex)
			{
			}

			// Token: 0x06007E19 RID: 32281 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateView()
			{
			}

			// Token: 0x06007E1A RID: 32282 RVA: 0x0000216A File Offset: 0x0000036A
			private IReadOnlyList<ValueTuple<SelectionItem, int, int>> CustomCollectionSelectionItems(GameObject rewardEntity)
			{
				return null;
			}

			// Token: 0x0400B682 RID: 46722
			private List<ColosseumRewardTournamentViewController.DuelTrialBehaviour.Data> dataList;

			// Token: 0x0200106F RID: 4207
			private class Data
			{
				// Token: 0x06007E1B RID: 32283 RVA: 0x00002739 File Offset: 0x00000939
				public Data(string winText)
				{
				}

				// Token: 0x0400B683 RID: 46723
				internal List<ColosseumRewardTournamentViewController.DuelTrialBehaviour.ItemData> itemDatas;

				// Token: 0x0400B684 RID: 46724
				internal string winText;
			}

			// Token: 0x02001070 RID: 4208
			private class ItemData
			{
				// Token: 0x06007E1C RID: 32284 RVA: 0x00002739 File Offset: 0x00000939
				public ItemData(int itemCategory, int itemID, int num, bool isPeriod, int shopId)
				{
				}

				// Token: 0x0400B685 RID: 46725
				internal int itemCategory;

				// Token: 0x0400B686 RID: 46726
				internal int itemID;

				// Token: 0x0400B687 RID: 46727
				internal int num;

				// Token: 0x0400B688 RID: 46728
				internal bool isPeriod;

				// Token: 0x0400B689 RID: 46729
				internal int shopId;
			}
		}

		// Token: 0x02001071 RID: 4209
		internal class VersusBehaviour : ColosseumRewardTournamentViewController.ModeBehaviour
		{
			// Token: 0x06007E1D RID: 32285 RVA: 0x000F682B File Offset: 0x000F4A2B
			public VersusBehaviour(ColosseumRewardTournamentViewController vc, InfinityScrollView isv, ElementObjectManager eom, int id, string startDateReward, string endDateReward)
				: base(null, null, null, 0, null, null)
			{
			}

			// Token: 0x06007E1E RID: 32286 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007E1F RID: 32287 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void InitializeScroll()
			{
			}

			// Token: 0x06007E20 RID: 32288 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnUpdateEntity(GameObject go, int dataIndex)
			{
			}

			// Token: 0x06007E21 RID: 32289 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateView()
			{
			}

			// Token: 0x0400B68A RID: 46730
			private List<ColosseumRewardTournamentViewController.VersusBehaviour.Data> dataList;

			// Token: 0x02001072 RID: 4210
			private class Data
			{
				// Token: 0x06007E22 RID: 32290 RVA: 0x00002739 File Offset: 0x00000939
				public Data(int itemCategory, int itemID, int needToken, int quantity, bool isPeriod, bool received)
				{
				}

				// Token: 0x0400B68B RID: 46731
				internal readonly int itemCategory;

				// Token: 0x0400B68C RID: 46732
				internal readonly int itemID;

				// Token: 0x0400B68D RID: 46733
				internal readonly int needToken;

				// Token: 0x0400B68E RID: 46734
				internal readonly int quantity;

				// Token: 0x0400B68F RID: 46735
				internal bool isPeriod;

				// Token: 0x0400B690 RID: 46736
				internal bool received;
			}
		}
	}
}
