using System;
using TMPro;
using UnityEngine;

namespace MDPro3.UI.PropertyOverride
{
	// Token: 0x02001451 RID: 5201
	public class PropertyOverrider_TextMeshProUGUI : PropertyOverrider
	{
		// Token: 0x060096BF RID: 38591 RVA: 0x0015DB8C File Offset: 0x0015BD8C
		protected override void DefaultOverride()
		{
			base.DefaultOverride();
			TextMeshProUGUI component = base.GetComponent<TextMeshProUGUI>();
			component.fontSize = this.m_FontSize.m_DefaultValue;
			component.enableAutoSizing = this.m_EnableAutoSize.m_DefaultValue;
			component.fontSizeMin = this.m_FontSizeMin.m_DefaultValue;
			component.fontSizeMax = this.m_FontSizeMax.m_DefaultValue;
		}

		// Token: 0x060096C0 RID: 38592 RVA: 0x0015DBE8 File Offset: 0x0015BDE8
		protected override void MobileOverride()
		{
			base.MobileOverride();
			TextMeshProUGUI component = base.GetComponent<TextMeshProUGUI>();
			component.fontSize = this.m_FontSize.m_MobileValue;
			component.enableAutoSizing = this.m_EnableAutoSize.m_MobileValue;
			component.fontSizeMin = this.m_FontSizeMin.m_MobileValue;
			component.fontSizeMax = this.m_FontSizeMax.m_MobileValue;
		}

		// Token: 0x0400D500 RID: 54528
		[SerializeField]
		private OverrideProperty_Float m_FontSize;

		// Token: 0x0400D501 RID: 54529
		[SerializeField]
		private OverrideProperty_Bool m_EnableAutoSize;

		// Token: 0x0400D502 RID: 54530
		[SerializeField]
		private OverrideProperty_Float m_FontSizeMin;

		// Token: 0x0400D503 RID: 54531
		[SerializeField]
		private OverrideProperty_Float m_FontSizeMax;
	}
}
