using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000D70 RID: 3440
	public abstract class DuelFieldBase : MonoBehaviour, IFieldPlacementInfo
	{
		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06006430 RID: 25648
		protected abstract string nearMatResourcePath { get; }

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06006431 RID: 25649
		protected abstract string farMatResourcePath { get; }

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06006432 RID: 25650
		protected abstract Vector3 matSize { get; }

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06006433 RID: 25651
		public abstract int numMonsterPlaces { get; }

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06006434 RID: 25652
		public abstract int numMagicPlaces { get; }

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06006435 RID: 25653
		public abstract int monsterStartIdx { get; }

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06006436 RID: 25654
		public abstract int monsterEndIdx { get; }

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06006437 RID: 25655
		public abstract int magicStartIdx { get; }

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06006438 RID: 25656
		public abstract int magicEndIdx { get; }

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06006439 RID: 25657 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600643A RID: 25658 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelGameObjectManager goManager
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x0600643B RID: 25659 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600643C RID: 25660 RVA: 0x0000216D File Offset: 0x0000036D
		public List<CardPlace> cardPlaces
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x0600643D RID: 25661 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600643E RID: 25662 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isInitialized
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x0600643F RID: 25663 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006440 RID: 25664 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isTerminated
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06006441 RID: 25665 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006442 RID: 25666 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isPrepared
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06006443 RID: 25667 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006444 RID: 25668 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isShownUp
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06006445 RID: 25669
		protected abstract void AssignAll(SharedDefinition.Location loc, GameObject parent);

		// Token: 0x06006446 RID: 25670
		protected abstract GameObject GetFrame(SharedDefinition.Location loc, int position);

		// Token: 0x06006447 RID: 25671
		protected abstract List<GameObject> GetFrames(SharedDefinition.Location loc);

		// Token: 0x06006448 RID: 25672
		protected abstract GameObject GetPlayMat(SharedDefinition.Location loc);

		// Token: 0x06006449 RID: 25673
		protected abstract MeshRenderer GetPlayMatRenderer(SharedDefinition.Location loc);

		// Token: 0x0600644A RID: 25674 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnUpdate()
		{
		}

		// Token: 0x0600644B RID: 25675 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnTerminate()
		{
		}

		// Token: 0x0600644C RID: 25676 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool UpdateInitialize()
		{
			return false;
		}

		// Token: 0x0600644D RID: 25677 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool UpdateTerminate()
		{
			return false;
		}

		// Token: 0x0600644E RID: 25678 RVA: 0x000F5B7C File Offset: 0x000F3D7C
		public static T Create<T>(DuelGameObjectManager goManager, GameObject root, string name) where T : DuelFieldBase
		{
			return default(T);
		}

		// Token: 0x0600644F RID: 25679 RVA: 0x0000216D File Offset: 0x0000036D
		protected void Initialize()
		{
		}

		// Token: 0x06006450 RID: 25680 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x06006451 RID: 25681 RVA: 0x0000216D File Offset: 0x0000036D
		public void PrepareToDuel()
		{
		}

		// Token: 0x06006452 RID: 25682 RVA: 0x0000216D File Offset: 0x0000036D
		public void Inactivate()
		{
		}

		// Token: 0x06006453 RID: 25683 RVA: 0x0000216A File Offset: 0x0000036A
		public CardPlace GetCardPlace(int team, int position)
		{
			return null;
		}

		// Token: 0x06006454 RID: 25684 RVA: 0x0000216A File Offset: 0x0000036A
		public CardLocator GetCardLocator(int team, int position, int index)
		{
			return null;
		}

		// Token: 0x06006455 RID: 25685 RVA: 0x000F5B92 File Offset: 0x000F3D92
		public bool GetPosture(int team, int position, int index, out Vector3 pos, out Quaternion rot)
		{
			pos = default(Vector3);
			rot = default(Quaternion);
			return false;
		}

		// Token: 0x06006456 RID: 25686 RVA: 0x0000216A File Offset: 0x0000036A
		public MonsterCardPlace[] GetMonsterPlaces(int team)
		{
			return null;
		}

		// Token: 0x06006457 RID: 25687 RVA: 0x0000216A File Offset: 0x0000036A
		public MagicCardPlace[] GetMagicPlaces(int team)
		{
			return null;
		}

		// Token: 0x06006458 RID: 25688 RVA: 0x0000216A File Offset: 0x0000036A
		private DeckCardPlace[] GetDeckPlaces(int team)
		{
			return null;
		}

		// Token: 0x06006459 RID: 25689 RVA: 0x0000216A File Offset: 0x0000036A
		private GraveCardPlace[] GetGravePlaces(int team)
		{
			return null;
		}

		// Token: 0x0600645A RID: 25690 RVA: 0x0000216A File Offset: 0x0000036A
		public CardPlace[] GetSwitchablePlaces(int team)
		{
			return null;
		}

		// Token: 0x0600645B RID: 25691 RVA: 0x000F5BA8 File Offset: 0x000F3DA8
		public Vector3 GetHandCardPosition(int index, int numCards, SharedDefinition.Location location)
		{
			return default(Vector3);
		}

		// Token: 0x0600645C RID: 25692 RVA: 0x000F5BC0 File Offset: 0x000F3DC0
		public Quaternion GetHandCardRotation(SharedDefinition.Location location)
		{
			return default(Quaternion);
		}

		// Token: 0x0600645D RID: 25693 RVA: 0x0000216D File Offset: 0x0000036D
		public void HighlightIfAvailable(bool available, uint cmdBit, Action onFinished)
		{
		}

		// Token: 0x0600645E RID: 25694 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateHighlightEffect()
		{
		}

		// Token: 0x0600645F RID: 25695 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateState(Action onFinished)
		{
		}

		// Token: 0x06006460 RID: 25696 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqAffectEffect(int team, int position)
		{
		}

		// Token: 0x06006461 RID: 25697 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearAffectEffect()
		{
		}

		// Token: 0x06006462 RID: 25698 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsAffectCardPlace(int player, int position)
		{
			return false;
		}

		// Token: 0x06006463 RID: 25699 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowUpOnStartDuel(bool playEffect)
		{
		}

		// Token: 0x06006464 RID: 25700 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator ShowUpCoroutine(bool playEffect)
		{
			return null;
		}

		// Token: 0x06006465 RID: 25701 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06006466 RID: 25702 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitLoadPlayMatStep()
		{
		}

		// Token: 0x06006467 RID: 25703 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializingStep()
		{
		}

		// Token: 0x06006468 RID: 25704 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitPreparedStep()
		{
		}

		// Token: 0x06006469 RID: 25705 RVA: 0x0000216D File Offset: 0x0000036D
		private void ExecDuelStep()
		{
		}

		// Token: 0x0600646A RID: 25706 RVA: 0x0000216D File Offset: 0x0000036D
		private void TerminatingStep()
		{
		}

		// Token: 0x0600646B RID: 25707 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x0600646C RID: 25708 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitAllPlaces()
		{
		}

		// Token: 0x0600646D RID: 25709 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator InitAllPlacesProcess()
		{
			return null;
		}

		// Token: 0x0600646E RID: 25710 RVA: 0x000F5BD6 File Offset: 0x000F3DD6
		public bool AnchorNameToPosition(string name, out int player, out int position)
		{
			player = 0;
			position = 0;
			return false;
		}

		// Token: 0x0600646F RID: 25711 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject CreateAnchor(SharedDefinition.Location loc, int position)
		{
			return null;
		}

		// Token: 0x06006470 RID: 25712 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddSelectionItem(SharedDefinition.Location location, int position, SelectionButton item)
		{
		}

		// Token: 0x06006471 RID: 25713 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveSelectionItem(SharedDefinition.Location location, int position, SelectionButton item)
		{
		}

		// Token: 0x06006472 RID: 25714 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveSelectionItem(SharedDefinition.Location location, int position, int index)
		{
		}

		// Token: 0x06006473 RID: 25715 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton GetSelectionItem(SharedDefinition.Location location, int position, int index)
		{
			return null;
		}

		// Token: 0x06006474 RID: 25716 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton GetSelectionItem(int player, int position, int index)
		{
			return null;
		}

		// Token: 0x06006475 RID: 25717 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton GetSelectionItem(Vector2 screenPoint)
		{
			return null;
		}

		// Token: 0x06006476 RID: 25718 RVA: 0x0000216A File Offset: 0x0000036A
		public DuelFieldBase.SelectionItemContainer GetSelectionItemContainer(SharedDefinition.Location location, int position)
		{
			return null;
		}

		// Token: 0x06006477 RID: 25719 RVA: 0x000F5BE0 File Offset: 0x000F3DE0
		public ValueTuple<SharedDefinition.Location, int, int> GetPosition(SelectionItem item)
		{
			return default(ValueTuple<SharedDefinition.Location, int, int>);
		}

		// Token: 0x06006478 RID: 25720 RVA: 0x000F5BF8 File Offset: 0x000F3DF8
		public ValueTuple<SharedDefinition.Location, int, int> GetPosition(Vector2 screen_point)
		{
			return default(ValueTuple<SharedDefinition.Location, int, int>);
		}

		// Token: 0x06006479 RID: 25721 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupSelectionItemTransition()
		{
		}

		// Token: 0x0600647A RID: 25722 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupSelectionItemTransition(SelectionButton item, PadInputDirection direction, SharedDefinition.Location transitionLocation, int transitionPosition)
		{
		}

		// Token: 0x0600647B RID: 25723 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SelectItem(int player, int position, int index)
		{
			return false;
		}

		// Token: 0x04009F09 RID: 40713
		private DuelFieldBase.Step step;

		// Token: 0x04009F0A RID: 40714
		private bool initCardPlaces;

		// Token: 0x04009F0B RID: 40715
		private List<TargetingLine> affectLinkLines;

		// Token: 0x04009F0C RID: 40716
		private List<CardPlace> affectCardPlaces;

		// Token: 0x04009F0D RID: 40717
		private GameObject nearMdl;

		// Token: 0x04009F0E RID: 40718
		private GameObject farMdl;

		// Token: 0x04009F0F RID: 40719
		private bool highlightAvailable;

		// Token: 0x04009F10 RID: 40720
		protected Dictionary<int, DuelFieldBase.HalfField> halfFields;

		// Token: 0x04009F11 RID: 40721
		protected NearHandCardPlace nearHandPlace;

		// Token: 0x04009F12 RID: 40722
		protected FarHandCardPlace farHandPlace;

		// Token: 0x04009F13 RID: 40723
		public List<DuelFieldBase.SelectionItemContainer> selectionItemList;

		// Token: 0x04009F14 RID: 40724
		private Selector selector;

		// Token: 0x04009F15 RID: 40725
		private Coroutine initAllPlaceCoroutine;

		// Token: 0x04009F16 RID: 40726
		private Coroutine showUpCoroutine;

		// Token: 0x04009F17 RID: 40727
		private bool loadNearMat;

		// Token: 0x04009F18 RID: 40728
		private bool loadFarMat;

		// Token: 0x04009F19 RID: 40729
		private bool dragging;

		// Token: 0x02000D71 RID: 3441
		private enum Step
		{
			// Token: 0x04009F1B RID: 40731
			WaitLoadPlayMat,
			// Token: 0x04009F1C RID: 40732
			Initializing,
			// Token: 0x04009F1D RID: 40733
			WaitPrepareToDuel,
			// Token: 0x04009F1E RID: 40734
			WaitPrepared,
			// Token: 0x04009F1F RID: 40735
			FinishPrepare,
			// Token: 0x04009F20 RID: 40736
			WaitShowUp,
			// Token: 0x04009F21 RID: 40737
			ExecDuel,
			// Token: 0x04009F22 RID: 40738
			Terminating,
			// Token: 0x04009F23 RID: 40739
			Finish
		}

		// Token: 0x02000D72 RID: 3442
		public class HalfField
		{
			// Token: 0x04009F24 RID: 40740
			public MonsterCardPlace[] monsterPlaces;

			// Token: 0x04009F25 RID: 40741
			public MagicCardPlace[] magicPlaces;

			// Token: 0x04009F26 RID: 40742
			public MagicCardPlace pendulumLPlace;

			// Token: 0x04009F27 RID: 40743
			public MagicCardPlace pendulumRPlace;

			// Token: 0x04009F28 RID: 40744
			public MagicCardPlace fieldPlace;

			// Token: 0x04009F29 RID: 40745
			public DeckCardPlace deckPlace;

			// Token: 0x04009F2A RID: 40746
			public DeckCardPlace extraPlace;

			// Token: 0x04009F2B RID: 40747
			public GraveCardPlace gravePlace;

			// Token: 0x04009F2C RID: 40748
			public GraveCardPlace excludePlace;
		}

		// Token: 0x02000D73 RID: 3443
		public class SelectionItemContainer
		{
			// Token: 0x04009F2D RID: 40749
			public SharedDefinition.Location location;

			// Token: 0x04009F2E RID: 40750
			public int position;

			// Token: 0x04009F2F RID: 40751
			public List<SelectionButton> item;
		}
	}
}
