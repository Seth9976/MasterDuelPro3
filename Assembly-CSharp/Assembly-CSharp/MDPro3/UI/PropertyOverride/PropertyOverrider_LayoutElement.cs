using System;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI.PropertyOverride
{
	// Token: 0x0200144C RID: 5196
	public class PropertyOverrider_LayoutElement : PropertyOverrider
	{
		// Token: 0x060096AF RID: 38575 RVA: 0x0015D908 File Offset: 0x0015BB08
		protected override void DefaultOverride()
		{
			base.DefaultOverride();
			LayoutElement component = base.GetComponent<LayoutElement>();
			component.ignoreLayout = this.m_IgnoreLayout.m_DefaultValue;
			component.minWidth = this.m_MinWidth.m_DefaultValue;
			component.minHeight = this.m_MinHeight.m_DefaultValue;
			component.preferredWidth = this.m_PreferredWidth.m_DefaultValue;
			component.preferredHeight = this.m_PreferredHeight.m_DefaultValue;
			component.flexibleWidth = this.m_FlexibleWidth.m_DefaultValue;
			component.flexibleHeight = this.m_FlexibleHeight.m_DefaultValue;
		}

		// Token: 0x060096B0 RID: 38576 RVA: 0x0015D998 File Offset: 0x0015BB98
		protected override void MobileOverride()
		{
			base.MobileOverride();
			LayoutElement component = base.GetComponent<LayoutElement>();
			component.ignoreLayout = this.m_IgnoreLayout.m_MobileValue;
			component.minWidth = this.m_MinWidth.m_MobileValue;
			component.minHeight = this.m_MinHeight.m_MobileValue;
			component.preferredWidth = this.m_PreferredWidth.m_MobileValue;
			component.preferredHeight = this.m_PreferredHeight.m_MobileValue;
			component.flexibleWidth = this.m_FlexibleWidth.m_MobileValue;
			component.flexibleHeight = this.m_FlexibleHeight.m_MobileValue;
		}

		// Token: 0x0400D4F3 RID: 54515
		[SerializeField]
		private OverrideProperty_Bool m_IgnoreLayout;

		// Token: 0x0400D4F4 RID: 54516
		[SerializeField]
		private OverrideProperty_Float m_MinWidth;

		// Token: 0x0400D4F5 RID: 54517
		[SerializeField]
		private OverrideProperty_Float m_MinHeight;

		// Token: 0x0400D4F6 RID: 54518
		[SerializeField]
		private OverrideProperty_Float m_PreferredWidth;

		// Token: 0x0400D4F7 RID: 54519
		[SerializeField]
		private OverrideProperty_Float m_PreferredHeight;

		// Token: 0x0400D4F8 RID: 54520
		[SerializeField]
		private OverrideProperty_Float m_FlexibleWidth;

		// Token: 0x0400D4F9 RID: 54521
		[SerializeField]
		private OverrideProperty_Float m_FlexibleHeight;
	}
}
