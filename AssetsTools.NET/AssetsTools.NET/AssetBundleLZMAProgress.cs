using System;
using SevenZip;

namespace AssetsTools.NET
{
	// Token: 0x02000038 RID: 56
	internal class AssetBundleLZMAProgress : ICodeProgress
	{
		// Token: 0x0600015F RID: 351 RVA: 0x0000E9FB File Offset: 0x0000CBFB
		public AssetBundleLZMAProgress(IAssetBundleCompressProgress progress, long length)
		{
			this.progress = progress;
			this.currentSize = 0L;
			this.length = length;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000EA1C File Offset: 0x0000CC1C
		public void SetProgress(long inSize, long outSize)
		{
			bool flag = this.progress != null;
			if (flag)
			{
				this.progress.SetProgress((float)inSize / (float)this.length);
			}
		}

		// Token: 0x0400015E RID: 350
		private IAssetBundleCompressProgress progress;

		// Token: 0x0400015F RID: 351
		private long currentSize;

		// Token: 0x04000160 RID: 352
		private long length;
	}
}
