using System;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x02000065 RID: 101
	public struct CameraState
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000260 RID: 608 RVA: 0x000111EA File Offset: 0x0000F3EA
		public bool HasLookAt
		{
			get
			{
				return this.ReferenceLookAt == this.ReferenceLookAt;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000261 RID: 609 RVA: 0x000111FD File Offset: 0x0000F3FD
		public Vector3 CorrectedPosition
		{
			get
			{
				return this.RawPosition + this.PositionCorrection;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00011210 File Offset: 0x0000F410
		public Quaternion CorrectedOrientation
		{
			get
			{
				return this.RawOrientation * this.OrientationCorrection;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000263 RID: 611 RVA: 0x000111FD File Offset: 0x0000F3FD
		public Vector3 FinalPosition
		{
			get
			{
				return this.RawPosition + this.PositionCorrection;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00011223 File Offset: 0x0000F423
		public Quaternion FinalOrientation
		{
			get
			{
				if (Mathf.Abs(this.Lens.Dutch) > 0.0001f)
				{
					return this.CorrectedOrientation * Quaternion.AngleAxis(this.Lens.Dutch, Vector3.forward);
				}
				return this.CorrectedOrientation;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00011264 File Offset: 0x0000F464
		public static CameraState Default
		{
			get
			{
				return new CameraState
				{
					Lens = LensSettings.Default,
					ReferenceUp = Vector3.up,
					ReferenceLookAt = CameraState.kNoPoint,
					RawPosition = Vector3.zero,
					RawOrientation = Quaternion.identity,
					ShotQuality = 1f,
					PositionCorrection = Vector3.zero,
					OrientationCorrection = Quaternion.identity,
					PositionDampingBypass = Vector3.zero,
					BlendHint = CameraState.BlendHintValue.Nothing
				};
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000266 RID: 614 RVA: 0x000112EE File Offset: 0x0000F4EE
		// (set) Token: 0x06000267 RID: 615 RVA: 0x000112F6 File Offset: 0x0000F4F6
		public int NumCustomBlendables { readonly get; private set; }

		// Token: 0x06000268 RID: 616 RVA: 0x00011300 File Offset: 0x0000F500
		public CameraState.CustomBlendable GetCustomBlendable(int index)
		{
			switch (index)
			{
			case 0:
				return this.mCustom0;
			case 1:
				return this.mCustom1;
			case 2:
				return this.mCustom2;
			case 3:
				return this.mCustom3;
			default:
				index -= 4;
				if (this.m_CustomOverflow != null && index < this.m_CustomOverflow.Count)
				{
					return this.m_CustomOverflow[index];
				}
				return new CameraState.CustomBlendable(null, 0f);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00011374 File Offset: 0x0000F574
		private int FindCustomBlendable(global::UnityEngine.Object custom)
		{
			if (this.mCustom0.m_Custom == custom)
			{
				return 0;
			}
			if (this.mCustom1.m_Custom == custom)
			{
				return 1;
			}
			if (this.mCustom2.m_Custom == custom)
			{
				return 2;
			}
			if (this.mCustom3.m_Custom == custom)
			{
				return 3;
			}
			if (this.m_CustomOverflow != null)
			{
				for (int i = 0; i < this.m_CustomOverflow.Count; i++)
				{
					if (this.m_CustomOverflow[i].m_Custom == custom)
					{
						return i + 4;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00011414 File Offset: 0x0000F614
		public void AddCustomBlendable(CameraState.CustomBlendable b)
		{
			int index = this.FindCustomBlendable(b.m_Custom);
			if (index >= 0)
			{
				b.m_Weight += this.GetCustomBlendable(index).m_Weight;
			}
			else
			{
				int numCustomBlendables = this.NumCustomBlendables;
				this.NumCustomBlendables = numCustomBlendables + 1;
				index = numCustomBlendables;
			}
			switch (index)
			{
			case 0:
				this.mCustom0 = b;
				return;
			case 1:
				this.mCustom1 = b;
				return;
			case 2:
				this.mCustom2 = b;
				return;
			case 3:
				this.mCustom3 = b;
				return;
			default:
				index -= 4;
				if (this.m_CustomOverflow == null)
				{
					this.m_CustomOverflow = new List<CameraState.CustomBlendable>();
				}
				if (index < this.m_CustomOverflow.Count)
				{
					this.m_CustomOverflow[index] = b;
					return;
				}
				this.m_CustomOverflow.Add(b);
				return;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000114D4 File Offset: 0x0000F6D4
		public static CameraState Lerp(CameraState stateA, CameraState stateB, float t)
		{
			t = Mathf.Clamp01(t);
			float adjustedT = t;
			CameraState state = default(CameraState);
			if ((stateA.BlendHint & stateB.BlendHint & CameraState.BlendHintValue.NoPosition) != CameraState.BlendHintValue.Nothing)
			{
				state.BlendHint |= CameraState.BlendHintValue.NoPosition;
			}
			if ((stateA.BlendHint & stateB.BlendHint & CameraState.BlendHintValue.NoOrientation) != CameraState.BlendHintValue.Nothing)
			{
				state.BlendHint |= CameraState.BlendHintValue.NoOrientation;
			}
			if ((stateA.BlendHint & stateB.BlendHint & CameraState.BlendHintValue.NoLens) != CameraState.BlendHintValue.Nothing)
			{
				state.BlendHint |= CameraState.BlendHintValue.NoLens;
			}
			if (((stateA.BlendHint | stateB.BlendHint) & CameraState.BlendHintValue.SphericalPositionBlend) != CameraState.BlendHintValue.Nothing)
			{
				state.BlendHint |= CameraState.BlendHintValue.SphericalPositionBlend;
			}
			if (((stateA.BlendHint | stateB.BlendHint) & CameraState.BlendHintValue.CylindricalPositionBlend) != CameraState.BlendHintValue.Nothing)
			{
				state.BlendHint |= CameraState.BlendHintValue.CylindricalPositionBlend;
			}
			if (((stateA.BlendHint | stateB.BlendHint) & CameraState.BlendHintValue.NoLens) == CameraState.BlendHintValue.Nothing)
			{
				state.Lens = LensSettings.Lerp(stateA.Lens, stateB.Lens, t);
			}
			else if ((stateA.BlendHint & stateB.BlendHint & CameraState.BlendHintValue.NoLens) == CameraState.BlendHintValue.Nothing)
			{
				if ((stateA.BlendHint & CameraState.BlendHintValue.NoLens) != CameraState.BlendHintValue.Nothing)
				{
					state.Lens = stateB.Lens;
				}
				else
				{
					state.Lens = stateA.Lens;
				}
			}
			state.ReferenceUp = Vector3.Slerp(stateA.ReferenceUp, stateB.ReferenceUp, t);
			state.ShotQuality = Mathf.Lerp(stateA.ShotQuality, stateB.ShotQuality, t);
			state.PositionCorrection = CameraState.ApplyPosBlendHint(stateA.PositionCorrection, stateA.BlendHint, stateB.PositionCorrection, stateB.BlendHint, state.PositionCorrection, Vector3.Lerp(stateA.PositionCorrection, stateB.PositionCorrection, t));
			state.OrientationCorrection = CameraState.ApplyRotBlendHint(stateA.OrientationCorrection, stateA.BlendHint, stateB.OrientationCorrection, stateB.BlendHint, state.OrientationCorrection, Quaternion.Slerp(stateA.OrientationCorrection, stateB.OrientationCorrection, t));
			if (!stateA.HasLookAt || !stateB.HasLookAt)
			{
				state.ReferenceLookAt = CameraState.kNoPoint;
			}
			else
			{
				float fovA = stateA.Lens.FieldOfView;
				float fovB = stateB.Lens.FieldOfView;
				if (((stateA.BlendHint | stateB.BlendHint) & CameraState.BlendHintValue.NoLens) == CameraState.BlendHintValue.Nothing && !state.Lens.Orthographic && !Mathf.Approximately(fovA, fovB))
				{
					LensSettings lens = state.Lens;
					lens.FieldOfView = CameraState.InterpolateFOV(fovA, fovB, Mathf.Max((stateA.ReferenceLookAt - stateA.CorrectedPosition).magnitude, stateA.Lens.NearClipPlane), Mathf.Max((stateB.ReferenceLookAt - stateB.CorrectedPosition).magnitude, stateB.Lens.NearClipPlane), t);
					state.Lens = lens;
					adjustedT = Mathf.Abs((lens.FieldOfView - fovA) / (fovB - fovA));
				}
				state.ReferenceLookAt = Vector3.Lerp(stateA.ReferenceLookAt, stateB.ReferenceLookAt, adjustedT);
			}
			state.RawPosition = CameraState.ApplyPosBlendHint(stateA.RawPosition, stateA.BlendHint, stateB.RawPosition, stateB.BlendHint, state.RawPosition, state.InterpolatePosition(stateA.RawPosition, stateA.ReferenceLookAt, stateB.RawPosition, stateB.ReferenceLookAt, t));
			if (state.HasLookAt && ((stateA.BlendHint | stateB.BlendHint) & CameraState.BlendHintValue.RadialAimBlend) != CameraState.BlendHintValue.Nothing)
			{
				state.ReferenceLookAt = state.RawPosition + Vector3.Slerp(stateA.ReferenceLookAt - state.RawPosition, stateB.ReferenceLookAt - state.RawPosition, adjustedT);
			}
			Quaternion newOrient = state.RawOrientation;
			if (((stateA.BlendHint | stateB.BlendHint) & CameraState.BlendHintValue.NoOrientation) == CameraState.BlendHintValue.Nothing)
			{
				Vector3 dirTarget = Vector3.zero;
				if (state.HasLookAt && Quaternion.Angle(stateA.RawOrientation, stateB.RawOrientation) > 0.0001f)
				{
					dirTarget = state.ReferenceLookAt - state.CorrectedPosition;
				}
				if (dirTarget.AlmostZero() || ((stateA.BlendHint | stateB.BlendHint) & CameraState.BlendHintValue.IgnoreLookAtTarget) != CameraState.BlendHintValue.Nothing)
				{
					newOrient = Quaternion.Slerp(stateA.RawOrientation, stateB.RawOrientation, t);
				}
				else
				{
					Vector3 up = state.ReferenceUp;
					dirTarget.Normalize();
					if (Vector3.Cross(dirTarget, up).AlmostZero())
					{
						newOrient = Quaternion.Slerp(stateA.RawOrientation, stateB.RawOrientation, t);
						up = newOrient * Vector3.up;
					}
					newOrient = Quaternion.LookRotation(dirTarget, up);
					Vector2 deltaA = -stateA.RawOrientation.GetCameraRotationToTarget(stateA.ReferenceLookAt - stateA.CorrectedPosition, up);
					Vector2 deltaB = -stateB.RawOrientation.GetCameraRotationToTarget(stateB.ReferenceLookAt - stateB.CorrectedPosition, up);
					newOrient = newOrient.ApplyCameraRotation(Vector2.Lerp(deltaA, deltaB, adjustedT), up);
				}
			}
			state.RawOrientation = CameraState.ApplyRotBlendHint(stateA.RawOrientation, stateA.BlendHint, stateB.RawOrientation, stateB.BlendHint, state.RawOrientation, newOrient);
			for (int i = 0; i < stateA.NumCustomBlendables; i++)
			{
				CameraState.CustomBlendable b = stateA.GetCustomBlendable(i);
				b.m_Weight *= 1f - t;
				if (b.m_Weight > 0f)
				{
					state.AddCustomBlendable(b);
				}
			}
			for (int j = 0; j < stateB.NumCustomBlendables; j++)
			{
				CameraState.CustomBlendable b2 = stateB.GetCustomBlendable(j);
				b2.m_Weight *= t;
				if (b2.m_Weight > 0f)
				{
					state.AddCustomBlendable(b2);
				}
			}
			return state;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00011A34 File Offset: 0x0000FC34
		private static float InterpolateFOV(float fovA, float fovB, float dA, float dB, float t)
		{
			float num = dA * 2f * Mathf.Tan(fovA * 0.017453292f / 2f);
			float hB = dB * 2f * Mathf.Tan(fovB * 0.017453292f / 2f);
			float h = Mathf.Lerp(num, hB, t);
			float fov = 179f;
			float d = Mathf.Lerp(dA, dB, t);
			if (d > 0.0001f)
			{
				fov = 2f * Mathf.Atan(h / (2f * d)) * 57.29578f;
			}
			return Mathf.Clamp(fov, Mathf.Min(fovA, fovB), Mathf.Max(fovA, fovB));
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00011AC6 File Offset: 0x0000FCC6
		private static Vector3 ApplyPosBlendHint(Vector3 posA, CameraState.BlendHintValue hintA, Vector3 posB, CameraState.BlendHintValue hintB, Vector3 original, Vector3 blended)
		{
			if (((hintA | hintB) & CameraState.BlendHintValue.NoPosition) == CameraState.BlendHintValue.Nothing)
			{
				return blended;
			}
			if ((hintA & hintB & CameraState.BlendHintValue.NoPosition) != CameraState.BlendHintValue.Nothing)
			{
				return original;
			}
			if ((hintA & CameraState.BlendHintValue.NoPosition) != CameraState.BlendHintValue.Nothing)
			{
				return posB;
			}
			return posA;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00011AE4 File Offset: 0x0000FCE4
		private static Quaternion ApplyRotBlendHint(Quaternion rotA, CameraState.BlendHintValue hintA, Quaternion rotB, CameraState.BlendHintValue hintB, Quaternion original, Quaternion blended)
		{
			if (((hintA | hintB) & CameraState.BlendHintValue.NoOrientation) == CameraState.BlendHintValue.Nothing)
			{
				return blended;
			}
			if ((hintA & hintB & CameraState.BlendHintValue.NoOrientation) != CameraState.BlendHintValue.Nothing)
			{
				return original;
			}
			if ((hintA & CameraState.BlendHintValue.NoOrientation) != CameraState.BlendHintValue.Nothing)
			{
				return rotB;
			}
			return rotA;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00011B04 File Offset: 0x0000FD04
		private Vector3 InterpolatePosition(Vector3 posA, Vector3 pivotA, Vector3 posB, Vector3 pivotB, float t)
		{
			if (pivotA == pivotA && pivotB == pivotB)
			{
				if ((this.BlendHint & CameraState.BlendHintValue.CylindricalPositionBlend) != CameraState.BlendHintValue.Nothing)
				{
					Vector3 a = Vector3.ProjectOnPlane(posA - pivotA, this.ReferenceUp);
					Vector3 b = Vector3.ProjectOnPlane(posB - pivotB, this.ReferenceUp);
					Vector3 c = Vector3.Slerp(a, b, t);
					posA = posA - a + c;
					posB = posB - b + c;
				}
				else if ((this.BlendHint & CameraState.BlendHintValue.SphericalPositionBlend) != CameraState.BlendHintValue.Nothing)
				{
					Vector3 c2 = Vector3.Slerp(posA - pivotA, posB - pivotB, t);
					posA = pivotA + c2;
					posB = pivotB + c2;
				}
			}
			return Vector3.Lerp(posA, posB, t);
		}

		// Token: 0x04000258 RID: 600
		public LensSettings Lens;

		// Token: 0x04000259 RID: 601
		public Vector3 ReferenceUp;

		// Token: 0x0400025A RID: 602
		public Vector3 ReferenceLookAt;

		// Token: 0x0400025B RID: 603
		public static Vector3 kNoPoint = new Vector3(float.NaN, float.NaN, float.NaN);

		// Token: 0x0400025C RID: 604
		public Vector3 RawPosition;

		// Token: 0x0400025D RID: 605
		public Quaternion RawOrientation;

		// Token: 0x0400025E RID: 606
		public Vector3 PositionDampingBypass;

		// Token: 0x0400025F RID: 607
		public float ShotQuality;

		// Token: 0x04000260 RID: 608
		public Vector3 PositionCorrection;

		// Token: 0x04000261 RID: 609
		public Quaternion OrientationCorrection;

		// Token: 0x04000262 RID: 610
		public CameraState.BlendHintValue BlendHint;

		// Token: 0x04000263 RID: 611
		private CameraState.CustomBlendable mCustom0;

		// Token: 0x04000264 RID: 612
		private CameraState.CustomBlendable mCustom1;

		// Token: 0x04000265 RID: 613
		private CameraState.CustomBlendable mCustom2;

		// Token: 0x04000266 RID: 614
		private CameraState.CustomBlendable mCustom3;

		// Token: 0x04000267 RID: 615
		private List<CameraState.CustomBlendable> m_CustomOverflow;

		// Token: 0x02000066 RID: 102
		public enum BlendHintValue
		{
			// Token: 0x0400026A RID: 618
			Nothing,
			// Token: 0x0400026B RID: 619
			NoPosition,
			// Token: 0x0400026C RID: 620
			NoOrientation,
			// Token: 0x0400026D RID: 621
			NoTransform,
			// Token: 0x0400026E RID: 622
			SphericalPositionBlend,
			// Token: 0x0400026F RID: 623
			CylindricalPositionBlend = 8,
			// Token: 0x04000270 RID: 624
			RadialAimBlend = 16,
			// Token: 0x04000271 RID: 625
			IgnoreLookAtTarget = 32,
			// Token: 0x04000272 RID: 626
			NoLens = 64
		}

		// Token: 0x02000067 RID: 103
		public struct CustomBlendable
		{
			// Token: 0x06000271 RID: 625 RVA: 0x00011BDE File Offset: 0x0000FDDE
			public CustomBlendable(global::UnityEngine.Object custom, float weight)
			{
				this.m_Custom = custom;
				this.m_Weight = weight;
			}

			// Token: 0x04000273 RID: 627
			public global::UnityEngine.Object m_Custom;

			// Token: 0x04000274 RID: 628
			public float m_Weight;
		}
	}
}
