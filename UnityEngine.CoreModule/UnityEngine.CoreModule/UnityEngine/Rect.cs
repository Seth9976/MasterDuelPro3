using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000C8 RID: 200
	[NativeClass("Rectf", "template<typename T> class RectT; typedef RectT<float> Rectf;")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeHeader("Runtime/Math/Rect.h")]
	public struct Rect : IEquatable<Rect>, IFormattable
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x0000AE77 File Offset: 0x00009077
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Rect(float x, float y, float width, float height)
		{
			this.m_XMin = x;
			this.m_YMin = y;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000AE97 File Offset: 0x00009097
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Rect(Vector2 position, Vector2 size)
		{
			this.m_XMin = position.x;
			this.m_YMin = position.y;
			this.m_Width = size.x;
			this.m_Height = size.y;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000AECA File Offset: 0x000090CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Rect(Rect source)
		{
			this.m_XMin = source.m_XMin;
			this.m_YMin = source.m_YMin;
			this.m_Width = source.m_Width;
			this.m_Height = source.m_Height;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0000AEFD File Offset: 0x000090FD
		public static Rect zero
		{
			get
			{
				return new Rect(0f, 0f, 0f, 0f);
			}
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000AF18 File Offset: 0x00009118
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rect MinMaxRect(float xmin, float ymin, float xmax, float ymax)
		{
			return new Rect(xmin, ymin, xmax - xmin, ymax - ymin);
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0000AF38 File Offset: 0x00009138
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x0000AF50 File Offset: 0x00009150
		public float x
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

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000AF5C File Offset: 0x0000915C
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x0000AF74 File Offset: 0x00009174
		public float y
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

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0000AF80 File Offset: 0x00009180
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x0000AFA3 File Offset: 0x000091A3
		public Vector2 position
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vector2(this.m_XMin, this.m_YMin);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_XMin = value.x;
				this.m_YMin = value.y;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0000AFC0 File Offset: 0x000091C0
		public Vector2 center
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vector2(this.x + this.m_Width / 2f, this.y + this.m_Height / 2f);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0000B000 File Offset: 0x00009200
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x0000B023 File Offset: 0x00009223
		public Vector2 min
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vector2(this.xMin, this.yMin);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.xMin = value.x;
				this.yMin = value.y;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0000B040 File Offset: 0x00009240
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x0000B063 File Offset: 0x00009263
		public Vector2 max
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vector2(this.xMax, this.yMax);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.xMax = value.x;
				this.yMax = value.y;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0000B080 File Offset: 0x00009280
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x0000B098 File Offset: 0x00009298
		public float width
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

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0000B0A4 File Offset: 0x000092A4
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x0000B0BC File Offset: 0x000092BC
		public float height
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

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0000B0C8 File Offset: 0x000092C8
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x0000B0EB File Offset: 0x000092EB
		public Vector2 size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vector2(this.m_Width, this.m_Height);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Width = value.x;
				this.m_Height = value.y;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0000B108 File Offset: 0x00009308
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x0000B120 File Offset: 0x00009320
		public float xMin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_XMin;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				float oldxmax = this.xMax;
				this.m_XMin = value;
				this.m_Width = oldxmax - this.m_XMin;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0000B14C File Offset: 0x0000934C
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x0000B164 File Offset: 0x00009364
		public float yMin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_YMin;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				float oldymax = this.yMax;
				this.m_YMin = value;
				this.m_Height = oldymax - this.m_YMin;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x0000B190 File Offset: 0x00009390
		// (set) Token: 0x06000508 RID: 1288 RVA: 0x0000B1AF File Offset: 0x000093AF
		public float xMax
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Width + this.m_XMin;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Width = value - this.m_XMin;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0000B1C0 File Offset: 0x000093C0
		// (set) Token: 0x0600050A RID: 1290 RVA: 0x0000B1DF File Offset: 0x000093DF
		public float yMax
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Height + this.m_YMin;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Height = value - this.m_YMin;
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000B1F0 File Offset: 0x000093F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(Vector2 point)
		{
			return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000B240 File Offset: 0x00009440
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(Vector3 point)
		{
			return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000B290 File Offset: 0x00009490
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Rect OrderMinMax(Rect rect)
		{
			bool flag = rect.xMin > rect.xMax;
			if (flag)
			{
				float temp = rect.xMin;
				rect.xMin = rect.xMax;
				rect.xMax = temp;
			}
			bool flag2 = rect.yMin > rect.yMax;
			if (flag2)
			{
				float temp2 = rect.yMin;
				rect.yMin = rect.yMax;
				rect.yMax = temp2;
			}
			return rect;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000B314 File Offset: 0x00009514
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Overlaps(Rect other)
		{
			return other.xMax > this.xMin && other.xMin < this.xMax && other.yMax > this.yMin && other.yMin < this.yMax;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000B368 File Offset: 0x00009568
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Overlaps(Rect other, bool allowInverse)
		{
			Rect self = this;
			if (allowInverse)
			{
				self = Rect.OrderMinMax(self);
				other = Rect.OrderMinMax(other);
			}
			return self.Overlaps(other);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000B3A0 File Offset: 0x000095A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Rect lhs, Rect rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000B3BC File Offset: 0x000095BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Rect lhs, Rect rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.width == rhs.width && lhs.height == rhs.height;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000B414 File Offset: 0x00009614
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.width.GetHashCode() << 2) ^ (this.y.GetHashCode() >> 2) ^ (this.height.GetHashCode() >> 1);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000B468 File Offset: 0x00009668
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			Rect r;
			bool flag;
			if (other is Rect)
			{
				r = (Rect)other;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return flag2 && this.Equals(r);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000B49C File Offset: 0x0000969C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Rect other)
		{
			return this.x.Equals(other.x) && this.y.Equals(other.y) && this.width.Equals(other.width) && this.height.Equals(other.height);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000B50C File Offset: 0x0000970C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000B528 File Offset: 0x00009728
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F2";
			}
			bool flag2 = formatProvider == null;
			if (flag2)
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

		// Token: 0x0400026C RID: 620
		[NativeName("x")]
		private float m_XMin;

		// Token: 0x0400026D RID: 621
		[NativeName("y")]
		private float m_YMin;

		// Token: 0x0400026E RID: 622
		[NativeName("width")]
		private float m_Width;

		// Token: 0x0400026F RID: 623
		[NativeName("height")]
		private float m_Height;
	}
}
