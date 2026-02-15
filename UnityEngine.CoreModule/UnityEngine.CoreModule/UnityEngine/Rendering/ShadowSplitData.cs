using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003C9 RID: 969
	[UsedByNativeCode]
	public struct ShadowSplitData : IEquatable<ShadowSplitData>
	{
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x00039DF0 File Offset: 0x00037FF0
		public int cullingPlaneCount
		{
			get
			{
				return this.m_CullingPlaneCount;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x00039E08 File Offset: 0x00038008
		public Vector4 cullingSphere
		{
			get
			{
				return this.m_CullingSphere;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (set) Token: 0x06001A85 RID: 6789 RVA: 0x00039E20 File Offset: 0x00038020
		public float shadowCascadeBlendCullingFactor
		{
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentException(string.Format("Value should range from {0} to {1}, but was {2}.", 0, 1, value));
				}
				this.m_ShadowCascadeBlendCullingFactor = value;
			}
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x00039E70 File Offset: 0x00038070
		public unsafe Plane GetCullingPlane(int index)
		{
			bool flag = index < 0 || index >= this.cullingPlaneCount;
			if (flag)
			{
				throw new ArgumentException("index", string.Format("Index should be at least {0} and less than cullingPlaneCount ({1}), but was {2}.", 0, this.cullingPlaneCount, index));
			}
			fixed (byte* ptr2 = &this.m_CullingPlanes.FixedElementField)
			{
				byte* ptr = ptr2;
				Plane* planes = (Plane*)ptr;
				return planes[index];
			}
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x00039EEC File Offset: 0x000380EC
		public bool Equals(ShadowSplitData other)
		{
			bool flag = this.m_CullingPlaneCount != other.m_CullingPlaneCount;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < this.cullingPlaneCount; i++)
				{
					bool flag3 = !this.GetCullingPlane(i).Equals(other.GetCullingPlane(i));
					if (flag3)
					{
						return false;
					}
				}
				flag2 = this.m_CullingSphere.Equals(other.m_CullingSphere);
			}
			return flag2;
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00039F74 File Offset: 0x00038174
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ShadowSplitData && this.Equals((ShadowSplitData)obj);
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x00039FAC File Offset: 0x000381AC
		public override int GetHashCode()
		{
			return (this.m_CullingPlaneCount * 397) ^ this.m_CullingSphere.GetHashCode();
		}

		// Token: 0x04000C7C RID: 3196
		private const int k_MaximumCullingPlaneCount = 10;

		// Token: 0x04000C7D RID: 3197
		public static readonly int maximumCullingPlaneCount = 10;

		// Token: 0x04000C7E RID: 3198
		private int m_CullingPlaneCount;

		// Token: 0x04000C7F RID: 3199
		[FixedBuffer(typeof(byte), 160)]
		internal ShadowSplitData.<m_CullingPlanes>e__FixedBuffer m_CullingPlanes;

		// Token: 0x04000C80 RID: 3200
		private Vector4 m_CullingSphere;

		// Token: 0x04000C81 RID: 3201
		private float m_ShadowCascadeBlendCullingFactor;

		// Token: 0x04000C82 RID: 3202
		private float m_CullingNearPlane;

		// Token: 0x04000C83 RID: 3203
		private Matrix4x4 m_CullingMatrix;

		// Token: 0x020003CA RID: 970
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 160)]
		public struct <m_CullingPlanes>e__FixedBuffer
		{
			// Token: 0x04000C84 RID: 3204
			public byte FixedElementField;
		}
	}
}
