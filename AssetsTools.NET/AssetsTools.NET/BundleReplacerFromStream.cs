using System;
using System.IO;
using AssetsTools.NET.Extra;

namespace AssetsTools.NET
{
	// Token: 0x02000060 RID: 96
	public class BundleReplacerFromStream : BundleReplacer
	{
		// Token: 0x0600034E RID: 846 RVA: 0x00014B84 File Offset: 0x00012D84
		public BundleReplacerFromStream(string oldName, string newName, bool hasSerializedData, Stream stream, long offset = 0L, long size = -1L, int bundleListIndex = -1)
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
			this.stream = stream;
			this.offset = offset;
			bool flag2 = size == -1L;
			if (flag2)
			{
				this.size = stream.Length;
			}
			else
			{
				this.size = size;
			}
			this.bundleListIndex = bundleListIndex;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00014BF8 File Offset: 0x00012DF8
		public override BundleReplacementType GetReplacementType()
		{
			return BundleReplacementType.AddOrModify;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00014C0C File Offset: 0x00012E0C
		public override int GetBundleListIndex()
		{
			return this.bundleListIndex;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00014C24 File Offset: 0x00012E24
		public override string GetOriginalEntryName()
		{
			return this.oldName;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00014C3C File Offset: 0x00012E3C
		public override string GetEntryName()
		{
			return this.newName;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00014C54 File Offset: 0x00012E54
		public override bool Init(AssetsFileReader entryReader, long entryPos, long entrySize, ClassDatabaseFile typeMeta = null)
		{
			return true;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0001452F File Offset: 0x0001272F
		public override void Uninit()
		{
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00014C68 File Offset: 0x00012E68
		public override long Write(AssetsFileWriter writer)
		{
			this.stream.Position = this.offset;
			this.stream.CopyToCompat(writer.BaseStream, this.size, 81920);
			return writer.Position;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00014CB0 File Offset: 0x00012EB0
		public override long WriteReplacer(AssetsFileWriter writer)
		{
			writer.Write(2);
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

		// Token: 0x06000357 RID: 855 RVA: 0x00014D5C File Offset: 0x00012F5C
		public override bool HasSerializedData()
		{
			return this.hasSerializedData;
		}

		// Token: 0x04000214 RID: 532
		private readonly string oldName;

		// Token: 0x04000215 RID: 533
		private readonly string newName;

		// Token: 0x04000216 RID: 534
		private readonly bool hasSerializedData;

		// Token: 0x04000217 RID: 535
		private readonly Stream stream;

		// Token: 0x04000218 RID: 536
		private readonly long offset;

		// Token: 0x04000219 RID: 537
		private readonly long size;

		// Token: 0x0400021A RID: 538
		private readonly int bundleListIndex;
	}
}
