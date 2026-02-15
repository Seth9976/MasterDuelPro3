using System;

namespace YgomSystem.Utility
{
	// Token: 0x020004FC RID: 1276
	public class AssetLinkContainerVariant : AssetLinkContainer
	{
		// Token: 0x06002829 RID: 10281 RVA: 0x0000216A File Offset: 0x0000036A
		public override AssetLinkContainer.Container GetContainer(string label)
		{
			return null;
		}

		// Token: 0x040028EE RID: 10478
		public AssetLinkContainer baseContainer;
	}
}
