using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using YgomGame.CardPack.Open.Actor;
using YgomGame.CardPack.Open.Widget;
using YgomSystem.UI;

namespace YgomGame.CardPack.Open.Sequence
{
	// Token: 0x020010CB RID: 4299
	public class SequenceBehaviourCardsOpen : SequenceBehaviour
	{
		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x06007FA2 RID: 32674 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool isAcceptToSkipLoop
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007FA3 RID: 32675 RVA: 0x000F68C6 File Offset: 0x000F4AC6
		public SequenceBehaviourCardsOpen(SequenceBehaviourWork sequenceBehaviourWork, DrawPackData packData, bool isLast)
			: base(null)
		{
		}

		// Token: 0x06007FA4 RID: 32676 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnBegin()
		{
		}

		// Token: 0x06007FA5 RID: 32677 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool OnUpdate()
		{
			return false;
		}

		// Token: 0x06007FA6 RID: 32678 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnEnd()
		{
		}

		// Token: 0x06007FA7 RID: 32679 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckHitDraggingCard(Vector2 prevScreenPos, Vector2 currentScreenPos)
		{
		}

		// Token: 0x06007FA8 RID: 32680 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenCardDetail(CardPackCardActor cardActor)
		{
		}

		// Token: 0x06007FA9 RID: 32681 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnAddCardGroup()
		{
		}

		// Token: 0x06007FAA RID: 32682 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnAddCardGroupComplete()
		{
		}

		// Token: 0x06007FAB RID: 32683 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDownTouchArea()
		{
		}

		// Token: 0x06007FAC RID: 32684 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDownCardActor(CardPackCardActor cardActor)
		{
		}

		// Token: 0x06007FAD RID: 32685 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDragTouchArea(SelectionItem.DragStatus dragStatus, Vector2 screenPos)
		{
		}

		// Token: 0x06007FAE RID: 32686 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDragCardActor(CardPackCardActor cardActor, SelectionItem.DragStatus dragStatus, Vector2 screenPos)
		{
		}

		// Token: 0x06007FAF RID: 32687 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpTouchArea()
		{
		}

		// Token: 0x06007FB0 RID: 32688 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCardActorUp(CardPackCardActor cardActor)
		{
		}

		// Token: 0x06007FB1 RID: 32689 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickAllCardOpen()
		{
		}

		// Token: 0x06007FB2 RID: 32690 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnInputAccept()
		{
		}

		// Token: 0x06007FB3 RID: 32691 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCardDetailKey(CardPackCardActor cardActor)
		{
		}

		// Token: 0x06007FB4 RID: 32692 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCardAcceptKey(CardPackCardActor cardActor)
		{
		}

		// Token: 0x06007FB5 RID: 32693 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCardActorClick(CardPackCardActor cardActor)
		{
		}

		// Token: 0x06007FB6 RID: 32694 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x0400B834 RID: 47156
		private readonly int k_Step00_OpenCards;

		// Token: 0x0400B835 RID: 47157
		private readonly int k_Step01_OpenCardsComplete;

		// Token: 0x0400B836 RID: 47158
		private readonly int k_Step02_WaitCardsConfirm;

		// Token: 0x0400B837 RID: 47159
		private readonly int k_Step03_WaitEndTimeline;

		// Token: 0x0400B838 RID: 47160
		private readonly int k_Step04_Finish;

		// Token: 0x0400B839 RID: 47161
		private bool m_IsLast;

		// Token: 0x0400B83A RID: 47162
		private bool m_SelectedDefaultCursor;

		// Token: 0x0400B83B RID: 47163
		private Vector2 m_PrevTouchScreenPos;

		// Token: 0x0400B83C RID: 47164
		private int m_Step;

		// Token: 0x0400B83D RID: 47165
		private bool m_TouchAllOpenTrigger;

		// Token: 0x0400B83E RID: 47166
		private readonly SequenceBehaviourCardsOpen.OpenCardGroupContainer m_OpenGroupContainer;

		// Token: 0x0400B83F RID: 47167
		private List<object> m_CardDetailMrks;

		// Token: 0x0400B840 RID: 47168
		private List<object> m_CardDetailPremiums;

		// Token: 0x020010CC RID: 4300
		public class PlayableCommand
		{
			// Token: 0x17001019 RID: 4121
			// (get) Token: 0x06007FB7 RID: 32695 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool isDone
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06007FB8 RID: 32696 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool Play()
			{
				return false;
			}

			// Token: 0x06007FB9 RID: 32697 RVA: 0x0000216A File Offset: 0x0000036A
			public SequenceBehaviourCardsOpen.PlayableCommand Duplicate()
			{
				return null;
			}

			// Token: 0x0400B841 RID: 47169
			public DrawCardData drawCardData;

			// Token: 0x0400B842 RID: 47170
			public CardPackCardActor cardActor;

			// Token: 0x0400B843 RID: 47171
			public PlayableAsset playableAsset;

			// Token: 0x0400B844 RID: 47172
			public bool isLoop;

			// Token: 0x0400B845 RID: 47173
			private int m_Step;
		}

		// Token: 0x020010CD RID: 4301
		public class OpenCardGroup
		{
			// Token: 0x1700101A RID: 4122
			// (get) Token: 0x06007FBB RID: 32699 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool isComplete
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700101B RID: 4123
			// (get) Token: 0x06007FBC RID: 32700 RVA: 0x000029CC File Offset: 0x00000BCC
			public int Count
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06007FBD RID: 32701 RVA: 0x00002739 File Offset: 0x00000939
			public OpenCardGroup(CardPackFoundKeyWidget foundKeyWidget)
			{
			}

			// Token: 0x06007FBE RID: 32702 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsContain(CardPackCardActor cardActor)
			{
				return false;
			}

			// Token: 0x06007FBF RID: 32703 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsReversedActor(CardPackCardActor cardActor)
			{
				return false;
			}

			// Token: 0x06007FC0 RID: 32704 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsPlayingActor(CardPackCardActor cardActor)
			{
				return false;
			}

			// Token: 0x06007FC1 RID: 32705 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool TryAddCardActor(ActorBindingRefs actorBindRefs, CardPackCardActor cardActor, DrawCardData drawCardData)
			{
				return false;
			}

			// Token: 0x06007FC2 RID: 32706 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool Update()
			{
				return false;
			}

			// Token: 0x0400B846 RID: 47174
			private readonly List<CardPackCardActor> m_CardActors;

			// Token: 0x0400B847 RID: 47175
			private readonly CardPackFoundKeyWidget m_FoundKeyWidget;

			// Token: 0x0400B848 RID: 47176
			private List<SequenceBehaviourCardsOpen.PlayableCommand> m_CommandsStep0;

			// Token: 0x0400B849 RID: 47177
			private Queue<SequenceBehaviourCardsOpen.PlayableCommand> m_CommandsStep1;

			// Token: 0x0400B84A RID: 47178
			private Queue<SequenceBehaviourCardsOpen.PlayableCommand> m_CommandsStep2;

			// Token: 0x0400B84B RID: 47179
			private List<ValueTuple<int, int>> m_FoundSecrets;

			// Token: 0x0400B84C RID: 47180
			private int m_Step;

			// Token: 0x0400B84D RID: 47181
			public bool isWaitStep0;
		}

		// Token: 0x020010CE RID: 4302
		public class OpenCardGroupContainer
		{
			// Token: 0x1700101C RID: 4124
			// (get) Token: 0x06007FC3 RID: 32707 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool isInEditGroup
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700101D RID: 4125
			// (get) Token: 0x06007FC4 RID: 32708 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool isComplete
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700101E RID: 4126
			// (get) Token: 0x06007FC5 RID: 32709 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool isAllCardOpened
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700101F RID: 4127
			// (get) Token: 0x06007FC6 RID: 32710 RVA: 0x0000216A File Offset: 0x0000036A
			public IReadOnlyList<DrawCardData> drawCardDatas
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06007FC7 RID: 32711 RVA: 0x00002739 File Offset: 0x00000939
			public OpenCardGroupContainer(SequenceBehaviourWork work, IReadOnlyList<DrawCardData> drawCardDatas)
			{
			}

			// Token: 0x06007FC8 RID: 32712 RVA: 0x000029CC File Offset: 0x00000BCC
			private bool IsContainGroup(CardPackCardActor cardActor)
			{
				return false;
			}

			// Token: 0x06007FC9 RID: 32713 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsCompeteReverseActor(CardPackCardActor cardActor)
			{
				return false;
			}

			// Token: 0x06007FCA RID: 32714 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool TryAddNextCardActor(CardPackCardActor cardActor)
			{
				return false;
			}

			// Token: 0x06007FCB RID: 32715 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool TryAddCardActor(CardPackCardActor cardActor, bool isWaitStep0 = false)
			{
				return false;
			}

			// Token: 0x06007FCC RID: 32716 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool AddRemainAllCards()
			{
				return false;
			}

			// Token: 0x06007FCD RID: 32717 RVA: 0x0000216D File Offset: 0x0000036D
			public void SplitGroup()
			{
			}

			// Token: 0x06007FCE RID: 32718 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool Update()
			{
				return false;
			}

			// Token: 0x0400B84E RID: 47182
			private readonly SequenceBehaviourWork m_Work;

			// Token: 0x0400B84F RID: 47183
			private readonly IReadOnlyList<DrawCardData> m_DrawCardDatas;

			// Token: 0x0400B850 RID: 47184
			private readonly List<SequenceBehaviourCardsOpen.OpenCardGroup> m_OpenCardGroups;

			// Token: 0x0400B851 RID: 47185
			private SequenceBehaviourCardsOpen.OpenCardGroup m_CurrentGroup;

			// Token: 0x0400B852 RID: 47186
			private bool m_IsAllCardOpened;

			// Token: 0x0400B853 RID: 47187
			private int m_Step;

			// Token: 0x0400B854 RID: 47188
			public Action onAddCardEvent;

			// Token: 0x0400B855 RID: 47189
			public Action onAddCardCompleteEvent;
		}
	}
}
