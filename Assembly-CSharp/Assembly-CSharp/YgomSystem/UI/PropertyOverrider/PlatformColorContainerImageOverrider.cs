using System;
using UnityEngine;
using YgomSystem.Utility;

namespace YgomSystem.UI.PropertyOverrider
{
	// Token: 0x02000670 RID: 1648
	public class PlatformColorContainerImageOverrider : PropertyOverriderBase<ColorContainerImage>
	{
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x0600332D RID: 13101 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideSpriteProperty spriteUnselected
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x0600332E RID: 13102 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideSpriteProperty spriteSelected
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x0600332F RID: 13103 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideSpriteProperty spriteButtonDown
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06003330 RID: 13104 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideSpriteProperty spriteButtonEnter
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06003331 RID: 13105 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideSpriteProperty spriteInactive
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x0000216A File Offset: 0x0000036A
		protected override ColorContainerImage GetTargetComponent()
		{
			return null;
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(ColorContainerImage target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Export(ColorContainerImage target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x04002F6A RID: 12138
		[SerializeField]
		public int colorContainerIndex;

		// Token: 0x04002F6B RID: 12139
		[SerializeField]
		private OverrideSpriteProperty m_SpriteUnselected;

		// Token: 0x04002F6C RID: 12140
		[SerializeField]
		private OverrideSpriteProperty m_SpriteSelected;

		// Token: 0x04002F6D RID: 12141
		[SerializeField]
		private OverrideSpriteProperty m_SpriteButtonDown;

		// Token: 0x04002F6E RID: 12142
		[SerializeField]
		private OverrideSpriteProperty m_SpriteButtonEnter;

		// Token: 0x04002F6F RID: 12143
		[SerializeField]
		private OverrideSpriteProperty m_SpriteInactive;
	}
}
