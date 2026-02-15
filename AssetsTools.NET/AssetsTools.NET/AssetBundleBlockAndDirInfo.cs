using System;

namespace AssetsTools.NET
{
	// Token: 0x0200002E RID: 46
	public class AssetBundleBlockAndDirInfo
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000CCDF File Offset: 0x0000AEDF
		// (set) Token: 0x06000119 RID: 281 RVA: 0x0000CCE7 File Offset: 0x0000AEE7
		public Hash128 Hash { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000CCF0 File Offset: 0x0000AEF0
		// (set) Token: 0x0600011B RID: 283 RVA: 0x0000CCF8 File Offset: 0x0000AEF8
		public AssetBundleBlockInfo[] BlockInfos { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000CD01 File Offset: 0x0000AF01
		// (set) Token: 0x0600011D RID: 285 RVA: 0x0000CD09 File Offset: 0x0000AF09
		public AssetBundleDirectoryInfo[] DirectoryInfos { get; set; }

		// Token: 0x0600011E RID: 286 RVA: 0x0000CD14 File Offset: 0x0000AF14
		public void Read(AssetsFileReader reader)
		{
			this.Hash = new Hash128(reader.ReadBytes(16));
			int num = reader.ReadInt32();
			this.BlockInfos = new AssetBundleBlockInfo[num];
			for (int i = 0; i < num; i++)
			{
				this.BlockInfos[i] = new AssetBundleBlockInfo();
				this.BlockInfos[i].DecompressedSize = reader.ReadUInt32();
				this.BlockInfos[i].CompressedSize = reader.ReadUInt32();
				this.BlockInfos[i].Flags = reader.ReadUInt16();
			}
			int num2 = reader.ReadInt32();
			this.DirectoryInfos = new AssetBundleDirectoryInfo[num2];
			for (int j = 0; j < num2; j++)
			{
				this.DirectoryInfos[j] = new AssetBundleDirectoryInfo();
				this.DirectoryInfos[j].Offset = reader.ReadInt64();
				this.DirectoryInfos[j].DecompressedSize = reader.ReadInt64();
				this.DirectoryInfos[j].Flags = reader.ReadUInt32();
				this.DirectoryInfos[j].Name = reader.ReadNullTerminated();
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000CE30 File Offset: 0x0000B030
		public void Write(AssetsFileWriter writer)
		{
			bool flag = this.Hash.data == null;
			if (flag)
			{
				writer.Write(0UL);
				writer.Write(0UL);
			}
			else
			{
				writer.Write(this.Hash.data);
			}
			int num = this.BlockInfos.Length;
			writer.Write(num);
			for (int i = 0; i < num; i++)
			{
				writer.Write(this.BlockInfos[i].DecompressedSize);
				writer.Write(this.BlockInfos[i].CompressedSize);
				writer.Write(this.BlockInfos[i].Flags);
			}
			int num2 = this.DirectoryInfos.Length;
			writer.Write(num2);
			for (int j = 0; j < num2; j++)
			{
				writer.Write(this.DirectoryInfos[j].Offset);
				writer.Write(this.DirectoryInfos[j].DecompressedSize);
				writer.Write(this.DirectoryInfos[j].Flags);
				writer.WriteNullTerminated(this.DirectoryInfos[j].Name);
			}
		}
	}
}
