using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003A0 RID: 928
	[UsedByNativeCode]
	public struct CameraProperties : IEquatable<CameraProperties>
	{
		// Token: 0x06001948 RID: 6472 RVA: 0x00036730 File Offset: 0x00034930
		public unsafe Plane GetShadowCullingPlane(int index)
		{
			bool flag = index < 0 || index >= 6;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, 6));
			}
			fixed (byte* ptr2 = &this.m_ShadowCullPlanes.FixedElementField)
			{
				byte* ptr = ptr2;
				Plane* planes = (Plane*)ptr;
				return planes[index];
			}
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x0003679C File Offset: 0x0003499C
		public unsafe Plane GetCameraCullingPlane(int index)
		{
			bool flag = index < 0 || index >= 6;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, 6));
			}
			fixed (byte* ptr2 = &this.m_CameraCullPlanes.FixedElementField)
			{
				byte* ptr = ptr2;
				Plane* planes = (Plane*)ptr;
				return planes[index];
			}
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x00036808 File Offset: 0x00034A08
		public unsafe bool Equals(CameraProperties other)
		{
			for (int i = 0; i < 6; i++)
			{
				bool flag = !this.GetShadowCullingPlane(i).Equals(other.GetShadowCullingPlane(i));
				if (flag)
				{
					return false;
				}
			}
			for (int j = 0; j < 6; j++)
			{
				bool flag2 = !this.GetCameraCullingPlane(j).Equals(other.GetCameraCullingPlane(j));
				if (flag2)
				{
					return false;
				}
			}
			fixed (float* ptr = &this.layerCullDistances.FixedElementField)
			{
				float* distancesPtr = ptr;
				for (int k = 0; k < 32; k++)
				{
					bool flag3 = distancesPtr[k] != *((ref other.layerCullDistances.FixedElementField) + (IntPtr)k * 4);
					if (flag3)
					{
						return false;
					}
				}
			}
			return this.screenRect.Equals(other.screenRect) && this.viewDir.Equals(other.viewDir) && this.projectionNear.Equals(other.projectionNear) && this.projectionFar.Equals(other.projectionFar) && this.cameraNear.Equals(other.cameraNear) && this.cameraFar.Equals(other.cameraFar) && this.cameraAspect.Equals(other.cameraAspect) && this.cameraToWorld.Equals(other.cameraToWorld) && this.actualWorldToClip.Equals(other.actualWorldToClip) && this.cameraClipToWorld.Equals(other.cameraClipToWorld) && this.cameraWorldToClip.Equals(other.cameraWorldToClip) && this.implicitProjection.Equals(other.implicitProjection) && this.stereoWorldToClipLeft.Equals(other.stereoWorldToClipLeft) && this.stereoWorldToClipRight.Equals(other.stereoWorldToClipRight) && this.worldToCamera.Equals(other.worldToCamera) && this.up.Equals(other.up) && this.right.Equals(other.right) && this.transformDirection.Equals(other.transformDirection) && this.cameraEuler.Equals(other.cameraEuler) && this.velocity.Equals(other.velocity) && this.farPlaneWorldSpaceLength.Equals(other.farPlaneWorldSpaceLength) && this.rendererCount == other.rendererCount && this.baseFarDistance.Equals(other.baseFarDistance) && this.shadowCullCenter.Equals(other.shadowCullCenter) && this.layerCullSpherical == other.layerCullSpherical && this.coreCameraValues.Equals(other.coreCameraValues) && this.cameraType == other.cameraType && this.projectionIsOblique == other.projectionIsOblique && this.isImplicitProjectionMatrix == other.isImplicitProjectionMatrix;
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x00036B5C File Offset: 0x00034D5C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is CameraProperties && this.Equals((CameraProperties)obj);
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x00036B94 File Offset: 0x00034D94
		public unsafe override int GetHashCode()
		{
			int hashCode = this.screenRect.GetHashCode();
			hashCode = (hashCode * 397) ^ this.viewDir.GetHashCode();
			hashCode = (hashCode * 397) ^ this.projectionNear.GetHashCode();
			hashCode = (hashCode * 397) ^ this.projectionFar.GetHashCode();
			hashCode = (hashCode * 397) ^ this.cameraNear.GetHashCode();
			hashCode = (hashCode * 397) ^ this.cameraFar.GetHashCode();
			hashCode = (hashCode * 397) ^ this.cameraAspect.GetHashCode();
			hashCode = (hashCode * 397) ^ this.cameraToWorld.GetHashCode();
			hashCode = (hashCode * 397) ^ this.actualWorldToClip.GetHashCode();
			hashCode = (hashCode * 397) ^ this.cameraClipToWorld.GetHashCode();
			hashCode = (hashCode * 397) ^ this.cameraWorldToClip.GetHashCode();
			hashCode = (hashCode * 397) ^ this.implicitProjection.GetHashCode();
			hashCode = (hashCode * 397) ^ this.stereoWorldToClipLeft.GetHashCode();
			hashCode = (hashCode * 397) ^ this.stereoWorldToClipRight.GetHashCode();
			hashCode = (hashCode * 397) ^ this.worldToCamera.GetHashCode();
			hashCode = (hashCode * 397) ^ this.up.GetHashCode();
			hashCode = (hashCode * 397) ^ this.right.GetHashCode();
			hashCode = (hashCode * 397) ^ this.transformDirection.GetHashCode();
			hashCode = (hashCode * 397) ^ this.cameraEuler.GetHashCode();
			hashCode = (hashCode * 397) ^ this.velocity.GetHashCode();
			hashCode = (hashCode * 397) ^ this.farPlaneWorldSpaceLength.GetHashCode();
			hashCode = (hashCode * 397) ^ (int)this.rendererCount;
			for (int i = 0; i < 6; i++)
			{
				hashCode = (hashCode * 397) ^ this.GetShadowCullingPlane(i).GetHashCode();
			}
			for (int j = 0; j < 6; j++)
			{
				hashCode = (hashCode * 397) ^ this.GetCameraCullingPlane(j).GetHashCode();
			}
			hashCode = (hashCode * 397) ^ this.baseFarDistance.GetHashCode();
			hashCode = (hashCode * 397) ^ this.shadowCullCenter.GetHashCode();
			fixed (float* ptr = &this.layerCullDistances.FixedElementField)
			{
				float* distancesPtr = ptr;
				for (int k = 0; k < 32; k++)
				{
					hashCode = (hashCode * 397) ^ distancesPtr[k].GetHashCode();
				}
			}
			hashCode = (hashCode * 397) ^ this.layerCullSpherical;
			hashCode = (hashCode * 397) ^ this.coreCameraValues.GetHashCode();
			hashCode = (hashCode * 397) ^ (int)this.cameraType;
			hashCode = (hashCode * 397) ^ this.projectionIsOblique;
			return (hashCode * 397) ^ this.isImplicitProjectionMatrix;
		}

		// Token: 0x04000B90 RID: 2960
		private const int k_NumLayers = 32;

		// Token: 0x04000B91 RID: 2961
		private Rect screenRect;

		// Token: 0x04000B92 RID: 2962
		private Vector3 viewDir;

		// Token: 0x04000B93 RID: 2963
		private float projectionNear;

		// Token: 0x04000B94 RID: 2964
		private float projectionFar;

		// Token: 0x04000B95 RID: 2965
		private float cameraNear;

		// Token: 0x04000B96 RID: 2966
		private float cameraFar;

		// Token: 0x04000B97 RID: 2967
		private float cameraAspect;

		// Token: 0x04000B98 RID: 2968
		private Matrix4x4 cameraToWorld;

		// Token: 0x04000B99 RID: 2969
		private Matrix4x4 actualWorldToClip;

		// Token: 0x04000B9A RID: 2970
		private Matrix4x4 cameraClipToWorld;

		// Token: 0x04000B9B RID: 2971
		private Matrix4x4 cameraWorldToClip;

		// Token: 0x04000B9C RID: 2972
		private Matrix4x4 implicitProjection;

		// Token: 0x04000B9D RID: 2973
		private Matrix4x4 stereoWorldToClipLeft;

		// Token: 0x04000B9E RID: 2974
		private Matrix4x4 stereoWorldToClipRight;

		// Token: 0x04000B9F RID: 2975
		private Matrix4x4 worldToCamera;

		// Token: 0x04000BA0 RID: 2976
		private Vector3 up;

		// Token: 0x04000BA1 RID: 2977
		private Vector3 right;

		// Token: 0x04000BA2 RID: 2978
		private Vector3 transformDirection;

		// Token: 0x04000BA3 RID: 2979
		private Vector3 cameraEuler;

		// Token: 0x04000BA4 RID: 2980
		private Vector3 velocity;

		// Token: 0x04000BA5 RID: 2981
		private float farPlaneWorldSpaceLength;

		// Token: 0x04000BA6 RID: 2982
		private uint rendererCount;

		// Token: 0x04000BA7 RID: 2983
		private const int k_PlaneCount = 6;

		// Token: 0x04000BA8 RID: 2984
		[FixedBuffer(typeof(byte), 96)]
		internal CameraProperties.<m_ShadowCullPlanes>e__FixedBuffer m_ShadowCullPlanes;

		// Token: 0x04000BA9 RID: 2985
		[FixedBuffer(typeof(byte), 96)]
		internal CameraProperties.<m_CameraCullPlanes>e__FixedBuffer m_CameraCullPlanes;

		// Token: 0x04000BAA RID: 2986
		private float baseFarDistance;

		// Token: 0x04000BAB RID: 2987
		private Vector3 shadowCullCenter;

		// Token: 0x04000BAC RID: 2988
		[FixedBuffer(typeof(float), 32)]
		internal CameraProperties.<layerCullDistances>e__FixedBuffer layerCullDistances;

		// Token: 0x04000BAD RID: 2989
		private int layerCullSpherical;

		// Token: 0x04000BAE RID: 2990
		private CoreCameraValues coreCameraValues;

		// Token: 0x04000BAF RID: 2991
		private uint cameraType;

		// Token: 0x04000BB0 RID: 2992
		private int projectionIsOblique;

		// Token: 0x04000BB1 RID: 2993
		private int isImplicitProjectionMatrix;

		// Token: 0x04000BB2 RID: 2994
		internal bool useInteractiveLightBakingData;

		// Token: 0x020003A1 RID: 929
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 128)]
		public struct <layerCullDistances>e__FixedBuffer
		{
			// Token: 0x04000BB3 RID: 2995
			public float FixedElementField;
		}

		// Token: 0x020003A2 RID: 930
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 96)]
		public struct <m_CameraCullPlanes>e__FixedBuffer
		{
			// Token: 0x04000BB4 RID: 2996
			public byte FixedElementField;
		}

		// Token: 0x020003A3 RID: 931
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 96)]
		public struct <m_ShadowCullPlanes>e__FixedBuffer
		{
			// Token: 0x04000BB5 RID: 2997
			public byte FixedElementField;
		}
	}
}
