using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x020001A9 RID: 425
	internal class ThemeVisualStyles : ThemeWin32Classic
	{
		// Token: 0x060010F6 RID: 4342 RVA: 0x00050C15 File Offset: 0x0004EE15
		public ThemeVisualStyles()
		{
			ThemeVisualStyles.Update();
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00050C22 File Offset: 0x0004EE22
		public override void ResetDefaults()
		{
			base.ResetDefaults();
			ThemeVisualStyles.Update();
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00050C2F File Offset: 0x0004EE2F
		private static void Update()
		{
			bool isEnabledByUser = VisualStyleInformation.IsEnabledByUser;
			ThemeVisualStyles.render_client_areas = isEnabledByUser && (Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled || Application.VisualStyleState == VisualStyleState.ClientAreaEnabled);
			ThemeVisualStyles.render_non_client_areas = isEnabledByUser && Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled;
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x00050C66 File Offset: 0x0004EE66
		public static bool RenderClientAreas
		{
			get
			{
				return ThemeVisualStyles.render_client_areas;
			}
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00050C70 File Offset: 0x0004EE70
		public override void DrawButtonBase(Graphics dc, Rectangle clip_area, ButtonBase button)
		{
			if (button.FlatStyle == FlatStyle.System)
			{
				ButtonRenderer.DrawButton(dc, new Rectangle(Point.Empty, button.Size), button.Text, button.Font, button.TextFormatFlags, null, Rectangle.Empty, ThemeWin32Classic.ShouldPaintFocusRectagle(button), ThemeVisualStyles.GetPushButtonState(button));
				return;
			}
			base.DrawButtonBase(dc, clip_area, button);
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00050CCA File Offset: 0x0004EECA
		private static PushButtonState GetPushButtonState(ButtonBase button)
		{
			if (!button.Enabled)
			{
				return PushButtonState.Disabled;
			}
			if (button.Pressed)
			{
				return PushButtonState.Pressed;
			}
			if (button.Entered)
			{
				return PushButtonState.Hot;
			}
			if (button.IsDefault || button.Focused || button.paint_as_acceptbutton)
			{
				return PushButtonState.Default;
			}
			return PushButtonState.Normal;
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00050D05 File Offset: 0x0004EF05
		public override void DrawButtonBackground(Graphics g, Button button, Rectangle clipArea)
		{
			if (!ThemeVisualStyles.RenderClientAreas || !button.UseVisualStyleBackColor)
			{
				base.DrawButtonBackground(g, button, clipArea);
				return;
			}
			ButtonRenderer.GetPushButtonRenderer(ThemeVisualStyles.GetPushButtonState(button)).DrawBackground(g, new Rectangle(Point.Empty, button.Size));
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00050D41 File Offset: 0x0004EF41
		protected override void CheckBox_DrawCheckBox(Graphics dc, CheckBox checkbox, ButtonState state, Rectangle checkbox_rectangle)
		{
			if (checkbox.Appearance == Appearance.Normal && checkbox.FlatStyle == FlatStyle.System)
			{
				CheckBoxRenderer.DrawCheckBox(dc, new Point(checkbox_rectangle.Left, checkbox_rectangle.Top), ThemeVisualStyles.GetCheckBoxState(checkbox));
				return;
			}
			base.CheckBox_DrawCheckBox(dc, checkbox, state, checkbox_rectangle);
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00050D80 File Offset: 0x0004EF80
		private static CheckBoxState GetCheckBoxState(CheckBox checkBox)
		{
			CheckState checkState = checkBox.CheckState;
			if (checkState != CheckState.Checked)
			{
				if (checkState != CheckState.Indeterminate)
				{
					if (!checkBox.Enabled)
					{
						return CheckBoxState.UncheckedDisabled;
					}
					if (checkBox.Pressed)
					{
						return CheckBoxState.UncheckedPressed;
					}
					if (checkBox.Entered)
					{
						return CheckBoxState.UncheckedHot;
					}
					return CheckBoxState.UncheckedNormal;
				}
				else
				{
					if (!checkBox.Enabled)
					{
						return CheckBoxState.MixedDisabled;
					}
					if (checkBox.Pressed)
					{
						return CheckBoxState.MixedPressed;
					}
					if (checkBox.Entered)
					{
						return CheckBoxState.MixedHot;
					}
					return CheckBoxState.MixedNormal;
				}
			}
			else
			{
				if (!checkBox.Enabled)
				{
					return CheckBoxState.CheckedDisabled;
				}
				if (checkBox.Pressed)
				{
					return CheckBoxState.CheckedPressed;
				}
				if (checkBox.Entered)
				{
					return CheckBoxState.CheckedHot;
				}
				return CheckBoxState.CheckedNormal;
			}
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00050E01 File Offset: 0x0004F001
		private static VisualStyleElement ComboBoxGetVisualStyleElement(ComboBox comboBox, ButtonState state)
		{
			if (state == ButtonState.Inactive)
			{
				return VisualStyleElement.ComboBox.DropDownButton.Disabled;
			}
			if (state == ButtonState.Pushed)
			{
				return VisualStyleElement.ComboBox.DropDownButton.Pressed;
			}
			if (comboBox.DropDownButtonEntered)
			{
				return VisualStyleElement.ComboBox.DropDownButton.Hot;
			}
			return VisualStyleElement.ComboBox.DropDownButton.Normal;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00050E34 File Offset: 0x0004F034
		public override void ComboBoxDrawNormalDropDownButton(ComboBox comboBox, Graphics g, Rectangle clippingArea, Rectangle area, ButtonState state)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.ComboBoxDrawNormalDropDownButton(comboBox, g, clippingArea, area, state);
				return;
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.ComboBoxGetVisualStyleElement(comboBox, state);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.ComboBoxDrawNormalDropDownButton(comboBox, g, clippingArea, area, state);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(g, area, clippingArea);
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00050E84 File Offset: 0x0004F084
		public override bool ComboBoxNormalDropDownButtonHasTransparentBackground(ComboBox comboBox, ButtonState state)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				return base.ComboBoxNormalDropDownButtonHasTransparentBackground(comboBox, state);
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.ComboBoxGetVisualStyleElement(comboBox, state);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				return base.ComboBoxNormalDropDownButtonHasTransparentBackground(comboBox, state);
			}
			return new VisualStyleRenderer(visualStyleElement).IsBackgroundPartiallyTransparent();
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00050EC8 File Offset: 0x0004F0C8
		public override bool ComboBoxDropDownButtonHasHotElementStyle(ComboBox comboBox)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				return base.ComboBoxDropDownButtonHasHotElementStyle(comboBox);
			}
			FlatStyle flatStyle = comboBox.FlatStyle;
			return flatStyle > FlatStyle.Popup || base.ComboBoxDropDownButtonHasHotElementStyle(comboBox);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00050EF8 File Offset: 0x0004F0F8
		private static bool ComboBoxShouldPaintBackground(ComboBox comboBox)
		{
			if (comboBox.DropDownStyle == ComboBoxStyle.Simple)
			{
				return false;
			}
			FlatStyle flatStyle = comboBox.FlatStyle;
			return flatStyle > FlatStyle.Popup;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00050F20 File Offset: 0x0004F120
		public override void ComboBoxDrawBackground(ComboBox comboBox, Graphics g, Rectangle clippingArea, FlatStyle style)
		{
			if (!ThemeVisualStyles.RenderClientAreas || !ThemeVisualStyles.ComboBoxShouldPaintBackground(comboBox))
			{
				base.ComboBoxDrawBackground(comboBox, g, clippingArea, style);
				return;
			}
			VisualStyleElement visualStyleElement;
			if (!comboBox.Enabled)
			{
				visualStyleElement = VisualStyleElement.ComboBox.Border.Disabled;
			}
			else if (comboBox.Entered)
			{
				visualStyleElement = VisualStyleElement.ComboBox.Border.Hot;
			}
			else if (comboBox.Focused)
			{
				visualStyleElement = VisualStyleElement.ComboBox.Border.Focused;
			}
			else
			{
				visualStyleElement = VisualStyleElement.ComboBox.Border.Normal;
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.ComboBoxDrawBackground(comboBox, g, clippingArea, style);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(g, new Rectangle(Point.Empty, comboBox.Size), clippingArea);
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00050FAF File Offset: 0x0004F1AF
		public override bool CombBoxBackgroundHasHotElementStyle(ComboBox comboBox)
		{
			return (ThemeVisualStyles.RenderClientAreas && ThemeVisualStyles.ComboBoxShouldPaintBackground(comboBox) && comboBox.Enabled && VisualStyleRenderer.IsElementDefined(VisualStyleElement.ComboBox.Border.Hot)) || base.CombBoxBackgroundHasHotElementStyle(comboBox);
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00050FE0 File Offset: 0x0004F1E0
		public override void CPDrawButton(Graphics dc, Rectangle rectangle, ButtonState state)
		{
			if (!ThemeVisualStyles.RenderClientAreas || (state & ButtonState.Flat) == ButtonState.Flat || (state & ButtonState.Checked) == ButtonState.Checked)
			{
				base.CPDrawButton(dc, rectangle, state);
				return;
			}
			VisualStyleElement visualStyleElement;
			if ((state & ButtonState.Inactive) == ButtonState.Inactive)
			{
				visualStyleElement = VisualStyleElement.Button.PushButton.Disabled;
			}
			else if ((state & ButtonState.Pushed) == ButtonState.Pushed)
			{
				visualStyleElement = VisualStyleElement.Button.PushButton.Pressed;
			}
			else
			{
				visualStyleElement = VisualStyleElement.Button.PushButton.Normal;
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.CPDrawButton(dc, rectangle, state);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, rectangle);
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0005106C File Offset: 0x0004F26C
		public override void CPDrawCaptionButton(Graphics graphics, Rectangle rectangle, CaptionButton button, ButtonState state)
		{
			if (!ThemeVisualStyles.RenderClientAreas || (state & ButtonState.Flat) == ButtonState.Flat || (state & ButtonState.Checked) == ButtonState.Checked)
			{
				base.CPDrawCaptionButton(graphics, rectangle, button, state);
				return;
			}
			VisualStyleElement captionButtonVisualStyleElement = ThemeVisualStyles.GetCaptionButtonVisualStyleElement(button, state);
			if (!VisualStyleRenderer.IsElementDefined(captionButtonVisualStyleElement))
			{
				base.CPDrawCaptionButton(graphics, rectangle, button, state);
				return;
			}
			new VisualStyleRenderer(captionButtonVisualStyleElement).DrawBackground(graphics, rectangle);
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x000510D4 File Offset: 0x0004F2D4
		private static VisualStyleElement GetCaptionButtonVisualStyleElement(CaptionButton button, ButtonState state)
		{
			switch (button)
			{
			case CaptionButton.Close:
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					return VisualStyleElement.Window.CloseButton.Disabled;
				}
				if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					return VisualStyleElement.Window.CloseButton.Pressed;
				}
				return VisualStyleElement.Window.CloseButton.Normal;
			case CaptionButton.Minimize:
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					return VisualStyleElement.Window.MinButton.Disabled;
				}
				if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					return VisualStyleElement.Window.MinButton.Pressed;
				}
				return VisualStyleElement.Window.MinButton.Normal;
			case CaptionButton.Maximize:
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					return VisualStyleElement.Window.MaxButton.Disabled;
				}
				if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					return VisualStyleElement.Window.MaxButton.Pressed;
				}
				return VisualStyleElement.Window.MaxButton.Normal;
			case CaptionButton.Restore:
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					return VisualStyleElement.Window.RestoreButton.Disabled;
				}
				if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					return VisualStyleElement.Window.RestoreButton.Pressed;
				}
				return VisualStyleElement.Window.RestoreButton.Normal;
			default:
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					return VisualStyleElement.Window.HelpButton.Disabled;
				}
				if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					return VisualStyleElement.Window.HelpButton.Pressed;
				}
				return VisualStyleElement.Window.HelpButton.Normal;
			}
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x000511E4 File Offset: 0x0004F3E4
		public override void CPDrawCheckBox(Graphics dc, Rectangle rectangle, ButtonState state)
		{
			if (!ThemeVisualStyles.RenderClientAreas || (state & ButtonState.Flat) == ButtonState.Flat)
			{
				base.CPDrawCheckBox(dc, rectangle, state);
				return;
			}
			VisualStyleElement visualStyleElement;
			if ((state & ButtonState.Checked) == ButtonState.Checked)
			{
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					visualStyleElement = VisualStyleElement.Button.CheckBox.CheckedDisabled;
				}
				else if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					visualStyleElement = VisualStyleElement.Button.CheckBox.CheckedPressed;
				}
				else
				{
					visualStyleElement = VisualStyleElement.Button.CheckBox.CheckedNormal;
				}
			}
			else if ((state & ButtonState.Inactive) == ButtonState.Inactive)
			{
				visualStyleElement = VisualStyleElement.Button.CheckBox.UncheckedDisabled;
			}
			else if ((state & ButtonState.Pushed) == ButtonState.Pushed)
			{
				visualStyleElement = VisualStyleElement.Button.CheckBox.UncheckedPressed;
			}
			else
			{
				visualStyleElement = VisualStyleElement.Button.CheckBox.UncheckedNormal;
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.CPDrawCheckBox(dc, rectangle, state);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, rectangle);
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x000512A4 File Offset: 0x0004F4A4
		public override void CPDrawComboButton(Graphics graphics, Rectangle rectangle, ButtonState state)
		{
			if (!ThemeVisualStyles.RenderClientAreas || (state & ButtonState.Flat) == ButtonState.Flat || (state & ButtonState.Checked) == ButtonState.Checked)
			{
				base.CPDrawComboButton(graphics, rectangle, state);
				return;
			}
			VisualStyleElement visualStyleElement;
			if ((state & ButtonState.Inactive) == ButtonState.Inactive)
			{
				visualStyleElement = VisualStyleElement.ComboBox.DropDownButton.Disabled;
			}
			else if ((state & ButtonState.Pushed) == ButtonState.Pushed)
			{
				visualStyleElement = VisualStyleElement.ComboBox.DropDownButton.Pressed;
			}
			else
			{
				visualStyleElement = VisualStyleElement.ComboBox.DropDownButton.Normal;
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.CPDrawComboButton(graphics, rectangle, state);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(graphics, rectangle);
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00051330 File Offset: 0x0004F530
		public override void CPDrawMixedCheckBox(Graphics dc, Rectangle rectangle, ButtonState state)
		{
			if (!ThemeVisualStyles.RenderClientAreas || (state & ButtonState.Flat) == ButtonState.Flat)
			{
				base.CPDrawMixedCheckBox(dc, rectangle, state);
				return;
			}
			VisualStyleElement visualStyleElement;
			if ((state & ButtonState.Checked) == ButtonState.Checked)
			{
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					visualStyleElement = VisualStyleElement.Button.CheckBox.MixedDisabled;
				}
				else if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					visualStyleElement = VisualStyleElement.Button.CheckBox.MixedPressed;
				}
				else
				{
					visualStyleElement = VisualStyleElement.Button.CheckBox.MixedNormal;
				}
			}
			else if ((state & ButtonState.Inactive) == ButtonState.Inactive)
			{
				visualStyleElement = VisualStyleElement.Button.CheckBox.UncheckedDisabled;
			}
			else if ((state & ButtonState.Pushed) == ButtonState.Pushed)
			{
				visualStyleElement = VisualStyleElement.Button.CheckBox.UncheckedPressed;
			}
			else
			{
				visualStyleElement = VisualStyleElement.Button.CheckBox.UncheckedNormal;
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.CPDrawMixedCheckBox(dc, rectangle, state);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, rectangle);
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x000513F0 File Offset: 0x0004F5F0
		public override void CPDrawScrollButton(Graphics dc, Rectangle area, ScrollButton type, ButtonState state)
		{
			if (!ThemeVisualStyles.RenderClientAreas || (state & ButtonState.Flat) == ButtonState.Flat || (state & ButtonState.Checked) == ButtonState.Checked)
			{
				base.CPDrawScrollButton(dc, area, type, state);
				return;
			}
			VisualStyleElement scrollButtonVisualStyleElement = ThemeVisualStyles.GetScrollButtonVisualStyleElement(type, state);
			if (!VisualStyleRenderer.IsElementDefined(scrollButtonVisualStyleElement))
			{
				base.CPDrawScrollButton(dc, area, type, state);
				return;
			}
			new VisualStyleRenderer(scrollButtonVisualStyleElement).DrawBackground(dc, area);
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00051458 File Offset: 0x0004F658
		private static VisualStyleElement GetScrollButtonVisualStyleElement(ScrollButton type, ButtonState state)
		{
			switch (type)
			{
			case ScrollButton.Min:
				if (ThemeVisualStyles.IsDisabled(state))
				{
					return VisualStyleElement.ScrollBar.ArrowButton.UpDisabled;
				}
				if (ThemeVisualStyles.IsPressed(state))
				{
					return VisualStyleElement.ScrollBar.ArrowButton.UpPressed;
				}
				return VisualStyleElement.ScrollBar.ArrowButton.UpNormal;
			case ScrollButton.Left:
				if (ThemeVisualStyles.IsDisabled(state))
				{
					return VisualStyleElement.ScrollBar.ArrowButton.LeftDisabled;
				}
				if (ThemeVisualStyles.IsPressed(state))
				{
					return VisualStyleElement.ScrollBar.ArrowButton.LeftPressed;
				}
				return VisualStyleElement.ScrollBar.ArrowButton.LeftNormal;
			case ScrollButton.Right:
				if (ThemeVisualStyles.IsDisabled(state))
				{
					return VisualStyleElement.ScrollBar.ArrowButton.RightDisabled;
				}
				if (ThemeVisualStyles.IsPressed(state))
				{
					return VisualStyleElement.ScrollBar.ArrowButton.RightPressed;
				}
				return VisualStyleElement.ScrollBar.ArrowButton.RightNormal;
			}
			if (ThemeVisualStyles.IsDisabled(state))
			{
				return VisualStyleElement.ScrollBar.ArrowButton.DownDisabled;
			}
			if (ThemeVisualStyles.IsPressed(state))
			{
				return VisualStyleElement.ScrollBar.ArrowButton.DownPressed;
			}
			return VisualStyleElement.ScrollBar.ArrowButton.DownNormal;
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00051504 File Offset: 0x0004F704
		private static bool IsDisabled(ButtonState state)
		{
			return (state & ButtonState.Inactive) == ButtonState.Inactive;
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00051514 File Offset: 0x0004F714
		private static bool IsPressed(ButtonState state)
		{
			return (state & ButtonState.Pushed) == ButtonState.Pushed;
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00051524 File Offset: 0x0004F724
		protected override void ListViewDrawColumnHeaderBackground(ListView listView, ColumnHeader columnHeader, Graphics g, Rectangle area, Rectangle clippingArea)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.ListViewDrawColumnHeaderBackground(listView, columnHeader, g, area, clippingArea);
				return;
			}
			VisualStyleElement visualStyleElement;
			if (listView.HeaderStyle == ColumnHeaderStyle.Clickable)
			{
				if (columnHeader.Pressed)
				{
					visualStyleElement = VisualStyleElement.Header.Item.Pressed;
				}
				else if (columnHeader == listView.EnteredColumnHeader)
				{
					visualStyleElement = VisualStyleElement.Header.Item.Hot;
				}
				else
				{
					visualStyleElement = VisualStyleElement.Header.Item.Normal;
				}
			}
			else
			{
				visualStyleElement = VisualStyleElement.Header.Item.Normal;
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.ListViewDrawColumnHeaderBackground(listView, columnHeader, g, area, clippingArea);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(g, area, clippingArea);
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x000515A4 File Offset: 0x0004F7A4
		protected override void ListViewDrawUnusedHeaderBackground(ListView listView, Graphics g, Rectangle area, Rectangle clippingArea)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.ListViewDrawUnusedHeaderBackground(listView, g, area, clippingArea);
				return;
			}
			VisualStyleElement normal = VisualStyleElement.Header.Item.Normal;
			if (!VisualStyleRenderer.IsElementDefined(normal))
			{
				base.ListViewDrawUnusedHeaderBackground(listView, g, area, clippingArea);
				return;
			}
			new VisualStyleRenderer(normal).DrawBackground(g, area, clippingArea);
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x000515ED File Offset: 0x0004F7ED
		public override bool ListViewHasHotHeaderStyle
		{
			get
			{
				return (ThemeVisualStyles.RenderClientAreas && VisualStyleRenderer.IsElementDefined(VisualStyleElement.Header.Item.Hot)) || base.ListViewHasHotHeaderStyle;
			}
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0005160C File Offset: 0x0004F80C
		public override int ListViewGetHeaderHeight(ListView listView, Font font)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				return base.ListViewGetHeaderHeight(listView, font);
			}
			VisualStyleElement normal = VisualStyleElement.Header.Item.Normal;
			if (!VisualStyleRenderer.IsElementDefined(normal))
			{
				return base.ListViewGetHeaderHeight(listView, font);
			}
			Control control = null;
			Graphics graphics;
			if (listView == null)
			{
				control = new Control();
				graphics = control.CreateGraphics();
			}
			else
			{
				graphics = listView.CreateGraphics();
			}
			int height = new VisualStyleRenderer(normal).GetPartSize(graphics, ThemeSizeType.True).Height;
			graphics.Dispose();
			if (listView == null)
			{
				control.Dispose();
			}
			return height;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00051680 File Offset: 0x0004F880
		public override void DrawGroupBox(Graphics dc, Rectangle area, GroupBox box)
		{
			GroupBoxRenderer.DrawGroupBox(dc, new Rectangle(Point.Empty, box.Size), box.Text, box.Font, (box.ForeColor == Control.DefaultForeColor) ? Color.Empty : box.ForeColor, box.Enabled ? GroupBoxState.Normal : GroupBoxState.Disabled);
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x000516DA File Offset: 0x0004F8DA
		private Rectangle ManagedWindowGetTitleBarRectangle(InternalWindowManager wm)
		{
			return new Rectangle(0, 0, wm.Form.Width, this.ManagedWindowTitleBarHeight(wm) + this.ManagedWindowBorderWidth(wm) * (wm.IsMinimized ? 2 : 1));
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0005170C File Offset: 0x0004F90C
		private Region ManagedWindowGetWindowRegion(Form form)
		{
			if (form.WindowManager is MdiWindowManager && form.WindowManager.IsMaximized)
			{
				return null;
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.ManagedWindowGetTitleBarVisualStyleElement(form.WindowManager);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				return null;
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
			if (!visualStyleRenderer.IsBackgroundPartiallyTransparent())
			{
				return null;
			}
			IDeviceContext measurementDeviceContext = ThemeVisualStyles.GetMeasurementDeviceContext();
			Rectangle rectangle = this.ManagedWindowGetTitleBarRectangle(form.WindowManager);
			Region backgroundRegion = visualStyleRenderer.GetBackgroundRegion(measurementDeviceContext, rectangle);
			ThemeVisualStyles.ReleaseMeasurementDeviceContext(measurementDeviceContext);
			backgroundRegion.Union(new Rectangle(0, rectangle.Bottom, form.Width, form.Height));
			return backgroundRegion;
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0005179C File Offset: 0x0004F99C
		public override void ManagedWindowOnSizeInitializedOrChanged(Form form)
		{
			base.ManagedWindowOnSizeInitializedOrChanged(form);
			if (!ThemeVisualStyles.render_non_client_areas)
			{
				return;
			}
			form.Region = this.ManagedWindowGetWindowRegion(form);
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x000517BC File Offset: 0x0004F9BC
		protected override Rectangle ManagedWindowDrawTitleBarAndBorders(Graphics dc, Rectangle clip, InternalWindowManager wm)
		{
			if (!ThemeVisualStyles.render_non_client_areas)
			{
				return base.ManagedWindowDrawTitleBarAndBorders(dc, clip, wm);
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.ManagedWindowGetTitleBarVisualStyleElement(wm);
			VisualStyleElement visualStyleElement2;
			VisualStyleElement visualStyleElement3;
			VisualStyleElement visualStyleElement4;
			ThemeVisualStyles.ManagedWindowGetBorderVisualStyleElements(wm, out visualStyleElement2, out visualStyleElement3, out visualStyleElement4);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement) || (!wm.IsMinimized && (!VisualStyleRenderer.IsElementDefined(visualStyleElement2) || !VisualStyleRenderer.IsElementDefined(visualStyleElement3) || !VisualStyleRenderer.IsElementDefined(visualStyleElement4))))
			{
				return base.ManagedWindowDrawTitleBarAndBorders(dc, clip, wm);
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
			Rectangle rectangle = this.ManagedWindowGetTitleBarRectangle(wm);
			visualStyleRenderer.DrawBackground(dc, rectangle, clip);
			if (!wm.IsMinimized)
			{
				int num = this.ManagedWindowBorderWidth(wm);
				visualStyleRenderer.SetParameters(visualStyleElement2);
				visualStyleRenderer.DrawBackground(dc, new Rectangle(0, rectangle.Bottom, num, wm.Form.Height - rectangle.Bottom), clip);
				visualStyleRenderer.SetParameters(visualStyleElement3);
				visualStyleRenderer.DrawBackground(dc, new Rectangle(wm.Form.Width - num, rectangle.Bottom, num, wm.Form.Height - rectangle.Bottom), clip);
				visualStyleRenderer.SetParameters(visualStyleElement4);
				visualStyleRenderer.DrawBackground(dc, new Rectangle(0, wm.Form.Height - num, wm.Form.Width, num), clip);
			}
			return rectangle;
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x000518F6 File Offset: 0x0004FAF6
		private static FormWindowState ManagedWindowGetWindowState(InternalWindowManager wm)
		{
			return wm.GetWindowState();
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x000518FE File Offset: 0x0004FAFE
		private static bool ManagedWindowIsDisabled(InternalWindowManager wm)
		{
			return !wm.Form.Enabled;
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0005190E File Offset: 0x0004FB0E
		private static bool ManagedWindowIsActive(InternalWindowManager wm)
		{
			return wm.IsActive;
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00051918 File Offset: 0x0004FB18
		private static VisualStyleElement ManagedWindowGetTitleBarVisualStyleElement(InternalWindowManager wm)
		{
			if (wm.IsToolWindow)
			{
				FormWindowState formWindowState = ThemeVisualStyles.ManagedWindowGetWindowState(wm);
				if (formWindowState != FormWindowState.Minimized)
				{
					if (formWindowState != FormWindowState.Maximized)
					{
						if (ThemeVisualStyles.ManagedWindowIsDisabled(wm))
						{
							return VisualStyleElement.Window.SmallCaption.Disabled;
						}
						if (ThemeVisualStyles.ManagedWindowIsActive(wm))
						{
							return VisualStyleElement.Window.SmallCaption.Active;
						}
						return VisualStyleElement.Window.SmallCaption.Inactive;
					}
					else
					{
						if (ThemeVisualStyles.ManagedWindowIsDisabled(wm))
						{
							return VisualStyleElement.Window.SmallMaxCaption.Disabled;
						}
						if (ThemeVisualStyles.ManagedWindowIsActive(wm))
						{
							return VisualStyleElement.Window.SmallMaxCaption.Active;
						}
						return VisualStyleElement.Window.SmallMaxCaption.Inactive;
					}
				}
				else
				{
					if (ThemeVisualStyles.ManagedWindowIsDisabled(wm))
					{
						return VisualStyleElement.Window.SmallMinCaption.Disabled;
					}
					if (ThemeVisualStyles.ManagedWindowIsActive(wm))
					{
						return VisualStyleElement.Window.SmallMinCaption.Active;
					}
					return VisualStyleElement.Window.SmallMinCaption.Inactive;
				}
			}
			else
			{
				FormWindowState formWindowState = ThemeVisualStyles.ManagedWindowGetWindowState(wm);
				if (formWindowState != FormWindowState.Minimized)
				{
					if (formWindowState != FormWindowState.Maximized)
					{
						if (ThemeVisualStyles.ManagedWindowIsDisabled(wm))
						{
							return VisualStyleElement.Window.Caption.Disabled;
						}
						if (ThemeVisualStyles.ManagedWindowIsActive(wm))
						{
							return VisualStyleElement.Window.Caption.Active;
						}
						return VisualStyleElement.Window.Caption.Inactive;
					}
					else
					{
						if (ThemeVisualStyles.ManagedWindowIsDisabled(wm))
						{
							return VisualStyleElement.Window.MaxCaption.Disabled;
						}
						if (ThemeVisualStyles.ManagedWindowIsActive(wm))
						{
							return VisualStyleElement.Window.MaxCaption.Active;
						}
						return VisualStyleElement.Window.MaxCaption.Inactive;
					}
				}
				else
				{
					if (ThemeVisualStyles.ManagedWindowIsDisabled(wm))
					{
						return VisualStyleElement.Window.MinCaption.Disabled;
					}
					if (ThemeVisualStyles.ManagedWindowIsActive(wm))
					{
						return VisualStyleElement.Window.MinCaption.Active;
					}
					return VisualStyleElement.Window.MinCaption.Inactive;
				}
			}
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00051A1C File Offset: 0x0004FC1C
		private static void ManagedWindowGetBorderVisualStyleElements(InternalWindowManager wm, out VisualStyleElement left, out VisualStyleElement right, out VisualStyleElement bottom)
		{
			bool flag = !ThemeVisualStyles.ManagedWindowIsDisabled(wm) && ThemeVisualStyles.ManagedWindowIsActive(wm);
			if (wm.IsToolWindow)
			{
				if (flag)
				{
					left = VisualStyleElement.Window.SmallFrameLeft.Active;
					right = VisualStyleElement.Window.SmallFrameRight.Active;
					bottom = VisualStyleElement.Window.SmallFrameBottom.Active;
					return;
				}
				left = VisualStyleElement.Window.SmallFrameLeft.Inactive;
				right = VisualStyleElement.Window.SmallFrameRight.Inactive;
				bottom = VisualStyleElement.Window.SmallFrameBottom.Inactive;
				return;
			}
			else
			{
				if (flag)
				{
					left = VisualStyleElement.Window.FrameLeft.Active;
					right = VisualStyleElement.Window.FrameRight.Active;
					bottom = VisualStyleElement.Window.FrameBottom.Active;
					return;
				}
				left = VisualStyleElement.Window.FrameLeft.Inactive;
				right = VisualStyleElement.Window.FrameRight.Inactive;
				bottom = VisualStyleElement.Window.FrameBottom.Inactive;
				return;
			}
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00051AA0 File Offset: 0x0004FCA0
		public override bool ManagedWindowTitleButtonHasHotElementStyle(TitleButton button, Form form)
		{
			if (ThemeVisualStyles.render_non_client_areas && (button.State & ButtonState.Inactive) != ButtonState.Inactive)
			{
				VisualStyleElement visualStyleElement;
				if (ThemeVisualStyles.ManagedWindowIsMaximizedMdiChild(form))
				{
					switch (button.Caption)
					{
					case CaptionButton.Close:
						visualStyleElement = VisualStyleElement.Window.MdiCloseButton.Hot;
						goto IL_00C9;
					case CaptionButton.Minimize:
						visualStyleElement = VisualStyleElement.Window.MdiMinButton.Hot;
						goto IL_00C9;
					case CaptionButton.Help:
						visualStyleElement = VisualStyleElement.Window.MdiHelpButton.Hot;
						goto IL_00C9;
					}
					visualStyleElement = VisualStyleElement.Window.MdiRestoreButton.Hot;
				}
				else if (form.WindowManager.IsToolWindow)
				{
					visualStyleElement = VisualStyleElement.Window.SmallCloseButton.Hot;
				}
				else
				{
					switch (button.Caption)
					{
					case CaptionButton.Close:
						visualStyleElement = VisualStyleElement.Window.CloseButton.Hot;
						goto IL_00C9;
					case CaptionButton.Minimize:
						visualStyleElement = VisualStyleElement.Window.MinButton.Hot;
						goto IL_00C9;
					case CaptionButton.Maximize:
						visualStyleElement = VisualStyleElement.Window.MaxButton.Hot;
						goto IL_00C9;
					case CaptionButton.Help:
						visualStyleElement = VisualStyleElement.Window.HelpButton.Hot;
						goto IL_00C9;
					}
					visualStyleElement = VisualStyleElement.Window.RestoreButton.Hot;
				}
				IL_00C9:
				if (VisualStyleRenderer.IsElementDefined(visualStyleElement))
				{
					return true;
				}
			}
			return base.ManagedWindowTitleButtonHasHotElementStyle(button, form);
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00051B88 File Offset: 0x0004FD88
		private static bool ManagedWindowIsMaximizedMdiChild(Form form)
		{
			return form.WindowManager is MdiWindowManager && ThemeVisualStyles.ManagedWindowGetWindowState(form.WindowManager) == FormWindowState.Maximized;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00051BA7 File Offset: 0x0004FDA7
		private static bool ManagedWindowTitleButtonIsDisabled(TitleButton button, InternalWindowManager wm)
		{
			return (button.State & ButtonState.Inactive) == ButtonState.Inactive;
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00051BBC File Offset: 0x0004FDBC
		private static bool ManagedWindowTitleButtonIsPressed(TitleButton button)
		{
			return (button.State & ButtonState.Pushed) == ButtonState.Pushed;
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00051BD4 File Offset: 0x0004FDD4
		private static VisualStyleElement ManagedWindowGetTitleButtonVisualStyleElement(TitleButton button, Form form)
		{
			if (form.WindowManager.IsToolWindow)
			{
				if (ThemeVisualStyles.ManagedWindowTitleButtonIsDisabled(button, form.WindowManager))
				{
					return VisualStyleElement.Window.SmallCloseButton.Disabled;
				}
				if (ThemeVisualStyles.ManagedWindowTitleButtonIsPressed(button))
				{
					return VisualStyleElement.Window.SmallCloseButton.Pressed;
				}
				if (button.Entered)
				{
					return VisualStyleElement.Window.SmallCloseButton.Hot;
				}
				return VisualStyleElement.Window.SmallCloseButton.Normal;
			}
			else
			{
				switch (button.Caption)
				{
				case CaptionButton.Close:
					if (ThemeVisualStyles.ManagedWindowTitleButtonIsDisabled(button, form.WindowManager))
					{
						return VisualStyleElement.Window.CloseButton.Disabled;
					}
					if (ThemeVisualStyles.ManagedWindowTitleButtonIsPressed(button))
					{
						return VisualStyleElement.Window.CloseButton.Pressed;
					}
					if (button.Entered)
					{
						return VisualStyleElement.Window.CloseButton.Hot;
					}
					return VisualStyleElement.Window.CloseButton.Normal;
				case CaptionButton.Minimize:
					if (ThemeVisualStyles.ManagedWindowTitleButtonIsDisabled(button, form.WindowManager))
					{
						return VisualStyleElement.Window.MinButton.Disabled;
					}
					if (ThemeVisualStyles.ManagedWindowTitleButtonIsPressed(button))
					{
						return VisualStyleElement.Window.MinButton.Pressed;
					}
					if (button.Entered)
					{
						return VisualStyleElement.Window.MinButton.Hot;
					}
					return VisualStyleElement.Window.MinButton.Normal;
				case CaptionButton.Maximize:
					if (ThemeVisualStyles.ManagedWindowTitleButtonIsDisabled(button, form.WindowManager))
					{
						return VisualStyleElement.Window.MaxButton.Disabled;
					}
					if (ThemeVisualStyles.ManagedWindowTitleButtonIsPressed(button))
					{
						return VisualStyleElement.Window.MaxButton.Pressed;
					}
					if (button.Entered)
					{
						return VisualStyleElement.Window.MaxButton.Hot;
					}
					return VisualStyleElement.Window.MaxButton.Normal;
				case CaptionButton.Help:
					if (ThemeVisualStyles.ManagedWindowTitleButtonIsDisabled(button, form.WindowManager))
					{
						return VisualStyleElement.Window.HelpButton.Disabled;
					}
					if (ThemeVisualStyles.ManagedWindowTitleButtonIsPressed(button))
					{
						return VisualStyleElement.Window.HelpButton.Pressed;
					}
					if (button.Entered)
					{
						return VisualStyleElement.Window.HelpButton.Hot;
					}
					return VisualStyleElement.Window.HelpButton.Normal;
				}
				if (ThemeVisualStyles.ManagedWindowTitleButtonIsDisabled(button, form.WindowManager))
				{
					return VisualStyleElement.Window.RestoreButton.Disabled;
				}
				if (ThemeVisualStyles.ManagedWindowTitleButtonIsPressed(button))
				{
					return VisualStyleElement.Window.RestoreButton.Pressed;
				}
				if (button.Entered)
				{
					return VisualStyleElement.Window.RestoreButton.Hot;
				}
				return VisualStyleElement.Window.RestoreButton.Normal;
			}
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00051D58 File Offset: 0x0004FF58
		protected override void ManagedWindowDrawTitleButton(Graphics dc, TitleButton button, Rectangle clip, Form form)
		{
			if (!ThemeVisualStyles.render_non_client_areas)
			{
				base.ManagedWindowDrawTitleButton(dc, button, clip, form);
				return;
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.ManagedWindowGetTitleButtonVisualStyleElement(button, form);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.ManagedWindowDrawTitleButton(dc, button, clip, form);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, button.Rectangle, clip);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00051DA8 File Offset: 0x0004FFA8
		public override Size ManagedWindowButtonSize(InternalWindowManager wm)
		{
			if (!ThemeVisualStyles.render_non_client_areas)
			{
				return base.ManagedWindowButtonSize(wm);
			}
			VisualStyleElement visualStyleElement = ((wm.IsToolWindow && !wm.IsMinimized) ? VisualStyleElement.Window.SmallCloseButton.Normal : VisualStyleElement.Window.CloseButton.Normal);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				return base.ManagedWindowButtonSize(wm);
			}
			IDeviceContext measurementDeviceContext = ThemeVisualStyles.GetMeasurementDeviceContext();
			Size partSize = new VisualStyleRenderer(visualStyleElement).GetPartSize(measurementDeviceContext, ThemeSizeType.True);
			ThemeVisualStyles.ReleaseMeasurementDeviceContext(measurementDeviceContext);
			return partSize;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00051E0C File Offset: 0x0005000C
		public override void ManagedWindowDrawMenuButton(Graphics dc, TitleButton button, Rectangle clip, InternalWindowManager wm)
		{
			if (!ThemeVisualStyles.render_non_client_areas)
			{
				base.ManagedWindowDrawMenuButton(dc, button, clip, wm);
				return;
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.ManagedWindowGetMenuButtonVisualStyleElement(button, wm);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.ManagedWindowDrawMenuButton(dc, button, clip, wm);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, button.Rectangle, clip);
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00051E5C File Offset: 0x0005005C
		private static VisualStyleElement ManagedWindowGetMenuButtonVisualStyleElement(TitleButton button, InternalWindowManager wm)
		{
			switch (button.Caption)
			{
			case CaptionButton.Close:
				if (ThemeVisualStyles.ManagedWindowTitleButtonIsDisabled(button, wm))
				{
					return VisualStyleElement.Window.MdiCloseButton.Disabled;
				}
				if (ThemeVisualStyles.ManagedWindowTitleButtonIsPressed(button))
				{
					return VisualStyleElement.Window.MdiCloseButton.Pressed;
				}
				if (button.Entered)
				{
					return VisualStyleElement.Window.MdiCloseButton.Hot;
				}
				return VisualStyleElement.Window.MdiCloseButton.Normal;
			case CaptionButton.Minimize:
				if (ThemeVisualStyles.ManagedWindowTitleButtonIsDisabled(button, wm))
				{
					return VisualStyleElement.Window.MdiMinButton.Disabled;
				}
				if (ThemeVisualStyles.ManagedWindowTitleButtonIsPressed(button))
				{
					return VisualStyleElement.Window.MdiMinButton.Pressed;
				}
				if (button.Entered)
				{
					return VisualStyleElement.Window.MdiMinButton.Hot;
				}
				return VisualStyleElement.Window.MdiMinButton.Normal;
			case CaptionButton.Help:
				if (ThemeVisualStyles.ManagedWindowTitleButtonIsDisabled(button, wm))
				{
					return VisualStyleElement.Window.MdiHelpButton.Disabled;
				}
				if (ThemeVisualStyles.ManagedWindowTitleButtonIsPressed(button))
				{
					return VisualStyleElement.Window.MdiHelpButton.Pressed;
				}
				if (button.Entered)
				{
					return VisualStyleElement.Window.MdiHelpButton.Hot;
				}
				return VisualStyleElement.Window.MdiHelpButton.Normal;
			}
			if (ThemeVisualStyles.ManagedWindowTitleButtonIsDisabled(button, wm))
			{
				return VisualStyleElement.Window.MdiRestoreButton.Disabled;
			}
			if (ThemeVisualStyles.ManagedWindowTitleButtonIsPressed(button))
			{
				return VisualStyleElement.Window.MdiRestoreButton.Pressed;
			}
			if (button.Entered)
			{
				return VisualStyleElement.Window.MdiRestoreButton.Hot;
			}
			return VisualStyleElement.Window.MdiRestoreButton.Normal;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00051F54 File Offset: 0x00050154
		public override void DrawScrollBar(Graphics dc, Rectangle clip, ScrollBar bar)
		{
			if (!ThemeVisualStyles.RenderClientAreas || !ThemeVisualStyles.ScrollBarAreElementsDefined)
			{
				base.DrawScrollBar(dc, clip, bar);
				return;
			}
			int scrollbutton_width = bar.scrollbutton_width;
			int scrollbutton_height = bar.scrollbutton_height;
			if (bar.vert)
			{
				bar.FirstArrowArea = new Rectangle(0, 0, bar.Width, scrollbutton_height);
				bar.SecondArrowArea = new Rectangle(0, bar.ClientRectangle.Height - scrollbutton_height, bar.Width, scrollbutton_height);
				Rectangle thumbPos = bar.ThumbPos;
				thumbPos.Width = bar.Width;
				bar.ThumbPos = thumbPos;
				VisualStyleElement visualStyleElement;
				if (bar.thumb_moving == ScrollBar.ThumbMoving.Backwards)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.LowerTrackVertical.Pressed;
				}
				else
				{
					visualStyleElement = (bar.Enabled ? VisualStyleElement.ScrollBar.LowerTrackVertical.Normal : VisualStyleElement.ScrollBar.LowerTrackVertical.Disabled);
				}
				VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
				Rectangle rectangle = new Rectangle(0, 0, bar.ClientRectangle.Width, bar.ThumbPos.Top);
				if (clip.IntersectsWith(rectangle))
				{
					visualStyleRenderer.DrawBackground(dc, rectangle, clip);
				}
				if (bar.thumb_moving == ScrollBar.ThumbMoving.Forward)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.LowerTrackVertical.Pressed;
				}
				else
				{
					visualStyleElement = (bar.Enabled ? VisualStyleElement.ScrollBar.LowerTrackVertical.Normal : VisualStyleElement.ScrollBar.LowerTrackVertical.Disabled);
				}
				visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
				Rectangle rectangle2 = new Rectangle(0, bar.ThumbPos.Bottom, bar.ClientRectangle.Width, bar.ClientRectangle.Height - bar.ThumbPos.Bottom);
				if (clip.IntersectsWith(rectangle2))
				{
					visualStyleRenderer.DrawBackground(dc, rectangle2, clip);
				}
				if (clip.IntersectsWith(bar.FirstArrowArea))
				{
					if (!bar.Enabled)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.UpDisabled;
					}
					else if (bar.firstbutton_state == ButtonState.Pushed)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.UpPressed;
					}
					else if (bar.FirstButtonEntered)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.UpHot;
					}
					else if (ThemeVisualStyles.ScrollBarHasHoverArrowButtonStyleVisualStyles && bar.Entered)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.UpHover;
					}
					else
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.UpNormal;
					}
					visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
					visualStyleRenderer.DrawBackground(dc, bar.FirstArrowArea);
				}
				if (clip.IntersectsWith(bar.SecondArrowArea))
				{
					if (!bar.Enabled)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.DownDisabled;
					}
					else if (bar.secondbutton_state == ButtonState.Pushed)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.DownPressed;
					}
					else if (bar.SecondButtonEntered)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.DownHot;
					}
					else if (ThemeVisualStyles.ScrollBarHasHoverArrowButtonStyleVisualStyles && bar.Entered)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.DownHover;
					}
					else
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.DownNormal;
					}
					visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
					visualStyleRenderer.DrawBackground(dc, bar.SecondArrowArea);
				}
				if (!bar.Enabled)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.LowerTrackVertical.Disabled;
				}
				else if (bar.ThumbPressed)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.ThumbButtonVertical.Pressed;
				}
				else if (bar.ThumbEntered)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.ThumbButtonVertical.Hot;
				}
				else
				{
					visualStyleElement = VisualStyleElement.ScrollBar.ThumbButtonVertical.Normal;
				}
				visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
				visualStyleRenderer.DrawBackground(dc, bar.ThumbPos, clip);
				if (bar.Enabled && bar.ThumbPos.Height >= 20)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.GripperVertical.Normal;
					if (VisualStyleRenderer.IsElementDefined(visualStyleElement))
					{
						visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
						visualStyleRenderer.DrawBackground(dc, bar.ThumbPos, clip);
						return;
					}
				}
			}
			else
			{
				bar.FirstArrowArea = new Rectangle(0, 0, scrollbutton_width, bar.Height);
				bar.SecondArrowArea = new Rectangle(bar.ClientRectangle.Width - scrollbutton_width, 0, scrollbutton_width, bar.Height);
				Rectangle thumbPos2 = bar.ThumbPos;
				thumbPos2.Height = bar.Height;
				bar.ThumbPos = thumbPos2;
				VisualStyleElement visualStyleElement;
				if (bar.thumb_moving == ScrollBar.ThumbMoving.Backwards)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.LeftTrackHorizontal.Pressed;
				}
				else
				{
					visualStyleElement = (bar.Enabled ? VisualStyleElement.ScrollBar.LeftTrackHorizontal.Normal : VisualStyleElement.ScrollBar.LeftTrackHorizontal.Disabled);
				}
				VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
				Rectangle rectangle3 = new Rectangle(0, 0, bar.ThumbPos.Left, bar.ClientRectangle.Height);
				if (clip.IntersectsWith(rectangle3))
				{
					visualStyleRenderer.DrawBackground(dc, rectangle3, clip);
				}
				if (bar.thumb_moving == ScrollBar.ThumbMoving.Forward)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.RightTrackHorizontal.Pressed;
				}
				else
				{
					visualStyleElement = (bar.Enabled ? VisualStyleElement.ScrollBar.RightTrackHorizontal.Normal : VisualStyleElement.ScrollBar.RightTrackHorizontal.Disabled);
				}
				visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
				Rectangle rectangle4 = new Rectangle(bar.ThumbPos.Right, 0, bar.ClientRectangle.Width - bar.ThumbPos.Right, bar.ClientRectangle.Height);
				if (clip.IntersectsWith(rectangle4))
				{
					visualStyleRenderer.DrawBackground(dc, rectangle4, clip);
				}
				if (clip.IntersectsWith(bar.FirstArrowArea))
				{
					if (!bar.Enabled)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.LeftDisabled;
					}
					else if (bar.firstbutton_state == ButtonState.Pushed)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.LeftPressed;
					}
					else if (bar.FirstButtonEntered)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.LeftHot;
					}
					else if (ThemeVisualStyles.ScrollBarHasHoverArrowButtonStyleVisualStyles && bar.Entered)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.LeftHover;
					}
					else
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.LeftNormal;
					}
					visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
					visualStyleRenderer.DrawBackground(dc, bar.FirstArrowArea);
				}
				if (clip.IntersectsWith(bar.SecondArrowArea))
				{
					if (!bar.Enabled)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.RightDisabled;
					}
					else if (bar.secondbutton_state == ButtonState.Pushed)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.RightPressed;
					}
					else if (bar.SecondButtonEntered)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.RightHot;
					}
					else if (ThemeVisualStyles.ScrollBarHasHoverArrowButtonStyleVisualStyles && bar.Entered)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.RightHover;
					}
					else
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.RightNormal;
					}
					visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
					visualStyleRenderer.DrawBackground(dc, bar.SecondArrowArea);
				}
				if (!bar.Enabled)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.RightTrackHorizontal.Disabled;
				}
				else if (bar.ThumbPressed)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Pressed;
				}
				else if (bar.ThumbEntered)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Hot;
				}
				else
				{
					visualStyleElement = VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Normal;
				}
				visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
				visualStyleRenderer.DrawBackground(dc, bar.ThumbPos, clip);
				if (bar.Enabled && bar.ThumbPos.Height >= 20)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.GripperHorizontal.Normal;
					if (VisualStyleRenderer.IsElementDefined(visualStyleElement))
					{
						visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
						visualStyleRenderer.DrawBackground(dc, bar.ThumbPos, clip);
					}
				}
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x0005250C File Offset: 0x0005070C
		public override bool ScrollBarHasHotElementStyles
		{
			get
			{
				if (!ThemeVisualStyles.RenderClientAreas)
				{
					return base.ScrollBarHasHotElementStyles;
				}
				return ThemeVisualStyles.ScrollBarAreElementsDefined;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x00052521 File Offset: 0x00050721
		public override bool ScrollBarHasPressedThumbStyle
		{
			get
			{
				if (!ThemeVisualStyles.RenderClientAreas)
				{
					return base.ScrollBarHasPressedThumbStyle;
				}
				return ThemeVisualStyles.ScrollBarAreElementsDefined;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x00052536 File Offset: 0x00050736
		public override bool ScrollBarHasHoverArrowButtonStyle
		{
			get
			{
				if (ThemeVisualStyles.RenderClientAreas && ThemeVisualStyles.ScrollBarHasHoverArrowButtonStyleVisualStyles)
				{
					return ThemeVisualStyles.ScrollBarAreElementsDefined;
				}
				return base.ScrollBarHasHoverArrowButtonStyle;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x00052554 File Offset: 0x00050754
		private static bool ScrollBarAreElementsDefined
		{
			get
			{
				return VisualStyleRenderer.IsElementDefined(VisualStyleElement.ScrollBar.ArrowButton.DownDisabled) && VisualStyleRenderer.IsElementDefined(VisualStyleElement.ScrollBar.LeftTrackHorizontal.Disabled) && VisualStyleRenderer.IsElementDefined(VisualStyleElement.ScrollBar.LowerTrackVertical.Disabled) && VisualStyleRenderer.IsElementDefined(VisualStyleElement.ScrollBar.RightTrackHorizontal.Disabled) && VisualStyleRenderer.IsElementDefined(VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Disabled) && VisualStyleRenderer.IsElementDefined(VisualStyleElement.ScrollBar.ThumbButtonVertical.Disabled) && VisualStyleRenderer.IsElementDefined(VisualStyleElement.ScrollBar.UpperTrackVertical.Disabled);
			}
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x000525B5 File Offset: 0x000507B5
		private static bool TextBoxBaseShouldPaint(TextBoxBase textBoxBase)
		{
			return textBoxBase.BorderStyle == BorderStyle.Fixed3D;
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x000525C0 File Offset: 0x000507C0
		private static VisualStyleElement TextBoxBaseGetVisualStyleElement(TextBoxBase textBoxBase)
		{
			if (!textBoxBase.Enabled)
			{
				return VisualStyleElement.TextBox.TextEdit.Disabled;
			}
			if (textBoxBase.ReadOnly)
			{
				return VisualStyleElement.TextBox.TextEdit.ReadOnly;
			}
			if (textBoxBase.Entered)
			{
				return VisualStyleElement.TextBox.TextEdit.Hot;
			}
			if (textBoxBase.Focused)
			{
				return VisualStyleElement.TextBox.TextEdit.Focused;
			}
			return VisualStyleElement.TextBox.TextEdit.Normal;
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00052600 File Offset: 0x00050800
		public override void TextBoxBaseFillBackground(TextBoxBase textBoxBase, Graphics g, Rectangle clippingArea)
		{
			if (!ThemeVisualStyles.RenderClientAreas || !ThemeVisualStyles.TextBoxBaseShouldPaint(textBoxBase))
			{
				base.TextBoxBaseFillBackground(textBoxBase, g, clippingArea);
				return;
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.TextBoxBaseGetVisualStyleElement(textBoxBase);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.TextBoxBaseFillBackground(textBoxBase, g, clippingArea);
				return;
			}
			Rectangle rectangle = new Rectangle(Point.Empty, textBoxBase.Size);
			rectangle.X -= (rectangle.Width - textBoxBase.ClientSize.Width) / 2;
			rectangle.Y -= (rectangle.Height - textBoxBase.ClientSize.Height) / 2;
			new VisualStyleRenderer(visualStyleElement).DrawBackground(g, rectangle, clippingArea);
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x000526AC File Offset: 0x000508AC
		public override bool TextBoxBaseHandleWmNcPaint(TextBoxBase textBoxBase, ref Message m)
		{
			if (!ThemeVisualStyles.RenderClientAreas || !ThemeVisualStyles.TextBoxBaseShouldPaint(textBoxBase))
			{
				return base.TextBoxBaseHandleWmNcPaint(textBoxBase, ref m);
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.TextBoxBaseGetVisualStyleElement(textBoxBase);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				return base.TextBoxBaseHandleWmNcPaint(textBoxBase, ref m);
			}
			PaintEventArgs paintEventArgs = XplatUI.PaintEventStart(ref m, textBoxBase.Handle, false);
			new VisualStyleRenderer(visualStyleElement).DrawBackgroundExcludingArea(paintEventArgs.Graphics, new Rectangle(Point.Empty, textBoxBase.Size), new Rectangle(new Point((textBoxBase.Width - textBoxBase.ClientSize.Width) / 2, (textBoxBase.Height - textBoxBase.ClientSize.Height) / 2), textBoxBase.ClientSize));
			XplatUI.PaintEventEnd(ref m, textBoxBase.Handle, false);
			return true;
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00052764 File Offset: 0x00050964
		public override bool TextBoxBaseShouldPaintBackground(TextBoxBase textBoxBase)
		{
			if (!ThemeVisualStyles.RenderClientAreas || !ThemeVisualStyles.TextBoxBaseShouldPaint(textBoxBase))
			{
				return base.TextBoxBaseShouldPaintBackground(textBoxBase);
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.TextBoxBaseGetVisualStyleElement(textBoxBase);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				return base.TextBoxBaseShouldPaintBackground(textBoxBase);
			}
			return new VisualStyleRenderer(visualStyleElement).IsBackgroundPartiallyTransparent();
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x000527AA File Offset: 0x000509AA
		private static bool ToolBarIsDisabled(ToolBarItem item)
		{
			return !item.Button.Enabled;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x000527BA File Offset: 0x000509BA
		private static bool ToolBarIsPressed(ToolBarItem item)
		{
			return item.Pressed;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x000527C2 File Offset: 0x000509C2
		private static bool ToolBarIsChecked(ToolBarItem item)
		{
			return item.Button.Pushed;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x000527CF File Offset: 0x000509CF
		private static bool ToolBarIsHot(ToolBarItem item)
		{
			return item.Hilight;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x000527D8 File Offset: 0x000509D8
		protected override void DrawToolBarButtonBorder(Graphics dc, ToolBarItem item, bool is_flat)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.DrawToolBarButtonBorder(dc, item, is_flat);
				return;
			}
			if (item.Button.Style == ToolBarButtonStyle.Separator)
			{
				return;
			}
			VisualStyleElement visualStyleElement;
			if (item.Button.Style == ToolBarButtonStyle.DropDownButton)
			{
				visualStyleElement = ThemeVisualStyles.ToolBarGetDropDownButtonVisualStyleElement(item);
			}
			else
			{
				visualStyleElement = ThemeVisualStyles.ToolBarGetButtonVisualStyleElement(item);
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.DrawToolBarButtonBorder(dc, item, is_flat);
				return;
			}
			Rectangle rectangle = item.Rectangle;
			if (item.Button.Style == ToolBarButtonStyle.DropDownButton && item.Button.Parent.DropDownArrows)
			{
				rectangle.Width -= this.ToolBarDropDownWidth;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, rectangle);
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x00052880 File Offset: 0x00050A80
		private static VisualStyleElement ToolBarGetDropDownButtonVisualStyleElement(ToolBarItem item)
		{
			if (item.Button.Parent.DropDownArrows)
			{
				if (ThemeVisualStyles.ToolBarIsDisabled(item))
				{
					return VisualStyleElement.ToolBar.SplitButton.Disabled;
				}
				if (ThemeVisualStyles.ToolBarIsPressed(item))
				{
					return VisualStyleElement.ToolBar.SplitButton.Pressed;
				}
				if (ThemeVisualStyles.ToolBarIsChecked(item))
				{
					if (ThemeVisualStyles.ToolBarIsHot(item))
					{
						return VisualStyleElement.ToolBar.SplitButton.HotChecked;
					}
					return VisualStyleElement.ToolBar.SplitButton.Checked;
				}
				else
				{
					if (ThemeVisualStyles.ToolBarIsHot(item))
					{
						return VisualStyleElement.ToolBar.SplitButton.Hot;
					}
					return VisualStyleElement.ToolBar.SplitButton.Normal;
				}
			}
			else
			{
				if (ThemeVisualStyles.ToolBarIsDisabled(item))
				{
					return VisualStyleElement.ToolBar.DropDownButton.Disabled;
				}
				if (ThemeVisualStyles.ToolBarIsPressed(item))
				{
					return VisualStyleElement.ToolBar.DropDownButton.Pressed;
				}
				if (ThemeVisualStyles.ToolBarIsChecked(item))
				{
					if (ThemeVisualStyles.ToolBarIsHot(item))
					{
						return VisualStyleElement.ToolBar.DropDownButton.HotChecked;
					}
					return VisualStyleElement.ToolBar.DropDownButton.Checked;
				}
				else
				{
					if (ThemeVisualStyles.ToolBarIsHot(item))
					{
						return VisualStyleElement.ToolBar.DropDownButton.Hot;
					}
					return VisualStyleElement.ToolBar.DropDownButton.Normal;
				}
			}
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x00052938 File Offset: 0x00050B38
		private static VisualStyleElement ToolBarGetButtonVisualStyleElement(ToolBarItem item)
		{
			if (ThemeVisualStyles.ToolBarIsDisabled(item))
			{
				return VisualStyleElement.ToolBar.Button.Disabled;
			}
			if (ThemeVisualStyles.ToolBarIsPressed(item))
			{
				return VisualStyleElement.ToolBar.Button.Pressed;
			}
			if (ThemeVisualStyles.ToolBarIsChecked(item))
			{
				if (ThemeVisualStyles.ToolBarIsHot(item))
				{
					return VisualStyleElement.ToolBar.Button.HotChecked;
				}
				return VisualStyleElement.ToolBar.Button.Checked;
			}
			else
			{
				if (ThemeVisualStyles.ToolBarIsHot(item))
				{
					return VisualStyleElement.ToolBar.Button.Hot;
				}
				return VisualStyleElement.ToolBar.Button.Normal;
			}
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00052990 File Offset: 0x00050B90
		protected override void DrawToolBarSeparator(Graphics dc, ToolBarItem item)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.DrawToolBarSeparator(dc, item);
				return;
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.ToolBarGetSeparatorVisualStyleElement(item);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.DrawToolBarSeparator(dc, item);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, item.Rectangle);
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x000529D7 File Offset: 0x00050BD7
		private static VisualStyleElement ToolBarGetSeparatorVisualStyleElement(ToolBarItem toolBarItem)
		{
			if (!toolBarItem.Button.Parent.Vertical)
			{
				return VisualStyleElement.ToolBar.SeparatorHorizontal.Normal;
			}
			return VisualStyleElement.ToolBar.SeparatorVertical.Normal;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x000529F6 File Offset: 0x00050BF6
		protected override void DrawToolBarToggleButtonBackground(Graphics dc, ToolBarItem item)
		{
			if (!ThemeVisualStyles.RenderClientAreas || !VisualStyleRenderer.IsElementDefined(ThemeVisualStyles.ToolBarGetButtonVisualStyleElement(item)))
			{
				base.DrawToolBarToggleButtonBackground(dc, item);
			}
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00052A14 File Offset: 0x00050C14
		protected override void DrawToolBarDropDownArrow(Graphics dc, ToolBarItem item, bool is_flat)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.DrawToolBarDropDownArrow(dc, item, is_flat);
				return;
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.ToolBarGetDropDownArrowVisualStyleElement(item);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.DrawToolBarDropDownArrow(dc, item, is_flat);
				return;
			}
			Rectangle rectangle = item.Rectangle;
			rectangle.X = item.Rectangle.Right - this.ToolBarDropDownWidth;
			rectangle.Width = this.ToolBarDropDownWidth;
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, rectangle);
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00052A88 File Offset: 0x00050C88
		private static VisualStyleElement ToolBarGetDropDownArrowVisualStyleElement(ToolBarItem item)
		{
			if (ThemeVisualStyles.ToolBarIsDisabled(item))
			{
				return VisualStyleElement.ToolBar.SplitButtonDropDown.Disabled;
			}
			if (ThemeVisualStyles.ToolBarIsPressed(item))
			{
				return VisualStyleElement.ToolBar.SplitButtonDropDown.Pressed;
			}
			if (ThemeVisualStyles.ToolBarIsChecked(item))
			{
				if (ThemeVisualStyles.ToolBarIsHot(item))
				{
					return VisualStyleElement.ToolBar.SplitButtonDropDown.HotChecked;
				}
				return VisualStyleElement.ToolBar.SplitButtonDropDown.Checked;
			}
			else
			{
				if (ThemeVisualStyles.ToolBarIsHot(item))
				{
					return VisualStyleElement.ToolBar.SplitButtonDropDown.Hot;
				}
				return VisualStyleElement.ToolBar.SplitButtonDropDown.Normal;
			}
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00052AE0 File Offset: 0x00050CE0
		public override bool ToolBarHasHotElementStyles(ToolBar toolBar)
		{
			return ThemeVisualStyles.RenderClientAreas || base.ToolBarHasHotElementStyles(toolBar);
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x00052AF2 File Offset: 0x00050CF2
		public override bool ToolBarHasHotCheckedElementStyles
		{
			get
			{
				return ThemeVisualStyles.RenderClientAreas || base.ToolBarHasHotCheckedElementStyles;
			}
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00052B04 File Offset: 0x00050D04
		protected override void ToolTipDrawBackground(Graphics dc, Rectangle clip_rectangle, ToolTip.ToolTipWindow control)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.ToolTipDrawBackground(dc, clip_rectangle, control);
				return;
			}
			VisualStyleElement normal = VisualStyleElement.ToolTip.Standard.Normal;
			if (!VisualStyleRenderer.IsElementDefined(normal))
			{
				base.ToolTipDrawBackground(dc, clip_rectangle, control);
				return;
			}
			new VisualStyleRenderer(normal).DrawBackground(dc, control.ClientRectangle);
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x00052B4C File Offset: 0x00050D4C
		public override bool ToolTipTransparentBackground
		{
			get
			{
				if (!ThemeVisualStyles.RenderClientAreas)
				{
					return base.ToolTipTransparentBackground;
				}
				VisualStyleElement normal = VisualStyleElement.ToolTip.Standard.Normal;
				if (!VisualStyleRenderer.IsElementDefined(normal))
				{
					return base.ToolTipTransparentBackground;
				}
				return new VisualStyleRenderer(normal).IsBackgroundPartiallyTransparent();
			}
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00052B88 File Offset: 0x00050D88
		[MonoInternalNote("Use the sizing information provided by the VisualStyles API.")]
		public override void TreeViewDrawNodePlusMinus(TreeView treeView, TreeNode node, Graphics dc, int x, int middle)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.TreeViewDrawNodePlusMinus(treeView, node, dc, x, middle);
				return;
			}
			VisualStyleElement visualStyleElement = (node.IsExpanded ? VisualStyleElement.TreeView.Glyph.Opened : VisualStyleElement.TreeView.Glyph.Closed);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.TreeViewDrawNodePlusMinus(treeView, node, dc, x, middle);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, new Rectangle(x, middle - 4, 9, 9));
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x00052BF0 File Offset: 0x00050DF0
		private static IDeviceContext GetMeasurementDeviceContext()
		{
			if (ThemeVisualStyles.control == null)
			{
				ThemeVisualStyles.control = new Control();
			}
			return ThemeVisualStyles.control.CreateGraphics();
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00052C0D File Offset: 0x00050E0D
		private static void ReleaseMeasurementDeviceContext(IDeviceContext dc)
		{
			dc.Dispose();
		}

		// Token: 0x04000B2C RID: 2860
		private static bool render_client_areas;

		// Token: 0x04000B2D RID: 2861
		private static bool render_non_client_areas;

		// Token: 0x04000B2E RID: 2862
		private static bool ScrollBarHasHoverArrowButtonStyleVisualStyles = Environment.OSVersion.Version.Major >= 6;

		// Token: 0x04000B2F RID: 2863
		private static Control control;
	}
}
