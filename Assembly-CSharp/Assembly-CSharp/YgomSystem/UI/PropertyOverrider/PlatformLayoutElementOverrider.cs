using System;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.Utility;

namespace YgomSystem.UI.PropertyOverrider
{
	// Token: 0x02000674 RID: 1652
	public class PlatformLayoutElementOverrider : PropertyOverriderBase<LayoutElement>
	{
		// Token: 0x06003343 RID: 13123 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(LayoutElement target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Export(LayoutElement target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x04002F7B RID: 12155
		[SerializeField]
		private OverrideBoolProperty m_IgnoreLayout;

		// Token: 0x04002F7C RID: 12156
		[SerializeField]
		private OverrideFloatProperty m_MinWidth;

		// Token: 0x04002F7D RID: 12157
		[SerializeField]
		private OverrideFloatProperty m_MinHeight;

		// Token: 0x04002F7E RID: 12158
		[SerializeField]
		private OverrideFloatProperty m_PreferredWidth;

		// Token: 0x04002F7F RID: 12159
		[SerializeField]
		private OverrideFloatProperty m_PreferredHeight;

		// Token: 0x04002F80 RID: 12160
		[SerializeField]
		private OverrideFloatProperty m_FlexibleWidth;

		// Token: 0x04002F81 RID: 12161
		[SerializeField]
		private OverrideFloatProperty m_FlexibleHeight;

		// Token: 0x04002F82 RID: 12162
		[SerializeField]
		private OverrideIntProperty m_LayoutPriority;
	}
}
