using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000E9E RID: 3742
	public abstract class HandCardPlace : CardPlace
	{
		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06006CE9 RID: 27881
		public abstract int handCardNum { get; }

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06006CEA RID: 27882
		protected abstract int selectedIndex { get; }

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06006CEB RID: 27883
		protected abstract int decidedIndex { get; }

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06006CEC RID: 27884 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int selectedViewIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06006CED RID: 27885 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int decidedViewIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06006CEE RID: 27886
		protected abstract Vector3 locatorOffsetTargetSelecting { get; }

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06006CEF RID: 27887
		protected abstract Vector3 locatorOffsetTargetDeciding { get; }

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06006CF0 RID: 27888
		public abstract bool isAllOpen { get; }

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06006CF1 RID: 27889 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x06006CF2 RID: 27890 RVA: 0x0000216D File Offset: 0x0000036D
		protected float angleOffset
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06006CF3 RID: 27891 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected float angleOffsetOrg
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06006CF4 RID: 27892 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected float clampedAngleOffset
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06006CF5 RID: 27893 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual HandCardManager.ViewSortMode sortMode
		{
			[CompilerGenerated]
			get
			{
				return HandCardManager.ViewSortMode.EngineIndex;
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06006CF6 RID: 27894 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int loadStartIdx
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06006CF7 RID: 27895 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int loadIndexIncValue
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06006CF8 RID: 27896 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool loadIsOver
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006CF9 RID: 27897
		protected abstract void HighlightImpl(int index, SharedDefinition.ActivateAura auraType);

		// Token: 0x06006CFA RID: 27898 RVA: 0x000F4E3E File Offset: 0x000F303E
		public HandCardPlace(DuelFieldBase duelField, int team, int position)
			: base(null, 0, 0)
		{
		}

		// Token: 0x06006CFB RID: 27899 RVA: 0x0000216D File Offset: 0x0000036D
		protected void CreateHandCardObject(string obj_name)
		{
		}

		// Token: 0x06006CFC RID: 27900 RVA: 0x000F6024 File Offset: 0x000F4224
		public override Vector3 GetScreenPos(int index, Vector2 ofsRate)
		{
			return default(Vector3);
		}

		// Token: 0x06006CFD RID: 27901 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnUpdate()
		{
		}

		// Token: 0x06006CFE RID: 27902 RVA: 0x0000216D File Offset: 0x0000036D
		private void IdleStep()
		{
		}

		// Token: 0x06006CFF RID: 27903 RVA: 0x0000216A File Offset: 0x0000036A
		public override CardLocator GetCardLocator(int index, bool create = true, bool insert = false)
		{
			return null;
		}

		// Token: 0x06006D00 RID: 27904 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupSelectionItem(SelectionButton item)
		{
		}

		// Token: 0x06006D01 RID: 27905 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetupAngleOffset(int viewIndex)
		{
		}

		// Token: 0x06006D02 RID: 27906 RVA: 0x000029CC File Offset: 0x00000BCC
		private int GetSelectionItemIndex(SelectionItem item)
		{
			return 0;
		}

		// Token: 0x06006D03 RID: 27907 RVA: 0x000029CC File Offset: 0x00000BCC
		private int GetSelectionItemViewIndex(SelectionItem item)
		{
			return 0;
		}

		// Token: 0x06006D04 RID: 27908
		protected abstract void SelectNextItem(int index);

		// Token: 0x06006D05 RID: 27909
		protected abstract void SelectPrevItem(int index);

		// Token: 0x06006D06 RID: 27910 RVA: 0x0000216D File Offset: 0x0000036D
		protected void UpdateSelectionItem(GameObject actItemParent, SelectionItem actItem, CardLocator locator)
		{
		}

		// Token: 0x06006D07 RID: 27911 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateLocatorOffset(CardLocator locator, bool immediate)
		{
		}

		// Token: 0x06006D08 RID: 27912 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateLocatorOffsetAll(bool immediate)
		{
		}

		// Token: 0x06006D09 RID: 27913 RVA: 0x0000216A File Offset: 0x0000036A
		protected override CardLocator OnEnter(CardRoot cardRoot, int index, bool reqUpdateIndices)
		{
			return null;
		}

		// Token: 0x06006D0A RID: 27914 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool OnLeave(CardRoot cardRoot, int index, bool reqUpdateIndices)
		{
			return false;
		}

		// Token: 0x06006D0B RID: 27915 RVA: 0x0000216D File Offset: 0x0000036D
		private void ArrangementLocators()
		{
		}

		// Token: 0x06006D0C RID: 27916 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnRegister(CardRoot cardRoot, int index, bool withEffect)
		{
		}

		// Token: 0x06006D0D RID: 27917 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnUnregister(CardRoot cardRoot, int index)
		{
		}

		// Token: 0x06006D0E RID: 27918 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ShuffleImpl(Action onFinished)
		{
		}

		// Token: 0x06006D0F RID: 27919 RVA: 0x0000216D File Offset: 0x0000036D
		protected void UpdateShuffle()
		{
		}

		// Token: 0x06006D10 RID: 27920 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void EndSacrificeTargetEffectImpl(int index, Action onFinished)
		{
		}

		// Token: 0x06006D11 RID: 27921 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ReqHighlightImpl(bool available, uint cmdBit, Action onFinished)
		{
		}

		// Token: 0x06006D12 RID: 27922 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ReqDecideEffectImpl(int index, Action onFinished)
		{
		}

		// Token: 0x06006D13 RID: 27923 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void FlipTurnImpl(int index, bool isFace, bool isAttack, bool immediate, Action onFinished)
		{
		}

		// Token: 0x06006D14 RID: 27924 RVA: 0x000F603C File Offset: 0x000F423C
		public override Vector3 GetTypicalPos()
		{
			return default(Vector3);
		}

		// Token: 0x06006D15 RID: 27925
		public abstract int UniqueIdToIndex(int uniqueId);

		// Token: 0x06006D16 RID: 27926 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SyncEngineHandCardIndex()
		{
		}

		// Token: 0x06006D17 RID: 27927 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SyncHandCardIndex(int[] unique_ids)
		{
		}

		// Token: 0x06006D18 RID: 27928
		public abstract void GetPosture(int index, out Vector3 position, out Quaternion rotation, out float depth, int card_num = -1, bool originPosition = false);

		// Token: 0x06006D19 RID: 27929 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected float GetFlabellateAngle(int viewIndex, bool origin, bool baseAngle, bool clamp180 = true)
		{
			return 0f;
		}

		// Token: 0x06006D1A RID: 27930 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected float GetFlabellateAngle(int viewIndex, int handNum, bool origin, bool baseAngle, bool clamp180 = true)
		{
			return 0f;
		}

		// Token: 0x06006D1B RID: 27931
		public abstract void UpdateCenterPosition();

		// Token: 0x06006D1C RID: 27932 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void UpdateLocators()
		{
		}

		// Token: 0x06006D1D RID: 27933 RVA: 0x0000216D File Offset: 0x0000036D
		protected void UpdateLocator(CardLocator actLocator, int index)
		{
		}

		// Token: 0x06006D1E RID: 27934 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void SetupCardLocator(CardLocator cardLocator)
		{
		}

		// Token: 0x06006D1F RID: 27935 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCardFloatingMove(bool useFloatingPivot)
		{
		}

		// Token: 0x06006D20 RID: 27936 RVA: 0x0000216D File Offset: 0x0000036D
		private void ResetCardPivotPosition()
		{
		}

		// Token: 0x06006D21 RID: 27937 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSort(HandCardManager.ViewSortMode sort, bool select = true)
		{
		}

		// Token: 0x06006D22 RID: 27938
		protected abstract void SetSortImpl(HandCardManager.ViewSortMode sort);

		// Token: 0x06006D23 RID: 27939 RVA: 0x0000216D File Offset: 0x0000036D
		public void InsertViewIndex(int targetIndex, int insertViewIndex, bool select)
		{
		}

		// Token: 0x06006D24 RID: 27940
		protected abstract void InsertViewIndexImpl(int targetIndex, int InsertViewIndex);

		// Token: 0x06006D25 RID: 27941 RVA: 0x0000216D File Offset: 0x0000036D
		public void RefreshCardLocators(bool updateCardPosition)
		{
		}

		// Token: 0x06006D26 RID: 27942
		protected abstract void RefreshCardLocatorsImpl();

		// Token: 0x06006D27 RID: 27943 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Select(int index, bool force = false)
		{
			return false;
		}

		// Token: 0x06006D28 RID: 27944 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SelectByViewIndex(int viewIndex, bool force = false)
		{
			return false;
		}

		// Token: 0x06006D29 RID: 27945 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowStatusLabel(bool immediate, bool showDetail)
		{
		}

		// Token: 0x06006D2A RID: 27946 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideStatusLabel(bool immediate, bool finishDetail = false)
		{
		}

		// Token: 0x06006D2B RID: 27947
		public abstract void UpdateAllCardPosition();

		// Token: 0x0400A80D RID: 43021
		private HandCardPlace.ShuffleStep shuffleStep;

		// Token: 0x0400A80E RID: 43022
		protected float shuffleTimer;

		// Token: 0x0400A80F RID: 43023
		protected Action onFinishedShuffle;

		// Token: 0x0400A810 RID: 43024
		protected float flabellateAngleBase;

		// Token: 0x0400A811 RID: 43025
		protected float flabellateAngle;

		// Token: 0x0400A812 RID: 43026
		protected float flabellateAngleLimit;

		// Token: 0x0400A813 RID: 43027
		protected float rate;

		// Token: 0x0400A814 RID: 43028
		protected float flabellateRadiusPerScreen;

		// Token: 0x0400A815 RID: 43029
		private float shuffleTimeShrink;

		// Token: 0x0400A816 RID: 43030
		private float shuffleTimeExpand;

		// Token: 0x0400A817 RID: 43031
		protected Vector3 centerViewport;

		// Token: 0x0400A818 RID: 43032
		protected float centerScale;

		// Token: 0x0400A819 RID: 43033
		protected float centerScaleSmall;

		// Token: 0x0400A81A RID: 43034
		protected float centerHeight;

		// Token: 0x0400A81B RID: 43035
		protected float selectingOffsetLerp;

		// Token: 0x0400A81C RID: 43036
		protected Dictionary<HandCardManager.DispMode, Vector3> centerPositions;

		// Token: 0x0400A81D RID: 43037
		protected int preSelectIndex;

		// Token: 0x0400A81E RID: 43038
		protected int preDecideIndex;

		// Token: 0x0400A81F RID: 43039
		private float _angleOffset;

		// Token: 0x0400A820 RID: 43040
		private Vector2 preScreenPoint;

		// Token: 0x0400A821 RID: 43041
		private bool dragAngleScroll;

		// Token: 0x0400A822 RID: 43042
		protected bool isBusy;

		// Token: 0x0400A823 RID: 43043
		protected List<ValueTuple<GameObject, SelectionButton>> selectionItems;

		// Token: 0x0400A824 RID: 43044
		protected GameObject handCardObj;

		// Token: 0x0400A825 RID: 43045
		private int createdHandCardCount;

		// Token: 0x0400A826 RID: 43046
		protected SharedDefinition.Location location;

		// Token: 0x0400A827 RID: 43047
		protected HandCardManager manager;

		// Token: 0x0400A828 RID: 43048
		private Selector selector;

		// Token: 0x0400A829 RID: 43049
		private bool showDetailStatus;

		// Token: 0x0400A82A RID: 43050
		private PlaceStatusLabel statusLabel;

		// Token: 0x02000E9F RID: 3743
		private enum ShuffleStep
		{
			// Token: 0x0400A82C RID: 43052
			Idle,
			// Token: 0x0400A82D RID: 43053
			Shrink,
			// Token: 0x0400A82E RID: 43054
			Expand
		}
	}
}
