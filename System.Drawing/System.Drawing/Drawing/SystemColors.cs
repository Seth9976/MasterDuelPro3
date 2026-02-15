using System;

namespace System.Drawing
{
	/// <summary>Each property of the <see cref="T:System.Drawing.SystemColors" /> class is a <see cref="T:System.Drawing.Color" /> structure that is the color of a Windows display element.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200001C RID: 28
	public static class SystemColors
	{
		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the active window's border.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the active window's border.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00004775 File Offset: 0x00002975
		public static Color ActiveBorder
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ActiveBorder);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of the active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the active window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000477D File Offset: 0x0000297D
		public static Color ActiveCaption
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ActiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the text in the active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the text in the active window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00004785 File Offset: 0x00002985
		public static Color ActiveCaptionText
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ActiveCaptionText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the application workspace. </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the application workspace.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000478D File Offset: 0x0000298D
		public static Color AppWorkspace
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.AppWorkspace);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the face color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the face color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00004795 File Offset: 0x00002995
		public static Color ButtonFace
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ButtonFace);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the highlight color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the highlight color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000047A1 File Offset: 0x000029A1
		public static Color ButtonHighlight
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ButtonHighlight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the shadow color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the shadow color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000047AD File Offset: 0x000029AD
		public static Color ButtonShadow
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ButtonShadow);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the face color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the face color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000047B9 File Offset: 0x000029B9
		public static Color Control
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.Control);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the shadow color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the shadow color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000047C1 File Offset: 0x000029C1
		public static Color ControlDark
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ControlDark);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the dark shadow color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the dark shadow color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000047C9 File Offset: 0x000029C9
		public static Color ControlDarkDark
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ControlDarkDark);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the light color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the light color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000047D1 File Offset: 0x000029D1
		public static Color ControlLight
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ControlLight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the highlight color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the highlight color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000047D9 File Offset: 0x000029D9
		public static Color ControlLightLight
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ControlLightLight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of text in a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of text in a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000047E2 File Offset: 0x000029E2
		public static Color ControlText
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ControlText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the desktop.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the desktop.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000047EB File Offset: 0x000029EB
		public static Color Desktop
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.Desktop);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the lightest color in the color gradient of an active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the lightest color in the color gradient of an active window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000047F4 File Offset: 0x000029F4
		public static Color GradientActiveCaption
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.GradientActiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the lightest color in the color gradient of an inactive window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the lightest color in the color gradient of an inactive window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00004800 File Offset: 0x00002A00
		public static Color GradientInactiveCaption
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.GradientInactiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of dimmed text. </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of dimmed text.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000076 RID: 118 RVA: 0x0000480C File Offset: 0x00002A0C
		public static Color GrayText
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.GrayText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of selected items.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background of selected items.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00004815 File Offset: 0x00002A15
		public static Color Highlight
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.Highlight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the text of selected items.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the text of selected items.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000481E File Offset: 0x00002A1E
		public static Color HighlightText
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.HighlightText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color used to designate a hot-tracked item. </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color used to designate a hot-tracked item.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00004827 File Offset: 0x00002A27
		public static Color HotTrack
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.HotTrack);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of an inactive window's border.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of an inactive window's border.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00004830 File Offset: 0x00002A30
		public static Color InactiveBorder
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.InactiveBorder);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of an inactive window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background of an inactive window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00004839 File Offset: 0x00002A39
		public static Color InactiveCaption
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.InactiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the text in an inactive window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the text in an inactive window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00004842 File Offset: 0x00002A42
		public static Color InactiveCaptionText
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.InactiveCaptionText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of a ToolTip.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background of a ToolTip.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000484B File Offset: 0x00002A4B
		public static Color Info
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.Info);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the text of a ToolTip.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the text of a ToolTip.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00004854 File Offset: 0x00002A54
		public static Color InfoText
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.InfoText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of a menu's background.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of a menu's background.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000485D File Offset: 0x00002A5D
		public static Color Menu
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.Menu);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of a menu bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background of a menu bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00004866 File Offset: 0x00002A66
		public static Color MenuBar
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.MenuBar);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color used to highlight menu items when the menu appears as a flat menu.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color used to highlight menu items when the menu appears as a flat menu.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00004872 File Offset: 0x00002A72
		public static Color MenuHighlight
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.MenuHighlight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of a menu's text.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of a menu's text.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000487E File Offset: 0x00002A7E
		public static Color MenuText
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.MenuText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of a scroll bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background of a scroll bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00004887 File Offset: 0x00002A87
		public static Color ScrollBar
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ScrollBar);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background in the client area of a window.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background in the client area of a window.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00004890 File Offset: 0x00002A90
		public static Color Window
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.Window);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of a window frame.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of a window frame.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00004899 File Offset: 0x00002A99
		public static Color WindowFrame
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.WindowFrame);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the text in the client area of a window.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the text in the client area of a window.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000048A2 File Offset: 0x00002AA2
		public static Color WindowText
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.WindowText);
			}
		}
	}
}
