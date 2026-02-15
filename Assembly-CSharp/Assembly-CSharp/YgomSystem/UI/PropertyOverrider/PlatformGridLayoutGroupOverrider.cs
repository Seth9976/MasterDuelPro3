using System;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.Utility;

namespace YgomSystem.UI.PropertyOverrider
{
	// Token: 0x02000671 RID: 1649
	public class PlatformGridLayoutGroupOverrider : PropertyOverriderBase<GridLayoutGroup>
	{
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06003336 RID: 13110 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideRectOffsetProperty padding
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06003337 RID: 13111 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideIntProperty constraintCount
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(GridLayoutGroup target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Export(GridLayoutGroup target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x04002F70 RID: 12144
		[SerializeField]
		private OverrideRectOffsetProperty m_Padding;

		// Token: 0x04002F71 RID: 12145
		[SerializeField]
		private OverrideTextAnchorProperty m_ChildAlignment;

		// Token: 0x04002F72 RID: 12146
		[SerializeField]
		private OverrideGridLayoutGroupCornerProperty m_StartCorner;

		// Token: 0x04002F73 RID: 12147
		[SerializeField]
		private OverrideGridLayoutGroupAxisProperty m_StartAxis;

		// Token: 0x04002F74 RID: 12148
		[SerializeField]
		private OverrideVector2Property m_CellSize;

		// Token: 0x04002F75 RID: 12149
		[SerializeField]
		private OverrideVector2Property m_Spacing;

		// Token: 0x04002F76 RID: 12150
		[SerializeField]
		private OverrideGridLayoutGroupConstraintProperty m_Constraint;

		// Token: 0x04002F77 RID: 12151
		[SerializeField]
		private OverrideIntProperty m_ConstraintCount;
	}
}
