using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003D6 RID: 982
	[UsedByNativeCode]
	public struct VisibleReflectionProbe : IEquatable<VisibleReflectionProbe>
	{
		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x0003AD3B File Offset: 0x00038F3B
		public Texture texture
		{
			get
			{
				return (Texture)Object.FindObjectFromInstanceID(this.m_TextureId);
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x0003AD4D File Offset: 0x00038F4D
		public ReflectionProbe reflectionProbe
		{
			get
			{
				return (ReflectionProbe)Object.FindObjectFromInstanceID(this.m_InstanceId);
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x0003AD60 File Offset: 0x00038F60
		public Bounds bounds
		{
			get
			{
				return this.m_Bounds;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x0003AD78 File Offset: 0x00038F78
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				return this.m_LocalToWorldMatrix;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x0003AD90 File Offset: 0x00038F90
		public Vector4 hdrData
		{
			get
			{
				return this.m_HdrData;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x0003ADA8 File Offset: 0x00038FA8
		public float blendDistance
		{
			get
			{
				return this.m_BlendDistance;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x0003ADC0 File Offset: 0x00038FC0
		public int importance
		{
			get
			{
				return this.m_Importance;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x0003ADD8 File Offset: 0x00038FD8
		public bool isBoxProjection
		{
			get
			{
				return Convert.ToBoolean(this.m_BoxProjection);
			}
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x0003ADF8 File Offset: 0x00038FF8
		public bool Equals(VisibleReflectionProbe other)
		{
			return this.m_Bounds.Equals(other.m_Bounds) && this.m_LocalToWorldMatrix.Equals(other.m_LocalToWorldMatrix) && this.m_HdrData.Equals(other.m_HdrData) && this.m_Center.Equals(other.m_Center) && this.m_BlendDistance.Equals(other.m_BlendDistance) && this.m_Importance == other.m_Importance && this.m_BoxProjection == other.m_BoxProjection && this.m_InstanceId == other.m_InstanceId && this.m_TextureId == other.m_TextureId;
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x0003AEA8 File Offset: 0x000390A8
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is VisibleReflectionProbe && this.Equals((VisibleReflectionProbe)obj);
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x0003AEE0 File Offset: 0x000390E0
		public override int GetHashCode()
		{
			int hashCode = this.m_Bounds.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_LocalToWorldMatrix.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_HdrData.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_Center.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_BlendDistance.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_Importance;
			hashCode = (hashCode * 397) ^ this.m_BoxProjection;
			hashCode = (hashCode * 397) ^ this.m_InstanceId;
			return (hashCode * 397) ^ this.m_TextureId;
		}

		// Token: 0x04000CE0 RID: 3296
		private Bounds m_Bounds;

		// Token: 0x04000CE1 RID: 3297
		private Matrix4x4 m_LocalToWorldMatrix;

		// Token: 0x04000CE2 RID: 3298
		private Vector4 m_HdrData;

		// Token: 0x04000CE3 RID: 3299
		private Vector3 m_Center;

		// Token: 0x04000CE4 RID: 3300
		private float m_BlendDistance;

		// Token: 0x04000CE5 RID: 3301
		private int m_Importance;

		// Token: 0x04000CE6 RID: 3302
		private int m_BoxProjection;

		// Token: 0x04000CE7 RID: 3303
		private int m_InstanceId;

		// Token: 0x04000CE8 RID: 3304
		private int m_TextureId;
	}
}
