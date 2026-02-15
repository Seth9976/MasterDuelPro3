using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003A5 RID: 933
	[UsedByNativeCode]
	public struct ScriptableCullingParameters : IEquatable<ScriptableCullingParameters>
	{
		// Token: 0x170003A2 RID: 930
		// (set) Token: 0x0600194D RID: 6477 RVA: 0x00036EE1 File Offset: 0x000350E1
		public int maximumVisibleLights
		{
			set
			{
				this.m_maximumVisibleLights = value;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (set) Token: 0x0600194E RID: 6478 RVA: 0x00036EEB File Offset: 0x000350EB
		public bool conservativeEnclosingSphere
		{
			set
			{
				this.m_ConservativeEnclosingSphere = value;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (set) Token: 0x0600194F RID: 6479 RVA: 0x00036EF5 File Offset: 0x000350F5
		public int numIterationsEnclosingSphere
		{
			set
			{
				this.m_NumIterationsEnclosingSphere = value;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x00036F00 File Offset: 0x00035100
		public int cullingPlaneCount
		{
			get
			{
				return this.m_CullingPlaneCount;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (set) Token: 0x06001951 RID: 6481 RVA: 0x00036F18 File Offset: 0x00035118
		public bool isOrthographic
		{
			set
			{
				this.m_LODParameters.isOrthographic = value;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (set) Token: 0x06001952 RID: 6482 RVA: 0x00036F28 File Offset: 0x00035128
		public float shadowDistance
		{
			set
			{
				this.m_ShadowDistance = value;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x00036F34 File Offset: 0x00035134
		// (set) Token: 0x06001954 RID: 6484 RVA: 0x00036F4C File Offset: 0x0003514C
		public CullingOptions cullingOptions
		{
			get
			{
				return this.m_CullingOptions;
			}
			set
			{
				this.m_CullingOptions = value;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (set) Token: 0x06001955 RID: 6485 RVA: 0x00036F56 File Offset: 0x00035156
		public ReflectionProbeSortingCriteria reflectionProbeSortingCriteria
		{
			set
			{
				this.m_ReflectionProbeSortingCriteria = value;
			}
		}

		// Token: 0x170003AA RID: 938
		// (set) Token: 0x06001956 RID: 6486 RVA: 0x00036F60 File Offset: 0x00035160
		public Matrix4x4 stereoViewMatrix
		{
			set
			{
				this.m_StereoViewMatrix = value;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x00036F6C File Offset: 0x0003516C
		// (set) Token: 0x06001958 RID: 6488 RVA: 0x00036F84 File Offset: 0x00035184
		public Matrix4x4 stereoProjectionMatrix
		{
			get
			{
				return this.m_StereoProjectionMatrix;
			}
			set
			{
				this.m_StereoProjectionMatrix = value;
			}
		}

		// Token: 0x170003AC RID: 940
		// (set) Token: 0x06001959 RID: 6489 RVA: 0x00036F8E File Offset: 0x0003518E
		public float stereoSeparationDistance
		{
			set
			{
				this.m_StereoSeparationDistance = value;
			}
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x00036F98 File Offset: 0x00035198
		public unsafe float GetLayerCullingDistance(int layerIndex)
		{
			bool flag = layerIndex < 0 || layerIndex >= 32;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "layerIndex", layerIndex, 32));
			}
			fixed (float* ptr2 = &this.m_LayerFarCullDistances.FixedElementField)
			{
				float* ptr = ptr2;
				return ptr[layerIndex];
			}
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x00036FF8 File Offset: 0x000351F8
		public unsafe Plane GetCullingPlane(int index)
		{
			bool flag = index < 0 || index >= this.cullingPlaneCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, this.cullingPlaneCount));
			}
			fixed (byte* ptr2 = &this.m_CullingPlanes.FixedElementField)
			{
				byte* ptr = ptr2;
				Plane* planes = (Plane*)ptr;
				return planes[index];
			}
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0003706C File Offset: 0x0003526C
		public bool Equals(ScriptableCullingParameters other)
		{
			for (int i = 0; i < 32; i++)
			{
				bool flag = !this.GetLayerCullingDistance(i).Equals(other.GetLayerCullingDistance(i));
				if (flag)
				{
					return false;
				}
			}
			for (int j = 0; j < this.cullingPlaneCount; j++)
			{
				bool flag2 = !this.GetCullingPlane(j).Equals(other.GetCullingPlane(j));
				if (flag2)
				{
					return false;
				}
			}
			return this.m_LODParameters.Equals(other.m_LODParameters) && this.m_CullingPlaneCount == other.m_CullingPlaneCount && this.m_CullingMask == other.m_CullingMask && this.m_SceneMask == other.m_SceneMask && this.m_ViewID == other.m_ViewID && this.m_LayerCull == other.m_LayerCull && this.m_CullingMatrix.Equals(other.m_CullingMatrix) && this.m_Origin.Equals(other.m_Origin) && this.m_ShadowDistance.Equals(other.m_ShadowDistance) && this.m_ShadowNearPlaneOffset.Equals(other.m_ShadowNearPlaneOffset) && this.m_CullingOptions == other.m_CullingOptions && this.m_ReflectionProbeSortingCriteria == other.m_ReflectionProbeSortingCriteria && this.m_CameraProperties.Equals(other.m_CameraProperties) && this.m_AccurateOcclusionThreshold.Equals(other.m_AccurateOcclusionThreshold) && this.m_StereoViewMatrix.Equals(other.m_StereoViewMatrix) && this.m_StereoProjectionMatrix.Equals(other.m_StereoProjectionMatrix) && this.m_StereoSeparationDistance.Equals(other.m_StereoSeparationDistance) && this.m_maximumVisibleLights == other.m_maximumVisibleLights && this.m_ConservativeEnclosingSphere == other.m_ConservativeEnclosingSphere && this.m_NumIterationsEnclosingSphere == other.m_NumIterationsEnclosingSphere;
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0003727C File Offset: 0x0003547C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ScriptableCullingParameters && this.Equals((ScriptableCullingParameters)obj);
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x000372B4 File Offset: 0x000354B4
		public override int GetHashCode()
		{
			int hashCode = this.m_LODParameters.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_CullingPlaneCount;
			hashCode = (hashCode * 397) ^ (int)this.m_CullingMask;
			hashCode = (hashCode * 397) ^ this.m_SceneMask.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_ViewID.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_LayerCull;
			hashCode = (hashCode * 397) ^ this.m_CullingMatrix.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_Origin.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_ShadowDistance.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_ShadowNearPlaneOffset.GetHashCode();
			hashCode = (hashCode * 397) ^ (int)this.m_CullingOptions;
			hashCode = (hashCode * 397) ^ (int)this.m_ReflectionProbeSortingCriteria;
			hashCode = (hashCode * 397) ^ this.m_CameraProperties.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_AccurateOcclusionThreshold.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_MaximumPortalCullingJobs.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_StereoViewMatrix.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_StereoProjectionMatrix.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_StereoSeparationDistance.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_maximumVisibleLights;
			hashCode = (hashCode * 397) ^ this.m_ConservativeEnclosingSphere.GetHashCode();
			return (hashCode * 397) ^ this.m_NumIterationsEnclosingSphere.GetHashCode();
		}

		// Token: 0x04000BBF RID: 3007
		private LODParameters m_LODParameters;

		// Token: 0x04000BC0 RID: 3008
		private const int k_MaximumCullingPlaneCount = 10;

		// Token: 0x04000BC1 RID: 3009
		public static readonly int maximumCullingPlaneCount = 10;

		// Token: 0x04000BC2 RID: 3010
		[FixedBuffer(typeof(byte), 160)]
		internal ScriptableCullingParameters.<m_CullingPlanes>e__FixedBuffer m_CullingPlanes;

		// Token: 0x04000BC3 RID: 3011
		private int m_CullingPlaneCount;

		// Token: 0x04000BC4 RID: 3012
		private uint m_CullingMask;

		// Token: 0x04000BC5 RID: 3013
		private ulong m_SceneMask;

		// Token: 0x04000BC6 RID: 3014
		private ulong m_ViewID;

		// Token: 0x04000BC7 RID: 3015
		private const int k_LayerCount = 32;

		// Token: 0x04000BC8 RID: 3016
		public static readonly int layerCount = 32;

		// Token: 0x04000BC9 RID: 3017
		[FixedBuffer(typeof(float), 32)]
		internal ScriptableCullingParameters.<m_LayerFarCullDistances>e__FixedBuffer m_LayerFarCullDistances;

		// Token: 0x04000BCA RID: 3018
		private int m_LayerCull;

		// Token: 0x04000BCB RID: 3019
		private Matrix4x4 m_CullingMatrix;

		// Token: 0x04000BCC RID: 3020
		private Vector3 m_Origin;

		// Token: 0x04000BCD RID: 3021
		private float m_ShadowDistance;

		// Token: 0x04000BCE RID: 3022
		private float m_ShadowNearPlaneOffset;

		// Token: 0x04000BCF RID: 3023
		private CullingOptions m_CullingOptions;

		// Token: 0x04000BD0 RID: 3024
		private ReflectionProbeSortingCriteria m_ReflectionProbeSortingCriteria;

		// Token: 0x04000BD1 RID: 3025
		private CameraProperties m_CameraProperties;

		// Token: 0x04000BD2 RID: 3026
		private float m_AccurateOcclusionThreshold;

		// Token: 0x04000BD3 RID: 3027
		private int m_MaximumPortalCullingJobs;

		// Token: 0x04000BD4 RID: 3028
		private const int k_CullingJobCountLowerLimit = 1;

		// Token: 0x04000BD5 RID: 3029
		private const int k_CullingJobCountUpperLimit = 16;

		// Token: 0x04000BD6 RID: 3030
		private Matrix4x4 m_StereoViewMatrix;

		// Token: 0x04000BD7 RID: 3031
		private Matrix4x4 m_StereoProjectionMatrix;

		// Token: 0x04000BD8 RID: 3032
		private float m_StereoSeparationDistance;

		// Token: 0x04000BD9 RID: 3033
		private int m_maximumVisibleLights;

		// Token: 0x04000BDA RID: 3034
		private bool m_ConservativeEnclosingSphere;

		// Token: 0x04000BDB RID: 3035
		private int m_NumIterationsEnclosingSphere;

		// Token: 0x020003A6 RID: 934
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 160)]
		public struct <m_CullingPlanes>e__FixedBuffer
		{
			// Token: 0x04000BDC RID: 3036
			public byte FixedElementField;
		}

		// Token: 0x020003A7 RID: 935
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 128)]
		public struct <m_LayerFarCullDistances>e__FixedBuffer
		{
			// Token: 0x04000BDD RID: 3037
			public float FixedElementField;
		}
	}
}
