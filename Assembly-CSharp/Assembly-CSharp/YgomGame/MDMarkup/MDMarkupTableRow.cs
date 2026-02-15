using System;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BCB RID: 3019
	public class MDMarkupTableRow : ElementWidgetBase
	{
		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06005628 RID: 22056 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005629 RID: 22057 RVA: 0x0000216D File Offset: 0x0000036D
		public bool borderVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x0600562A RID: 22058 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600562B RID: 22059 RVA: 0x0000216D File Offset: 0x0000036D
		public bool bgVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x0600562C RID: 22060 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public MDMarkupTableRow(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x04009315 RID: 37653
		private readonly string k_ELabelBG;
	}
}
