using System;
using System.Collections.Generic;

namespace AssetsTools.NET
{
	// Token: 0x0200005E RID: 94
	public class BundleReplacerFromAssets : BundleReplacer
	{
		// Token: 0x0600033A RID: 826 RVA: 0x00014747 File Offset: 0x00012947
		public BundleReplacerFromAssets(string oldName, string newName, AssetsFile assetsFile, List<AssetsReplacer> assetReplacers, int bundleListIndex = -1, ClassDatabaseFile typeMeta = null)
		{
			this.oldName = oldName;
			this.newName = newName ?? oldName;
			this.assetsFile = assetsFile;
			this.assetReplacers = assetReplacers;
			this.bundleListIndex = bundleListIndex;
			this.typeMeta = typeMeta;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00014784 File Offset: 0x00012984
		public override BundleReplacementType GetReplacementType()
		{
			return BundleReplacementType.AddOrModify;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00014798 File Offset: 0x00012998
		public override int GetBundleListIndex()
		{
			return this.bundleListIndex;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000147B0 File Offset: 0x000129B0
		public override string GetOriginalEntryName()
		{
			return this.oldName;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000147C8 File Offset: 0x000129C8
		public override string GetEntryName()
		{
			return this.newName;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000147E0 File Offset: 0x000129E0
		public override bool Init(AssetsFileReader entryReader, long entryPos, long entrySize, ClassDatabaseFile typeMeta = null)
		{
			bool flag = this.assetsFile != null;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				this.typeMeta = typeMeta;
				bool flag3 = entryReader == null;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					SegmentStream segmentStream = new SegmentStream(entryReader.BaseStream, entryPos, entrySize);
					AssetsFileReader assetsFileReader = new AssetsFileReader(segmentStream);
					this.assetsFile = new AssetsFile();
					this.assetsFile.Read(assetsFileReader);
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00014847 File Offset: 0x00012A47
		public override void Uninit()
		{
			this.assetsFile.Close();
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00014858 File Offset: 0x00012A58
		public override long Write(AssetsFileWriter writer)
		{
			SegmentStream segmentStream = new SegmentStream(writer.BaseStream, writer.Position);
			AssetsFileWriter assetsFileWriter = new AssetsFileWriter(segmentStream);
			this.assetsFile.Write(assetsFileWriter, -1L, this.assetReplacers, this.typeMeta);
			writer.Position = writer.BaseStream.Length;
			return writer.Position;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000148B8 File Offset: 0x00012AB8
		public override long WriteReplacer(AssetsFileWriter writer)
		{
			writer.Write(4);
			writer.Write(1);
			bool flag = this.oldName != null;
			if (flag)
			{
				writer.Write(1);
				writer.WriteCountStringInt16(this.oldName);
			}
			else
			{
				writer.Write(0);
			}
			bool flag2 = this.newName != null;
			if (flag2)
			{
				writer.Write(1);
				writer.WriteCountStringInt16(this.newName);
			}
			else
			{
				writer.Write(0);
			}
			writer.Write(1);
			writer.Write((long)this.assetReplacers.Count);
			foreach (AssetsReplacer assetsReplacer in this.assetReplacers)
			{
				assetsReplacer.WriteReplacer(writer);
			}
			return writer.Position;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x000149A8 File Offset: 0x00012BA8
		public override bool HasSerializedData()
		{
			return true;
		}

		// Token: 0x04000208 RID: 520
		private readonly string oldName;

		// Token: 0x04000209 RID: 521
		private readonly string newName;

		// Token: 0x0400020A RID: 522
		private readonly int bundleListIndex;

		// Token: 0x0400020B RID: 523
		private AssetsFile assetsFile;

		// Token: 0x0400020C RID: 524
		private readonly List<AssetsReplacer> assetReplacers;

		// Token: 0x0400020D RID: 525
		private ClassDatabaseFile typeMeta;
	}
}
