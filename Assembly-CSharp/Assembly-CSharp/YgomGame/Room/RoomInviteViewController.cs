using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YgomGame.Friend;
using YgomGame.Menu;
using YgomSystem.Network;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Room
{
	// Token: 0x020009F4 RID: 2548
	public class RoomInviteViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06004A1E RID: 18974 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004A1F RID: 18975 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004A20 RID: 18976 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionEnd(ViewController.TransitionType type)
		{
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPIFriendGetList(UnityAction onSuccess = null)
		{
		}

		// Token: 0x06004A23 RID: 18979 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedEntity(GameObject gob)
		{
		}

		// Token: 0x06004A24 RID: 18980 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateEntity(GameObject gob, int index)
		{
		}

		// Token: 0x06004A25 RID: 18981 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetData()
		{
		}

		// Token: 0x06004A26 RID: 18982 RVA: 0x000029CC File Offset: 0x00000BCC
		private int CompareData(RoomInviteViewController.Data a, RoomInviteViewController.Data b)
		{
			return 0;
		}

		// Token: 0x06004A27 RID: 18983 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPIRoomInvite(long[] pcodes)
		{
		}

		// Token: 0x06004A28 RID: 18984 RVA: 0x0000216A File Offset: 0x0000036A
		private Handle APIRoomFriendInvite(long[] _invite_list_)
		{
			return null;
		}

		// Token: 0x04008816 RID: 34838
		private readonly string SCROLL_LABEL;

		// Token: 0x04008817 RID: 34839
		private readonly string BTN_INVITE_LABEL;

		// Token: 0x04008818 RID: 34840
		private readonly string INPUT_LABEL;

		// Token: 0x04008819 RID: 34841
		private readonly string TXT_EMPTY_LABEL;

		// Token: 0x0400881A RID: 34842
		private readonly string TXT_INVITE_COUNT_LABEL;

		// Token: 0x0400881B RID: 34843
		private InfinityScrollView isv;

		// Token: 0x0400881C RID: 34844
		private Dictionary<GameObject, FriendWidget> m_EntityWidgetMap;

		// Token: 0x0400881D RID: 34845
		private List<RoomInviteViewController.Data> dataList;

		// Token: 0x0400881E RID: 34846
		private int currentInvite;

		// Token: 0x0400881F RID: 34847
		private int maxInvite;

		// Token: 0x020009F5 RID: 2549
		internal class Data
		{
			// Token: 0x06004A2A RID: 18986 RVA: 0x00002739 File Offset: 0x00000939
			public Data(long pcode, object playerInfo, bool isOnline, bool isSelected = false)
			{
			}

			// Token: 0x04008820 RID: 34848
			internal long pcode;

			// Token: 0x04008821 RID: 34849
			internal object playerInfo;

			// Token: 0x04008822 RID: 34850
			internal bool isOnline;

			// Token: 0x04008823 RID: 34851
			internal bool isSelected;
		}
	}
}
