using System;
using System.Collections.Generic;

namespace YgomGame.SubMenu
{
	// Token: 0x020008E4 RID: 2276
	public class DeckEditSubMenuViewController : SubMenuViewController
	{
		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060042B4 RID: 17076 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060042B5 RID: 17077 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060042B6 RID: 17078 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060042B7 RID: 17079 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(Action onClickMassDiamangle, Action onClickMultiDismantle, Action onClickTrialDraw, Action onClickMultiCreate)
		{
		}

		// Token: 0x060042B8 RID: 17080 RVA: 0x0000216A File Offset: 0x0000036A
		private static Dictionary<string, object> CreateArgs(Action onClickMassDiamangle, Action onClickMultiDismantle, Action onClickTrialDraw, Action onClickMultiCreate)
		{
			return null;
		}

		// Token: 0x04008125 RID: 33061
		private const string argKeyOnClickMassDismantle = "OnClickMassDismantle";

		// Token: 0x04008126 RID: 33062
		private const string argKeyOnClickMultiDismantle = "OnClickMultiDismantle";

		// Token: 0x04008127 RID: 33063
		private const string argKeyOnClickTrialDraw = "OnClickTrialDraw";

		// Token: 0x04008128 RID: 33064
		private const string argKeyOnClickMultiCreate = "OnClickMultiCreate";
	}
}
