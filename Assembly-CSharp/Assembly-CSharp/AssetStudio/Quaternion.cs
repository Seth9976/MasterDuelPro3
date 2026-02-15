using System;
using System.Runtime.InteropServices;

namespace AssetStudio
{
	// Token: 0x02000170 RID: 368
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct Quaternion : IEquatable<Quaternion>
	{
		// Token: 0x0600050E RID: 1294 RVA: 0x000178CB File Offset: 0x00015ACB
		public Quaternion(float x, float y, float z, float w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		// Token: 0x1700007B RID: 123
		public float this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.X;
				case 1:
					return this.Y;
				case 2:
					return this.Z;
				case 3:
					return this.W;
				default:
					throw new ArgumentOutOfRangeException("index", "Invalid Quaternion index!");
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this.X = value;
					return;
				case 1:
					this.Y = value;
					return;
				case 2:
					this.Z = value;
					return;
				case 3:
					this.W = value;
					return;
				default:
					throw new ArgumentOutOfRangeException("index", "Invalid Quaternion index!");
				}
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00017990 File Offset: 0x00015B90
		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ (this.Y.GetHashCode() << 2) ^ (this.Z.GetHashCode() >> 2) ^ (this.W.GetHashCode() >> 1);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x000179C7 File Offset: 0x00015BC7
		public override bool Equals(object other)
		{
			return other is Quaternion && this.Equals((Quaternion)other);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x000179E0 File Offset: 0x00015BE0
		public bool Equals(Quaternion other)
		{
			return this.X.Equals(other.X) && this.Y.Equals(other.Y) && this.Z.Equals(other.Z) && this.W.Equals(other.W);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00017A39 File Offset: 0x00015C39
		public static float Dot(Quaternion a, Quaternion b)
		{
			return a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00017A72 File Offset: 0x00015C72
		private static bool IsEqualUsingDot(float dot)
		{
			return dot > 0.999999f;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00017A7C File Offset: 0x00015C7C
		public static bool operator ==(Quaternion lhs, Quaternion rhs)
		{
			return Quaternion.IsEqualUsingDot(Quaternion.Dot(lhs, rhs));
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00017A8A File Offset: 0x00015C8A
		public static bool operator !=(Quaternion lhs, Quaternion rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040009AB RID: 2475
		public float X;

		// Token: 0x040009AC RID: 2476
		public float Y;

		// Token: 0x040009AD RID: 2477
		public float Z;

		// Token: 0x040009AE RID: 2478
		public float W;

		// Token: 0x040009AF RID: 2479
		private const float kEpsilon = 1E-06f;
	}
}
