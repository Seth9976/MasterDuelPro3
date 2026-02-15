using System;

namespace System.Drawing
{
	/// <summary>Specifies the known system colors.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000009 RID: 9
	public enum KnownColor
	{
		/// <summary>The system-defined color of the active window's border.</summary>
		// Token: 0x04000005 RID: 5
		ActiveBorder = 1,
		/// <summary>The system-defined color of the background of the active window's title bar.</summary>
		// Token: 0x04000006 RID: 6
		ActiveCaption,
		/// <summary>The system-defined color of the text in the active window's title bar.</summary>
		// Token: 0x04000007 RID: 7
		ActiveCaptionText,
		/// <summary>The system-defined color of the application workspace. The application workspace is the area in a multiple-document view that is not being occupied by documents.</summary>
		// Token: 0x04000008 RID: 8
		AppWorkspace,
		/// <summary>The system-defined face color of a 3-D element.</summary>
		// Token: 0x04000009 RID: 9
		Control,
		/// <summary>The system-defined shadow color of a 3-D element. The shadow color is applied to parts of a 3-D element that face away from the light source.</summary>
		// Token: 0x0400000A RID: 10
		ControlDark,
		/// <summary>The system-defined color that is the dark shadow color of a 3-D element. The dark shadow color is applied to the parts of a 3-D element that are the darkest color.</summary>
		// Token: 0x0400000B RID: 11
		ControlDarkDark,
		/// <summary>The system-defined color that is the light color of a 3-D element. The light color is applied to parts of a 3-D element that face the light source.</summary>
		// Token: 0x0400000C RID: 12
		ControlLight,
		/// <summary>The system-defined highlight color of a 3-D element. The highlight color is applied to the parts of a 3-D element that are the lightest color.</summary>
		// Token: 0x0400000D RID: 13
		ControlLightLight,
		/// <summary>The system-defined color of text in a 3-D element.</summary>
		// Token: 0x0400000E RID: 14
		ControlText,
		/// <summary>The system-defined color of the desktop.</summary>
		// Token: 0x0400000F RID: 15
		Desktop,
		/// <summary>The system-defined color of dimmed text. Items in a list that are disabled are displayed in dimmed text.</summary>
		// Token: 0x04000010 RID: 16
		GrayText,
		/// <summary>The system-defined color of the background of selected items. This includes selected menu items as well as selected text. </summary>
		// Token: 0x04000011 RID: 17
		Highlight,
		/// <summary>The system-defined color of the text of selected items.</summary>
		// Token: 0x04000012 RID: 18
		HighlightText,
		/// <summary>The system-defined color used to designate a hot-tracked item. Single-clicking a hot-tracked item executes the item.</summary>
		// Token: 0x04000013 RID: 19
		HotTrack,
		/// <summary>The system-defined color of an inactive window's border.</summary>
		// Token: 0x04000014 RID: 20
		InactiveBorder,
		/// <summary>The system-defined color of the background of an inactive window's title bar.</summary>
		// Token: 0x04000015 RID: 21
		InactiveCaption,
		/// <summary>The system-defined color of the text in an inactive window's title bar.</summary>
		// Token: 0x04000016 RID: 22
		InactiveCaptionText,
		/// <summary>The system-defined color of the background of a ToolTip.</summary>
		// Token: 0x04000017 RID: 23
		Info,
		/// <summary>The system-defined color of the text of a ToolTip.</summary>
		// Token: 0x04000018 RID: 24
		InfoText,
		/// <summary>The system-defined color of a menu's background.</summary>
		// Token: 0x04000019 RID: 25
		Menu,
		/// <summary>The system-defined color of a menu's text.</summary>
		// Token: 0x0400001A RID: 26
		MenuText,
		/// <summary>The system-defined color of the background of a scroll bar.</summary>
		// Token: 0x0400001B RID: 27
		ScrollBar,
		/// <summary>The system-defined color of the background in the client area of a window.</summary>
		// Token: 0x0400001C RID: 28
		Window,
		/// <summary>The system-defined color of a window frame.</summary>
		// Token: 0x0400001D RID: 29
		WindowFrame,
		/// <summary>The system-defined color of the text in the client area of a window.</summary>
		// Token: 0x0400001E RID: 30
		WindowText,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400001F RID: 31
		Transparent,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000020 RID: 32
		AliceBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000021 RID: 33
		AntiqueWhite,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000022 RID: 34
		Aqua,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000023 RID: 35
		Aquamarine,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000024 RID: 36
		Azure,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000025 RID: 37
		Beige,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000026 RID: 38
		Bisque,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000027 RID: 39
		Black,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000028 RID: 40
		BlanchedAlmond,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000029 RID: 41
		Blue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400002A RID: 42
		BlueViolet,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400002B RID: 43
		Brown,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400002C RID: 44
		BurlyWood,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400002D RID: 45
		CadetBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400002E RID: 46
		Chartreuse,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400002F RID: 47
		Chocolate,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000030 RID: 48
		Coral,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000031 RID: 49
		CornflowerBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000032 RID: 50
		Cornsilk,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000033 RID: 51
		Crimson,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000034 RID: 52
		Cyan,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000035 RID: 53
		DarkBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000036 RID: 54
		DarkCyan,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000037 RID: 55
		DarkGoldenrod,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000038 RID: 56
		DarkGray,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000039 RID: 57
		DarkGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400003A RID: 58
		DarkKhaki,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400003B RID: 59
		DarkMagenta,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400003C RID: 60
		DarkOliveGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400003D RID: 61
		DarkOrange,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400003E RID: 62
		DarkOrchid,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400003F RID: 63
		DarkRed,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000040 RID: 64
		DarkSalmon,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000041 RID: 65
		DarkSeaGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000042 RID: 66
		DarkSlateBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000043 RID: 67
		DarkSlateGray,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000044 RID: 68
		DarkTurquoise,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000045 RID: 69
		DarkViolet,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000046 RID: 70
		DeepPink,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000047 RID: 71
		DeepSkyBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000048 RID: 72
		DimGray,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000049 RID: 73
		DodgerBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400004A RID: 74
		Firebrick,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400004B RID: 75
		FloralWhite,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400004C RID: 76
		ForestGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400004D RID: 77
		Fuchsia,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400004E RID: 78
		Gainsboro,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400004F RID: 79
		GhostWhite,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000050 RID: 80
		Gold,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000051 RID: 81
		Goldenrod,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000052 RID: 82
		Gray,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000053 RID: 83
		Green,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000054 RID: 84
		GreenYellow,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000055 RID: 85
		Honeydew,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000056 RID: 86
		HotPink,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000057 RID: 87
		IndianRed,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000058 RID: 88
		Indigo,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000059 RID: 89
		Ivory,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400005A RID: 90
		Khaki,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400005B RID: 91
		Lavender,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400005C RID: 92
		LavenderBlush,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400005D RID: 93
		LawnGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400005E RID: 94
		LemonChiffon,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400005F RID: 95
		LightBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000060 RID: 96
		LightCoral,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000061 RID: 97
		LightCyan,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000062 RID: 98
		LightGoldenrodYellow,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000063 RID: 99
		LightGray,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000064 RID: 100
		LightGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000065 RID: 101
		LightPink,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000066 RID: 102
		LightSalmon,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000067 RID: 103
		LightSeaGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000068 RID: 104
		LightSkyBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000069 RID: 105
		LightSlateGray,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400006A RID: 106
		LightSteelBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400006B RID: 107
		LightYellow,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400006C RID: 108
		Lime,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400006D RID: 109
		LimeGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400006E RID: 110
		Linen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400006F RID: 111
		Magenta,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000070 RID: 112
		Maroon,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000071 RID: 113
		MediumAquamarine,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000072 RID: 114
		MediumBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000073 RID: 115
		MediumOrchid,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000074 RID: 116
		MediumPurple,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000075 RID: 117
		MediumSeaGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000076 RID: 118
		MediumSlateBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000077 RID: 119
		MediumSpringGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000078 RID: 120
		MediumTurquoise,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000079 RID: 121
		MediumVioletRed,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400007A RID: 122
		MidnightBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400007B RID: 123
		MintCream,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400007C RID: 124
		MistyRose,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400007D RID: 125
		Moccasin,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400007E RID: 126
		NavajoWhite,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400007F RID: 127
		Navy,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000080 RID: 128
		OldLace,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000081 RID: 129
		Olive,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000082 RID: 130
		OliveDrab,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000083 RID: 131
		Orange,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000084 RID: 132
		OrangeRed,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000085 RID: 133
		Orchid,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000086 RID: 134
		PaleGoldenrod,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000087 RID: 135
		PaleGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000088 RID: 136
		PaleTurquoise,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000089 RID: 137
		PaleVioletRed,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400008A RID: 138
		PapayaWhip,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400008B RID: 139
		PeachPuff,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400008C RID: 140
		Peru,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400008D RID: 141
		Pink,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400008E RID: 142
		Plum,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400008F RID: 143
		PowderBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000090 RID: 144
		Purple,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000091 RID: 145
		Red,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000092 RID: 146
		RosyBrown,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000093 RID: 147
		RoyalBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000094 RID: 148
		SaddleBrown,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000095 RID: 149
		Salmon,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000096 RID: 150
		SandyBrown,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000097 RID: 151
		SeaGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000098 RID: 152
		SeaShell,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000099 RID: 153
		Sienna,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400009A RID: 154
		Silver,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400009B RID: 155
		SkyBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400009C RID: 156
		SlateBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400009D RID: 157
		SlateGray,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400009E RID: 158
		Snow,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400009F RID: 159
		SpringGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000A0 RID: 160
		SteelBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000A1 RID: 161
		Tan,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000A2 RID: 162
		Teal,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000A3 RID: 163
		Thistle,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000A4 RID: 164
		Tomato,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000A5 RID: 165
		Turquoise,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000A6 RID: 166
		Violet,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000A7 RID: 167
		Wheat,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000A8 RID: 168
		White,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000A9 RID: 169
		WhiteSmoke,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000AA RID: 170
		Yellow,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000AB RID: 171
		YellowGreen,
		/// <summary>The system-defined face color of a 3-D element.</summary>
		// Token: 0x040000AC RID: 172
		ButtonFace,
		/// <summary>The system-defined color that is the highlight color of a 3-D element. This color is applied to parts of a 3-D element that face the light source.</summary>
		// Token: 0x040000AD RID: 173
		ButtonHighlight,
		/// <summary>The system-defined color that is the shadow color of a 3-D element. This color is applied to parts of a 3-D element that face away from the light source.</summary>
		// Token: 0x040000AE RID: 174
		ButtonShadow,
		/// <summary>The system-defined color of the lightest color in the color gradient of an active window's title bar.</summary>
		// Token: 0x040000AF RID: 175
		GradientActiveCaption,
		/// <summary>The system-defined color of the lightest color in the color gradient of an inactive window's title bar. </summary>
		// Token: 0x040000B0 RID: 176
		GradientInactiveCaption,
		/// <summary>The system-defined color of the background of a menu bar.</summary>
		// Token: 0x040000B1 RID: 177
		MenuBar,
		/// <summary>The system-defined color used to highlight menu items when the menu appears as a flat menu.</summary>
		// Token: 0x040000B2 RID: 178
		MenuHighlight
	}
}
