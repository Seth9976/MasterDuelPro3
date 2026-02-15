using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides colors used for Microsoft Office display elements.</summary>
	// Token: 0x02000168 RID: 360
	public class ProfessionalColorTable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ProfessionalColorTable" /> class. </summary>
		// Token: 0x06000DCF RID: 3535 RVA: 0x0003BC27 File Offset: 0x00039E27
		public ProfessionalColorTable()
		{
			this.CalculateColors();
		}

		/// <summary>Gets the starting color of the gradient used when the button is checked.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the button is checked.</returns>
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x0003BC35 File Offset: 0x00039E35
		public virtual Color ButtonCheckedGradientBegin
		{
			get
			{
				return this.button_checked_gradient_begin;
			}
		}

		/// <summary>Gets the end color of the gradient used when the button is checked.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the button is checked.</returns>
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x0003BC3D File Offset: 0x00039E3D
		public virtual Color ButtonCheckedGradientEnd
		{
			get
			{
				return this.button_checked_gradient_end;
			}
		}

		/// <summary>Gets the solid color used when the button is checked.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color used when the button is checked.</returns>
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x0003BC45 File Offset: 0x00039E45
		public virtual Color ButtonCheckedHighlight
		{
			get
			{
				return this.button_checked_highlight;
			}
		}

		/// <summary>Gets the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientEnd" /> colors.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientEnd" /> colors.</returns>
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x0003BC4D File Offset: 0x00039E4D
		public virtual Color ButtonPressedBorder
		{
			get
			{
				return this.button_pressed_border;
			}
		}

		/// <summary>Gets the starting color of the gradient used when the button is pressed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the button is pressed.</returns>
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x0003BC55 File Offset: 0x00039E55
		public virtual Color ButtonPressedGradientBegin
		{
			get
			{
				return this.button_pressed_gradient_begin;
			}
		}

		/// <summary>Gets the end color of the gradient used when the button is pressed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the button is pressed.</returns>
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x0003BC5D File Offset: 0x00039E5D
		public virtual Color ButtonPressedGradientEnd
		{
			get
			{
				return this.button_pressed_gradient_end;
			}
		}

		/// <summary>Gets the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientEnd" /> colors.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientEnd" /> colors.</returns>
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x0003BC65 File Offset: 0x00039E65
		public virtual Color ButtonSelectedBorder
		{
			get
			{
				return this.button_selected_border;
			}
		}

		/// <summary>Gets the starting color of the gradient used when the button is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the button is selected.</returns>
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x0003BC6D File Offset: 0x00039E6D
		public virtual Color ButtonSelectedGradientBegin
		{
			get
			{
				return this.button_selected_gradient_begin;
			}
		}

		/// <summary>Gets the end color of the gradient used when the button is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the button is selected.</returns>
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x0003BC75 File Offset: 0x00039E75
		public virtual Color ButtonSelectedGradientEnd
		{
			get
			{
				return this.button_selected_gradient_end;
			}
		}

		/// <summary>Gets the solid color to use when the button is checked and selected and gradients are being used.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color to use when the button is checked and selected and gradients are being used.</returns>
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x0003BC7D File Offset: 0x00039E7D
		public virtual Color CheckPressedBackground
		{
			get
			{
				return this.check_pressed_background;
			}
		}

		/// <summary>Gets the solid color to use when the button is checked and selected and gradients are being used.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color to use when the button is checked and selected and gradients are being used.</returns>
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x0003BC85 File Offset: 0x00039E85
		public virtual Color CheckSelectedBackground
		{
			get
			{
				return this.check_selected_background;
			}
		}

		/// <summary>Gets the color to use for shadow effects on the grip (move handle).</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use for shadow effects on the grip (move handle).</returns>
		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x0003BC8D File Offset: 0x00039E8D
		public virtual Color GripDark
		{
			get
			{
				return this.grip_dark;
			}
		}

		/// <summary>Gets the color to use for highlight effects on the grip (move handle).</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use for highlight effects on the grip (move handle).</returns>
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x0003BC95 File Offset: 0x00039E95
		public virtual Color GripLight
		{
			get
			{
				return this.grip_light;
			}
		}

		/// <summary>Gets the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</returns>
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x0003BC9D File Offset: 0x00039E9D
		public virtual Color ImageMarginGradientEnd
		{
			get
			{
				return this.image_margin_gradient_end;
			}
		}

		/// <summary>Gets the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</returns>
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x0003BCA5 File Offset: 0x00039EA5
		public virtual Color ImageMarginGradientMiddle
		{
			get
			{
				return this.image_margin_gradient_middle;
			}
		}

		/// <summary>Gets the color that is the border color to use on a <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use on a <see cref="T:System.Windows.Forms.MenuStrip" />.</returns>
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x0003BCAD File Offset: 0x00039EAD
		public virtual Color MenuBorder
		{
			get
			{
				return this.menu_border;
			}
		}

		/// <summary>Gets the border color to use with a <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with a <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</returns>
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x0003BCB5 File Offset: 0x00039EB5
		public virtual Color MenuItemBorder
		{
			get
			{
				return this.menu_item_border;
			}
		}

		/// <summary>Gets the end color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</returns>
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x0003BCBD File Offset: 0x00039EBD
		public virtual Color MenuItemSelectedGradientEnd
		{
			get
			{
				return this.menu_item_selected_gradient_end;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</returns>
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x0003BCC5 File Offset: 0x00039EC5
		public virtual Color MenuStripGradientBegin
		{
			get
			{
				return this.menu_strip_gradient_begin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</returns>
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x0003BCCD File Offset: 0x00039ECD
		public virtual Color MenuStripGradientEnd
		{
			get
			{
				return this.menu_strip_gradient_end;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</returns>
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x0003BCD5 File Offset: 0x00039ED5
		public virtual Color OverflowButtonGradientBegin
		{
			get
			{
				return this.overflow_button_gradient_begin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</returns>
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x0003BCDD File Offset: 0x00039EDD
		public virtual Color OverflowButtonGradientEnd
		{
			get
			{
				return this.overflow_button_gradient_end;
			}
		}

		/// <summary>Gets the color to use to for shadow effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use to for shadow effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</returns>
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x0003BCE5 File Offset: 0x00039EE5
		public virtual Color SeparatorDark
		{
			get
			{
				return this.separator_dark;
			}
		}

		/// <summary>Gets the color to use to for highlight effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use to for highlight effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</returns>
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x0003BCED File Offset: 0x00039EED
		public virtual Color SeparatorLight
		{
			get
			{
				return this.separator_light;
			}
		}

		/// <summary>Gets the border color to use on the bottom edge of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use on the bottom edge of the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x0003BCF5 File Offset: 0x00039EF5
		public virtual Color ToolStripBorder
		{
			get
			{
				return this.tool_strip_border;
			}
		}

		/// <summary>Gets the solid background color of the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid background color of the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</returns>
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x0003BCFD File Offset: 0x00039EFD
		public virtual Color ToolStripDropDownBackground
		{
			get
			{
				return this.tool_strip_drop_down_background;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</returns>
		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x0003BD05 File Offset: 0x00039F05
		public virtual Color ToolStripGradientBegin
		{
			get
			{
				return this.tool_strip_gradient_begin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</returns>
		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x0003BD0D File Offset: 0x00039F0D
		public virtual Color ToolStripGradientEnd
		{
			get
			{
				return this.tool_strip_gradient_end;
			}
		}

		/// <summary>Gets or sets a value indicating whether to use <see cref="T:System.Drawing.SystemColors" /> rather than colors that match the current visual style. </summary>
		/// <returns>true to use <see cref="T:System.Drawing.SystemColors" />; otherwise, false. The default is false.</returns>
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x0003BD15 File Offset: 0x00039F15
		public bool UseSystemColors
		{
			get
			{
				return this.use_system_colors;
			}
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0003BD20 File Offset: 0x00039F20
		private void CalculateColors()
		{
			switch (this.GetCurrentStyle())
			{
			case ProfessionalColorTable.ColorSchemes.Classic:
				this.button_checked_gradient_begin = Color.Empty;
				this.button_checked_gradient_end = Color.Empty;
				this.button_checked_gradient_middle = Color.Empty;
				this.button_checked_highlight = Color.FromArgb(184, 191, 211);
				this.button_checked_highlight_border = Color.FromKnownColor(KnownColor.Highlight);
				this.button_pressed_border = Color.FromKnownColor(KnownColor.Highlight);
				this.button_pressed_gradient_begin = Color.FromArgb(133, 146, 181);
				this.button_pressed_gradient_end = Color.FromArgb(133, 146, 181);
				this.button_pressed_gradient_middle = Color.FromArgb(133, 146, 181);
				this.button_pressed_highlight = Color.FromArgb(131, 144, 179);
				this.button_pressed_highlight_border = Color.FromKnownColor(KnownColor.Highlight);
				this.button_selected_border = Color.FromKnownColor(KnownColor.Highlight);
				this.button_selected_gradient_begin = Color.FromArgb(182, 189, 210);
				this.button_selected_gradient_end = Color.FromArgb(182, 189, 210);
				this.button_selected_gradient_middle = Color.FromArgb(182, 189, 210);
				this.button_selected_highlight = Color.FromArgb(184, 191, 211);
				this.button_selected_highlight_border = Color.FromKnownColor(KnownColor.Highlight);
				this.check_background = Color.FromKnownColor(KnownColor.Highlight);
				this.check_pressed_background = Color.FromArgb(133, 146, 181);
				this.check_selected_background = Color.FromArgb(133, 146, 181);
				this.grip_dark = Color.FromArgb(160, 160, 160);
				this.grip_light = SystemColors.Window;
				this.image_margin_gradient_begin = Color.FromArgb(245, 244, 242);
				this.image_margin_gradient_end = SystemColors.Control;
				this.image_margin_gradient_middle = Color.FromArgb(234, 232, 228);
				this.image_margin_revealed_gradient_begin = Color.FromArgb(238, 236, 233);
				this.image_margin_revealed_gradient_end = Color.FromArgb(216, 213, 206);
				this.image_margin_revealed_gradient_middle = Color.FromArgb(225, 222, 217);
				this.menu_border = Color.FromArgb(102, 102, 102);
				this.menu_item_border = SystemColors.Highlight;
				this.menu_item_pressed_gradient_begin = Color.FromArgb(245, 244, 242);
				this.menu_item_pressed_gradient_end = Color.FromArgb(234, 232, 228);
				this.menu_item_pressed_gradient_middle = Color.FromArgb(225, 222, 217);
				this.menu_item_selected = SystemColors.Window;
				this.menu_item_selected_gradient_begin = Color.FromArgb(182, 189, 210);
				this.menu_item_selected_gradient_end = Color.FromArgb(182, 189, 210);
				this.menu_strip_gradient_begin = SystemColors.ButtonFace;
				this.menu_strip_gradient_end = Color.FromArgb(246, 245, 244);
				this.overflow_button_gradient_begin = Color.FromArgb(225, 222, 217);
				this.overflow_button_gradient_end = SystemColors.ButtonShadow;
				this.overflow_button_gradient_middle = Color.FromArgb(216, 213, 206);
				this.rafting_container_gradient_begin = SystemColors.ButtonFace;
				this.rafting_container_gradient_end = Color.FromArgb(246, 245, 244);
				this.separator_dark = Color.FromArgb(166, 166, 166);
				this.separator_light = SystemColors.ButtonHighlight;
				this.status_strip_gradient_begin = SystemColors.ButtonFace;
				this.status_strip_gradient_end = Color.FromArgb(246, 245, 244);
				this.tool_strip_border = Color.FromArgb(219, 216, 209);
				this.tool_strip_content_panel_gradient_begin = SystemColors.ButtonFace;
				this.tool_strip_content_panel_gradient_end = Color.FromArgb(246, 245, 244);
				this.tool_strip_drop_down_background = SystemColors.Window;
				this.tool_strip_gradient_begin = Color.FromArgb(245, 244, 242);
				this.tool_strip_gradient_end = SystemColors.ButtonFace;
				this.tool_strip_gradient_middle = Color.FromArgb(234, 232, 228);
				this.tool_strip_panel_gradient_begin = SystemColors.ButtonFace;
				this.tool_strip_panel_gradient_end = Color.FromArgb(246, 245, 244);
				return;
			case ProfessionalColorTable.ColorSchemes.NormalColor:
				this.button_checked_gradient_begin = (this.use_system_colors ? Color.Empty : Color.FromArgb(255, 223, 154));
				this.button_checked_gradient_end = (this.use_system_colors ? Color.Empty : Color.FromArgb(255, 166, 76));
				this.button_checked_gradient_middle = (this.use_system_colors ? Color.Empty : Color.FromArgb(255, 195, 116));
				this.button_checked_highlight = Color.FromArgb(195, 211, 237);
				this.button_checked_highlight_border = Color.FromKnownColor(KnownColor.Highlight);
				this.button_pressed_border = (this.use_system_colors ? Color.FromKnownColor(KnownColor.Highlight) : Color.FromArgb(0, 0, 128));
				this.button_pressed_gradient_begin = (this.use_system_colors ? Color.FromArgb(152, 181, 226) : Color.FromArgb(254, 128, 62));
				this.button_pressed_gradient_end = (this.use_system_colors ? Color.FromArgb(152, 181, 226) : Color.FromArgb(255, 223, 154));
				this.button_pressed_gradient_middle = (this.use_system_colors ? Color.FromArgb(152, 181, 226) : Color.FromArgb(255, 177, 109));
				this.button_pressed_highlight = (this.use_system_colors ? Color.FromArgb(150, 179, 225) : Color.FromArgb(150, 179, 225));
				this.button_pressed_highlight_border = Color.FromKnownColor(KnownColor.Highlight);
				this.button_selected_border = (this.use_system_colors ? Color.FromKnownColor(KnownColor.Highlight) : Color.FromArgb(0, 0, 128));
				this.button_selected_gradient_begin = (this.use_system_colors ? Color.FromArgb(193, 210, 238) : Color.FromArgb(255, 255, 222));
				this.button_selected_gradient_end = (this.use_system_colors ? Color.FromArgb(193, 210, 238) : Color.FromArgb(255, 203, 136));
				this.button_selected_gradient_middle = (this.use_system_colors ? Color.FromArgb(193, 210, 238) : Color.FromArgb(255, 225, 172));
				this.button_selected_highlight = (this.use_system_colors ? Color.FromArgb(195, 211, 237) : Color.FromArgb(195, 211, 237));
				this.button_selected_highlight_border = (this.use_system_colors ? Color.FromKnownColor(KnownColor.Highlight) : Color.FromArgb(0, 0, 128));
				this.check_background = (this.use_system_colors ? Color.FromKnownColor(KnownColor.Highlight) : Color.FromArgb(255, 192, 111));
				this.check_pressed_background = (this.use_system_colors ? Color.FromArgb(152, 181, 226) : Color.FromArgb(254, 128, 62));
				this.check_selected_background = (this.use_system_colors ? Color.FromArgb(152, 181, 226) : Color.FromArgb(254, 128, 62));
				this.grip_dark = (this.use_system_colors ? Color.FromArgb(193, 190, 179) : Color.FromArgb(39, 65, 118));
				this.grip_light = (this.use_system_colors ? SystemColors.Window : Color.FromArgb(255, 255, 255));
				this.image_margin_gradient_begin = (this.use_system_colors ? Color.FromArgb(251, 250, 246) : Color.FromArgb(227, 239, 255));
				this.image_margin_gradient_end = (this.use_system_colors ? SystemColors.Control : Color.FromArgb(123, 164, 224));
				this.image_margin_gradient_middle = (this.use_system_colors ? Color.FromArgb(246, 244, 236) : Color.FromArgb(203, 225, 252));
				this.image_margin_revealed_gradient_begin = (this.use_system_colors ? Color.FromArgb(247, 246, 239) : Color.FromArgb(203, 221, 246));
				this.image_margin_revealed_gradient_end = (this.use_system_colors ? Color.FromArgb(238, 235, 220) : Color.FromArgb(114, 155, 215));
				this.image_margin_revealed_gradient_middle = (this.use_system_colors ? Color.FromArgb(242, 240, 228) : Color.FromArgb(161, 197, 249));
				this.menu_border = (this.use_system_colors ? Color.FromArgb(138, 134, 122) : Color.FromArgb(0, 45, 150));
				this.menu_item_border = (this.use_system_colors ? SystemColors.Highlight : Color.FromArgb(0, 0, 128));
				this.menu_item_pressed_gradient_begin = (this.use_system_colors ? Color.FromArgb(251, 250, 246) : Color.FromArgb(227, 239, 255));
				this.menu_item_pressed_gradient_end = (this.use_system_colors ? Color.FromArgb(246, 244, 236) : Color.FromArgb(123, 164, 224));
				this.menu_item_pressed_gradient_middle = (this.use_system_colors ? Color.FromArgb(242, 240, 228) : Color.FromArgb(161, 197, 249));
				this.menu_item_selected = (this.use_system_colors ? SystemColors.Window : Color.FromArgb(255, 238, 194));
				this.menu_item_selected_gradient_begin = (this.use_system_colors ? Color.FromArgb(193, 210, 238) : Color.FromArgb(255, 255, 222));
				this.menu_item_selected_gradient_end = (this.use_system_colors ? Color.FromArgb(193, 210, 238) : Color.FromArgb(255, 203, 136));
				this.menu_strip_gradient_begin = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(158, 190, 245));
				this.menu_strip_gradient_end = (this.use_system_colors ? Color.FromArgb(251, 250, 247) : Color.FromArgb(196, 218, 250));
				this.overflow_button_gradient_begin = (this.use_system_colors ? Color.FromArgb(242, 240, 228) : Color.FromArgb(127, 177, 250));
				this.overflow_button_gradient_end = (this.use_system_colors ? SystemColors.ButtonShadow : Color.FromArgb(0, 53, 145));
				this.overflow_button_gradient_middle = (this.use_system_colors ? Color.FromArgb(238, 235, 220) : Color.FromArgb(82, 127, 208));
				this.rafting_container_gradient_begin = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(158, 190, 245));
				this.rafting_container_gradient_end = (this.use_system_colors ? Color.FromArgb(251, 250, 247) : Color.FromArgb(196, 218, 250));
				this.separator_dark = (this.use_system_colors ? Color.FromArgb(197, 194, 184) : Color.FromArgb(106, 140, 203));
				this.separator_light = (this.use_system_colors ? SystemColors.ButtonHighlight : Color.FromArgb(241, 249, 255));
				this.status_strip_gradient_begin = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(158, 190, 245));
				this.status_strip_gradient_end = (this.use_system_colors ? Color.FromArgb(251, 250, 247) : Color.FromArgb(196, 218, 250));
				this.tool_strip_border = (this.use_system_colors ? Color.FromArgb(239, 237, 222) : Color.FromArgb(59, 97, 156));
				this.tool_strip_content_panel_gradient_begin = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(158, 190, 245));
				this.tool_strip_content_panel_gradient_end = (this.use_system_colors ? Color.FromArgb(251, 250, 247) : Color.FromArgb(196, 218, 250));
				this.tool_strip_drop_down_background = (this.use_system_colors ? Color.FromArgb(252, 252, 249) : Color.FromArgb(246, 246, 246));
				this.tool_strip_gradient_begin = (this.use_system_colors ? Color.FromArgb(251, 250, 246) : Color.FromArgb(227, 239, 255));
				this.tool_strip_gradient_end = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(123, 164, 224));
				this.tool_strip_gradient_middle = (this.use_system_colors ? Color.FromArgb(246, 244, 236) : Color.FromArgb(203, 225, 252));
				this.tool_strip_panel_gradient_begin = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(158, 190, 245));
				this.tool_strip_panel_gradient_end = (this.use_system_colors ? Color.FromArgb(251, 250, 247) : Color.FromArgb(196, 218, 250));
				return;
			case ProfessionalColorTable.ColorSchemes.HomeStead:
				this.button_checked_gradient_begin = (this.use_system_colors ? Color.Empty : Color.FromArgb(255, 223, 154));
				this.button_checked_gradient_end = (this.use_system_colors ? Color.Empty : Color.FromArgb(255, 166, 76));
				this.button_checked_gradient_middle = (this.use_system_colors ? Color.Empty : Color.FromArgb(255, 195, 116));
				this.button_checked_highlight = Color.FromArgb(223, 227, 213);
				this.button_checked_highlight_border = Color.FromKnownColor(KnownColor.Highlight);
				this.button_pressed_border = (this.use_system_colors ? Color.FromKnownColor(KnownColor.Highlight) : Color.FromArgb(63, 93, 56));
				this.button_pressed_gradient_begin = (this.use_system_colors ? Color.FromArgb(201, 208, 184) : Color.FromArgb(254, 128, 62));
				this.button_pressed_gradient_end = (this.use_system_colors ? Color.FromArgb(201, 208, 184) : Color.FromArgb(255, 223, 154));
				this.button_pressed_gradient_middle = (this.use_system_colors ? Color.FromArgb(201, 208, 184) : Color.FromArgb(255, 177, 109));
				this.button_pressed_highlight = (this.use_system_colors ? Color.FromArgb(200, 206, 182) : Color.FromArgb(200, 206, 182));
				this.button_pressed_highlight_border = Color.FromKnownColor(KnownColor.Highlight);
				this.button_selected_border = (this.use_system_colors ? Color.FromKnownColor(KnownColor.Highlight) : Color.FromArgb(63, 93, 56));
				this.button_selected_gradient_begin = (this.use_system_colors ? Color.FromArgb(223, 227, 212) : Color.FromArgb(255, 255, 222));
				this.button_selected_gradient_end = (this.use_system_colors ? Color.FromArgb(223, 227, 212) : Color.FromArgb(255, 203, 136));
				this.button_selected_gradient_middle = (this.use_system_colors ? Color.FromArgb(223, 227, 212) : Color.FromArgb(255, 225, 172));
				this.button_selected_highlight = (this.use_system_colors ? Color.FromArgb(223, 227, 213) : Color.FromArgb(223, 227, 213));
				this.button_selected_highlight_border = (this.use_system_colors ? Color.FromKnownColor(KnownColor.Highlight) : Color.FromArgb(63, 93, 56));
				this.check_background = (this.use_system_colors ? Color.FromKnownColor(KnownColor.Highlight) : Color.FromArgb(255, 192, 111));
				this.check_pressed_background = (this.use_system_colors ? Color.FromArgb(201, 208, 184) : Color.FromArgb(254, 128, 62));
				this.check_selected_background = (this.use_system_colors ? Color.FromArgb(201, 208, 184) : Color.FromArgb(254, 128, 62));
				this.grip_dark = (this.use_system_colors ? Color.FromArgb(193, 190, 179) : Color.FromArgb(81, 94, 51));
				this.grip_light = (this.use_system_colors ? SystemColors.Window : Color.FromArgb(255, 255, 255));
				this.image_margin_gradient_begin = (this.use_system_colors ? Color.FromArgb(251, 250, 246) : Color.FromArgb(255, 255, 237));
				this.image_margin_gradient_end = (this.use_system_colors ? SystemColors.Control : Color.FromArgb(181, 196, 143));
				this.image_margin_gradient_middle = (this.use_system_colors ? Color.FromArgb(246, 244, 236) : Color.FromArgb(206, 220, 167));
				this.image_margin_revealed_gradient_begin = (this.use_system_colors ? Color.FromArgb(247, 246, 239) : Color.FromArgb(230, 230, 209));
				this.image_margin_revealed_gradient_end = (this.use_system_colors ? Color.FromArgb(238, 235, 220) : Color.FromArgb(160, 177, 116));
				this.image_margin_revealed_gradient_middle = (this.use_system_colors ? Color.FromArgb(242, 240, 228) : Color.FromArgb(186, 201, 143));
				this.menu_border = (this.use_system_colors ? Color.FromArgb(138, 134, 122) : Color.FromArgb(117, 141, 94));
				this.menu_item_border = (this.use_system_colors ? SystemColors.Highlight : Color.FromArgb(63, 93, 56));
				this.menu_item_pressed_gradient_begin = (this.use_system_colors ? Color.FromArgb(251, 250, 246) : Color.FromArgb(237, 240, 214));
				this.menu_item_pressed_gradient_end = (this.use_system_colors ? Color.FromArgb(246, 244, 236) : Color.FromArgb(181, 196, 143));
				this.menu_item_pressed_gradient_middle = (this.use_system_colors ? Color.FromArgb(242, 240, 228) : Color.FromArgb(186, 201, 143));
				this.menu_item_selected = (this.use_system_colors ? SystemColors.Window : Color.FromArgb(255, 238, 194));
				this.menu_item_selected_gradient_begin = (this.use_system_colors ? Color.FromArgb(223, 227, 212) : Color.FromArgb(255, 255, 222));
				this.menu_item_selected_gradient_end = (this.use_system_colors ? Color.FromArgb(223, 227, 212) : Color.FromArgb(255, 203, 136));
				this.menu_strip_gradient_begin = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(217, 217, 167));
				this.menu_strip_gradient_end = (this.use_system_colors ? Color.FromArgb(251, 250, 247) : Color.FromArgb(242, 241, 228));
				this.overflow_button_gradient_begin = (this.use_system_colors ? Color.FromArgb(242, 240, 228) : Color.FromArgb(186, 204, 150));
				this.overflow_button_gradient_end = (this.use_system_colors ? SystemColors.ButtonShadow : Color.FromArgb(96, 119, 107));
				this.overflow_button_gradient_middle = (this.use_system_colors ? Color.FromArgb(238, 235, 220) : Color.FromArgb(141, 160, 107));
				this.rafting_container_gradient_begin = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(217, 217, 167));
				this.rafting_container_gradient_end = (this.use_system_colors ? Color.FromArgb(251, 250, 247) : Color.FromArgb(242, 241, 228));
				this.separator_dark = (this.use_system_colors ? Color.FromArgb(197, 194, 184) : Color.FromArgb(96, 128, 88));
				this.separator_light = (this.use_system_colors ? SystemColors.ButtonHighlight : Color.FromArgb(244, 247, 222));
				this.status_strip_gradient_begin = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(217, 217, 167));
				this.status_strip_gradient_end = (this.use_system_colors ? Color.FromArgb(251, 250, 247) : Color.FromArgb(242, 241, 228));
				this.tool_strip_border = (this.use_system_colors ? Color.FromArgb(239, 237, 222) : Color.FromArgb(96, 128, 88));
				this.tool_strip_content_panel_gradient_begin = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(217, 217, 167));
				this.tool_strip_content_panel_gradient_end = (this.use_system_colors ? Color.FromArgb(251, 250, 247) : Color.FromArgb(242, 241, 228));
				this.tool_strip_drop_down_background = (this.use_system_colors ? Color.FromArgb(252, 252, 249) : Color.FromArgb(244, 244, 238));
				this.tool_strip_gradient_begin = (this.use_system_colors ? Color.FromArgb(251, 250, 246) : Color.FromArgb(255, 255, 237));
				this.tool_strip_gradient_end = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(181, 196, 143));
				this.tool_strip_gradient_middle = (this.use_system_colors ? Color.FromArgb(246, 244, 236) : Color.FromArgb(206, 220, 167));
				this.tool_strip_panel_gradient_begin = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(217, 217, 167));
				this.tool_strip_panel_gradient_end = (this.use_system_colors ? Color.FromArgb(251, 250, 247) : Color.FromArgb(242, 241, 228));
				return;
			case ProfessionalColorTable.ColorSchemes.Metallic:
				this.button_checked_gradient_begin = (this.use_system_colors ? Color.Empty : Color.FromArgb(255, 223, 154));
				this.button_checked_gradient_end = (this.use_system_colors ? Color.Empty : Color.FromArgb(255, 166, 76));
				this.button_checked_gradient_middle = (this.use_system_colors ? Color.Empty : Color.FromArgb(255, 195, 116));
				this.button_checked_highlight = Color.FromArgb(231, 232, 235);
				this.button_checked_highlight_border = Color.FromKnownColor(KnownColor.Highlight);
				this.button_pressed_border = (this.use_system_colors ? Color.FromKnownColor(KnownColor.Highlight) : Color.FromArgb(75, 75, 111));
				this.button_pressed_gradient_begin = (this.use_system_colors ? Color.FromArgb(217, 218, 223) : Color.FromArgb(254, 128, 62));
				this.button_pressed_gradient_end = (this.use_system_colors ? Color.FromArgb(217, 218, 223) : Color.FromArgb(255, 223, 154));
				this.button_pressed_gradient_middle = (this.use_system_colors ? Color.FromArgb(217, 218, 223) : Color.FromArgb(255, 177, 109));
				this.button_pressed_highlight = (this.use_system_colors ? Color.FromArgb(215, 216, 222) : Color.FromArgb(215, 216, 222));
				this.button_pressed_highlight_border = Color.FromKnownColor(KnownColor.Highlight);
				this.button_selected_border = (this.use_system_colors ? Color.FromKnownColor(KnownColor.Highlight) : Color.FromArgb(75, 75, 111));
				this.button_selected_gradient_begin = (this.use_system_colors ? Color.FromArgb(232, 233, 236) : Color.FromArgb(255, 255, 222));
				this.button_selected_gradient_end = (this.use_system_colors ? Color.FromArgb(232, 233, 236) : Color.FromArgb(255, 203, 136));
				this.button_selected_gradient_middle = (this.use_system_colors ? Color.FromArgb(232, 233, 236) : Color.FromArgb(255, 225, 172));
				this.button_selected_highlight = (this.use_system_colors ? Color.FromArgb(231, 232, 235) : Color.FromArgb(231, 232, 235));
				this.button_selected_highlight_border = (this.use_system_colors ? Color.FromKnownColor(KnownColor.Highlight) : Color.FromArgb(75, 75, 111));
				this.check_background = (this.use_system_colors ? Color.FromKnownColor(KnownColor.Highlight) : Color.FromArgb(255, 192, 111));
				this.check_pressed_background = (this.use_system_colors ? Color.FromArgb(217, 218, 223) : Color.FromArgb(254, 128, 62));
				this.check_selected_background = (this.use_system_colors ? Color.FromArgb(217, 218, 223) : Color.FromArgb(254, 128, 62));
				this.grip_dark = (this.use_system_colors ? Color.FromArgb(182, 182, 185) : Color.FromArgb(84, 84, 117));
				this.grip_light = (this.use_system_colors ? SystemColors.Window : Color.FromArgb(255, 255, 255));
				this.image_margin_gradient_begin = (this.use_system_colors ? Color.FromArgb(248, 248, 249) : Color.FromArgb(249, 249, 255));
				this.image_margin_gradient_end = (this.use_system_colors ? SystemColors.Control : Color.FromArgb(147, 145, 176));
				this.image_margin_gradient_middle = (this.use_system_colors ? Color.FromArgb(240, 239, 241) : Color.FromArgb(225, 226, 236));
				this.image_margin_revealed_gradient_begin = (this.use_system_colors ? Color.FromArgb(243, 242, 244) : Color.FromArgb(215, 215, 226));
				this.image_margin_revealed_gradient_end = (this.use_system_colors ? Color.FromArgb(227, 226, 230) : Color.FromArgb(118, 116, 151));
				this.image_margin_revealed_gradient_middle = (this.use_system_colors ? Color.FromArgb(233, 233, 235) : Color.FromArgb(184, 185, 202));
				this.menu_border = (this.use_system_colors ? Color.FromArgb(126, 126, 129) : Color.FromArgb(124, 124, 148));
				this.menu_item_border = (this.use_system_colors ? SystemColors.Highlight : Color.FromArgb(75, 75, 111));
				this.menu_item_pressed_gradient_begin = (this.use_system_colors ? Color.FromArgb(248, 248, 249) : Color.FromArgb(232, 233, 242));
				this.menu_item_pressed_gradient_end = (this.use_system_colors ? Color.FromArgb(240, 239, 241) : Color.FromArgb(172, 170, 194));
				this.menu_item_pressed_gradient_middle = (this.use_system_colors ? Color.FromArgb(233, 233, 235) : Color.FromArgb(184, 185, 202));
				this.menu_item_selected = (this.use_system_colors ? SystemColors.Window : Color.FromArgb(255, 238, 194));
				this.menu_item_selected_gradient_begin = (this.use_system_colors ? Color.FromArgb(232, 233, 236) : Color.FromArgb(255, 255, 222));
				this.menu_item_selected_gradient_end = (this.use_system_colors ? Color.FromArgb(232, 233, 236) : Color.FromArgb(255, 203, 136));
				this.menu_strip_gradient_begin = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(215, 215, 229));
				this.menu_strip_gradient_end = (this.use_system_colors ? Color.FromArgb(249, 248, 249) : Color.FromArgb(243, 243, 247));
				this.overflow_button_gradient_begin = (this.use_system_colors ? Color.FromArgb(233, 233, 235) : Color.FromArgb(186, 185, 206));
				this.overflow_button_gradient_end = (this.use_system_colors ? SystemColors.ButtonShadow : Color.FromArgb(118, 116, 146));
				this.overflow_button_gradient_middle = (this.use_system_colors ? Color.FromArgb(227, 226, 230) : Color.FromArgb(156, 155, 180));
				this.rafting_container_gradient_begin = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(215, 215, 229));
				this.rafting_container_gradient_end = (this.use_system_colors ? Color.FromArgb(249, 248, 249) : Color.FromArgb(243, 243, 247));
				this.separator_dark = (this.use_system_colors ? Color.FromArgb(186, 186, 189) : Color.FromArgb(110, 109, 143));
				this.separator_light = (this.use_system_colors ? SystemColors.ButtonHighlight : Color.FromArgb(255, 255, 255));
				this.status_strip_gradient_begin = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(215, 215, 229));
				this.status_strip_gradient_end = (this.use_system_colors ? Color.FromArgb(249, 248, 249) : Color.FromArgb(243, 243, 247));
				this.tool_strip_border = (this.use_system_colors ? Color.FromArgb(229, 228, 232) : Color.FromArgb(124, 124, 148));
				this.tool_strip_content_panel_gradient_begin = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(215, 215, 229));
				this.tool_strip_content_panel_gradient_end = (this.use_system_colors ? Color.FromArgb(249, 248, 249) : Color.FromArgb(243, 243, 247));
				this.tool_strip_drop_down_background = (this.use_system_colors ? Color.FromArgb(251, 250, 251) : Color.FromArgb(253, 250, 255));
				this.tool_strip_gradient_begin = (this.use_system_colors ? Color.FromArgb(248, 248, 249) : Color.FromArgb(249, 249, 255));
				this.tool_strip_gradient_end = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(147, 145, 176));
				this.tool_strip_gradient_middle = (this.use_system_colors ? Color.FromArgb(240, 239, 241) : Color.FromArgb(225, 226, 236));
				this.tool_strip_panel_gradient_begin = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(215, 215, 229));
				this.tool_strip_panel_gradient_end = (this.use_system_colors ? Color.FromArgb(249, 248, 249) : Color.FromArgb(243, 243, 247));
				return;
			case ProfessionalColorTable.ColorSchemes.MediaCenter:
				this.button_checked_gradient_begin = (this.use_system_colors ? Color.Empty : Color.FromArgb(226, 229, 238));
				this.button_checked_gradient_end = (this.use_system_colors ? Color.Empty : Color.FromArgb(226, 229, 238));
				this.button_checked_gradient_middle = (this.use_system_colors ? Color.Empty : Color.FromArgb(226, 229, 238));
				this.button_checked_highlight = Color.FromArgb(196, 208, 229);
				this.button_checked_highlight_border = Color.FromKnownColor(KnownColor.Highlight);
				this.button_pressed_border = (this.use_system_colors ? Color.FromKnownColor(KnownColor.Highlight) : Color.FromArgb(51, 94, 168));
				this.button_pressed_gradient_begin = (this.use_system_colors ? Color.FromArgb(153, 175, 212) : Color.FromArgb(153, 175, 212));
				this.button_pressed_gradient_end = (this.use_system_colors ? Color.FromArgb(153, 175, 212) : Color.FromArgb(153, 175, 212));
				this.button_pressed_gradient_middle = (this.use_system_colors ? Color.FromArgb(153, 175, 212) : Color.FromArgb(153, 175, 212));
				this.button_pressed_highlight = (this.use_system_colors ? Color.FromArgb(152, 173, 210) : Color.FromArgb(152, 173, 210));
				this.button_pressed_highlight_border = Color.FromKnownColor(KnownColor.Highlight);
				this.button_selected_border = (this.use_system_colors ? Color.FromKnownColor(KnownColor.Highlight) : Color.FromArgb(51, 94, 168));
				this.button_selected_gradient_begin = (this.use_system_colors ? Color.FromArgb(194, 207, 229) : Color.FromArgb(194, 207, 229));
				this.button_selected_gradient_end = (this.use_system_colors ? Color.FromArgb(194, 207, 229) : Color.FromArgb(194, 207, 229));
				this.button_selected_gradient_middle = (this.use_system_colors ? Color.FromArgb(194, 207, 229) : Color.FromArgb(194, 207, 229));
				this.button_selected_highlight = (this.use_system_colors ? Color.FromArgb(196, 208, 229) : Color.FromArgb(196, 208, 229));
				this.button_selected_highlight_border = (this.use_system_colors ? Color.FromKnownColor(KnownColor.Highlight) : Color.FromArgb(51, 94, 168));
				this.check_background = (this.use_system_colors ? Color.FromKnownColor(KnownColor.Highlight) : Color.FromArgb(226, 229, 238));
				this.check_pressed_background = (this.use_system_colors ? Color.FromArgb(153, 175, 212) : Color.FromArgb(51, 94, 168));
				this.check_selected_background = (this.use_system_colors ? Color.FromArgb(153, 175, 212) : Color.FromArgb(51, 94, 168));
				this.grip_dark = (this.use_system_colors ? Color.FromArgb(189, 188, 191) : Color.FromArgb(189, 188, 191));
				this.grip_light = (this.use_system_colors ? SystemColors.Window : Color.FromArgb(255, 255, 255));
				this.image_margin_gradient_begin = (this.use_system_colors ? Color.FromArgb(250, 250, 251) : Color.FromArgb(252, 252, 252));
				this.image_margin_gradient_end = (this.use_system_colors ? SystemColors.Control : Color.FromArgb(235, 233, 237));
				this.image_margin_gradient_middle = (this.use_system_colors ? Color.FromArgb(245, 244, 246) : Color.FromArgb(245, 244, 246));
				this.image_margin_revealed_gradient_begin = (this.use_system_colors ? Color.FromArgb(247, 246, 248) : Color.FromArgb(247, 246, 248));
				this.image_margin_revealed_gradient_end = (this.use_system_colors ? Color.FromArgb(237, 235, 239) : Color.FromArgb(228, 226, 230));
				this.image_margin_revealed_gradient_middle = (this.use_system_colors ? Color.FromArgb(241, 240, 242) : Color.FromArgb(241, 240, 242));
				this.menu_border = (this.use_system_colors ? Color.FromArgb(134, 133, 136) : Color.FromArgb(134, 133, 136));
				this.menu_item_border = (this.use_system_colors ? SystemColors.Highlight : Color.FromArgb(51, 94, 168));
				this.menu_item_pressed_gradient_begin = (this.use_system_colors ? Color.FromArgb(250, 250, 251) : Color.FromArgb(252, 252, 252));
				this.menu_item_pressed_gradient_end = (this.use_system_colors ? Color.FromArgb(245, 244, 246) : Color.FromArgb(245, 244, 246));
				this.menu_item_pressed_gradient_middle = (this.use_system_colors ? Color.FromArgb(241, 240, 242) : Color.FromArgb(241, 240, 242));
				this.menu_item_selected = (this.use_system_colors ? SystemColors.Window : Color.FromArgb(194, 207, 229));
				this.menu_item_selected_gradient_begin = (this.use_system_colors ? Color.FromArgb(194, 207, 229) : Color.FromArgb(194, 207, 229));
				this.menu_item_selected_gradient_end = (this.use_system_colors ? Color.FromArgb(194, 207, 229) : Color.FromArgb(194, 207, 229));
				this.menu_strip_gradient_begin = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(235, 233, 237));
				this.menu_strip_gradient_end = (this.use_system_colors ? Color.FromArgb(251, 250, 251) : Color.FromArgb(251, 250, 251));
				this.overflow_button_gradient_begin = (this.use_system_colors ? Color.FromArgb(241, 240, 242) : Color.FromArgb(242, 242, 242));
				this.overflow_button_gradient_end = (this.use_system_colors ? SystemColors.ButtonShadow : Color.FromArgb(167, 166, 170));
				this.overflow_button_gradient_middle = (this.use_system_colors ? Color.FromArgb(237, 235, 239) : Color.FromArgb(224, 224, 225));
				this.rafting_container_gradient_begin = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(235, 233, 237));
				this.rafting_container_gradient_end = (this.use_system_colors ? Color.FromArgb(251, 250, 251) : Color.FromArgb(251, 250, 251));
				this.separator_dark = (this.use_system_colors ? Color.FromArgb(193, 193, 196) : Color.FromArgb(193, 193, 196));
				this.separator_light = (this.use_system_colors ? SystemColors.ButtonHighlight : Color.FromArgb(255, 255, 255));
				this.status_strip_gradient_begin = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(235, 233, 237));
				this.status_strip_gradient_end = (this.use_system_colors ? Color.FromArgb(251, 250, 251) : Color.FromArgb(251, 250, 251));
				this.tool_strip_border = (this.use_system_colors ? Color.FromArgb(238, 237, 240) : Color.FromArgb(238, 237, 240));
				this.tool_strip_content_panel_gradient_begin = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(235, 233, 237));
				this.tool_strip_content_panel_gradient_end = (this.use_system_colors ? Color.FromArgb(251, 250, 251) : Color.FromArgb(251, 250, 251));
				this.tool_strip_drop_down_background = (this.use_system_colors ? Color.FromArgb(252, 252, 252) : Color.FromArgb(252, 252, 252));
				this.tool_strip_gradient_begin = (this.use_system_colors ? Color.FromArgb(250, 250, 251) : Color.FromArgb(252, 252, 252));
				this.tool_strip_gradient_end = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(235, 233, 237));
				this.tool_strip_gradient_middle = (this.use_system_colors ? Color.FromArgb(245, 244, 246) : Color.FromArgb(245, 244, 246));
				this.tool_strip_panel_gradient_begin = (this.use_system_colors ? SystemColors.ButtonFace : Color.FromArgb(235, 233, 237));
				this.tool_strip_panel_gradient_end = (this.use_system_colors ? Color.FromArgb(251, 250, 251) : Color.FromArgb(251, 250, 251));
				return;
			case ProfessionalColorTable.ColorSchemes.Aero:
				this.button_checked_gradient_begin = Color.Empty;
				this.button_checked_gradient_end = Color.Empty;
				this.button_checked_gradient_middle = Color.Empty;
				this.button_checked_highlight = Color.FromArgb(196, 225, 255);
				this.button_checked_highlight_border = Color.FromKnownColor(KnownColor.Highlight);
				this.button_pressed_border = Color.FromKnownColor(KnownColor.Highlight);
				this.button_pressed_gradient_begin = Color.FromArgb(153, 204, 255);
				this.button_pressed_gradient_end = Color.FromArgb(153, 204, 255);
				this.button_pressed_gradient_middle = Color.FromArgb(153, 204, 255);
				this.button_pressed_highlight = Color.FromArgb(152, 203, 255);
				this.button_pressed_highlight_border = Color.FromKnownColor(KnownColor.Highlight);
				this.button_selected_border = (this.use_system_colors ? Color.FromKnownColor(KnownColor.Highlight) : Color.FromArgb(51, 94, 168));
				this.button_selected_gradient_begin = Color.FromArgb(194, 224, 255);
				this.button_selected_gradient_end = Color.FromArgb(194, 224, 255);
				this.button_selected_gradient_middle = Color.FromArgb(194, 224, 255);
				this.button_selected_highlight = Color.FromArgb(196, 225, 255);
				this.button_selected_highlight_border = Color.FromKnownColor(KnownColor.Highlight);
				this.check_background = Color.FromKnownColor(KnownColor.Highlight);
				this.check_pressed_background = Color.FromArgb(153, 204, 255);
				this.check_selected_background = Color.FromArgb(153, 204, 255);
				this.grip_dark = Color.FromArgb(184, 184, 184);
				this.grip_light = SystemColors.Window;
				this.image_margin_gradient_begin = Color.FromArgb(252, 252, 252);
				this.image_margin_gradient_end = SystemColors.Control;
				this.image_margin_gradient_middle = Color.FromArgb(250, 250, 250);
				this.image_margin_revealed_gradient_begin = Color.FromArgb(251, 251, 251);
				this.image_margin_revealed_gradient_end = Color.FromArgb(245, 245, 245);
				this.image_margin_revealed_gradient_middle = Color.FromArgb(247, 247, 247);
				this.menu_border = Color.FromArgb(128, 128, 128);
				this.menu_item_border = SystemColors.Highlight;
				this.menu_item_pressed_gradient_begin = Color.FromArgb(252, 252, 252);
				this.menu_item_pressed_gradient_end = Color.FromArgb(250, 250, 250);
				this.menu_item_pressed_gradient_middle = Color.FromArgb(247, 247, 247);
				this.menu_item_selected = SystemColors.Window;
				this.menu_item_selected_gradient_begin = Color.FromArgb(194, 224, 255);
				this.menu_item_selected_gradient_end = Color.FromArgb(194, 224, 255);
				this.menu_strip_gradient_begin = SystemColors.ButtonFace;
				this.menu_strip_gradient_end = Color.FromArgb(253, 253, 253);
				this.overflow_button_gradient_begin = Color.FromArgb(247, 247, 247);
				this.overflow_button_gradient_end = SystemColors.ButtonShadow;
				this.overflow_button_gradient_middle = Color.FromArgb(245, 245, 245);
				this.rafting_container_gradient_begin = SystemColors.ButtonFace;
				this.rafting_container_gradient_end = Color.FromArgb(253, 253, 253);
				this.separator_dark = Color.FromArgb(189, 189, 189);
				this.separator_light = SystemColors.ButtonHighlight;
				this.status_strip_gradient_begin = SystemColors.ButtonFace;
				this.status_strip_gradient_end = Color.FromArgb(253, 253, 253);
				this.tool_strip_border = Color.FromArgb(246, 246, 246);
				this.tool_strip_content_panel_gradient_begin = SystemColors.ButtonFace;
				this.tool_strip_content_panel_gradient_end = Color.FromArgb(253, 253, 253);
				this.tool_strip_drop_down_background = Color.FromArgb(253, 253, 253);
				this.tool_strip_gradient_begin = Color.FromArgb(252, 252, 252);
				this.tool_strip_gradient_end = SystemColors.ButtonFace;
				this.tool_strip_gradient_middle = Color.FromArgb(250, 250, 250);
				this.tool_strip_panel_gradient_begin = SystemColors.ButtonFace;
				this.tool_strip_panel_gradient_end = Color.FromArgb(253, 253, 253);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0003EFDC File Offset: 0x0003D1DC
		private ProfessionalColorTable.ColorSchemes GetCurrentStyle()
		{
			if (!VisualStyleInformation.IsEnabledByUser || string.IsNullOrEmpty(VisualStylesEngine.Instance.VisualStyleInformationFileName))
			{
				return ProfessionalColorTable.ColorSchemes.Classic;
			}
			string text = Path.GetFileNameWithoutExtension(VisualStylesEngine.Instance.VisualStyleInformationFileName).ToLowerInvariant();
			if (text == "aero")
			{
				return ProfessionalColorTable.ColorSchemes.Aero;
			}
			if (text == "royale")
			{
				return ProfessionalColorTable.ColorSchemes.MediaCenter;
			}
			text = VisualStyleInformation.ColorScheme;
			if (text == "NormalColor")
			{
				return ProfessionalColorTable.ColorSchemes.NormalColor;
			}
			if (text == "HomeStead")
			{
				return ProfessionalColorTable.ColorSchemes.HomeStead;
			}
			if (!(text == "Metallic"))
			{
				return ProfessionalColorTable.ColorSchemes.Classic;
			}
			return ProfessionalColorTable.ColorSchemes.Metallic;
		}

		// Token: 0x0400089D RID: 2205
		private bool use_system_colors;

		// Token: 0x0400089E RID: 2206
		private Color button_checked_gradient_begin;

		// Token: 0x0400089F RID: 2207
		private Color button_checked_gradient_end;

		// Token: 0x040008A0 RID: 2208
		private Color button_checked_gradient_middle;

		// Token: 0x040008A1 RID: 2209
		private Color button_checked_highlight;

		// Token: 0x040008A2 RID: 2210
		private Color button_checked_highlight_border;

		// Token: 0x040008A3 RID: 2211
		private Color button_pressed_border;

		// Token: 0x040008A4 RID: 2212
		private Color button_pressed_gradient_begin;

		// Token: 0x040008A5 RID: 2213
		private Color button_pressed_gradient_end;

		// Token: 0x040008A6 RID: 2214
		private Color button_pressed_gradient_middle;

		// Token: 0x040008A7 RID: 2215
		private Color button_pressed_highlight;

		// Token: 0x040008A8 RID: 2216
		private Color button_pressed_highlight_border;

		// Token: 0x040008A9 RID: 2217
		private Color button_selected_border;

		// Token: 0x040008AA RID: 2218
		private Color button_selected_gradient_begin;

		// Token: 0x040008AB RID: 2219
		private Color button_selected_gradient_end;

		// Token: 0x040008AC RID: 2220
		private Color button_selected_gradient_middle;

		// Token: 0x040008AD RID: 2221
		private Color button_selected_highlight;

		// Token: 0x040008AE RID: 2222
		private Color button_selected_highlight_border;

		// Token: 0x040008AF RID: 2223
		private Color check_background;

		// Token: 0x040008B0 RID: 2224
		private Color check_pressed_background;

		// Token: 0x040008B1 RID: 2225
		private Color check_selected_background;

		// Token: 0x040008B2 RID: 2226
		private Color grip_dark;

		// Token: 0x040008B3 RID: 2227
		private Color grip_light;

		// Token: 0x040008B4 RID: 2228
		private Color image_margin_gradient_begin;

		// Token: 0x040008B5 RID: 2229
		private Color image_margin_gradient_end;

		// Token: 0x040008B6 RID: 2230
		private Color image_margin_gradient_middle;

		// Token: 0x040008B7 RID: 2231
		private Color image_margin_revealed_gradient_begin;

		// Token: 0x040008B8 RID: 2232
		private Color image_margin_revealed_gradient_end;

		// Token: 0x040008B9 RID: 2233
		private Color image_margin_revealed_gradient_middle;

		// Token: 0x040008BA RID: 2234
		private Color menu_border;

		// Token: 0x040008BB RID: 2235
		private Color menu_item_border;

		// Token: 0x040008BC RID: 2236
		private Color menu_item_pressed_gradient_begin;

		// Token: 0x040008BD RID: 2237
		private Color menu_item_pressed_gradient_end;

		// Token: 0x040008BE RID: 2238
		private Color menu_item_pressed_gradient_middle;

		// Token: 0x040008BF RID: 2239
		private Color menu_item_selected;

		// Token: 0x040008C0 RID: 2240
		private Color menu_item_selected_gradient_begin;

		// Token: 0x040008C1 RID: 2241
		private Color menu_item_selected_gradient_end;

		// Token: 0x040008C2 RID: 2242
		private Color menu_strip_gradient_begin;

		// Token: 0x040008C3 RID: 2243
		private Color menu_strip_gradient_end;

		// Token: 0x040008C4 RID: 2244
		private Color overflow_button_gradient_begin;

		// Token: 0x040008C5 RID: 2245
		private Color overflow_button_gradient_end;

		// Token: 0x040008C6 RID: 2246
		private Color overflow_button_gradient_middle;

		// Token: 0x040008C7 RID: 2247
		private Color rafting_container_gradient_begin;

		// Token: 0x040008C8 RID: 2248
		private Color rafting_container_gradient_end;

		// Token: 0x040008C9 RID: 2249
		private Color separator_dark;

		// Token: 0x040008CA RID: 2250
		private Color separator_light;

		// Token: 0x040008CB RID: 2251
		private Color status_strip_gradient_begin;

		// Token: 0x040008CC RID: 2252
		private Color status_strip_gradient_end;

		// Token: 0x040008CD RID: 2253
		private Color tool_strip_border;

		// Token: 0x040008CE RID: 2254
		private Color tool_strip_content_panel_gradient_begin;

		// Token: 0x040008CF RID: 2255
		private Color tool_strip_content_panel_gradient_end;

		// Token: 0x040008D0 RID: 2256
		private Color tool_strip_drop_down_background;

		// Token: 0x040008D1 RID: 2257
		private Color tool_strip_gradient_begin;

		// Token: 0x040008D2 RID: 2258
		private Color tool_strip_gradient_end;

		// Token: 0x040008D3 RID: 2259
		private Color tool_strip_gradient_middle;

		// Token: 0x040008D4 RID: 2260
		private Color tool_strip_panel_gradient_begin;

		// Token: 0x040008D5 RID: 2261
		private Color tool_strip_panel_gradient_end;

		// Token: 0x02000169 RID: 361
		private enum ColorSchemes
		{
			// Token: 0x040008D7 RID: 2263
			Classic,
			// Token: 0x040008D8 RID: 2264
			NormalColor,
			// Token: 0x040008D9 RID: 2265
			HomeStead,
			// Token: 0x040008DA RID: 2266
			Metallic,
			// Token: 0x040008DB RID: 2267
			MediaCenter,
			// Token: 0x040008DC RID: 2268
			Aero
		}
	}
}
