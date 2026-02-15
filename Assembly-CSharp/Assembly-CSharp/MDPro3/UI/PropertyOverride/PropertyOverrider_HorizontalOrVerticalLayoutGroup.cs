using System;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI.PropertyOverride
{
	// Token: 0x0200144A RID: 5194
	public class PropertyOverrider_HorizontalOrVerticalLayoutGroup : PropertyOverrider
	{
		// Token: 0x060096A9 RID: 38569 RVA: 0x0015D86C File Offset: 0x0015BA6C
		protected override void DefaultOverride()
		{
			base.DefaultOverride();
			HorizontalOrVerticalLayoutGroup component = base.GetComponent<HorizontalOrVerticalLayoutGroup>();
			component.padding = this.m_Padding.m_DefaultValue;
			component.spacing = this.m_Spacing.m_DefaultValue;
		}

		// Token: 0x060096AA RID: 38570 RVA: 0x0015D89B File Offset: 0x0015BA9B
		protected override void MobileOverride()
		{
			base.MobileOverride();
			HorizontalOrVerticalLayoutGroup component = base.GetComponent<HorizontalOrVerticalLayoutGroup>();
			component.padding = this.m_Padding.m_MobileValue;
			component.spacing = this.m_Spacing.m_MobileValue;
		}

		// Token: 0x0400D4F0 RID: 54512
		[SerializeField]
		private OverrideProperty_RectOffset m_Padding;

		// Token: 0x0400D4F1 RID: 54513
		[SerializeField]
		private OverrideProperty_Float m_Spacing;
	}
}
