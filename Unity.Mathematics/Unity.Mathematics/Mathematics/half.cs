using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200003A RID: 58
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct half : IEquatable<half>, IFormattable
	{
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x000442E3 File Offset: 0x000424E3
		public static float MaxValue
		{
			get
			{
				return 65504f;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x000442EA File Offset: 0x000424EA
		public static float MinValue
		{
			get
			{
				return -65504f;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x000442F1 File Offset: 0x000424F1
		public static half MaxValueAsHalf
		{
			get
			{
				return new half(half.MaxValue);
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x000442FD File Offset: 0x000424FD
		public static half MinValueAsHalf
		{
			get
			{
				return new half(half.MinValue);
			}
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x00044309 File Offset: 0x00042509
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half(half x)
		{
			this.value = x.value;
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x00044317 File Offset: 0x00042517
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half(float v)
		{
			this.value = (ushort)math.f32tof16(v);
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x00044326 File Offset: 0x00042526
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half(double v)
		{
			this.value = (ushort)math.f32tof16((float)v);
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x0000D99A File Offset: 0x0000BB9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half(float v)
		{
			return new half(v);
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x0000D9A2 File Offset: 0x0000BBA2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half(double v)
		{
			return new half(v);
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x00044336 File Offset: 0x00042536
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float(half d)
		{
			return math.f16tof32((uint)d.value);
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00044343 File Offset: 0x00042543
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double(half d)
		{
			return (double)math.f16tof32((uint)d.value);
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00044351 File Offset: 0x00042551
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(half lhs, half rhs)
		{
			return lhs.value == rhs.value;
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x00044361 File Offset: 0x00042561
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(half lhs, half rhs)
		{
			return lhs.value != rhs.value;
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x00044351 File Offset: 0x00042551
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(half rhs)
		{
			return this.value == rhs.value;
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x00044374 File Offset: 0x00042574
		public override bool Equals(object o)
		{
			if (o is half)
			{
				half converted = (half)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x00044399 File Offset: 0x00042599
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)this.value;
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x000443A4 File Offset: 0x000425A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return math.f16tof32((uint)this.value).ToString();
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x000443C4 File Offset: 0x000425C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return math.f16tof32((uint)this.value).ToString(format, formatProvider);
		}

		// Token: 0x040000E2 RID: 226
		public ushort value;

		// Token: 0x040000E3 RID: 227
		public static readonly half zero;
	}
}
