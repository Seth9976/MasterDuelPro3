using System;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI.PropertyOverride
{
	// Token: 0x02001450 RID: 5200
	public class PropertyOverrider_Slider : PropertyOverrider
	{
		// Token: 0x060096BC RID: 38588 RVA: 0x0015DB2D File Offset: 0x0015BD2D
		protected override void DefaultOverride()
		{
			base.DefaultOverride();
			Slider component = base.GetComponent<Slider>();
			component.minValue = this.m_MinValue.m_DefaultValue;
			component.maxValue = this.m_MaxValue.m_DefaultValue;
		}

		// Token: 0x060096BD RID: 38589 RVA: 0x0015DB5C File Offset: 0x0015BD5C
		protected override void MobileOverride()
		{
			base.MobileOverride();
			Slider component = base.GetComponent<Slider>();
			component.minValue = this.m_MinValue.m_MobileValue;
			component.maxValue = this.m_MaxValue.m_MobileValue;
		}

		// Token: 0x0400D4FE RID: 54526
		[SerializeField]
		private OverrideProperty_Float m_MinValue;

		// Token: 0x0400D4FF RID: 54527
		[SerializeField]
		private OverrideProperty_Float m_MaxValue;
	}
}
