using System;
using Cinemachine.Utility;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x02000033 RID: 51
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[AddComponentMenu("Cinemachine/CinemachineSmoothPath")]
	[SaveDuringPlay]
	[DisallowMultipleComponent]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineSmoothPath.html")]
	public class CinemachineSmoothPath : CinemachinePathBase
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00008BC2 File Offset: 0x00006DC2
		public override float MinPos
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00009028 File Offset: 0x00007228
		public override float MaxPos
		{
			get
			{
				int count = this.m_Waypoints.Length - 1;
				if (count < 1)
				{
					return 0f;
				}
				return (float)(this.m_Looped ? (count + 1) : count);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00009059 File Offset: 0x00007259
		public override bool Looped
		{
			get
			{
				return this.m_Looped;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00008CCD File Offset: 0x00006ECD
		public override int DistanceCacheSampleStepsPerSegment
		{
			get
			{
				return this.m_Resolution;
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00008CC5 File Offset: 0x00006EC5
		private void OnValidate()
		{
			this.InvalidateDistanceCache();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00009064 File Offset: 0x00007264
		private void Reset()
		{
			this.m_Looped = false;
			this.m_Waypoints = new CinemachineSmoothPath.Waypoint[]
			{
				new CinemachineSmoothPath.Waypoint
				{
					position = new Vector3(0f, 0f, -5f)
				},
				new CinemachineSmoothPath.Waypoint
				{
					position = new Vector3(0f, 0f, 5f)
				}
			};
			this.m_Appearance = new CinemachinePathBase.Appearance();
			this.InvalidateDistanceCache();
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000090EB File Offset: 0x000072EB
		public override void InvalidateDistanceCache()
		{
			base.InvalidateDistanceCache();
			this.m_ControlPoints1 = null;
			this.m_ControlPoints2 = null;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00009104 File Offset: 0x00007304
		internal void UpdateControlPoints()
		{
			int numPoints = ((this.m_Waypoints == null) ? 0 : this.m_Waypoints.Length);
			if (numPoints > 1 && (this.Looped != this.m_IsLoopedCache || this.m_ControlPoints1 == null || this.m_ControlPoints1.Length != numPoints || this.m_ControlPoints2 == null || this.m_ControlPoints2.Length != numPoints))
			{
				Vector4[] p = new Vector4[numPoints];
				Vector4[] p2 = new Vector4[numPoints];
				Vector4[] K = new Vector4[numPoints];
				for (int i = 0; i < numPoints; i++)
				{
					K[i] = this.m_Waypoints[i].AsVector4;
				}
				if (this.Looped)
				{
					SplineHelpers.ComputeSmoothControlPointsLooped(ref K, ref p, ref p2);
				}
				else
				{
					SplineHelpers.ComputeSmoothControlPoints(ref K, ref p, ref p2);
				}
				this.m_ControlPoints1 = new CinemachineSmoothPath.Waypoint[numPoints];
				this.m_ControlPoints2 = new CinemachineSmoothPath.Waypoint[numPoints];
				for (int j = 0; j < numPoints; j++)
				{
					this.m_ControlPoints1[j] = CinemachineSmoothPath.Waypoint.FromVector4(p[j]);
					this.m_ControlPoints2[j] = CinemachineSmoothPath.Waypoint.FromVector4(p2[j]);
				}
				this.m_IsLoopedCache = this.Looped;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000922C File Offset: 0x0000742C
		private float GetBoundingIndices(float pos, out int indexA, out int indexB)
		{
			pos = this.StandardizePos(pos);
			int numWaypoints = this.m_Waypoints.Length;
			if (numWaypoints < 2)
			{
				indexA = (indexB = 0);
			}
			else
			{
				indexA = Mathf.FloorToInt(pos);
				if (indexA >= numWaypoints)
				{
					pos -= this.MaxPos;
					indexA = 0;
				}
				indexB = indexA + 1;
				if (indexB == numWaypoints)
				{
					if (this.Looped)
					{
						indexB = 0;
					}
					else
					{
						indexB--;
						indexA--;
					}
				}
			}
			return pos;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00009298 File Offset: 0x00007498
		public override Vector3 EvaluateLocalPosition(float pos)
		{
			Vector3 result = Vector3.zero;
			if (this.m_Waypoints.Length != 0)
			{
				this.UpdateControlPoints();
				int indexA;
				int indexB;
				pos = this.GetBoundingIndices(pos, out indexA, out indexB);
				if (indexA == indexB)
				{
					result = this.m_Waypoints[indexA].position;
				}
				else
				{
					result = SplineHelpers.Bezier3(pos - (float)indexA, this.m_Waypoints[indexA].position, this.m_ControlPoints1[indexA].position, this.m_ControlPoints2[indexA].position, this.m_Waypoints[indexB].position);
				}
			}
			return result;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00009330 File Offset: 0x00007530
		public override Vector3 EvaluateLocalTangent(float pos)
		{
			Vector3 result = Vector3.forward;
			if (this.m_Waypoints.Length > 1)
			{
				this.UpdateControlPoints();
				int indexA;
				int indexB;
				pos = this.GetBoundingIndices(pos, out indexA, out indexB);
				if (!this.Looped && indexA == this.m_Waypoints.Length - 1)
				{
					indexA--;
				}
				result = SplineHelpers.BezierTangent3(pos - (float)indexA, this.m_Waypoints[indexA].position, this.m_ControlPoints1[indexA].position, this.m_ControlPoints2[indexA].position, this.m_Waypoints[indexB].position);
			}
			return result;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000093CC File Offset: 0x000075CC
		public override Quaternion EvaluateLocalOrientation(float pos)
		{
			Quaternion result = Quaternion.identity;
			if (this.m_Waypoints.Length != 0)
			{
				int indexA;
				int indexB;
				pos = this.GetBoundingIndices(pos, out indexA, out indexB);
				float roll;
				if (indexA == indexB)
				{
					roll = this.m_Waypoints[indexA].roll;
				}
				else
				{
					this.UpdateControlPoints();
					roll = SplineHelpers.Bezier1(pos - (float)indexA, this.m_Waypoints[indexA].roll, this.m_ControlPoints1[indexA].roll, this.m_ControlPoints2[indexA].roll, this.m_Waypoints[indexB].roll);
				}
				Vector3 fwd = this.EvaluateLocalTangent(pos);
				if (!fwd.AlmostZero())
				{
					result = Quaternion.LookRotation(fwd) * CinemachineSmoothPath.RollAroundForward(roll);
				}
			}
			return result;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000948C File Offset: 0x0000768C
		private static Quaternion RollAroundForward(float angle)
		{
			float halfAngle = angle * 0.5f * 0.017453292f;
			return new Quaternion(0f, 0f, Mathf.Sin(halfAngle), Mathf.Cos(halfAngle));
		}

		// Token: 0x040000FA RID: 250
		[Tooltip("If checked, then the path ends are joined to form a continuous loop.")]
		public bool m_Looped;

		// Token: 0x040000FB RID: 251
		[Tooltip("The waypoints that define the path.  They will be interpolated using a bezier curve.")]
		public CinemachineSmoothPath.Waypoint[] m_Waypoints = Array.Empty<CinemachineSmoothPath.Waypoint>();

		// Token: 0x040000FC RID: 252
		internal CinemachineSmoothPath.Waypoint[] m_ControlPoints1;

		// Token: 0x040000FD RID: 253
		internal CinemachineSmoothPath.Waypoint[] m_ControlPoints2;

		// Token: 0x040000FE RID: 254
		private bool m_IsLoopedCache;

		// Token: 0x02000034 RID: 52
		[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
		[Serializable]
		public struct Waypoint
		{
			// Token: 0x17000036 RID: 54
			// (get) Token: 0x0600013C RID: 316 RVA: 0x000094D5 File Offset: 0x000076D5
			internal Vector4 AsVector4
			{
				get
				{
					return new Vector4(this.position.x, this.position.y, this.position.z, this.roll);
				}
			}

			// Token: 0x0600013D RID: 317 RVA: 0x00009504 File Offset: 0x00007704
			internal static CinemachineSmoothPath.Waypoint FromVector4(Vector4 v)
			{
				return new CinemachineSmoothPath.Waypoint
				{
					position = new Vector3(v[0], v[1], v[2]),
					roll = v[3]
				};
			}

			// Token: 0x040000FF RID: 255
			[Tooltip("Position in path-local space")]
			public Vector3 position;

			// Token: 0x04000100 RID: 256
			[Tooltip("Defines the roll of the path at this waypoint.  The other orientation axes are inferred from the tangent and world up.")]
			public float roll;
		}
	}
}
