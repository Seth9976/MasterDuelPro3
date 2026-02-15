using System;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI.PropertyOverride
{
	// Token: 0x0200144E RID: 5198
	public class PropertyOverrider_RectMask2D : PropertyOverrider
	{
		// Token: 0x060096B6 RID: 38582 RVA: 0x0015DA93 File Offset: 0x0015BC93
		protected override void DefaultOverride()
		{
			base.DefaultOverride();
			base.GetComponent<RectMask2D>().padding = this.m_Padding.m_DefaultValue;
		}

		// Token: 0x060096B7 RID: 38583 RVA: 0x0015DAB1 File Offset: 0x0015BCB1
		protected override void MobileOverride()
		{
			base.MobileOverride();
			base.GetComponent<RectMask2D>().padding = this.m_Padding.m_MobileValue;
		}

		// Token: 0x0400D4FB RID: 54523
		[SerializeField]
		private OverrideProperty_Vector4 m_Padding;
	}
}
