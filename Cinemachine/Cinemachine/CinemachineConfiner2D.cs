using System;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x02000020 RID: 32
	[AddComponentMenu("")]
	[SaveDuringPlay]
	[ExecuteAlways]
	[DisallowMultipleComponent]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineConfiner2D.html")]
	public class CinemachineConfiner2D : CinemachineExtension
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x000065D3 File Offset: 0x000047D3
		public void InvalidateCache()
		{
			this.m_shapeCache.Invalidate();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000065E0 File Offset: 0x000047E0
		public bool ValidateCache(float cameraAspectRatio)
		{
			bool flag;
			return this.m_shapeCache.ValidateCache(this.m_BoundingShape2D, this.m_MaxWindowSize, cameraAspectRatio, this.m_Padding, out flag);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00006610 File Offset: 0x00004810
		protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
		{
			if (stage == CinemachineCore.Stage.Body)
			{
				float aspectRatio = state.Lens.Aspect;
				bool confinerStateChanged;
				if (!this.m_shapeCache.ValidateCache(this.m_BoundingShape2D, this.m_MaxWindowSize, aspectRatio, this.m_Padding, out confinerStateChanged))
				{
					return;
				}
				Vector3 oldCameraPos = state.CorrectedPosition;
				Vector3 cameraPosLocal = this.m_shapeCache.m_DeltaWorldToBaked.MultiplyPoint3x4(oldCameraPos);
				float bakedSpaceFrustumHeight = this.CalculateHalfFrustumHeight(in state, in cameraPosLocal.z) * this.m_shapeCache.m_DeltaWorldToBaked.lossyScale.x;
				CinemachineConfiner2D.VcamExtraState extra = base.GetExtraState<CinemachineConfiner2D.VcamExtraState>(vcam);
				extra.m_vcam = vcam;
				if (confinerStateChanged || extra.m_BakedSolution == null || !extra.m_BakedSolution.IsValid())
				{
					extra.m_BakedSolution = this.m_shapeCache.m_confinerOven.GetBakedSolution(bakedSpaceFrustumHeight);
				}
				ConfinerOven.BakedSolution bakedSolution = extra.m_BakedSolution;
				Vector2 vector = cameraPosLocal;
				cameraPosLocal = bakedSolution.ConfinePoint(in vector);
				Vector3 newCameraPos = this.m_shapeCache.m_DeltaBakedToWorld.MultiplyPoint3x4(cameraPosLocal);
				Vector3 fwd = state.CorrectedOrientation * Vector3.forward;
				newCameraPos -= fwd * Vector3.Dot(fwd, newCameraPos - oldCameraPos);
				Vector3 prev = extra.m_PreviousDisplacement;
				Vector3 displacement = newCameraPos - oldCameraPos;
				extra.m_PreviousDisplacement = displacement;
				if (!base.VirtualCamera.PreviousStateIsValid || deltaTime < 0f || this.m_Damping <= 0f)
				{
					extra.m_DampedDisplacement = Vector3.zero;
				}
				else
				{
					if (prev.sqrMagnitude > 0.01f && Vector2.Angle(prev, displacement) > 10f)
					{
						extra.m_DampedDisplacement += displacement - prev;
					}
					extra.m_DampedDisplacement -= Damper.Damp(extra.m_DampedDisplacement, this.m_Damping, deltaTime);
					displacement -= extra.m_DampedDisplacement;
				}
				state.PositionCorrection += displacement;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00006814 File Offset: 0x00004A14
		private float CalculateHalfFrustumHeight(in CameraState state, in float cameraPosLocalZ)
		{
			LensSettings lens = state.Lens;
			float frustumHeight;
			if (lens.Orthographic)
			{
				frustumHeight = state.Lens.OrthographicSize;
			}
			else
			{
				frustumHeight = cameraPosLocalZ * Mathf.Tan(state.Lens.FieldOfView * 0.5f * 0.017453292f);
			}
			return Mathf.Abs(frustumHeight);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00006865 File Offset: 0x00004A65
		private void OnValidate()
		{
			this.m_Damping = Mathf.Max(0f, this.m_Damping);
			this.m_shapeCache.m_maxComputationTimePerFrameInSeconds = this.m_MaxComputationTimePerFrameInSeconds;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000688E File Offset: 0x00004A8E
		private void Reset()
		{
			this.m_Damping = 0.5f;
			this.m_MaxWindowSize = -1f;
		}

		// Token: 0x04000099 RID: 153
		[Tooltip("The 2D shape within which the camera is to be contained.  Can be a 2D polygon or 2D composite collider.")]
		public Collider2D m_BoundingShape2D;

		// Token: 0x0400009A RID: 154
		[Tooltip("Damping applied around corners to avoid jumps.  Higher numbers are more gradual.")]
		[Range(0f, 5f)]
		public float m_Damping;

		// Token: 0x0400009B RID: 155
		[Tooltip("To optimize computation and memory costs, set this to the largest view size that the camera is expected to have.  The confiner will not compute a polygon cache for frustum sizes larger than this.  This refers to the size in world units of the frustum at the confiner plane (for orthographic cameras, this is just the orthographic size).  If set to 0, then this parameter is ignored and a polygon cache will be calculated for all potential window sizes.")]
		public float m_MaxWindowSize;

		// Token: 0x0400009C RID: 156
		[Tooltip("For large window sizes, the confiner will potentially generate polygons with zero area.  The padding may be used to add a small amount of area to these polygons, to prevent them from being a series of disconnected dots.")]
		[Range(0f, 100f)]
		public float m_Padding;

		// Token: 0x0400009D RID: 157
		private float m_MaxComputationTimePerFrameInSeconds = 0.008333334f;

		// Token: 0x0400009E RID: 158
		private const float k_cornerAngleTreshold = 10f;

		// Token: 0x0400009F RID: 159
		private CinemachineConfiner2D.ShapeCache m_shapeCache;

		// Token: 0x02000021 RID: 33
		private class VcamExtraState
		{
			// Token: 0x040000A0 RID: 160
			public Vector3 m_PreviousDisplacement;

			// Token: 0x040000A1 RID: 161
			public Vector3 m_DampedDisplacement;

			// Token: 0x040000A2 RID: 162
			public ConfinerOven.BakedSolution m_BakedSolution;

			// Token: 0x040000A3 RID: 163
			public CinemachineVirtualCameraBase m_vcam;
		}

		// Token: 0x02000022 RID: 34
		private struct ShapeCache
		{
			// Token: 0x060000C0 RID: 192 RVA: 0x000068BC File Offset: 0x00004ABC
			public void Invalidate()
			{
				this.m_aspectRatio = 0f;
				this.m_maxWindowSize = -1f;
				this.m_DeltaBakedToWorld = (this.m_DeltaWorldToBaked = Matrix4x4.identity);
				this.m_boundingShape2D = null;
				this.m_OriginalPath = null;
				this.m_confinerOven = null;
			}

			// Token: 0x060000C1 RID: 193 RVA: 0x00006908 File Offset: 0x00004B08
			public bool ValidateCache(Collider2D boundingShape2D, float maxWindowSize, float aspectRatio, float skeletonPadding, out bool confinerStateChanged)
			{
				confinerStateChanged = false;
				if (this.IsValid(in boundingShape2D, in aspectRatio, in maxWindowSize, in skeletonPadding))
				{
					if (this.m_confinerOven.State == ConfinerOven.BakingState.BAKING)
					{
						this.m_confinerOven.BakeConfiner(this.m_maxComputationTimePerFrameInSeconds);
						confinerStateChanged = this.m_confinerOven.State > ConfinerOven.BakingState.BAKING;
					}
					this.CalculateDeltaTransformationMatrix();
					if (this.m_DeltaWorldToBaked.lossyScale.IsUniform())
					{
						return true;
					}
				}
				this.Invalidate();
				confinerStateChanged = true;
				Type colliderType = ((boundingShape2D == null) ? null : boundingShape2D.GetType());
				if (colliderType == typeof(PolygonCollider2D))
				{
					PolygonCollider2D poly = boundingShape2D as PolygonCollider2D;
					this.m_OriginalPath = new List<List<Vector2>>();
					this.m_bakedToWorld = boundingShape2D.transform.localToWorldMatrix;
					for (int i = 0; i < poly.pathCount; i++)
					{
						Vector2[] path = poly.GetPath(i);
						List<Vector2> dst = new List<Vector2>();
						for (int j = 0; j < path.Length; j++)
						{
							dst.Add(this.m_bakedToWorld.MultiplyPoint3x4(path[j]));
						}
						this.m_OriginalPath.Add(dst);
					}
				}
				else
				{
					if (!(colliderType == typeof(CompositeCollider2D)))
					{
						return false;
					}
					CompositeCollider2D poly2 = boundingShape2D as CompositeCollider2D;
					this.m_OriginalPath = new List<List<Vector2>>();
					this.m_bakedToWorld = boundingShape2D.transform.localToWorldMatrix;
					Vector2[] path2 = new Vector2[poly2.pointCount];
					for (int k = 0; k < poly2.pathCount; k++)
					{
						int numPoints = poly2.GetPath(k, path2);
						List<Vector2> dst2 = new List<Vector2>();
						for (int l = 0; l < numPoints; l++)
						{
							dst2.Add(this.m_bakedToWorld.MultiplyPoint3x4(path2[l]));
						}
						this.m_OriginalPath.Add(dst2);
					}
				}
				this.m_confinerOven = new ConfinerOven(in this.m_OriginalPath, in aspectRatio, maxWindowSize, skeletonPadding);
				this.m_aspectRatio = aspectRatio;
				this.m_boundingShape2D = boundingShape2D;
				this.m_maxWindowSize = maxWindowSize;
				this.m_skeletonPadding = skeletonPadding;
				this.CalculateDeltaTransformationMatrix();
				return true;
			}

			// Token: 0x060000C2 RID: 194 RVA: 0x00006B28 File Offset: 0x00004D28
			private bool IsValid(in Collider2D boundingShape2D, in float aspectRatio, in float maxOrthoSize, in float padding)
			{
				return boundingShape2D != null && this.m_boundingShape2D != null && this.m_boundingShape2D == boundingShape2D && this.m_OriginalPath != null && this.m_confinerOven != null && Mathf.Abs(this.m_aspectRatio - aspectRatio) < 0.0001f && Mathf.Abs(this.m_maxWindowSize - maxOrthoSize) < 0.0001f && Mathf.Abs(this.m_skeletonPadding - padding) < 0.0001f;
			}

			// Token: 0x060000C3 RID: 195 RVA: 0x00006BB0 File Offset: 0x00004DB0
			private void CalculateDeltaTransformationMatrix()
			{
				Matrix4x4 i = Matrix4x4.Translate(-this.m_boundingShape2D.offset) * this.m_boundingShape2D.transform.worldToLocalMatrix;
				this.m_DeltaWorldToBaked = this.m_bakedToWorld * i;
				this.m_DeltaBakedToWorld = this.m_DeltaWorldToBaked.inverse;
			}

			// Token: 0x040000A4 RID: 164
			public ConfinerOven m_confinerOven;

			// Token: 0x040000A5 RID: 165
			public List<List<Vector2>> m_OriginalPath;

			// Token: 0x040000A6 RID: 166
			public Matrix4x4 m_DeltaWorldToBaked;

			// Token: 0x040000A7 RID: 167
			public Matrix4x4 m_DeltaBakedToWorld;

			// Token: 0x040000A8 RID: 168
			private float m_aspectRatio;

			// Token: 0x040000A9 RID: 169
			private float m_maxWindowSize;

			// Token: 0x040000AA RID: 170
			private float m_skeletonPadding;

			// Token: 0x040000AB RID: 171
			internal float m_maxComputationTimePerFrameInSeconds;

			// Token: 0x040000AC RID: 172
			private Matrix4x4 m_bakedToWorld;

			// Token: 0x040000AD RID: 173
			private Collider2D m_boundingShape2D;
		}
	}
}
