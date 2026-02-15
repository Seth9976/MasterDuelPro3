using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000E92 RID: 3730
	public class GenericCardListController : MonoBehaviour, IGenericScrollViewSupport
	{
		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06006C5B RID: 27739 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006C5C RID: 27740 RVA: 0x0000216D File Offset: 0x0000036D
		public GenericCardListController.ListStatue Statue
		{
			[CompilerGenerated]
			get
			{
				return GenericCardListController.ListStatue.OPEN;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06006C5D RID: 27741 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isOpen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06006C5E RID: 27742 RVA: 0x0000216A File Offset: 0x0000036A
		private List<int> m_CurrentDataList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006C5F RID: 27743 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(Transform parent, DuelClient host, UnityAction<GenericCardListController> onFinish)
		{
		}

		// Token: 0x06006C60 RID: 27744 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateList(int team, int position)
		{
		}

		// Token: 0x06006C61 RID: 27745 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateAsEffectList(int team, int position)
		{
		}

		// Token: 0x06006C62 RID: 27746 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Show()
		{
			return false;
		}

		// Token: 0x06006C63 RID: 27747 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Close()
		{
			return false;
		}

		// Token: 0x06006C64 RID: 27748 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAlpha(float alpha)
		{
		}

		// Token: 0x06006C65 RID: 27749 RVA: 0x0000216D File Offset: 0x0000036D
		public void ChangeList()
		{
		}

		// Token: 0x06006C66 RID: 27750 RVA: 0x0000216D File Offset: 0x0000036D
		public void ChangeListImpl()
		{
		}

		// Token: 0x06006C67 RID: 27751 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateContent()
		{
		}

		// Token: 0x06006C68 RID: 27752 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetShortkeyIconVisible(bool visible)
		{
		}

		// Token: 0x06006C69 RID: 27753 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitComponent()
		{
		}

		// Token: 0x06006C6A RID: 27754 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitDataListTable()
		{
		}

		// Token: 0x06006C6B RID: 27755 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06006C6C RID: 27756 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateDataList()
		{
		}

		// Token: 0x06006C6D RID: 27757 RVA: 0x0000216D File Offset: 0x0000036D
		private void Open()
		{
		}

		// Token: 0x06006C6E RID: 27758 RVA: 0x000F5FF4 File Offset: 0x000F41F4
		private ValueTuple<int, int> GetOwnerAndLocateByListType(GenericCardListController.ListType type)
		{
			return default(ValueTuple<int, int>);
		}

		// Token: 0x06006C6F RID: 27759 RVA: 0x000029CC File Offset: 0x00000BCC
		private GenericCardListController.ListType GetListTypeByOwnerAndLocate(int owner, int locate)
		{
			return GenericCardListController.ListType.NONE;
		}

		// Token: 0x06006C70 RID: 27760 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetUidCard(int dataindex, GameObject gob)
		{
		}

		// Token: 0x06006C71 RID: 27761 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCidCard(int dataindex, GameObject gob)
		{
		}

		// Token: 0x06006C72 RID: 27762 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemSetData(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06006C73 RID: 27763 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnGsvStanby()
		{
		}

		// Token: 0x06006C74 RID: 27764 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetStatues(ElementObjectManager eom, int cardid)
		{
		}

		// Token: 0x06006C75 RID: 27765 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCardPicture(RawImage cardpicture, GameObject cardmask, int uid)
		{
		}

		// Token: 0x06006C76 RID: 27766 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetLinkMarkers(ElementObjectManager eom, int linkmask, int linknum)
		{
		}

		// Token: 0x06006C77 RID: 27767 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetLevel(ElementObjectManager eom, int level)
		{
		}

		// Token: 0x06006C78 RID: 27768 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetRank(ElementObjectManager eom, int rank)
		{
		}

		// Token: 0x06006C79 RID: 27769 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateCardSourceTable(int uid, int pos)
		{
		}

		// Token: 0x06006C7A RID: 27770 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemInitialize(GameObject gob)
		{
		}

		// Token: 0x06006C7B RID: 27771 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemExit(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06006C7C RID: 27772 RVA: 0x0000216D File Offset: 0x0000036D
		protected void Initialize()
		{
		}

		// Token: 0x0400A7B0 RID: 42928
		private const string PREHAB_PATH = "Prefabs/Duel/UI/GenericCardList";

		// Token: 0x0400A7B1 RID: 42929
		private const string PREHAB_PATH_MOBILE = "Prefabs/Duel/UI/GenericCardList_Mobile";

		// Token: 0x0400A7B2 RID: 42930
		private const string LABEL_EO_SCROLLVIEW = "ScrollView";

		// Token: 0x0400A7B3 RID: 42931
		private const string LABEL_EO_LISTTYPEICON = "ListTypeIcon";

		// Token: 0x0400A7B4 RID: 42932
		private const string LABEL_EO_SCROLLUP = "ScrollUp";

		// Token: 0x0400A7B5 RID: 42933
		private const string LABEL_EO_SCROLLDOWN = "ScrollDown";

		// Token: 0x0400A7B6 RID: 42934
		private const string LABEL_EO_CARDIMAGE = "CardImage";

		// Token: 0x0400A7B7 RID: 42935
		private const string LABEL_EO_CARDMASK = "CardMask";

		// Token: 0x0400A7B8 RID: 42936
		private const string LABEL_EO_STATUEROOT = "StatueRoot";

		// Token: 0x0400A7B9 RID: 42937
		private const string LABEL_EO_FROMEXTRAICON = "FromExtraIcon";

		// Token: 0x0400A7BA RID: 42938
		private const string LABEL_EO_STATUEICON = "Icon";

		// Token: 0x0400A7BB RID: 42939
		private const string LABEL_EO_LINKMARKERS = "LinkMarkers";

		// Token: 0x0400A7BC RID: 42940
		private const string LABEL_EO_STATUETEXT = "Text";

		// Token: 0x0400A7BD RID: 42941
		private const string LABEL_EO_TEXTTOTALCOUNT = "TextTotalCount";

		// Token: 0x0400A7BE RID: 42942
		private const string LABEL_EO_WINDOW = "Window";

		// Token: 0x0400A7BF RID: 42943
		private const string LABEL_TWEEN_SHOW = "Show";

		// Token: 0x0400A7C0 RID: 42944
		private const string LABEL_TWEEN_HIDE = "Hide";

		// Token: 0x0400A7C1 RID: 42945
		private const string LABEL_TWEEN_CHANGELIST = "ChangeList";

		// Token: 0x0400A7C2 RID: 42946
		private const string LABEL_TWEEN_MOVEIN = "MoveIn";

		// Token: 0x0400A7C3 RID: 42947
		private const string LABEL_TWEEN_MOVEOUT = "MoveOut";

		// Token: 0x0400A7C4 RID: 42948
		private const string LABEL_TWEEN_CHANGELISTSTEP1 = "ChangeList_Step1";

		// Token: 0x0400A7C5 RID: 42949
		private const string LABEL_TWEEN_CHANGELISTSTEP0 = "ChangeList_Step0";

		// Token: 0x0400A7C6 RID: 42950
		private GenericCardListController.ListType m_Type;

		// Token: 0x0400A7C7 RID: 42951
		private ElementObjectManager m_Eom;

		// Token: 0x0400A7C8 RID: 42952
		private GenericScrollView m_Gsv;

		// Token: 0x0400A7C9 RID: 42953
		private Tween m_TweenClose;

		// Token: 0x0400A7CA RID: 42954
		private Tween m_TweenOpen;

		// Token: 0x0400A7CB RID: 42955
		private GameObject m_Window;

		// Token: 0x0400A7CC RID: 42956
		private Dictionary<GenericCardListController.ListType, List<int>> m_DataListTable;

		// Token: 0x0400A7CD RID: 42957
		private Dictionary<int, int> m_CardSourceTable;

		// Token: 0x0400A7CE RID: 42958
		private int m_TargetUniqueId;

		// Token: 0x0400A7CF RID: 42959
		private DuelClient m_Host;

		// Token: 0x0400A7D0 RID: 42960
		private bool m_CloseDuelLog;

		// Token: 0x02000E93 RID: 3731
		public enum ListType
		{
			// Token: 0x0400A7D2 RID: 42962
			NONE,
			// Token: 0x0400A7D3 RID: 42963
			EXTRA_TEAM0,
			// Token: 0x0400A7D4 RID: 42964
			EXTRA_TEAM1,
			// Token: 0x0400A7D5 RID: 42965
			GRAVE_TEAM0,
			// Token: 0x0400A7D6 RID: 42966
			GRAVE_TEAM1,
			// Token: 0x0400A7D7 RID: 42967
			EXCLUDED_TEAM0,
			// Token: 0x0400A7D8 RID: 42968
			EXCLUDED_TEAM1,
			// Token: 0x0400A7D9 RID: 42969
			OVERLAYMATLIST_TEAM0,
			// Token: 0x0400A7DA RID: 42970
			OVERLAYMATLIST_TEAM1,
			// Token: 0x0400A7DB RID: 42971
			INHERITEFFECTLIST
		}

		// Token: 0x02000E94 RID: 3732
		public enum ListStatue
		{
			// Token: 0x0400A7DD RID: 42973
			OPEN,
			// Token: 0x0400A7DE RID: 42974
			OPENING,
			// Token: 0x0400A7DF RID: 42975
			UPDATING,
			// Token: 0x0400A7E0 RID: 42976
			CLOSE,
			// Token: 0x0400A7E1 RID: 42977
			CLOSING
		}
	}
}
