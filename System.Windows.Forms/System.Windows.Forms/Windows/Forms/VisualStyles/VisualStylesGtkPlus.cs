using System;
using System.Drawing;

namespace System.Windows.Forms.VisualStyles
{
	// Token: 0x0200036E RID: 878
	internal class VisualStylesGtkPlus : IVisualStyles
	{
		// Token: 0x06001CB9 RID: 7353 RVA: 0x000873AD File Offset: 0x000855AD
		public static bool Initialize()
		{
			return GtkPlus.Initialize();
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x000873B4 File Offset: 0x000855B4
		private static GtkPlus GtkPlus
		{
			get
			{
				return GtkPlus.Instance;
			}
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x00002D70 File Offset: 0x00000F70
		public int UxThemeCloseThemeData(IntPtr hTheme)
		{
			return 0;
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x000873BB File Offset: 0x000855BB
		public int UxThemeDrawThemeBackground(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds, Rectangle clipRectangle)
		{
			if (!this.DrawBackground((VisualStylesGtkPlus.ThemeHandle)(int)hTheme, dc, iPartId, iStateId, bounds, clipRectangle, Rectangle.Empty))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x000873DB File Offset: 0x000855DB
		public int UxThemeDrawThemeBackground(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds)
		{
			return this.UxThemeDrawThemeBackground(hTheme, dc, iPartId, iStateId, bounds, bounds);
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x000873EC File Offset: 0x000855EC
		private bool DrawBackground(VisualStylesGtkPlus.ThemeHandle themeHandle, IDeviceContext dc, int part, int state, Rectangle bounds, Rectangle clipRectangle, Rectangle excludedArea)
		{
			switch (themeHandle)
			{
			case VisualStylesGtkPlus.ThemeHandle.BUTTON:
				switch (part)
				{
				case 1:
				{
					GtkPlusState gtkPlusState;
					switch (state)
					{
					case 1:
						gtkPlusState = GtkPlusState.Normal;
						break;
					case 2:
						gtkPlusState = GtkPlusState.Hot;
						break;
					case 3:
						gtkPlusState = GtkPlusState.Pressed;
						break;
					case 4:
						gtkPlusState = GtkPlusState.Disabled;
						break;
					case 5:
						gtkPlusState = GtkPlusState.Normal;
						break;
					default:
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.ButtonPaint(dc, bounds, clipRectangle, state == 5, gtkPlusState);
					return true;
				}
				case 2:
				{
					GtkPlusState gtkPlusState;
					GtkPlusToggleButtonValue gtkPlusToggleButtonValue;
					switch (state)
					{
					case 1:
						gtkPlusState = GtkPlusState.Normal;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Unchecked;
						break;
					case 2:
						gtkPlusState = GtkPlusState.Hot;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Unchecked;
						break;
					case 3:
						gtkPlusState = GtkPlusState.Pressed;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Unchecked;
						break;
					case 4:
						gtkPlusState = GtkPlusState.Disabled;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Unchecked;
						break;
					case 5:
						gtkPlusState = GtkPlusState.Normal;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Checked;
						break;
					case 6:
						gtkPlusState = GtkPlusState.Hot;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Checked;
						break;
					case 7:
						gtkPlusState = GtkPlusState.Pressed;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Checked;
						break;
					case 8:
						gtkPlusState = GtkPlusState.Disabled;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Checked;
						break;
					default:
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.RadioButtonPaint(dc, bounds, clipRectangle, gtkPlusState, gtkPlusToggleButtonValue);
					return true;
				}
				case 3:
				{
					GtkPlusState gtkPlusState;
					GtkPlusToggleButtonValue gtkPlusToggleButtonValue;
					switch (state)
					{
					case 1:
						gtkPlusState = GtkPlusState.Normal;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Unchecked;
						break;
					case 2:
						gtkPlusState = GtkPlusState.Hot;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Unchecked;
						break;
					case 3:
						gtkPlusState = GtkPlusState.Pressed;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Unchecked;
						break;
					case 4:
						gtkPlusState = GtkPlusState.Disabled;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Unchecked;
						break;
					case 5:
						gtkPlusState = GtkPlusState.Normal;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Checked;
						break;
					case 6:
						gtkPlusState = GtkPlusState.Hot;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Checked;
						break;
					case 7:
						gtkPlusState = GtkPlusState.Pressed;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Checked;
						break;
					case 8:
						gtkPlusState = GtkPlusState.Disabled;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Checked;
						break;
					case 9:
						gtkPlusState = GtkPlusState.Normal;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Mixed;
						break;
					case 10:
						gtkPlusState = GtkPlusState.Hot;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Mixed;
						break;
					case 11:
						gtkPlusState = GtkPlusState.Pressed;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Mixed;
						break;
					case 12:
						gtkPlusState = GtkPlusState.Disabled;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Mixed;
						break;
					default:
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.CheckBoxPaint(dc, bounds, clipRectangle, gtkPlusState, gtkPlusToggleButtonValue);
					return true;
				}
				case 4:
				{
					GtkPlusState gtkPlusState;
					if (state != 1)
					{
						if (state != 2)
						{
							return false;
						}
						gtkPlusState = GtkPlusState.Disabled;
					}
					else
					{
						gtkPlusState = GtkPlusState.Normal;
					}
					VisualStylesGtkPlus.GtkPlus.GroupBoxPaint(dc, bounds, excludedArea, gtkPlusState);
					return true;
				}
				default:
					return false;
				}
				break;
			case VisualStylesGtkPlus.ThemeHandle.COMBOBOX:
				if (part == 1)
				{
					GtkPlusState gtkPlusState;
					switch (state)
					{
					case 1:
						gtkPlusState = GtkPlusState.Normal;
						break;
					case 2:
						gtkPlusState = GtkPlusState.Hot;
						break;
					case 3:
						gtkPlusState = GtkPlusState.Pressed;
						break;
					case 4:
						gtkPlusState = GtkPlusState.Disabled;
						break;
					default:
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.ComboBoxPaintDropDownButton(dc, bounds, clipRectangle, gtkPlusState);
					return true;
				}
				if (part != 4)
				{
					return false;
				}
				if (state - 1 <= 3)
				{
					VisualStylesGtkPlus.GtkPlus.ComboBoxPaintBorder(dc, bounds, clipRectangle);
					return true;
				}
				return false;
			case VisualStylesGtkPlus.ThemeHandle.EDIT:
				if (part == 1)
				{
					GtkPlusState gtkPlusState;
					switch (state)
					{
					case 1:
					case 2:
					case 3:
					case 5:
					case 6:
					case 7:
						gtkPlusState = GtkPlusState.Normal;
						break;
					case 4:
						gtkPlusState = GtkPlusState.Disabled;
						break;
					default:
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.TextBoxPaint(dc, bounds, excludedArea, gtkPlusState);
					return true;
				}
				return false;
			case VisualStylesGtkPlus.ThemeHandle.HEADER:
				if (part == 1)
				{
					GtkPlusState gtkPlusState;
					switch (state)
					{
					case 1:
						gtkPlusState = GtkPlusState.Normal;
						break;
					case 2:
						gtkPlusState = GtkPlusState.Hot;
						break;
					case 3:
						gtkPlusState = GtkPlusState.Pressed;
						break;
					default:
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.HeaderPaint(dc, bounds, clipRectangle, gtkPlusState);
					return true;
				}
				return false;
			case VisualStylesGtkPlus.ThemeHandle.PROGRESS:
				if (part - 1 <= 1)
				{
					VisualStylesGtkPlus.GtkPlus.ProgressBarPaintBar(dc, bounds, clipRectangle);
					return true;
				}
				if (part - 3 > 1)
				{
					return false;
				}
				VisualStylesGtkPlus.GtkPlus.ProgressBarPaintChunk(dc, bounds, clipRectangle);
				return true;
			case VisualStylesGtkPlus.ThemeHandle.REBAR:
				if (part == 3)
				{
					VisualStylesGtkPlus.GtkPlus.ToolBarPaint(dc, bounds, clipRectangle);
					return true;
				}
				return false;
			case VisualStylesGtkPlus.ThemeHandle.SCROLLBAR:
				switch (part)
				{
				case 1:
				{
					GtkPlusState gtkPlusState;
					bool flag;
					bool flag2;
					switch (state)
					{
					case 1:
						gtkPlusState = GtkPlusState.Normal;
						flag = false;
						flag2 = true;
						break;
					case 2:
						gtkPlusState = GtkPlusState.Hot;
						flag = false;
						flag2 = true;
						break;
					case 3:
						gtkPlusState = GtkPlusState.Pressed;
						flag = false;
						flag2 = true;
						break;
					case 4:
						gtkPlusState = GtkPlusState.Disabled;
						flag = false;
						flag2 = true;
						break;
					case 5:
						gtkPlusState = GtkPlusState.Normal;
						flag = false;
						flag2 = false;
						break;
					case 6:
						gtkPlusState = GtkPlusState.Hot;
						flag = false;
						flag2 = false;
						break;
					case 7:
						gtkPlusState = GtkPlusState.Pressed;
						flag = false;
						flag2 = false;
						break;
					case 8:
						gtkPlusState = GtkPlusState.Disabled;
						flag = false;
						flag2 = false;
						break;
					case 9:
						gtkPlusState = GtkPlusState.Normal;
						flag = true;
						flag2 = true;
						break;
					case 10:
						gtkPlusState = GtkPlusState.Hot;
						flag = true;
						flag2 = true;
						break;
					case 11:
						gtkPlusState = GtkPlusState.Pressed;
						flag = true;
						flag2 = true;
						break;
					case 12:
						gtkPlusState = GtkPlusState.Disabled;
						flag = true;
						flag2 = true;
						break;
					case 13:
						gtkPlusState = GtkPlusState.Normal;
						flag = true;
						flag2 = false;
						break;
					case 14:
						gtkPlusState = GtkPlusState.Hot;
						flag = true;
						flag2 = false;
						break;
					case 15:
						gtkPlusState = GtkPlusState.Pressed;
						flag = true;
						flag2 = false;
						break;
					case 16:
						gtkPlusState = GtkPlusState.Disabled;
						flag = true;
						flag2 = false;
						break;
					default:
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.ScrollBarPaintArrowButton(dc, bounds, clipRectangle, gtkPlusState, flag, flag2);
					return true;
				}
				case 2:
				case 3:
				{
					GtkPlusState gtkPlusState;
					if (!VisualStylesGtkPlus.GetGtkPlusState((SCROLLBARSTYLESTATES)state, out gtkPlusState))
					{
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.ScrollBarPaintThumbButton(dc, bounds, clipRectangle, gtkPlusState, part == 2);
					return true;
				}
				case 4:
				case 5:
				case 6:
				case 7:
				{
					GtkPlusState gtkPlusState;
					if (!VisualStylesGtkPlus.GetGtkPlusState((SCROLLBARSTYLESTATES)state, out gtkPlusState))
					{
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.ScrollBarPaintTrack(dc, bounds, clipRectangle, gtkPlusState, part == 4 || part == 5, part == 5 || part == 7);
					return true;
				}
				default:
					return false;
				}
				break;
			case VisualStylesGtkPlus.ThemeHandle.SPIN:
			{
				GtkPlusState gtkPlusState;
				bool flag3;
				if (part != 1)
				{
					if (part != 2)
					{
						return false;
					}
					flag3 = false;
					switch (state)
					{
					case 1:
						gtkPlusState = GtkPlusState.Normal;
						break;
					case 2:
						gtkPlusState = GtkPlusState.Hot;
						break;
					case 3:
						gtkPlusState = GtkPlusState.Pressed;
						break;
					case 4:
						gtkPlusState = GtkPlusState.Disabled;
						break;
					default:
						return false;
					}
				}
				else
				{
					flag3 = true;
					switch (state)
					{
					case 1:
						gtkPlusState = GtkPlusState.Normal;
						break;
					case 2:
						gtkPlusState = GtkPlusState.Hot;
						break;
					case 3:
						gtkPlusState = GtkPlusState.Pressed;
						break;
					case 4:
						gtkPlusState = GtkPlusState.Disabled;
						break;
					default:
						return false;
					}
				}
				VisualStylesGtkPlus.GtkPlus.UpDownPaint(dc, bounds, clipRectangle, flag3, gtkPlusState);
				return true;
			}
			case VisualStylesGtkPlus.ThemeHandle.STATUS:
				if (part == 3)
				{
					VisualStylesGtkPlus.GtkPlus.StatusBarPaintGripper(dc, bounds, clipRectangle);
					return true;
				}
				return false;
			case VisualStylesGtkPlus.ThemeHandle.TAB:
			{
				bool flag4;
				switch (part)
				{
				case 1:
					switch (state)
					{
					case 1:
					case 2:
					case 4:
						flag4 = false;
						break;
					case 3:
						flag4 = true;
						break;
					default:
						return false;
					}
					break;
				case 2:
					switch (state)
					{
					case 1:
					case 2:
					case 4:
						flag4 = false;
						break;
					case 3:
						flag4 = true;
						break;
					default:
						return false;
					}
					break;
				case 3:
					switch (state)
					{
					case 1:
					case 2:
					case 4:
						flag4 = false;
						break;
					case 3:
						flag4 = true;
						break;
					default:
						return false;
					}
					break;
				case 4:
					flag4 = false;
					break;
				case 5:
					switch (state)
					{
					case 1:
					case 2:
					case 4:
						flag4 = false;
						break;
					case 3:
						flag4 = true;
						break;
					default:
						return false;
					}
					break;
				case 6:
					switch (state)
					{
					case 1:
					case 2:
					case 4:
						flag4 = false;
						break;
					case 3:
						flag4 = true;
						break;
					default:
						return false;
					}
					break;
				case 7:
					switch (state)
					{
					case 1:
					case 2:
					case 4:
						flag4 = false;
						break;
					case 3:
						flag4 = true;
						break;
					default:
						return false;
					}
					break;
				case 8:
					flag4 = false;
					break;
				case 9:
					VisualStylesGtkPlus.GtkPlus.TabControlPaintPane(dc, bounds, clipRectangle);
					return true;
				default:
					return false;
				}
				VisualStylesGtkPlus.GtkPlus.TabControlPaintTabItem(dc, bounds, clipRectangle, flag4 ? GtkPlusState.Pressed : GtkPlusState.Normal);
				return true;
			}
			case VisualStylesGtkPlus.ThemeHandle.TOOLBAR:
				if (part == 1)
				{
					GtkPlusState gtkPlusState;
					switch (state)
					{
					case 1:
						gtkPlusState = GtkPlusState.Normal;
						break;
					case 2:
						gtkPlusState = GtkPlusState.Hot;
						break;
					case 3:
						gtkPlusState = GtkPlusState.Pressed;
						break;
					case 4:
						gtkPlusState = GtkPlusState.Disabled;
						break;
					case 5:
					case 6:
						VisualStylesGtkPlus.GtkPlus.ToolBarPaintCheckedButton(dc, bounds, clipRectangle);
						return true;
					default:
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.ToolBarPaintButton(dc, bounds, clipRectangle, gtkPlusState);
					return true;
				}
				return false;
			case VisualStylesGtkPlus.ThemeHandle.TRACKBAR:
				switch (part)
				{
				case 1:
					if (state == 1)
					{
						VisualStylesGtkPlus.GtkPlus.TrackBarPaintTrack(dc, bounds, clipRectangle, true);
						return true;
					}
					return false;
				case 2:
					if (state == 1)
					{
						VisualStylesGtkPlus.GtkPlus.TrackBarPaintTrack(dc, bounds, clipRectangle, false);
						return true;
					}
					return false;
				case 3:
				{
					GtkPlusState gtkPlusState;
					switch (state)
					{
					case 1:
						gtkPlusState = GtkPlusState.Normal;
						break;
					case 2:
						gtkPlusState = GtkPlusState.Hot;
						break;
					case 3:
						gtkPlusState = GtkPlusState.Pressed;
						break;
					case 4:
						gtkPlusState = GtkPlusState.Selected;
						break;
					case 5:
						gtkPlusState = GtkPlusState.Disabled;
						break;
					default:
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.TrackBarPaintThumb(dc, bounds, clipRectangle, gtkPlusState, true);
					return true;
				}
				case 6:
				{
					GtkPlusState gtkPlusState;
					switch (state)
					{
					case 1:
						gtkPlusState = GtkPlusState.Normal;
						break;
					case 2:
						gtkPlusState = GtkPlusState.Hot;
						break;
					case 3:
						gtkPlusState = GtkPlusState.Pressed;
						break;
					case 4:
						gtkPlusState = GtkPlusState.Selected;
						break;
					case 5:
						gtkPlusState = GtkPlusState.Disabled;
						break;
					default:
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.TrackBarPaintThumb(dc, bounds, clipRectangle, gtkPlusState, false);
					return true;
				}
				}
				return false;
			case VisualStylesGtkPlus.ThemeHandle.TREEVIEW:
				if (part == 2)
				{
					bool flag5;
					if (state != 1)
					{
						if (state != 2)
						{
							return false;
						}
						flag5 = false;
					}
					else
					{
						flag5 = true;
					}
					VisualStylesGtkPlus.GtkPlus.TreeViewPaintGlyph(dc, bounds, clipRectangle, flag5);
					return true;
				}
				return false;
			default:
				return false;
			}
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x00087C92 File Offset: 0x00085E92
		private static bool GetGtkPlusState(SCROLLBARSTYLESTATES state, out GtkPlusState result)
		{
			switch (state)
			{
			case SCROLLBARSTYLESTATES.SCRBS_NORMAL:
				result = GtkPlusState.Normal;
				break;
			case SCROLLBARSTYLESTATES.SCRBS_HOT:
				result = GtkPlusState.Hot;
				break;
			case SCROLLBARSTYLESTATES.SCRBS_PRESSED:
				result = GtkPlusState.Pressed;
				break;
			case SCROLLBARSTYLESTATES.SCRBS_DISABLED:
				result = GtkPlusState.Disabled;
				break;
			default:
				result = GtkPlusState.Normal;
				return false;
			}
			return true;
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x00006F54 File Offset: 0x00005154
		public int UxThemeDrawThemeText(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, string text, TextFormatFlags textFlags, Rectangle bounds)
		{
			return 1;
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x00087CC8 File Offset: 0x00085EC8
		public int UxThemeGetThemeBackgroundRegion(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds, out Region result)
		{
			result = null;
			return 1;
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x00087CCF File Offset: 0x00085ECF
		public int UxThemeGetThemeColor(IntPtr hTheme, int iPartId, int iStateId, ColorProperty prop, out Color result)
		{
			result = Color.Black;
			return 1;
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x00087CE0 File Offset: 0x00085EE0
		public int UxThemeGetThemePartSize(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, ThemeSizeType type, out Size result)
		{
			if (!this.GetPartSize((VisualStylesGtkPlus.ThemeHandle)(int)hTheme, dc, iPartId, iStateId, Rectangle.Empty, false, type, out result))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x00087D0C File Offset: 0x00085F0C
		private bool GetPartSize(VisualStylesGtkPlus.ThemeHandle themeHandle, IDeviceContext dc, int part, int state, Rectangle bounds, bool rectangleSpecified, ThemeSizeType type, out Size result)
		{
			if (themeHandle != VisualStylesGtkPlus.ThemeHandle.BUTTON)
			{
				if (themeHandle != VisualStylesGtkPlus.ThemeHandle.HEADER)
				{
					if (themeHandle == VisualStylesGtkPlus.ThemeHandle.TRACKBAR)
					{
						switch (part)
						{
						case 1:
							result = new Size(0, 4);
							return true;
						case 2:
							result = new Size(4, 0);
							return true;
						case 3:
						case 6:
							result = ThemeWin32Classic.TrackBarGetThumbSize();
							if (part == 6)
							{
								int width = result.Width;
								result.Width = result.Height;
								result.Height = width;
							}
							return true;
						}
					}
				}
				else if (part == 1)
				{
					result = new Size(0, ThemeWin32Classic.ListViewGetHeaderHeight());
					return true;
				}
			}
			else
			{
				if (part == 2)
				{
					result = VisualStylesGtkPlus.GtkPlus.RadioButtonGetSize();
					return true;
				}
				if (part == 3)
				{
					result = VisualStylesGtkPlus.GtkPlus.CheckBoxGetSize();
					return true;
				}
			}
			result = Size.Empty;
			return false;
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x00087DFE File Offset: 0x00085FFE
		public int UxThemeGetThemeTextExtent(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, string textToDraw, TextFormatFlags flags, Rectangle bounds, out Rectangle result)
		{
			result = Rectangle.Empty;
			return 1;
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x00006F54 File Offset: 0x00005154
		public bool UxThemeIsAppThemed()
		{
			return true;
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x00006F54 File Offset: 0x00005154
		public bool UxThemeIsThemeActive()
		{
			return true;
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x00006F54 File Offset: 0x00005154
		public bool UxThemeIsThemeBackgroundPartiallyTransparent(IntPtr hTheme, int iPartId, int iStateId)
		{
			return true;
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x00087E10 File Offset: 0x00086010
		public bool UxThemeIsThemePartDefined(IntPtr hTheme, int iPartId)
		{
			switch ((int)hTheme)
			{
			case 1:
				return iPartId - 1 <= 3;
			case 2:
				return iPartId == 1 || iPartId == 4;
			case 3:
				return iPartId == 1;
			case 4:
				return iPartId == 1;
			case 5:
				return iPartId - 1 <= 3;
			case 6:
				return iPartId == 3;
			case 7:
				return iPartId - 1 <= 6;
			case 8:
				return iPartId - 1 <= 1;
			case 9:
				return iPartId == 3;
			case 10:
				return iPartId - 1 <= 8;
			case 11:
				return iPartId == 1;
			case 12:
				return iPartId - 1 <= 2 || iPartId == 6;
			case 13:
				return iPartId == 2;
			default:
				return false;
			}
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x00087F14 File Offset: 0x00086114
		public IntPtr UxThemeOpenThemeData(IntPtr hWnd, string classList)
		{
			VisualStylesGtkPlus.ThemeHandle themeHandle;
			try
			{
				themeHandle = (VisualStylesGtkPlus.ThemeHandle)Enum.Parse(typeof(VisualStylesGtkPlus.ThemeHandle), classList);
			}
			catch (ArgumentException)
			{
				return IntPtr.Zero;
			}
			return (IntPtr)((int)themeHandle);
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001CCB RID: 7371 RVA: 0x00003C7A File Offset: 0x00001E7A
		public string VisualStyleInformationColorScheme
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001CCC RID: 7372 RVA: 0x00003C7A File Offset: 0x00001E7A
		public string VisualStyleInformationFileName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001CCD RID: 7373 RVA: 0x00006F54 File Offset: 0x00005154
		public bool VisualStyleInformationIsSupportedByOS
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x00087F5C File Offset: 0x0008615C
		public void VisualStyleRendererDrawBackgroundExcludingArea(IntPtr theme, IDeviceContext dc, int part, int state, Rectangle bounds, Rectangle excludedArea)
		{
			this.DrawBackground((VisualStylesGtkPlus.ThemeHandle)(int)theme, dc, part, state, bounds, bounds, excludedArea);
		}

		// Token: 0x0200036F RID: 879
		private enum ThemeHandle
		{
			// Token: 0x04001829 RID: 6185
			BUTTON = 1,
			// Token: 0x0400182A RID: 6186
			COMBOBOX,
			// Token: 0x0400182B RID: 6187
			EDIT,
			// Token: 0x0400182C RID: 6188
			HEADER,
			// Token: 0x0400182D RID: 6189
			PROGRESS,
			// Token: 0x0400182E RID: 6190
			REBAR,
			// Token: 0x0400182F RID: 6191
			SCROLLBAR,
			// Token: 0x04001830 RID: 6192
			SPIN,
			// Token: 0x04001831 RID: 6193
			STATUS,
			// Token: 0x04001832 RID: 6194
			TAB,
			// Token: 0x04001833 RID: 6195
			TOOLBAR,
			// Token: 0x04001834 RID: 6196
			TRACKBAR,
			// Token: 0x04001835 RID: 6197
			TREEVIEW
		}
	}
}
