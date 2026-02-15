using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.Numerics.Hashing;

namespace System.Drawing
{
	/// <summary>Represents an ARGB (alpha, red, green, blue) color.</summary>
	/// <filterpriority>1</filterpriority>
	/// <completionlist cref="T:System.Drawing.Color" />
	// Token: 0x0200001E RID: 30
	[TypeConverter(typeof(ColorConverter))]
	[Editor("System.Drawing.Design.ColorEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[DebuggerDisplay("{NameAndARGBValue}")]
	[Serializable]
	public readonly struct Color : IEquatable<Color>
	{
		/// <summary>Gets a system-defined color.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00004951 File Offset: 0x00002B51
		public static Color Transparent
		{
			get
			{
				return new Color(KnownColor.Transparent);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF0F8FF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000495A File Offset: 0x00002B5A
		public static Color AliceBlue
		{
			get
			{
				return new Color(KnownColor.AliceBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFAEBD7.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00004963 File Offset: 0x00002B63
		public static Color AntiqueWhite
		{
			get
			{
				return new Color(KnownColor.AntiqueWhite);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00FFFF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000496C File Offset: 0x00002B6C
		public static Color Aqua
		{
			get
			{
				return new Color(KnownColor.Aqua);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF7FFFD4.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00004975 File Offset: 0x00002B75
		public static Color Aquamarine
		{
			get
			{
				return new Color(KnownColor.Aquamarine);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF0FFFF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000497E File Offset: 0x00002B7E
		public static Color Azure
		{
			get
			{
				return new Color(KnownColor.Azure);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF5F5DC.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00004987 File Offset: 0x00002B87
		public static Color Beige
		{
			get
			{
				return new Color(KnownColor.Beige);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFE4C4.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004990 File Offset: 0x00002B90
		public static Color Bisque
		{
			get
			{
				return new Color(KnownColor.Bisque);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF000000.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00004999 File Offset: 0x00002B99
		public static Color Black
		{
			get
			{
				return new Color(KnownColor.Black);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFEBCD.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000049A2 File Offset: 0x00002BA2
		public static Color BlanchedAlmond
		{
			get
			{
				return new Color(KnownColor.BlanchedAlmond);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF0000FF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000049AB File Offset: 0x00002BAB
		public static Color Blue
		{
			get
			{
				return new Color(KnownColor.Blue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF8A2BE2.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000049B4 File Offset: 0x00002BB4
		public static Color BlueViolet
		{
			get
			{
				return new Color(KnownColor.BlueViolet);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFA52A2A.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000049BD File Offset: 0x00002BBD
		public static Color Brown
		{
			get
			{
				return new Color(KnownColor.Brown);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFDEB887.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000049C6 File Offset: 0x00002BC6
		public static Color BurlyWood
		{
			get
			{
				return new Color(KnownColor.BurlyWood);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF5F9EA0.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000049CF File Offset: 0x00002BCF
		public static Color CadetBlue
		{
			get
			{
				return new Color(KnownColor.CadetBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF7FFF00.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000049D8 File Offset: 0x00002BD8
		public static Color Chartreuse
		{
			get
			{
				return new Color(KnownColor.Chartreuse);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFD2691E.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000049E1 File Offset: 0x00002BE1
		public static Color Chocolate
		{
			get
			{
				return new Color(KnownColor.Chocolate);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF7F50.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000049EA File Offset: 0x00002BEA
		public static Color Coral
		{
			get
			{
				return new Color(KnownColor.Coral);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF6495ED.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000049F3 File Offset: 0x00002BF3
		public static Color CornflowerBlue
		{
			get
			{
				return new Color(KnownColor.CornflowerBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFF8DC.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000049FC File Offset: 0x00002BFC
		public static Color Cornsilk
		{
			get
			{
				return new Color(KnownColor.Cornsilk);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFDC143C.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00004A05 File Offset: 0x00002C05
		public static Color Crimson
		{
			get
			{
				return new Color(KnownColor.Crimson);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00FFFF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004A0E File Offset: 0x00002C0E
		public static Color Cyan
		{
			get
			{
				return new Color(KnownColor.Cyan);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00008B.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004A17 File Offset: 0x00002C17
		public static Color DarkBlue
		{
			get
			{
				return new Color(KnownColor.DarkBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF008B8B.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004A20 File Offset: 0x00002C20
		public static Color DarkCyan
		{
			get
			{
				return new Color(KnownColor.DarkCyan);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFB8860B.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004A29 File Offset: 0x00002C29
		public static Color DarkGoldenrod
		{
			get
			{
				return new Color(KnownColor.DarkGoldenrod);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFA9A9A9.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004A32 File Offset: 0x00002C32
		public static Color DarkGray
		{
			get
			{
				return new Color(KnownColor.DarkGray);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF006400.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00004A3B File Offset: 0x00002C3B
		public static Color DarkGreen
		{
			get
			{
				return new Color(KnownColor.DarkGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFBDB76B.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00004A44 File Offset: 0x00002C44
		public static Color DarkKhaki
		{
			get
			{
				return new Color(KnownColor.DarkKhaki);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF8B008B.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00004A4D File Offset: 0x00002C4D
		public static Color DarkMagenta
		{
			get
			{
				return new Color(KnownColor.DarkMagenta);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF556B2F.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00004A56 File Offset: 0x00002C56
		public static Color DarkOliveGreen
		{
			get
			{
				return new Color(KnownColor.DarkOliveGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF8C00.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00004A5F File Offset: 0x00002C5F
		public static Color DarkOrange
		{
			get
			{
				return new Color(KnownColor.DarkOrange);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF9932CC.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004A68 File Offset: 0x00002C68
		public static Color DarkOrchid
		{
			get
			{
				return new Color(KnownColor.DarkOrchid);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF8B0000.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004A71 File Offset: 0x00002C71
		public static Color DarkRed
		{
			get
			{
				return new Color(KnownColor.DarkRed);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFE9967A.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004A7A File Offset: 0x00002C7A
		public static Color DarkSalmon
		{
			get
			{
				return new Color(KnownColor.DarkSalmon);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF8FBC8F.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00004A83 File Offset: 0x00002C83
		public static Color DarkSeaGreen
		{
			get
			{
				return new Color(KnownColor.DarkSeaGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF483D8B.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004A8C File Offset: 0x00002C8C
		public static Color DarkSlateBlue
		{
			get
			{
				return new Color(KnownColor.DarkSlateBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF2F4F4F.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00004A95 File Offset: 0x00002C95
		public static Color DarkSlateGray
		{
			get
			{
				return new Color(KnownColor.DarkSlateGray);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00CED1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004A9E File Offset: 0x00002C9E
		public static Color DarkTurquoise
		{
			get
			{
				return new Color(KnownColor.DarkTurquoise);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF9400D3.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004AA7 File Offset: 0x00002CA7
		public static Color DarkViolet
		{
			get
			{
				return new Color(KnownColor.DarkViolet);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF1493.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00004AB0 File Offset: 0x00002CB0
		public static Color DeepPink
		{
			get
			{
				return new Color(KnownColor.DeepPink);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00BFFF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004AB9 File Offset: 0x00002CB9
		public static Color DeepSkyBlue
		{
			get
			{
				return new Color(KnownColor.DeepSkyBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF696969.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004AC2 File Offset: 0x00002CC2
		public static Color DimGray
		{
			get
			{
				return new Color(KnownColor.DimGray);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF1E90FF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004ACB File Offset: 0x00002CCB
		public static Color DodgerBlue
		{
			get
			{
				return new Color(KnownColor.DodgerBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFB22222.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004AD4 File Offset: 0x00002CD4
		public static Color Firebrick
		{
			get
			{
				return new Color(KnownColor.Firebrick);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFAF0.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004ADD File Offset: 0x00002CDD
		public static Color FloralWhite
		{
			get
			{
				return new Color(KnownColor.FloralWhite);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF228B22.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004AE6 File Offset: 0x00002CE6
		public static Color ForestGreen
		{
			get
			{
				return new Color(KnownColor.ForestGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF00FF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004AEF File Offset: 0x00002CEF
		public static Color Fuchsia
		{
			get
			{
				return new Color(KnownColor.Fuchsia);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFDCDCDC.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004AF8 File Offset: 0x00002CF8
		public static Color Gainsboro
		{
			get
			{
				return new Color(KnownColor.Gainsboro);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF8F8FF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00004B01 File Offset: 0x00002D01
		public static Color GhostWhite
		{
			get
			{
				return new Color(KnownColor.GhostWhite);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFD700.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004B0A File Offset: 0x00002D0A
		public static Color Gold
		{
			get
			{
				return new Color(KnownColor.Gold);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFDAA520.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004B13 File Offset: 0x00002D13
		public static Color Goldenrod
		{
			get
			{
				return new Color(KnownColor.Goldenrod);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF808080.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> strcture representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004B1C File Offset: 0x00002D1C
		public static Color Gray
		{
			get
			{
				return new Color(KnownColor.Gray);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF008000.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004B25 File Offset: 0x00002D25
		public static Color Green
		{
			get
			{
				return new Color(KnownColor.Green);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFADFF2F.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004B2E File Offset: 0x00002D2E
		public static Color GreenYellow
		{
			get
			{
				return new Color(KnownColor.GreenYellow);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF0FFF0.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00004B37 File Offset: 0x00002D37
		public static Color Honeydew
		{
			get
			{
				return new Color(KnownColor.Honeydew);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF69B4.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004B40 File Offset: 0x00002D40
		public static Color HotPink
		{
			get
			{
				return new Color(KnownColor.HotPink);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFCD5C5C.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004B49 File Offset: 0x00002D49
		public static Color IndianRed
		{
			get
			{
				return new Color(KnownColor.IndianRed);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF4B0082.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004B52 File Offset: 0x00002D52
		public static Color Indigo
		{
			get
			{
				return new Color(KnownColor.Indigo);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFFF0.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004B5B File Offset: 0x00002D5B
		public static Color Ivory
		{
			get
			{
				return new Color(KnownColor.Ivory);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF0E68C.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004B64 File Offset: 0x00002D64
		public static Color Khaki
		{
			get
			{
				return new Color(KnownColor.Khaki);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFE6E6FA.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004B6D File Offset: 0x00002D6D
		public static Color Lavender
		{
			get
			{
				return new Color(KnownColor.Lavender);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFF0F5.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004B76 File Offset: 0x00002D76
		public static Color LavenderBlush
		{
			get
			{
				return new Color(KnownColor.LavenderBlush);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF7CFC00.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004B7F File Offset: 0x00002D7F
		public static Color LawnGreen
		{
			get
			{
				return new Color(KnownColor.LawnGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFACD.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004B88 File Offset: 0x00002D88
		public static Color LemonChiffon
		{
			get
			{
				return new Color(KnownColor.LemonChiffon);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFADD8E6.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00004B91 File Offset: 0x00002D91
		public static Color LightBlue
		{
			get
			{
				return new Color(KnownColor.LightBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF08080.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004B9A File Offset: 0x00002D9A
		public static Color LightCoral
		{
			get
			{
				return new Color(KnownColor.LightCoral);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFE0FFFF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004BA3 File Offset: 0x00002DA3
		public static Color LightCyan
		{
			get
			{
				return new Color(KnownColor.LightCyan);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFAFAD2.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004BAC File Offset: 0x00002DAC
		public static Color LightGoldenrodYellow
		{
			get
			{
				return new Color(KnownColor.LightGoldenrodYellow);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF90EE90.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004BB5 File Offset: 0x00002DB5
		public static Color LightGreen
		{
			get
			{
				return new Color(KnownColor.LightGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFD3D3D3.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004BBE File Offset: 0x00002DBE
		public static Color LightGray
		{
			get
			{
				return new Color(KnownColor.LightGray);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFB6C1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004BC7 File Offset: 0x00002DC7
		public static Color LightPink
		{
			get
			{
				return new Color(KnownColor.LightPink);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFA07A.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004BD0 File Offset: 0x00002DD0
		public static Color LightSalmon
		{
			get
			{
				return new Color(KnownColor.LightSalmon);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF20B2AA.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004BD9 File Offset: 0x00002DD9
		public static Color LightSeaGreen
		{
			get
			{
				return new Color(KnownColor.LightSeaGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF87CEFA.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004BE2 File Offset: 0x00002DE2
		public static Color LightSkyBlue
		{
			get
			{
				return new Color(KnownColor.LightSkyBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF778899.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004BEB File Offset: 0x00002DEB
		public static Color LightSlateGray
		{
			get
			{
				return new Color(KnownColor.LightSlateGray);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFB0C4DE.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004BF4 File Offset: 0x00002DF4
		public static Color LightSteelBlue
		{
			get
			{
				return new Color(KnownColor.LightSteelBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFFE0.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004BFD File Offset: 0x00002DFD
		public static Color LightYellow
		{
			get
			{
				return new Color(KnownColor.LightYellow);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00FF00.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004C06 File Offset: 0x00002E06
		public static Color Lime
		{
			get
			{
				return new Color(KnownColor.Lime);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF32CD32.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004C0F File Offset: 0x00002E0F
		public static Color LimeGreen
		{
			get
			{
				return new Color(KnownColor.LimeGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFAF0E6.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004C18 File Offset: 0x00002E18
		public static Color Linen
		{
			get
			{
				return new Color(KnownColor.Linen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF00FF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004C21 File Offset: 0x00002E21
		public static Color Magenta
		{
			get
			{
				return new Color(KnownColor.Magenta);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF800000.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004C2A File Offset: 0x00002E2A
		public static Color Maroon
		{
			get
			{
				return new Color(KnownColor.Maroon);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF66CDAA.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004C33 File Offset: 0x00002E33
		public static Color MediumAquamarine
		{
			get
			{
				return new Color(KnownColor.MediumAquamarine);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF0000CD.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00004C3C File Offset: 0x00002E3C
		public static Color MediumBlue
		{
			get
			{
				return new Color(KnownColor.MediumBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFBA55D3.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004C45 File Offset: 0x00002E45
		public static Color MediumOrchid
		{
			get
			{
				return new Color(KnownColor.MediumOrchid);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF9370DB.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004C4E File Offset: 0x00002E4E
		public static Color MediumPurple
		{
			get
			{
				return new Color(KnownColor.MediumPurple);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF3CB371.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004C57 File Offset: 0x00002E57
		public static Color MediumSeaGreen
		{
			get
			{
				return new Color(KnownColor.MediumSeaGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF7B68EE.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004C60 File Offset: 0x00002E60
		public static Color MediumSlateBlue
		{
			get
			{
				return new Color(KnownColor.MediumSlateBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00FA9A.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004C69 File Offset: 0x00002E69
		public static Color MediumSpringGreen
		{
			get
			{
				return new Color(KnownColor.MediumSpringGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF48D1CC.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004C72 File Offset: 0x00002E72
		public static Color MediumTurquoise
		{
			get
			{
				return new Color(KnownColor.MediumTurquoise);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFC71585.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004C7B File Offset: 0x00002E7B
		public static Color MediumVioletRed
		{
			get
			{
				return new Color(KnownColor.MediumVioletRed);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF191970.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004C84 File Offset: 0x00002E84
		public static Color MidnightBlue
		{
			get
			{
				return new Color(KnownColor.MidnightBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF5FFFA.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004C8D File Offset: 0x00002E8D
		public static Color MintCream
		{
			get
			{
				return new Color(KnownColor.MintCream);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFE4E1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004C96 File Offset: 0x00002E96
		public static Color MistyRose
		{
			get
			{
				return new Color(KnownColor.MistyRose);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFE4B5.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004C9F File Offset: 0x00002E9F
		public static Color Moccasin
		{
			get
			{
				return new Color(KnownColor.Moccasin);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFDEAD.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004CA8 File Offset: 0x00002EA8
		public static Color NavajoWhite
		{
			get
			{
				return new Color(KnownColor.NavajoWhite);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF000080.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004CB1 File Offset: 0x00002EB1
		public static Color Navy
		{
			get
			{
				return new Color(KnownColor.Navy);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFDF5E6.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004CBA File Offset: 0x00002EBA
		public static Color OldLace
		{
			get
			{
				return new Color(KnownColor.OldLace);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF808000.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004CC3 File Offset: 0x00002EC3
		public static Color Olive
		{
			get
			{
				return new Color(KnownColor.Olive);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF6B8E23.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004CCC File Offset: 0x00002ECC
		public static Color OliveDrab
		{
			get
			{
				return new Color(KnownColor.OliveDrab);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFA500.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004CD5 File Offset: 0x00002ED5
		public static Color Orange
		{
			get
			{
				return new Color(KnownColor.Orange);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF4500.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004CDE File Offset: 0x00002EDE
		public static Color OrangeRed
		{
			get
			{
				return new Color(KnownColor.OrangeRed);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFDA70D6.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004CEA File Offset: 0x00002EEA
		public static Color Orchid
		{
			get
			{
				return new Color(KnownColor.Orchid);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFEEE8AA.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00004CF6 File Offset: 0x00002EF6
		public static Color PaleGoldenrod
		{
			get
			{
				return new Color(KnownColor.PaleGoldenrod);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF98FB98.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004D02 File Offset: 0x00002F02
		public static Color PaleGreen
		{
			get
			{
				return new Color(KnownColor.PaleGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFAFEEEE.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00004D0E File Offset: 0x00002F0E
		public static Color PaleTurquoise
		{
			get
			{
				return new Color(KnownColor.PaleTurquoise);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFDB7093.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004D1A File Offset: 0x00002F1A
		public static Color PaleVioletRed
		{
			get
			{
				return new Color(KnownColor.PaleVioletRed);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFEFD5.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00004D26 File Offset: 0x00002F26
		public static Color PapayaWhip
		{
			get
			{
				return new Color(KnownColor.PapayaWhip);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFDAB9.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004D32 File Offset: 0x00002F32
		public static Color PeachPuff
		{
			get
			{
				return new Color(KnownColor.PeachPuff);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFCD853F.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00004D3E File Offset: 0x00002F3E
		public static Color Peru
		{
			get
			{
				return new Color(KnownColor.Peru);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFC0CB.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004D4A File Offset: 0x00002F4A
		public static Color Pink
		{
			get
			{
				return new Color(KnownColor.Pink);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFDDA0DD.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004D56 File Offset: 0x00002F56
		public static Color Plum
		{
			get
			{
				return new Color(KnownColor.Plum);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFB0E0E6.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00004D62 File Offset: 0x00002F62
		public static Color PowderBlue
		{
			get
			{
				return new Color(KnownColor.PowderBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF800080.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004D6E File Offset: 0x00002F6E
		public static Color Purple
		{
			get
			{
				return new Color(KnownColor.Purple);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF0000.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00004D7A File Offset: 0x00002F7A
		public static Color Red
		{
			get
			{
				return new Color(KnownColor.Red);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFBC8F8F.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00004D86 File Offset: 0x00002F86
		public static Color RosyBrown
		{
			get
			{
				return new Color(KnownColor.RosyBrown);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF4169E1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004D92 File Offset: 0x00002F92
		public static Color RoyalBlue
		{
			get
			{
				return new Color(KnownColor.RoyalBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF8B4513.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00004D9E File Offset: 0x00002F9E
		public static Color SaddleBrown
		{
			get
			{
				return new Color(KnownColor.SaddleBrown);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFA8072.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004DAA File Offset: 0x00002FAA
		public static Color Salmon
		{
			get
			{
				return new Color(KnownColor.Salmon);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF4A460.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00004DB6 File Offset: 0x00002FB6
		public static Color SandyBrown
		{
			get
			{
				return new Color(KnownColor.SandyBrown);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF2E8B57.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00004DC2 File Offset: 0x00002FC2
		public static Color SeaGreen
		{
			get
			{
				return new Color(KnownColor.SeaGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFF5EE.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004DCE File Offset: 0x00002FCE
		public static Color SeaShell
		{
			get
			{
				return new Color(KnownColor.SeaShell);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFA0522D.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004DDA File Offset: 0x00002FDA
		public static Color Sienna
		{
			get
			{
				return new Color(KnownColor.Sienna);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFC0C0C0.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00004DE6 File Offset: 0x00002FE6
		public static Color Silver
		{
			get
			{
				return new Color(KnownColor.Silver);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF87CEEB.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00004DF2 File Offset: 0x00002FF2
		public static Color SkyBlue
		{
			get
			{
				return new Color(KnownColor.SkyBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF6A5ACD.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00004DFE File Offset: 0x00002FFE
		public static Color SlateBlue
		{
			get
			{
				return new Color(KnownColor.SlateBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF708090.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00004E0A File Offset: 0x0000300A
		public static Color SlateGray
		{
			get
			{
				return new Color(KnownColor.SlateGray);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFAFA.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00004E16 File Offset: 0x00003016
		public static Color Snow
		{
			get
			{
				return new Color(KnownColor.Snow);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00FF7F.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00004E22 File Offset: 0x00003022
		public static Color SpringGreen
		{
			get
			{
				return new Color(KnownColor.SpringGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF4682B4.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00004E2E File Offset: 0x0000302E
		public static Color SteelBlue
		{
			get
			{
				return new Color(KnownColor.SteelBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFD2B48C.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00004E3A File Offset: 0x0000303A
		public static Color Tan
		{
			get
			{
				return new Color(KnownColor.Tan);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF008080.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00004E46 File Offset: 0x00003046
		public static Color Teal
		{
			get
			{
				return new Color(KnownColor.Teal);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFD8BFD8.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00004E52 File Offset: 0x00003052
		public static Color Thistle
		{
			get
			{
				return new Color(KnownColor.Thistle);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF6347.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00004E5E File Offset: 0x0000305E
		public static Color Tomato
		{
			get
			{
				return new Color(KnownColor.Tomato);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF40E0D0.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00004E6A File Offset: 0x0000306A
		public static Color Turquoise
		{
			get
			{
				return new Color(KnownColor.Turquoise);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFEE82EE.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00004E76 File Offset: 0x00003076
		public static Color Violet
		{
			get
			{
				return new Color(KnownColor.Violet);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF5DEB3.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00004E82 File Offset: 0x00003082
		public static Color Wheat
		{
			get
			{
				return new Color(KnownColor.Wheat);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFFFF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00004E8E File Offset: 0x0000308E
		public static Color White
		{
			get
			{
				return new Color(KnownColor.White);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF5F5F5.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00004E9A File Offset: 0x0000309A
		public static Color WhiteSmoke
		{
			get
			{
				return new Color(KnownColor.WhiteSmoke);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFF00.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00004EA6 File Offset: 0x000030A6
		public static Color Yellow
		{
			get
			{
				return new Color(KnownColor.Yellow);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF9ACD32.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00004EB2 File Offset: 0x000030B2
		public static Color YellowGreen
		{
			get
			{
				return new Color(KnownColor.YellowGreen);
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004EBE File Offset: 0x000030BE
		internal Color(KnownColor knownColor)
		{
			this.value = 0L;
			this.state = 1;
			this.name = null;
			this.knownColor = (short)knownColor;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004EDE File Offset: 0x000030DE
		private Color(long value, short state, string name, KnownColor knownColor)
		{
			this.value = value;
			this.state = state;
			this.name = name;
			this.knownColor = (short)knownColor;
		}

		/// <summary>Gets the red component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The red component value of this <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00004EFE File Offset: 0x000030FE
		public byte R
		{
			get
			{
				return (byte)((this.Value >> 16) & 255L);
			}
		}

		/// <summary>Gets the green component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The green component value of this <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00004F11 File Offset: 0x00003111
		public byte G
		{
			get
			{
				return (byte)((this.Value >> 8) & 255L);
			}
		}

		/// <summary>Gets the blue component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The blue component value of this <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00004F23 File Offset: 0x00003123
		public byte B
		{
			get
			{
				return (byte)(this.Value & 255L);
			}
		}

		/// <summary>Gets the alpha component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The alpha component value of this <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00004F33 File Offset: 0x00003133
		public byte A
		{
			get
			{
				return (byte)((this.Value >> 24) & 255L);
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Color" /> structure is a predefined color. Predefined colors are represented by the elements of the <see cref="T:System.Drawing.KnownColor" /> enumeration.</summary>
		/// <returns>true if this <see cref="T:System.Drawing.Color" /> was created from a predefined color by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00004F46 File Offset: 0x00003146
		public bool IsKnownColor
		{
			get
			{
				return (this.state & 1) != 0;
			}
		}

		/// <summary>Specifies whether this <see cref="T:System.Drawing.Color" /> structure is uninitialized.</summary>
		/// <returns>This property returns true if this color is uninitialized; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00004F53 File Offset: 0x00003153
		public bool IsEmpty
		{
			get
			{
				return this.state == 0;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Color" /> structure is a named color or a member of the <see cref="T:System.Drawing.KnownColor" /> enumeration.</summary>
		/// <returns>true if this <see cref="T:System.Drawing.Color" /> was created by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00004F5E File Offset: 0x0000315E
		public bool IsNamedColor
		{
			get
			{
				return (this.state & 8) != 0 || this.IsKnownColor;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Color" /> structure is a system color. A system color is a color that is used in a Windows display element. System colors are represented by elements of the <see cref="T:System.Drawing.KnownColor" /> enumeration.</summary>
		/// <returns>true if this <see cref="T:System.Drawing.Color" /> was created from a system color by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00004F72 File Offset: 0x00003172
		public bool IsSystemColor
		{
			get
			{
				return this.IsKnownColor && (this.knownColor <= 26 || this.knownColor > 167);
			}
		}

		/// <summary>Gets the name of this <see cref="T:System.Drawing.Color" />.</summary>
		/// <returns>The name of this <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00004F97 File Offset: 0x00003197
		public string Name
		{
			get
			{
				if ((this.state & 8) != 0)
				{
					return this.name;
				}
				if (this.IsKnownColor)
				{
					return KnownColorTable.KnownColorToName((KnownColor)this.knownColor);
				}
				return Convert.ToString(this.value, 16);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00004FCB File Offset: 0x000031CB
		private long Value
		{
			get
			{
				if ((this.state & 2) != 0)
				{
					return this.value;
				}
				if (this.IsKnownColor)
				{
					return (long)KnownColorTable.KnownColorToArgb((KnownColor)this.knownColor);
				}
				return 0L;
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004FF8 File Offset: 0x000031F8
		private static void CheckByte(int value, string name)
		{
			if (value < 0 || value > 255)
			{
				throw new ArgumentException(SR.Format("Value of '{1}' is not valid for '{0}'. '{0}' should be greater than or equal to {2} and less than or equal to {3}.", new object[] { name, value, 0, 255 }));
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000504A File Offset: 0x0000324A
		private static long MakeArgb(byte alpha, byte red, byte green, byte blue)
		{
			return (long)((ulong)(((int)red << 16) | ((int)green << 8) | (int)blue | ((int)alpha << 24)) & (ulong)(-1));
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from a 32-bit ARGB value.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> structure that this method creates.</returns>
		/// <param name="argb">A value specifying the 32-bit ARGB value. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000125 RID: 293 RVA: 0x0000505F File Offset: 0x0000325F
		public static Color FromArgb(int argb)
		{
			return new Color((long)argb & (long)((ulong)(-1)), 2, null, (KnownColor)0);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the four ARGB component (alpha, red, green, and blue) values. Although this method allows a 32-bit value to be passed for each component, the value of each component is limited to 8 bits.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
		/// <param name="alpha">The alpha component. Valid values are 0 through 255. </param>
		/// <param name="red">The red component. Valid values are 0 through 255. </param>
		/// <param name="green">The green component. Valid values are 0 through 255. </param>
		/// <param name="blue">The blue component. Valid values are 0 through 255. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="alpha" />, <paramref name="red" />, <paramref name="green" />, or <paramref name="blue" /> is less than 0 or greater than 255.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000126 RID: 294 RVA: 0x00005070 File Offset: 0x00003270
		public static Color FromArgb(int alpha, int red, int green, int blue)
		{
			Color.CheckByte(alpha, "alpha");
			Color.CheckByte(red, "red");
			Color.CheckByte(green, "green");
			Color.CheckByte(blue, "blue");
			return new Color(Color.MakeArgb((byte)alpha, (byte)red, (byte)green, (byte)blue), 2, null, (KnownColor)0);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the specified 8-bit color values (red, green, and blue). The alpha value is implicitly 255 (fully opaque). Although this method allows a 32-bit value to be passed for each color component, the value of each component is limited to 8 bits.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
		/// <param name="red">The red component value for the new <see cref="T:System.Drawing.Color" />. Valid values are 0 through 255. </param>
		/// <param name="green">The green component value for the new <see cref="T:System.Drawing.Color" />. Valid values are 0 through 255. </param>
		/// <param name="blue">The blue component value for the new <see cref="T:System.Drawing.Color" />. Valid values are 0 through 255. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="red" />, <paramref name="green" />, or <paramref name="blue" /> is less than 0 or greater than 255.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000127 RID: 295 RVA: 0x000050BE File Offset: 0x000032BE
		public static Color FromArgb(int red, int green, int blue)
		{
			return Color.FromArgb(255, red, green, blue);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the specified predefined color.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
		/// <param name="color">An element of the <see cref="T:System.Drawing.KnownColor" /> enumeration. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000128 RID: 296 RVA: 0x000050CD File Offset: 0x000032CD
		public static Color FromKnownColor(KnownColor color)
		{
			if (color > (KnownColor)0 && color <= KnownColor.MenuHighlight)
			{
				return new Color(color);
			}
			return Color.FromName(color.ToString());
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the specified name of a predefined color.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
		/// <param name="name">A string that is the name of a predefined color. Valid names are the same as the names of the elements of the <see cref="T:System.Drawing.KnownColor" /> enumeration. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000129 RID: 297 RVA: 0x000050F4 File Offset: 0x000032F4
		public static Color FromName(string name)
		{
			Color color;
			if (ColorTable.TryGetNamedColor(name, out color))
			{
				return color;
			}
			return new Color(0L, 8, name, (KnownColor)0);
		}

		/// <summary>Gets the hue-saturation-brightness (HSB) brightness value for this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The brightness of this <see cref="T:System.Drawing.Color" />. The brightness ranges from 0.0 through 1.0, where 0.0 represents black and 1.0 represents white.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600012A RID: 298 RVA: 0x00005118 File Offset: 0x00003318
		public float GetBrightness()
		{
			float num = (float)this.R / 255f;
			float num2 = (float)this.G / 255f;
			float num3 = (float)this.B / 255f;
			float num4 = num;
			float num5 = num;
			if (num2 > num4)
			{
				num4 = num2;
			}
			else if (num2 < num5)
			{
				num5 = num2;
			}
			if (num3 > num4)
			{
				num4 = num3;
			}
			else if (num3 < num5)
			{
				num5 = num3;
			}
			return (num4 + num5) / 2f;
		}

		/// <summary>Gets the 32-bit ARGB value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The 32-bit ARGB value of this <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600012B RID: 299 RVA: 0x00005176 File Offset: 0x00003376
		public int ToArgb()
		{
			return (int)this.Value;
		}

		/// <summary>Gets the <see cref="T:System.Drawing.KnownColor" /> value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>An element of the <see cref="T:System.Drawing.KnownColor" /> enumeration, if the <see cref="T:System.Drawing.Color" /> is created from a predefined color by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600012C RID: 300 RVA: 0x0000517F File Offset: 0x0000337F
		public KnownColor ToKnownColor()
		{
			return (KnownColor)this.knownColor;
		}

		/// <summary>Converts this <see cref="T:System.Drawing.Color" /> structure to a human-readable string.</summary>
		/// <returns>A string that is the name of this <see cref="T:System.Drawing.Color" />, if the <see cref="T:System.Drawing.Color" /> is created from a predefined color by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, a string that consists of the ARGB component names and their values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600012D RID: 301 RVA: 0x00005188 File Offset: 0x00003388
		public override string ToString()
		{
			if ((this.state & 8) != 0 || (this.state & 1) != 0)
			{
				return "Color [" + this.Name + "]";
			}
			if ((this.state & 2) != 0)
			{
				return string.Concat(new string[]
				{
					"Color [A=",
					this.A.ToString(),
					", R=",
					this.R.ToString(),
					", G=",
					this.G.ToString(),
					", B=",
					this.B.ToString(),
					"]"
				});
			}
			return "Color [Empty]";
		}

		/// <summary>Tests whether two specified <see cref="T:System.Drawing.Color" /> structures are equivalent.</summary>
		/// <returns>true if the two <see cref="T:System.Drawing.Color" /> structures are equal; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.Drawing.Color" /> that is to the left of the equality operator. </param>
		/// <param name="right">The <see cref="T:System.Drawing.Color" /> that is to the right of the equality operator. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600012E RID: 302 RVA: 0x00005247 File Offset: 0x00003447
		public static bool operator ==(Color left, Color right)
		{
			return left.value == right.value && left.state == right.state && left.knownColor == right.knownColor && left.name == right.name;
		}

		/// <summary>Tests whether two specified <see cref="T:System.Drawing.Color" /> structures are different.</summary>
		/// <returns>true if the two <see cref="T:System.Drawing.Color" /> structures are different; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.Drawing.Color" /> that is to the left of the inequality operator. </param>
		/// <param name="right">The <see cref="T:System.Drawing.Color" /> that is to the right of the inequality operator. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600012F RID: 303 RVA: 0x00005286 File Offset: 0x00003486
		public static bool operator !=(Color left, Color right)
		{
			return !(left == right);
		}

		/// <summary>Tests whether the specified object is a <see cref="T:System.Drawing.Color" /> structure and is equivalent to this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.Drawing.Color" /> structure equivalent to this <see cref="T:System.Drawing.Color" /> structure; otherwise, false.</returns>
		/// <param name="obj">The object to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000130 RID: 304 RVA: 0x00005292 File Offset: 0x00003492
		public override bool Equals(object obj)
		{
			return obj is Color && this.Equals((Color)obj);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000052AA File Offset: 0x000034AA
		public bool Equals(Color other)
		{
			return this == other;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>An integer value that specifies the hash code for this <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000132 RID: 306 RVA: 0x000052B8 File Offset: 0x000034B8
		public override int GetHashCode()
		{
			if ((this.name != null) & !this.IsKnownColor)
			{
				return this.name.GetHashCode();
			}
			return HashHelpers.Combine(HashHelpers.Combine(this.value.GetHashCode(), this.state.GetHashCode()), this.knownColor.GetHashCode());
		}

		/// <summary>Represents a color that is null.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040000EF RID: 239
		public static readonly Color Empty;

		// Token: 0x040000F0 RID: 240
		private readonly string name;

		// Token: 0x040000F1 RID: 241
		private readonly long value;

		// Token: 0x040000F2 RID: 242
		private readonly short knownColor;

		// Token: 0x040000F3 RID: 243
		private readonly short state;
	}
}
