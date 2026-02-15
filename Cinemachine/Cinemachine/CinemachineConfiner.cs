using System;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x0200001D RID: 29
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[AddComponentMenu("")]
	[SaveDuringPlay]
	[ExecuteAlways]
	[DisallowMultipleComponent]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineConfiner.html")]
	public class CinemachineConfiner : CinemachineExtension
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00005F6C File Offset: 0x0000416C
		public bool CameraWasDisplaced(CinemachineVirtualCameraBase vcam)
		{
			return this.GetCameraDisplacementDistance(vcam) > 0f;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005F7C File Offset: 0x0000417C
		public float GetCameraDisplacementDistance(CinemachineVirtualCameraBase vcam)
		{
			return base.GetExtraState<CinemachineConfiner.VcamExtraState>(vcam).confinerDisplacement;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005F8A File Offset: 0x0000418A
		private void OnValidate()
		{
			this.m_Damping = Mathf.Max(0f, this.m_Damping);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005FA2 File Offset: 0x000041A2
		protected override void ConnectToVcam(bool connect)
		{
			base.ConnectToVcam(connect);
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00005FAC File Offset: 0x000041AC
		public bool IsValid
		{
			get
			{
				return (this.m_ConfineMode == CinemachineConfiner.Mode.Confine3D && this.m_BoundingVolume != null && this.m_BoundingVolume.enabled && this.m_BoundingVolume.gameObject.activeInHierarchy) || (this.m_ConfineMode == CinemachineConfiner.Mode.Confine2D && this.m_BoundingShape2D != null && this.m_BoundingShape2D.enabled && this.m_BoundingShape2D.gameObject.activeInHierarchy);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00006026 File Offset: 0x00004226
		public override float GetMaxDampTime()
		{
			return this.m_Damping;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00006030 File Offset: 0x00004230
		protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
		{
			if (this.IsValid && stage == CinemachineCore.Stage.Body)
			{
				CinemachineConfiner.VcamExtraState extra = base.GetExtraState<CinemachineConfiner.VcamExtraState>(vcam);
				Vector3 displacement;
				if (this.m_ConfineScreenEdges && state.Lens.Orthographic)
				{
					displacement = this.ConfineScreenEdges(ref state);
				}
				else
				{
					displacement = this.ConfinePoint(state.CorrectedPosition);
				}
				if (this.m_Damping > 0f && deltaTime >= 0f && base.VirtualCamera.PreviousStateIsValid)
				{
					Vector3 delta = displacement - extra.m_previousDisplacement;
					delta = Damper.Damp(delta, this.m_Damping, deltaTime);
					displacement = extra.m_previousDisplacement + delta;
				}
				extra.m_previousDisplacement = displacement;
				state.PositionCorrection += displacement;
				extra.confinerDisplacement = displacement.magnitude;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000060F9 File Offset: 0x000042F9
		public void InvalidatePathCache()
		{
			this.m_pathCache = null;
			this.m_BoundingShape2DCache = null;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000610C File Offset: 0x0000430C
		private bool ValidatePathCache()
		{
			if (this.m_BoundingShape2DCache != this.m_BoundingShape2D)
			{
				this.InvalidatePathCache();
				this.m_BoundingShape2DCache = this.m_BoundingShape2D;
			}
			Type colliderType = ((this.m_BoundingShape2D == null) ? null : this.m_BoundingShape2D.GetType());
			if (colliderType == typeof(PolygonCollider2D))
			{
				PolygonCollider2D poly = this.m_BoundingShape2D as PolygonCollider2D;
				if (this.m_pathCache == null || this.m_pathCache.Count != poly.pathCount || this.m_pathTotalPointCount != poly.GetTotalPointCount())
				{
					this.m_pathCache = new List<List<Vector2>>();
					for (int i = 0; i < poly.pathCount; i++)
					{
						Vector2[] path = poly.GetPath(i);
						List<Vector2> dst = new List<Vector2>();
						for (int j = 0; j < path.Length; j++)
						{
							dst.Add(path[j]);
						}
						this.m_pathCache.Add(dst);
					}
					this.m_pathTotalPointCount = poly.GetTotalPointCount();
				}
				return true;
			}
			if (colliderType == typeof(CompositeCollider2D))
			{
				CompositeCollider2D poly2 = this.m_BoundingShape2D as CompositeCollider2D;
				if (this.m_pathCache == null || this.m_pathCache.Count != poly2.pathCount || this.m_pathTotalPointCount != poly2.pointCount)
				{
					this.m_pathCache = new List<List<Vector2>>();
					Vector2[] path2 = new Vector2[poly2.pointCount];
					Vector3 lossyScale = this.m_BoundingShape2D.transform.lossyScale;
					Vector2 revertCompositeColliderScale = new Vector2(1f / lossyScale.x, 1f / lossyScale.y);
					for (int k = 0; k < poly2.pathCount; k++)
					{
						int numPoints = poly2.GetPath(k, path2);
						List<Vector2> dst2 = new List<Vector2>();
						for (int l = 0; l < numPoints; l++)
						{
							dst2.Add(path2[l] * revertCompositeColliderScale);
						}
						this.m_pathCache.Add(dst2);
					}
					this.m_pathTotalPointCount = poly2.pointCount;
				}
				return true;
			}
			this.InvalidatePathCache();
			return false;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00006320 File Offset: 0x00004520
		private Vector3 ConfinePoint(Vector3 camPos)
		{
			if (this.m_ConfineMode == CinemachineConfiner.Mode.Confine3D)
			{
				return this.m_BoundingVolume.ClosestPoint(camPos) - camPos;
			}
			Vector2 p = camPos;
			Vector2 closest = p;
			if (this.m_BoundingShape2D.OverlapPoint(camPos))
			{
				return Vector3.zero;
			}
			if (!this.ValidatePathCache())
			{
				return Vector3.zero;
			}
			float bestDistance = float.MaxValue;
			for (int i = 0; i < this.m_pathCache.Count; i++)
			{
				int numPoints = this.m_pathCache[i].Count;
				if (numPoints > 0)
				{
					Vector2 v0 = this.m_BoundingShape2D.transform.TransformPoint(this.m_pathCache[i][numPoints - 1] + this.m_BoundingShape2D.offset);
					for (int j = 0; j < numPoints; j++)
					{
						Vector2 v = this.m_BoundingShape2D.transform.TransformPoint(this.m_pathCache[i][j] + this.m_BoundingShape2D.offset);
						Vector2 c = Vector2.Lerp(v0, v, p.ClosestPointOnSegment(v0, v));
						float d = Vector2.SqrMagnitude(p - c);
						if (d < bestDistance)
						{
							bestDistance = d;
							closest = c;
						}
						v0 = v;
					}
				}
			}
			return closest - p;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00006488 File Offset: 0x00004688
		private Vector3 ConfineScreenEdges(ref CameraState state)
		{
			Quaternion correctedOrientation = state.CorrectedOrientation;
			float dy = state.Lens.OrthographicSize;
			float dx = dy * state.Lens.Aspect;
			Vector3 vx = correctedOrientation * Vector3.right * dx;
			Vector3 vy = correctedOrientation * Vector3.up * dy;
			Vector3 displacement = Vector3.zero;
			Vector3 camPos = state.CorrectedPosition;
			Vector3 lastD = Vector3.zero;
			for (int i = 0; i < 12; i++)
			{
				Vector3 d = this.ConfinePoint(camPos - vy - vx);
				if (d.AlmostZero())
				{
					d = this.ConfinePoint(camPos + vy + vx);
				}
				if (d.AlmostZero())
				{
					d = this.ConfinePoint(camPos - vy + vx);
				}
				if (d.AlmostZero())
				{
					d = this.ConfinePoint(camPos + vy - vx);
				}
				if (d.AlmostZero())
				{
					break;
				}
				if ((d + lastD).AlmostZero())
				{
					displacement += d * 0.5f;
					break;
				}
				displacement += d;
				camPos += d;
				lastD = d;
			}
			return displacement;
		}

		// Token: 0x0400008C RID: 140
		[Tooltip("The confiner can operate using a 2D bounding shape or a 3D bounding volume")]
		public CinemachineConfiner.Mode m_ConfineMode;

		// Token: 0x0400008D RID: 141
		[Tooltip("The volume within which the camera is to be contained")]
		public Collider m_BoundingVolume;

		// Token: 0x0400008E RID: 142
		[Tooltip("The 2D shape within which the camera is to be contained")]
		public Collider2D m_BoundingShape2D;

		// Token: 0x0400008F RID: 143
		private Collider2D m_BoundingShape2DCache;

		// Token: 0x04000090 RID: 144
		[Tooltip("If camera is orthographic, screen edges will be confined to the volume.  If not checked, then only the camera center will be confined")]
		public bool m_ConfineScreenEdges = true;

		// Token: 0x04000091 RID: 145
		[Tooltip("How gradually to return the camera to the bounding volume if it goes beyond the borders.  Higher numbers are more gradual.")]
		[Range(0f, 10f)]
		public float m_Damping;

		// Token: 0x04000092 RID: 146
		private List<List<Vector2>> m_pathCache;

		// Token: 0x04000093 RID: 147
		private int m_pathTotalPointCount;

		// Token: 0x0200001E RID: 30
		public enum Mode
		{
			// Token: 0x04000095 RID: 149
			Confine2D,
			// Token: 0x04000096 RID: 150
			Confine3D
		}

		// Token: 0x0200001F RID: 31
		private class VcamExtraState
		{
			// Token: 0x04000097 RID: 151
			public Vector3 m_previousDisplacement;

			// Token: 0x04000098 RID: 152
			public float confinerDisplacement;
		}
	}
}
