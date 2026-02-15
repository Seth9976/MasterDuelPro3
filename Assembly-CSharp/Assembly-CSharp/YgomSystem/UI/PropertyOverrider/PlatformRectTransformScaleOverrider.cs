using System;
using UnityEngine;
using YgomSystem.Utility;

namespace YgomSystem.UI.PropertyOverrider
{
	// Token: 0x02000679 RID: 1657
	public class PlatformRectTransformScaleOverrider : PropertyOverriderBase<RectTransform>
	{
		// Token: 0x0600335F RID: 13151 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(RectTransform target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Export(RectTransform target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x04002F95 RID: 12181
		[SerializeField]
		private OverrideVector3Property m_Scale;
	}
}
