using System;
using UnityEngine;

namespace MDPro3.UI.PropertyOverride
{
	// Token: 0x02001452 RID: 5202
	public class PropertyOverrider_Transform : PropertyOverrider
	{
		// Token: 0x060096C2 RID: 38594 RVA: 0x0015DC44 File Offset: 0x0015BE44
		protected override void DefaultOverride()
		{
			base.DefaultOverride();
			base.transform.localPosition = this.m_Position.m_DefaultValue;
			base.transform.localRotation = this.m_Rotation.m_DefaultValue;
			base.transform.localScale = this.m_Scale.m_DefaultValue;
		}

		// Token: 0x060096C3 RID: 38595 RVA: 0x0015DC9C File Offset: 0x0015BE9C
		protected override void MobileOverride()
		{
			base.MobileOverride();
			base.transform.localPosition = this.m_Position.m_MobileValue;
			base.transform.localRotation = this.m_Rotation.m_MobileValue;
			base.transform.localScale = this.m_Scale.m_MobileValue;
		}

		// Token: 0x0400D504 RID: 54532
		[SerializeField]
		private OverrideProperty_Vector3 m_Position;

		// Token: 0x0400D505 RID: 54533
		[SerializeField]
		private OverrideProperty_Quaternion m_Rotation;

		// Token: 0x0400D506 RID: 54534
		[SerializeField]
		private OverrideProperty_Vector3 m_Scale;
	}
}
