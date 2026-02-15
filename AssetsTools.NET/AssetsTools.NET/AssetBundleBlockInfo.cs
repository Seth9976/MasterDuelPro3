using System;

namespace AssetsTools.NET
{
	// Token: 0x0200002F RID: 47
	public class AssetBundleBlockInfo
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000CF58 File Offset: 0x0000B158
		// (set) Token: 0x06000122 RID: 290 RVA: 0x0000CF60 File Offset: 0x0000B160
		public uint DecompressedSize { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000CF69 File Offset: 0x0000B169
		// (set) Token: 0x06000124 RID: 292 RVA: 0x0000CF71 File Offset: 0x0000B171
		public uint CompressedSize { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000CF7A File Offset: 0x0000B17A
		// (set) Token: 0x06000126 RID: 294 RVA: 0x0000CF82 File Offset: 0x0000B182
		public ushort Flags { get; set; }

		// Token: 0x06000127 RID: 295 RVA: 0x0000CF8C File Offset: 0x0000B18C
		public byte GetCompressionType()
		{
			return (byte)(this.Flags & 63);
		}
	}
}
