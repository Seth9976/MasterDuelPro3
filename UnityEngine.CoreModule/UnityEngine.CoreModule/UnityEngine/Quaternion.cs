using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200014D RID: 333
	[Il2CppEagerStaticClassConstruction]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Math/MathScripting.h")]
	[DefaultMember("Item")]
	[NativeType(Header = "Runtime/Math/Quaternion.h")]
	public struct Quaternion : IEquatable<Quaternion>, IFormattable
	{
		// Token: 0x06000E0B RID: 3595 RVA: 0x0001CDD4 File Offset: 0x0001AFD4
		[FreeFunction("FromToQuaternionSafe", IsThreadSafe = true)]
		public static Quaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
		{
			Quaternion quaternion;
			Quaternion.FromToRotation_Injected(ref fromDirection, ref toDirection, out quaternion);
			return quaternion;
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0001CDF0 File Offset: 0x0001AFF0
		[FreeFunction(IsThreadSafe = true)]
		public static Quaternion Inverse(Quaternion rotation)
		{
			Quaternion quaternion;
			Quaternion.Inverse_Injected(ref rotation, out quaternion);
			return quaternion;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0001CE08 File Offset: 0x0001B008
		[FreeFunction("QuaternionScripting::Slerp", IsThreadSafe = true)]
		public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
		{
			Quaternion quaternion;
			Quaternion.Slerp_Injected(ref a, ref b, t, out quaternion);
			return quaternion;
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0001CE24 File Offset: 0x0001B024
		[FreeFunction("QuaternionScripting::SlerpUnclamped", IsThreadSafe = true)]
		public static Quaternion SlerpUnclamped(Quaternion a, Quaternion b, float t)
		{
			Quaternion quaternion;
			Quaternion.SlerpUnclamped_Injected(ref a, ref b, t, out quaternion);
			return quaternion;
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0001CE40 File Offset: 0x0001B040
		[FreeFunction("EulerToQuaternion", IsThreadSafe = true)]
		private static Quaternion Internal_FromEulerRad(Vector3 euler)
		{
			Quaternion quaternion;
			Quaternion.Internal_FromEulerRad_Injected(ref euler, out quaternion);
			return quaternion;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0001CE58 File Offset: 0x0001B058
		[FreeFunction("QuaternionScripting::ToEuler", IsThreadSafe = true)]
		private static Vector3 Internal_ToEulerRad(Quaternion rotation)
		{
			Vector3 vector;
			Quaternion.Internal_ToEulerRad_Injected(ref rotation, out vector);
			return vector;
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0001CE70 File Offset: 0x0001B070
		[FreeFunction("QuaternionScripting::ToAxisAngle", IsThreadSafe = true)]
		private static void Internal_ToAxisAngleRad(Quaternion q, out Vector3 axis, out float angle)
		{
			Quaternion.Internal_ToAxisAngleRad_Injected(ref q, out axis, out angle);
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0001CE88 File Offset: 0x0001B088
		[FreeFunction("QuaternionScripting::AngleAxis", IsThreadSafe = true)]
		public static Quaternion AngleAxis(float angle, Vector3 axis)
		{
			Quaternion quaternion;
			Quaternion.AngleAxis_Injected(angle, ref axis, out quaternion);
			return quaternion;
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0001CEA0 File Offset: 0x0001B0A0
		[FreeFunction("QuaternionScripting::LookRotation", IsThreadSafe = true)]
		public static Quaternion LookRotation(Vector3 forward, [DefaultValue("Vector3.up")] Vector3 upwards)
		{
			Quaternion quaternion;
			Quaternion.LookRotation_Injected(ref forward, ref upwards, out quaternion);
			return quaternion;
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0001CEBC File Offset: 0x0001B0BC
		[ExcludeFromDocs]
		public static Quaternion LookRotation(Vector3 forward)
		{
			return Quaternion.LookRotation(forward, Vector3.up);
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0001CED9 File Offset: 0x0001B0D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Quaternion(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0001CEFC File Offset: 0x0001B0FC
		public static Quaternion identity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Quaternion.identityQuaternion;
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0001CF14 File Offset: 0x0001B114
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
		{
			return new Quaternion(lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y, lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z, lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x, lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0001D008 File Offset: 0x0001B208
		public static Vector3 operator *(Quaternion rotation, Vector3 point)
		{
			float x = rotation.x * 2f;
			float y = rotation.y * 2f;
			float z = rotation.z * 2f;
			float xx = rotation.x * x;
			float yy = rotation.y * y;
			float zz = rotation.z * z;
			float xy = rotation.x * y;
			float xz = rotation.x * z;
			float yz = rotation.y * z;
			float wx = rotation.w * x;
			float wy = rotation.w * y;
			float wz = rotation.w * z;
			Vector3 res;
			res.x = (1f - (yy + zz)) * point.x + (xy - wz) * point.y + (xz + wy) * point.z;
			res.y = (xy + wz) * point.x + (1f - (xx + zz)) * point.y + (yz - wx) * point.z;
			res.z = (xz - wy) * point.x + (yz + wx) * point.y + (1f - (xx + yy)) * point.z;
			return res;
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0001D138 File Offset: 0x0001B338
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsEqualUsingDot(float dot)
		{
			return dot > 0.999999f;
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0001D154 File Offset: 0x0001B354
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Quaternion lhs, Quaternion rhs)
		{
			return Quaternion.IsEqualUsingDot(Quaternion.Dot(lhs, rhs));
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0001D174 File Offset: 0x0001B374
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Quaternion lhs, Quaternion rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0001D190 File Offset: 0x0001B390
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Quaternion a, Quaternion b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0001D1DC File Offset: 0x0001B3DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Angle(Quaternion a, Quaternion b)
		{
			float dot = Mathf.Min(Mathf.Abs(Quaternion.Dot(a, b)), 1f);
			return Quaternion.IsEqualUsingDot(dot) ? 0f : (Mathf.Acos(dot) * 2f * 57.29578f);
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0001D228 File Offset: 0x0001B428
		private static Vector3 Internal_MakePositive(Vector3 euler)
		{
			float negativeFlip = -0.005729578f;
			float positiveFlip = 360f + negativeFlip;
			bool flag = euler.x < negativeFlip;
			if (flag)
			{
				euler.x += 360f;
			}
			else
			{
				bool flag2 = euler.x > positiveFlip;
				if (flag2)
				{
					euler.x -= 360f;
				}
			}
			bool flag3 = euler.y < negativeFlip;
			if (flag3)
			{
				euler.y += 360f;
			}
			else
			{
				bool flag4 = euler.y > positiveFlip;
				if (flag4)
				{
					euler.y -= 360f;
				}
			}
			bool flag5 = euler.z < negativeFlip;
			if (flag5)
			{
				euler.z += 360f;
			}
			else
			{
				bool flag6 = euler.z > positiveFlip;
				if (flag6)
				{
					euler.z -= 360f;
				}
			}
			return euler;
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0001D308 File Offset: 0x0001B508
		public Vector3 eulerAngles
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Quaternion.Internal_MakePositive(Quaternion.Internal_ToEulerRad(this) * 57.29578f);
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0001D334 File Offset: 0x0001B534
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion Euler(float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z) * 0.017453292f);
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0001D360 File Offset: 0x0001B560
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion Euler(Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad(euler * 0.017453292f);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0001D382 File Offset: 0x0001B582
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ToAngleAxis(out float angle, out Vector3 axis)
		{
			Quaternion.Internal_ToAxisAngleRad(this, out axis, out angle);
			angle *= 57.29578f;
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0001D3A0 File Offset: 0x0001B5A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion Normalize(Quaternion q)
		{
			float mag = Mathf.Sqrt(Quaternion.Dot(q, q));
			bool flag = mag < Mathf.Epsilon;
			Quaternion quaternion;
			if (flag)
			{
				quaternion = Quaternion.identity;
			}
			else
			{
				quaternion = new Quaternion(q.x / mag, q.y / mag, q.z / mag, q.w / mag);
			}
			return quaternion;
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x0001D3F8 File Offset: 0x0001B5F8
		public Quaternion normalized
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Quaternion.Normalize(this);
			}
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0001D418 File Offset: 0x0001B618
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2) ^ (this.z.GetHashCode() >> 2) ^ (this.w.GetHashCode() >> 1);
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0001D460 File Offset: 0x0001B660
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			Quaternion q;
			bool flag;
			if (other is Quaternion)
			{
				q = (Quaternion)other;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return flag2 && this.Equals(q);
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0001D494 File Offset: 0x0001B694
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Quaternion other)
		{
			return this.x.Equals(other.x) && this.y.Equals(other.y) && this.z.Equals(other.z) && this.w.Equals(other.w);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0001D4F4 File Offset: 0x0001B6F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0001D510 File Offset: 0x0001B710
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
			return UnityString.Format("({0}, {1}, {2}, {3})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider),
				this.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x06000E2B RID: 3627
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FromToRotation_Injected([In] ref Vector3 fromDirection, [In] ref Vector3 toDirection, out Quaternion ret);

		// Token: 0x06000E2C RID: 3628
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Inverse_Injected([In] ref Quaternion rotation, out Quaternion ret);

		// Token: 0x06000E2D RID: 3629
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Slerp_Injected([In] ref Quaternion a, [In] ref Quaternion b, float t, out Quaternion ret);

		// Token: 0x06000E2E RID: 3630
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SlerpUnclamped_Injected([In] ref Quaternion a, [In] ref Quaternion b, float t, out Quaternion ret);

		// Token: 0x06000E2F RID: 3631
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_FromEulerRad_Injected([In] ref Vector3 euler, out Quaternion ret);

		// Token: 0x06000E30 RID: 3632
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ToEulerRad_Injected([In] ref Quaternion rotation, out Vector3 ret);

		// Token: 0x06000E31 RID: 3633
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ToAxisAngleRad_Injected([In] ref Quaternion q, out Vector3 axis, out float angle);

		// Token: 0x06000E32 RID: 3634
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AngleAxis_Injected(float angle, [In] ref Vector3 axis, out Quaternion ret);

		// Token: 0x06000E33 RID: 3635
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LookRotation_Injected([In] ref Vector3 forward, [DefaultValue("Vector3.up")] [In] ref Vector3 upwards, out Quaternion ret);

		// Token: 0x040005A7 RID: 1447
		public float x;

		// Token: 0x040005A8 RID: 1448
		public float y;

		// Token: 0x040005A9 RID: 1449
		public float z;

		// Token: 0x040005AA RID: 1450
		public float w;

		// Token: 0x040005AB RID: 1451
		private static readonly Quaternion identityQuaternion = new Quaternion(0f, 0f, 0f, 1f);

		// Token: 0x040005AC RID: 1452
		public const float kEpsilon = 1E-06f;
	}
}
