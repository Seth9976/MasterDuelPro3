using System;
using System.Collections.Generic;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000A52 RID: 2642
	public class CushionViewController : BaseMenuViewController
	{
		// Token: 0x06004D32 RID: 19762 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(ViewController rootView, Action pushAction, Action popAction)
		{
		}

		// Token: 0x06004D33 RID: 19763 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(Dictionary<string, object> args)
		{
		}

		// Token: 0x06004D34 RID: 19764 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004D35 RID: 19765 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004D36 RID: 19766 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStack(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x04008ACD RID: 35533
		public const string PREFAB_PATH = "Utility/CushionView";

		// Token: 0x04008ACE RID: 35534
		public const string k_ArgsKeyRootView = "RootView";

		// Token: 0x04008ACF RID: 35535
		public const string k_ArgsKeyPushAction = "PushAction";

		// Token: 0x04008AD0 RID: 35536
		public const string k_ArgsKeyPopAction = "PopAction";

		// Token: 0x04008AD1 RID: 35537
		private ViewController m_RootView;

		// Token: 0x04008AD2 RID: 35538
		private Action m_PushAction;

		// Token: 0x04008AD3 RID: 35539
		private Action m_PopAction;
	}
}
