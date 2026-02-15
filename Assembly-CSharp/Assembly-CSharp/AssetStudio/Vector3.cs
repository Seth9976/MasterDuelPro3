using System;
using System.Runtime.InteropServices;

namespace AssetStudio
{
	// Token: 0x02000172 RID: 370
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct Vector3 : IEquatable<Vector3>
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x00017D30 File Offset: 0x00015F30
		public Vector3(float x, float y, float z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		// Token: 0x1700007E RID: 126
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
				default:
					throw new ArgumentOutOfRangeException("index", "Invalid Vector3 index!");
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
				default:
					throw new ArgumentOutOfRangeException("index", "Invalid Vector3 index!");
				}
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00017DBE File Offset: 0x00015FBE
		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ (this.Y.GetHashCode() << 2) ^ (this.Z.GetHashCode() >> 2);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00017DE7 File Offset: 0x00015FE7
		public override bool Equals(object other)
		{
			return other is Vector3 && this.Equals((Vector3)other);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00017DFF File Offset: 0x00015FFF
		public bool Equals(Vector3 other)
		{
			return this.X.Equals(other.X) && this.Y.Equals(other.Y) && this.Z.Equals(other.Z);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00017E3C File Offset: 0x0001603C
		public void Normalize()
		{
			float length = this.Length();
			if (length > 1E-05f)
			{
				float invNorm = 1f / length;
				this.X *= invNorm;
				this.Y *= invNorm;
				this.Z *= invNorm;
				return;
			}
			this.X = 0f;
			this.Y = 0f;
			this.Z = 0f;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00017EAC File Offset: 0x000160AC
		public float Length()
		{
			return (float)Math.Sqrt((double)this.LengthSquared());
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00017EBB File Offset: 0x000160BB
		public float LengthSquared()
		{
			return this.X * this.X + this.Y * this.Y + this.Z * this.Z;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00017EE8 File Offset: 0x000160E8
		public static Vector3 Zero
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x00017EFE File Offset: 0x000160FE
		public static Vector3 One
		{
			get
			{
				return new Vector3(1f, 1f, 1f);
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00017F14 File Offset: 0x00016114
		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00017F42 File Offset: 0x00016142
		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00017F70 File Offset: 0x00016170
		public static Vector3 operator -(Vector3 a)
		{
			return new Vector3(-a.X, -a.Y, -a.Z);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00017F8C File Offset: 0x0001618C
		public static Vector3 operator *(Vector3 a, float d)
		{
			return new Vector3(a.X * d, a.Y * d, a.Z * d);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00017FAB File Offset: 0x000161AB
		public static Vector3 operator *(float d, Vector3 a)
		{
			return new Vector3(a.X * d, a.Y * d, a.Z * d);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00017FCA File Offset: 0x000161CA
		public static Vector3 operator /(Vector3 a, float d)
		{
			return new Vector3(a.X / d, a.Y / d, a.Z / d);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00017FEC File Offset: 0x000161EC
		public static bool operator ==(Vector3 lhs, Vector3 rhs)
		{
			return (lhs - rhs).LengthSquared() < 9.9999994E-11f;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001800F File Offset: 0x0001620F
		public static bool operator !=(Vector3 lhs, Vector3 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001801B File Offset: 0x0001621B
		public static implicit operator Vector2(Vector3 v)
		{
			return new Vector2(v.X, v.Y);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001802E File Offset: 0x0001622E
		public static implicit operator Vector4(Vector3 v)
		{
			return new Vector4(v.X, v.Y, v.Z, 0f);
		}

		// Token: 0x040009B3 RID: 2483
		public float X;

		// Token: 0x040009B4 RID: 2484
		public float Y;

		// Token: 0x040009B5 RID: 2485
		public float Z;

		// Token: 0x040009B6 RID: 2486
		private const float kEpsilon = 1E-05f;
	}
}
