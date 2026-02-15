using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003D4 RID: 980
	[UsedByNativeCode]
	public struct VisibleLight : IEquatable<VisibleLight>
	{
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001AD7 RID: 6871 RVA: 0x0003AB28 File Offset: 0x00038D28
		public Light light
		{
			get
			{
				return (Light)Object.FindObjectFromInstanceID(this.m_InstanceId);
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001AD8 RID: 6872 RVA: 0x0003AB3C File Offset: 0x00038D3C
		public LightType lightType
		{
			get
			{
				return this.m_LightType;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x0003AB54 File Offset: 0x00038D54
		public Color finalColor
		{
			get
			{
				return this.m_FinalColor;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001ADA RID: 6874 RVA: 0x0003AB6C File Offset: 0x00038D6C
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				return this.m_LocalToWorldMatrix;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x0003AB84 File Offset: 0x00038D84
		public float range
		{
			get
			{
				return this.m_Range;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001ADC RID: 6876 RVA: 0x0003AB9C File Offset: 0x00038D9C
		public float spotAngle
		{
			get
			{
				return this.m_SpotAngle;
			}
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x0003ABB4 File Offset: 0x00038DB4
		public bool Equals(VisibleLight other)
		{
			return this.m_LightType == other.m_LightType && this.m_FinalColor.Equals(other.m_FinalColor) && this.m_ScreenRect.Equals(other.m_ScreenRect) && this.m_LocalToWorldMatrix.Equals(other.m_LocalToWorldMatrix) && this.m_Range.Equals(other.m_Range) && this.m_SpotAngle.Equals(other.m_SpotAngle) && this.m_InstanceId == other.m_InstanceId && this.m_Flags == other.m_Flags;
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0003AC54 File Offset: 0x00038E54
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is VisibleLight && this.Equals((VisibleLight)obj);
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0003AC8C File Offset: 0x00038E8C
		public override int GetHashCode()
		{
			int hashCode = (int)this.m_LightType;
			hashCode = (hashCode * 397) ^ this.m_FinalColor.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_ScreenRect.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_LocalToWorldMatrix.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_Range.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_SpotAngle.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_InstanceId;
			return (hashCode * 397) ^ (int)this.m_Flags;
		}

		// Token: 0x04000CD4 RID: 3284
		private LightType m_LightType;

		// Token: 0x04000CD5 RID: 3285
		private Color m_FinalColor;

		// Token: 0x04000CD6 RID: 3286
		private Rect m_ScreenRect;

		// Token: 0x04000CD7 RID: 3287
		private Matrix4x4 m_LocalToWorldMatrix;

		// Token: 0x04000CD8 RID: 3288
		private float m_Range;

		// Token: 0x04000CD9 RID: 3289
		private float m_SpotAngle;

		// Token: 0x04000CDA RID: 3290
		private int m_InstanceId;

		// Token: 0x04000CDB RID: 3291
		private VisibleLightFlags m_Flags;
	}
}
