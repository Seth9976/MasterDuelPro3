using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200014B RID: 331
	[NativeType(Header = "Runtime/Math/Matrix4x4.h")]
	[NativeHeader("Runtime/Math/MathScripting.h")]
	[Il2CppEagerStaticClassConstruction]
	[NativeClass("Matrix4x4f")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct Matrix4x4 : IEquatable<Matrix4x4>, IFormattable
	{
		// Token: 0x06000DA4 RID: 3492 RVA: 0x0001A9E8 File Offset: 0x00018BE8
		[ThreadSafe]
		private Quaternion GetRotation()
		{
			Quaternion quaternion;
			Matrix4x4.GetRotation_Injected(ref this, out quaternion);
			return quaternion;
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0001AA00 File Offset: 0x00018C00
		[ThreadSafe]
		private Vector3 GetLossyScale()
		{
			Vector3 vector;
			Matrix4x4.GetLossyScale_Injected(ref this, out vector);
			return vector;
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0001AA18 File Offset: 0x00018C18
		[ThreadSafe]
		private FrustumPlanes DecomposeProjection()
		{
			FrustumPlanes frustumPlanes;
			Matrix4x4.DecomposeProjection_Injected(ref this, out frustumPlanes);
			return frustumPlanes;
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x0001AA30 File Offset: 0x00018C30
		public Quaternion rotation
		{
			get
			{
				return this.GetRotation();
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x0001AA48 File Offset: 0x00018C48
		public Vector3 lossyScale
		{
			get
			{
				return this.GetLossyScale();
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x0001AA60 File Offset: 0x00018C60
		public FrustumPlanes decomposeProjection
		{
			get
			{
				return this.DecomposeProjection();
			}
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0001AA78 File Offset: 0x00018C78
		[FreeFunction("MatrixScripting::TRS", IsThreadSafe = true)]
		public static Matrix4x4 TRS(Vector3 pos, Quaternion q, Vector3 s)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.TRS_Injected(ref pos, ref q, ref s, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0001AA94 File Offset: 0x00018C94
		[FreeFunction("MatrixScripting::Inverse3DAffine", IsThreadSafe = true)]
		public static bool Inverse3DAffine(Matrix4x4 input, ref Matrix4x4 result)
		{
			return Matrix4x4.Inverse3DAffine_Injected(ref input, ref result);
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0001AAAC File Offset: 0x00018CAC
		[FreeFunction("MatrixScripting::Inverse", IsThreadSafe = true)]
		public static Matrix4x4 Inverse(Matrix4x4 m)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Inverse_Injected(ref m, out matrix4x);
			return matrix4x;
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x0001AAC4 File Offset: 0x00018CC4
		public Matrix4x4 inverse
		{
			get
			{
				return Matrix4x4.Inverse(this);
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0001AAE4 File Offset: 0x00018CE4
		[FreeFunction("MatrixScripting::Transpose", IsThreadSafe = true)]
		public static Matrix4x4 Transpose(Matrix4x4 m)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Transpose_Injected(ref m, out matrix4x);
			return matrix4x;
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x0001AAFC File Offset: 0x00018CFC
		public Matrix4x4 transpose
		{
			get
			{
				return Matrix4x4.Transpose(this);
			}
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0001AB1C File Offset: 0x00018D1C
		[FreeFunction("MatrixScripting::Ortho", IsThreadSafe = true)]
		public static Matrix4x4 Ortho(float left, float right, float bottom, float top, float zNear, float zFar)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Ortho_Injected(left, right, bottom, top, zNear, zFar, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0001AB3C File Offset: 0x00018D3C
		[FreeFunction("MatrixScripting::Perspective", IsThreadSafe = true)]
		public static Matrix4x4 Perspective(float fov, float aspect, float zNear, float zFar)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Perspective_Injected(fov, aspect, zNear, zFar, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0001AB58 File Offset: 0x00018D58
		[FreeFunction("MatrixScripting::LookAt", IsThreadSafe = true)]
		public static Matrix4x4 LookAt(Vector3 from, Vector3 to, Vector3 up)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.LookAt_Injected(ref from, ref to, ref up, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0001AB74 File Offset: 0x00018D74
		[FreeFunction("MatrixScripting::Frustum", IsThreadSafe = true)]
		public static Matrix4x4 Frustum(float left, float right, float bottom, float top, float zNear, float zFar)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Frustum_Injected(left, right, bottom, top, zNear, zFar, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0001AB94 File Offset: 0x00018D94
		public static Matrix4x4 Frustum(FrustumPlanes fp)
		{
			return Matrix4x4.Frustum(fp.left, fp.right, fp.bottom, fp.top, fp.zNear, fp.zFar);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0001ABD0 File Offset: 0x00018DD0
		public Matrix4x4(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
		{
			this.m00 = column0.x;
			this.m01 = column1.x;
			this.m02 = column2.x;
			this.m03 = column3.x;
			this.m10 = column0.y;
			this.m11 = column1.y;
			this.m12 = column2.y;
			this.m13 = column3.y;
			this.m20 = column0.z;
			this.m21 = column1.z;
			this.m22 = column2.z;
			this.m23 = column3.z;
			this.m30 = column0.w;
			this.m31 = column1.w;
			this.m32 = column2.w;
			this.m33 = column3.w;
		}

		// Token: 0x17000242 RID: 578
		public float this[int row, int column]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this[row + column * 4];
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this[row + column * 4] = value;
			}
		}

		// Token: 0x17000243 RID: 579
		public float this[int index]
		{
			get
			{
				float num;
				switch (index)
				{
				case 0:
					num = this.m00;
					break;
				case 1:
					num = this.m10;
					break;
				case 2:
					num = this.m20;
					break;
				case 3:
					num = this.m30;
					break;
				case 4:
					num = this.m01;
					break;
				case 5:
					num = this.m11;
					break;
				case 6:
					num = this.m21;
					break;
				case 7:
					num = this.m31;
					break;
				case 8:
					num = this.m02;
					break;
				case 9:
					num = this.m12;
					break;
				case 10:
					num = this.m22;
					break;
				case 11:
					num = this.m32;
					break;
				case 12:
					num = this.m03;
					break;
				case 13:
					num = this.m13;
					break;
				case 14:
					num = this.m23;
					break;
				case 15:
					num = this.m33;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid matrix index!");
				}
				return num;
			}
			set
			{
				switch (index)
				{
				case 0:
					this.m00 = value;
					break;
				case 1:
					this.m10 = value;
					break;
				case 2:
					this.m20 = value;
					break;
				case 3:
					this.m30 = value;
					break;
				case 4:
					this.m01 = value;
					break;
				case 5:
					this.m11 = value;
					break;
				case 6:
					this.m21 = value;
					break;
				case 7:
					this.m31 = value;
					break;
				case 8:
					this.m02 = value;
					break;
				case 9:
					this.m12 = value;
					break;
				case 10:
					this.m22 = value;
					break;
				case 11:
					this.m32 = value;
					break;
				case 12:
					this.m03 = value;
					break;
				case 13:
					this.m13 = value;
					break;
				case 14:
					this.m23 = value;
					break;
				case 15:
					this.m33 = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid matrix index!");
				}
			}
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0001AEDC File Offset: 0x000190DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return this.GetColumn(0).GetHashCode() ^ (this.GetColumn(1).GetHashCode() << 2) ^ (this.GetColumn(2).GetHashCode() >> 2) ^ (this.GetColumn(3).GetHashCode() >> 1);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0001AF4C File Offset: 0x0001914C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			Matrix4x4 i;
			bool flag;
			if (other is Matrix4x4)
			{
				i = (Matrix4x4)other;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return flag2 && this.Equals(i);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0001AF80 File Offset: 0x00019180
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Matrix4x4 other)
		{
			return this.GetColumn(0).Equals(other.GetColumn(0)) && this.GetColumn(1).Equals(other.GetColumn(1)) && this.GetColumn(2).Equals(other.GetColumn(2)) && this.GetColumn(3).Equals(other.GetColumn(3));
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0001AFF8 File Offset: 0x000191F8
		public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			Matrix4x4 res;
			res.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20 + lhs.m03 * rhs.m30;
			res.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21 + lhs.m03 * rhs.m31;
			res.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22 + lhs.m03 * rhs.m32;
			res.m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03 * rhs.m33;
			res.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20 + lhs.m13 * rhs.m30;
			res.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31;
			res.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32;
			res.m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33;
			res.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20 + lhs.m23 * rhs.m30;
			res.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31;
			res.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32;
			res.m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33;
			res.m30 = lhs.m30 * rhs.m00 + lhs.m31 * rhs.m10 + lhs.m32 * rhs.m20 + lhs.m33 * rhs.m30;
			res.m31 = lhs.m30 * rhs.m01 + lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31;
			res.m32 = lhs.m30 * rhs.m02 + lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32;
			res.m33 = lhs.m30 * rhs.m03 + lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33;
			return res;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0001B3EC File Offset: 0x000195EC
		public static Vector4 operator *(Matrix4x4 lhs, Vector4 vector)
		{
			Vector4 res;
			res.x = lhs.m00 * vector.x + lhs.m01 * vector.y + lhs.m02 * vector.z + lhs.m03 * vector.w;
			res.y = lhs.m10 * vector.x + lhs.m11 * vector.y + lhs.m12 * vector.z + lhs.m13 * vector.w;
			res.z = lhs.m20 * vector.x + lhs.m21 * vector.y + lhs.m22 * vector.z + lhs.m23 * vector.w;
			res.w = lhs.m30 * vector.x + lhs.m31 * vector.y + lhs.m32 * vector.z + lhs.m33 * vector.w;
			return res;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0001B4F8 File Offset: 0x000196F8
		public static bool operator ==(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			return lhs.GetColumn(0) == rhs.GetColumn(0) && lhs.GetColumn(1) == rhs.GetColumn(1) && lhs.GetColumn(2) == rhs.GetColumn(2) && lhs.GetColumn(3) == rhs.GetColumn(3);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0001B568 File Offset: 0x00019768
		public Vector4 GetColumn(int index)
		{
			Vector4 vector;
			switch (index)
			{
			case 0:
				vector = new Vector4(this.m00, this.m10, this.m20, this.m30);
				break;
			case 1:
				vector = new Vector4(this.m01, this.m11, this.m21, this.m31);
				break;
			case 2:
				vector = new Vector4(this.m02, this.m12, this.m22, this.m32);
				break;
			case 3:
				vector = new Vector4(this.m03, this.m13, this.m23, this.m33);
				break;
			default:
				throw new IndexOutOfRangeException("Invalid column index!");
			}
			return vector;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0001B624 File Offset: 0x00019824
		public Vector4 GetRow(int index)
		{
			Vector4 vector;
			switch (index)
			{
			case 0:
				vector = new Vector4(this.m00, this.m01, this.m02, this.m03);
				break;
			case 1:
				vector = new Vector4(this.m10, this.m11, this.m12, this.m13);
				break;
			case 2:
				vector = new Vector4(this.m20, this.m21, this.m22, this.m23);
				break;
			case 3:
				vector = new Vector4(this.m30, this.m31, this.m32, this.m33);
				break;
			default:
				throw new IndexOutOfRangeException("Invalid row index!");
			}
			return vector;
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0001B6E0 File Offset: 0x000198E0
		public Vector3 GetPosition()
		{
			return new Vector3(this.m03, this.m13, this.m23);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0001B709 File Offset: 0x00019909
		public void SetColumn(int index, Vector4 column)
		{
			this[0, index] = column.x;
			this[1, index] = column.y;
			this[2, index] = column.z;
			this[3, index] = column.w;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0001B748 File Offset: 0x00019948
		public Vector3 MultiplyPoint(Vector3 point)
		{
			Vector3 res;
			res.x = this.m00 * point.x + this.m01 * point.y + this.m02 * point.z + this.m03;
			res.y = this.m10 * point.x + this.m11 * point.y + this.m12 * point.z + this.m13;
			res.z = this.m20 * point.x + this.m21 * point.y + this.m22 * point.z + this.m23;
			float w = this.m30 * point.x + this.m31 * point.y + this.m32 * point.z + this.m33;
			w = 1f / w;
			res.x *= w;
			res.y *= w;
			res.z *= w;
			return res;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0001B860 File Offset: 0x00019A60
		public Vector3 MultiplyPoint3x4(Vector3 point)
		{
			Vector3 res;
			res.x = this.m00 * point.x + this.m01 * point.y + this.m02 * point.z + this.m03;
			res.y = this.m10 * point.x + this.m11 * point.y + this.m12 * point.z + this.m13;
			res.z = this.m20 * point.x + this.m21 * point.y + this.m22 * point.z + this.m23;
			return res;
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0001B918 File Offset: 0x00019B18
		public Vector3 MultiplyVector(Vector3 vector)
		{
			Vector3 res;
			res.x = this.m00 * vector.x + this.m01 * vector.y + this.m02 * vector.z;
			res.y = this.m10 * vector.x + this.m11 * vector.y + this.m12 * vector.z;
			res.z = this.m20 * vector.x + this.m21 * vector.y + this.m22 * vector.z;
			return res;
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0001B9BC File Offset: 0x00019BBC
		public static Matrix4x4 Scale(Vector3 vector)
		{
			Matrix4x4 i;
			i.m00 = vector.x;
			i.m01 = 0f;
			i.m02 = 0f;
			i.m03 = 0f;
			i.m10 = 0f;
			i.m11 = vector.y;
			i.m12 = 0f;
			i.m13 = 0f;
			i.m20 = 0f;
			i.m21 = 0f;
			i.m22 = vector.z;
			i.m23 = 0f;
			i.m30 = 0f;
			i.m31 = 0f;
			i.m32 = 0f;
			i.m33 = 1f;
			return i;
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0001BA94 File Offset: 0x00019C94
		public static Matrix4x4 Translate(Vector3 vector)
		{
			Matrix4x4 i;
			i.m00 = 1f;
			i.m01 = 0f;
			i.m02 = 0f;
			i.m03 = vector.x;
			i.m10 = 0f;
			i.m11 = 1f;
			i.m12 = 0f;
			i.m13 = vector.y;
			i.m20 = 0f;
			i.m21 = 0f;
			i.m22 = 1f;
			i.m23 = vector.z;
			i.m30 = 0f;
			i.m31 = 0f;
			i.m32 = 0f;
			i.m33 = 1f;
			return i;
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0001BB6C File Offset: 0x00019D6C
		public static Matrix4x4 Rotate(Quaternion q)
		{
			float x = q.x * 2f;
			float y = q.y * 2f;
			float z = q.z * 2f;
			float xx = q.x * x;
			float yy = q.y * y;
			float zz = q.z * z;
			float xy = q.x * y;
			float xz = q.x * z;
			float yz = q.y * z;
			float wx = q.w * x;
			float wy = q.w * y;
			float wz = q.w * z;
			Matrix4x4 i;
			i.m00 = 1f - (yy + zz);
			i.m10 = xy + wz;
			i.m20 = xz - wy;
			i.m30 = 0f;
			i.m01 = xy - wz;
			i.m11 = 1f - (xx + zz);
			i.m21 = yz + wx;
			i.m31 = 0f;
			i.m02 = xz + wy;
			i.m12 = yz - wx;
			i.m22 = 1f - (xx + yy);
			i.m32 = 0f;
			i.m03 = 0f;
			i.m13 = 0f;
			i.m23 = 0f;
			i.m33 = 1f;
			return i;
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0001BCD4 File Offset: 0x00019ED4
		public static Matrix4x4 zero
		{
			get
			{
				return Matrix4x4.zeroMatrix;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x0001BCEC File Offset: 0x00019EEC
		public static Matrix4x4 identity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Matrix4x4.identityMatrix;
			}
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0001BD04 File Offset: 0x00019F04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0001BD20 File Offset: 0x00019F20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F5";
			}
			bool flag2 = formatProvider == null;
			if (flag2)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("{0}\t{1}\t{2}\t{3}\n{4}\t{5}\t{6}\t{7}\n{8}\t{9}\t{10}\t{11}\n{12}\t{13}\t{14}\t{15}\n", new object[]
			{
				this.m00.ToString(format, formatProvider),
				this.m01.ToString(format, formatProvider),
				this.m02.ToString(format, formatProvider),
				this.m03.ToString(format, formatProvider),
				this.m10.ToString(format, formatProvider),
				this.m11.ToString(format, formatProvider),
				this.m12.ToString(format, formatProvider),
				this.m13.ToString(format, formatProvider),
				this.m20.ToString(format, formatProvider),
				this.m21.ToString(format, formatProvider),
				this.m22.ToString(format, formatProvider),
				this.m23.ToString(format, formatProvider),
				this.m30.ToString(format, formatProvider),
				this.m31.ToString(format, formatProvider),
				this.m32.ToString(format, formatProvider),
				this.m33.ToString(format, formatProvider)
			});
		}

		// Token: 0x06000DCF RID: 3535
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRotation_Injected(ref Matrix4x4 _unity_self, out Quaternion ret);

		// Token: 0x06000DD0 RID: 3536
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLossyScale_Injected(ref Matrix4x4 _unity_self, out Vector3 ret);

		// Token: 0x06000DD1 RID: 3537
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DecomposeProjection_Injected(ref Matrix4x4 _unity_self, out FrustumPlanes ret);

		// Token: 0x06000DD2 RID: 3538
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TRS_Injected([In] ref Vector3 pos, [In] ref Quaternion q, [In] ref Vector3 s, out Matrix4x4 ret);

		// Token: 0x06000DD3 RID: 3539
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Inverse3DAffine_Injected([In] ref Matrix4x4 input, ref Matrix4x4 result);

		// Token: 0x06000DD4 RID: 3540
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Inverse_Injected([In] ref Matrix4x4 m, out Matrix4x4 ret);

		// Token: 0x06000DD5 RID: 3541
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Transpose_Injected([In] ref Matrix4x4 m, out Matrix4x4 ret);

		// Token: 0x06000DD6 RID: 3542
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Ortho_Injected(float left, float right, float bottom, float top, float zNear, float zFar, out Matrix4x4 ret);

		// Token: 0x06000DD7 RID: 3543
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Perspective_Injected(float fov, float aspect, float zNear, float zFar, out Matrix4x4 ret);

		// Token: 0x06000DD8 RID: 3544
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LookAt_Injected([In] ref Vector3 from, [In] ref Vector3 to, [In] ref Vector3 up, out Matrix4x4 ret);

		// Token: 0x06000DD9 RID: 3545
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Frustum_Injected(float left, float right, float bottom, float top, float zNear, float zFar, out Matrix4x4 ret);

		// Token: 0x04000586 RID: 1414
		[NativeName("m_Data[0]")]
		public float m00;

		// Token: 0x04000587 RID: 1415
		[NativeName("m_Data[1]")]
		public float m10;

		// Token: 0x04000588 RID: 1416
		[NativeName("m_Data[2]")]
		public float m20;

		// Token: 0x04000589 RID: 1417
		[NativeName("m_Data[3]")]
		public float m30;

		// Token: 0x0400058A RID: 1418
		[NativeName("m_Data[4]")]
		public float m01;

		// Token: 0x0400058B RID: 1419
		[NativeName("m_Data[5]")]
		public float m11;

		// Token: 0x0400058C RID: 1420
		[NativeName("m_Data[6]")]
		public float m21;

		// Token: 0x0400058D RID: 1421
		[NativeName("m_Data[7]")]
		public float m31;

		// Token: 0x0400058E RID: 1422
		[NativeName("m_Data[8]")]
		public float m02;

		// Token: 0x0400058F RID: 1423
		[NativeName("m_Data[9]")]
		public float m12;

		// Token: 0x04000590 RID: 1424
		[NativeName("m_Data[10]")]
		public float m22;

		// Token: 0x04000591 RID: 1425
		[NativeName("m_Data[11]")]
		public float m32;

		// Token: 0x04000592 RID: 1426
		[NativeName("m_Data[12]")]
		public float m03;

		// Token: 0x04000593 RID: 1427
		[NativeName("m_Data[13]")]
		public float m13;

		// Token: 0x04000594 RID: 1428
		[NativeName("m_Data[14]")]
		public float m23;

		// Token: 0x04000595 RID: 1429
		[NativeName("m_Data[15]")]
		public float m33;

		// Token: 0x04000596 RID: 1430
		private static readonly Matrix4x4 zeroMatrix = new Matrix4x4(new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f));

		// Token: 0x04000597 RID: 1431
		private static readonly Matrix4x4 identityMatrix = new Matrix4x4(new Vector4(1f, 0f, 0f, 0f), new Vector4(0f, 1f, 0f, 0f), new Vector4(0f, 0f, 1f, 0f), new Vector4(0f, 0f, 0f, 1f));
	}
}
