using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.Bg;

namespace YgomGame.Duel
{
	// Token: 0x02000D76 RID: 3446
	public class DuelGameObjectManager : MonoBehaviour
	{
		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x0600648F RID: 25743 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006490 RID: 25744 RVA: 0x0000216D File Offset: 0x0000036D
		public RunEffectWorker effectWorker
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

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06006491 RID: 25745 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006492 RID: 25746 RVA: 0x0000216D File Offset: 0x0000036D
		public PopUpTextManager popUpTextManager
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

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06006493 RID: 25747 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006494 RID: 25748 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06006495 RID: 25749 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006496 RID: 25750 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isPreparedToDuel
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

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06006497 RID: 25751 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006498 RID: 25752 RVA: 0x0000216D File Offset: 0x0000036D
		private CardInstancePool cardInstancePool
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

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06006499 RID: 25753 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600649A RID: 25754 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelEffectPool duelEffectPool
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

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x0600649B RID: 25755 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600649C RID: 25756 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelResourcePool duelResourcePool
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

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x0600649D RID: 25757 RVA: 0x0000216A File Offset: 0x0000036A
		public MainCameraOrganizer mainCamera
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x0600649E RID: 25758 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600649F RID: 25759 RVA: 0x0000216D File Offset: 0x0000036D
		public BgManager bg
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

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x060064A0 RID: 25760 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064A1 RID: 25761 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelFieldBase duelField
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

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x060064A2 RID: 25762 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064A3 RID: 25763 RVA: 0x0000216D File Offset: 0x0000036D
		public FaceDownCardEffectPool faceDownCardEffectPool
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

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x060064A4 RID: 25764 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064A5 RID: 25765 RVA: 0x0000216D File Offset: 0x0000036D
		public ActivePlayerFieldEffect activePlayerFieldEffect
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

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x060064A6 RID: 25766 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064A7 RID: 25767 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelTimer3D duelTimer
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

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x060064A8 RID: 25768 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isShownUp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060064A9 RID: 25769 RVA: 0x0000216A File Offset: 0x0000036A
		public static DuelGameObjectManager Create()
		{
			return null;
		}

		// Token: 0x060064AA RID: 25770 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x060064AB RID: 25771 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator InitializeProcess()
		{
			return null;
		}

		// Token: 0x060064AC RID: 25772 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRunEffectWorker(RunEffectWorker worker)
		{
		}

		// Token: 0x060064AD RID: 25773 RVA: 0x0000216D File Offset: 0x0000036D
		public static void WarningGameObjectExists(string path)
		{
		}

		// Token: 0x060064AE RID: 25774 RVA: 0x0000216A File Offset: 0x0000036A
		public static GameObject CreateGameObject(GameObject parent, string name, Type[] components = null)
		{
			return null;
		}

		// Token: 0x060064AF RID: 25775 RVA: 0x000F5C30 File Offset: 0x000F3E30
		public static T CreateGameObject<T>(GameObject parent, string name, Type[] components = null) where T : MonoBehaviour
		{
			return default(T);
		}

		// Token: 0x060064B0 RID: 25776 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x060064B1 RID: 25777 RVA: 0x0000216D File Offset: 0x0000036D
		public void PrepareToDuel()
		{
		}

		// Token: 0x060064B2 RID: 25778 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator PrepareToDuelProcess()
		{
			return null;
		}

		// Token: 0x060064B3 RID: 25779 RVA: 0x0000216A File Offset: 0x0000036A
		public CardRoot RentCardInstance()
		{
			return null;
		}

		// Token: 0x060064B4 RID: 25780 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReturnCardInstance(CardRoot cardRoot)
		{
		}

		// Token: 0x060064B5 RID: 25781 RVA: 0x0000216A File Offset: 0x0000036A
		public CardRoot FindCardInstance(int uniqueId)
		{
			return null;
		}

		// Token: 0x060064B6 RID: 25782 RVA: 0x0000216A File Offset: 0x0000036A
		public CardRoot FindCardInstance(int player, int position, int index)
		{
			return null;
		}

		// Token: 0x060064B7 RID: 25783 RVA: 0x0000216A File Offset: 0x0000036A
		public CardRoot FindPlacedCardInstance(int player, int position, int index)
		{
			return null;
		}

		// Token: 0x060064B8 RID: 25784 RVA: 0x0000216A File Offset: 0x0000036A
		public List<CardRoot> FindPlacedCardsInstance(int player, int position, int excludeIndex = -1)
		{
			return null;
		}

		// Token: 0x060064B9 RID: 25785 RVA: 0x0000216D File Offset: 0x0000036D
		private void ReturnAllCards()
		{
		}

		// Token: 0x060064BA RID: 25786 RVA: 0x0000216D File Offset: 0x0000036D
		private void SyncEngineCards()
		{
		}

		// Token: 0x060064BB RID: 25787 RVA: 0x0000216D File Offset: 0x0000036D
		public void RefreshFieldCard()
		{
		}

		// Token: 0x060064BC RID: 25788 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsCardEffectPlaying(CardRoot excludeCard = null)
		{
			return false;
		}

		// Token: 0x060064BD RID: 25789 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsCardEffectPlaying(Type type, CardRoot excludeCard = null)
		{
			return false;
		}

		// Token: 0x060064BE RID: 25790 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsZoneEffectPlaying(ZoneCard.Zone zone, ZoneCard.Mode mode, CardRoot excludeCard = null)
		{
			return false;
		}

		// Token: 0x060064BF RID: 25791 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsMoveEffectRequested(CardRoot excludeCard = null)
		{
			return false;
		}

		// Token: 0x060064C0 RID: 25792 RVA: 0x0000216A File Offset: 0x0000036A
		public CardRoot GetFieldCardAt(int index)
		{
			return null;
		}

		// Token: 0x060064C1 RID: 25793 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetFieldCardNum()
		{
			return 0;
		}

		// Token: 0x060064C2 RID: 25794 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetVisible(bool visible)
		{
		}

		// Token: 0x060064C3 RID: 25795 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowDuelStartField(bool playEffect)
		{
		}

		// Token: 0x060064C4 RID: 25796 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckCardEffectPlaying()
		{
		}

		// Token: 0x060064C5 RID: 25797 RVA: 0x0000216D File Offset: 0x0000036D
		public void RequestActionOnPlayedCardEffect(Action action)
		{
		}

		// Token: 0x060064C6 RID: 25798 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x060064C7 RID: 25799 RVA: 0x0000216D File Offset: 0x0000036D
		private void IdleStep()
		{
		}

		// Token: 0x060064C8 RID: 25800 RVA: 0x0000216D File Offset: 0x0000036D
		private void TerminatingStep()
		{
		}

		// Token: 0x04009F3A RID: 40762
		private DuelGameObjectManager.Step step;

		// Token: 0x04009F3B RID: 40763
		private List<CardRoot> cardRoots;

		// Token: 0x04009F3C RID: 40764
		private bool reqCardEffectFinishedCallback;

		// Token: 0x04009F3D RID: 40765
		public Action onCardEffectFinished;

		// Token: 0x02000D77 RID: 3447
		private enum Step
		{
			// Token: 0x04009F3F RID: 40767
			PreLoading,
			// Token: 0x04009F40 RID: 40768
			PreLoaded,
			// Token: 0x04009F41 RID: 40769
			Preparing,
			// Token: 0x04009F42 RID: 40770
			Idle,
			// Token: 0x04009F43 RID: 40771
			Terminating
		}
	}
}
