using System;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x02000581 RID: 1409
	public class ColorContainerGraphic : ColorContainer
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06002CA7 RID: 11431 RVA: 0x000F1DC8 File Offset: 0x000EFFC8
		public Graphic targetGraphic
		{
			get
			{
				if (this._targetGraphic == null)
				{
					this._targetGraphic = base.GetComponent<Graphic>();
				}
				return this._targetGraphic;
			}
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x000F1DEA File Offset: 0x000EFFEA
		public override void SetColor(ColorContainer.SelectMode select_mode, ColorContainer.StatusMode status_mode, bool is_active = true)
		{
			this.SetTargetGraphicColor(select_mode, status_mode, is_active);
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x000F1DF8 File Offset: 0x000EFFF8
		private void SetTargetGraphicColor(ColorContainer.SelectMode select_mode, ColorContainer.StatusMode status_mode, bool is_active)
		{
			Color color = this.baseColor;
			if (select_mode != ColorContainer.SelectMode.Unselected)
			{
				if (select_mode == ColorContainer.SelectMode.Selected)
				{
					if (this.colorModeSelected == ColorContainer.ColorMode.Multiple)
					{
						color *= base.GetColorSelected();
					}
					else
					{
						color = base.GetColorSelected();
					}
				}
			}
			else if (this.colorModeUnselected == ColorContainer.ColorMode.Multiple)
			{
				color *= base.GetColorUnselected();
			}
			else
			{
				color = base.GetColorUnselected();
			}
			if (status_mode != ColorContainer.StatusMode.Down)
			{
				if (status_mode == ColorContainer.StatusMode.Enter)
				{
					if (this.colorModeButtonEnter == ColorContainer.ColorMode.Multiple)
					{
						color *= base.GetColorButtonEnter();
					}
					else
					{
						color = base.GetColorButtonEnter();
					}
				}
			}
			else if (this.colorModeButtonDown == ColorContainer.ColorMode.Multiple)
			{
				color *= base.GetColorButtonDown();
			}
			else
			{
				color = base.GetColorButtonDown();
			}
			if (!is_active)
			{
				if (this.colorModeButtonInactive == ColorContainer.ColorMode.Multiple)
				{
					color *= base.GetColorButtonInactive();
				}
				else
				{
					color = base.GetColorButtonInactive();
				}
			}
			this.targetGraphic.color = color;
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTargetGraphicColor(Color select_color, ColorContainer.ColorMode select_color_mode, Color status_color, ColorContainer.ColorMode status_color_mode, Color active_color, ColorContainer.ColorMode active_color_mode)
		{
		}

		// Token: 0x04002AF9 RID: 11001
		private Graphic _targetGraphic;
	}
}
