using System;

namespace AssetsTools.NET
{
	// Token: 0x0200005B RID: 91
	public class BundleRenamer : BundleReplacer
	{
		// Token: 0x06000325 RID: 805 RVA: 0x000145B8 File Offset: 0x000127B8
		public BundleRenamer(string oldName, string newName, int bundleListIndex = -1, bool hasSerializedData = false)
		{
			this.oldName = oldName;
			bool flag = newName == null;
			if (flag)
			{
				this.newName = oldName;
			}
			else
			{
				this.newName = newName;
			}
			this.bundleListIndex = bundleListIndex;
			this.hasSerializedData = hasSerializedData;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000145FC File Offset: 0x000127FC
		public override BundleReplacementType GetReplacementType()
		{
			return BundleReplacementType.Rename;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00014610 File Offset: 0x00012810
		public override int GetBundleListIndex()
		{
			return this.bundleListIndex;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00014628 File Offset: 0x00012828
		public override string GetOriginalEntryName()
		{
			return this.oldName;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00014640 File Offset: 0x00012840
		public override string GetEntryName()
		{
			return this.newName;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00014658 File Offset: 0x00012858
		public override bool Init(AssetsFileReader entryReader, long entryPos, long entrySize, ClassDatabaseFile typeMeta = null)
		{
			return true;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0001452F File Offset: 0x0001272F
		public override void Uninit()
		{
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0001466C File Offset: 0x0001286C
		public override long Write(AssetsFileWriter writer)
		{
			return writer.Position;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00014684 File Offset: 0x00012884
		public override long WriteReplacer(AssetsFileWriter writer)
		{
			writer.Write(1);
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
			writer.Write(this.hasSerializedData);
			return writer.Position;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0001471C File Offset: 0x0001291C
		public override bool HasSerializedData()
		{
			return this.hasSerializedData;
		}

		// Token: 0x04000200 RID: 512
		private readonly string oldName;

		// Token: 0x04000201 RID: 513
		private readonly string newName;

		// Token: 0x04000202 RID: 514
		private readonly int bundleListIndex;

		// Token: 0x04000203 RID: 515
		private readonly bool hasSerializedData;
	}
}
