using System;
using UnityEngine;

namespace MDPro3.UI.PropertyOverride
{
	// Token: 0x02001448 RID: 5192
	public class PropertyOverrider_GameObject : PropertyOverrider
	{
		// Token: 0x060096A3 RID: 38563 RVA: 0x0015D750 File Offset: 0x0015B950
		protected override void DefaultOverride()
		{
			base.MobileOverride();
			base.gameObject.SetActive(this.active.m_DefaultValue);
		}

		// Token: 0x060096A4 RID: 38564 RVA: 0x0015D76E File Offset: 0x0015B96E
		protected override void MobileOverride()
		{
			base.MobileOverride();
			base.gameObject.SetActive(this.active.m_MobileValue);
		}

		// Token: 0x0400D4EB RID: 54507
		[SerializeField]
		private OverrideProperty_Bool active;
	}
}
