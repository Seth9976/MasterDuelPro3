using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200014C RID: 332
	[NativeHeader("Runtime/Math/Vector3.h")]
	[NativeClass("Vector3f")]
	[NativeType(Header = "Runtime/Math/Vector3.h")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[Il2CppEagerStaticClassConstruction]
	[NativeHeader("Runtime/Math/MathScripting.h")]
	public struct Vector3 : IEquatable<Vector3>, IFormattable
	{
		// Token: 0x06000DDA RID: 3546 RVA: 0x0001BF5C File Offset: 0x0001A15C
		[FreeFunction("VectorScripting::Slerp", IsThreadSafe = true)]
		public static Vector3 Slerp(Vector3 a, Vector3 b, float t)
		{
			Vector3 vector;
			Vector3.Slerp_Injected(ref a, ref b, t, out vector);
			return vector;
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0001BF78 File Offset: 0x0001A178
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
		{
			return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0001C038 File Offset: 0x0001A238
		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float omega = 2f / smoothTime;
			float x = omega * deltaTime;
			float exp = 1f / (1f + x + 0.48f * x * x + 0.235f * x * x * x);
			float change_x = current.x - target.x;
			float change_y = current.y - target.y;
			float change_z = current.z - target.z;
			Vector3 originalTo = target;
			float maxChange = maxSpeed * smoothTime;
			float maxChangeSq = maxChange * maxChange;
			float sqrmag = change_x * change_x + change_y * change_y + change_z * change_z;
			bool flag = sqrmag > maxChangeSq;
			if (flag)
			{
				float mag = (float)Math.Sqrt((double)sqrmag);
				change_x = change_x / mag * maxChange;
				change_y = change_y / mag * maxChange;
				change_z = change_z / mag * maxChange;
			}
			target.x = current.x - change_x;
			target.y = current.y - change_y;
			target.z = current.z - change_z;
			float temp_x = (currentVelocity.x + omega * change_x) * deltaTime;
			float temp_y = (currentVelocity.y + omega * change_y) * deltaTime;
			float temp_z = (currentVelocity.z + omega * change_z) * deltaTime;
			currentVelocity.x = (currentVelocity.x - omega * temp_x) * exp;
			currentVelocity.y = (currentVelocity.y - omega * temp_y) * exp;
			currentVelocity.z = (currentVelocity.z - omega * temp_z) * exp;
			float output_x = target.x + (change_x + temp_x) * exp;
			float output_y = target.y + (change_y + temp_y) * exp;
			float output_z = target.z + (change_z + temp_z) * exp;
			float origMinusCurrent_x = originalTo.x - current.x;
			float origMinusCurrent_y = originalTo.y - current.y;
			float origMinusCurrent_z = originalTo.z - current.z;
			float outMinusOrig_x = output_x - originalTo.x;
			float outMinusOrig_y = output_y - originalTo.y;
			float outMinusOrig_z = output_z - originalTo.z;
			bool flag2 = origMinusCurrent_x * outMinusOrig_x + origMinusCurrent_y * outMinusOrig_y + origMinusCurrent_z * outMinusOrig_z > 0f;
			if (flag2)
			{
				output_x = originalTo.x;
				output_y = originalTo.y;
				output_z = originalTo.z;
				currentVelocity.x = (output_x - originalTo.x) / deltaTime;
				currentVelocity.y = (output_y - originalTo.y) / deltaTime;
				currentVelocity.z = (output_z - originalTo.z) / deltaTime;
			}
			return new Vector3(output_x, output_y, output_z);
		}

		// Token: 0x17000246 RID: 582
		public float this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				float num;
				switch (index)
				{
				case 0:
					num = this.x;
					break;
				case 1:
					num = this.y;
					break;
				case 2:
					num = this.z;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
				return num;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				switch (index)
				{
				case 0:
					this.x = value;
					break;
				case 1:
					this.y = value;
					break;
				case 2:
					this.z = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
			}
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0001C35C File Offset: 0x0001A55C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0001C374 File Offset: 0x0001A574
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3(float x, float y)
		{
			this.x = x;
			this.y = y;
			this.z = 0f;
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0001C390 File Offset: 0x0001A590
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Scale(Vector3 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
			this.z *= scale.z;
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0001C3CC File Offset: 0x0001A5CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0001C434 File Offset: 0x0001A634
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2) ^ (this.z.GetHashCode() >> 2);
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0001C470 File Offset: 0x0001A670
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			Vector3 v;
			bool flag;
			if (other is Vector3)
			{
				v = (Vector3)other;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return flag2 && this.Equals(v);
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0001C4A4 File Offset: 0x0001A6A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Vector3 other)
		{
			return this.x == other.x && this.y == other.y && this.z == other.z;
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0001C4E4 File Offset: 0x0001A6E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Normalize(Vector3 value)
		{
			float mag = Vector3.Magnitude(value);
			bool flag = mag > 1E-05f;
			Vector3 vector;
			if (flag)
			{
				vector = value / mag;
			}
			else
			{
				vector = Vector3.zero;
			}
			return vector;
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0001C518 File Offset: 0x0001A718
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Normalize()
		{
			float mag = Vector3.Magnitude(this);
			bool flag = mag > 1E-05f;
			if (flag)
			{
				this /= mag;
			}
			else
			{
				this = Vector3.zero;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x0001C560 File Offset: 0x0001A760
		public Vector3 normalized
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.Normalize(this);
			}
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0001C580 File Offset: 0x0001A780
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Vector3 lhs, Vector3 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0001C5BC File Offset: 0x0001A7BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Project(Vector3 vector, Vector3 onNormal)
		{
			float sqrMag = Vector3.Dot(onNormal, onNormal);
			bool flag = sqrMag < Mathf.Epsilon;
			Vector3 vector2;
			if (flag)
			{
				vector2 = Vector3.zero;
			}
			else
			{
				float dot = Vector3.Dot(vector, onNormal);
				vector2 = new Vector3(onNormal.x * dot / sqrMag, onNormal.y * dot / sqrMag, onNormal.z * dot / sqrMag);
			}
			return vector2;
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0001C618 File Offset: 0x0001A818
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
		{
			float sqrMag = Vector3.Dot(planeNormal, planeNormal);
			bool flag = sqrMag < Mathf.Epsilon;
			Vector3 vector2;
			if (flag)
			{
				vector2 = vector;
			}
			else
			{
				float dot = Vector3.Dot(vector, planeNormal);
				vector2 = new Vector3(vector.x - planeNormal.x * dot / sqrMag, vector.y - planeNormal.y * dot / sqrMag, vector.z - planeNormal.z * dot / sqrMag);
			}
			return vector2;
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0001C684 File Offset: 0x0001A884
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Angle(Vector3 from, Vector3 to)
		{
			float denominator = (float)Math.Sqrt((double)(from.sqrMagnitude * to.sqrMagnitude));
			bool flag = denominator < 1E-15f;
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				float dot = Mathf.Clamp(Vector3.Dot(from, to) / denominator, -1f, 1f);
				num = (float)Math.Acos((double)dot) * 57.29578f;
			}
			return num;
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0001C6E8 File Offset: 0x0001A8E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float SignedAngle(Vector3 from, Vector3 to, Vector3 axis)
		{
			float unsignedAngle = Vector3.Angle(from, to);
			float cross_x = from.y * to.z - from.z * to.y;
			float cross_y = from.z * to.x - from.x * to.z;
			float cross_z = from.x * to.y - from.y * to.x;
			float sign = Mathf.Sign(axis.x * cross_x + axis.y * cross_y + axis.z * cross_z);
			return unsignedAngle * sign;
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0001C780 File Offset: 0x0001A980
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector3 a, Vector3 b)
		{
			float diff_x = a.x - b.x;
			float diff_y = a.y - b.y;
			float diff_z = a.z - b.z;
			return (float)Math.Sqrt((double)(diff_x * diff_x + diff_y * diff_y + diff_z * diff_z));
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0001C7D0 File Offset: 0x0001A9D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 ClampMagnitude(Vector3 vector, float maxLength)
		{
			float sqrmag = vector.sqrMagnitude;
			bool flag = sqrmag > maxLength * maxLength;
			Vector3 vector2;
			if (flag)
			{
				float mag = (float)Math.Sqrt((double)sqrmag);
				float normalized_x = vector.x / mag;
				float normalized_y = vector.y / mag;
				float normalized_z = vector.z / mag;
				vector2 = new Vector3(normalized_x * maxLength, normalized_y * maxLength, normalized_z * maxLength);
			}
			else
			{
				vector2 = vector;
			}
			return vector2;
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0001C834 File Offset: 0x0001AA34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Magnitude(Vector3 vector)
		{
			return (float)Math.Sqrt((double)(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z));
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0001C878 File Offset: 0x0001AA78
		public float magnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (float)Math.Sqrt((double)(this.x * this.x + this.y * this.y + this.z * this.z));
			}
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0001C8BC File Offset: 0x0001AABC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float SqrMagnitude(Vector3 vector)
		{
			return vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x0001C8F8 File Offset: 0x0001AAF8
		public float sqrMagnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.x * this.x + this.y * this.y + this.z * this.z;
			}
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0001C934 File Offset: 0x0001AB34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Min(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0001C980 File Offset: 0x0001AB80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Max(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x0001C9CC File Offset: 0x0001ABCC
		public static Vector3 zero
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.zeroVector;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0001C9E4 File Offset: 0x0001ABE4
		public static Vector3 one
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.oneVector;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x0001C9FC File Offset: 0x0001ABFC
		public static Vector3 forward
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.forwardVector;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x0001CA14 File Offset: 0x0001AC14
		public static Vector3 back
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.backVector;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x0001CA2C File Offset: 0x0001AC2C
		public static Vector3 up
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.upVector;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x0001CA44 File Offset: 0x0001AC44
		public static Vector3 down
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.downVector;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x0001CA5C File Offset: 0x0001AC5C
		public static Vector3 left
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.leftVector;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x0001CA74 File Offset: 0x0001AC74
		public static Vector3 right
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.rightVector;
			}
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0001CA8C File Offset: 0x0001AC8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0001CACC File Offset: 0x0001ACCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0001CB0C File Offset: 0x0001AD0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator -(Vector3 a)
		{
			return new Vector3(-a.x, -a.y, -a.z);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0001CB38 File Offset: 0x0001AD38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator *(Vector3 a, float d)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0001CB68 File Offset: 0x0001AD68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator *(float d, Vector3 a)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0001CB98 File Offset: 0x0001AD98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator /(Vector3 a, float d)
		{
			return new Vector3(a.x / d, a.y / d, a.z / d);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0001CBC8 File Offset: 0x0001ADC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector3 lhs, Vector3 rhs)
		{
			float diff_x = lhs.x - rhs.x;
			float diff_y = lhs.y - rhs.y;
			float diff_z = lhs.z - rhs.z;
			float sqrmag = diff_x * diff_x + diff_y * diff_y + diff_z * diff_z;
			return sqrmag < 9.9999994E-11f;
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0001CC1C File Offset: 0x0001AE1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector3 lhs, Vector3 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0001CC38 File Offset: 0x0001AE38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0001CC54 File Offset: 0x0001AE54
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
			return UnityString.Format("({0}, {1}, {2})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x06000E0A RID: 3594
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Slerp_Injected([In] ref Vector3 a, [In] ref Vector3 b, float t, out Vector3 ret);

		// Token: 0x04000598 RID: 1432
		public const float kEpsilon = 1E-05f;

		// Token: 0x04000599 RID: 1433
		public const float kEpsilonNormalSqrt = 1E-15f;

		// Token: 0x0400059A RID: 1434
		public float x;

		// Token: 0x0400059B RID: 1435
		public float y;

		// Token: 0x0400059C RID: 1436
		public float z;

		// Token: 0x0400059D RID: 1437
		private static readonly Vector3 zeroVector = new Vector3(0f, 0f, 0f);

		// Token: 0x0400059E RID: 1438
		private static readonly Vector3 oneVector = new Vector3(1f, 1f, 1f);

		// Token: 0x0400059F RID: 1439
		private static readonly Vector3 upVector = new Vector3(0f, 1f, 0f);

		// Token: 0x040005A0 RID: 1440
		private static readonly Vector3 downVector = new Vector3(0f, -1f, 0f);

		// Token: 0x040005A1 RID: 1441
		private static readonly Vector3 leftVector = new Vector3(-1f, 0f, 0f);

		// Token: 0x040005A2 RID: 1442
		private static readonly Vector3 rightVector = new Vector3(1f, 0f, 0f);

		// Token: 0x040005A3 RID: 1443
		private static readonly Vector3 forwardVector = new Vector3(0f, 0f, 1f);

		// Token: 0x040005A4 RID: 1444
		private static readonly Vector3 backVector = new Vector3(0f, 0f, -1f);

		// Token: 0x040005A5 RID: 1445
		private static readonly Vector3 positiveInfinityVector = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

		// Token: 0x040005A6 RID: 1446
		private static readonly Vector3 negativeInfinityVector = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
	}
}
