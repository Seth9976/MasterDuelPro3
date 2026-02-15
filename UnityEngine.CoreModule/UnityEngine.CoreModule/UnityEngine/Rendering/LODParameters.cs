using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003B3 RID: 947
	public struct LODParameters : IEquatable<LODParameters>
	{
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x060019A1 RID: 6561 RVA: 0x00037DF8 File Offset: 0x00035FF8
		// (set) Token: 0x060019A2 RID: 6562 RVA: 0x00037E15 File Offset: 0x00036015
		public bool isOrthographic
		{
			get
			{
				return Convert.ToBoolean(this.m_IsOrthographic);
			}
			set
			{
				this.m_IsOrthographic = Convert.ToInt32(value);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x060019A3 RID: 6563 RVA: 0x00037E24 File Offset: 0x00036024
		public Vector3 cameraPosition
		{
			get
			{
				return this.m_CameraPosition;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x060019A4 RID: 6564 RVA: 0x00037E3C File Offset: 0x0003603C
		public float fieldOfView
		{
			get
			{
				return this.m_FieldOfView;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x060019A5 RID: 6565 RVA: 0x00037E54 File Offset: 0x00036054
		public float orthoSize
		{
			get
			{
				return this.m_OrthoSize;
			}
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x00037E6C File Offset: 0x0003606C
		public bool Equals(LODParameters other)
		{
			return this.m_IsOrthographic == other.m_IsOrthographic && this.m_CameraPosition.Equals(other.m_CameraPosition) && this.m_FieldOfView.Equals(other.m_FieldOfView) && this.m_OrthoSize.Equals(other.m_OrthoSize) && this.m_CameraPixelHeight == other.m_CameraPixelHeight;
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x00037ED8 File Offset: 0x000360D8
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is LODParameters && this.Equals((LODParameters)obj);
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x00037F10 File Offset: 0x00036110
		public override int GetHashCode()
		{
			int hashCode = this.m_IsOrthographic;
			hashCode = (hashCode * 397) ^ this.m_CameraPosition.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_FieldOfView.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_OrthoSize.GetHashCode();
			return (hashCode * 397) ^ this.m_CameraPixelHeight;
		}

		// Token: 0x04000C08 RID: 3080
		private int m_IsOrthographic;

		// Token: 0x04000C09 RID: 3081
		private Vector3 m_CameraPosition;

		// Token: 0x04000C0A RID: 3082
		private float m_FieldOfView;

		// Token: 0x04000C0B RID: 3083
		private float m_OrthoSize;

		// Token: 0x04000C0C RID: 3084
		private int m_CameraPixelHeight;
	}
}
