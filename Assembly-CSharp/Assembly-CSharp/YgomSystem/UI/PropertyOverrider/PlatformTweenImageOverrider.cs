using System;
using UnityEngine;
using YgomSystem.Utility;

namespace YgomSystem.UI.PropertyOverrider
{
	// Token: 0x02000680 RID: 1664
	public class PlatformTweenImageOverrider : PropertyOverriderBase<TweenImage>
	{
		// Token: 0x0600337D RID: 13181 RVA: 0x0000216A File Offset: 0x0000036A
		protected override TweenImage GetTargetComponent()
		{
			return null;
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(TweenImage target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Export(TweenImage target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x04002FA6 RID: 12198
		[SerializeField]
		private TweenImage m_TargetTween;

		// Token: 0x04002FA7 RID: 12199
		[SerializeField]
		private OverrideSpriteArrayProperty m_Frames;
	}
}
