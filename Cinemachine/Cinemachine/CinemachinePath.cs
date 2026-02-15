using System;
using Cinemachine.Utility;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x0200002F RID: 47
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[AddComponentMenu("Cinemachine/CinemachinePath")]
	[SaveDuringPlay]
	[DisallowMultipleComponent]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachinePath.html")]
	public class CinemachinePath : CinemachinePathBase
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00008BC2 File Offset: 0x00006DC2
		public override float MinPos
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00008BCC File Offset: 0x00006DCC
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

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00008BFD File Offset: 0x00006DFD
		public override bool Looped
		{
			get
			{
				return this.m_Looped;
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00008C08 File Offset: 0x00006E08
		private void Reset()
		{
			this.m_Looped = false;
			this.m_Waypoints = new CinemachinePath.Waypoint[]
			{
				new CinemachinePath.Waypoint
				{
					position = new Vector3(0f, 0f, -5f),
					tangent = new Vector3(1f, 0f, 0f)
				},
				new CinemachinePath.Waypoint
				{
					position = new Vector3(0f, 0f, 5f),
					tangent = new Vector3(1f, 0f, 0f)
				}
			};
			this.m_Appearance = new CinemachinePathBase.Appearance();
			this.InvalidateDistanceCache();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00008CC5 File Offset: 0x00006EC5
		private void OnValidate()
		{
			this.InvalidateDistanceCache();
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00008CCD File Offset: 0x00006ECD
		public override int DistanceCacheSampleStepsPerSegment
		{
			get
			{
				return this.m_Resolution;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00008CD8 File Offset: 0x00006ED8
		private float GetBoundingIndices(float pos, out int indexA, out int indexB)
		{
			pos = this.StandardizePos(pos);
			int rounded = Mathf.RoundToInt(pos);
			if (Mathf.Abs(pos - (float)rounded) < 0.0001f)
			{
				indexA = (indexB = ((rounded == this.m_Waypoints.Length) ? 0 : rounded));
			}
			else
			{
				indexA = Mathf.FloorToInt(pos);
				if (indexA >= this.m_Waypoints.Length)
				{
					pos -= this.MaxPos;
					indexA = 0;
				}
				indexB = Mathf.CeilToInt(pos);
				if (indexB >= this.m_Waypoints.Length)
				{
					indexB = 0;
				}
			}
			return pos;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00008D58 File Offset: 0x00006F58
		public override Vector3 EvaluateLocalPosition(float pos)
		{
			Vector3 result = Vector3.zero;
			if (this.m_Waypoints.Length != 0)
			{
				int indexA;
				int indexB;
				pos = this.GetBoundingIndices(pos, out indexA, out indexB);
				if (indexA == indexB)
				{
					result = this.m_Waypoints[indexA].position;
				}
				else
				{
					CinemachinePath.Waypoint wpA = this.m_Waypoints[indexA];
					CinemachinePath.Waypoint wpB = this.m_Waypoints[indexB];
					result = SplineHelpers.Bezier3(pos - (float)indexA, this.m_Waypoints[indexA].position, wpA.position + wpA.tangent, wpB.position - wpB.tangent, wpB.position);
				}
			}
			return result;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00008E00 File Offset: 0x00007000
		public override Vector3 EvaluateLocalTangent(float pos)
		{
			Vector3 result = Vector3.forward;
			if (this.m_Waypoints.Length != 0)
			{
				int indexA;
				int indexB;
				pos = this.GetBoundingIndices(pos, out indexA, out indexB);
				if (indexA == indexB)
				{
					result = this.m_Waypoints[indexA].tangent;
				}
				else
				{
					CinemachinePath.Waypoint wpA = this.m_Waypoints[indexA];
					CinemachinePath.Waypoint wpB = this.m_Waypoints[indexB];
					result = SplineHelpers.BezierTangent3(pos - (float)indexA, this.m_Waypoints[indexA].position, wpA.position + wpA.tangent, wpB.position - wpB.tangent, wpB.position);
				}
			}
			return result;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00008EA8 File Offset: 0x000070A8
		public override Quaternion EvaluateLocalOrientation(float pos)
		{
			Quaternion result = Quaternion.identity;
			if (this.m_Waypoints.Length != 0)
			{
				int indexA;
				int indexB;
				pos = this.GetBoundingIndices(pos, out indexA, out indexB);
				Vector3 fwd = this.EvaluateLocalTangent(pos);
				if (!fwd.AlmostZero())
				{
					result = Quaternion.LookRotation(fwd) * CinemachinePath.RollAroundForward(this.GetRoll(indexA, indexB, pos));
				}
			}
			return result;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00008EFC File Offset: 0x000070FC
		internal float GetRoll(int indexA, int indexB, float standardizedPos)
		{
			if (indexA == indexB)
			{
				return this.m_Waypoints[indexA].roll;
			}
			float rollA = this.m_Waypoints[indexA].roll;
			float rollB = this.m_Waypoints[indexB].roll;
			if (indexB == 0)
			{
				rollA %= 360f;
				rollB %= 360f;
			}
			return Mathf.Lerp(rollA, rollB, standardizedPos - (float)indexA);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00008F64 File Offset: 0x00007164
		private static Quaternion RollAroundForward(float angle)
		{
			float halfAngle = angle * 0.5f * 0.017453292f;
			return new Quaternion(0f, 0f, Mathf.Sin(halfAngle), Mathf.Cos(halfAngle));
		}

		// Token: 0x040000F5 RID: 245
		[Tooltip("If checked, then the path ends are joined to form a continuous loop.")]
		public bool m_Looped;

		// Token: 0x040000F6 RID: 246
		[Tooltip("The waypoints that define the path.  They will be interpolated using a bezier curve.")]
		public CinemachinePath.Waypoint[] m_Waypoints = Array.Empty<CinemachinePath.Waypoint>();

		// Token: 0x02000030 RID: 48
		[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
		[Serializable]
		public struct Waypoint
		{
			// Token: 0x040000F7 RID: 247
			[Tooltip("Position in path-local space")]
			public Vector3 position;

			// Token: 0x040000F8 RID: 248
			[Tooltip("Offset from the position, which defines the tangent of the curve at the waypoint.  The length of the tangent encodes the strength of the bezier handle.  The same handle is used symmetrically on both sides of the waypoint, to ensure smoothness.")]
			public Vector3 tangent;

			// Token: 0x040000F9 RID: 249
			[Tooltip("Defines the roll of the path at this waypoint.  The other orientation axes are inferred from the tangent and world up.")]
			public float roll;
		}
	}
}
