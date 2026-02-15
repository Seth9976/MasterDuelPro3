using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.VisualStyles
{
	// Token: 0x020002D0 RID: 720
	internal class GtkPlus
	{
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x00084BAC File Offset: 0x00082DAC
		public static GtkPlus Instance
		{
			get
			{
				return GtkPlus.instance;
			}
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x00084BB4 File Offset: 0x00082DB4
		public static bool Initialize()
		{
			bool flag;
			try
			{
				if (GtkPlus.gtk_check_version(2U, 10U, 0U) != IntPtr.Zero)
				{
					flag = false;
				}
				else
				{
					int num = 0;
					string[] array = new string[1];
					bool flag2 = GtkPlus.gtk_init_check(ref num, ref array);
					if (flag2)
					{
						GtkPlus.instance = new GtkPlus();
					}
					flag = flag2;
				}
			}
			catch (DllNotFoundException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x00084C14 File Offset: 0x00082E14
		protected GtkPlus()
		{
			this.widgets = new IntPtr[this.WidgetTypeCount];
			this.styles = new IntPtr[this.WidgetTypeCount];
			this.window = GtkPlus.gtk_window_new(GtkPlus.GtkWindowType.GTK_WINDOW_TOPLEVEL);
			this.@fixed = GtkPlus.gtk_fixed_new();
			GtkPlus.gtk_container_add(this.window, this.@fixed);
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[0] = GtkPlus.gtk_button_new());
			GtkPlus.GTK_WIDGET_SET_FLAGS(this.widgets[0], GtkPlus.GtkWidgetFlags.GTK_CAN_DEFAULT);
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[1] = GtkPlus.gtk_check_button_new());
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[2] = GtkPlus.gtk_combo_box_entry_new());
			GtkPlus.gtk_widget_realize(this.widgets[2]);
			this.combo_box_drop_down_toggle_button = GtkPlus.GetFirstChildWidgetOfType.Get(this.widgets[2], GtkPlus.gtk_toggle_button_get_type());
			GtkPlus.gtk_widget_realize(this.combo_box_drop_down_toggle_button);
			this.combo_box_drop_down_arrow = GtkPlus.GetFirstChildWidgetOfType.Get(this.combo_box_drop_down_toggle_button, GtkPlus.gtk_arrow_get_type());
			GtkPlus.g_object_ref(this.combo_box_drop_down_toggle_button_style = GtkPlus.GetWidgetStyle(this.combo_box_drop_down_toggle_button));
			GtkPlus.g_object_ref(this.combo_box_drop_down_arrow_style = GtkPlus.GetWidgetStyle(this.combo_box_drop_down_arrow));
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[3] = GtkPlus.gtk_frame_new(null));
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[4] = GtkPlus.gtk_progress_bar_new());
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[5] = GtkPlus.gtk_radio_button_new(IntPtr.Zero));
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[6] = GtkPlus.gtk_hscrollbar_new(IntPtr.Zero));
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[7] = GtkPlus.gtk_vscrollbar_new(IntPtr.Zero));
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[8] = GtkPlus.gtk_statusbar_new());
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[9] = GtkPlus.gtk_notebook_new());
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[10] = GtkPlus.gtk_entry_new());
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[11] = GtkPlus.gtk_toolbar_new());
			IntPtr intPtr = GtkPlus.gtk_tool_button_new(IntPtr.Zero, null);
			GtkPlus.gtk_toolbar_insert(this.widgets[11], intPtr, -1);
			this.tool_bar_button = GtkPlus.gtk_bin_get_child(intPtr);
			GtkPlus.g_object_ref(this.tool_bar_button_style = GtkPlus.GetWidgetStyle(this.tool_bar_button));
			IntPtr intPtr2 = GtkPlus.gtk_toggle_tool_button_new();
			GtkPlus.gtk_toolbar_insert(this.widgets[11], intPtr2, -1);
			this.tool_bar_toggle_button = GtkPlus.gtk_bin_get_child(intPtr2);
			GtkPlus.g_object_ref(this.tool_bar_toggle_button_style = GtkPlus.GetWidgetStyle(this.tool_bar_toggle_button));
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[12] = GtkPlus.gtk_hscale_new_with_range(0.0, 1.0, 1.0));
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[13] = GtkPlus.gtk_vscale_new_with_range(0.0, 1.0, 1.0));
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[14] = GtkPlus.gtk_tree_view_new());
			this.tree_view_column = GtkPlus.gtk_tree_view_column_new();
			GtkPlus.gtk_tree_view_insert_column(this.widgets[14], this.tree_view_column, -1);
			GtkPlus.GtkTreeViewColumn gtkTreeViewColumn = (GtkPlus.GtkTreeViewColumn)Marshal.PtrToStructure(this.tree_view_column, typeof(GtkPlus.GtkTreeViewColumn));
			this.tree_view_column_button = gtkTreeViewColumn.button;
			GtkPlus.g_object_ref(this.tree_view_column_button_style = GtkPlus.GetWidgetStyle(this.tree_view_column_button));
			IntPtr intPtr3 = GtkPlus.gtk_adjustment_new(0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[15] = GtkPlus.gtk_spin_button_new(intPtr3, 0.0, 0U));
			for (int i = 0; i < this.WidgetTypeCount; i++)
			{
				GtkPlus.g_object_ref(this.styles[i] = GtkPlus.GetWidgetStyle(this.widgets[i]));
			}
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x00085198 File Offset: 0x00083398
		protected override void Finalize()
		{
			try
			{
				GtkPlus.gtk_object_destroy(this.window);
				for (int i = 0; i < this.WidgetTypeCount; i++)
				{
					GtkPlus.g_object_unref(this.styles[i]);
				}
				GtkPlus.g_object_unref(this.combo_box_drop_down_toggle_button_style);
				GtkPlus.g_object_unref(this.combo_box_drop_down_arrow_style);
				GtkPlus.g_object_unref(this.tool_bar_button_style);
				GtkPlus.g_object_unref(this.tool_bar_toggle_button_style);
				GtkPlus.g_object_unref(this.tree_view_column_button_style);
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x00085220 File Offset: 0x00083420
		public void ButtonPaint(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, bool @default, GtkPlusState state)
		{
			this.button_painter.Configure(@default, state);
			this.Paint(GtkPlus.WidgetType.Button, bounds, dc, clippingArea, this.button_painter);
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x00085241 File Offset: 0x00083441
		public void CheckBoxPaint(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, GtkPlusState state, GtkPlusToggleButtonValue value)
		{
			this.check_box_painter.Configure(state, value);
			this.Paint(GtkPlus.WidgetType.CheckBox, bounds, dc, clippingArea, this.check_box_painter);
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x00085262 File Offset: 0x00083462
		private Size GetGtkCheckButtonIndicatorSize(GtkPlus.WidgetType widgetType)
		{
			int widgetStyleInteger = GtkPlus.GetWidgetStyleInteger(this.widgets[(int)widgetType], "indicator-size");
			return new Size(widgetStyleInteger, widgetStyleInteger);
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x0008527C File Offset: 0x0008347C
		public Size CheckBoxGetSize()
		{
			return this.GetGtkCheckButtonIndicatorSize(GtkPlus.WidgetType.CheckBox);
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x00085285 File Offset: 0x00083485
		public void ComboBoxPaintDropDownButton(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, GtkPlusState state)
		{
			this.combo_box_drop_down_button_painter.Configure(state);
			this.Paint(GtkPlus.WidgetType.ComboBox, bounds, dc, clippingArea, this.combo_box_drop_down_button_painter);
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x000852A4 File Offset: 0x000834A4
		public void ComboBoxPaintBorder(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea)
		{
			this.Paint(GtkPlus.WidgetType.ComboBox, bounds, dc, clippingArea, this.combo_box_border_painter);
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x000852B6 File Offset: 0x000834B6
		public void GroupBoxPaint(IDeviceContext dc, Rectangle bounds, Rectangle excludedArea, GtkPlusState state)
		{
			this.group_box_painter.Configure(state);
			this.PaintExcludingArea(GtkPlus.WidgetType.GroupBox, bounds, dc, excludedArea, this.group_box_painter);
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x000852D5 File Offset: 0x000834D5
		public void HeaderPaint(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, GtkPlusState state)
		{
			this.header_painter.Configure(state);
			this.Paint(GtkPlus.WidgetType.TreeView, bounds, dc, clippingArea, this.header_painter);
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x000852F5 File Offset: 0x000834F5
		public void ProgressBarPaintBar(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea)
		{
			this.Paint(GtkPlus.WidgetType.ProgressBar, bounds, dc, clippingArea, this.progress_bar_bar_painter);
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x00085307 File Offset: 0x00083507
		public void ProgressBarPaintChunk(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea)
		{
			this.Paint(GtkPlus.WidgetType.ProgressBar, bounds, dc, clippingArea, this.progress_bar_chunk_painter);
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x00085319 File Offset: 0x00083519
		public void RadioButtonPaint(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, GtkPlusState state, GtkPlusToggleButtonValue value)
		{
			this.radio_button_painter.Configure(state, value);
			this.Paint(GtkPlus.WidgetType.RadioButton, bounds, dc, clippingArea, this.radio_button_painter);
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x0008533A File Offset: 0x0008353A
		public Size RadioButtonGetSize()
		{
			return this.GetGtkCheckButtonIndicatorSize(GtkPlus.WidgetType.RadioButton);
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x00085343 File Offset: 0x00083543
		public void ScrollBarPaintArrowButton(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, GtkPlusState state, bool horizontal, bool upOrLeft)
		{
			this.scroll_bar_arrow_button_painter.Configure(state, horizontal, upOrLeft);
			this.Paint(horizontal ? GtkPlus.WidgetType.HScrollBar : GtkPlus.WidgetType.VScrollBar, bounds, dc, clippingArea, this.scroll_bar_arrow_button_painter);
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x0008536D File Offset: 0x0008356D
		public void ScrollBarPaintThumbButton(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, GtkPlusState state, bool horizontal)
		{
			this.scroll_bar_thumb_button_painter.Configure(state, horizontal);
			this.Paint(horizontal ? GtkPlus.WidgetType.HScrollBar : GtkPlus.WidgetType.VScrollBar, bounds, dc, clippingArea, this.scroll_bar_thumb_button_painter);
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x00085395 File Offset: 0x00083595
		public void ScrollBarPaintTrack(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, GtkPlusState state, bool horizontal, bool upOrLeft)
		{
			this.scroll_bar_track_painter.Configure(state, upOrLeft);
			this.Paint(horizontal ? GtkPlus.WidgetType.HScrollBar : GtkPlus.WidgetType.VScrollBar, bounds, dc, clippingArea, this.scroll_bar_track_painter);
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x000853BD File Offset: 0x000835BD
		public void StatusBarPaintGripper(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea)
		{
			this.Paint(GtkPlus.WidgetType.StatusBar, bounds, dc, clippingArea, this.status_bar_gripper_painter);
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x000853CF File Offset: 0x000835CF
		public void TabControlPaintPane(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea)
		{
			this.Paint(GtkPlus.WidgetType.TabControl, bounds, dc, clippingArea, this.tab_control_pane_painter);
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x000853E2 File Offset: 0x000835E2
		public void TabControlPaintTabItem(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, GtkPlusState state)
		{
			this.tab_control_tab_item_painter.Configure(state);
			this.Paint(GtkPlus.WidgetType.TabControl, bounds, dc, clippingArea, this.tab_control_tab_item_painter);
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x00085402 File Offset: 0x00083602
		public void TextBoxPaint(IDeviceContext dc, Rectangle bounds, Rectangle excludedArea, GtkPlusState state)
		{
			this.text_box_painter.Configure(state);
			this.PaintExcludingArea(GtkPlus.WidgetType.TextBox, bounds, dc, excludedArea, this.text_box_painter);
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x00085422 File Offset: 0x00083622
		public void ToolBarPaint(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea)
		{
			this.Paint(GtkPlus.WidgetType.ToolBar, bounds, dc, clippingArea, this.tool_bar_painter);
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x00085435 File Offset: 0x00083635
		public void ToolBarPaintButton(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, GtkPlusState state)
		{
			this.tool_bar_button_painter.Configure(state);
			this.Paint(GtkPlus.WidgetType.Button, bounds, dc, clippingArea, this.tool_bar_button_painter);
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x00085454 File Offset: 0x00083654
		public void ToolBarPaintCheckedButton(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea)
		{
			this.Paint(GtkPlus.WidgetType.Button, bounds, dc, clippingArea, this.tool_bar_checked_button_painter);
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x00085466 File Offset: 0x00083666
		public void TrackBarPaintTrack(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, bool horizontal)
		{
			this.Paint(horizontal ? GtkPlus.WidgetType.HorizontalTrackBar : GtkPlus.WidgetType.VerticalTrackBar, bounds, dc, clippingArea, this.track_bar_track_painter);
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x00085481 File Offset: 0x00083681
		public void TrackBarPaintThumb(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, GtkPlusState state, bool horizontal)
		{
			this.track_bar_thumb_painter.Configure(state, horizontal);
			this.Paint(horizontal ? GtkPlus.WidgetType.HorizontalTrackBar : GtkPlus.WidgetType.VerticalTrackBar, bounds, dc, clippingArea, this.track_bar_thumb_painter);
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x000854AB File Offset: 0x000836AB
		public void TreeViewPaintGlyph(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, bool closed)
		{
			this.tree_view_glyph_painter.Configure(closed);
			this.Paint(GtkPlus.WidgetType.TreeView, bounds, dc, clippingArea, this.tree_view_glyph_painter);
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x000854CB File Offset: 0x000836CB
		public void UpDownPaint(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, bool up, GtkPlusState state)
		{
			this.up_down_painter.Configure(up, state);
			this.Paint(GtkPlus.WidgetType.UpDown, bounds, dc, clippingArea, this.up_down_painter);
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x000854F0 File Offset: 0x000836F0
		private void Paint(GtkPlus.WidgetType widgetType, Rectangle bounds, IDeviceContext dc, Rectangle clippingArea, GtkPlus.Painter painter)
		{
			this.Paint(widgetType, bounds, dc, GtkPlus.TransparencyType.Alpha, Color.Black, GtkPlus.DeviceContextType.Native, clippingArea, painter, Rectangle.Empty);
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x00085518 File Offset: 0x00083718
		private void PaintExcludingArea(GtkPlus.WidgetType widgetType, Rectangle bounds, IDeviceContext dc, Rectangle excludedArea, GtkPlus.Painter painter)
		{
			this.Paint(widgetType, bounds, dc, GtkPlus.TransparencyType.Alpha, Color.Black, GtkPlus.DeviceContextType.Native, bounds, painter, excludedArea);
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x0008553C File Offset: 0x0008373C
		private unsafe void Paint(GtkPlus.WidgetType widgetType, Rectangle bounds, IDeviceContext dc, GtkPlus.TransparencyType transparencyType, Color background, GtkPlus.DeviceContextType deviceContextType, Rectangle clippingArea, GtkPlus.Painter painter, Rectangle excludedArea)
		{
			Rectangle rectangle = Rectangle.Intersect(bounds, clippingArea);
			if (rectangle.Width == 0 || rectangle.Height == 0)
			{
				return;
			}
			rectangle.Offset(-bounds.X, -bounds.Y);
			excludedArea.Offset(-bounds.X, -bounds.Y);
			IntPtr intPtr = GtkPlus.gdk_pixmap_new(IntPtr.Zero, bounds.Width, bounds.Height, 24);
			painter.AttachStyle(widgetType, intPtr, this);
			IntPtr intPtr2 = GtkPlus.gdk_gc_new(intPtr);
			GtkPlus.GdkColor gdkColor = new GtkPlus.GdkColor(background);
			GtkPlus.gdk_gc_set_rgb_fg_color(intPtr2, ref gdkColor);
			IntPtr intPtr3;
			IntPtr intPtr4;
			int num;
			this.Paint(intPtr, intPtr2, bounds, widgetType, out intPtr3, out intPtr4, out num, rectangle, painter, excludedArea);
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			int num2 = 0;
			GtkPlus.GdkColor gdkColor2 = default(GtkPlus.GdkColor);
			if (transparencyType == GtkPlus.TransparencyType.Alpha)
			{
				gdkColor2.red = ushort.MaxValue;
				gdkColor2.green = ushort.MaxValue;
				gdkColor2.blue = ushort.MaxValue;
				GtkPlus.gdk_gc_set_rgb_fg_color(intPtr2, ref gdkColor2);
				this.Paint(intPtr, intPtr2, bounds, widgetType, out zero, out zero2, out num2, rectangle, painter, excludedArea);
			}
			GtkPlus.g_object_unref(intPtr2);
			byte* ptr = (byte*)(void*)intPtr4;
			byte* ptr2 = (byte*)(void*)zero2;
			for (int i = 0; i < rectangle.Height; i++)
			{
				byte* ptr3 = ptr;
				byte* ptr4 = ptr2;
				for (int j = 0; j < rectangle.Width; j++)
				{
					if (transparencyType != GtkPlus.TransparencyType.Color)
					{
						if (transparencyType == GtkPlus.TransparencyType.Alpha)
						{
							ptr3[3] = *ptr3 - *ptr4 + byte.MaxValue;
						}
					}
					else if (*ptr3 == background.R && ptr3[1] == background.G && ptr3[2] == background.B)
					{
						ptr3[3] = 0;
					}
					byte b = *ptr3;
					*ptr3 = ptr3[2];
					ptr3[2] = b;
					ptr3 += 4;
					ptr4 += 4;
				}
				ptr += num;
				ptr2 += num2;
			}
			if (transparencyType == GtkPlus.TransparencyType.Alpha)
			{
				GtkPlus.g_object_unref(zero);
			}
			GtkPlus.g_object_unref(intPtr);
			Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height, num, PixelFormat.Format32bppPArgb, intPtr4);
			bool flag = false;
			Graphics graphics;
			if (deviceContextType != GtkPlus.DeviceContextType.Graphics)
			{
				if (deviceContextType != GtkPlus.DeviceContextType.Native)
				{
					graphics = dc as Graphics;
					if (graphics == null)
					{
						flag = true;
						graphics = Graphics.FromHdc(dc.GetHdc());
					}
					else
					{
						flag = false;
					}
				}
				else
				{
					graphics = Graphics.FromHdc(dc.GetHdc());
				}
			}
			else
			{
				graphics = (Graphics)dc;
			}
			rectangle.Offset(bounds.X, bounds.Y);
			graphics.DrawImage(bitmap, rectangle.Location);
			if (deviceContextType != GtkPlus.DeviceContextType.Graphics)
			{
				if (deviceContextType == GtkPlus.DeviceContextType.Native)
				{
					graphics.Dispose();
					dc.ReleaseHdc();
				}
				else if (flag)
				{
					graphics.Dispose();
					dc.ReleaseHdc();
				}
			}
			bitmap.Dispose();
			GtkPlus.g_object_unref(intPtr3);
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x000857EC File Offset: 0x000839EC
		private void Paint(IntPtr drawable, IntPtr gc, Rectangle rectangle, GtkPlus.WidgetType widgetType, out IntPtr pixbuf, out IntPtr pixelData, out int rowstride, Rectangle clippingArea, GtkPlus.Painter painter, Rectangle excludedArea)
		{
			GtkPlus.gdk_draw_rectangle(drawable, gc, true, clippingArea.X, clippingArea.Y, clippingArea.Width, clippingArea.Height);
			painter.Paint(this.styles[(int)widgetType], drawable, new GtkPlus.GdkRectangle(clippingArea), this.widgets[(int)widgetType], 0, 0, rectangle.Width, rectangle.Height, this);
			if (excludedArea.Width != 0)
			{
				GtkPlus.gdk_draw_rectangle(drawable, gc, true, excludedArea.X, excludedArea.Y, excludedArea.Width, excludedArea.Height);
			}
			if ((pixbuf = GtkPlus.gdk_pixbuf_new(GtkPlus.GdkColorspace.GDK_COLORSPACE_RGB, true, 8, clippingArea.Width, clippingArea.Height)) == IntPtr.Zero || GtkPlus.gdk_pixbuf_get_from_drawable(pixbuf, drawable, IntPtr.Zero, clippingArea.X, clippingArea.Y, 0, 0, clippingArea.Width, clippingArea.Height) == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			pixelData = GtkPlus.gdk_pixbuf_get_pixels(pixbuf);
			rowstride = GtkPlus.gdk_pixbuf_get_rowstride(pixbuf);
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x000858F8 File Offset: 0x00083AF8
		private static GtkPlus.GtkShadowType GetWidgetStyleShadowType(IntPtr widget)
		{
			GtkPlus.GtkShadowType gtkShadowType;
			GtkPlus.gtk_widget_style_get(widget, "shadow-type", out gtkShadowType, IntPtr.Zero);
			return gtkShadowType;
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x00085918 File Offset: 0x00083B18
		private static int GetWidgetStyleInteger(IntPtr widget, string propertyName)
		{
			int num;
			GtkPlus.gtk_widget_style_get(widget, propertyName, out num, IntPtr.Zero);
			return num;
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x00085934 File Offset: 0x00083B34
		private static float GetWidgetStyleSingle(IntPtr widget, string propertyName)
		{
			float num;
			GtkPlus.gtk_widget_style_get(widget, propertyName, out num, IntPtr.Zero);
			return num;
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x00085950 File Offset: 0x00083B50
		private static bool GetWidgetStyleBoolean(IntPtr widget, string propertyName)
		{
			bool flag;
			GtkPlus.gtk_widget_style_get(widget, propertyName, out flag, IntPtr.Zero);
			return flag;
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x0008596C File Offset: 0x00083B6C
		private static IntPtr GetWidgetStyle(IntPtr widget)
		{
			return GtkPlus.gtk_rc_get_style(widget);
		}

		// Token: 0x06001B34 RID: 6964
		[DllImport("libgdk-x11-2.0.so")]
		private static extern void gdk_draw_rectangle(IntPtr drawable, IntPtr gc, bool filled, int x, int y, int width, int height);

		// Token: 0x06001B35 RID: 6965
		[DllImport("libgdk-x11-2.0.so")]
		private static extern IntPtr gdk_gc_new(IntPtr drawable);

		// Token: 0x06001B36 RID: 6966
		[DllImport("libgdk-x11-2.0.so")]
		private static extern void gdk_gc_set_rgb_fg_color(IntPtr gc, ref GtkPlus.GdkColor color);

		// Token: 0x06001B37 RID: 6967
		[DllImport("libgdk-x11-2.0.so")]
		private static extern IntPtr gdk_pixbuf_get_from_drawable(IntPtr dest, IntPtr src, IntPtr cmap, int src_x, int src_y, int dest_x, int dest_y, int width, int height);

		// Token: 0x06001B38 RID: 6968
		[DllImport("libgdk-x11-2.0.so")]
		private static extern IntPtr gdk_pixmap_new(IntPtr drawable, int width, int height, int depth);

		// Token: 0x06001B39 RID: 6969
		[DllImport("libgdk_pixbuf-2.0.so")]
		private static extern IntPtr gdk_pixbuf_get_pixels(IntPtr pixbuf);

		// Token: 0x06001B3A RID: 6970
		[DllImport("libgdk_pixbuf-2.0.so")]
		private static extern int gdk_pixbuf_get_rowstride(IntPtr pixbuf);

		// Token: 0x06001B3B RID: 6971
		[DllImport("libgdk_pixbuf-2.0.so")]
		private static extern IntPtr gdk_pixbuf_new(GtkPlus.GdkColorspace colorspace, bool has_alpha, int bits_per_sample, int width, int height);

		// Token: 0x06001B3C RID: 6972
		[DllImport("libgtk-x11-2.0.so")]
		private static extern bool gtk_init_check(ref int argc, ref string[] argv);

		// Token: 0x06001B3D RID: 6973
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_check_version(uint required_major, uint required_minor, uint required_micro);

		// Token: 0x06001B3E RID: 6974
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_container_add(IntPtr container, IntPtr widget);

		// Token: 0x06001B3F RID: 6975
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_container_forall(IntPtr container, GtkPlus.GtkCallback callback, IntPtr callback_data);

		// Token: 0x06001B40 RID: 6976
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_object_destroy(IntPtr @object);

		// Token: 0x06001B41 RID: 6977
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_rc_get_style(IntPtr widget);

		// Token: 0x06001B42 RID: 6978
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_style_attach(IntPtr style, IntPtr window);

		// Token: 0x06001B43 RID: 6979
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_widget_realize(IntPtr widget);

		// Token: 0x06001B44 RID: 6980
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_widget_style_get(IntPtr widget, string property, out int value, IntPtr nullTerminator);

		// Token: 0x06001B45 RID: 6981
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_widget_style_get(IntPtr widget, string property, out float value, IntPtr nullTerminator);

		// Token: 0x06001B46 RID: 6982
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_widget_style_get(IntPtr widget, string property1, out int value1, string property2, out int value2, IntPtr nullTerminator);

		// Token: 0x06001B47 RID: 6983
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_widget_style_get(IntPtr widget, string property, out GtkPlus.GtkShadowType value, IntPtr nullTerminator);

		// Token: 0x06001B48 RID: 6984
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_widget_style_get(IntPtr widget, string property, out bool value, IntPtr nullTerminator);

		// Token: 0x06001B49 RID: 6985
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_window_new(GtkPlus.GtkWindowType type);

		// Token: 0x06001B4A RID: 6986
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_window_set_default(IntPtr window, IntPtr default_widget);

		// Token: 0x06001B4B RID: 6987
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_adjustment_new(double value, double lower, double upper, double step_increment, double page_increment, double page_size);

		// Token: 0x06001B4C RID: 6988
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_tree_view_column_new();

		// Token: 0x06001B4D RID: 6989
		[DllImport("libgtk-x11-2.0.so")]
		private static extern int gtk_tree_view_insert_column(IntPtr tree_view, IntPtr column, int position);

		// Token: 0x06001B4E RID: 6990
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_toolbar_insert(IntPtr toolbar, IntPtr item, int pos);

		// Token: 0x06001B4F RID: 6991
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_bin_get_child(IntPtr bin);

		// Token: 0x06001B50 RID: 6992
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_arrow_get_type();

		// Token: 0x06001B51 RID: 6993
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_container_get_type();

		// Token: 0x06001B52 RID: 6994
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_toggle_button_get_type();

		// Token: 0x06001B53 RID: 6995
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_button_new();

		// Token: 0x06001B54 RID: 6996
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_check_button_new();

		// Token: 0x06001B55 RID: 6997
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_combo_box_entry_new();

		// Token: 0x06001B56 RID: 6998
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_entry_new();

		// Token: 0x06001B57 RID: 6999
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_fixed_new();

		// Token: 0x06001B58 RID: 7000
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_frame_new(string label);

		// Token: 0x06001B59 RID: 7001
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_hscale_new_with_range(double min, double max, double step);

		// Token: 0x06001B5A RID: 7002
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_hscrollbar_new(IntPtr adjustment);

		// Token: 0x06001B5B RID: 7003
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_notebook_new();

		// Token: 0x06001B5C RID: 7004
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_progress_bar_new();

		// Token: 0x06001B5D RID: 7005
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_radio_button_new(IntPtr group);

		// Token: 0x06001B5E RID: 7006
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_spin_button_new(IntPtr adjustment, double climb_rate, uint digits);

		// Token: 0x06001B5F RID: 7007
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_statusbar_new();

		// Token: 0x06001B60 RID: 7008
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_toggle_tool_button_new();

		// Token: 0x06001B61 RID: 7009
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_toolbar_new();

		// Token: 0x06001B62 RID: 7010
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_tool_button_new(IntPtr icon_widget, string label);

		// Token: 0x06001B63 RID: 7011
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_tree_view_new();

		// Token: 0x06001B64 RID: 7012
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_vscale_new_with_range(double min, double max, double step);

		// Token: 0x06001B65 RID: 7013
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_vscrollbar_new(IntPtr adjustment);

		// Token: 0x06001B66 RID: 7014
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_paint_arrow(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, GtkPlus.GtkShadowType shadow_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, GtkPlus.GtkArrowType arrow_type, bool fill, int x, int y, int width, int height);

		// Token: 0x06001B67 RID: 7015
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_paint_box(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, GtkPlus.GtkShadowType shadow_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, int x, int y, int width, int height);

		// Token: 0x06001B68 RID: 7016
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_paint_box_gap(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, GtkPlus.GtkShadowType shadow_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, int x, int y, int width, int height, GtkPlus.GtkPositionType gap_side, int gap_x, int gap_width);

		// Token: 0x06001B69 RID: 7017
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_paint_check(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, GtkPlus.GtkShadowType shadow_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, int x, int y, int width, int height);

		// Token: 0x06001B6A RID: 7018
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_paint_expander(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, int x, int y, GtkPlus.GtkExpanderStyle expander_style);

		// Token: 0x06001B6B RID: 7019
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_paint_extension(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, GtkPlus.GtkShadowType shadow_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, int x, int y, int width, int height, GtkPlus.GtkPositionType gap_side);

		// Token: 0x06001B6C RID: 7020
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_paint_flat_box(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, GtkPlus.GtkShadowType shadow_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, int x, int y, int width, int height);

		// Token: 0x06001B6D RID: 7021
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_paint_option(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, GtkPlus.GtkShadowType shadow_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, int x, int y, int width, int height);

		// Token: 0x06001B6E RID: 7022
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_paint_resize_grip(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, GtkPlus.GdkWindowEdge edge, int x, int y, int width, int height);

		// Token: 0x06001B6F RID: 7023
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_paint_shadow(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, GtkPlus.GtkShadowType shadow_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, int x, int y, int width, int height);

		// Token: 0x06001B70 RID: 7024
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_paint_slider(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, GtkPlus.GtkShadowType shadow_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, int x, int y, int width, int height, GtkPlus.GtkOrientation orientation);

		// Token: 0x06001B71 RID: 7025 RVA: 0x00085974 File Offset: 0x00083B74
		private static void GTK_WIDGET_SET_FLAGS(IntPtr wid, GtkPlus.GtkWidgetFlags flag)
		{
			GtkPlus.GtkObject gtkObject = (GtkPlus.GtkObject)Marshal.PtrToStructure(wid, typeof(GtkPlus.GtkObject));
			gtkObject.flags |= (uint)flag;
			Marshal.StructureToPtr<GtkPlus.GtkObject>(gtkObject, wid, false);
		}

		// Token: 0x06001B72 RID: 7026
		[DllImport("libgobject-2.0.so")]
		private static extern IntPtr g_object_ref(IntPtr @object);

		// Token: 0x06001B73 RID: 7027
		[DllImport("libgobject-2.0.so")]
		private static extern void g_object_unref(IntPtr @object);

		// Token: 0x06001B74 RID: 7028
		[DllImport("libgobject-2.0.so")]
		private static extern bool g_type_check_instance_is_a(IntPtr type_instance, IntPtr iface_type);

		// Token: 0x06001B75 RID: 7029
		[DllImport("libgobject-2.0.so")]
		private static extern void g_object_get(IntPtr @object, string property_name, out bool value, IntPtr nullTerminator);

		// Token: 0x04001681 RID: 5761
		private static GtkPlus instance;

		// Token: 0x04001682 RID: 5762
		private readonly int WidgetTypeCount = Enum.GetNames(typeof(GtkPlus.WidgetType)).Length;

		// Token: 0x04001683 RID: 5763
		private readonly IntPtr[] widgets;

		// Token: 0x04001684 RID: 5764
		private readonly IntPtr window;

		// Token: 0x04001685 RID: 5765
		private readonly IntPtr @fixed;

		// Token: 0x04001686 RID: 5766
		private readonly IntPtr[] styles;

		// Token: 0x04001687 RID: 5767
		private readonly IntPtr combo_box_drop_down_toggle_button;

		// Token: 0x04001688 RID: 5768
		private readonly IntPtr combo_box_drop_down_arrow;

		// Token: 0x04001689 RID: 5769
		private IntPtr combo_box_drop_down_toggle_button_style;

		// Token: 0x0400168A RID: 5770
		private IntPtr combo_box_drop_down_arrow_style;

		// Token: 0x0400168B RID: 5771
		private readonly IntPtr tool_bar_button;

		// Token: 0x0400168C RID: 5772
		private readonly IntPtr tool_bar_toggle_button;

		// Token: 0x0400168D RID: 5773
		private IntPtr tool_bar_button_style;

		// Token: 0x0400168E RID: 5774
		private IntPtr tool_bar_toggle_button_style;

		// Token: 0x0400168F RID: 5775
		private readonly IntPtr tree_view_column;

		// Token: 0x04001690 RID: 5776
		private readonly IntPtr tree_view_column_button;

		// Token: 0x04001691 RID: 5777
		private IntPtr tree_view_column_button_style;

		// Token: 0x04001692 RID: 5778
		private readonly GtkPlus.ButtonPainter button_painter = new GtkPlus.ButtonPainter();

		// Token: 0x04001693 RID: 5779
		private readonly GtkPlus.CheckBoxPainter check_box_painter = new GtkPlus.CheckBoxPainter();

		// Token: 0x04001694 RID: 5780
		private readonly GtkPlus.RadioButtonPainter radio_button_painter = new GtkPlus.RadioButtonPainter();

		// Token: 0x04001695 RID: 5781
		private readonly GtkPlus.ComboBoxDropDownButtonPainter combo_box_drop_down_button_painter = new GtkPlus.ComboBoxDropDownButtonPainter();

		// Token: 0x04001696 RID: 5782
		private readonly GtkPlus.ComboBoxBorderPainter combo_box_border_painter = new GtkPlus.ComboBoxBorderPainter();

		// Token: 0x04001697 RID: 5783
		private readonly GtkPlus.GroupBoxPainter group_box_painter = new GtkPlus.GroupBoxPainter();

		// Token: 0x04001698 RID: 5784
		private readonly GtkPlus.HeaderPainter header_painter = new GtkPlus.HeaderPainter();

		// Token: 0x04001699 RID: 5785
		private readonly GtkPlus.ProgressBarBarPainter progress_bar_bar_painter = new GtkPlus.ProgressBarBarPainter();

		// Token: 0x0400169A RID: 5786
		private readonly GtkPlus.ProgressBarChunkPainter progress_bar_chunk_painter = new GtkPlus.ProgressBarChunkPainter();

		// Token: 0x0400169B RID: 5787
		private readonly GtkPlus.ScrollBarArrowButtonPainter scroll_bar_arrow_button_painter = new GtkPlus.ScrollBarArrowButtonPainter();

		// Token: 0x0400169C RID: 5788
		private readonly GtkPlus.ScrollBarThumbButtonPainter scroll_bar_thumb_button_painter = new GtkPlus.ScrollBarThumbButtonPainter();

		// Token: 0x0400169D RID: 5789
		private readonly GtkPlus.ScrollBarTrackPainter scroll_bar_track_painter = new GtkPlus.ScrollBarTrackPainter();

		// Token: 0x0400169E RID: 5790
		private readonly GtkPlus.StatusBarGripperPainter status_bar_gripper_painter = new GtkPlus.StatusBarGripperPainter();

		// Token: 0x0400169F RID: 5791
		private readonly GtkPlus.TabControlPanePainter tab_control_pane_painter = new GtkPlus.TabControlPanePainter();

		// Token: 0x040016A0 RID: 5792
		private readonly GtkPlus.TabControlTabItemPainter tab_control_tab_item_painter = new GtkPlus.TabControlTabItemPainter();

		// Token: 0x040016A1 RID: 5793
		private readonly GtkPlus.TextBoxPainter text_box_painter = new GtkPlus.TextBoxPainter();

		// Token: 0x040016A2 RID: 5794
		private readonly GtkPlus.ToolBarPainter tool_bar_painter = new GtkPlus.ToolBarPainter();

		// Token: 0x040016A3 RID: 5795
		private readonly GtkPlus.ToolBarButtonPainter tool_bar_button_painter = new GtkPlus.ToolBarButtonPainter();

		// Token: 0x040016A4 RID: 5796
		private readonly GtkPlus.ToolBarCheckedButtonPainter tool_bar_checked_button_painter = new GtkPlus.ToolBarCheckedButtonPainter();

		// Token: 0x040016A5 RID: 5797
		private readonly GtkPlus.TrackBarTrackPainter track_bar_track_painter = new GtkPlus.TrackBarTrackPainter();

		// Token: 0x040016A6 RID: 5798
		private readonly GtkPlus.TrackBarThumbPainter track_bar_thumb_painter = new GtkPlus.TrackBarThumbPainter();

		// Token: 0x040016A7 RID: 5799
		private readonly GtkPlus.TreeViewGlyphPainter tree_view_glyph_painter = new GtkPlus.TreeViewGlyphPainter();

		// Token: 0x040016A8 RID: 5800
		private readonly GtkPlus.UpDownPainter up_down_painter = new GtkPlus.UpDownPainter();

		// Token: 0x020002D1 RID: 721
		private abstract class Painter
		{
			// Token: 0x06001B76 RID: 7030 RVA: 0x000859AB File Offset: 0x00083BAB
			public virtual void AttachStyle(GtkPlus.WidgetType widgetType, IntPtr drawable, GtkPlus gtkPlus)
			{
				gtkPlus.styles[(int)widgetType] = GtkPlus.gtk_style_attach(gtkPlus.styles[(int)widgetType], drawable);
			}

			// Token: 0x06001B77 RID: 7031
			public abstract void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus);
		}

		// Token: 0x020002D2 RID: 722
		private enum TransparencyType
		{
			// Token: 0x040016AA RID: 5802
			None,
			// Token: 0x040016AB RID: 5803
			Color,
			// Token: 0x040016AC RID: 5804
			Alpha
		}

		// Token: 0x020002D3 RID: 723
		private enum DeviceContextType
		{
			// Token: 0x040016AE RID: 5806
			Unknown,
			// Token: 0x040016AF RID: 5807
			Graphics,
			// Token: 0x040016B0 RID: 5808
			Native
		}

		// Token: 0x020002D4 RID: 724
		private class ButtonPainter : GtkPlus.Painter
		{
			// Token: 0x06001B79 RID: 7033 RVA: 0x000859C3 File Offset: 0x00083BC3
			public void Configure(bool @default, GtkPlusState state)
			{
				this.@default = @default;
				this.state = state;
			}

			// Token: 0x06001B7A RID: 7034 RVA: 0x000859D4 File Offset: 0x00083BD4
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				if (this.@default)
				{
					GtkPlus.gtk_window_set_default(gtkPlus.window, widget);
					GtkPlus.gtk_paint_box(style, window, GtkPlus.GtkStateType.GTK_STATE_NORMAL, GtkPlus.GtkShadowType.GTK_SHADOW_IN, ref area, widget, "buttondefault", x, y, width, height);
					GtkPlus.gtk_window_set_default(gtkPlus.window, IntPtr.Zero);
					return;
				}
				GtkPlus.gtk_paint_box(style, window, (GtkPlus.GtkStateType)this.state, (this.state == GtkPlusState.Pressed) ? GtkPlus.GtkShadowType.GTK_SHADOW_IN : GtkPlus.GtkShadowType.GTK_SHADOW_OUT, ref area, widget, "button", x, y, width, height);
			}

			// Token: 0x040016B1 RID: 5809
			private bool @default;

			// Token: 0x040016B2 RID: 5810
			private GtkPlusState state;
		}

		// Token: 0x020002D5 RID: 725
		private abstract class ToggleButtonPainter : GtkPlus.Painter
		{
			// Token: 0x06001B7C RID: 7036 RVA: 0x00085A56 File Offset: 0x00083C56
			public void Configure(GtkPlusState state, GtkPlusToggleButtonValue value)
			{
				this.state = state;
				this.value = value;
			}

			// Token: 0x06001B7D RID: 7037 RVA: 0x00085A68 File Offset: 0x00083C68
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				this.PaintFunction(style, window, (GtkPlus.GtkStateType)this.state, (GtkPlus.GtkShadowType)this.value, ref area, widget, this.Detail, x, y, width, height);
			}

			// Token: 0x17000662 RID: 1634
			// (get) Token: 0x06001B7E RID: 7038
			protected abstract string Detail { get; }

			// Token: 0x17000663 RID: 1635
			// (get) Token: 0x06001B7F RID: 7039
			protected abstract GtkPlus.ToggleButtonPaintFunction PaintFunction { get; }

			// Token: 0x040016B3 RID: 5811
			private GtkPlusState state;

			// Token: 0x040016B4 RID: 5812
			private GtkPlusToggleButtonValue value;
		}

		// Token: 0x020002D6 RID: 726
		// (Invoke) Token: 0x06001B82 RID: 7042
		private delegate void ToggleButtonPaintFunction(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, GtkPlus.GtkShadowType shadow_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, int x, int y, int width, int height);

		// Token: 0x020002D7 RID: 727
		private class CheckBoxPainter : GtkPlus.ToggleButtonPainter
		{
			// Token: 0x17000664 RID: 1636
			// (get) Token: 0x06001B83 RID: 7043 RVA: 0x00085AA0 File Offset: 0x00083CA0
			protected override string Detail
			{
				get
				{
					return "checkbutton";
				}
			}

			// Token: 0x17000665 RID: 1637
			// (get) Token: 0x06001B84 RID: 7044 RVA: 0x00085AA7 File Offset: 0x00083CA7
			protected override GtkPlus.ToggleButtonPaintFunction PaintFunction
			{
				get
				{
					return new GtkPlus.ToggleButtonPaintFunction(GtkPlus.gtk_paint_check);
				}
			}
		}

		// Token: 0x020002D8 RID: 728
		private class RadioButtonPainter : GtkPlus.ToggleButtonPainter
		{
			// Token: 0x17000666 RID: 1638
			// (get) Token: 0x06001B86 RID: 7046 RVA: 0x00085ABD File Offset: 0x00083CBD
			protected override string Detail
			{
				get
				{
					return "radiobutton";
				}
			}

			// Token: 0x17000667 RID: 1639
			// (get) Token: 0x06001B87 RID: 7047 RVA: 0x00085AC4 File Offset: 0x00083CC4
			protected override GtkPlus.ToggleButtonPaintFunction PaintFunction
			{
				get
				{
					return new GtkPlus.ToggleButtonPaintFunction(GtkPlus.gtk_paint_option);
				}
			}
		}

		// Token: 0x020002D9 RID: 729
		private class ComboBoxDropDownButtonPainter : GtkPlus.Painter
		{
			// Token: 0x06001B89 RID: 7049 RVA: 0x00085AD2 File Offset: 0x00083CD2
			public void Configure(GtkPlusState state)
			{
				this.state = state;
			}

			// Token: 0x06001B8A RID: 7050 RVA: 0x00085ADB File Offset: 0x00083CDB
			public override void AttachStyle(GtkPlus.WidgetType widgetType, IntPtr drawable, GtkPlus gtkPlus)
			{
				gtkPlus.combo_box_drop_down_toggle_button_style = GtkPlus.gtk_style_attach(gtkPlus.combo_box_drop_down_toggle_button_style, drawable);
				gtkPlus.combo_box_drop_down_arrow_style = GtkPlus.gtk_style_attach(gtkPlus.combo_box_drop_down_arrow_style, drawable);
			}

			// Token: 0x06001B8B RID: 7051 RVA: 0x00085B04 File Offset: 0x00083D04
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlusState gtkPlusState = this.state;
				GtkPlus.GtkShadowType gtkShadowType;
				if (gtkPlusState != GtkPlusState.Pressed)
				{
					if (gtkPlusState == GtkPlusState.Disabled)
					{
						gtkShadowType = GtkPlus.GtkShadowType.GTK_SHADOW_ETCHED_IN;
					}
					else
					{
						gtkShadowType = GtkPlus.GtkShadowType.GTK_SHADOW_OUT;
					}
				}
				else
				{
					gtkShadowType = GtkPlus.GtkShadowType.GTK_SHADOW_IN;
				}
				GtkPlus.gtk_paint_box(gtkPlus.combo_box_drop_down_toggle_button_style, window, (GtkPlus.GtkStateType)this.state, gtkShadowType, ref area, gtkPlus.combo_box_drop_down_toggle_button, "button", x, y, width, height);
				GtkPlus.GtkMisc gtkMisc = (GtkPlus.GtkMisc)Marshal.PtrToStructure(gtkPlus.combo_box_drop_down_arrow, typeof(GtkPlus.GtkMisc));
				int num = (int)((float)Math.Min(width - (int)(gtkMisc.xpad * 2), height - (int)(gtkMisc.ypad * 2)) * GtkPlus.GetWidgetStyleSingle(gtkPlus.combo_box_drop_down_arrow, "arrow-scaling"));
				GtkPlus.gtk_paint_arrow(gtkPlus.combo_box_drop_down_arrow_style, window, (GtkPlus.GtkStateType)this.state, GtkPlus.GtkShadowType.GTK_SHADOW_NONE, ref area, gtkPlus.combo_box_drop_down_arrow, "arrow", GtkPlus.GtkArrowType.GTK_ARROW_DOWN, true, (int)Math.Floor((double)((float)(x + (int)gtkMisc.xpad) + (float)(width - num) * gtkMisc.xalign)), (int)Math.Floor((double)((float)(y + (int)gtkMisc.ypad) + (float)(height - num) * gtkMisc.yalign)), num, num);
			}

			// Token: 0x040016B5 RID: 5813
			private GtkPlusState state;
		}

		// Token: 0x020002DA RID: 730
		private class ComboBoxBorderPainter : GtkPlus.Painter
		{
			// Token: 0x06001B8D RID: 7053 RVA: 0x00085C04 File Offset: 0x00083E04
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_shadow(style, window, GtkPlus.GtkStateType.GTK_STATE_NORMAL, GtkPlus.GtkShadowType.GTK_SHADOW_IN, ref area, widget, "combobox", x, y, width, height);
			}
		}

		// Token: 0x020002DB RID: 731
		private class GroupBoxPainter : GtkPlus.Painter
		{
			// Token: 0x06001B8F RID: 7055 RVA: 0x00085C2B File Offset: 0x00083E2B
			public void Configure(GtkPlusState state)
			{
				this.state = state;
			}

			// Token: 0x06001B90 RID: 7056 RVA: 0x00085C34 File Offset: 0x00083E34
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_shadow(style, window, (GtkPlus.GtkStateType)this.state, GtkPlus.GtkShadowType.GTK_SHADOW_ETCHED_IN, ref area, widget, "frame", x, y, width, height);
			}

			// Token: 0x040016B6 RID: 5814
			private GtkPlusState state;
		}

		// Token: 0x020002DC RID: 732
		private class HeaderPainter : GtkPlus.Painter
		{
			// Token: 0x06001B92 RID: 7058 RVA: 0x00085C60 File Offset: 0x00083E60
			public void Configure(GtkPlusState state)
			{
				this.state = state;
			}

			// Token: 0x06001B93 RID: 7059 RVA: 0x00085C69 File Offset: 0x00083E69
			public override void AttachStyle(GtkPlus.WidgetType widgetType, IntPtr drawable, GtkPlus gtkPlus)
			{
				gtkPlus.tree_view_column_button_style = GtkPlus.gtk_style_attach(gtkPlus.tree_view_column_button_style, drawable);
			}

			// Token: 0x06001B94 RID: 7060 RVA: 0x00085C80 File Offset: 0x00083E80
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_box(gtkPlus.tree_view_column_button_style, window, (GtkPlus.GtkStateType)this.state, (this.state == GtkPlusState.Pressed) ? GtkPlus.GtkShadowType.GTK_SHADOW_IN : GtkPlus.GtkShadowType.GTK_SHADOW_OUT, ref area, gtkPlus.tree_view_column_button, "button", x, y, width, height);
			}

			// Token: 0x040016B7 RID: 5815
			private GtkPlusState state;
		}

		// Token: 0x020002DD RID: 733
		private class ProgressBarBarPainter : GtkPlus.Painter
		{
			// Token: 0x06001B96 RID: 7062 RVA: 0x00085CC4 File Offset: 0x00083EC4
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_box(style, window, GtkPlus.GtkStateType.GTK_STATE_NORMAL, GtkPlus.GtkShadowType.GTK_SHADOW_IN, ref area, widget, "trough", x, y, width, height);
			}
		}

		// Token: 0x020002DE RID: 734
		private class ProgressBarChunkPainter : GtkPlus.Painter
		{
			// Token: 0x06001B98 RID: 7064 RVA: 0x00085CEC File Offset: 0x00083EEC
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_box(style, window, GtkPlus.GtkStateType.GTK_STATE_PRELIGHT, GtkPlus.GtkShadowType.GTK_SHADOW_OUT, ref area, widget, "bar", x, y, width, height);
			}
		}

		// Token: 0x020002DF RID: 735
		private class ScrollBarArrowButtonPainter : GtkPlus.Painter
		{
			// Token: 0x06001B9A RID: 7066 RVA: 0x00085D13 File Offset: 0x00083F13
			public void Configure(GtkPlusState state, bool horizontal, bool upOrLeft)
			{
				this.state = state;
				this.horizontal = horizontal;
				this.up_or_left = upOrLeft;
			}

			// Token: 0x06001B9B RID: 7067 RVA: 0x00085D2C File Offset: 0x00083F2C
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				bool flag;
				GtkPlus.g_object_get(widget, "can-focus", out flag, IntPtr.Zero);
				if (flag)
				{
					int num;
					int num2;
					GtkPlus.gtk_widget_style_get(widget, "focus-line-width", out num, "focus-padding", out num2, IntPtr.Zero);
					int num3 = num + num2;
					if (this.horizontal)
					{
						y -= num3;
						height -= 2 * num3;
					}
					else
					{
						x -= num3;
						width -= 2 * num3;
					}
				}
				GtkPlus.GtkShadowType gtkShadowType = ((this.state == GtkPlusState.Pressed) ? GtkPlus.GtkShadowType.GTK_SHADOW_IN : GtkPlus.GtkShadowType.GTK_SHADOW_OUT);
				string text = (this.horizontal ? "hscrollbar" : "vscrollbar");
				GtkPlus.gtk_paint_box(style, window, (GtkPlus.GtkStateType)this.state, gtkShadowType, ref area, widget, text, x, y, width, height);
				width /= 2;
				height /= 2;
				x += width / 2;
				y += height / 2;
				if (this.state == GtkPlusState.Pressed)
				{
					int num4;
					int num5;
					GtkPlus.gtk_widget_style_get(widget, "arrow-displacement-x", out num4, "arrow-displacement-y", out num5, IntPtr.Zero);
					x += num4;
					y += num5;
				}
				GtkPlus.gtk_paint_arrow(style, window, (GtkPlus.GtkStateType)this.state, gtkShadowType, ref area, widget, text, this.horizontal ? (this.up_or_left ? GtkPlus.GtkArrowType.GTK_ARROW_LEFT : GtkPlus.GtkArrowType.GTK_ARROW_RIGHT) : (this.up_or_left ? GtkPlus.GtkArrowType.GTK_ARROW_UP : GtkPlus.GtkArrowType.GTK_ARROW_DOWN), true, x, y, width, height);
			}

			// Token: 0x040016B8 RID: 5816
			private GtkPlusState state;

			// Token: 0x040016B9 RID: 5817
			private bool horizontal;

			// Token: 0x040016BA RID: 5818
			private bool up_or_left;
		}

		// Token: 0x020002E0 RID: 736
		private abstract class RangeThumbButtonPainter : GtkPlus.Painter
		{
			// Token: 0x17000668 RID: 1640
			// (get) Token: 0x06001B9D RID: 7069 RVA: 0x00085E64 File Offset: 0x00084064
			protected bool Horizontal
			{
				get
				{
					return this.horizontal;
				}
			}

			// Token: 0x06001B9E RID: 7070 RVA: 0x00085E6C File Offset: 0x0008406C
			public void Configure(GtkPlusState state, bool horizontal)
			{
				this.state = state;
				this.horizontal = horizontal;
			}

			// Token: 0x06001B9F RID: 7071 RVA: 0x00085E7C File Offset: 0x0008407C
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_slider(style, window, (GtkPlus.GtkStateType)this.state, (this.state == GtkPlusState.Pressed && GtkPlus.GetWidgetStyleBoolean(widget, "activate-slider")) ? GtkPlus.GtkShadowType.GTK_SHADOW_IN : GtkPlus.GtkShadowType.GTK_SHADOW_OUT, ref area, widget, this.Detail, x, y, width, height, this.horizontal ? GtkPlus.GtkOrientation.GTK_ORIENTATION_HORIZONTAL : GtkPlus.GtkOrientation.GTK_ORIENTATION_VERTICAL);
			}

			// Token: 0x17000669 RID: 1641
			// (get) Token: 0x06001BA0 RID: 7072
			protected abstract string Detail { get; }

			// Token: 0x040016BB RID: 5819
			private GtkPlusState state;

			// Token: 0x040016BC RID: 5820
			private bool horizontal;
		}

		// Token: 0x020002E1 RID: 737
		private class ScrollBarThumbButtonPainter : GtkPlus.RangeThumbButtonPainter
		{
			// Token: 0x1700066A RID: 1642
			// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x00085ECF File Offset: 0x000840CF
			protected override string Detail
			{
				get
				{
					return "slider";
				}
			}
		}

		// Token: 0x020002E2 RID: 738
		private class ScrollBarTrackPainter : GtkPlus.Painter
		{
			// Token: 0x06001BA4 RID: 7076 RVA: 0x00085EDE File Offset: 0x000840DE
			public void Configure(GtkPlusState state, bool upOrLeft)
			{
				this.state = state;
				this.up_or_left = upOrLeft;
			}

			// Token: 0x06001BA5 RID: 7077 RVA: 0x00085EF0 File Offset: 0x000840F0
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_box(style, window, (this.state == GtkPlusState.Pressed) ? GtkPlus.GtkStateType.GTK_STATE_ACTIVE : GtkPlus.GtkStateType.GTK_STATE_INSENSITIVE, GtkPlus.GtkShadowType.GTK_SHADOW_IN, ref area, widget, GtkPlus.GetWidgetStyleBoolean(widget, "trough-side-details") ? (this.up_or_left ? "trough-upper" : "trough-lower") : "trough", x, y, width, height);
			}

			// Token: 0x040016BD RID: 5821
			private GtkPlusState state;

			// Token: 0x040016BE RID: 5822
			private bool up_or_left;
		}

		// Token: 0x020002E3 RID: 739
		private class StatusBarGripperPainter : GtkPlus.Painter
		{
			// Token: 0x06001BA7 RID: 7079 RVA: 0x00085F48 File Offset: 0x00084148
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_resize_grip(style, window, GtkPlus.GtkStateType.GTK_STATE_NORMAL, ref area, widget, "statusbar", GtkPlus.GdkWindowEdge.GDK_WINDOW_EDGE_SOUTH_EAST, x, y, width, height);
			}
		}

		// Token: 0x020002E4 RID: 740
		private class TabControlPanePainter : GtkPlus.Painter
		{
			// Token: 0x06001BA9 RID: 7081 RVA: 0x00085F70 File Offset: 0x00084170
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_box_gap(style, window, GtkPlus.GtkStateType.GTK_STATE_NORMAL, GtkPlus.GtkShadowType.GTK_SHADOW_OUT, ref area, widget, "notebook", x, y, width, height, GtkPlus.GtkPositionType.GTK_POS_TOP, 0, 0);
			}
		}

		// Token: 0x020002E5 RID: 741
		private class TabControlTabItemPainter : GtkPlus.Painter
		{
			// Token: 0x06001BAB RID: 7083 RVA: 0x00085F9A File Offset: 0x0008419A
			public void Configure(GtkPlusState state)
			{
				this.state = state;
			}

			// Token: 0x06001BAC RID: 7084 RVA: 0x00085FA4 File Offset: 0x000841A4
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_extension(style, window, (GtkPlus.GtkStateType)this.state, GtkPlus.GtkShadowType.GTK_SHADOW_OUT, ref area, widget, "tab", x, y, width, height, GtkPlus.GtkPositionType.GTK_POS_BOTTOM);
			}

			// Token: 0x040016BF RID: 5823
			private GtkPlusState state;
		}

		// Token: 0x020002E6 RID: 742
		private class TextBoxPainter : GtkPlus.Painter
		{
			// Token: 0x06001BAE RID: 7086 RVA: 0x00085FD1 File Offset: 0x000841D1
			public void Configure(GtkPlusState state)
			{
				this.state = state;
			}

			// Token: 0x06001BAF RID: 7087 RVA: 0x00085FDC File Offset: 0x000841DC
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_shadow(style, window, GtkPlus.GtkStateType.GTK_STATE_NORMAL, GtkPlus.GtkShadowType.GTK_SHADOW_IN, ref area, widget, "entry", x, y, width, height);
				GtkPlus.GtkStyle gtkStyle = (GtkPlus.GtkStyle)Marshal.PtrToStructure(style, typeof(GtkPlus.GtkStyle));
				x += gtkStyle.xthickness;
				y += gtkStyle.ythickness;
				width -= 2 * gtkStyle.xthickness;
				height -= 2 * gtkStyle.ythickness;
				GtkPlus.gtk_paint_flat_box(style, window, (GtkPlus.GtkStateType)this.state, GtkPlus.GtkShadowType.GTK_SHADOW_NONE, ref area, widget, "entry_bg", x, y, width, height);
			}

			// Token: 0x040016C0 RID: 5824
			private GtkPlusState state;
		}

		// Token: 0x020002E7 RID: 743
		private class ToolBarPainter : GtkPlus.Painter
		{
			// Token: 0x06001BB1 RID: 7089 RVA: 0x00086068 File Offset: 0x00084268
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_box(style, window, GtkPlus.GtkStateType.GTK_STATE_NORMAL, GtkPlus.GetWidgetStyleShadowType(widget), ref area, widget, "toolbar", x, y, width, height);
			}
		}

		// Token: 0x020002E8 RID: 744
		private class ToolBarButtonPainter : GtkPlus.Painter
		{
			// Token: 0x06001BB3 RID: 7091 RVA: 0x00086095 File Offset: 0x00084295
			public void Configure(GtkPlusState state)
			{
				this.state = state;
			}

			// Token: 0x06001BB4 RID: 7092 RVA: 0x0008609E File Offset: 0x0008429E
			public override void AttachStyle(GtkPlus.WidgetType widgetType, IntPtr drawable, GtkPlus gtkPlus)
			{
				gtkPlus.tool_bar_button_style = GtkPlus.gtk_style_attach(gtkPlus.tool_bar_button_style, drawable);
			}

			// Token: 0x06001BB5 RID: 7093 RVA: 0x000860B4 File Offset: 0x000842B4
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_box(gtkPlus.tool_bar_button_style, window, (GtkPlus.GtkStateType)this.state, (this.state == GtkPlusState.Pressed) ? GtkPlus.GtkShadowType.GTK_SHADOW_IN : GtkPlus.GtkShadowType.GTK_SHADOW_OUT, ref area, gtkPlus.tool_bar_button, "button", x, y, width, height);
			}

			// Token: 0x040016C1 RID: 5825
			private GtkPlusState state;
		}

		// Token: 0x020002E9 RID: 745
		private class ToolBarCheckedButtonPainter : GtkPlus.Painter
		{
			// Token: 0x06001BB7 RID: 7095 RVA: 0x000860F7 File Offset: 0x000842F7
			public override void AttachStyle(GtkPlus.WidgetType widgetType, IntPtr drawable, GtkPlus gtkPlus)
			{
				gtkPlus.tool_bar_toggle_button_style = GtkPlus.gtk_style_attach(gtkPlus.tool_bar_toggle_button_style, drawable);
			}

			// Token: 0x06001BB8 RID: 7096 RVA: 0x0008610C File Offset: 0x0008430C
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_box(gtkPlus.tool_bar_toggle_button_style, window, GtkPlus.GtkStateType.GTK_STATE_ACTIVE, GtkPlus.GtkShadowType.GTK_SHADOW_IN, ref area, gtkPlus.tool_bar_toggle_button, "button", x, y, width, height);
			}
		}

		// Token: 0x020002EA RID: 746
		private class TrackBarTrackPainter : GtkPlus.Painter
		{
			// Token: 0x06001BBA RID: 7098 RVA: 0x00086140 File Offset: 0x00084340
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_box(style, window, GtkPlus.GtkStateType.GTK_STATE_ACTIVE, GtkPlus.GtkShadowType.GTK_SHADOW_IN, ref area, widget, "trough", x, y, width, height);
			}
		}

		// Token: 0x020002EB RID: 747
		private class TrackBarThumbPainter : GtkPlus.RangeThumbButtonPainter
		{
			// Token: 0x1700066B RID: 1643
			// (get) Token: 0x06001BBC RID: 7100 RVA: 0x00086167 File Offset: 0x00084367
			protected override string Detail
			{
				get
				{
					if (!base.Horizontal)
					{
						return "vscale";
					}
					return "hscale";
				}
			}
		}

		// Token: 0x020002EC RID: 748
		private class TreeViewGlyphPainter : GtkPlus.Painter
		{
			// Token: 0x06001BBE RID: 7102 RVA: 0x0008617C File Offset: 0x0008437C
			public void Configure(bool closed)
			{
				this.closed = closed;
			}

			// Token: 0x06001BBF RID: 7103 RVA: 0x00086188 File Offset: 0x00084388
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_expander(style, window, GtkPlus.GtkStateType.GTK_STATE_NORMAL, ref area, widget, "treeview", x + width / 2, y + height / 2, this.closed ? GtkPlus.GtkExpanderStyle.GTK_EXPANDER_COLLAPSED : GtkPlus.GtkExpanderStyle.GTK_EXPANDER_EXPANDED);
			}

			// Token: 0x040016C2 RID: 5826
			private bool closed;
		}

		// Token: 0x020002ED RID: 749
		private class UpDownPainter : GtkPlus.Painter
		{
			// Token: 0x06001BC1 RID: 7105 RVA: 0x000861C0 File Offset: 0x000843C0
			public void Configure(bool up, GtkPlusState state)
			{
				this.up = up;
				this.state = state;
			}

			// Token: 0x06001BC2 RID: 7106 RVA: 0x000861D0 File Offset: 0x000843D0
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.GtkShadowType gtkShadowType = GtkPlus.GetWidgetStyleShadowType(widget);
				if (gtkShadowType != GtkPlus.GtkShadowType.GTK_SHADOW_NONE)
				{
					GtkPlus.gtk_paint_box(style, window, GtkPlus.GtkStateType.GTK_STATE_NORMAL, gtkShadowType, ref area, widget, "spinbutton", x, y - (this.up ? 0 : height), width, height * 2);
				}
				gtkShadowType = ((this.state == GtkPlusState.Pressed) ? GtkPlus.GtkShadowType.GTK_SHADOW_IN : GtkPlus.GtkShadowType.GTK_SHADOW_OUT);
				GtkPlus.gtk_paint_box(style, window, (GtkPlus.GtkStateType)this.state, gtkShadowType, ref area, widget, this.up ? "spinbutton_up" : "spinbutton_down", x, y, width, height);
				if (this.up)
				{
					y += 2;
				}
				height -= 2;
				width -= 3;
				x++;
				int num = width / 2;
				num -= num % 2 - 1;
				int num2 = (num + 1) / 2;
				x += (width - num) / 2;
				y += (height - num2) / 2;
				height = num2;
				width = num;
				GtkPlus.gtk_paint_arrow(style, window, (GtkPlus.GtkStateType)this.state, gtkShadowType, ref area, widget, "spinbutton", this.up ? GtkPlus.GtkArrowType.GTK_ARROW_UP : GtkPlus.GtkArrowType.GTK_ARROW_DOWN, true, x, y, width, height);
			}

			// Token: 0x040016C3 RID: 5827
			private bool up;

			// Token: 0x040016C4 RID: 5828
			private GtkPlusState state;
		}

		// Token: 0x020002EE RID: 750
		private enum WidgetType
		{
			// Token: 0x040016C6 RID: 5830
			Button,
			// Token: 0x040016C7 RID: 5831
			CheckBox,
			// Token: 0x040016C8 RID: 5832
			ComboBox,
			// Token: 0x040016C9 RID: 5833
			GroupBox,
			// Token: 0x040016CA RID: 5834
			ProgressBar,
			// Token: 0x040016CB RID: 5835
			RadioButton,
			// Token: 0x040016CC RID: 5836
			HScrollBar,
			// Token: 0x040016CD RID: 5837
			VScrollBar,
			// Token: 0x040016CE RID: 5838
			StatusBar,
			// Token: 0x040016CF RID: 5839
			TabControl,
			// Token: 0x040016D0 RID: 5840
			TextBox,
			// Token: 0x040016D1 RID: 5841
			ToolBar,
			// Token: 0x040016D2 RID: 5842
			HorizontalTrackBar,
			// Token: 0x040016D3 RID: 5843
			VerticalTrackBar,
			// Token: 0x040016D4 RID: 5844
			TreeView,
			// Token: 0x040016D5 RID: 5845
			UpDown
		}

		// Token: 0x020002EF RID: 751
		private static class GetFirstChildWidgetOfType
		{
			// Token: 0x06001BC4 RID: 7108 RVA: 0x000862CC File Offset: 0x000844CC
			public static IntPtr Get(IntPtr parent, IntPtr childType)
			{
				GtkPlus.GetFirstChildWidgetOfType.Type = childType;
				GtkPlus.GetFirstChildWidgetOfType.Result = IntPtr.Zero;
				GtkPlus.GetFirstChildWidgetOfType.ContainersToSearch = new ArrayList();
				GtkPlus.GetFirstChildWidgetOfType.ContainersToSearch.Add(parent);
				do
				{
					ArrayList containersToSearch = GtkPlus.GetFirstChildWidgetOfType.ContainersToSearch;
					GtkPlus.GetFirstChildWidgetOfType.ContainersToSearch = new ArrayList();
					foreach (object obj in containersToSearch)
					{
						IntPtr intPtr = (IntPtr)obj;
						GtkPlus.gtk_widget_realize(intPtr);
						GtkPlus.gtk_container_forall(intPtr, new GtkPlus.GtkCallback(GtkPlus.GetFirstChildWidgetOfType.Callback), IntPtr.Zero);
						if (GtkPlus.GetFirstChildWidgetOfType.Result != IntPtr.Zero)
						{
							return GtkPlus.GetFirstChildWidgetOfType.Result;
						}
					}
				}
				while (GtkPlus.GetFirstChildWidgetOfType.ContainersToSearch.Count != 0);
				return IntPtr.Zero;
			}

			// Token: 0x06001BC5 RID: 7109 RVA: 0x0008639C File Offset: 0x0008459C
			private static void Callback(IntPtr widget, IntPtr data)
			{
				if (GtkPlus.GetFirstChildWidgetOfType.Result != IntPtr.Zero)
				{
					return;
				}
				if (GtkPlus.g_type_check_instance_is_a(widget, GtkPlus.GetFirstChildWidgetOfType.Type))
				{
					GtkPlus.GetFirstChildWidgetOfType.Result = widget;
					return;
				}
				if (GtkPlus.g_type_check_instance_is_a(widget, GtkPlus.gtk_container_get_type()))
				{
					GtkPlus.GetFirstChildWidgetOfType.ContainersToSearch.Add(widget);
				}
			}

			// Token: 0x040016D6 RID: 5846
			private static IntPtr Type;

			// Token: 0x040016D7 RID: 5847
			private static IntPtr Result;

			// Token: 0x040016D8 RID: 5848
			private static ArrayList ContainersToSearch;
		}

		// Token: 0x020002F0 RID: 752
		private struct GdkColor
		{
			// Token: 0x06001BC6 RID: 7110 RVA: 0x000863ED File Offset: 0x000845ED
			public GdkColor(Color value)
			{
				this.pixel = 0U;
				this.red = (ushort)(value.R << 8);
				this.green = (ushort)(value.G << 8);
				this.blue = (ushort)(value.B << 8);
			}

			// Token: 0x040016D9 RID: 5849
			public uint pixel;

			// Token: 0x040016DA RID: 5850
			public ushort red;

			// Token: 0x040016DB RID: 5851
			public ushort green;

			// Token: 0x040016DC RID: 5852
			public ushort blue;
		}

		// Token: 0x020002F1 RID: 753
		internal struct GdkRectangle
		{
			// Token: 0x06001BC7 RID: 7111 RVA: 0x00086426 File Offset: 0x00084626
			public GdkRectangle(Rectangle value)
			{
				this.x = value.X;
				this.y = value.Y;
				this.width = value.Width;
				this.height = value.Height;
			}

			// Token: 0x040016DD RID: 5853
			public int x;

			// Token: 0x040016DE RID: 5854
			public int y;

			// Token: 0x040016DF RID: 5855
			public int width;

			// Token: 0x040016E0 RID: 5856
			public int height;
		}

		// Token: 0x020002F2 RID: 754
		private enum GdkColorspace
		{
			// Token: 0x040016E2 RID: 5858
			GDK_COLORSPACE_RGB
		}

		// Token: 0x020002F3 RID: 755
		// (Invoke) Token: 0x06001BC9 RID: 7113
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void GtkCallback(IntPtr widget, IntPtr data);

		// Token: 0x020002F4 RID: 756
		internal enum GtkShadowType
		{
			// Token: 0x040016E4 RID: 5860
			GTK_SHADOW_NONE,
			// Token: 0x040016E5 RID: 5861
			GTK_SHADOW_IN,
			// Token: 0x040016E6 RID: 5862
			GTK_SHADOW_OUT,
			// Token: 0x040016E7 RID: 5863
			GTK_SHADOW_ETCHED_IN,
			// Token: 0x040016E8 RID: 5864
			GTK_SHADOW_ETCHED_OUT
		}

		// Token: 0x020002F5 RID: 757
		private enum GtkStateType
		{
			// Token: 0x040016EA RID: 5866
			GTK_STATE_NORMAL,
			// Token: 0x040016EB RID: 5867
			GTK_STATE_ACTIVE,
			// Token: 0x040016EC RID: 5868
			GTK_STATE_PRELIGHT,
			// Token: 0x040016ED RID: 5869
			GTK_STATE_SELECTED,
			// Token: 0x040016EE RID: 5870
			GTK_STATE_INSENSITIVE
		}

		// Token: 0x020002F6 RID: 758
		private enum GtkWindowType
		{
			// Token: 0x040016F0 RID: 5872
			GTK_WINDOW_TOPLEVEL,
			// Token: 0x040016F1 RID: 5873
			GTK_WINDOW_POPUP
		}

		// Token: 0x020002F7 RID: 759
		private enum GtkArrowType
		{
			// Token: 0x040016F3 RID: 5875
			GTK_ARROW_UP,
			// Token: 0x040016F4 RID: 5876
			GTK_ARROW_DOWN,
			// Token: 0x040016F5 RID: 5877
			GTK_ARROW_LEFT,
			// Token: 0x040016F6 RID: 5878
			GTK_ARROW_RIGHT,
			// Token: 0x040016F7 RID: 5879
			GTK_ARROW_NONE
		}

		// Token: 0x020002F8 RID: 760
		private enum GtkOrientation
		{
			// Token: 0x040016F9 RID: 5881
			GTK_ORIENTATION_HORIZONTAL,
			// Token: 0x040016FA RID: 5882
			GTK_ORIENTATION_VERTICAL
		}

		// Token: 0x020002F9 RID: 761
		private enum GtkExpanderStyle
		{
			// Token: 0x040016FC RID: 5884
			GTK_EXPANDER_COLLAPSED,
			// Token: 0x040016FD RID: 5885
			GTK_EXPANDER_SEMI_COLLAPSED,
			// Token: 0x040016FE RID: 5886
			GTK_EXPANDER_SEMI_EXPANDED,
			// Token: 0x040016FF RID: 5887
			GTK_EXPANDER_EXPANDED
		}

		// Token: 0x020002FA RID: 762
		private enum GtkPositionType
		{
			// Token: 0x04001701 RID: 5889
			GTK_POS_LEFT,
			// Token: 0x04001702 RID: 5890
			GTK_POS_RIGHT,
			// Token: 0x04001703 RID: 5891
			GTK_POS_TOP,
			// Token: 0x04001704 RID: 5892
			GTK_POS_BOTTOM
		}

		// Token: 0x020002FB RID: 763
		private enum GtkWidgetFlags : uint
		{
			// Token: 0x04001706 RID: 5894
			GTK_CAN_DEFAULT = 8192U
		}

		// Token: 0x020002FC RID: 764
		private enum GdkWindowEdge
		{
			// Token: 0x04001708 RID: 5896
			GDK_WINDOW_EDGE_NORTH_WEST,
			// Token: 0x04001709 RID: 5897
			GDK_WINDOW_EDGE_NORTH,
			// Token: 0x0400170A RID: 5898
			GDK_WINDOW_EDGE_NORTH_EAST,
			// Token: 0x0400170B RID: 5899
			GDK_WINDOW_EDGE_WEST,
			// Token: 0x0400170C RID: 5900
			GDK_WINDOW_EDGE_EAST,
			// Token: 0x0400170D RID: 5901
			GDK_WINDOW_EDGE_SOUTH_WEST,
			// Token: 0x0400170E RID: 5902
			GDK_WINDOW_EDGE_SOUTH,
			// Token: 0x0400170F RID: 5903
			GDK_WINDOW_EDGE_SOUTH_EAST
		}

		// Token: 0x020002FD RID: 765
		private struct GtkStyle
		{
			// Token: 0x04001710 RID: 5904
			private GtkPlus.GObject parent_instance;

			// Token: 0x04001711 RID: 5905
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			private GtkPlus.GdkColor[] fg;

			// Token: 0x04001712 RID: 5906
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			private GtkPlus.GdkColor[] bg;

			// Token: 0x04001713 RID: 5907
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			private GtkPlus.GdkColor[] light;

			// Token: 0x04001714 RID: 5908
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			private GtkPlus.GdkColor[] dark;

			// Token: 0x04001715 RID: 5909
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			private GtkPlus.GdkColor[] mid;

			// Token: 0x04001716 RID: 5910
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			private GtkPlus.GdkColor[] text;

			// Token: 0x04001717 RID: 5911
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			private GtkPlus.GdkColor[] @base;

			// Token: 0x04001718 RID: 5912
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			private GtkPlus.GdkColor[] text_aa;

			// Token: 0x04001719 RID: 5913
			private GtkPlus.GdkColor black;

			// Token: 0x0400171A RID: 5914
			private GtkPlus.GdkColor white;

			// Token: 0x0400171B RID: 5915
			private IntPtr font_desc;

			// Token: 0x0400171C RID: 5916
			public int xthickness;

			// Token: 0x0400171D RID: 5917
			public int ythickness;
		}

		// Token: 0x020002FE RID: 766
		private struct GtkWidget
		{
			// Token: 0x0400171E RID: 5918
			private GtkPlus.GtkObject @object;

			// Token: 0x0400171F RID: 5919
			private ushort private_flags;

			// Token: 0x04001720 RID: 5920
			private byte state;

			// Token: 0x04001721 RID: 5921
			private byte saved_state;

			// Token: 0x04001722 RID: 5922
			private string name;

			// Token: 0x04001723 RID: 5923
			private IntPtr style;

			// Token: 0x04001724 RID: 5924
			private GtkPlus.GtkRequisition requisition;

			// Token: 0x04001725 RID: 5925
			public GtkPlus.GdkRectangle allocation;

			// Token: 0x04001726 RID: 5926
			private IntPtr window;

			// Token: 0x04001727 RID: 5927
			private IntPtr parent;
		}

		// Token: 0x020002FF RID: 767
		private struct GtkObject
		{
			// Token: 0x04001728 RID: 5928
			private GtkPlus.GObject parent_instance;

			// Token: 0x04001729 RID: 5929
			public uint flags;
		}

		// Token: 0x02000300 RID: 768
		private struct GtkRequisition
		{
			// Token: 0x0400172A RID: 5930
			private int width;

			// Token: 0x0400172B RID: 5931
			private int height;
		}

		// Token: 0x02000301 RID: 769
		private struct GtkMisc
		{
			// Token: 0x0400172C RID: 5932
			private GtkPlus.GtkWidget widget;

			// Token: 0x0400172D RID: 5933
			public float xalign;

			// Token: 0x0400172E RID: 5934
			public float yalign;

			// Token: 0x0400172F RID: 5935
			public ushort xpad;

			// Token: 0x04001730 RID: 5936
			public ushort ypad;
		}

		// Token: 0x02000302 RID: 770
		private struct GtkTreeViewColumn
		{
			// Token: 0x04001731 RID: 5937
			private GtkPlus.GtkObject parent;

			// Token: 0x04001732 RID: 5938
			private IntPtr tree_view;

			// Token: 0x04001733 RID: 5939
			public IntPtr button;
		}

		// Token: 0x02000303 RID: 771
		private struct GTypeInstance
		{
			// Token: 0x04001734 RID: 5940
			private IntPtr g_class;
		}

		// Token: 0x02000304 RID: 772
		internal struct GObject
		{
			// Token: 0x04001735 RID: 5941
			private GtkPlus.GTypeInstance g_type_instance;

			// Token: 0x04001736 RID: 5942
			private uint ref_count;

			// Token: 0x04001737 RID: 5943
			private IntPtr qdata;
		}
	}
}
