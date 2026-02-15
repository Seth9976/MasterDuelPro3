using System;
using System.Drawing;
using System.Windows.Forms.Theming.Default;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms.Theming.VisualStyles
{
	// Token: 0x02000376 RID: 886
	internal class CheckBoxPainter : CheckBoxPainter
	{
		// Token: 0x06001CF0 RID: 7408 RVA: 0x00088430 File Offset: 0x00086630
		public override void DrawNormalCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			CheckBoxState checkBoxState;
			if (state != CheckState.Checked)
			{
				if (state != CheckState.Indeterminate)
				{
					checkBoxState = CheckBoxState.UncheckedNormal;
				}
				else
				{
					checkBoxState = CheckBoxState.MixedNormal;
				}
			}
			else
			{
				checkBoxState = CheckBoxState.CheckedNormal;
			}
			CheckBoxPainter.DrawCheckBox(g, bounds, checkBoxState);
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x0008845C File Offset: 0x0008665C
		public override void DrawHotCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			CheckBoxState checkBoxState;
			if (state != CheckState.Checked)
			{
				if (state != CheckState.Indeterminate)
				{
					checkBoxState = CheckBoxState.UncheckedHot;
				}
				else
				{
					checkBoxState = CheckBoxState.MixedHot;
				}
			}
			else
			{
				checkBoxState = CheckBoxState.CheckedHot;
			}
			CheckBoxPainter.DrawCheckBox(g, bounds, checkBoxState);
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x00088488 File Offset: 0x00086688
		public override void DrawPressedCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			CheckBoxState checkBoxState;
			if (state != CheckState.Checked)
			{
				if (state != CheckState.Indeterminate)
				{
					checkBoxState = CheckBoxState.UncheckedPressed;
				}
				else
				{
					checkBoxState = CheckBoxState.MixedPressed;
				}
			}
			else
			{
				checkBoxState = CheckBoxState.CheckedPressed;
			}
			CheckBoxPainter.DrawCheckBox(g, bounds, checkBoxState);
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x000884B4 File Offset: 0x000866B4
		public override void DrawDisabledCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			CheckBoxState checkBoxState;
			if (state != CheckState.Checked)
			{
				if (state != CheckState.Indeterminate)
				{
					checkBoxState = CheckBoxState.UncheckedDisabled;
				}
				else
				{
					checkBoxState = CheckBoxState.MixedDisabled;
				}
			}
			else
			{
				checkBoxState = CheckBoxState.CheckedDisabled;
			}
			CheckBoxPainter.DrawCheckBox(g, bounds, checkBoxState);
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x000884E0 File Offset: 0x000866E0
		private static void DrawCheckBox(Graphics g, Rectangle bounds, CheckBoxState state)
		{
			CheckBoxRenderer.DrawCheckBox(g, bounds.Location, state);
		}
	}
}
