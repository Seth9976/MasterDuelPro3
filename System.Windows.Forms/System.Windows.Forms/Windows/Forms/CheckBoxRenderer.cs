using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to render a check box control with or without visual styles. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000031 RID: 49
	public sealed class CheckBoxRenderer
	{
		/// <summary>Draws a check box control in the specified state and location.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the check box.</param>
		/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the check box glyph at.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.CheckBoxState" /> values that specifies the visual state of the check box.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000116 RID: 278 RVA: 0x00004D90 File Offset: 0x00002F90
		public static void DrawCheckBox(Graphics g, Point glyphLocation, CheckBoxState state)
		{
			CheckBoxRenderer.DrawCheckBox(g, glyphLocation, Rectangle.Empty, string.Empty, null, TextFormatFlags.HorizontalCenter, null, Rectangle.Empty, false, state);
		}

		/// <summary>Draws a check box control in the specified state and location; with the specified text, text formatting, and image; and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the check box.</param>
		/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the check box glyph at.</param>
		/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="checkBoxText" /> in.</param>
		/// <param name="checkBoxText">The <see cref="T:System.String" /> to draw with the check box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="checkBoxText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw with the check box.</param>
		/// <param name="imageBounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of <paramref name="image" />.</param>
		/// <param name="focused">true to draw a focus rectangle; otherwise, false.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.CheckBoxState" /> values that specifies the visual state of the check box.</param>
		// Token: 0x06000117 RID: 279 RVA: 0x00004DB8 File Offset: 0x00002FB8
		public static void DrawCheckBox(Graphics g, Point glyphLocation, Rectangle textBounds, string checkBoxText, Font font, TextFormatFlags flags, Image image, Rectangle imageBounds, bool focused, CheckBoxState state)
		{
			Rectangle rectangle = new Rectangle(glyphLocation, CheckBoxRenderer.GetGlyphSize(g, state));
			if (Application.RenderWithVisualStyles || CheckBoxRenderer.always_use_visual_styles)
			{
				VisualStyleRenderer checkBoxRenderer = CheckBoxRenderer.GetCheckBoxRenderer(state);
				checkBoxRenderer.DrawBackground(g, rectangle);
				if (image != null)
				{
					checkBoxRenderer.DrawImage(g, imageBounds, image);
				}
				if (focused)
				{
					ControlPaint.DrawFocusRectangle(g, textBounds);
				}
				if (checkBoxText != string.Empty)
				{
					if (state == CheckBoxState.CheckedDisabled || state == CheckBoxState.MixedDisabled || state == CheckBoxState.UncheckedDisabled)
					{
						TextRenderer.DrawText(g, checkBoxText, font, textBounds, SystemColors.GrayText, flags);
						return;
					}
					TextRenderer.DrawText(g, checkBoxText, font, textBounds, SystemColors.ControlText, flags);
					return;
				}
			}
			else
			{
				switch (state)
				{
				case CheckBoxState.UncheckedNormal:
				case CheckBoxState.UncheckedHot:
					ControlPaint.DrawCheckBox(g, rectangle, ButtonState.Normal);
					break;
				case CheckBoxState.UncheckedPressed:
				case CheckBoxState.UncheckedDisabled:
					ControlPaint.DrawCheckBox(g, rectangle, ButtonState.Inactive);
					break;
				case CheckBoxState.CheckedNormal:
				case CheckBoxState.CheckedHot:
					ControlPaint.DrawCheckBox(g, rectangle, ButtonState.Checked);
					break;
				case CheckBoxState.CheckedPressed:
					ControlPaint.DrawCheckBox(g, rectangle, ButtonState.Pushed | ButtonState.Checked);
					break;
				case CheckBoxState.CheckedDisabled:
				case CheckBoxState.MixedPressed:
				case CheckBoxState.MixedDisabled:
					ControlPaint.DrawCheckBox(g, rectangle, ButtonState.Inactive | ButtonState.Checked);
					break;
				case CheckBoxState.MixedNormal:
				case CheckBoxState.MixedHot:
					ControlPaint.DrawMixedCheckBox(g, rectangle, ButtonState.Checked);
					break;
				}
				if (image != null)
				{
					g.DrawImage(image, imageBounds);
				}
				if (focused)
				{
					ControlPaint.DrawFocusRectangle(g, textBounds);
				}
				if (checkBoxText != string.Empty)
				{
					TextRenderer.DrawText(g, checkBoxText, font, textBounds, SystemColors.ControlText, flags);
				}
			}
		}

		/// <summary>Returns the size of the check box glyph.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the check box glyph.</returns>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.CheckBoxState" /> values that specifies the visual state of the check box.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000118 RID: 280 RVA: 0x00004F11 File Offset: 0x00003111
		public static Size GetGlyphSize(Graphics g, CheckBoxState state)
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				return new Size(13, 13);
			}
			return CheckBoxRenderer.GetCheckBoxRenderer(state).GetPartSize(g, ThemeSizeType.Draw);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004F34 File Offset: 0x00003134
		private static VisualStyleRenderer GetCheckBoxRenderer(CheckBoxState state)
		{
			switch (state)
			{
			case CheckBoxState.UncheckedHot:
				return new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.UncheckedHot);
			case CheckBoxState.UncheckedPressed:
				return new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.UncheckedPressed);
			case CheckBoxState.UncheckedDisabled:
				return new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.UncheckedDisabled);
			case CheckBoxState.CheckedNormal:
				return new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.CheckedNormal);
			case CheckBoxState.CheckedHot:
				return new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.CheckedHot);
			case CheckBoxState.CheckedPressed:
				return new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.CheckedPressed);
			case CheckBoxState.CheckedDisabled:
				return new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.CheckedDisabled);
			case CheckBoxState.MixedNormal:
				return new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.MixedNormal);
			case CheckBoxState.MixedHot:
				return new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.MixedHot);
			case CheckBoxState.MixedPressed:
				return new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.MixedPressed);
			case CheckBoxState.MixedDisabled:
				return new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.MixedDisabled);
			}
			return new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.UncheckedNormal);
		}

		// Token: 0x0400011D RID: 285
		private static bool always_use_visual_styles;
	}
}
