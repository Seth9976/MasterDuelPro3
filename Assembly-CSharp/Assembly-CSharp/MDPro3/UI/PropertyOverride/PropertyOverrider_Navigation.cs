using System;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI.PropertyOverride
{
	// Token: 0x0200144D RID: 5197
	public class PropertyOverrider_Navigation : PropertyOverrider
	{
		// Token: 0x060096B2 RID: 38578 RVA: 0x0015DA27 File Offset: 0x0015BC27
		protected override void Awake()
		{
			if (this.m_Navigation.m_DefaultValue.mode == Navigation.Mode.None)
			{
				this.m_Navigation.m_DefaultValue = base.GetComponent<Selectable>().navigation;
			}
			base.Awake();
		}

		// Token: 0x060096B3 RID: 38579 RVA: 0x0015DA57 File Offset: 0x0015BC57
		protected override void DefaultOverride()
		{
			base.DefaultOverride();
			base.GetComponent<Selectable>().navigation = this.m_Navigation.m_DefaultValue;
		}

		// Token: 0x060096B4 RID: 38580 RVA: 0x0015DA75 File Offset: 0x0015BC75
		protected override void MobileOverride()
		{
			base.MobileOverride();
			base.GetComponent<Selectable>().navigation = this.m_Navigation.m_MobileValue;
		}

		// Token: 0x0400D4FA RID: 54522
		[SerializeField]
		private OverrideProperty_Navigation m_Navigation;
	}
}
