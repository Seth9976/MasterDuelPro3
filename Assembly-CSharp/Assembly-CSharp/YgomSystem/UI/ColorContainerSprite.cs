using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000584 RID: 1412
	public class ColorContainerSprite : ColorContainer
	{
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06002CBD RID: 11453 RVA: 0x0000216A File Offset: 0x0000036A
		public SpriteRenderer targetSpriteRenderer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SetColor(ColorContainer.SelectMode select_mode, ColorContainer.StatusMode status_mode, bool is_active = true)
		{
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTargetGraphicColor(ColorContainer.SelectMode select_mode, ColorContainer.StatusMode status_mode, bool is_active)
		{
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTargetGraphicColor(Color select_color, ColorContainer.ColorMode select_color_mode, Color status_color, ColorContainer.ColorMode status_color_mode, Color active_color, ColorContainer.ColorMode active_color_mode)
		{
		}

		// Token: 0x04002B02 RID: 11010
		private SpriteRenderer _targetSpriteRenderer;
	}
}
