using System;
using UnityEngine;

namespace MDPro3.UI.PropertyOverride
{
	// Token: 0x0200144F RID: 5199
	public class PropertyOverrider_RectTransform : PropertyOverrider
	{
		// Token: 0x060096B9 RID: 38585 RVA: 0x0015DACF File Offset: 0x0015BCCF
		protected override void DefaultOverride()
		{
			base.DefaultOverride();
			RectTransform component = base.GetComponent<RectTransform>();
			component.anchoredPosition = this.m_AnchoredPosition.m_DefaultValue;
			component.sizeDelta = this.m_SizeDelta.m_DefaultValue;
		}

		// Token: 0x060096BA RID: 38586 RVA: 0x0015DAFE File Offset: 0x0015BCFE
		protected override void MobileOverride()
		{
			base.MobileOverride();
			RectTransform component = base.GetComponent<RectTransform>();
			component.anchoredPosition = this.m_AnchoredPosition.m_MobileValue;
			component.sizeDelta = this.m_SizeDelta.m_MobileValue;
		}

		// Token: 0x0400D4FC RID: 54524
		[SerializeField]
		private OverrideProperty_Vector2 m_AnchoredPosition;

		// Token: 0x0400D4FD RID: 54525
		[SerializeField]
		private OverrideProperty_Vector2 m_SizeDelta;
	}
}
