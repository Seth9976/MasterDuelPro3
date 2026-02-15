using System;
using System.Collections.Generic;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000A4D RID: 2637
	public class BindingPrefabViewController : BaseMenuViewController, IDynamicChangeDispHeaderSupported
	{
		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06004CE7 RID: 19687 RVA: 0x0000216A File Offset: 0x0000036A
		public ElementObjectManager view
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004CE8 RID: 19688 RVA: 0x000029CC File Offset: 0x00000BCC
		public HeaderViewController.IsDispHeader IsDispContents()
		{
			return (HeaderViewController.IsDispHeader)0;
		}

		// Token: 0x06004CE9 RID: 19689 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PushOpen(ViewControllerManager manager, string prefPath, Action<BindingPrefabViewController> onStackEntryCallback, Action<BindingPrefabViewController> onCreateViewCallback, Action<BindingPrefabViewController> onStackRemoveCallback, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06004CEA RID: 19690 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004CEB RID: 19691 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004CEC RID: 19692 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x04008AA8 RID: 35496
		private const string k_ArgsPrefPath = "prefPath";

		// Token: 0x04008AA9 RID: 35497
		private const string k_ArgsOnStackEntryCallback = "onStackEntryCallback";

		// Token: 0x04008AAA RID: 35498
		private const string k_ArgsOnCreateViewCallback = "onCreateViewCallback";

		// Token: 0x04008AAB RID: 35499
		private const string k_ArgsOnStackRemoveCallback = "onStackRemoveCallback";

		// Token: 0x04008AAC RID: 35500
		public HeaderViewController.IsDispHeader headerDispFlags;
	}
}
