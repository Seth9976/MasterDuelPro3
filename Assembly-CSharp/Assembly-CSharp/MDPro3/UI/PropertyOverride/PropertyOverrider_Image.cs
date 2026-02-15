using System;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI.PropertyOverride
{
	// Token: 0x0200144B RID: 5195
	public class PropertyOverrider_Image : PropertyOverrider
	{
		// Token: 0x060096AC RID: 38572 RVA: 0x0015D8CA File Offset: 0x0015BACA
		protected override void DefaultOverride()
		{
			base.DefaultOverride();
			base.GetComponent<Image>().sprite = this.m_Sprite.m_DefaultValue;
		}

		// Token: 0x060096AD RID: 38573 RVA: 0x0015D8E8 File Offset: 0x0015BAE8
		protected override void MobileOverride()
		{
			base.MobileOverride();
			base.GetComponent<Image>().sprite = this.m_Sprite.m_MobileValue;
		}

		// Token: 0x0400D4F2 RID: 54514
		[SerializeField]
		private OverrideProperty_Sprite m_Sprite;
	}
}
