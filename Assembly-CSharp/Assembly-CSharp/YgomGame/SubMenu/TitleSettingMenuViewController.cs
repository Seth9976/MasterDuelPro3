using System;

namespace YgomGame.SubMenu
{
	// Token: 0x020008E8 RID: 2280
	public class TitleSettingMenuViewController : SubMenuViewController
	{
		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060042CE RID: 17102 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060042CF RID: 17103 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060042D0 RID: 17104 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x04008142 RID: 33090
		private bool isFirstPlay;

		// Token: 0x04008143 RID: 33091
		private bool cacheClearflag;
	}
}
