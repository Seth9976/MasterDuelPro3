using System;

namespace AssetsTools.NET
{
	// Token: 0x02000035 RID: 53
	public class AssetBundleFSHeader
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000E575 File Offset: 0x0000C775
		// (set) Token: 0x06000145 RID: 325 RVA: 0x0000E57D File Offset: 0x0000C77D
		public long TotalFileSize { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000E586 File Offset: 0x0000C786
		// (set) Token: 0x06000147 RID: 327 RVA: 0x0000E58E File Offset: 0x0000C78E
		public uint CompressedSize { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000E597 File Offset: 0x0000C797
		// (set) Token: 0x06000149 RID: 329 RVA: 0x0000E59F File Offset: 0x0000C79F
		public uint DecompressedSize { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000E5A8 File Offset: 0x0000C7A8
		// (set) Token: 0x0600014B RID: 331 RVA: 0x0000E5B0 File Offset: 0x0000C7B0
		public AssetBundleFSHeaderFlags Flags { get; set; }

		// Token: 0x0600014C RID: 332 RVA: 0x0000E5B9 File Offset: 0x0000C7B9
		public void Read(AssetsFileReader reader)
		{
			this.TotalFileSize = reader.ReadInt64();
			this.CompressedSize = reader.ReadUInt32();
			this.DecompressedSize = reader.ReadUInt32();
			this.Flags = (AssetBundleFSHeaderFlags)reader.ReadUInt32();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000E5F0 File Offset: 0x0000C7F0
		public void Write(AssetsFileWriter writer)
		{
			writer.Write(this.TotalFileSize);
			writer.Write(this.CompressedSize);
			writer.Write(this.DecompressedSize);
			writer.Write((uint)this.Flags);
		}
	}
}
