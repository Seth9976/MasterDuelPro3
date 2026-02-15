using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Friend
{
	// Token: 0x02000C0F RID: 3087
	public class FriendListWidget : ElementWidgetBehaviourBase<FriendListWidget>
	{
		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x0600579F RID: 22431 RVA: 0x0000216A File Offset: 0x0000036A
		public Selector selector
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x060057A0 RID: 22432 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<IPlayerContext> friendContexts
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x060057A1 RID: 22433 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<IPlayerContext> displayFriendContexts
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x060057A2 RID: 22434 RVA: 0x0000216A File Offset: 0x0000036A
		public InfinityScrollView scrollView
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x060057A3 RID: 22435 RVA: 0x0000216A File Offset: 0x0000036A
		public ScrollRect scrollRect
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x060057A4 RID: 22436 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060057A5 RID: 22437 RVA: 0x0000216D File Offset: 0x0000036D
		public int defaultIdx
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x060057A6 RID: 22438 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060057A7 RID: 22439 RVA: 0x0000216D File Offset: 0x0000036D
		public bool scrollEnabled
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x14000087 RID: 135
		// (add) Token: 0x060057A8 RID: 22440 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x060057A9 RID: 22441 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<IPlayerContext> onClickEntityEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x14000088 RID: 136
		// (add) Token: 0x060057AA RID: 22442 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x060057AB RID: 22443 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<int, IPlayerContext> onSelectedEntityEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x14000089 RID: 137
		// (add) Token: 0x060057AC RID: 22444 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x060057AD RID: 22445 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<bool> onOpenCloseEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x1400008A RID: 138
		// (add) Token: 0x060057AE RID: 22446 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x060057AF RID: 22447 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onUpdateDataCountEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x1400008B RID: 139
		// (add) Token: 0x060057B0 RID: 22448 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x060057B1 RID: 22449 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onReachScrollHeadEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x1400008C RID: 140
		// (add) Token: 0x060057B2 RID: 22450 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x060057B3 RID: 22451 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onReachScrollTailEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x060057B4 RID: 22452 RVA: 0x0000216A File Offset: 0x0000036A
		public static FriendListWidget Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x060057B5 RID: 22453 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x060057B6 RID: 22454 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(IReadOnlyList<IPlayerContext> friendContexts, Action callback)
		{
		}

		// Token: 0x060057B7 RID: 22455 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<IPlayerContext> SearchInnerViewportPlayers()
		{
			return null;
		}

		// Token: 0x060057B8 RID: 22456 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool Filter(IPlayerContext playerContext)
		{
			return false;
		}

		// Token: 0x060057B9 RID: 22457 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateDataCount()
		{
		}

		// Token: 0x060057BA RID: 22458 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateData()
		{
		}

		// Token: 0x060057BB RID: 22459 RVA: 0x000F1669 File Offset: 0x000EF869
		public long GetSelectedPcode()
		{
			return 0L;
		}

		// Token: 0x060057BC RID: 22460 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool TrySelectInViewportPcode(long pcode, bool focus = false, bool isIniitialSelect = false)
		{
			return false;
		}

		// Token: 0x060057BD RID: 22461 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool FocusPlayer(long pcode, bool isIniitialSelect = false)
		{
			return false;
		}

		// Token: 0x060057BE RID: 22462 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImmediateApplyMovement()
		{
		}

		// Token: 0x060057BF RID: 22463 RVA: 0x0000216D File Offset: 0x0000036D
		public void FixEntitiesSelectedTween()
		{
		}

		// Token: 0x060057C0 RID: 22464 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenOrClose(bool isOn)
		{
		}

		// Token: 0x060057C1 RID: 22465 RVA: 0x0000216D File Offset: 0x0000036D
		public void Open()
		{
		}

		// Token: 0x060057C2 RID: 22466 RVA: 0x0000216D File Offset: 0x0000036D
		public void Close()
		{
		}

		// Token: 0x060057C3 RID: 22467 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool IsSelectableDataIndex(int dataindex)
		{
			return false;
		}

		// Token: 0x060057C4 RID: 22468 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnCreatedEntity(GameObject gob)
		{
		}

		// Token: 0x060057C5 RID: 22469 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnUpdateEntity(GameObject gob, int dataindex)
		{
		}

		// Token: 0x060057C6 RID: 22470 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnScrollValueChanged(Vector2 normalizedPos)
		{
		}

		// Token: 0x060057C7 RID: 22471 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickedEntityWidget(FriendWidget clickedWidget)
		{
		}

		// Token: 0x060057C8 RID: 22472 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSelectedEntityWidget(FriendWidget selectedWidget)
		{
		}

		// Token: 0x0400944D RID: 37965
		private readonly string k_ELabelEmptyText;

		// Token: 0x0400944E RID: 37966
		private readonly int k_TemplateIdxPlayer;

		// Token: 0x0400944F RID: 37967
		private readonly int k_TemplateIdxPinLine;

		// Token: 0x04009450 RID: 37968
		private InfinityScrollView m_ScrollView;

		// Token: 0x04009451 RID: 37969
		private ScrollRect m_ScrollRect;

		// Token: 0x04009452 RID: 37970
		private Selector m_Selector;

		// Token: 0x04009453 RID: 37971
		private Dictionary<GameObject, FriendWidget> m_EntityWidgetMap;

		// Token: 0x04009454 RID: 37972
		public string nameFilter;

		// Token: 0x04009455 RID: 37973
		public bool followStateVisible;

		// Token: 0x04009456 RID: 37974
		public bool isUsePinLine;

		// Token: 0x04009457 RID: 37975
		public string emptyMessage;

		// Token: 0x04009458 RID: 37976
		public string filteredEmptyMessage;

		// Token: 0x04009459 RID: 37977
		public string filteredMessage;

		// Token: 0x0400945A RID: 37978
		private IReadOnlyList<IPlayerContext> m_FriendContexts;

		// Token: 0x0400945B RID: 37979
		private List<IPlayerContext> m_DisplayFriendContexts;

		// Token: 0x0400945C RID: 37980
		private List<int> m_TemplateIdxs;

		// Token: 0x0400945D RID: 37981
		private List<IPlayerContext> m_SearchPlayerList;

		// Token: 0x0400945E RID: 37982
		private bool m_IsContainPin;

		// Token: 0x0400945F RID: 37983
		public bool dumpPos;
	}
}
