using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003CE RID: 974
	public struct SortingSettings : IEquatable<SortingSettings>
	{
		// Token: 0x06001A92 RID: 6802 RVA: 0x0003A0E8 File Offset: 0x000382E8
		public SortingSettings(Camera camera)
		{
			ScriptableRenderContext.InitializeSortSettings(camera, out this);
			this.m_Criteria = this.criteria;
		}

		// Token: 0x17000402 RID: 1026
		// (set) Token: 0x06001A93 RID: 6803 RVA: 0x0003A0FF File Offset: 0x000382FF
		public Vector3 customAxis
		{
			set
			{
				this.m_CustomAxis = value;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x0003A10C File Offset: 0x0003830C
		// (set) Token: 0x06001A95 RID: 6805 RVA: 0x0003A124 File Offset: 0x00038324
		public SortingCriteria criteria
		{
			get
			{
				return this.m_Criteria;
			}
			set
			{
				this.m_Criteria = value;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (set) Token: 0x06001A96 RID: 6806 RVA: 0x0003A12E File Offset: 0x0003832E
		public DistanceMetric distanceMetric
		{
			set
			{
				this.m_DistanceMetric = value;
			}
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x0003A138 File Offset: 0x00038338
		public bool Equals(SortingSettings other)
		{
			return this.m_WorldToCameraMatrix.Equals(other.m_WorldToCameraMatrix) && this.m_CameraPosition.Equals(other.m_CameraPosition) && this.m_CustomAxis.Equals(other.m_CustomAxis) && this.m_Criteria == other.m_Criteria && this.m_DistanceMetric == other.m_DistanceMetric;
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x0003A1A4 File Offset: 0x000383A4
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is SortingSettings && this.Equals((SortingSettings)obj);
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x0003A1DC File Offset: 0x000383DC
		public override int GetHashCode()
		{
			int hashCode = this.m_WorldToCameraMatrix.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_CameraPosition.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_CustomAxis.GetHashCode();
			hashCode = (hashCode * 397) ^ (int)this.m_Criteria;
			return (hashCode * 397) ^ (int)this.m_DistanceMetric;
		}

		// Token: 0x04000C96 RID: 3222
		private Matrix4x4 m_WorldToCameraMatrix;

		// Token: 0x04000C97 RID: 3223
		private Vector3 m_CameraPosition;

		// Token: 0x04000C98 RID: 3224
		private Vector3 m_CustomAxis;

		// Token: 0x04000C99 RID: 3225
		private SortingCriteria m_Criteria;

		// Token: 0x04000C9A RID: 3226
		private DistanceMetric m_DistanceMetric;
	}
}
