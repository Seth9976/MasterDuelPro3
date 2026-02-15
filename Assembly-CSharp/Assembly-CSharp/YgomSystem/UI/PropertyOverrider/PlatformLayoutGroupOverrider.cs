using System;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.Utility;

namespace YgomSystem.UI.PropertyOverrider
{
	// Token: 0x02000675 RID: 1653
	public class PlatformLayoutGroupOverrider : PropertyOverriderBase<HorizontalOrVerticalLayoutGroup>
	{
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06003346 RID: 13126 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideRectOffsetProperty padding
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06003347 RID: 13127 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideFloatProperty spacing
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06003348 RID: 13128 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideTextAnchorProperty childAlignment
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(HorizontalOrVerticalLayoutGroup target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Export(HorizontalOrVerticalLayoutGroup target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x04002F83 RID: 12163
		[SerializeField]
		private OverrideRectOffsetProperty m_Padding;

		// Token: 0x04002F84 RID: 12164
		[SerializeField]
		private OverrideTextAnchorProperty m_ChildAlignment;

		// Token: 0x04002F85 RID: 12165
		[SerializeField]
		private OverrideFloatProperty m_Spacing;

		// Token: 0x04002F86 RID: 12166
		[SerializeField]
		private OverrideBoolProperty m_ChildForceExpandWidth;

		// Token: 0x04002F87 RID: 12167
		[SerializeField]
		private OverrideBoolProperty m_ChildForceExpandHeight;

		// Token: 0x04002F88 RID: 12168
		[SerializeField]
		private OverrideBoolProperty m_ChildControlWidth;

		// Token: 0x04002F89 RID: 12169
		[SerializeField]
		private OverrideBoolProperty m_ChildControlHeight;

		// Token: 0x04002F8A RID: 12170
		[SerializeField]
		private OverrideBoolProperty m_ChildScaleWidth;

		// Token: 0x04002F8B RID: 12171
		[SerializeField]
		private OverrideBoolProperty m_ChildScaleHeight;

		// Token: 0x04002F8C RID: 12172
		[SerializeField]
		private OverrideBoolProperty m_ReverseArrangement;
	}
}
