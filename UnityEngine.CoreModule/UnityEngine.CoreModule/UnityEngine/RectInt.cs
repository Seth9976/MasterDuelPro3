using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000C9 RID: 201
	[UsedByNativeCode]
	public struct RectInt : IEquatable<RectInt>, IFormattable
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0000B5BC File Offset: 0x000097BC
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x0000B5D4 File Offset: 0x000097D4
		public int x
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_XMin;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_XMin = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x0000B5E0 File Offset: 0x000097E0
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x0000B5F8 File Offset: 0x000097F8
		public int y
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_YMin;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_YMin = value;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x0000B604 File Offset: 0x00009804
		// (set) Token: 0x0600051C RID: 1308 RVA: 0x0000B61C File Offset: 0x0000981C
		public int width
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Width;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Width = value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x0000B628 File Offset: 0x00009828
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x0000B640 File Offset: 0x00009840
		public int height
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Height;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Height = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x0000B64C File Offset: 0x0000984C
		public int xMin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Math.Min(this.m_XMin, this.m_XMin + this.m_Width);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0000B678 File Offset: 0x00009878
		public int yMin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Math.Min(this.m_YMin, this.m_YMin + this.m_Height);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0000B6A4 File Offset: 0x000098A4
		public int xMax
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Math.Max(this.m_XMin, this.m_XMin + this.m_Width);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0000B6D0 File Offset: 0x000098D0
		public int yMax
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Math.Max(this.m_YMin, this.m_YMin + this.m_Height);
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0000B6FA File Offset: 0x000098FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public RectInt(int xMin, int yMin, int width, int height)
		{
			this.m_XMin = xMin;
			this.m_YMin = yMin;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000B71C File Offset: 0x0000991C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Overlaps(RectInt other)
		{
			return other.xMin < this.xMax && other.xMax > this.xMin && other.yMin < this.yMax && other.yMax > this.yMin;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0000B770 File Offset: 0x00009970
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000B78C File Offset: 0x0000998C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = formatProvider == null;
			if (flag)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("(x:{0}, y:{1}, width:{2}, height:{3})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.width.ToString(format, formatProvider),
				this.height.ToString(format, formatProvider)
			});
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000B810 File Offset: 0x00009A10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			int xHash = this.x.GetHashCode();
			int yHash = this.y.GetHashCode();
			int wHash = this.width.GetHashCode();
			int hHash = this.height.GetHashCode();
			return xHash ^ (yHash << 4) ^ (yHash >> 28) ^ (wHash >> 4) ^ (wHash << 28) ^ (hHash >> 4) ^ (hHash << 28);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000B880 File Offset: 0x00009A80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			bool flag = !(other is RectInt);
			return !flag && this.Equals((RectInt)other);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000B8B4 File Offset: 0x00009AB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(RectInt other)
		{
			return this.m_XMin == other.m_XMin && this.m_YMin == other.m_YMin && this.m_Width == other.m_Width && this.m_Height == other.m_Height;
		}

		// Token: 0x04000270 RID: 624
		private int m_XMin;

		// Token: 0x04000271 RID: 625
		private int m_YMin;

		// Token: 0x04000272 RID: 626
		private int m_Width;

		// Token: 0x04000273 RID: 627
		private int m_Height;
	}
}
