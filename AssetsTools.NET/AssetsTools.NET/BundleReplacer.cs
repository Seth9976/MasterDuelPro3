using System;

namespace AssetsTools.NET
{
	// Token: 0x0200005D RID: 93
	public abstract class BundleReplacer
	{
		// Token: 0x0600032F RID: 815
		public abstract BundleReplacementType GetReplacementType();

		// Token: 0x06000330 RID: 816
		public abstract int GetBundleListIndex();

		// Token: 0x06000331 RID: 817
		public abstract string GetOriginalEntryName();

		// Token: 0x06000332 RID: 818
		public abstract string GetEntryName();

		// Token: 0x06000333 RID: 819
		public abstract bool Init(AssetsFileReader entryReader, long entryPos, long entrySize, ClassDatabaseFile typeMeta = null);

		// Token: 0x06000334 RID: 820
		public abstract void Uninit();

		// Token: 0x06000335 RID: 821
		public abstract long Write(AssetsFileWriter writer);

		// Token: 0x06000336 RID: 822
		public abstract long WriteReplacer(AssetsFileWriter writer);

		// Token: 0x06000337 RID: 823
		public abstract bool HasSerializedData();

		// Token: 0x06000338 RID: 824 RVA: 0x00014734 File Offset: 0x00012934
		public static BundleReplacer ReadBundleReplacer(AssetsFileReader reader)
		{
			return null;
		}
	}
}
