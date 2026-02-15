using System;
using UnityEngine;
using YgomSystem.Utility;

namespace YgomSystem.UI.PropertyOverrider
{
	// Token: 0x0200067A RID: 1658
	public class PlatformSpriteRendererOverrider : PropertyOverriderBase<SpriteRenderer>
	{
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06003362 RID: 13154 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideSpriteProperty sprite
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(SpriteRenderer target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Export(SpriteRenderer target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x04002F96 RID: 12182
		[SerializeField]
		private OverrideSpriteProperty m_Sprite;
	}
}
