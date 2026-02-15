using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.Network;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Friend
{
	// Token: 0x02000C11 RID: 3089
	public class FriendViewController : BaseBlurOverlayViewController
	{
		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x060057CA RID: 22474 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060057CB RID: 22475 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060057CC RID: 22476 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060057CD RID: 22477 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x060057CE RID: 22478 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStack(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x060057CF RID: 22479 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x060057D0 RID: 22480 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenPlayerActionSheet(IPlayerContext player, FriendViewController.PlayerActionSheetEntry visibleFlags)
		{
		}

		// Token: 0x060057D1 RID: 22481 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayToOpen(GameObject target)
		{
		}

		// Token: 0x060057D2 RID: 22482 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayToClose(GameObject target)
		{
		}

		// Token: 0x060057D3 RID: 22483 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayFollowerLoadingIcon(bool isHead)
		{
		}

		// Token: 0x060057D4 RID: 22484 RVA: 0x0000216D File Offset: 0x0000036D
		private void StopFollowerLoadingIcon(bool isHead)
		{
		}

		// Token: 0x060057D5 RID: 22485 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFollowToggleChange(bool isOn)
		{
		}

		// Token: 0x060057D6 RID: 22486 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFollowerToggleChange(bool isOn)
		{
		}

		// Token: 0x060057D7 RID: 22487 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnBlockToggleChange(bool isOn)
		{
		}

		// Token: 0x060057D8 RID: 22488 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnActiveToggleChange(int toggleIdx, bool isOn)
		{
		}

		// Token: 0x060057D9 RID: 22489 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFollowerReachScrollHead()
		{
		}

		// Token: 0x060057DA RID: 22490 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFollowerReachScrollTail()
		{
		}

		// Token: 0x060057DB RID: 22491 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFollowerAdditionalLoad(long date, long pcode, int dir, Action complete)
		{
		}

		// Token: 0x060057DC RID: 22492 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickFriendSearchButton()
		{
		}

		// Token: 0x060057DD RID: 22493 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSubmitFollowFilter(string input)
		{
		}

		// Token: 0x060057DE RID: 22494 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFollowUpdateDataCount()
		{
		}

		// Token: 0x060057DF RID: 22495 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnOpenCloseFollowList(bool isOpen)
		{
		}

		// Token: 0x060057E0 RID: 22496 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnOpenCloseFollowerList(bool isOpen)
		{
		}

		// Token: 0x060057E1 RID: 22497 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnOpenCloseBlockList(bool isOpen)
		{
		}

		// Token: 0x060057E2 RID: 22498 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickFollowPlayer(IPlayerContext player)
		{
		}

		// Token: 0x060057E3 RID: 22499 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickFollowerPlayer(IPlayerContext player)
		{
		}

		// Token: 0x060057E4 RID: 22500 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickBlockPlayer(IPlayerContext player)
		{
		}

		// Token: 0x060057E5 RID: 22501 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickFriendProfile(long pcode)
		{
		}

		// Token: 0x060057E6 RID: 22502 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickFriendSetPin(long pcode)
		{
		}

		// Token: 0x060057E7 RID: 22503 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickBlockOff(long pcode)
		{
		}

		// Token: 0x060057E8 RID: 22504 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickFriendDuelEntry(long pcode)
		{
		}

		// Token: 0x060057E9 RID: 22505 RVA: 0x0000216A File Offset: 0x0000036A
		private Handle APIRoomEntry(int _id_, int _is_specter_, Dictionary<string, object> _options_)
		{
			return null;
		}

		// Token: 0x060057EA RID: 22506 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickFriendTeamEntry(int teamId)
		{
		}

		// Token: 0x060057EB RID: 22507 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickCancel()
		{
		}

		// Token: 0x04009460 RID: 37984
		private readonly string k_FriendActionSheetIcon;

		// Token: 0x04009461 RID: 37985
		private readonly string k_ELabelBackButton;

		// Token: 0x04009462 RID: 37986
		private readonly string k_ELabelCancelButton;

		// Token: 0x04009463 RID: 37987
		private readonly string k_ELabelFollowRoot;

		// Token: 0x04009464 RID: 37988
		private readonly string k_ELabelFriendSearchButton;

		// Token: 0x04009465 RID: 37989
		private readonly string k_ELabelFollowNumText;

		// Token: 0x04009466 RID: 37990
		private readonly string k_ELabelFollowToggle;

		// Token: 0x04009467 RID: 37991
		private readonly string k_ELabelFollowList;

		// Token: 0x04009468 RID: 37992
		private readonly string k_ELabelFollowFilterInput;

		// Token: 0x04009469 RID: 37993
		private readonly string k_ELabelFollowFilterIcon;

		// Token: 0x0400946A RID: 37994
		private readonly string k_ELabelFollowInnerSelector;

		// Token: 0x0400946B RID: 37995
		private readonly string k_ELabelFollowerRoot;

		// Token: 0x0400946C RID: 37996
		private readonly string k_ELabelFollowerToggle;

		// Token: 0x0400946D RID: 37997
		private readonly string k_ELabelFollowerList;

		// Token: 0x0400946E RID: 37998
		private readonly string k_ELabelFollowerHeadLoadingIcon;

		// Token: 0x0400946F RID: 37999
		private readonly string k_ELabelFollowerTailLoadingIcon;

		// Token: 0x04009470 RID: 38000
		private readonly string k_ELabelBlockRoot;

		// Token: 0x04009471 RID: 38001
		private readonly string k_ELabelBlockToggle;

		// Token: 0x04009472 RID: 38002
		private readonly string k_ELabelBlockList;

		// Token: 0x04009473 RID: 38003
		private readonly string k_ASArgsPcode;

		// Token: 0x04009474 RID: 38004
		private readonly string k_TweenToOpen;

		// Token: 0x04009475 RID: 38005
		private readonly string k_TweenListToClose;

		// Token: 0x04009476 RID: 38006
		private readonly string k_TweenSearchActive;

		// Token: 0x04009477 RID: 38007
		private readonly string k_TweenSearchInactive;

		// Token: 0x04009478 RID: 38008
		private ElementObjectManager m_FriendActionSheetIconPref;

		// Token: 0x04009479 RID: 38009
		private FriendDefinitionSetting m_FriendDefinitionSetting;

		// Token: 0x0400947A RID: 38010
		private GameObject m_FollowRoot;

		// Token: 0x0400947B RID: 38011
		private ToggleWidget m_FollowToggle;

		// Token: 0x0400947C RID: 38012
		private FriendListWidget m_FollowListWidget;

		// Token: 0x0400947D RID: 38013
		private TMP_Text m_FollowNumText;

		// Token: 0x0400947E RID: 38014
		private Selector m_FollowInnerSelector;

		// Token: 0x0400947F RID: 38015
		private InputFieldWidget m_FollowFilterInput;

		// Token: 0x04009480 RID: 38016
		private GameObject m_FollowerRoot;

		// Token: 0x04009481 RID: 38017
		private ToggleWidget m_FollowerToggle;

		// Token: 0x04009482 RID: 38018
		private FriendListWidget m_FollowerListWidget;

		// Token: 0x04009483 RID: 38019
		private GameObject m_BlockRoot;

		// Token: 0x04009484 RID: 38020
		private ToggleWidget m_BlockToggle;

		// Token: 0x04009485 RID: 38021
		private FriendListWidget m_BlockListWidget;

		// Token: 0x04009486 RID: 38022
		private bool m_IsReady;

		// Token: 0x04009487 RID: 38023
		private bool m_IsInitializedFollowerList;

		// Token: 0x04009488 RID: 38024
		private bool m_IsInitializedBlockList;

		// Token: 0x04009489 RID: 38025
		private FollowContextCollection m_FollowContexts;

		// Token: 0x0400948A RID: 38026
		private FollowerContextCollection m_FollowerContexts;

		// Token: 0x0400948B RID: 38027
		private BlockContextCollection m_BlockContexts;

		// Token: 0x0400948C RID: 38028
		private bool m_Quite;

		// Token: 0x0400948D RID: 38029
		private bool m_IsSuspended;

		// Token: 0x0400948E RID: 38030
		private float m_PollingTimer;

		// Token: 0x0400948F RID: 38031
		private float m_PollingSpan;

		// Token: 0x04009490 RID: 38032
		private List<long> m_SearchDisplayPcodeList;

		// Token: 0x02000C12 RID: 3090
		private enum PlayerActionSheetEntry
		{
			// Token: 0x04009492 RID: 38034
			Profile = 2,
			// Token: 0x04009493 RID: 38035
			Pin = 4,
			// Token: 0x04009494 RID: 38036
			Audience = 8,
			// Token: 0x04009495 RID: 38037
			Duel = 16,
			// Token: 0x04009496 RID: 38038
			Block = 32
		}
	}
}
