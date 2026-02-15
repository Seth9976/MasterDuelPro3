using System;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.Utility;

namespace YgomSystem.UI.PropertyOverrider
{
	// Token: 0x02000672 RID: 1650
	public class PlatformImageOverrider : PropertyOverriderBase<Image>
	{
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x0600333B RID: 13115 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideSpriteProperty sprite
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x0600333C RID: 13116 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideMaterialProperty material
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(Image target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Export(Image target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x04002F78 RID: 12152
		[SerializeField]
		private OverrideSpriteProperty m_SourceImage;

		// Token: 0x04002F79 RID: 12153
		[SerializeField]
		private OverrideMaterialProperty m_Material;
	}
}
