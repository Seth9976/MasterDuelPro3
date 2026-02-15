using System;
using UnityEngine;

namespace MDPro3.UI.PropertyOverride
{
	// Token: 0x02001453 RID: 5203
	public class PropertyOverrider_UIScrollToSelection : PropertyOverrider
	{
		// Token: 0x060096C5 RID: 38597 RVA: 0x0015DCF1 File Offset: 0x0015BEF1
		protected override void DefaultOverride()
		{
			base.DefaultOverride();
			UIScrollToSelection component = base.GetComponent<UIScrollToSelection>();
			component.topPadding = this.m_TopPadding.m_DefaultValue;
			component.bottomPadding = this.m_BottomPadding.m_DefaultValue;
		}

		// Token: 0x060096C6 RID: 38598 RVA: 0x0015DD20 File Offset: 0x0015BF20
		protected override void MobileOverride()
		{
			base.MobileOverride();
			UIScrollToSelection component = base.GetComponent<UIScrollToSelection>();
			component.topPadding = this.m_TopPadding.m_MobileValue;
			component.bottomPadding = this.m_BottomPadding.m_MobileValue;
		}

		// Token: 0x0400D507 RID: 54535
		[SerializeField]
		private OverrideProperty_Float m_TopPadding;

		// Token: 0x0400D508 RID: 54536
		[SerializeField]
		private OverrideProperty_Float m_BottomPadding;
	}
}
