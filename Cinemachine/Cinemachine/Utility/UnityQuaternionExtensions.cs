using System;
using UnityEngine;

namespace Cinemachine.Utility
{
	// Token: 0x020000F1 RID: 241
	public static class UnityQuaternionExtensions
	{
		// Token: 0x0600057A RID: 1402 RVA: 0x0002327C File Offset: 0x0002147C
		public static Quaternion SlerpWithReferenceUp(Quaternion qA, Quaternion qB, float t, Vector3 up)
		{
			Vector3 dirA = (qA * Vector3.forward).ProjectOntoPlane(up);
			Vector3 dirB = (qB * Vector3.forward).ProjectOntoPlane(up);
			if (dirA.AlmostZero() || dirB.AlmostZero())
			{
				return Quaternion.Slerp(qA, qB, t);
			}
			Quaternion quaternion = Quaternion.LookRotation(dirA, up);
			Quaternion quaternion2 = Quaternion.Inverse(quaternion);
			Quaternion qA2 = quaternion2 * qA;
			Quaternion qB2 = quaternion2 * qB;
			Vector3 eA = qA2.eulerAngles;
			Vector3 eB = qB2.eulerAngles;
			return quaternion * Quaternion.Euler(Mathf.LerpAngle(eA.x, eB.x, t), Mathf.LerpAngle(eA.y, eB.y, t), Mathf.LerpAngle(eA.z, eB.z, t));
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0002333C File Offset: 0x0002153C
		public static Quaternion Normalized(this Quaternion q)
		{
			Vector4 v = new Vector4(q.x, q.y, q.z, q.w).normalized;
			return new Quaternion(v.x, v.y, v.z, v.w);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0002338C File Offset: 0x0002158C
		public static Vector2 GetCameraRotationToTarget(this Quaternion orient, Vector3 lookAtDir, Vector3 worldUp)
		{
			if (lookAtDir.AlmostZero())
			{
				return Vector2.zero;
			}
			Quaternion quaternion = Quaternion.Inverse(orient);
			Vector3 up = quaternion * worldUp;
			lookAtDir = quaternion * lookAtDir;
			float angleH = 0f;
			Vector3 targetDirH = lookAtDir.ProjectOntoPlane(up);
			if (!targetDirH.AlmostZero())
			{
				Vector3 currentDirH = Vector3.forward.ProjectOntoPlane(up);
				if (currentDirH.AlmostZero())
				{
					if (Vector3.Dot(currentDirH, up) > 0f)
					{
						currentDirH = Vector3.down.ProjectOntoPlane(up);
					}
					else
					{
						currentDirH = Vector3.up.ProjectOntoPlane(up);
					}
				}
				angleH = UnityVectorExtensions.SignedAngle(currentDirH, targetDirH, up);
			}
			Quaternion q = Quaternion.AngleAxis(angleH, up);
			return new Vector2(UnityVectorExtensions.SignedAngle(q * Vector3.forward, lookAtDir, q * Vector3.right), angleH);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00023448 File Offset: 0x00021648
		public static Quaternion ApplyCameraRotation(this Quaternion orient, Vector2 rot, Vector3 worldUp)
		{
			if (rot.sqrMagnitude < 0.0001f)
			{
				return orient;
			}
			Quaternion q = Quaternion.AngleAxis(rot.x, Vector3.right);
			return Quaternion.AngleAxis(rot.y, worldUp) * orient * q;
		}
	}
}
