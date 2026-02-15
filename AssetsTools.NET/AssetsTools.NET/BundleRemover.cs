using System;

namespace AssetsTools.NET
{
	// Token: 0x0200005A RID: 90
	public class BundleRemover : BundleReplacer
	{
		// Token: 0x0600031A RID: 794 RVA: 0x0001448F File Offset: 0x0001268F
		public BundleRemover(string name, int bundleListIndex = -1)
		{
			this.name = name;
			this.bundleListIndex = bundleListIndex;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000144A7 File Offset: 0x000126A7
		public BundleRemover(int index)
		{
			this.name = null;
			this.bundleListIndex = index;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000144C0 File Offset: 0x000126C0
		public override BundleReplacementType GetReplacementType()
		{
			return BundleReplacementType.Remove;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000144D4 File Offset: 0x000126D4
		public override int GetBundleListIndex()
		{
			return this.bundleListIndex;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000144EC File Offset: 0x000126EC
		public override string GetOriginalEntryName()
		{
			return this.name;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00014504 File Offset: 0x00012704
		public override string GetEntryName()
		{
			return this.name;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0001451C File Offset: 0x0001271C
		public override bool Init(AssetsFileReader entryReader, long entryPos, long entrySize, ClassDatabaseFile typeMeta = null)
		{
			return true;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0001452F File Offset: 0x0001272F
		public override void Uninit()
		{
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00014534 File Offset: 0x00012734
		public override long Write(AssetsFileWriter writer)
		{
			return writer.Position;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0001454C File Offset: 0x0001274C
		public override long WriteReplacer(AssetsFileWriter writer)
		{
			writer.Write(0);
			writer.Write(1);
			bool flag = this.name != null;
			if (flag)
			{
				writer.Write(1);
				writer.WriteCountStringInt16(this.name);
			}
			else
			{
				writer.Write(0);
			}
			return writer.Position;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000145A4 File Offset: 0x000127A4
		public override bool HasSerializedData()
		{
			return false;
		}

		// Token: 0x040001FE RID: 510
		private readonly string name;

		// Token: 0x040001FF RID: 511
		private readonly int bundleListIndex;
	}
}
