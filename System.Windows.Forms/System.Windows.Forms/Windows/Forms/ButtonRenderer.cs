using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to render a button control with or without visual styles. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200002A RID: 42
	public sealed class ButtonRenderer
	{
		/// <summary>Draws a button control in the specified state and bounds; with the specified text, text formatting, and image; and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the button.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the button.</param>
		/// <param name="buttonText">The <see cref="T:System.String" /> to draw on the button.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="buttonText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values to apply to <paramref name="buttonText" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw on the button.</param>
		/// <param name="imageBounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of <paramref name="image" />.</param>
		/// <param name="focused">true to draw a focus rectangle on the button; otherwise, false.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
		// Token: 0x060000F7 RID: 247 RVA: 0x00004948 File Offset: 0x00002B48
		public static void DrawButton(Graphics g, Rectangle bounds, string buttonText, Font font, TextFormatFlags flags, Image image, Rectangle imageBounds, bool focused, PushButtonState state)
		{
			if (Application.RenderWithVisualStyles || ButtonRenderer.always_use_visual_styles)
			{
				VisualStyleRenderer pushButtonRenderer = ButtonRenderer.GetPushButtonRenderer(state);
				pushButtonRenderer.DrawBackground(g, bounds);
				if (image != null)
				{
					pushButtonRenderer.DrawImage(g, imageBounds, image);
				}
			}
			else
			{
				if (state == PushButtonState.Pressed)
				{
					ControlPaint.DrawButton(g, bounds, ButtonState.Pushed);
				}
				else
				{
					ControlPaint.DrawButton(g, bounds, ButtonState.Normal);
				}
				if (image != null)
				{
					g.DrawImage(image, imageBounds);
				}
			}
			Rectangle rectangle = bounds;
			rectangle.Inflate(-3, -3);
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, rectangle);
			}
			if (buttonText != string.Empty)
			{
				if (state == PushButtonState.Disabled)
				{
					TextRenderer.DrawText(g, buttonText, font, rectangle, SystemColors.GrayText, flags);
					return;
				}
				TextRenderer.DrawText(g, buttonText, font, rectangle, SystemColors.ControlText, flags);
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000049F8 File Offset: 0x00002BF8
		internal static VisualStyleRenderer GetPushButtonRenderer(PushButtonState state)
		{
			switch (state)
			{
			case PushButtonState.Normal:
				return new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Normal);
			case PushButtonState.Hot:
				return new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Hot);
			case PushButtonState.Pressed:
				return new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Pressed);
			case PushButtonState.Disabled:
				return new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Disabled);
			}
			return new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Default);
		}

		// Token: 0x04000100 RID: 256
		private static bool always_use_visual_styles;
	}
}
