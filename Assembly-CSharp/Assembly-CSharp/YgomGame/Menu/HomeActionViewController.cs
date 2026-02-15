using System;
using System.Collections.Generic;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000A83 RID: 2691
	public class HomeActionViewController : BaseMenuViewController, ICommonHeaderSupported, IGemSupported, IConfigButtonSupported
	{
		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06004EC3 RID: 20163 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsPlaying
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06004EC4 RID: 20164 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004EC5 RID: 20165 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(List<HomeAction> actionList)
		{
		}

		// Token: 0x06004EC6 RID: 20166 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06004EC7 RID: 20167 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStack(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x06004EC8 RID: 20168 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004EC9 RID: 20169 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004ECA RID: 20170 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayHomeAction()
		{
		}

		// Token: 0x04008C8F RID: 35983
		public const string PREFAB_PATH = "Home/HomeAction";

		// Token: 0x04008C90 RID: 35984
		public const int P_HELP = 100;

		// Token: 0x04008C91 RID: 35985
		public const int P_ROOM_INVITE = 50;

		// Token: 0x04008C92 RID: 35986
		public const int P_PARTICIPATION_CONFIRM = 40;

		// Token: 0x04008C93 RID: 35987
		public const int P_LOGIN_BONUS = 30;

		// Token: 0x04008C94 RID: 35988
		public const int P_FORCE_NOTIFY = 20;

		// Token: 0x04008C95 RID: 35989
		public const int P_EVENT_NOTIFY = 10;

		// Token: 0x04008C96 RID: 35990
		public const int P_DEFAULT = 0;

		// Token: 0x04008C97 RID: 35991
		private const string ARG_KEY_ACTIONS = "Actions";

		// Token: 0x04008C98 RID: 35992
		private List<HomeAction> actionList;

		// Token: 0x04008C99 RID: 35993
		private int isPlayingCount;

		// Token: 0x04008C9A RID: 35994
		private bool isAcceptedPop;

		// Token: 0x04008C9B RID: 35995
		private int currentActionIndex;
	}
}
