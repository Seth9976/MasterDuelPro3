using System;
using UnityEngine;

namespace Cinemachine.Utility
{
	// Token: 0x020000F0 RID: 240
	public static class UnityVectorExtensions
	{
		// Token: 0x06000568 RID: 1384 RVA: 0x00022D2B File Offset: 0x00020F2B
		public static bool IsNaN(this Vector2 v)
		{
			return float.IsNaN(v.x) || float.IsNaN(v.y);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00022D47 File Offset: 0x00020F47
		public static bool IsNaN(this Vector3 v)
		{
			return float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00022D70 File Offset: 0x00020F70
		public static float ClosestPointOnSegment(this Vector3 p, Vector3 s0, Vector3 s1)
		{
			Vector3 s2 = s1 - s0;
			float len2 = Vector3.SqrMagnitude(s2);
			if (len2 < 0.0001f)
			{
				return 0f;
			}
			return Mathf.Clamp01(Vector3.Dot(p - s0, s2) / len2);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00022DB0 File Offset: 0x00020FB0
		public static float ClosestPointOnSegment(this Vector2 p, Vector2 s0, Vector2 s1)
		{
			Vector2 s2 = s1 - s0;
			float len2 = Vector2.SqrMagnitude(s2);
			if (len2 < 0.0001f)
			{
				return 0f;
			}
			return Mathf.Clamp01(Vector2.Dot(p - s0, s2) / len2);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00022DEE File Offset: 0x00020FEE
		public static Vector3 ProjectOntoPlane(this Vector3 vector, Vector3 planeNormal)
		{
			return vector - Vector3.Dot(vector, planeNormal) * planeNormal;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00022E04 File Offset: 0x00021004
		public static Vector2 SquareNormalize(this Vector2 v)
		{
			float d = Mathf.Max(Mathf.Abs(v.x), Mathf.Abs(v.y));
			if (d >= 0.0001f)
			{
				return v / d;
			}
			return Vector2.zero;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00022E44 File Offset: 0x00021044
		public static int FindIntersection(in Vector2 p1, in Vector2 p2, in Vector2 q1, in Vector2 q2, out Vector2 intersection)
		{
			Vector2 p3 = p2 - p1;
			Vector2 q3 = q2 - q1;
			Vector2 pq = q1 - p1;
			float pXq = p3.Cross(q3);
			if (Mathf.Abs(pXq) < 1E-05f)
			{
				intersection = Vector2.positiveInfinity;
				if (Mathf.Abs(pq.Cross(p3)) >= 1E-05f)
				{
					return 0;
				}
				float dotPQ = Vector2.Dot(q3, p3);
				if (dotPQ > 0f && (p1 - q2).sqrMagnitude < 0.001f)
				{
					intersection = q2;
					return 4;
				}
				if (dotPQ < 0f && (p2 - q2).sqrMagnitude < 0.001f)
				{
					intersection = p2;
					return 4;
				}
				float dot = Vector2.Dot(pq, p3);
				if (0f <= dot && dot <= Vector2.Dot(p3, p3))
				{
					if (dot < 0.0001f)
					{
						if (dotPQ <= 0f && (p1 - q1).sqrMagnitude < 0.001f)
						{
							intersection = p1;
						}
					}
					else if (dotPQ > 0f && (p2 - q1).sqrMagnitude < 0.001f)
					{
						intersection = p2;
					}
					return 4;
				}
				dot = Vector2.Dot(p1 - q1, q3);
				if (0f <= dot && dot <= Vector2.Dot(q3, q3))
				{
					return 4;
				}
				return 3;
			}
			else
			{
				float t = pq.Cross(q3) / pXq;
				intersection = p1 + t * p3;
				float u = pq.Cross(p3) / pXq;
				if (0f <= t && t <= 1f && 0f <= u && u <= 1f)
				{
					return 2;
				}
				return 1;
			}
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00023061 File Offset: 0x00021261
		private static float Cross(this Vector2 v1, Vector2 v2)
		{
			return v1.x * v2.y - v1.y * v2.x;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0002307E File Offset: 0x0002127E
		public static Vector2 Abs(this Vector2 v)
		{
			return new Vector2(Mathf.Abs(v.x), Mathf.Abs(v.y));
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0002309B File Offset: 0x0002129B
		public static Vector3 Abs(this Vector3 v)
		{
			return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x000230C3 File Offset: 0x000212C3
		public static bool IsUniform(this Vector2 v)
		{
			return Math.Abs(v.x - v.y) < 0.0001f;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000230DE File Offset: 0x000212DE
		public static bool IsUniform(this Vector3 v)
		{
			return Math.Abs(v.x - v.y) < 0.0001f && Math.Abs(v.x - v.z) < 0.0001f;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00023114 File Offset: 0x00021314
		public static bool AlmostZero(this Vector3 v)
		{
			return v.sqrMagnitude < 9.999999E-09f;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00023124 File Offset: 0x00021324
		internal static void ConservativeSetPositionAndRotation(this Transform t, Vector3 pos, Quaternion rot)
		{
			if (t.position.Equals(pos) && t.rotation.Equals(rot))
			{
				return;
			}
			t.SetPositionAndRotation(pos, rot);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0002315C File Offset: 0x0002135C
		public static float Angle(Vector3 v1, Vector3 v2)
		{
			v1.Normalize();
			v2.Normalize();
			return Mathf.Atan2((v1 - v2).magnitude, (v1 + v2).magnitude) * 57.29578f * 2f;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000231A8 File Offset: 0x000213A8
		public static float SignedAngle(Vector3 v1, Vector3 v2, Vector3 up)
		{
			float angle = UnityVectorExtensions.Angle(v1, v2);
			if (Mathf.Sign(Vector3.Dot(up, Vector3.Cross(v1, v2))) < 0f)
			{
				return -angle;
			}
			return angle;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000231DC File Offset: 0x000213DC
		public static Quaternion SafeFromToRotation(Vector3 v1, Vector3 v2, Vector3 up)
		{
			Vector3 axis = Vector3.Cross(v1, v2);
			if (axis.AlmostZero())
			{
				axis = up;
			}
			return Quaternion.AngleAxis(UnityVectorExtensions.Angle(v1, v2), axis);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00023208 File Offset: 0x00021408
		public static Vector3 SlerpWithReferenceUp(Vector3 vA, Vector3 vB, float t, Vector3 up)
		{
			float dA = vA.magnitude;
			float dB = vB.magnitude;
			if (dA < 0.0001f || dB < 0.0001f)
			{
				return Vector3.Lerp(vA, vB, t);
			}
			Vector3 vector = vA / dA;
			Vector3 dirB = vB / dB;
			Quaternion quaternion = Quaternion.LookRotation(vector, up);
			Quaternion qB = Quaternion.LookRotation(dirB, up);
			return UnityQuaternionExtensions.SlerpWithReferenceUp(quaternion, qB, t, up) * Vector3.forward * Mathf.Lerp(dA, dB, t);
		}

		// Token: 0x040004C0 RID: 1216
		public const float Epsilon = 0.0001f;
	}
}
