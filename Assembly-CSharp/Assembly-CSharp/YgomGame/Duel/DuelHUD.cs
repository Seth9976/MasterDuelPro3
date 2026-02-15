using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.Settings;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000D78 RID: 3448
	public class DuelHUD : MonoBehaviour
	{
		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x060064CA RID: 25802 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064CB RID: 25803 RVA: 0x0000216D File Offset: 0x0000036D
		public GameObject root
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

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x060064CC RID: 25804 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064CD RID: 25805 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelClient host
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

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x060064CE RID: 25806 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064CF RID: 25807 RVA: 0x0000216D File Offset: 0x0000036D
		public ActivateConfirmToggle activateToggle
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

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x060064D0 RID: 25808 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064D1 RID: 25809 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelStatusViewer statusViewer
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

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x060064D2 RID: 25810 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064D3 RID: 25811 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelCursorJump cursorJump
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

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x060064D4 RID: 25812 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064D5 RID: 25813 RVA: 0x0000216D File Offset: 0x0000036D
		public PlaceStatusManager placeStatus
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

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x060064D6 RID: 25814 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064D7 RID: 25815 RVA: 0x0000216D File Offset: 0x0000036D
		public ManaSetManager manaSetManager
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

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x060064D8 RID: 25816 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060064D9 RID: 25817 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isShowingSettingMenu
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

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x060064DA RID: 25818 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isCancelButtonActivated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x060064DB RID: 25819 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isDecisionButtonActivated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x060064DC RID: 25820 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064DD RID: 25821 RVA: 0x0000216D File Offset: 0x0000036D
		public CardInfo cardInfoL
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

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x060064DE RID: 25822 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064DF RID: 25823 RVA: 0x0000216D File Offset: 0x0000036D
		public CardInfo cardInfoR
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

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x060064E0 RID: 25824 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064E1 RID: 25825 RVA: 0x0000216D File Offset: 0x0000036D
		public CardReportTelopManager cardReportTelopManager
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

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x060064E2 RID: 25826 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064E3 RID: 25827 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelLogController duellog
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

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x060064E4 RID: 25828 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064E5 RID: 25829 RVA: 0x0000216D File Offset: 0x0000036D
		public GenericCardListController genericCardList
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

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x060064E6 RID: 25830 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064E7 RID: 25831 RVA: 0x0000216D File Offset: 0x0000036D
		public GenericCardListEx relativeCardList
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

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x060064E8 RID: 25832 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064E9 RID: 25833 RVA: 0x0000216D File Offset: 0x0000036D
		public CardInfoDetailForDuel cardInfoDetail
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

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x060064EA RID: 25834 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064EB RID: 25835 RVA: 0x0000216D File Offset: 0x0000036D
		public CpuThinkingIcon cpuThinkingIcon
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

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x060064EC RID: 25836 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064ED RID: 25837 RVA: 0x0000216D File Offset: 0x0000036D
		public CardSelectionList cardSelectionList
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

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x060064EE RID: 25838 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064EF RID: 25839 RVA: 0x0000216D File Offset: 0x0000036D
		public PhaseSelectWindow phaseWindow
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

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x060064F0 RID: 25840 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064F1 RID: 25841 RVA: 0x0000216D File Offset: 0x0000036D
		public FullScreenUiBg fullScreenUiBg
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

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x060064F2 RID: 25842 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool duelOver
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x060064F3 RID: 25843 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060064F4 RID: 25844 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x060064F5 RID: 25845 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060064F6 RID: 25846 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x060064F7 RID: 25847 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060064F8 RID: 25848 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x060064F9 RID: 25849 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064FA RID: 25850 RVA: 0x0000216D File Offset: 0x0000036D
		private GameObject bgFade
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

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x060064FB RID: 25851 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064FC RID: 25852 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelLP nearLPCounter
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

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x060064FD RID: 25853 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060064FE RID: 25854 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelLP farLPCounter
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

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x060064FF RID: 25855 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isReplayPause
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06006500 RID: 25856 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isVisibleAllPlaceStatusLabel
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006501 RID: 25857 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06006502 RID: 25858 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(DuelClient host)
		{
		}

		// Token: 0x06006503 RID: 25859 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator InitializeProcess()
		{
			return null;
		}

		// Token: 0x06006504 RID: 25860 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupSettingMenu()
		{
		}

		// Token: 0x06006505 RID: 25861 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenSettingMenu()
		{
		}

		// Token: 0x06006506 RID: 25862 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x06006507 RID: 25863 RVA: 0x0000216D File Offset: 0x0000036D
		public void PrepareToDuel()
		{
		}

		// Token: 0x06006508 RID: 25864 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator PrepareToDuelProcess()
		{
			return null;
		}

		// Token: 0x06006509 RID: 25865 RVA: 0x0000216D File Offset: 0x0000036D
		public void DuelStart()
		{
		}

		// Token: 0x0600650A RID: 25866 RVA: 0x0000216D File Offset: 0x0000036D
		public void DuelEnd()
		{
		}

		// Token: 0x0600650B RID: 25867 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideButtons()
		{
		}

		// Token: 0x0600650C RID: 25868 RVA: 0x0000216D File Offset: 0x0000036D
		public void CloseDialog()
		{
		}

		// Token: 0x0600650D RID: 25869 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetLP(SharedDefinition.Location side, int lp)
		{
		}

		// Token: 0x0600650E RID: 25870 RVA: 0x0000216D File Offset: 0x0000036D
		public void ChangeLP(SharedDefinition.Location side, int afterLP, int damage, Engine.DamageType type, Action onFinished = null)
		{
		}

		// Token: 0x0600650F RID: 25871 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDisp(bool disp)
		{
		}

		// Token: 0x06006510 RID: 25872 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowAllCardStatusLabel()
		{
		}

		// Token: 0x06006511 RID: 25873 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideAllCardStatusLabel()
		{
		}

		// Token: 0x06006512 RID: 25874 RVA: 0x0000216A File Offset: 0x0000036A
		public PlaceStatusLabel UsePlaceStatusLabel(SharedDefinition.Location location, bool lieDown, bool hand)
		{
			return null;
		}

		// Token: 0x06006513 RID: 25875 RVA: 0x0000216D File Offset: 0x0000036D
		public void UnusePlaceStatusLabel(PlaceStatusLabel instance)
		{
		}

		// Token: 0x06006514 RID: 25876 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowAllPlaceStatusLabel()
		{
		}

		// Token: 0x06006515 RID: 25877 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideAllPlaceStatusLabel()
		{
		}

		// Token: 0x06006516 RID: 25878 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowDamageFrame()
		{
		}

		// Token: 0x06006517 RID: 25879 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBgColor(Color color)
		{
		}

		// Token: 0x06006518 RID: 25880 RVA: 0x0000216D File Offset: 0x0000036D
		private void updateNetworkStatus()
		{
		}

		// Token: 0x06006519 RID: 25881 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateActivateToggle(SettingsUtil.DuelParam.MANUAL_TYPE type)
		{
		}

		// Token: 0x0600651A RID: 25882 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClickReplayStop()
		{
		}

		// Token: 0x0600651B RID: 25883 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickReplayFast()
		{
		}

		// Token: 0x0600651C RID: 25884 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnChangeWatcherNum(int num)
		{
		}

		// Token: 0x0600651D RID: 25885 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateWatcherNum()
		{
		}

		// Token: 0x0600651E RID: 25886 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickDuelLogButton()
		{
		}

		// Token: 0x0600651F RID: 25887 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenDuelLog()
		{
		}

		// Token: 0x06006520 RID: 25888 RVA: 0x0000216D File Offset: 0x0000036D
		public void CloseDuelLog(bool forMobile)
		{
		}

		// Token: 0x06006521 RID: 25889 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnChangeDuelLogOpenClose(bool isOpen)
		{
		}

		// Token: 0x06006522 RID: 25890 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDuelLogButtonStatus(bool isOpen)
		{
		}

		// Token: 0x06006523 RID: 25891 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupCardinfoL(CardInfo instance, Transform parent, bool ismobilelayout)
		{
		}

		// Token: 0x06006524 RID: 25892 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupCardinfoR(CardInfo instance)
		{
		}

		// Token: 0x06006525 RID: 25893 RVA: 0x0000216D File Offset: 0x0000036D
		public void CloseCardInfoDetail()
		{
		}

		// Token: 0x06006526 RID: 25894 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickCancelButtonCallback(Action callback)
		{
		}

		// Token: 0x06006527 RID: 25895 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDispCancelButton(bool disp)
		{
		}

		// Token: 0x06006528 RID: 25896 RVA: 0x0000216D File Offset: 0x0000036D
		public void ActivateCancelButton(Action clickCallback)
		{
		}

		// Token: 0x06006529 RID: 25897 RVA: 0x0000216D File Offset: 0x0000036D
		public void DeactivateCancelButton()
		{
		}

		// Token: 0x0600652A RID: 25898 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickDecisionButtonCallback(Action callback)
		{
		}

		// Token: 0x0600652B RID: 25899 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDispDecisionButton(bool disp)
		{
		}

		// Token: 0x0600652C RID: 25900 RVA: 0x0000216D File Offset: 0x0000036D
		public void ActivateDecisionButton(Action clickCallback)
		{
		}

		// Token: 0x0600652D RID: 25901 RVA: 0x0000216D File Offset: 0x0000036D
		public void DeactivateDecisionButton()
		{
		}

		// Token: 0x0600652E RID: 25902 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenCardInfo(int team, int position, int index, bool isLeft = true, bool lockDisp = true, bool force = true, bool miniDisp = true, bool setTargetTopCardIndex = false)
		{
		}

		// Token: 0x0600652F RID: 25903 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenCardInfo(bool isLeft = true)
		{
		}

		// Token: 0x06006530 RID: 25904 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenCardInfoByUniqueID(int uniqueID, bool isLeft = true, bool lockDisp = true, bool showToHappenHighlight = false, int highlightRfxTableIndex = -1)
		{
		}

		// Token: 0x06006531 RID: 25905 RVA: 0x0000216D File Offset: 0x0000036D
		public void CloseCardInfo(bool isLeft = true)
		{
		}

		// Token: 0x06006532 RID: 25906 RVA: 0x0000216D File Offset: 0x0000036D
		public void CloseCardInfoMini(bool isLeft = true)
		{
		}

		// Token: 0x06006533 RID: 25907 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardInfoMiniPos(CardInfo.ShowPos miniPos, bool isLeft = true)
		{
		}

		// Token: 0x06006534 RID: 25908 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPhaseButtonIconPosition(Vector3 worldPosition)
		{
		}

		// Token: 0x06006535 RID: 25909 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDispPhaseButtonIcon(bool disp)
		{
		}

		// Token: 0x06006536 RID: 25910 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActivateShortcutIcon(bool activate)
		{
		}

		// Token: 0x04009F44 RID: 40772
		private const string SE_DUEL_SELECT = "SE_DUEL_SELECT";

		// Token: 0x04009F45 RID: 40773
		[SerializeField]
		private GameObject prefabUI;

		// Token: 0x04009F46 RID: 40774
		[SerializeField]
		private GameObject handStatusLabelSrc;

		// Token: 0x04009F47 RID: 40775
		[SerializeField]
		private GameObject placeStatusLabelSrc;

		// Token: 0x04009F48 RID: 40776
		[SerializeField]
		private GameObject placeStatusLabelRootSrc;

		// Token: 0x04009F49 RID: 40777
		[SerializeField]
		private global::UnityEngine.Object screenFadeSrc;

		// Token: 0x04009F4A RID: 40778
		private ElementObjectManager ui;

		// Token: 0x04009F4B RID: 40779
		private DamageFrame damageFrame;

		// Token: 0x04009F4C RID: 40780
		private const int LATENCY_THRESHOLD = 127;

		// Token: 0x04009F4D RID: 40781
		private ReplayControl replayCtrl;

		// Token: 0x04009F4E RID: 40782
		private GameObject buttonDuelMenu;

		// Token: 0x04009F4F RID: 40783
		private AudienceInfo audienceInfo;

		// Token: 0x04009F50 RID: 40784
		private ElementObjectManager logButton;

		// Token: 0x04009F51 RID: 40785
		private GameObject duelLogOnIcon;

		// Token: 0x04009F52 RID: 40786
		private GameObject duelLogOffIcon;

		// Token: 0x04009F53 RID: 40787
		private SelectionButton cancelButton;

		// Token: 0x04009F54 RID: 40788
		private Action onClickCancelButton;

		// Token: 0x04009F55 RID: 40789
		private SelectionButton decisionButton;

		// Token: 0x04009F56 RID: 40790
		private Action onClickDecisionButton;

		// Token: 0x04009F57 RID: 40791
		private GameObject phaseButtonIcon;

		// Token: 0x04009F58 RID: 40792
		private const string KEY_WATCH = "w";

		// Token: 0x04009F59 RID: 40793
		private bool duelEnd;

		// Token: 0x04009F5A RID: 40794
		private Coroutine initializeCoroutine;

		// Token: 0x04009F5B RID: 40795
		private Coroutine prepareToDuelCoroutine;
	}
}
