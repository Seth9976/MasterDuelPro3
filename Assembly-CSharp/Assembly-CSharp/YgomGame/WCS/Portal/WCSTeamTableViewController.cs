using System;
using System.Collections;
using YgomGame.Menu;
using YgomSystem.Network;
using YgomSystem.UI;

namespace YgomGame.WCS.Portal
{
	// Token: 0x02000817 RID: 2071
	public class WCSTeamTableViewController : WCSTeamTableViewControllerBase, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06004002 RID: 16386 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected virtual float pollingPeriod
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06004003 RID: 16387 RVA: 0x000029CC File Offset: 0x00000BCC
		public int roomId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06004004 RID: 16388 RVA: 0x0000216A File Offset: 0x0000036A
		public string roomUniqueId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06004005 RID: 16389 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual object roomInfo
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004006 RID: 16390 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(ViewControllerManager manager, int roomId, string roomUniqueId)
		{
		}

		// Token: 0x06004007 RID: 16391 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06004008 RID: 16392 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionEnd(ViewController.TransitionType type)
		{
		}

		// Token: 0x06004009 RID: 16393 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnDestroy()
		{
		}

		// Token: 0x0600400A RID: 16394 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600400B RID: 16395 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void InitializeView()
		{
		}

		// Token: 0x0600400C RID: 16396 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void ApplyData()
		{
		}

		// Token: 0x0600400D RID: 16397 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenTeamIntroduction(int teamId)
		{
		}

		// Token: 0x0600400E RID: 16398 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void CallWatchDuelAPI(int roomId, string roomUniqueId, int tableIndex)
		{
		}

		// Token: 0x0600400F RID: 16399 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartPolling()
		{
		}

		// Token: 0x06004010 RID: 16400 RVA: 0x0000216D File Offset: 0x0000036D
		private void EndPolling()
		{
		}

		// Token: 0x06004011 RID: 16401 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual Handle CallPollingAPI()
		{
			return null;
		}

		// Token: 0x06004012 RID: 16402 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator Polling()
		{
			return null;
		}

		// Token: 0x06004013 RID: 16403 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual bool IsForceLeaving(WcsCode err)
		{
			return false;
		}

		// Token: 0x04003927 RID: 14631
		private const string VC_PREFAB_PATH = "WCS/Portal/WCSTeamTableForPortal";

		// Token: 0x04003928 RID: 14632
		private const string ARG_KEY_ROOM_ID = "room_id";

		// Token: 0x04003929 RID: 14633
		private const string ARG_KEY_ROOM_UNIQUE_ID = "room_unique_id";

		// Token: 0x0400392A RID: 14634
		private int _roomId;

		// Token: 0x0400392B RID: 14635
		private string _roomUniqueId;

		// Token: 0x0400392C RID: 14636
		private IEnumerator _pollingRoutine;
	}
}
