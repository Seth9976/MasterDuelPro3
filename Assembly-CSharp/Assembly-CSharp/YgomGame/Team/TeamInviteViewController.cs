using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YgomGame.Friend;
using YgomGame.Menu;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Team
{
	// Token: 0x020008BC RID: 2236
	public class TeamInviteViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x0600416B RID: 16747 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(ViewControllerManager manager)
		{
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600416E RID: 16750 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x0600416F RID: 16751 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPIFriendGetList(UnityAction onSuccess = null)
		{
		}

		// Token: 0x06004170 RID: 16752 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionEnd(ViewController.TransitionType type)
		{
		}

		// Token: 0x06004171 RID: 16753 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedEntity(GameObject gob)
		{
		}

		// Token: 0x06004172 RID: 16754 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateEntity(GameObject gob, int index)
		{
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetData()
		{
		}

		// Token: 0x06004174 RID: 16756 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPITeamInvite(long[] pcodes)
		{
		}

		// Token: 0x04007FBD RID: 32701
		private static readonly string VC_PATH;

		// Token: 0x04007FBE RID: 32702
		private static readonly string SCROLL_LABEL;

		// Token: 0x04007FBF RID: 32703
		private static readonly string BTN_INVITE_LABEL;

		// Token: 0x04007FC0 RID: 32704
		private static readonly string TXT_EMPTY_LABEL;

		// Token: 0x04007FC1 RID: 32705
		private static readonly string TXT_INVITE_COUNT_LABEL;

		// Token: 0x04007FC2 RID: 32706
		private InfinityScrollView isv;

		// Token: 0x04007FC3 RID: 32707
		private Dictionary<GameObject, FriendWidget> m_EntityWidgetMap;

		// Token: 0x04007FC4 RID: 32708
		private List<TeamInviteViewController.Data> dataList;

		// Token: 0x04007FC5 RID: 32709
		private int currentInvite;

		// Token: 0x04007FC6 RID: 32710
		private int maxInvite;

		// Token: 0x020008BD RID: 2237
		private class Data
		{
			// Token: 0x06004176 RID: 16758 RVA: 0x00002739 File Offset: 0x00000939
			public Data(long pcode, object playerInfo, bool isOnline, bool isSelected = false)
			{
			}

			// Token: 0x04007FC7 RID: 32711
			internal long pcode;

			// Token: 0x04007FC8 RID: 32712
			internal object playerInfo;

			// Token: 0x04007FC9 RID: 32713
			internal bool isOnline;

			// Token: 0x04007FCA RID: 32714
			internal bool isSelected;
		}
	}
}
