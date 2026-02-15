using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YgomGame.Menu;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Room
{
	// Token: 0x020009F8 RID: 2552
	public class RoomReplayViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06004A35 RID: 18997 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004A36 RID: 18998 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004A37 RID: 18999 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004A38 RID: 19000 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateEntity(GameObject gob, int index)
		{
		}

		// Token: 0x06004A39 RID: 19001 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetData()
		{
		}

		// Token: 0x06004A3A RID: 19002 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPIRoomGetResultList(UnityAction onSuccess = null)
		{
		}

		// Token: 0x04008839 RID: 34873
		private readonly string SCROLL_LABEL;

		// Token: 0x0400883A RID: 34874
		private readonly string TXT_WIN_LABEL;

		// Token: 0x0400883B RID: 34875
		private readonly string TXT_LOSE_LABEL;

		// Token: 0x0400883C RID: 34876
		private readonly string TXT_DRAW_LABEL;

		// Token: 0x0400883D RID: 34877
		private readonly string IMG_ICON_LABEL;

		// Token: 0x0400883E RID: 34878
		private readonly string PLATFORM_NAME_LABEL;

		// Token: 0x0400883F RID: 34879
		private readonly string PLATFORM_ICON_LABEL;

		// Token: 0x04008840 RID: 34880
		private readonly string TXT_EMPTY_LABEL;

		// Token: 0x04008841 RID: 34881
		private InfinityScrollView isv;

		// Token: 0x04008842 RID: 34882
		private List<RoomReplayViewController.ReplayData> dataList;

		// Token: 0x04008843 RID: 34883
		private bool isReplay;

		// Token: 0x020009F9 RID: 2553
		public enum BattleStatus
		{
			// Token: 0x04008845 RID: 34885
			WIN = 1,
			// Token: 0x04008846 RID: 34886
			LOSE,
			// Token: 0x04008847 RID: 34887
			DRAW
		}

		// Token: 0x020009FA RID: 2554
		internal class ReplayData
		{
			// Token: 0x06004A3C RID: 19004 RVA: 0x00002739 File Offset: 0x00000939
			public ReplayData(RoomReplayViewController.MemberData[] members, long did)
			{
			}

			// Token: 0x04008848 RID: 34888
			internal RoomReplayViewController.MemberData[] members;

			// Token: 0x04008849 RID: 34889
			internal long did;
		}

		// Token: 0x020009FB RID: 2555
		internal class MemberData
		{
			// Token: 0x0400884A RID: 34890
			internal long pcode;

			// Token: 0x0400884B RID: 34891
			internal string name;

			// Token: 0x0400884C RID: 34892
			internal int iconID;

			// Token: 0x0400884D RID: 34893
			internal int iconFrameID;

			// Token: 0x0400884E RID: 34894
			internal RoomReplayViewController.BattleStatus batteStatus;

			// Token: 0x0400884F RID: 34895
			internal bool isResistedPlatform;

			// Token: 0x04008850 RID: 34896
			internal bool isSamePlatform;

			// Token: 0x04008851 RID: 34897
			internal string platformName;
		}
	}
}
