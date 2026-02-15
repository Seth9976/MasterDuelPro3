using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to render a group box control with or without visual styles. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000BF RID: 191
	public sealed class GroupBoxRenderer
	{
		/// <summary>Draws a group box control in the specified state and bounds, with the specified text, font, and color.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the group box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the group box.</param>
		/// <param name="groupBoxText">The <see cref="T:System.String" /> to draw with the group box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="groupBoxText" />.</param>
		/// <param name="textColor">The <see cref="T:System.Drawing.Color" /> to apply to <paramref name="groupBoxText" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.GroupBoxState" /> values that specifies the visual state of the group box.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000770 RID: 1904 RVA: 0x00020B8C File Offset: 0x0001ED8C
		public static void DrawGroupBox(Graphics g, Rectangle bounds, string groupBoxText, Font font, Color textColor, GroupBoxState state)
		{
			GroupBoxRenderer.DrawGroupBox(g, bounds, groupBoxText, font, textColor, TextFormatFlags.Left, state);
		}

		/// <summary>Draws a group box control in the specified state and bounds, with the specified text, font, color, and text formatting.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the group box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the group box.</param>
		/// <param name="groupBoxText">The <see cref="T:System.String" /> to draw with the group box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="groupBoxText" />.</param>
		/// <param name="textColor">The <see cref="T:System.Drawing.Color" /> to apply to <paramref name="groupBoxText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.GroupBoxState" /> values that specifies the visual state of the group box.</param>
		// Token: 0x06000771 RID: 1905 RVA: 0x00020B9C File Offset: 0x0001ED9C
		public static void DrawGroupBox(Graphics g, Rectangle bounds, string groupBoxText, Font font, Color textColor, TextFormatFlags flags, GroupBoxState state)
		{
			Size size = TextRenderer.MeasureText(groupBoxText, font);
			if (Application.RenderWithVisualStyles || GroupBoxRenderer.always_use_visual_styles)
			{
				VisualStyleRenderer visualStyleRenderer;
				Rectangle rectangle;
				if (state == GroupBoxState.Normal || state != GroupBoxState.Disabled)
				{
					visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Button.GroupBox.Normal);
					rectangle = new Rectangle(bounds.Left, bounds.Top + size.Height / 2 - 1, bounds.Width, bounds.Height - size.Height / 2 + 1);
				}
				else
				{
					visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Button.GroupBox.Disabled);
					rectangle = new Rectangle(bounds.Left, bounds.Top + size.Height / 2 - 2, bounds.Width, bounds.Height - size.Height / 2 + 2);
				}
				if (groupBoxText == string.Empty)
				{
					visualStyleRenderer.DrawBackground(g, bounds);
				}
				else
				{
					visualStyleRenderer.DrawBackgroundExcludingArea(g, rectangle, new Rectangle(bounds.Left + 9, bounds.Top, size.Width - 3, size.Height));
				}
				if (textColor == Color.Empty)
				{
					textColor = visualStyleRenderer.GetColor(ColorProperty.TextColor);
				}
				if (groupBoxText != string.Empty)
				{
					TextRenderer.DrawText(g, groupBoxText, font, new Point(bounds.Left + 8, bounds.Top), textColor, flags);
					return;
				}
			}
			else
			{
				Rectangle rectangle2 = new Rectangle(bounds.Left, bounds.Top + size.Height / 2, bounds.Width, bounds.Height - size.Height / 2);
				Region clip = g.Clip;
				g.SetClip(new Rectangle(bounds.Left + 9, bounds.Top, size.Width - 3, size.Height), CombineMode.Exclude);
				ControlPaint.DrawBorder3D(g, rectangle2, Border3DStyle.Etched);
				g.Clip = clip;
				if (groupBoxText != string.Empty)
				{
					if (textColor == Color.Empty)
					{
						textColor = ((state == GroupBoxState.Normal) ? SystemColors.ControlText : SystemColors.GrayText);
					}
					TextRenderer.DrawText(g, groupBoxText, font, new Point(bounds.Left + 8, bounds.Top), textColor, flags);
				}
			}
		}

		// Token: 0x040004C2 RID: 1218
		private static bool always_use_visual_styles;
	}
}
