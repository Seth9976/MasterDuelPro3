using System;
using UnityEngine;
using YgomSystem.UI.InfinityScroll;
using YgomSystem.Utility;

namespace YgomSystem.UI.PropertyOverrider
{
	// Token: 0x02000673 RID: 1651
	public class PlatformInfinityScrollViewOverrider : PropertyOverriderBase<InfinityScrollView>
	{
		// Token: 0x06003340 RID: 13120 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(InfinityScrollView target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Export(InfinityScrollView target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x04002F7A RID: 12154
		[SerializeField]
		private OverrideStringProperty m_ELabelTemplate;
	}
}
