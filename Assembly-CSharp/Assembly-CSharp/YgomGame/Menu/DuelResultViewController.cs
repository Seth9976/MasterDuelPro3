using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using YgomGame.Dialog.CommonDialog;
using YgomGame.Duel;
using YgomGame.Effect;
using YgomSystem.ElementSystem;
using YgomSystem.Timeline;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Menu
{
	// Token: 0x02000A57 RID: 2647
	public class DuelResultViewController : BaseMenuViewController
	{
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06004D65 RID: 19813 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06004D66 RID: 19814 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int selectorPriorityAddRange
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06004D67 RID: 19815 RVA: 0x000029CC File Offset: 0x00000BCC
		private int GetRemainAddRange()
		{
			return 0;
		}

		// Token: 0x06004D68 RID: 19816 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004D69 RID: 19817 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004D6A RID: 19818 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06004D6B RID: 19819 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnFocusChanged(bool setfocus)
		{
		}

		// Token: 0x06004D6C RID: 19820 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerator StartResult()
		{
			return null;
		}

		// Token: 0x06004D6D RID: 19821 RVA: 0x0000216D File Offset: 0x0000036D
		private void ToRetryDuel(Util.GameMode gameMode, int tid = 0)
		{
		}

		// Token: 0x06004D6E RID: 19822 RVA: 0x0000216D File Offset: 0x0000036D
		private void ToRetryMatching(PvpMenuDefine.MatchingType matchingType, int tid = 0)
		{
		}

		// Token: 0x06004D6F RID: 19823 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClickRetry(Util.GameMode gameMode, int tournamentId)
		{
		}

		// Token: 0x06004D70 RID: 19824 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClickSave(Util.GameMode mode, long did, int eventID)
		{
		}

		// Token: 0x04008B2B RID: 35627
		private readonly string AVATARMODEL_ROOT_LABEL;

		// Token: 0x04008B2C RID: 35628
		private readonly string LEVELINFO_LABEL;

		// Token: 0x04008B2D RID: 35629
		private readonly string RESULTINFO_SCORE_REWARD_LABEL;

		// Token: 0x04008B2E RID: 35630
		private readonly string ROOT_RANK_LABEL;

		// Token: 0x04008B2F RID: 35631
		private readonly string BTN_RETRY_LABEL;

		// Token: 0x04008B30 RID: 35632
		private readonly string BTN_SAVE_LABEL;

		// Token: 0x04008B31 RID: 35633
		private readonly string BTN_BACK_LABEL;

		// Token: 0x04008B32 RID: 35634
		private readonly string SCROLLREWARD_LABEL;

		// Token: 0x04008B33 RID: 35635
		private readonly string TEMPLATENORMAL_LABEL;

		// Token: 0x04008B34 RID: 35636
		private readonly string TEMPLATERARE_LABEL;

		// Token: 0x04008B35 RID: 35637
		[SerializeField]
		private DuelResultViewController.LevelUpPlayer m_LevelupPlayer;

		// Token: 0x04008B36 RID: 35638
		[SerializeField]
		private DuelResultViewController.GetScoreReward m_GetScoreReward;

		// Token: 0x04008B37 RID: 35639
		private Transform m_AvatarModelRoot;

		// Token: 0x04008B38 RID: 35640
		private Character2D m_AvatarModel;

		// Token: 0x04008B39 RID: 35641
		private SelectionButton RetryButton;

		// Token: 0x04008B3A RID: 35642
		private SelectionButton SaveButton;

		// Token: 0x04008B3B RID: 35643
		private SelectionButton BackButton;

		// Token: 0x04008B3C RID: 35644
		private IEnumerator yAnimateRank;

		// Token: 0x04008B3D RID: 35645
		private Util.GameMode m_GameMode;

		// Token: 0x04008B3E RID: 35646
		private static IEnumerator coroutine;

		// Token: 0x04008B3F RID: 35647
		private int remainAddRangeCount;

		// Token: 0x02000A58 RID: 2648
		private class ClassChange : DuelResultViewController.TweenResultPlayer
		{
			// Token: 0x1700072F RID: 1839
			// (get) Token: 0x06004D72 RID: 19826 RVA: 0x000029CC File Offset: 0x00000BCC
			protected override bool isFinish
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06004D73 RID: 19827 RVA: 0x0000216A File Offset: 0x0000036A
			public override IEnumerator Play()
			{
				return null;
			}

			// Token: 0x06004D74 RID: 19828 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void OnClickClose()
			{
			}

			// Token: 0x06004D75 RID: 19829 RVA: 0x0000216D File Offset: 0x0000036D
			public override void ImportWork(object workData)
			{
			}

			// Token: 0x06004D76 RID: 19830 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetTimeline(PlayableDirector timeline)
			{
			}

			// Token: 0x06004D77 RID: 19831 RVA: 0x0000216A File Offset: 0x0000036A
			private EventPlayableAsset GetEventPlayableAsset(PlayableDirector timeline)
			{
				return null;
			}

			// Token: 0x04008B40 RID: 35648
			private readonly string k_MessageTextLabel;

			// Token: 0x04008B41 RID: 35649
			private readonly string k_NextRankTextLabel;

			// Token: 0x04008B42 RID: 35650
			private readonly string k_RankChangeInfoTextLabel;

			// Token: 0x04008B43 RID: 35651
			private readonly string k_RankIconBeforeLabel;

			// Token: 0x04008B44 RID: 35652
			private readonly string k_RankIconAfterLabel;

			// Token: 0x04008B45 RID: 35653
			private bool m_IsFinish;

			// Token: 0x04008B46 RID: 35654
			private PlayableDirector m_Timeline;
		}

		// Token: 0x02000A59 RID: 2649
		private class CommonDialogResultPlayer : DuelResultViewController.IResultPlayer
		{
			// Token: 0x06004D79 RID: 19833 RVA: 0x0000216A File Offset: 0x0000036A
			public static DuelResultViewController.CommonDialogResultPlayer CreateConfirm(string title, string message, string buttonLabel)
			{
				return null;
			}

			// Token: 0x06004D7A RID: 19834 RVA: 0x0000216A File Offset: 0x0000036A
			public IEnumerator Play()
			{
				return null;
			}

			// Token: 0x06004D7B RID: 19835 RVA: 0x0000216D File Offset: 0x0000036D
			private void DialogCallback()
			{
			}

			// Token: 0x04008B47 RID: 35655
			private IEntryData[] m_DialogEntries;

			// Token: 0x04008B48 RID: 35656
			private bool m_IsFinish;
		}

		// Token: 0x02000A5A RID: 2650
		private class DLvChange : DuelResultViewController.TweenResultPlayer
		{
			// Token: 0x17000730 RID: 1840
			// (get) Token: 0x06004D7D RID: 19837 RVA: 0x000029CC File Offset: 0x00000BCC
			protected override bool isFinish
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06004D7E RID: 19838 RVA: 0x0000216A File Offset: 0x0000036A
			public override IEnumerator Play()
			{
				return null;
			}

			// Token: 0x06004D7F RID: 19839 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void OnClickClose()
			{
			}

			// Token: 0x06004D80 RID: 19840 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetTimeline(PlayableDirector timeline)
			{
			}

			// Token: 0x06004D81 RID: 19841 RVA: 0x0000216D File Offset: 0x0000036D
			public override void ImportWork(object workData)
			{
			}

			// Token: 0x06004D82 RID: 19842 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetGameMode(Util.GameMode gameMode)
			{
			}

			// Token: 0x06004D83 RID: 19843 RVA: 0x0000216A File Offset: 0x0000036A
			private EventPlayableAsset GetEventPlayableAsset(PlayableDirector timeline)
			{
				return null;
			}

			// Token: 0x04008B49 RID: 35657
			private readonly string TEXT_MESSAGE_LABEL;

			// Token: 0x04008B4A RID: 35658
			private readonly string TEXT_RANK_BEFORE_LABEL;

			// Token: 0x04008B4B RID: 35659
			private readonly string TEXT_RANK_AFTER_LABEL;

			// Token: 0x04008B4C RID: 35660
			private bool m_IsFinish;

			// Token: 0x04008B4D RID: 35661
			private PlayableDirector m_Timeline;

			// Token: 0x04008B4E RID: 35662
			private Util.GameMode mode;

			// Token: 0x04008B4F RID: 35663
			private string modeName;
		}

		// Token: 0x02000A5B RID: 2651
		[Serializable]
		private class GetScoreReward : DuelResultViewController.TweenResultPlayer
		{
			// Token: 0x17000731 RID: 1841
			// (get) Token: 0x06004D85 RID: 19845 RVA: 0x000029CC File Offset: 0x00000BCC
			protected override bool isFinish
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000732 RID: 1842
			// (get) Token: 0x06004D86 RID: 19846 RVA: 0x0000216A File Offset: 0x0000036A
			protected override Selector selector
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06004D87 RID: 19847 RVA: 0x0000216D File Offset: 0x0000036D
			public override void Initialize(ElementObjectManager eom, int selectorGroupPriority)
			{
			}

			// Token: 0x06004D88 RID: 19848 RVA: 0x0000216D File Offset: 0x0000036D
			public override void ImportWork(object workData)
			{
			}

			// Token: 0x06004D89 RID: 19849 RVA: 0x0000216A File Offset: 0x0000036A
			public override IEnumerator Play()
			{
				return null;
			}

			// Token: 0x06004D8A RID: 19850 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void OnClickClose()
			{
			}

			// Token: 0x04008B50 RID: 35664
			[SerializeField]
			private float m_OpenChestIntervalSeconds;

			// Token: 0x04008B51 RID: 35665
			[SerializeField]
			private BezierMotionContainer bezierCraftCreate;

			// Token: 0x04008B52 RID: 35666
			private readonly string k_PrefabPathTrailEffect;

			// Token: 0x04008B53 RID: 35667
			private readonly string k_EImgTotalChest;

			// Token: 0x04008B54 RID: 35668
			private readonly string k_ETextEmpty;

			// Token: 0x04008B55 RID: 35669
			private int m_TotalScore;

			// Token: 0x04008B56 RID: 35670
			private readonly int m_NeedScore;

			// Token: 0x04008B57 RID: 35671
			private DuelResultViewController.ResultInfoItems m_ResultInfoItem;

			// Token: 0x04008B58 RID: 35672
			private ChainedBezierMotion motion;

			// Token: 0x04008B59 RID: 35673
			private EffectHandler m_EffectTrail;

			// Token: 0x04008B5A RID: 35674
			private Transform m_OriginTrans;

			// Token: 0x04008B5B RID: 35675
			private Dictionary<int, object> m_RewardMap;

			// Token: 0x04008B5C RID: 35676
			private bool m_isFinish;
		}

		// Token: 0x02000A5C RID: 2652
		public interface IResultPlayer
		{
			// Token: 0x06004D8C RID: 19852
			IEnumerator Play();
		}

		// Token: 0x02000A5D RID: 2653
		private class ItemReceiveDialogResultPlayer : DuelResultViewController.IResultPlayer
		{
			// Token: 0x06004D8D RID: 19853 RVA: 0x0000216A File Offset: 0x0000036A
			public static DuelResultViewController.ItemReceiveDialogResultPlayer CreateItemReceive(string title, EntryItemListData receiveItemListData, bool isSendPresent)
			{
				return null;
			}

			// Token: 0x06004D8E RID: 19854 RVA: 0x0000216A File Offset: 0x0000036A
			public IEnumerator Play()
			{
				return null;
			}

			// Token: 0x06004D8F RID: 19855 RVA: 0x0000216D File Offset: 0x0000036D
			private void DialogCallback()
			{
			}

			// Token: 0x04008B5D RID: 35677
			private bool m_IsFinish;

			// Token: 0x04008B5E RID: 35678
			private string title;

			// Token: 0x04008B5F RID: 35679
			private EntryItemListData receiveItemListData;

			// Token: 0x04008B60 RID: 35680
			private bool isSendPresent;
		}

		// Token: 0x02000A5E RID: 2654
		[Serializable]
		private class LevelUpPlayer : DuelResultViewController.TweenResultPlayer
		{
			// Token: 0x17000733 RID: 1843
			// (get) Token: 0x06004D91 RID: 19857 RVA: 0x000029CC File Offset: 0x00000BCC
			protected override bool isFinish
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000734 RID: 1844
			// (get) Token: 0x06004D92 RID: 19858 RVA: 0x0000216A File Offset: 0x0000036A
			protected override Selector selector
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06004D93 RID: 19859 RVA: 0x0000216D File Offset: 0x0000036D
			public override void Initialize(ElementObjectManager eom, int selectorGroupPriority)
			{
			}

			// Token: 0x06004D94 RID: 19860 RVA: 0x0000216D File Offset: 0x0000036D
			public override void ImportWork(object workData)
			{
			}

			// Token: 0x06004D95 RID: 19861 RVA: 0x0000216A File Offset: 0x0000036A
			public override IEnumerator Play()
			{
				return null;
			}

			// Token: 0x06004D96 RID: 19862 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void OnClickClose()
			{
			}

			// Token: 0x04008B61 RID: 35681
			[SerializeField]
			private float m_GaugeSpeedPerUnitLevel;

			// Token: 0x04008B62 RID: 35682
			private float m_increasedExpAmount;

			// Token: 0x04008B63 RID: 35683
			private int m_bLevel;

			// Token: 0x04008B64 RID: 35684
			private float m_bExpPercent;

			// Token: 0x04008B65 RID: 35685
			private int m_aLevel;

			// Token: 0x04008B66 RID: 35686
			private int m_aExp;

			// Token: 0x04008B67 RID: 35687
			private int m_aNeedExp;

			// Token: 0x04008B68 RID: 35688
			private bool m_isFinish;
		}

		// Token: 0x02000A5F RID: 2655
		private class RankChange : DuelResultViewController.TweenResultPlayer
		{
			// Token: 0x17000735 RID: 1845
			// (get) Token: 0x06004D98 RID: 19864 RVA: 0x000029CC File Offset: 0x00000BCC
			protected override bool isFinish
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06004D99 RID: 19865 RVA: 0x0000216A File Offset: 0x0000036A
			public override IEnumerator Play()
			{
				return null;
			}

			// Token: 0x06004D9A RID: 19866 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void OnClickClose()
			{
			}

			// Token: 0x06004D9B RID: 19867 RVA: 0x0000216D File Offset: 0x0000036D
			public override void ImportWork(object workData)
			{
			}

			// Token: 0x06004D9C RID: 19868 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetTimeline(PlayableDirector timeline)
			{
			}

			// Token: 0x06004D9D RID: 19869 RVA: 0x0000216A File Offset: 0x0000036A
			private EventPlayableAsset GetEventPlayableAsset(PlayableDirector timeline)
			{
				return null;
			}

			// Token: 0x04008B69 RID: 35689
			private readonly string k_MessageTextLabel;

			// Token: 0x04008B6A RID: 35690
			private readonly string k_NextRankTextLabel;

			// Token: 0x04008B6B RID: 35691
			private readonly string k_RankChangeInfoTextLabel;

			// Token: 0x04008B6C RID: 35692
			private readonly string k_RankIconBeforeLabel;

			// Token: 0x04008B6D RID: 35693
			private readonly string k_RankIconAfterLabel;

			// Token: 0x04008B6E RID: 35694
			private bool m_IsFinish;

			// Token: 0x04008B6F RID: 35695
			private PlayableDirector m_Timeline;
		}

		// Token: 0x02000A60 RID: 2656
		public class ResultInfoItems
		{
			// Token: 0x06004D9F RID: 19871 RVA: 0x0000216D File Offset: 0x0000036D
			public void Initialize(ElementObjectManager eom)
			{
			}

			// Token: 0x06004DA0 RID: 19872 RVA: 0x0000216D File Offset: 0x0000036D
			private void InitializeScroll()
			{
			}

			// Token: 0x06004DA1 RID: 19873 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnCreatedEntity(GameObject go)
			{
			}

			// Token: 0x06004DA2 RID: 19874 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnUpdateEntity(GameObject go, int index)
			{
			}

			// Token: 0x06004DA3 RID: 19875 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetItems(List<DuelResultViewController.ResultInfoItems.Entity> entitys)
			{
			}

			// Token: 0x06004DA4 RID: 19876 RVA: 0x000F4AA0 File Offset: 0x000F2CA0
			public Vector3 GetCurrentEntityPosition(int correctionValue = 0)
			{
				return default(Vector3);
			}

			// Token: 0x06004DA5 RID: 19877 RVA: 0x000F4AB8 File Offset: 0x000F2CB8
			public Quaternion GetCurrentEntityRotation(int correctionValue = 0)
			{
				return default(Quaternion);
			}

			// Token: 0x06004DA6 RID: 19878 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetEntityCount()
			{
				return 0;
			}

			// Token: 0x06004DA7 RID: 19879 RVA: 0x0000216D File Offset: 0x0000036D
			public void DispChest()
			{
			}

			// Token: 0x06004DA8 RID: 19880 RVA: 0x0000216D File Offset: 0x0000036D
			public void DispRemainChest()
			{
			}

			// Token: 0x06004DA9 RID: 19881 RVA: 0x0000216D File Offset: 0x0000036D
			public void OpenChest()
			{
			}

			// Token: 0x06004DAA RID: 19882 RVA: 0x0000216D File Offset: 0x0000036D
			public void OpenRemainChest()
			{
			}

			// Token: 0x06004DAB RID: 19883 RVA: 0x0000216D File Offset: 0x0000036D
			public void ScrollFirstIndex()
			{
			}

			// Token: 0x04008B70 RID: 35696
			private readonly string k_ItemNumLabel;

			// Token: 0x04008B71 RID: 35697
			private readonly string k_ItemIconLabel;

			// Token: 0x04008B72 RID: 35698
			private readonly string k_ItemOpenLabel;

			// Token: 0x04008B73 RID: 35699
			private readonly string k_ItemUnopenLabel;

			// Token: 0x04008B74 RID: 35700
			private readonly string k_ScrollLabel;

			// Token: 0x04008B75 RID: 35701
			private readonly string k_BtnLabel;

			// Token: 0x04008B76 RID: 35702
			private readonly string k_PrefabPathDispEffect;

			// Token: 0x04008B77 RID: 35703
			private readonly string k_PrefabPathOpenEffect;

			// Token: 0x04008B78 RID: 35704
			private ElementObjectManager m_Eom;

			// Token: 0x04008B79 RID: 35705
			private InfinityScrollView m_Isv;

			// Token: 0x04008B7A RID: 35706
			private List<DuelResultViewController.ResultInfoItems.Entity> m_EntityList;

			// Token: 0x04008B7B RID: 35707
			private int m_DispCount;

			// Token: 0x04008B7C RID: 35708
			private int m_OpenCount;

			// Token: 0x04008B7D RID: 35709
			private bool isMobile;

			// Token: 0x02000A61 RID: 2657
			public enum ChestType
			{
				// Token: 0x04008B7F RID: 35711
				NORMAL = 1,
				// Token: 0x04008B80 RID: 35712
				RARE
			}

			// Token: 0x02000A62 RID: 2658
			public enum ChestStatus
			{
				// Token: 0x04008B82 RID: 35714
				INVISIBLE,
				// Token: 0x04008B83 RID: 35715
				UNOPEN,
				// Token: 0x04008B84 RID: 35716
				OPEN
			}

			// Token: 0x02000A63 RID: 2659
			public class Entity
			{
				// Token: 0x06004DAD RID: 19885 RVA: 0x00002739 File Offset: 0x00000939
				public Entity(int num, int itemId, DuelResultViewController.ResultInfoItems.ChestType type)
				{
				}

				// Token: 0x04008B85 RID: 35717
				public int m_Num;

				// Token: 0x04008B86 RID: 35718
				public int m_ItemId;

				// Token: 0x04008B87 RID: 35719
				public readonly DuelResultViewController.ResultInfoItems.ChestType m_Type;

				// Token: 0x04008B88 RID: 35720
				public DuelResultViewController.ResultInfoItems.ChestStatus m_Status;
			}
		}

		// Token: 0x02000A64 RID: 2660
		public class ResultInfoScores
		{
			// Token: 0x06004DAE RID: 19886 RVA: 0x0000216D File Offset: 0x0000036D
			public void Initialize(ElementObjectManager eom)
			{
			}

			// Token: 0x06004DAF RID: 19887 RVA: 0x0000216D File Offset: 0x0000036D
			private void InitializeScroll()
			{
			}

			// Token: 0x06004DB0 RID: 19888 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnUpdateEntity(GameObject go, int index)
			{
			}

			// Token: 0x06004DB1 RID: 19889 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetTotalPoint(int point)
			{
			}

			// Token: 0x06004DB2 RID: 19890 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetOtherPoints(List<DuelResultViewController.ResultInfoScores.Entity> entitys)
			{
			}

			// Token: 0x04008B89 RID: 35721
			private readonly string k_TotalPointLabel;

			// Token: 0x04008B8A RID: 35722
			private readonly string k_TotalPointTextLabel;

			// Token: 0x04008B8B RID: 35723
			private readonly string k_ScrollLabel;

			// Token: 0x04008B8C RID: 35724
			private readonly string k_ItemPointLabel;

			// Token: 0x04008B8D RID: 35725
			private readonly string k_ItemTextLabel;

			// Token: 0x04008B8E RID: 35726
			private ElementObjectManager m_Eom;

			// Token: 0x04008B8F RID: 35727
			private InfinityScrollView m_Isv;

			// Token: 0x04008B90 RID: 35728
			private List<DuelResultViewController.ResultInfoScores.Entity> m_EntityList;

			// Token: 0x02000A65 RID: 2661
			public class Entity
			{
				// Token: 0x06004DB4 RID: 19892 RVA: 0x00002739 File Offset: 0x00000939
				public Entity(string label, int point)
				{
				}

				// Token: 0x06004DB5 RID: 19893 RVA: 0x00002739 File Offset: 0x00000939
				public Entity(string label, int point, Color color)
				{
				}

				// Token: 0x04008B91 RID: 35729
				public string label;

				// Token: 0x04008B92 RID: 35730
				public int point;

				// Token: 0x04008B93 RID: 35731
				public Color color;
			}
		}

		// Token: 0x02000A66 RID: 2662
		private class ReviewResultPlayer : DuelResultViewController.IResultPlayer
		{
			// Token: 0x06004DB6 RID: 19894 RVA: 0x0000216A File Offset: 0x0000036A
			public static DuelResultViewController.ReviewResultPlayer OpenReview()
			{
				return null;
			}

			// Token: 0x06004DB7 RID: 19895 RVA: 0x0000216A File Offset: 0x0000036A
			public IEnumerator Play()
			{
				return null;
			}

			// Token: 0x04008B94 RID: 35732
			private bool m_IsFinish;
		}

		// Token: 0x02000A67 RID: 2663
		private abstract class TweenResultPlayer : DuelResultViewController.IResultPlayer
		{
			// Token: 0x17000736 RID: 1846
			// (get) Token: 0x06004DB9 RID: 19897 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual Selector selector
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000737 RID: 1847
			// (get) Token: 0x06004DBA RID: 19898 RVA: 0x000029CC File Offset: 0x00000BCC
			protected virtual bool isFinish
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06004DBB RID: 19899 RVA: 0x0000216D File Offset: 0x0000036D
			public virtual void Initialize(ElementObjectManager eom, int selectorGroupPriority)
			{
			}

			// Token: 0x06004DBC RID: 19900
			public abstract void ImportWork(object workData);

			// Token: 0x06004DBD RID: 19901 RVA: 0x0000216A File Offset: 0x0000036A
			public virtual IEnumerator Play()
			{
				return null;
			}

			// Token: 0x06004DBE RID: 19902 RVA: 0x0000216D File Offset: 0x0000036D
			protected virtual void OnClickClose()
			{
			}

			// Token: 0x04008B95 RID: 35733
			protected readonly string k_TweenOpenKey;

			// Token: 0x04008B96 RID: 35734
			protected readonly string k_TweenCloseKey;

			// Token: 0x04008B97 RID: 35735
			protected readonly string k_CloseButtonLabel;

			// Token: 0x04008B98 RID: 35736
			protected ElementObjectManager m_Eom;
		}
	}
}
