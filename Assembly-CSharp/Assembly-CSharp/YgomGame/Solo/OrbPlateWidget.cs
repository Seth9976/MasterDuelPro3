using System;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Solo
{
	// Token: 0x020008EE RID: 2286
	public class OrbPlateWidget : ElementWidgetBase
	{
		// Token: 0x060042F2 RID: 17138 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public OrbPlateWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateOrbNum()
		{
		}

		// Token: 0x04008157 RID: 33111
		private readonly string k_EOrb;

		// Token: 0x04008158 RID: 33112
		private readonly string k_EImage;

		// Token: 0x04008159 RID: 33113
		private readonly string k_EText;

		// Token: 0x0400815A RID: 33114
		private readonly int k_DarkOrbId;
	}
}
