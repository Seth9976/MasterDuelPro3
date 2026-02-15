using System;
using UnityEngine;
using YgomSystem.Utility;

namespace YgomSystem.UI.PropertyOverrider
{
	// Token: 0x0200067E RID: 1662
	public class PlatformTextOverrider : PropertyOverriderBase<MDText>
	{
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06003373 RID: 13171 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideIntProperty fontSize
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06003374 RID: 13172 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideBoolProperty bestFit
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06003375 RID: 13173 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideIntProperty minSize
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06003376 RID: 13174 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideIntProperty maxSize
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(MDText target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Export(MDText target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x04002F9F RID: 12191
		[SerializeField]
		private OverrideIntProperty m_FontSize;

		// Token: 0x04002FA0 RID: 12192
		[SerializeField]
		private OverrideBoolProperty m_BestFit;

		// Token: 0x04002FA1 RID: 12193
		[SerializeField]
		private OverrideIntProperty m_MinSize;

		// Token: 0x04002FA2 RID: 12194
		[SerializeField]
		private OverrideIntProperty m_MaxSize;
	}
}
