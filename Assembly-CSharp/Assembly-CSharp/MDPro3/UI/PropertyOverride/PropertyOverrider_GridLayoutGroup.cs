using System;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI.PropertyOverride
{
	// Token: 0x02001449 RID: 5193
	public class PropertyOverrider_GridLayoutGroup : PropertyOverrider
	{
		// Token: 0x060096A6 RID: 38566 RVA: 0x0015D794 File Offset: 0x0015B994
		protected override void DefaultOverride()
		{
			base.DefaultOverride();
			GridLayoutGroup target = base.GetComponent<GridLayoutGroup>();
			target.padding = this.m_Padding.m_DefaultValue;
			target.cellSize = this.m_CellSize.m_DefaultValue;
			target.spacing = this.m_Spacing.m_DefaultValue;
			if (this.m_ConstraintCount.m_DefaultValue > 0)
			{
				target.constraintCount = this.m_ConstraintCount.m_DefaultValue;
			}
		}

		// Token: 0x060096A7 RID: 38567 RVA: 0x0015D800 File Offset: 0x0015BA00
		protected override void MobileOverride()
		{
			base.MobileOverride();
			GridLayoutGroup target = base.GetComponent<GridLayoutGroup>();
			target.padding = this.m_Padding.m_MobileValue;
			target.cellSize = this.m_CellSize.m_MobileValue;
			target.spacing = this.m_Spacing.m_MobileValue;
			if (this.m_ConstraintCount.m_MobileValue > 0)
			{
				target.constraintCount = this.m_ConstraintCount.m_MobileValue;
			}
		}

		// Token: 0x0400D4EC RID: 54508
		[SerializeField]
		private OverrideProperty_RectOffset m_Padding;

		// Token: 0x0400D4ED RID: 54509
		[SerializeField]
		private OverrideProperty_Vector2 m_CellSize;

		// Token: 0x0400D4EE RID: 54510
		[SerializeField]
		private OverrideProperty_Vector2 m_Spacing;

		// Token: 0x0400D4EF RID: 54511
		[SerializeField]
		private OverrideProperty_Int m_ConstraintCount;
	}
}
