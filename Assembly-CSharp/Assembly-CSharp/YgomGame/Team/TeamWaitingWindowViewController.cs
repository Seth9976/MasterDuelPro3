using System;
using YgomGame.Menu;
using YgomSystem.UI;

namespace YgomGame.Team
{
	// Token: 0x020008E3 RID: 2275
	public class TeamWaitingWindowViewController : BaseMenuViewController
	{
		// Token: 0x060042AF RID: 17071 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(ViewControllerManager manager, string message = "")
		{
		}

		// Token: 0x060042B0 RID: 17072 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool Close(ViewControllerManager manager)
		{
			return false;
		}

		// Token: 0x060042B1 RID: 17073 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060042B2 RID: 17074 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x04008120 RID: 33056
		public const string PREFAB_PATH = "Team/TeamWaitingWindow";

		// Token: 0x04008121 RID: 33057
		private const string KEY_MESSAGE = "message";

		// Token: 0x04008122 RID: 33058
		private readonly string E_ButtonCancel;

		// Token: 0x04008123 RID: 33059
		private readonly string E_RootText;

		// Token: 0x04008124 RID: 33060
		private readonly string E_TextSearching;
	}
}
