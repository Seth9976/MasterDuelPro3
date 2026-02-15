using System;
using System.Runtime.InteropServices;

namespace AssetStudio
{
	// Token: 0x02000173 RID: 371
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct Vector4 : IEquatable<Vector4>
	{
		// Token: 0x06000543 RID: 1347 RVA: 0x0001804C File Offset: 0x0001624C
		public Vector4(float x, float y, float z, float w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001806B File Offset: 0x0001626B
		public Vector4(Vector3 value, float w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = value.Z;
			this.W = w;
		}

		// Token: 0x17000081 RID: 129
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
					throw new ArgumentOutOfRangeException("index", "Invalid Vector4 index!");
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
					throw new ArgumentOutOfRangeException("index", "Invalid Vector4 index!");
				}
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001813C File Offset: 0x0001633C
		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ (this.Y.GetHashCode() << 2) ^ (this.Z.GetHashCode() >> 2) ^ (this.W.GetHashCode() >> 1);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00018173 File Offset: 0x00016373
		public override bool Equals(object other)
		{
			return other is Vector4 && this.Equals((Vector4)other);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001818C File Offset: 0x0001638C
		public bool Equals(Vector4 other)
		{
			return this.X.Equals(other.X) && this.Y.Equals(other.Y) && this.Z.Equals(other.Z) && this.W.Equals(other.W);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x000181E8 File Offset: 0x000163E8
		public void Normalize()
		{
			float length = this.Length();
			if (length > 1E-05f)
			{
				float invNorm = 1f / length;
				this.X *= invNorm;
				this.Y *= invNorm;
				this.Z *= invNorm;
				this.W *= invNorm;
				return;
			}
			this.X = 0f;
			this.Y = 0f;
			this.Z = 0f;
			this.W = 0f;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00018271 File Offset: 0x00016471
		public float Length()
		{
			return (float)Math.Sqrt((double)this.LengthSquared());
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00018280 File Offset: 0x00016480
		public float LengthSquared()
		{
			return this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x000182BC File Offset: 0x000164BC
		public static Vector4 Zero
		{
			get
			{
				return default(Vector4);
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x000182D2 File Offset: 0x000164D2
		public static Vector4 operator +(Vector4 a, Vector4 b)
		{
			return new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001830D File Offset: 0x0001650D
		public static Vector4 operator -(Vector4 a, Vector4 b)
		{
			return new Vector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00018348 File Offset: 0x00016548
		public static Vector4 operator -(Vector4 a)
		{
			return new Vector4(-a.X, -a.Y, -a.Z, -a.W);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001836B File Offset: 0x0001656B
		public static Vector4 operator *(Vector4 a, float d)
		{
			return new Vector4(a.X * d, a.Y * d, a.Z * d, a.W * d);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00018392 File Offset: 0x00016592
		public static Vector4 operator *(float d, Vector4 a)
		{
			return new Vector4(a.X * d, a.Y * d, a.Z * d, a.W * d);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000183B9 File Offset: 0x000165B9
		public static Vector4 operator /(Vector4 a, float d)
		{
			return new Vector4(a.X / d, a.Y / d, a.Z / d, a.W / d);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000183E0 File Offset: 0x000165E0
		public static bool operator ==(Vector4 lhs, Vector4 rhs)
		{
			return (lhs - rhs).LengthSquared() < 9.9999994E-11f;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00018403 File Offset: 0x00016603
		public static bool operator !=(Vector4 lhs, Vector4 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001840F File Offset: 0x0001660F
		public static implicit operator Vector2(Vector4 v)
		{
			return new Vector2(v.X, v.Y);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00018422 File Offset: 0x00016622
		public static implicit operator Vector3(Vector4 v)
		{
			return new Vector3(v.X, v.Y, v.Z);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001843B File Offset: 0x0001663B
		public static implicit operator Color(Vector4 v)
		{
			return new Color(v.X, v.Y, v.Z, v.W);
		}

		// Token: 0x040009B7 RID: 2487
		public float X;

		// Token: 0x040009B8 RID: 2488
		public float Y;

		// Token: 0x040009B9 RID: 2489
		public float Z;

		// Token: 0x040009BA RID: 2490
		public float W;

		// Token: 0x040009BB RID: 2491
		private const float kEpsilon = 1E-05f;
	}
}
