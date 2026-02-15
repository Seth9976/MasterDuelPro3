using System;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BB9 RID: 3001
	public class MDMarkupImageWidget : MDMarkupWidgetBase
	{
		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x060055AF RID: 21935 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool isReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060055B0 RID: 21936 RVA: 0x000F4CC8 File Offset: 0x000F2EC8
		public MDMarkupImageWidget(ElementObjectManager eom, MDMarkupIndentWidget indentWidget)
			: base(null, null)
		{
		}

		// Token: 0x060055B1 RID: 21937 RVA: 0x0000216D File Offset: 0x0000036D
		public override void BindContentData(IMDMarkupContent mdMarkupContent)
		{
		}

		// Token: 0x060055B2 RID: 21938 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnReady()
		{
		}

		// Token: 0x040092C4 RID: 37572
		private readonly string k_ELabelImage;

		// Token: 0x040092C5 RID: 37573
		public readonly Image m_Image;

		// Token: 0x040092C6 RID: 37574
		private bool m_IsReady;
	}
}
