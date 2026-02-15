using System;
using System.Drawing;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x020001A7 RID: 423
	internal abstract class Theme
	{
		// Token: 0x06001060 RID: 4192 RVA: 0x00050764 File Offset: 0x0004E964
		private void SetSystemColors(KnownColor kc, Color value)
		{
			if (this.update == null)
			{
				Type type = Type.GetType("System.Drawing.KnownColors, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				if (type != null)
				{
					this.update = type.GetMethod("Update", BindingFlags.Static | BindingFlags.Public);
				}
			}
			if (this.update != null)
			{
				this.update.Invoke(null, new object[]
				{
					(int)kc,
					value.ToArgb()
				});
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x000507DF File Offset: 0x0004E9DF
		public virtual Color ColorScrollBar
		{
			get
			{
				return SystemColors.ScrollBar;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x000507E6 File Offset: 0x0004E9E6
		// (set) Token: 0x06001063 RID: 4195 RVA: 0x000507ED File Offset: 0x0004E9ED
		public virtual Color ColorMenu
		{
			get
			{
				return SystemColors.Menu;
			}
			set
			{
				this.SetSystemColors(KnownColor.Menu, value);
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x000507F8 File Offset: 0x0004E9F8
		public virtual Color ColorWindow
		{
			get
			{
				return SystemColors.Window;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x000507FF File Offset: 0x0004E9FF
		// (set) Token: 0x06001066 RID: 4198 RVA: 0x00050806 File Offset: 0x0004EA06
		public virtual Color ColorMenuText
		{
			get
			{
				return SystemColors.MenuText;
			}
			set
			{
				this.SetSystemColors(KnownColor.MenuText, value);
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x00050811 File Offset: 0x0004EA11
		public virtual Color ColorWindowText
		{
			get
			{
				return SystemColors.WindowText;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x00050818 File Offset: 0x0004EA18
		// (set) Token: 0x06001069 RID: 4201 RVA: 0x0005081F File Offset: 0x0004EA1F
		public virtual Color ColorHighlight
		{
			get
			{
				return SystemColors.Highlight;
			}
			set
			{
				this.SetSystemColors(KnownColor.Highlight, value);
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x0005082A File Offset: 0x0004EA2A
		// (set) Token: 0x0600106B RID: 4203 RVA: 0x00050831 File Offset: 0x0004EA31
		public virtual Color ColorHighlightText
		{
			get
			{
				return SystemColors.HighlightText;
			}
			set
			{
				this.SetSystemColors(KnownColor.HighlightText, value);
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x0005083C File Offset: 0x0004EA3C
		// (set) Token: 0x0600106D RID: 4205 RVA: 0x00050843 File Offset: 0x0004EA43
		public virtual Color ColorControl
		{
			get
			{
				return SystemColors.Control;
			}
			set
			{
				this.SetSystemColors(KnownColor.Control, value);
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x0005084D File Offset: 0x0004EA4D
		// (set) Token: 0x0600106F RID: 4207 RVA: 0x00050854 File Offset: 0x0004EA54
		public virtual Color ColorControlDark
		{
			get
			{
				return SystemColors.ControlDark;
			}
			set
			{
				this.SetSystemColors(KnownColor.ControlDark, value);
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x0005085E File Offset: 0x0004EA5E
		public virtual Color ColorGrayText
		{
			get
			{
				return SystemColors.GrayText;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x00050865 File Offset: 0x0004EA65
		// (set) Token: 0x06001072 RID: 4210 RVA: 0x0005086C File Offset: 0x0004EA6C
		public virtual Color ColorControlText
		{
			get
			{
				return SystemColors.ControlText;
			}
			set
			{
				this.SetSystemColors(KnownColor.ControlText, value);
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x00050877 File Offset: 0x0004EA77
		public virtual Color ColorInactiveCaptionText
		{
			get
			{
				return SystemColors.InactiveCaptionText;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x0005087E File Offset: 0x0004EA7E
		// (set) Token: 0x06001075 RID: 4213 RVA: 0x00050885 File Offset: 0x0004EA85
		public virtual Color ColorControlLight
		{
			get
			{
				return SystemColors.ControlLight;
			}
			set
			{
				this.SetSystemColors(KnownColor.ControlLight, value);
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x0005088F File Offset: 0x0004EA8F
		// (set) Token: 0x06001077 RID: 4215 RVA: 0x00050896 File Offset: 0x0004EA96
		public virtual Color ColorControlDarkDark
		{
			get
			{
				return SystemColors.ControlDarkDark;
			}
			set
			{
				this.SetSystemColors(KnownColor.ControlDarkDark, value);
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x000508A0 File Offset: 0x0004EAA0
		// (set) Token: 0x06001079 RID: 4217 RVA: 0x000508A7 File Offset: 0x0004EAA7
		public virtual Color ColorControlLightLight
		{
			get
			{
				return SystemColors.ControlLightLight;
			}
			set
			{
				this.SetSystemColors(KnownColor.ControlLightLight, value);
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x000508B2 File Offset: 0x0004EAB2
		public virtual Color ColorInfoText
		{
			get
			{
				return SystemColors.InfoText;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x000508B9 File Offset: 0x0004EAB9
		public virtual Color ColorInfo
		{
			get
			{
				return SystemColors.Info;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x000508C0 File Offset: 0x0004EAC0
		public virtual Color DefaultControlBackColor
		{
			get
			{
				return this.ColorControl;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x000508C8 File Offset: 0x0004EAC8
		public virtual Color DefaultControlForeColor
		{
			get
			{
				return this.ColorControlText;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x000508D0 File Offset: 0x0004EAD0
		public virtual Font DefaultFont
		{
			get
			{
				Font font;
				if ((font = this.default_font) == null)
				{
					font = (this.default_font = SystemFonts.DefaultFont);
				}
				return font;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x000508F5 File Offset: 0x0004EAF5
		public virtual Size BorderSizableSize
		{
			get
			{
				return new Size(3, 3);
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x000508FE File Offset: 0x0004EAFE
		public virtual Size Border3DSize
		{
			get
			{
				return XplatUI.Border3DSize;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x00050905 File Offset: 0x0004EB05
		public virtual Size BorderStaticSize
		{
			get
			{
				return new Size(1, 1);
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x0005090E File Offset: 0x0004EB0E
		public virtual Size BorderSize
		{
			get
			{
				return XplatUI.BorderSize;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x00050915 File Offset: 0x0004EB15
		public virtual Size CaptionButtonSize
		{
			get
			{
				return XplatUI.CaptionButtonSize;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x0005091C File Offset: 0x0004EB1C
		public virtual int CaptionHeight
		{
			get
			{
				return XplatUI.CaptionHeight;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x00050923 File Offset: 0x0004EB23
		public virtual Size DoubleClickSize
		{
			get
			{
				return new Size(4, 4);
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x0005092C File Offset: 0x0004EB2C
		public virtual int DoubleClickTime
		{
			get
			{
				return XplatUI.DoubleClickTime;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x00050933 File Offset: 0x0004EB33
		public virtual Size FrameBorderSize
		{
			get
			{
				return XplatUI.FrameBorderSize;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x0005093A File Offset: 0x0004EB3A
		public virtual int HorizontalScrollBarHeight
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x0005093E File Offset: 0x0004EB3E
		public virtual bool MenuAccessKeysUnderlined
		{
			get
			{
				return XplatUI.MenuAccessKeysUnderlined;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x00050945 File Offset: 0x0004EB45
		public virtual Size MenuButtonSize
		{
			get
			{
				return XplatUI.MenuButtonSize;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x0005094C File Offset: 0x0004EB4C
		public virtual Size MenuCheckSize
		{
			get
			{
				return new Size(13, 13);
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x00050958 File Offset: 0x0004EB58
		public virtual Font MenuFont
		{
			get
			{
				Font font;
				if ((font = this.default_font) == null)
				{
					font = (this.default_font = SystemFonts.DefaultFont);
				}
				return font;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x0005097D File Offset: 0x0004EB7D
		public virtual int MenuHeight
		{
			get
			{
				return XplatUI.MenuHeight;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x00050984 File Offset: 0x0004EB84
		public virtual int MouseWheelScrollLines
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x00050987 File Offset: 0x0004EB87
		public virtual Size ToolWindowCaptionButtonSize
		{
			get
			{
				return XplatUI.ToolWindowCaptionButtonSize;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x0005098E File Offset: 0x0004EB8E
		public virtual int ToolWindowCaptionHeight
		{
			get
			{
				return XplatUI.ToolWindowCaptionHeight;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x0005093A File Offset: 0x0004EB3A
		public virtual int VerticalScrollBarWidth
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001092 RID: 4242
		public abstract Font WindowBorderFont { get; }

		// Token: 0x06001093 RID: 4243 RVA: 0x00050995 File Offset: 0x0004EB95
		public int Clamp(int value, int lower, int upper)
		{
			if (value < lower)
			{
				return lower;
			}
			if (value > upper)
			{
				return upper;
			}
			return value;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x000509A4 File Offset: 0x0004EBA4
		[MonoInternalNote("Figure out where to point for My Network Places")]
		public virtual string Places(UIIcon index)
		{
			switch (index)
			{
			case UIIcon.PlacesRecentDocuments:
				return Environment.GetFolderPath(Environment.SpecialFolder.Recent);
			case UIIcon.PlacesDesktop:
				return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
			case UIIcon.PlacesPersonal:
				return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			case UIIcon.PlacesMyComputer:
				return Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
			case UIIcon.PlacesMyNetwork:
				return "/tmp";
			default:
				throw new ArgumentOutOfRangeException("index", index, "Unsupported place");
			}
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00050A08 File Offset: 0x0004EC08
		private Image GetSizedResourceImage(string name, int width)
		{
			Image image = this.ResPool.GetUIImage(name, width);
			if (image != null)
			{
				return image;
			}
			if (width > 0)
			{
				image = ResourceImageLoader.Get(string.Format("{1}_{0}", name, width));
				if (image != null)
				{
					this.ResPool.AddUIImage(image, name, width);
					return image;
				}
			}
			image = ResourceImageLoader.Get(name);
			if (image == null)
			{
				return null;
			}
			this.ResPool.AddUIImage(image, name, 0);
			if (image.Width != width && width != 0)
			{
				Console.Error.WriteLine("warning: requesting icon that not been tuned {0}_{1} {2}", width, name, image.Width);
				int num = image.Height * width / image.Width;
				Bitmap bitmap = new Bitmap(width, num);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.DrawImage(image, 0, 0, width, num);
				}
				this.ResPool.AddUIImage(bitmap, name, width);
				return bitmap;
			}
			return image;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00050AF4 File Offset: 0x0004ECF4
		public virtual Image Images(UIIcon index, int size)
		{
			switch (index)
			{
			case UIIcon.PlacesRecentDocuments:
				return this.GetSizedResourceImage("document-open.png", size);
			case UIIcon.PlacesDesktop:
				return this.GetSizedResourceImage("user-desktop.png", size);
			case UIIcon.PlacesPersonal:
				return this.GetSizedResourceImage("user-home.png", size);
			case UIIcon.PlacesMyComputer:
				return this.GetSizedResourceImage("computer.png", size);
			case UIIcon.PlacesMyNetwork:
				return this.GetSizedResourceImage("folder-remote.png", size);
			case UIIcon.MessageBoxError:
				return this.GetSizedResourceImage("dialog-error.png", size);
			case UIIcon.MessageBoxQuestion:
				return this.GetSizedResourceImage("dialog-question.png", size);
			case UIIcon.MessageBoxWarning:
				return this.GetSizedResourceImage("dialog-warning.png", size);
			case UIIcon.MessageBoxInfo:
				return this.GetSizedResourceImage("dialog-information.png", size);
			case UIIcon.NormalFolder:
				return this.GetSizedResourceImage("folder.png", size);
			default:
				throw new ArgumentException("Invalid Icon type requested", "index");
			}
		}

		// Token: 0x06001097 RID: 4247
		public abstract void ResetDefaults();

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001098 RID: 4248
		public abstract bool DoubleBufferingSupported { get; }

		// Token: 0x06001099 RID: 4249
		public abstract Size CalculateButtonAutoSize(Button button);

		// Token: 0x0600109A RID: 4250
		public abstract void CalculateButtonTextAndImageLayout(Graphics g, ButtonBase b, out Rectangle textRectangle, out Rectangle imageRectangle);

		// Token: 0x0600109B RID: 4251
		public abstract void DrawButton(Graphics g, Button b, Rectangle textBounds, Rectangle imageBounds, Rectangle clipRectangle);

		// Token: 0x0600109C RID: 4252
		public abstract void DrawFlatButton(Graphics g, ButtonBase b, Rectangle textBounds, Rectangle imageBounds, Rectangle clipRectangle);

		// Token: 0x0600109D RID: 4253
		public abstract void DrawPopupButton(Graphics g, Button b, Rectangle textBounds, Rectangle imageBounds, Rectangle clipRectangle);

		// Token: 0x0600109E RID: 4254
		public abstract void DrawButtonBase(Graphics dc, Rectangle clip_area, ButtonBase button);

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600109F RID: 4255
		public abstract Size ButtonBaseDefaultSize { get; }

		// Token: 0x060010A0 RID: 4256
		public abstract Size CalculateCheckBoxAutoSize(CheckBox checkBox);

		// Token: 0x060010A1 RID: 4257
		public abstract void CalculateCheckBoxTextAndImageLayout(ButtonBase b, Point offset, out Rectangle glyphArea, out Rectangle textRectangle, out Rectangle imageRectangle);

		// Token: 0x060010A2 RID: 4258
		public abstract void DrawCheckBox(Graphics g, CheckBox cb, Rectangle glyphArea, Rectangle textBounds, Rectangle imageBounds, Rectangle clipRectangle);

		// Token: 0x060010A3 RID: 4259
		public abstract void DrawCheckBox(Graphics dc, Rectangle clip_area, CheckBox checkbox);

		// Token: 0x060010A4 RID: 4260
		public abstract void DrawComboBoxItem(ComboBox ctrl, DrawItemEventArgs e);

		// Token: 0x060010A5 RID: 4261
		public abstract void DrawFlatStyleComboButton(Graphics graphics, Rectangle rectangle, ButtonState state);

		// Token: 0x060010A6 RID: 4262
		public abstract void ComboBoxDrawNormalDropDownButton(ComboBox comboBox, Graphics g, Rectangle clippingArea, Rectangle area, ButtonState state);

		// Token: 0x060010A7 RID: 4263
		public abstract bool ComboBoxNormalDropDownButtonHasTransparentBackground(ComboBox comboBox, ButtonState state);

		// Token: 0x060010A8 RID: 4264
		public abstract bool ComboBoxDropDownButtonHasHotElementStyle(ComboBox comboBox);

		// Token: 0x060010A9 RID: 4265
		public abstract void ComboBoxDrawBackground(ComboBox comboBox, Graphics g, Rectangle clippingArea, FlatStyle style);

		// Token: 0x060010AA RID: 4266
		public abstract bool CombBoxBackgroundHasHotElementStyle(ComboBox comboBox);

		// Token: 0x060010AB RID: 4267
		public abstract void DrawGroupBox(Graphics dc, Rectangle clip_area, GroupBox box);

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060010AC RID: 4268
		public abstract Size GroupBoxDefaultSize { get; }

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060010AD RID: 4269
		public abstract Size HScrollBarDefaultSize { get; }

		// Token: 0x060010AE RID: 4270
		public abstract void DrawListViewItems(Graphics dc, Rectangle clip_rectangle, ListView control);

		// Token: 0x060010AF RID: 4271
		public abstract void DrawListViewHeader(Graphics dc, Rectangle clip_rectangle, ListView control);

		// Token: 0x060010B0 RID: 4272
		public abstract void DrawListViewHeaderDragDetails(Graphics dc, ListView control, ColumnHeader drag_column, int target_x);

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060010B1 RID: 4273
		public abstract bool ListViewHasHotHeaderStyle { get; }

		// Token: 0x060010B2 RID: 4274
		public abstract int ListViewGetHeaderHeight(ListView listView, Font font);

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060010B3 RID: 4275
		public abstract Size ListViewCheckBoxSize { get; }

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060010B4 RID: 4276
		public abstract int ListViewDefaultColumnWidth { get; }

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060010B5 RID: 4277
		public abstract int ListViewVerticalSpacing { get; }

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060010B6 RID: 4278
		public abstract int ListViewEmptyColumnWidth { get; }

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060010B7 RID: 4279
		public abstract int ListViewHorizontalSpacing { get; }

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060010B8 RID: 4280
		public abstract Size ListViewDefaultSize { get; }

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060010B9 RID: 4281
		public abstract int ListViewItemPaddingWidth { get; }

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060010BA RID: 4282
		public abstract int ListViewTileWidthFactor { get; }

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060010BB RID: 4283
		public abstract int ListViewTileHeightFactor { get; }

		// Token: 0x060010BC RID: 4284
		public abstract void CalcItemSize(Graphics dc, MenuItem item, int y, int x, bool menuBar);

		// Token: 0x060010BD RID: 4285
		public abstract void CalcPopupMenuSize(Graphics dc, Menu menu);

		// Token: 0x060010BE RID: 4286
		public abstract int CalcMenuBarSize(Graphics dc, Menu menu, int width);

		// Token: 0x060010BF RID: 4287
		public abstract void DrawMenuBar(Graphics dc, Menu menu, Rectangle rect);

		// Token: 0x060010C0 RID: 4288
		public abstract void DrawMenuItem(MenuItem item, DrawItemEventArgs e);

		// Token: 0x060010C1 RID: 4289
		public abstract void DrawPopupMenu(Graphics dc, Menu menu, Rectangle cliparea, Rectangle rect);

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060010C2 RID: 4290
		public abstract Size PanelDefaultSize { get; }

		// Token: 0x060010C3 RID: 4291
		public abstract void DrawPictureBox(Graphics dc, Rectangle clip, PictureBox pb);

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060010C4 RID: 4292
		public abstract Size PictureBoxDefaultSize { get; }

		// Token: 0x060010C5 RID: 4293
		public abstract void DrawScrollBar(Graphics dc, Rectangle clip_rectangle, ScrollBar bar);

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060010C6 RID: 4294
		public abstract int ScrollBarButtonSize { get; }

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060010C7 RID: 4295
		public abstract bool ScrollBarHasHotElementStyles { get; }

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060010C8 RID: 4296
		public abstract bool ScrollBarHasPressedThumbStyle { get; }

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060010C9 RID: 4297
		public abstract bool ScrollBarHasHoverArrowButtonStyle { get; }

		// Token: 0x060010CA RID: 4298
		public abstract void TextBoxBaseFillBackground(TextBoxBase textBoxBase, Graphics g, Rectangle clippingArea);

		// Token: 0x060010CB RID: 4299
		public abstract bool TextBoxBaseHandleWmNcPaint(TextBoxBase textBoxBase, ref Message m);

		// Token: 0x060010CC RID: 4300
		public abstract bool TextBoxBaseShouldPaintBackground(TextBoxBase textBoxBase);

		// Token: 0x060010CD RID: 4301
		public abstract void DrawToolBar(Graphics dc, Rectangle clip_rectangle, ToolBar control);

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060010CE RID: 4302
		public abstract int ToolBarGripWidth { get; }

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060010CF RID: 4303
		public abstract int ToolBarImageGripWidth { get; }

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060010D0 RID: 4304
		public abstract int ToolBarSeparatorWidth { get; }

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060010D1 RID: 4305
		public abstract int ToolBarDropDownWidth { get; }

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060010D2 RID: 4306
		public abstract int ToolBarDropDownArrowWidth { get; }

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060010D3 RID: 4307
		public abstract int ToolBarDropDownArrowHeight { get; }

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060010D4 RID: 4308
		public abstract Size ToolBarDefaultSize { get; }

		// Token: 0x060010D5 RID: 4309
		public abstract bool ToolBarHasHotElementStyles(ToolBar toolBar);

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060010D6 RID: 4310
		public abstract bool ToolBarHasHotCheckedElementStyles { get; }

		// Token: 0x060010D7 RID: 4311
		public abstract void DrawToolTip(Graphics dc, Rectangle clip_rectangle, ToolTip.ToolTipWindow control);

		// Token: 0x060010D8 RID: 4312
		public abstract Size ToolTipSize(ToolTip.ToolTipWindow tt, string text);

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060010D9 RID: 4313
		public abstract bool ToolTipTransparentBackground { get; }

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060010DA RID: 4314
		public abstract Size VScrollBarDefaultSize { get; }

		// Token: 0x060010DB RID: 4315
		public abstract void TreeViewDrawNodePlusMinus(TreeView treeView, TreeNode node, Graphics dc, int x, int middle);

		// Token: 0x060010DC RID: 4316
		public abstract void DrawManagedWindowDecorations(Graphics dc, Rectangle clip, InternalWindowManager wm);

		// Token: 0x060010DD RID: 4317
		public abstract int ManagedWindowTitleBarHeight(InternalWindowManager wm);

		// Token: 0x060010DE RID: 4318
		public abstract int ManagedWindowBorderWidth(InternalWindowManager wm);

		// Token: 0x060010DF RID: 4319
		public abstract Size ManagedWindowButtonSize(InternalWindowManager wm);

		// Token: 0x060010E0 RID: 4320
		public abstract void ManagedWindowSetButtonLocations(InternalWindowManager wm);

		// Token: 0x060010E1 RID: 4321
		public abstract Rectangle ManagedWindowGetTitleBarIconArea(InternalWindowManager wm);

		// Token: 0x060010E2 RID: 4322
		public abstract Size ManagedWindowGetMenuButtonSize(InternalWindowManager wm);

		// Token: 0x060010E3 RID: 4323
		public abstract bool ManagedWindowTitleButtonHasHotElementStyle(TitleButton button, Form form);

		// Token: 0x060010E4 RID: 4324
		public abstract void ManagedWindowDrawMenuButton(Graphics dc, TitleButton button, Rectangle clip, InternalWindowManager wm);

		// Token: 0x060010E5 RID: 4325
		public abstract void ManagedWindowOnSizeInitializedOrChanged(Form form);

		// Token: 0x060010E6 RID: 4326
		public abstract void CPDrawBorder(Graphics graphics, Rectangle bounds, Color leftColor, int leftWidth, ButtonBorderStyle leftStyle, Color topColor, int topWidth, ButtonBorderStyle topStyle, Color rightColor, int rightWidth, ButtonBorderStyle rightStyle, Color bottomColor, int bottomWidth, ButtonBorderStyle bottomStyle);

		// Token: 0x060010E7 RID: 4327
		public abstract void CPDrawBorder3D(Graphics graphics, Rectangle rectangle, Border3DStyle style, Border3DSide sides);

		// Token: 0x060010E8 RID: 4328
		public abstract void CPDrawBorder3D(Graphics graphics, Rectangle rectangle, Border3DStyle style, Border3DSide sides, Color control_color);

		// Token: 0x060010E9 RID: 4329
		public abstract void CPDrawButton(Graphics graphics, Rectangle rectangle, ButtonState state);

		// Token: 0x060010EA RID: 4330
		public abstract void CPDrawCaptionButton(Graphics graphics, Rectangle rectangle, CaptionButton button, ButtonState state);

		// Token: 0x060010EB RID: 4331
		public abstract void CPDrawCheckBox(Graphics graphics, Rectangle rectangle, ButtonState state);

		// Token: 0x060010EC RID: 4332
		public abstract void CPDrawComboButton(Graphics graphics, Rectangle rectangle, ButtonState state);

		// Token: 0x060010ED RID: 4333
		public abstract void CPDrawFocusRectangle(Graphics graphics, Rectangle rectangle, Color foreColor, Color backColor);

		// Token: 0x060010EE RID: 4334
		public abstract void CPDrawImageDisabled(Graphics graphics, Image image, int x, int y, Color background);

		// Token: 0x060010EF RID: 4335
		public abstract void CPDrawMenuGlyph(Graphics graphics, Rectangle rectangle, MenuGlyph glyph, Color color, Color backColor);

		// Token: 0x060010F0 RID: 4336
		public abstract void CPDrawMixedCheckBox(Graphics graphics, Rectangle rectangle, ButtonState state);

		// Token: 0x060010F1 RID: 4337
		public abstract void CPDrawScrollButton(Graphics graphics, Rectangle rectangle, ScrollButton button, ButtonState state);

		// Token: 0x060010F2 RID: 4338
		public abstract void CPDrawSizeGrip(Graphics graphics, Color backColor, Rectangle bounds);

		// Token: 0x060010F3 RID: 4339
		public abstract void CPDrawStringDisabled(Graphics graphics, string s, Font font, Color color, RectangleF layoutRectangle, StringFormat format);

		// Token: 0x04000B26 RID: 2854
		private Font default_font;

		// Token: 0x04000B27 RID: 2855
		protected Color defaultWindowBackColor;

		// Token: 0x04000B28 RID: 2856
		protected Color defaultWindowForeColor;

		// Token: 0x04000B29 RID: 2857
		internal SystemResPool ResPool = new SystemResPool();

		// Token: 0x04000B2A RID: 2858
		private MethodInfo update;
	}
}
