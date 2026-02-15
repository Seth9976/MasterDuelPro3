using System;
using System.Runtime.InteropServices;

namespace AssetStudio
{
	// Token: 0x0200016F RID: 367
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct Matrix4x4 : IEquatable<Matrix4x4>
	{
		// Token: 0x060004FE RID: 1278 RVA: 0x00016C74 File Offset: 0x00014E74
		public Matrix4x4(float[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (values.Length != 16)
			{
				throw new ArgumentOutOfRangeException("values", "There must be sixteen and only sixteen input values for Matrix.");
			}
			this.M00 = values[0];
			this.M10 = values[1];
			this.M20 = values[2];
			this.M30 = values[3];
			this.M01 = values[4];
			this.M11 = values[5];
			this.M21 = values[6];
			this.M31 = values[7];
			this.M02 = values[8];
			this.M12 = values[9];
			this.M22 = values[10];
			this.M32 = values[11];
			this.M03 = values[12];
			this.M13 = values[13];
			this.M23 = values[14];
			this.M33 = values[15];
		}

		// Token: 0x17000079 RID: 121
		public float this[int row, int column]
		{
			get
			{
				return this[row + column * 4];
			}
			set
			{
				this[row + column * 4] = value;
			}
		}

		// Token: 0x1700007A RID: 122
		public float this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.M00;
				case 1:
					return this.M10;
				case 2:
					return this.M20;
				case 3:
					return this.M30;
				case 4:
					return this.M01;
				case 5:
					return this.M11;
				case 6:
					return this.M21;
				case 7:
					return this.M31;
				case 8:
					return this.M02;
				case 9:
					return this.M12;
				case 10:
					return this.M22;
				case 11:
					return this.M32;
				case 12:
					return this.M03;
				case 13:
					return this.M13;
				case 14:
					return this.M23;
				case 15:
					return this.M33;
				default:
					throw new ArgumentOutOfRangeException("index", "Invalid Matrix4x4 index!");
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this.M00 = value;
					return;
				case 1:
					this.M10 = value;
					return;
				case 2:
					this.M20 = value;
					return;
				case 3:
					this.M30 = value;
					return;
				case 4:
					this.M01 = value;
					return;
				case 5:
					this.M11 = value;
					return;
				case 6:
					this.M21 = value;
					return;
				case 7:
					this.M31 = value;
					return;
				case 8:
					this.M02 = value;
					return;
				case 9:
					this.M12 = value;
					return;
				case 10:
					this.M22 = value;
					return;
				case 11:
					this.M32 = value;
					return;
				case 12:
					this.M03 = value;
					return;
				case 13:
					this.M13 = value;
					return;
				case 14:
					this.M23 = value;
					return;
				case 15:
					this.M33 = value;
					return;
				default:
					throw new ArgumentOutOfRangeException("index", "Invalid Matrix4x4 index!");
				}
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00016F14 File Offset: 0x00015114
		public override int GetHashCode()
		{
			return this.GetColumn(0).GetHashCode() ^ (this.GetColumn(1).GetHashCode() << 2) ^ (this.GetColumn(2).GetHashCode() >> 2) ^ (this.GetColumn(3).GetHashCode() >> 1);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00016F7E File Offset: 0x0001517E
		public override bool Equals(object other)
		{
			return other is Matrix4x4 && this.Equals((Matrix4x4)other);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00016F98 File Offset: 0x00015198
		public bool Equals(Matrix4x4 other)
		{
			return this.GetColumn(0).Equals(other.GetColumn(0)) && this.GetColumn(1).Equals(other.GetColumn(1)) && this.GetColumn(2).Equals(other.GetColumn(2)) && this.GetColumn(3).Equals(other.GetColumn(3));
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001700C File Offset: 0x0001520C
		public Vector4 GetColumn(int index)
		{
			switch (index)
			{
			case 0:
				return new Vector4(this.M00, this.M10, this.M20, this.M30);
			case 1:
				return new Vector4(this.M01, this.M11, this.M21, this.M31);
			case 2:
				return new Vector4(this.M02, this.M12, this.M22, this.M32);
			case 3:
				return new Vector4(this.M03, this.M13, this.M23, this.M33);
			default:
				throw new IndexOutOfRangeException("Invalid column index!");
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x000170B4 File Offset: 0x000152B4
		public Vector4 GetRow(int index)
		{
			switch (index)
			{
			case 0:
				return new Vector4(this.M00, this.M01, this.M02, this.M03);
			case 1:
				return new Vector4(this.M10, this.M11, this.M12, this.M13);
			case 2:
				return new Vector4(this.M20, this.M21, this.M22, this.M23);
			case 3:
				return new Vector4(this.M30, this.M31, this.M32, this.M33);
			default:
				throw new IndexOutOfRangeException("Invalid row index!");
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001715C File Offset: 0x0001535C
		public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			Matrix4x4 res;
			res.M00 = lhs.M00 * rhs.M00 + lhs.M01 * rhs.M10 + lhs.M02 * rhs.M20 + lhs.M03 * rhs.M30;
			res.M01 = lhs.M00 * rhs.M01 + lhs.M01 * rhs.M11 + lhs.M02 * rhs.M21 + lhs.M03 * rhs.M31;
			res.M02 = lhs.M00 * rhs.M02 + lhs.M01 * rhs.M12 + lhs.M02 * rhs.M22 + lhs.M03 * rhs.M32;
			res.M03 = lhs.M00 * rhs.M03 + lhs.M01 * rhs.M13 + lhs.M02 * rhs.M23 + lhs.M03 * rhs.M33;
			res.M10 = lhs.M10 * rhs.M00 + lhs.M11 * rhs.M10 + lhs.M12 * rhs.M20 + lhs.M13 * rhs.M30;
			res.M11 = lhs.M10 * rhs.M01 + lhs.M11 * rhs.M11 + lhs.M12 * rhs.M21 + lhs.M13 * rhs.M31;
			res.M12 = lhs.M10 * rhs.M02 + lhs.M11 * rhs.M12 + lhs.M12 * rhs.M22 + lhs.M13 * rhs.M32;
			res.M13 = lhs.M10 * rhs.M03 + lhs.M11 * rhs.M13 + lhs.M12 * rhs.M23 + lhs.M13 * rhs.M33;
			res.M20 = lhs.M20 * rhs.M00 + lhs.M21 * rhs.M10 + lhs.M22 * rhs.M20 + lhs.M23 * rhs.M30;
			res.M21 = lhs.M20 * rhs.M01 + lhs.M21 * rhs.M11 + lhs.M22 * rhs.M21 + lhs.M23 * rhs.M31;
			res.M22 = lhs.M20 * rhs.M02 + lhs.M21 * rhs.M12 + lhs.M22 * rhs.M22 + lhs.M23 * rhs.M32;
			res.M23 = lhs.M20 * rhs.M03 + lhs.M21 * rhs.M13 + lhs.M22 * rhs.M23 + lhs.M23 * rhs.M33;
			res.M30 = lhs.M30 * rhs.M00 + lhs.M31 * rhs.M10 + lhs.M32 * rhs.M20 + lhs.M33 * rhs.M30;
			res.M31 = lhs.M30 * rhs.M01 + lhs.M31 * rhs.M11 + lhs.M32 * rhs.M21 + lhs.M33 * rhs.M31;
			res.M32 = lhs.M30 * rhs.M02 + lhs.M31 * rhs.M12 + lhs.M32 * rhs.M22 + lhs.M33 * rhs.M32;
			res.M33 = lhs.M30 * rhs.M03 + lhs.M31 * rhs.M13 + lhs.M32 * rhs.M23 + lhs.M33 * rhs.M33;
			return res;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001754C File Offset: 0x0001574C
		public static bool operator ==(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			return lhs.GetColumn(0) == rhs.GetColumn(0) && lhs.GetColumn(1) == rhs.GetColumn(1) && lhs.GetColumn(2) == rhs.GetColumn(2) && lhs.GetColumn(3) == rhs.GetColumn(3);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000175B5 File Offset: 0x000157B5
		public static bool operator !=(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000175C4 File Offset: 0x000157C4
		public static Matrix4x4 Scale(Vector3 vector)
		{
			Matrix4x4 i;
			i.M00 = vector.X;
			i.M01 = 0f;
			i.M02 = 0f;
			i.M03 = 0f;
			i.M10 = 0f;
			i.M11 = vector.Y;
			i.M12 = 0f;
			i.M13 = 0f;
			i.M20 = 0f;
			i.M21 = 0f;
			i.M22 = vector.Z;
			i.M23 = 0f;
			i.M30 = 0f;
			i.M31 = 0f;
			i.M32 = 0f;
			i.M33 = 1f;
			return i;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00017698 File Offset: 0x00015898
		public static Matrix4x4 Translate(Vector3 vector)
		{
			Matrix4x4 i;
			i.M00 = 1f;
			i.M01 = 0f;
			i.M02 = 0f;
			i.M03 = vector.X;
			i.M10 = 0f;
			i.M11 = 1f;
			i.M12 = 0f;
			i.M13 = vector.Y;
			i.M20 = 0f;
			i.M21 = 0f;
			i.M22 = 1f;
			i.M23 = vector.Z;
			i.M30 = 0f;
			i.M31 = 0f;
			i.M32 = 0f;
			i.M33 = 1f;
			return i;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001776C File Offset: 0x0001596C
		public static Matrix4x4 Rotate(Quaternion q)
		{
			float x = q.X * 2f;
			float y = q.Y * 2f;
			float z = q.Z * 2f;
			float xx = q.X * x;
			float yy = q.Y * y;
			float zz = q.Z * z;
			float xy = q.X * y;
			float xz = q.X * z;
			float yz = q.Y * z;
			float wx = q.W * x;
			float wy = q.W * y;
			float wz = q.W * z;
			Matrix4x4 i;
			i.M00 = 1f - (yy + zz);
			i.M10 = xy + wz;
			i.M20 = xz - wy;
			i.M30 = 0f;
			i.M01 = xy - wz;
			i.M11 = 1f - (xx + zz);
			i.M21 = yz + wx;
			i.M31 = 0f;
			i.M02 = xz + wy;
			i.M12 = yz - wx;
			i.M22 = 1f - (xx + yy);
			i.M32 = 0f;
			i.M03 = 0f;
			i.M13 = 0f;
			i.M23 = 0f;
			i.M33 = 1f;
			return i;
		}

		// Token: 0x0400099B RID: 2459
		public float M00;

		// Token: 0x0400099C RID: 2460
		public float M10;

		// Token: 0x0400099D RID: 2461
		public float M20;

		// Token: 0x0400099E RID: 2462
		public float M30;

		// Token: 0x0400099F RID: 2463
		public float M01;

		// Token: 0x040009A0 RID: 2464
		public float M11;

		// Token: 0x040009A1 RID: 2465
		public float M21;

		// Token: 0x040009A2 RID: 2466
		public float M31;

		// Token: 0x040009A3 RID: 2467
		public float M02;

		// Token: 0x040009A4 RID: 2468
		public float M12;

		// Token: 0x040009A5 RID: 2469
		public float M22;

		// Token: 0x040009A6 RID: 2470
		public float M32;

		// Token: 0x040009A7 RID: 2471
		public float M03;

		// Token: 0x040009A8 RID: 2472
		public float M13;

		// Token: 0x040009A9 RID: 2473
		public float M23;

		// Token: 0x040009AA RID: 2474
		public float M33;
	}
}
