using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000C3 RID: 195
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeType(Header = "Runtime/Geometry/AABB.h")]
	[NativeHeader("Runtime/Geometry/AABB.h")]
	[NativeHeader("Runtime/Geometry/Intersection.h")]
	[NativeClass("AABB")]
	[NativeHeader("Runtime/Geometry/Ray.h")]
	[NativeHeader("Runtime/Math/MathScripting.h")]
	public struct Bounds : IEquatable<Bounds>, IFormattable
	{
		// Token: 0x060004B8 RID: 1208 RVA: 0x0000A591 File Offset: 0x00008791
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Bounds(Vector3 center, Vector3 size)
		{
			this.m_Center = center;
			this.m_Extents = size * 0.5f;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000A5AC File Offset: 0x000087AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return this.center.GetHashCode() ^ (this.extents.GetHashCode() << 2);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000A5EC File Offset: 0x000087EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			bool flag = !(other is Bounds);
			return !flag && this.Equals((Bounds)other);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000A620 File Offset: 0x00008820
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Bounds other)
		{
			return this.center.Equals(other.center) && this.extents.Equals(other.extents);
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x0000A664 File Offset: 0x00008864
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x0000A67C File Offset: 0x0000887C
		public Vector3 center
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Center;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Center = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0000A688 File Offset: 0x00008888
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x0000A6AA File Offset: 0x000088AA
		public Vector3 size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Extents * 2f;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Extents = value * 0.5f;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0000A6C0 File Offset: 0x000088C0
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x0000A6D8 File Offset: 0x000088D8
		public Vector3 extents
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Extents;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Extents = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0000A6E4 File Offset: 0x000088E4
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x0000A707 File Offset: 0x00008907
		public Vector3 min
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.center - this.extents;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.SetMinMax(value, this.max);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0000A718 File Offset: 0x00008918
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x0000A73B File Offset: 0x0000893B
		public Vector3 max
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.center + this.extents;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.SetMinMax(this.min, value);
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000A74C File Offset: 0x0000894C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Bounds lhs, Bounds rhs)
		{
			return lhs.center == rhs.center && lhs.extents == rhs.extents;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000A78C File Offset: 0x0000898C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Bounds lhs, Bounds rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000A7A8 File Offset: 0x000089A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetMinMax(Vector3 min, Vector3 max)
		{
			this.extents = (max - min) * 0.5f;
			this.center = min + this.extents;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000A7D6 File Offset: 0x000089D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Encapsulate(Vector3 point)
		{
			this.SetMinMax(Vector3.Min(this.min, point), Vector3.Max(this.max, point));
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000A7F8 File Offset: 0x000089F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Encapsulate(Bounds bounds)
		{
			this.Encapsulate(bounds.center - bounds.extents);
			this.Encapsulate(bounds.center + bounds.extents);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000A830 File Offset: 0x00008A30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Intersects(Bounds bounds)
		{
			return this.min.x <= bounds.max.x && this.max.x >= bounds.min.x && this.min.y <= bounds.max.y && this.max.y >= bounds.min.y && this.min.z <= bounds.max.z && this.max.z >= bounds.min.z;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0000A8E4 File Offset: 0x00008AE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0000A900 File Offset: 0x00008B00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
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
			return UnityString.Format("Center: {0}, Extents: {1}", new object[]
			{
				this.m_Center.ToString(format, formatProvider),
				this.m_Extents.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000263 RID: 611
		private Vector3 m_Center;

		// Token: 0x04000264 RID: 612
		[NativeName("m_Extent")]
		private Vector3 m_Extents;
	}
}
