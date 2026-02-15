using System;
using Cinemachine.Utility;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x0200007B RID: 123
	public abstract class CinemachinePathBase : MonoBehaviour
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000300 RID: 768
		public abstract float MinPos { get; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000301 RID: 769
		public abstract float MaxPos { get; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000302 RID: 770
		public abstract bool Looped { get; }

		// Token: 0x06000303 RID: 771 RVA: 0x00013264 File Offset: 0x00011464
		public virtual float StandardizePos(float pos)
		{
			if (this.Looped && this.MaxPos > 0f)
			{
				pos %= this.MaxPos;
				if (pos < 0f)
				{
					pos += this.MaxPos;
				}
				return pos;
			}
			return Mathf.Clamp(pos, 0f, this.MaxPos);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000132B5 File Offset: 0x000114B5
		public virtual Vector3 EvaluatePosition(float pos)
		{
			return base.transform.TransformPoint(this.EvaluateLocalPosition(pos));
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000132C9 File Offset: 0x000114C9
		public virtual Vector3 EvaluateTangent(float pos)
		{
			return base.transform.TransformDirection(this.EvaluateLocalTangent(pos));
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000132DD File Offset: 0x000114DD
		public virtual Quaternion EvaluateOrientation(float pos)
		{
			return base.transform.rotation * this.EvaluateLocalOrientation(pos);
		}

		// Token: 0x06000307 RID: 775
		public abstract Vector3 EvaluateLocalPosition(float pos);

		// Token: 0x06000308 RID: 776
		public abstract Vector3 EvaluateLocalTangent(float pos);

		// Token: 0x06000309 RID: 777
		public abstract Quaternion EvaluateLocalOrientation(float pos);

		// Token: 0x0600030A RID: 778 RVA: 0x000132F8 File Offset: 0x000114F8
		public virtual float FindClosestPoint(Vector3 p, int startSegment, int searchRadius, int stepsPerSegment)
		{
			float start = this.MinPos;
			float end = this.MaxPos;
			if (searchRadius >= 0)
			{
				if (this.Looped)
				{
					int r = Mathf.Min(searchRadius, Mathf.FloorToInt((end - start) / 2f));
					start = (float)(startSegment - r);
					end = (float)(startSegment + r + 1);
				}
				else
				{
					start = Mathf.Max((float)(startSegment - searchRadius), this.MinPos);
					end = Mathf.Min((float)(startSegment + searchRadius + 1), this.MaxPos);
				}
			}
			stepsPerSegment = Mathf.RoundToInt(Mathf.Clamp((float)stepsPerSegment, 1f, 100f));
			float stepSize = 1f / (float)stepsPerSegment;
			float bestPos = (float)startSegment;
			float bestDistance = float.MaxValue;
			int iterations = ((stepsPerSegment == 1) ? 1 : 3);
			for (int i = 0; i < iterations; i++)
			{
				Vector3 v0 = this.EvaluatePosition(start);
				for (float f = start + stepSize; f <= end; f += stepSize)
				{
					Vector3 v = this.EvaluatePosition(f);
					float t = p.ClosestPointOnSegment(v0, v);
					float d = Vector3.SqrMagnitude(p - Vector3.Lerp(v0, v, t));
					if (d < bestDistance)
					{
						bestDistance = d;
						bestPos = f - (1f - t) * stepSize;
					}
					v0 = v;
				}
				start = bestPos - stepSize;
				end = bestPos + stepSize;
				stepSize /= (float)stepsPerSegment;
			}
			return bestPos;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00013424 File Offset: 0x00011624
		public float MinUnit(CinemachinePathBase.PositionUnits units)
		{
			if (units == CinemachinePathBase.PositionUnits.Normalized)
			{
				return 0f;
			}
			if (units != CinemachinePathBase.PositionUnits.Distance)
			{
				return this.MinPos;
			}
			return 0f;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00013440 File Offset: 0x00011640
		public float MaxUnit(CinemachinePathBase.PositionUnits units)
		{
			if (units == CinemachinePathBase.PositionUnits.Normalized)
			{
				return 1f;
			}
			if (units != CinemachinePathBase.PositionUnits.Distance)
			{
				return this.MaxPos;
			}
			return this.PathLength;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00013460 File Offset: 0x00011660
		public virtual float StandardizeUnit(float pos, CinemachinePathBase.PositionUnits units)
		{
			if (units == CinemachinePathBase.PositionUnits.PathUnits)
			{
				return this.StandardizePos(pos);
			}
			if (units == CinemachinePathBase.PositionUnits.Distance)
			{
				return this.StandardizePathDistance(pos);
			}
			float len = this.PathLength;
			if (len < 0.0001f)
			{
				return 0f;
			}
			return this.StandardizePathDistance(pos * len) / len;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000134A4 File Offset: 0x000116A4
		public Vector3 EvaluatePositionAtUnit(float pos, CinemachinePathBase.PositionUnits units)
		{
			return this.EvaluatePosition(this.ToNativePathUnits(pos, units));
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000134B4 File Offset: 0x000116B4
		public Vector3 EvaluateTangentAtUnit(float pos, CinemachinePathBase.PositionUnits units)
		{
			return this.EvaluateTangent(this.ToNativePathUnits(pos, units));
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000134C4 File Offset: 0x000116C4
		public Quaternion EvaluateOrientationAtUnit(float pos, CinemachinePathBase.PositionUnits units)
		{
			return this.EvaluateOrientation(this.ToNativePathUnits(pos, units));
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000311 RID: 785
		public abstract int DistanceCacheSampleStepsPerSegment { get; }

		// Token: 0x06000312 RID: 786 RVA: 0x000134D4 File Offset: 0x000116D4
		public virtual void InvalidateDistanceCache()
		{
			this.m_DistanceToPos = null;
			this.m_PosToDistance = null;
			this.m_CachedSampleSteps = 0;
			this.m_PathLength = 0f;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000134F6 File Offset: 0x000116F6
		public bool DistanceCacheIsValid()
		{
			return this.MaxPos == this.MinPos || (this.m_DistanceToPos != null && this.m_PosToDistance != null && this.m_CachedSampleSteps == this.DistanceCacheSampleStepsPerSegment && this.m_CachedSampleSteps > 0);
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00013531 File Offset: 0x00011731
		public float PathLength
		{
			get
			{
				if (this.DistanceCacheSampleStepsPerSegment < 1)
				{
					return 0f;
				}
				if (!this.DistanceCacheIsValid())
				{
					this.ResamplePath(this.DistanceCacheSampleStepsPerSegment);
				}
				return this.m_PathLength;
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0001355C File Offset: 0x0001175C
		public float StandardizePathDistance(float distance)
		{
			float length = this.PathLength;
			if (length < 1E-05f)
			{
				return 0f;
			}
			if (this.Looped)
			{
				distance %= length;
				if (distance < 0f)
				{
					distance += length;
				}
			}
			return Mathf.Clamp(distance, 0f, length);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000135A4 File Offset: 0x000117A4
		public float ToNativePathUnits(float pos, CinemachinePathBase.PositionUnits units)
		{
			if (units == CinemachinePathBase.PositionUnits.PathUnits)
			{
				return pos;
			}
			if (this.DistanceCacheSampleStepsPerSegment < 1 || this.PathLength < 0.0001f)
			{
				return this.MinPos;
			}
			if (units == CinemachinePathBase.PositionUnits.Normalized)
			{
				pos *= this.PathLength;
			}
			pos = this.StandardizePathDistance(pos);
			float d = pos / this.m_cachedDistanceStepSize;
			int i = Mathf.FloorToInt(d);
			if (i >= this.m_DistanceToPos.Length - 1)
			{
				return this.MaxPos;
			}
			float t = d - (float)i;
			return this.MinPos + Mathf.Lerp(this.m_DistanceToPos[i], this.m_DistanceToPos[i + 1], t);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00013634 File Offset: 0x00011834
		public float FromPathNativeUnits(float pos, CinemachinePathBase.PositionUnits units)
		{
			if (units == CinemachinePathBase.PositionUnits.PathUnits)
			{
				return pos;
			}
			float length = this.PathLength;
			if (this.DistanceCacheSampleStepsPerSegment < 1 || length < 0.0001f)
			{
				return 0f;
			}
			pos = this.StandardizePos(pos);
			float d = pos / this.m_cachedPosStepSize;
			int i = Mathf.FloorToInt(d);
			if (i >= this.m_PosToDistance.Length - 1)
			{
				pos = this.m_PathLength;
			}
			else
			{
				float t = d - (float)i;
				pos = Mathf.Lerp(this.m_PosToDistance[i], this.m_PosToDistance[i + 1], t);
			}
			if (units == CinemachinePathBase.PositionUnits.Normalized)
			{
				pos /= length;
			}
			return pos;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000136C0 File Offset: 0x000118C0
		private void ResamplePath(int stepsPerSegment)
		{
			this.InvalidateDistanceCache();
			float minPos = this.MinPos;
			float maxPos = this.MaxPos;
			float stepSize = 1f / (float)Mathf.Max(1, stepsPerSegment);
			int numKeys = Mathf.RoundToInt((maxPos - minPos) / stepSize) + 1;
			this.m_PosToDistance = new float[numKeys];
			this.m_CachedSampleSteps = stepsPerSegment;
			this.m_cachedPosStepSize = stepSize;
			Vector3 p0 = this.EvaluatePosition(0f);
			this.m_PosToDistance[0] = 0f;
			float pos = minPos;
			for (int i = 1; i < numKeys; i++)
			{
				pos += stepSize;
				Vector3 p = this.EvaluatePosition(pos);
				float d = Vector3.Distance(p0, p);
				this.m_PathLength += d;
				p0 = p;
				this.m_PosToDistance[i] = this.m_PathLength;
			}
			this.m_DistanceToPos = new float[numKeys];
			this.m_DistanceToPos[0] = 0f;
			if (numKeys > 1)
			{
				stepSize = this.m_PathLength / (float)(numKeys - 1);
				this.m_cachedDistanceStepSize = stepSize;
				float distance = 0f;
				int posIndex = 1;
				for (int j = 1; j < numKeys; j++)
				{
					distance += stepSize;
					float d2 = this.m_PosToDistance[posIndex];
					while (d2 < distance && posIndex < numKeys - 1)
					{
						d2 = this.m_PosToDistance[++posIndex];
					}
					float d3 = this.m_PosToDistance[posIndex - 1];
					float delta = d2 - d3;
					float t = (distance - d3) / delta;
					this.m_DistanceToPos[j] = this.m_cachedPosStepSize * (t + (float)posIndex - 1f);
				}
			}
		}

		// Token: 0x040002CC RID: 716
		[Tooltip("Path samples per waypoint.  This is used for calculating path distances.")]
		[Range(1f, 100f)]
		public int m_Resolution = 20;

		// Token: 0x040002CD RID: 717
		[Tooltip("The settings that control how the path will appear in the editor scene view.")]
		public CinemachinePathBase.Appearance m_Appearance = new CinemachinePathBase.Appearance();

		// Token: 0x040002CE RID: 718
		private float[] m_DistanceToPos;

		// Token: 0x040002CF RID: 719
		private float[] m_PosToDistance;

		// Token: 0x040002D0 RID: 720
		private int m_CachedSampleSteps;

		// Token: 0x040002D1 RID: 721
		private float m_PathLength;

		// Token: 0x040002D2 RID: 722
		private float m_cachedPosStepSize;

		// Token: 0x040002D3 RID: 723
		private float m_cachedDistanceStepSize;

		// Token: 0x0200007C RID: 124
		[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
		[Serializable]
		public class Appearance
		{
			// Token: 0x040002D4 RID: 724
			[Tooltip("The color of the path itself when it is active in the editor")]
			public Color pathColor = Color.green;

			// Token: 0x040002D5 RID: 725
			[Tooltip("The color of the path itself when it is inactive in the editor")]
			public Color inactivePathColor = Color.gray;

			// Token: 0x040002D6 RID: 726
			[Tooltip("The width of the railroad-tracks that are drawn to represent the path")]
			[Range(0f, 10f)]
			public float width = 0.2f;
		}

		// Token: 0x0200007D RID: 125
		public enum PositionUnits
		{
			// Token: 0x040002D8 RID: 728
			PathUnits,
			// Token: 0x040002D9 RID: 729
			Distance,
			// Token: 0x040002DA RID: 730
			Normalized
		}
	}
}
