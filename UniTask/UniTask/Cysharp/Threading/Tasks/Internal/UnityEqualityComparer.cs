using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000240 RID: 576
	internal static class UnityEqualityComparer
	{
		// Token: 0x06000D0A RID: 3338 RVA: 0x0002D53D File Offset: 0x0002B73D
		public static IEqualityComparer<T> GetDefault<T>()
		{
			return UnityEqualityComparer.Cache<T>.Comparer;
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0002D544 File Offset: 0x0002B744
		private static object GetDefaultHelper(Type type)
		{
			RuntimeTypeHandle t = type.TypeHandle;
			if (t.Equals(UnityEqualityComparer.vector2Type))
			{
				return UnityEqualityComparer.Vector2;
			}
			if (t.Equals(UnityEqualityComparer.vector3Type))
			{
				return UnityEqualityComparer.Vector3;
			}
			if (t.Equals(UnityEqualityComparer.vector4Type))
			{
				return UnityEqualityComparer.Vector4;
			}
			if (t.Equals(UnityEqualityComparer.colorType))
			{
				return UnityEqualityComparer.Color;
			}
			if (t.Equals(UnityEqualityComparer.color32Type))
			{
				return UnityEqualityComparer.Color32;
			}
			if (t.Equals(UnityEqualityComparer.rectType))
			{
				return UnityEqualityComparer.Rect;
			}
			if (t.Equals(UnityEqualityComparer.boundsType))
			{
				return UnityEqualityComparer.Bounds;
			}
			if (t.Equals(UnityEqualityComparer.quaternionType))
			{
				return UnityEqualityComparer.Quaternion;
			}
			if (t.Equals(UnityEqualityComparer.vector2IntType))
			{
				return UnityEqualityComparer.Vector2Int;
			}
			if (t.Equals(UnityEqualityComparer.vector3IntType))
			{
				return UnityEqualityComparer.Vector3Int;
			}
			if (t.Equals(UnityEqualityComparer.rangeIntType))
			{
				return UnityEqualityComparer.RangeInt;
			}
			if (t.Equals(UnityEqualityComparer.rectIntType))
			{
				return UnityEqualityComparer.RectInt;
			}
			if (t.Equals(UnityEqualityComparer.boundsIntType))
			{
				return UnityEqualityComparer.BoundsInt;
			}
			return null;
		}

		// Token: 0x0400068D RID: 1677
		public static readonly IEqualityComparer<Vector2> Vector2 = new UnityEqualityComparer.Vector2EqualityComparer();

		// Token: 0x0400068E RID: 1678
		public static readonly IEqualityComparer<Vector3> Vector3 = new UnityEqualityComparer.Vector3EqualityComparer();

		// Token: 0x0400068F RID: 1679
		public static readonly IEqualityComparer<Vector4> Vector4 = new UnityEqualityComparer.Vector4EqualityComparer();

		// Token: 0x04000690 RID: 1680
		public static readonly IEqualityComparer<Color> Color = new UnityEqualityComparer.ColorEqualityComparer();

		// Token: 0x04000691 RID: 1681
		public static readonly IEqualityComparer<Color32> Color32 = new UnityEqualityComparer.Color32EqualityComparer();

		// Token: 0x04000692 RID: 1682
		public static readonly IEqualityComparer<Rect> Rect = new UnityEqualityComparer.RectEqualityComparer();

		// Token: 0x04000693 RID: 1683
		public static readonly IEqualityComparer<Bounds> Bounds = new UnityEqualityComparer.BoundsEqualityComparer();

		// Token: 0x04000694 RID: 1684
		public static readonly IEqualityComparer<Quaternion> Quaternion = new UnityEqualityComparer.QuaternionEqualityComparer();

		// Token: 0x04000695 RID: 1685
		private static readonly RuntimeTypeHandle vector2Type = typeof(Vector2).TypeHandle;

		// Token: 0x04000696 RID: 1686
		private static readonly RuntimeTypeHandle vector3Type = typeof(Vector3).TypeHandle;

		// Token: 0x04000697 RID: 1687
		private static readonly RuntimeTypeHandle vector4Type = typeof(Vector4).TypeHandle;

		// Token: 0x04000698 RID: 1688
		private static readonly RuntimeTypeHandle colorType = typeof(Color).TypeHandle;

		// Token: 0x04000699 RID: 1689
		private static readonly RuntimeTypeHandle color32Type = typeof(Color32).TypeHandle;

		// Token: 0x0400069A RID: 1690
		private static readonly RuntimeTypeHandle rectType = typeof(Rect).TypeHandle;

		// Token: 0x0400069B RID: 1691
		private static readonly RuntimeTypeHandle boundsType = typeof(Bounds).TypeHandle;

		// Token: 0x0400069C RID: 1692
		private static readonly RuntimeTypeHandle quaternionType = typeof(Quaternion).TypeHandle;

		// Token: 0x0400069D RID: 1693
		public static readonly IEqualityComparer<Vector2Int> Vector2Int = new UnityEqualityComparer.Vector2IntEqualityComparer();

		// Token: 0x0400069E RID: 1694
		public static readonly IEqualityComparer<Vector3Int> Vector3Int = new UnityEqualityComparer.Vector3IntEqualityComparer();

		// Token: 0x0400069F RID: 1695
		public static readonly IEqualityComparer<RangeInt> RangeInt = new UnityEqualityComparer.RangeIntEqualityComparer();

		// Token: 0x040006A0 RID: 1696
		public static readonly IEqualityComparer<RectInt> RectInt = new UnityEqualityComparer.RectIntEqualityComparer();

		// Token: 0x040006A1 RID: 1697
		public static readonly IEqualityComparer<BoundsInt> BoundsInt = new UnityEqualityComparer.BoundsIntEqualityComparer();

		// Token: 0x040006A2 RID: 1698
		private static readonly RuntimeTypeHandle vector2IntType = typeof(Vector2Int).TypeHandle;

		// Token: 0x040006A3 RID: 1699
		private static readonly RuntimeTypeHandle vector3IntType = typeof(Vector3Int).TypeHandle;

		// Token: 0x040006A4 RID: 1700
		private static readonly RuntimeTypeHandle rangeIntType = typeof(RangeInt).TypeHandle;

		// Token: 0x040006A5 RID: 1701
		private static readonly RuntimeTypeHandle rectIntType = typeof(RectInt).TypeHandle;

		// Token: 0x040006A6 RID: 1702
		private static readonly RuntimeTypeHandle boundsIntType = typeof(BoundsInt).TypeHandle;

		// Token: 0x02000241 RID: 577
		private static class Cache<T>
		{
			// Token: 0x06000D0D RID: 3341 RVA: 0x0002D7F4 File Offset: 0x0002B9F4
			static Cache()
			{
				object comparer = UnityEqualityComparer.GetDefaultHelper(typeof(T));
				if (comparer == null)
				{
					UnityEqualityComparer.Cache<T>.Comparer = EqualityComparer<T>.Default;
					return;
				}
				UnityEqualityComparer.Cache<T>.Comparer = (IEqualityComparer<T>)comparer;
			}

			// Token: 0x040006A7 RID: 1703
			public static readonly IEqualityComparer<T> Comparer;
		}

		// Token: 0x02000242 RID: 578
		private sealed class Vector2EqualityComparer : IEqualityComparer<Vector2>
		{
			// Token: 0x06000D0E RID: 3342 RVA: 0x0002D82A File Offset: 0x0002BA2A
			public bool Equals(Vector2 self, Vector2 vector)
			{
				return self.x.Equals(vector.x) && self.y.Equals(vector.y);
			}

			// Token: 0x06000D0F RID: 3343 RVA: 0x0002D854 File Offset: 0x0002BA54
			public int GetHashCode(Vector2 obj)
			{
				return obj.x.GetHashCode() ^ (obj.y.GetHashCode() << 2);
			}
		}

		// Token: 0x02000243 RID: 579
		private sealed class Vector3EqualityComparer : IEqualityComparer<Vector3>
		{
			// Token: 0x06000D11 RID: 3345 RVA: 0x0002D871 File Offset: 0x0002BA71
			public bool Equals(Vector3 self, Vector3 vector)
			{
				return self.x.Equals(vector.x) && self.y.Equals(vector.y) && self.z.Equals(vector.z);
			}

			// Token: 0x06000D12 RID: 3346 RVA: 0x0002D8AF File Offset: 0x0002BAAF
			public int GetHashCode(Vector3 obj)
			{
				return obj.x.GetHashCode() ^ (obj.y.GetHashCode() << 2) ^ (obj.z.GetHashCode() >> 2);
			}
		}

		// Token: 0x02000244 RID: 580
		private sealed class Vector4EqualityComparer : IEqualityComparer<Vector4>
		{
			// Token: 0x06000D14 RID: 3348 RVA: 0x0002D8DC File Offset: 0x0002BADC
			public bool Equals(Vector4 self, Vector4 vector)
			{
				return self.x.Equals(vector.x) && self.y.Equals(vector.y) && self.z.Equals(vector.z) && self.w.Equals(vector.w);
			}

			// Token: 0x06000D15 RID: 3349 RVA: 0x0002D939 File Offset: 0x0002BB39
			public int GetHashCode(Vector4 obj)
			{
				return obj.x.GetHashCode() ^ (obj.y.GetHashCode() << 2) ^ (obj.z.GetHashCode() >> 2) ^ (obj.w.GetHashCode() >> 1);
			}
		}

		// Token: 0x02000245 RID: 581
		private sealed class ColorEqualityComparer : IEqualityComparer<Color>
		{
			// Token: 0x06000D17 RID: 3351 RVA: 0x0002D974 File Offset: 0x0002BB74
			public bool Equals(Color self, Color other)
			{
				return self.r.Equals(other.r) && self.g.Equals(other.g) && self.b.Equals(other.b) && self.a.Equals(other.a);
			}

			// Token: 0x06000D18 RID: 3352 RVA: 0x0002D9D1 File Offset: 0x0002BBD1
			public int GetHashCode(Color obj)
			{
				return obj.r.GetHashCode() ^ (obj.g.GetHashCode() << 2) ^ (obj.b.GetHashCode() >> 2) ^ (obj.a.GetHashCode() >> 1);
			}
		}

		// Token: 0x02000246 RID: 582
		private sealed class RectEqualityComparer : IEqualityComparer<Rect>
		{
			// Token: 0x06000D1A RID: 3354 RVA: 0x0002DA0C File Offset: 0x0002BC0C
			public bool Equals(Rect self, Rect other)
			{
				return self.x.Equals(other.x) && self.width.Equals(other.width) && self.y.Equals(other.y) && self.height.Equals(other.height);
			}

			// Token: 0x06000D1B RID: 3355 RVA: 0x0002DA7C File Offset: 0x0002BC7C
			public int GetHashCode(Rect obj)
			{
				return obj.x.GetHashCode() ^ (obj.width.GetHashCode() << 2) ^ (obj.y.GetHashCode() >> 2) ^ (obj.height.GetHashCode() >> 1);
			}
		}

		// Token: 0x02000247 RID: 583
		private sealed class BoundsEqualityComparer : IEqualityComparer<Bounds>
		{
			// Token: 0x06000D1D RID: 3357 RVA: 0x0002DAD0 File Offset: 0x0002BCD0
			public bool Equals(Bounds self, Bounds vector)
			{
				return self.center.Equals(vector.center) && self.extents.Equals(vector.extents);
			}

			// Token: 0x06000D1E RID: 3358 RVA: 0x0002DB10 File Offset: 0x0002BD10
			public int GetHashCode(Bounds obj)
			{
				return obj.center.GetHashCode() ^ (obj.extents.GetHashCode() << 2);
			}
		}

		// Token: 0x02000248 RID: 584
		private sealed class QuaternionEqualityComparer : IEqualityComparer<Quaternion>
		{
			// Token: 0x06000D20 RID: 3360 RVA: 0x0002DB4C File Offset: 0x0002BD4C
			public bool Equals(Quaternion self, Quaternion vector)
			{
				return self.x.Equals(vector.x) && self.y.Equals(vector.y) && self.z.Equals(vector.z) && self.w.Equals(vector.w);
			}

			// Token: 0x06000D21 RID: 3361 RVA: 0x0002DBA9 File Offset: 0x0002BDA9
			public int GetHashCode(Quaternion obj)
			{
				return obj.x.GetHashCode() ^ (obj.y.GetHashCode() << 2) ^ (obj.z.GetHashCode() >> 2) ^ (obj.w.GetHashCode() >> 1);
			}
		}

		// Token: 0x02000249 RID: 585
		private sealed class Color32EqualityComparer : IEqualityComparer<Color32>
		{
			// Token: 0x06000D23 RID: 3363 RVA: 0x0002DBE4 File Offset: 0x0002BDE4
			public bool Equals(Color32 self, Color32 vector)
			{
				return self.a.Equals(vector.a) && self.r.Equals(vector.r) && self.g.Equals(vector.g) && self.b.Equals(vector.b);
			}

			// Token: 0x06000D24 RID: 3364 RVA: 0x0002DC41 File Offset: 0x0002BE41
			public int GetHashCode(Color32 obj)
			{
				return obj.a.GetHashCode() ^ (obj.r.GetHashCode() << 2) ^ (obj.g.GetHashCode() >> 2) ^ (obj.b.GetHashCode() >> 1);
			}
		}

		// Token: 0x0200024A RID: 586
		private sealed class Vector2IntEqualityComparer : IEqualityComparer<Vector2Int>
		{
			// Token: 0x06000D26 RID: 3366 RVA: 0x0002DC7C File Offset: 0x0002BE7C
			public bool Equals(Vector2Int self, Vector2Int vector)
			{
				return self.x.Equals(vector.x) && self.y.Equals(vector.y);
			}

			// Token: 0x06000D27 RID: 3367 RVA: 0x0002DCBC File Offset: 0x0002BEBC
			public int GetHashCode(Vector2Int obj)
			{
				return obj.x.GetHashCode() ^ (obj.y.GetHashCode() << 2);
			}
		}

		// Token: 0x0200024B RID: 587
		private sealed class Vector3IntEqualityComparer : IEqualityComparer<Vector3Int>
		{
			// Token: 0x06000D29 RID: 3369 RVA: 0x0002DCEC File Offset: 0x0002BEEC
			public bool Equals(Vector3Int self, Vector3Int vector)
			{
				return self.x.Equals(vector.x) && self.y.Equals(vector.y) && self.z.Equals(vector.z);
			}

			// Token: 0x06000D2A RID: 3370 RVA: 0x0002DD44 File Offset: 0x0002BF44
			public int GetHashCode(Vector3Int obj)
			{
				return obj.x.GetHashCode() ^ (obj.y.GetHashCode() << 2) ^ (obj.z.GetHashCode() >> 2);
			}

			// Token: 0x040006A8 RID: 1704
			public static readonly UnityEqualityComparer.Vector3IntEqualityComparer Default = new UnityEqualityComparer.Vector3IntEqualityComparer();
		}

		// Token: 0x0200024C RID: 588
		private sealed class RangeIntEqualityComparer : IEqualityComparer<RangeInt>
		{
			// Token: 0x06000D2D RID: 3373 RVA: 0x0002DD90 File Offset: 0x0002BF90
			public bool Equals(RangeInt self, RangeInt vector)
			{
				return self.start.Equals(vector.start) && self.length.Equals(vector.length);
			}

			// Token: 0x06000D2E RID: 3374 RVA: 0x0002DDBA File Offset: 0x0002BFBA
			public int GetHashCode(RangeInt obj)
			{
				return obj.start.GetHashCode() ^ (obj.length.GetHashCode() << 2);
			}
		}

		// Token: 0x0200024D RID: 589
		private sealed class RectIntEqualityComparer : IEqualityComparer<RectInt>
		{
			// Token: 0x06000D30 RID: 3376 RVA: 0x0002DDD8 File Offset: 0x0002BFD8
			public bool Equals(RectInt self, RectInt other)
			{
				return self.x.Equals(other.x) && self.width.Equals(other.width) && self.y.Equals(other.y) && self.height.Equals(other.height);
			}

			// Token: 0x06000D31 RID: 3377 RVA: 0x0002DE48 File Offset: 0x0002C048
			public int GetHashCode(RectInt obj)
			{
				return obj.x.GetHashCode() ^ (obj.width.GetHashCode() << 2) ^ (obj.y.GetHashCode() >> 2) ^ (obj.height.GetHashCode() >> 1);
			}
		}

		// Token: 0x0200024E RID: 590
		private sealed class BoundsIntEqualityComparer : IEqualityComparer<BoundsInt>
		{
			// Token: 0x06000D33 RID: 3379 RVA: 0x0002DE9A File Offset: 0x0002C09A
			public bool Equals(BoundsInt self, BoundsInt vector)
			{
				return UnityEqualityComparer.Vector3IntEqualityComparer.Default.Equals(self.position, vector.position) && UnityEqualityComparer.Vector3IntEqualityComparer.Default.Equals(self.size, vector.size);
			}

			// Token: 0x06000D34 RID: 3380 RVA: 0x0002DED0 File Offset: 0x0002C0D0
			public int GetHashCode(BoundsInt obj)
			{
				return UnityEqualityComparer.Vector3IntEqualityComparer.Default.GetHashCode(obj.position) ^ (UnityEqualityComparer.Vector3IntEqualityComparer.Default.GetHashCode(obj.size) << 2);
			}
		}
	}
}
