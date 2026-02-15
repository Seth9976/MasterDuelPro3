using System;

namespace AssetsTools.NET
{
	// Token: 0x0200005F RID: 95
	public class BundleReplacerFromMemory : BundleReplacer
	{
		// Token: 0x06000344 RID: 836 RVA: 0x000149BC File Offset: 0x00012BBC
		public BundleReplacerFromMemory(string oldName, string newName, bool hasSerializedData, byte[] buffer, long size, int bundleListIndex = -1)
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
			this.hasSerializedData = hasSerializedData;
			this.buffer = buffer;
			bool flag2 = size == -1L;
			if (flag2)
			{
				this.size = (long)buffer.Length;
			}
			else
			{
				this.size = size;
			}
			this.bundleListIndex = bundleListIndex;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00014A28 File Offset: 0x00012C28
		public override BundleReplacementType GetReplacementType()
		{
			return BundleReplacementType.AddOrModify;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00014A3C File Offset: 0x00012C3C
		public override int GetBundleListIndex()
		{
			return this.bundleListIndex;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00014A54 File Offset: 0x00012C54
		public override string GetOriginalEntryName()
		{
			return this.oldName;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00014A6C File Offset: 0x00012C6C
		public override string GetEntryName()
		{
			return this.newName;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00014A84 File Offset: 0x00012C84
		public override bool Init(AssetsFileReader entryReader, long entryPos, long entrySize, ClassDatabaseFile typeMeta = null)
		{
			return true;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0001452F File Offset: 0x0001272F
		public override void Uninit()
		{
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00014A98 File Offset: 0x00012C98
		public override long Write(AssetsFileWriter writer)
		{
			writer.Write(this.buffer);
			return writer.Position;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00014AC0 File Offset: 0x00012CC0
		public override long WriteReplacer(AssetsFileWriter writer)
		{
			writer.Write(3);
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
			writer.Write(this.size);
			this.Write(writer);
			return writer.Position;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00014B6C File Offset: 0x00012D6C
		public override bool HasSerializedData()
		{
			return this.hasSerializedData;
		}

		// Token: 0x0400020E RID: 526
		private readonly string oldName;

		// Token: 0x0400020F RID: 527
		private readonly string newName;

		// Token: 0x04000210 RID: 528
		private readonly bool hasSerializedData;

		// Token: 0x04000211 RID: 529
		private readonly byte[] buffer;

		// Token: 0x04000212 RID: 530
		private readonly long size;

		// Token: 0x04000213 RID: 531
		private readonly int bundleListIndex;
	}
}
