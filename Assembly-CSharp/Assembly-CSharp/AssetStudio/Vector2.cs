using System;
using System.Runtime.InteropServices;

namespace AssetStudio
{
	// Token: 0x02000171 RID: 369
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct Vector2 : IEquatable<Vector2>
	{
		// Token: 0x06000518 RID: 1304 RVA: 0x00017A96 File Offset: 0x00015C96
		public Vector2(float x, float y)
		{
			this.X = x;
			this.Y = y;
		}

		// Token: 0x1700007C RID: 124
		public float this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this.X;
				}
				if (index != 1)
				{
					throw new ArgumentOutOfRangeException("index", "Invalid Vector2 index!");
				}
				return this.Y;
			}
			set
			{
				if (index == 0)
				{
					this.X = value;
					return;
				}
				if (index != 1)
				{
					throw new ArgumentOutOfRangeException("index", "Invalid Vector2 index!");
				}
				this.Y = value;
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00017AF8 File Offset: 0x00015CF8
		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ (this.Y.GetHashCode() << 2);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00017B13 File Offset: 0x00015D13
		public override bool Equals(object other)
		{
			return other is Vector2 && this.Equals((Vector2)other);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00017B2B File Offset: 0x00015D2B
		public bool Equals(Vector2 other)
		{
			return this.X.Equals(other.X) && this.Y.Equals(other.Y);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00017B54 File Offset: 0x00015D54
		public void Normalize()
		{
			float length = this.Length();
			if (length > 1E-05f)
			{
				float invNorm = 1f / length;
				this.X *= invNorm;
				this.Y *= invNorm;
				return;
			}
			this.X = 0f;
			this.Y = 0f;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00017BAB File Offset: 0x00015DAB
		public float Length()
		{
			return (float)Math.Sqrt((double)this.LengthSquared());
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00017BBA File Offset: 0x00015DBA
		public float LengthSquared()
		{
			return this.X * this.X + this.Y * this.Y;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00017BD8 File Offset: 0x00015DD8
		public static Vector2 Zero
		{
			get
			{
				return default(Vector2);
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00017BEE File Offset: 0x00015DEE
		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			return new Vector2(a.X + b.X, a.Y + b.Y);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00017C0F File Offset: 0x00015E0F
		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			return new Vector2(a.X - b.X, a.Y - b.Y);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00017C30 File Offset: 0x00015E30
		public static Vector2 operator *(Vector2 a, Vector2 b)
		{
			return new Vector2(a.X * b.X, a.Y * b.Y);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00017C51 File Offset: 0x00015E51
		public static Vector2 operator /(Vector2 a, Vector2 b)
		{
			return new Vector2(a.X / b.X, a.Y / b.Y);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00017C72 File Offset: 0x00015E72
		public static Vector2 operator -(Vector2 a)
		{
			return new Vector2(-a.X, -a.Y);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00017C87 File Offset: 0x00015E87
		public static Vector2 operator *(Vector2 a, float d)
		{
			return new Vector2(a.X * d, a.Y * d);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00017C9E File Offset: 0x00015E9E
		public static Vector2 operator *(float d, Vector2 a)
		{
			return new Vector2(a.X * d, a.Y * d);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00017CB5 File Offset: 0x00015EB5
		public static Vector2 operator /(Vector2 a, float d)
		{
			return new Vector2(a.X / d, a.Y / d);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00017CCC File Offset: 0x00015ECC
		public static bool operator ==(Vector2 lhs, Vector2 rhs)
		{
			return (lhs - rhs).LengthSquared() < 9.9999994E-11f;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00017CEF File Offset: 0x00015EEF
		public static bool operator !=(Vector2 lhs, Vector2 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00017CFB File Offset: 0x00015EFB
		public static implicit operator Vector3(Vector2 v)
		{
			return new Vector3(v.X, v.Y, 0f);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00017D13 File Offset: 0x00015F13
		public static implicit operator Vector4(Vector2 v)
		{
			return new Vector4(v.X, v.Y, 0f, 0f);
		}

		// Token: 0x040009B0 RID: 2480
		public float X;

		// Token: 0x040009B1 RID: 2481
		public float Y;

		// Token: 0x040009B2 RID: 2482
		private const float kEpsilon = 1E-05f;
	}
}
