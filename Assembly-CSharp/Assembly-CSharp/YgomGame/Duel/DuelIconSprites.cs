using System;
using UnityEngine;
using YgomGame.Card;

namespace YgomGame.Duel
{
	// Token: 0x02000D7B RID: 3451
	public class DuelIconSprites : CardIconSprites
	{
		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x0600653A RID: 25914 RVA: 0x0000216A File Offset: 0x0000036A
		public new static DuelIconSprites instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600653B RID: 25915 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetCircle(bool myself)
		{
			return null;
		}

		// Token: 0x0600653C RID: 25916 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetPosIcon(int player, int position)
		{
			return null;
		}

		// Token: 0x0600653D RID: 25917 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetLevelIcon()
		{
			return null;
		}

		// Token: 0x0600653E RID: 25918 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetRankIcon()
		{
			return null;
		}

		// Token: 0x0600653F RID: 25919 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetScaleIcon()
		{
			return null;
		}

		// Token: 0x06006540 RID: 25920 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetTunerIcon()
		{
			return null;
		}

		// Token: 0x06006541 RID: 25921 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetOverlayIcon()
		{
			return null;
		}

		// Token: 0x06006542 RID: 25922 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetFieldAreaIcon(int player)
		{
			return null;
		}

		// Token: 0x06006543 RID: 25923 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetFieldZoneIcon()
		{
			return null;
		}

		// Token: 0x06006544 RID: 25924 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetCheckTimingIcon()
		{
			return null;
		}

		// Token: 0x06006545 RID: 25925 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetArrowIcon(bool isbattlearrow)
		{
			return null;
		}

		// Token: 0x06006546 RID: 25926 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetCounterIcon(Engine.CounterType counter)
		{
			return null;
		}

		// Token: 0x06006547 RID: 25927 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetDiceIcon(int dicenum, bool isnear)
		{
			return null;
		}

		// Token: 0x06006548 RID: 25928 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetDuelLogIcon(LOGACTIONTYPE type)
		{
			return null;
		}

		// Token: 0x06006549 RID: 25929 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetNumberIcon(int number)
		{
			return null;
		}

		// Token: 0x0600654A RID: 25930 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetNumberIconForDuelChain(int number)
		{
			return null;
		}

		// Token: 0x0600654B RID: 25931 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetDisableIcon()
		{
			return null;
		}

		// Token: 0x0600654C RID: 25932 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetUnAttackableIcon()
		{
			return null;
		}

		// Token: 0x0600654D RID: 25933 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetBattleStepIcon(Engine.StepType step)
		{
			return null;
		}

		// Token: 0x0600654E RID: 25934 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetDamageStepIcon(Engine.DmgStepType step)
		{
			return null;
		}

		// Token: 0x0600654F RID: 25935 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetListTypeIcon(GenericCardListController.ListType type)
		{
			return null;
		}

		// Token: 0x06006550 RID: 25936 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetCardSelectionListGroupBg(int index)
		{
			return null;
		}

		// Token: 0x06006551 RID: 25937 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetCoinIcon(bool face)
		{
			return null;
		}

		// Token: 0x04009F69 RID: 40809
		private static DuelIconSprites m_Instance;

		// Token: 0x04009F6A RID: 40810
		private const string PATH = "Duel/ScriptableObject/DuelIconObject";

		// Token: 0x04009F6B RID: 40811
		[SerializeField]
		private Sprite[] circle;

		// Token: 0x04009F6C RID: 40812
		[SerializeField]
		private DuelIconSprites.PositionIconable[] positionIcons;

		// Token: 0x04009F6D RID: 40813
		[SerializeField]
		private Sprite iconFieldAreaNear;

		// Token: 0x04009F6E RID: 40814
		[SerializeField]
		private Sprite iconFieldAreaFar;

		// Token: 0x04009F6F RID: 40815
		[SerializeField]
		private Sprite iconFieldZone;

		// Token: 0x04009F70 RID: 40816
		[SerializeField]
		private Sprite iconCheckTiming;

		// Token: 0x04009F71 RID: 40817
		[SerializeField]
		private Sprite iconTuner;

		// Token: 0x04009F72 RID: 40818
		[SerializeField]
		private Sprite iconOverlay;

		// Token: 0x04009F73 RID: 40819
		[SerializeField]
		private Sprite iconCardMoveArrow;

		// Token: 0x04009F74 RID: 40820
		[SerializeField]
		private Sprite iconAttackArrow;

		// Token: 0x04009F75 RID: 40821
		[SerializeField]
		private Sprite iconCslGroupBgI;

		// Token: 0x04009F76 RID: 40822
		[SerializeField]
		private Sprite iconCslGroupBgO;

		// Token: 0x04009F77 RID: 40823
		[SerializeField]
		private Sprite iconCslGroupBgL;

		// Token: 0x04009F78 RID: 40824
		[SerializeField]
		private Sprite iconCslGroupBgR;

		// Token: 0x04009F79 RID: 40825
		[SerializeField]
		private Sprite iconLevel;

		// Token: 0x04009F7A RID: 40826
		[SerializeField]
		private Sprite iconRank;

		// Token: 0x04009F7B RID: 40827
		[SerializeField]
		private Sprite iconScale;

		// Token: 0x04009F7C RID: 40828
		[SerializeField]
		private DuelIconSprites.CounterIconTable[] counterIcons;

		// Token: 0x04009F7D RID: 40829
		[SerializeField]
		private Sprite disableIcon;

		// Token: 0x04009F7E RID: 40830
		[SerializeField]
		private Sprite unAttackableIcon;

		// Token: 0x04009F7F RID: 40831
		[SerializeField]
		private DuelIconSprites.DuelLogIconTable[] duellogIcons;

		// Token: 0x04009F80 RID: 40832
		[SerializeField]
		private DuelIconSprites.NumIconTable[] numberIcons;

		// Token: 0x04009F81 RID: 40833
		[SerializeField]
		private DuelIconSprites.NumIconTable[] numberIconsForDuelChain;

		// Token: 0x04009F82 RID: 40834
		[SerializeField]
		private DuelIconSprites.BattleStepIcon[] battleStepIcons;

		// Token: 0x04009F83 RID: 40835
		[SerializeField]
		private DuelIconSprites.DamageStepIcon[] damageStepIcons;

		// Token: 0x04009F84 RID: 40836
		[SerializeField]
		private DuelIconSprites.ListTypeIcon[] listTypeIcon;

		// Token: 0x04009F85 RID: 40837
		[SerializeField]
		private DuelIconSprites.DiceIconTable[] diceIcons;

		// Token: 0x04009F86 RID: 40838
		[SerializeField]
		private DuelIconSprites.CoinIconTable[] coinIcons;

		// Token: 0x02000D7C RID: 3452
		[Serializable]
		public struct PositionIconable
		{
			// Token: 0x04009F87 RID: 40839
			public bool player;

			// Token: 0x04009F88 RID: 40840
			public int position;

			// Token: 0x04009F89 RID: 40841
			public Sprite icon;
		}

		// Token: 0x02000D7D RID: 3453
		[Serializable]
		public struct CounterIconTable
		{
			// Token: 0x04009F8A RID: 40842
			public Engine.CounterType counter;

			// Token: 0x04009F8B RID: 40843
			public Sprite icon;
		}

		// Token: 0x02000D7E RID: 3454
		[Serializable]
		public struct DuelLogIconTable
		{
			// Token: 0x04009F8C RID: 40844
			public LOGACTIONTYPE type;

			// Token: 0x04009F8D RID: 40845
			public Sprite icon;
		}

		// Token: 0x02000D7F RID: 3455
		[Serializable]
		public struct NumIconTable
		{
			// Token: 0x04009F8E RID: 40846
			public int number;

			// Token: 0x04009F8F RID: 40847
			public Sprite icon;
		}

		// Token: 0x02000D80 RID: 3456
		[Serializable]
		public struct BattleStepIcon
		{
			// Token: 0x04009F90 RID: 40848
			public Engine.StepType step;

			// Token: 0x04009F91 RID: 40849
			public Sprite icon;
		}

		// Token: 0x02000D81 RID: 3457
		[Serializable]
		public struct DamageStepIcon
		{
			// Token: 0x04009F92 RID: 40850
			public Engine.DmgStepType step;

			// Token: 0x04009F93 RID: 40851
			public Sprite icon;
		}

		// Token: 0x02000D82 RID: 3458
		[Serializable]
		public struct ListTypeIcon
		{
			// Token: 0x04009F94 RID: 40852
			public GenericCardListController.ListType type;

			// Token: 0x04009F95 RID: 40853
			public Sprite icon;
		}

		// Token: 0x02000D83 RID: 3459
		[Serializable]
		public struct DiceIconTable
		{
			// Token: 0x04009F96 RID: 40854
			public int dicenum;

			// Token: 0x04009F97 RID: 40855
			public bool isnear;

			// Token: 0x04009F98 RID: 40856
			public Sprite sprite;
		}

		// Token: 0x02000D84 RID: 3460
		[Serializable]
		public struct CoinIconTable
		{
			// Token: 0x04009F99 RID: 40857
			public bool face;

			// Token: 0x04009F9A RID: 40858
			public Sprite sprite;
		}
	}
}
