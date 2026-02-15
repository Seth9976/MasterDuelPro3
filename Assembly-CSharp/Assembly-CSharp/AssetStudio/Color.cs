using System;
using System.Runtime.InteropServices;

namespace AssetStudio
{
	// Token: 0x0200016C RID: 364
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct Color : IEquatable<Color>
	{
		// Token: 0x06000482 RID: 1154 RVA: 0x00015F71 File Offset: 0x00014171
		public Color(float r, float g, float b, float a)
		{
			this.R = r;
			this.G = g;
			this.B = b;
			this.A = a;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00015F90 File Offset: 0x00014190
		public override int GetHashCode()
		{
			return this.GetHashCode();
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00015FB6 File Offset: 0x000141B6
		public override bool Equals(object other)
		{
			return other is Color && this.Equals((Color)other);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00015FD0 File Offset: 0x000141D0
		public bool Equals(Color other)
		{
			return this.R.Equals(other.R) && this.G.Equals(other.G) && this.B.Equals(other.B) && this.A.Equals(other.A);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00016029 File Offset: 0x00014229
		public static Color operator +(Color a, Color b)
		{
			return new Color(a.R + b.R, a.G + b.G, a.B + b.B, a.A + b.A);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00016064 File Offset: 0x00014264
		public static Color operator -(Color a, Color b)
		{
			return new Color(a.R - b.R, a.G - b.G, a.B - b.B, a.A - b.A);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001609F File Offset: 0x0001429F
		public static Color operator *(Color a, Color b)
		{
			return new Color(a.R * b.R, a.G * b.G, a.B * b.B, a.A * b.A);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000160DA File Offset: 0x000142DA
		public static Color operator *(Color a, float b)
		{
			return new Color(a.R * b, a.G * b, a.B * b, a.A * b);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00016101 File Offset: 0x00014301
		public static Color operator *(float b, Color a)
		{
			return new Color(a.R * b, a.G * b, a.B * b, a.A * b);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00016128 File Offset: 0x00014328
		public static Color operator /(Color a, float b)
		{
			return new Color(a.R / b, a.G / b, a.B / b, a.A / b);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001614F File Offset: 0x0001434F
		public static bool operator ==(Color lhs, Color rhs)
		{
			return lhs == rhs;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00016162 File Offset: 0x00014362
		public static bool operator !=(Color lhs, Color rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001616E File Offset: 0x0001436E
		public static implicit operator Vector4(Color c)
		{
			return new Vector4(c.R, c.G, c.B, c.A);
		}

		// Token: 0x0400098B RID: 2443
		public float R;

		// Token: 0x0400098C RID: 2444
		public float G;

		// Token: 0x0400098D RID: 2445
		public float B;

		// Token: 0x0400098E RID: 2446
		public float A;
	}
}
