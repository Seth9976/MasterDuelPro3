using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000583 RID: 1411
	public class ColorContainerMatHDR : ColorContainer
	{
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06002CB9 RID: 11449 RVA: 0x0000216A File Offset: 0x0000036A
		private Renderer targetRenderer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x000F1ED8 File Offset: 0x000F00D8
		private Color GetColorByMode(ColorContainer.SelectMode selectMode, ColorContainer.StatusMode statusMode, bool isActive)
		{
			return default(Color);
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SetColor(ColorContainer.SelectMode select_mode, ColorContainer.StatusMode status_mode, bool is_active = true)
		{
		}

		// Token: 0x04002B00 RID: 11008
		public string colorParamName;

		// Token: 0x04002B01 RID: 11009
		private Renderer m_TargetRenderer;
	}
}
