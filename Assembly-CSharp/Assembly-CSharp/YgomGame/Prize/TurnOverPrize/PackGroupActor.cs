using System;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Prize.TurnOverPrize
{
	// Token: 0x02000A19 RID: 2585
	public class PackGroupActor : ElementWidgetBase
	{
		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06004B05 RID: 19205 RVA: 0x0000216A File Offset: 0x0000036A
		public PackActor[] packs
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004B06 RID: 19206 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public PackGroupActor(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06004B07 RID: 19207 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetButtonsEnabled(bool enabled)
		{
		}

		// Token: 0x06004B08 RID: 19208 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetArrowsVisible(bool enabled)
		{
		}

		// Token: 0x04008929 RID: 35113
		private const string k_ELabelPackFormat = "Pack{0:D2}";

		// Token: 0x0400892A RID: 35114
		private PackActor[] m_Packs;
	}
}
