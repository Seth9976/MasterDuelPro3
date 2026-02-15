using System;

namespace AssetStudio
{
	// Token: 0x020000D1 RID: 209
	public class AssetInfo
	{
		// Token: 0x0600030F RID: 783 RVA: 0x0000E8C1 File Offset: 0x0000CAC1
		public AssetInfo(ObjectReader reader)
		{
			this.preloadIndex = reader.ReadInt32();
			this.preloadSize = reader.ReadInt32();
			this.asset = new PPtr<Object>(reader);
		}

		// Token: 0x04000645 RID: 1605
		public int preloadIndex;

		// Token: 0x04000646 RID: 1606
		public int preloadSize;

		// Token: 0x04000647 RID: 1607
		public PPtr<Object> asset;
	}
}
