using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Friend
{
	// Token: 0x02000C13 RID: 3091
	public class FriendWidget : ElementWidgetBehaviourBase<FriendWidget>
	{
		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x060057ED RID: 22509 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060057EE RID: 22510 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectionButton button
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

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x060057EF RID: 22511 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060057F0 RID: 22512 RVA: 0x0000216D File Offset: 0x0000036D
		public PlatformPlayerNameGroup platformPlayerNameGroup
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

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x060057F1 RID: 22513 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060057F2 RID: 22514 RVA: 0x0000216D File Offset: 0x0000036D
		public PlatformPlayerIcon platformPlayerIcon
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

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x060057F3 RID: 22515 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060057F4 RID: 22516 RVA: 0x0000216D File Offset: 0x0000036D
		public GameObject profileIconRoot
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

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x060057F5 RID: 22517 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060057F6 RID: 22518 RVA: 0x0000216D File Offset: 0x0000036D
		public Image wallpaperImage
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

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x060057F7 RID: 22519 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060057F8 RID: 22520 RVA: 0x0000216D File Offset: 0x0000036D
		public GameObject followIcon
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

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x060057F9 RID: 22521 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060057FA RID: 22522 RVA: 0x0000216D File Offset: 0x0000036D
		public GameObject followerIcon
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

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x060057FB RID: 22523 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060057FC RID: 22524 RVA: 0x0000216D File Offset: 0x0000036D
		public GameObject roomBadge
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

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x060057FD RID: 22525 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject pinLineRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x060057FE RID: 22526 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject pinLineOn
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x060057FF RID: 22527 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject pinLineOff
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (set) Token: 0x06005800 RID: 22528 RVA: 0x0000216D File Offset: 0x0000036D
		public bool followStateVisible
		{
			set
			{
			}
		}

		// Token: 0x1400008D RID: 141
		// (add) Token: 0x06005801 RID: 22529 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06005802 RID: 22530 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<FriendWidget> onClickEvent
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

		// Token: 0x1400008E RID: 142
		// (add) Token: 0x06005803 RID: 22531 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06005804 RID: 22532 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<FriendWidget> onSelectedEvent
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

		// Token: 0x06005805 RID: 22533 RVA: 0x0000216A File Offset: 0x0000036A
		public static FriendWidget Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x06005806 RID: 22534 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x06005807 RID: 22535 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetProfileIcon(int iconId, int iconFrameId)
		{
		}

		// Token: 0x06005808 RID: 22536 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetWallpaper(int wallpaperId)
		{
		}

		// Token: 0x06005809 RID: 22537 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetStatusText(bool isEnableDuelWatch, bool isOnline)
		{
		}

		// Token: 0x0600580A RID: 22538 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetFollowState(FollowState followState)
		{
		}

		// Token: 0x0600580B RID: 22539 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRoomBadge(FollowState followState, int inviteRoomId, int inviteTeamId)
		{
		}

		// Token: 0x0600580C RID: 22540 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClick()
		{
		}

		// Token: 0x0600580D RID: 22541 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSelected()
		{
		}

		// Token: 0x04009497 RID: 38039
		private readonly string k_ELabelPlatformPlayerNameGroup;

		// Token: 0x04009498 RID: 38040
		private readonly string k_ELabelPlatformPlayerIcon;

		// Token: 0x04009499 RID: 38041
		private readonly string k_ELabelProfileIcon;

		// Token: 0x0400949A RID: 38042
		private readonly string k_ELabelFollowIcon;

		// Token: 0x0400949B RID: 38043
		private readonly string k_ELabelFollowerIcon;

		// Token: 0x0400949C RID: 38044
		private readonly string k_ELabelDuelIcon;

		// Token: 0x0400949D RID: 38045
		private readonly string k_ELabelOnlineIcon;

		// Token: 0x0400949E RID: 38046
		private readonly string k_ELabelOfflineIcon;

		// Token: 0x0400949F RID: 38047
		private readonly string k_ELabelFriendPinLine;

		// Token: 0x040094A0 RID: 38048
		private readonly string k_ELabelFriendPinLineOn;

		// Token: 0x040094A1 RID: 38049
		private readonly string k_ELabelFriendPinLineOff;

		// Token: 0x040094A2 RID: 38050
		private readonly string k_ELabelWallpaper;

		// Token: 0x040094A3 RID: 38051
		private readonly string k_ELabelRoomBadge;

		// Token: 0x040094A4 RID: 38052
		private readonly string k_TweenOn;

		// Token: 0x040094A5 RID: 38053
		private readonly string k_TweenOff;
	}
}
