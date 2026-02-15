using System;

namespace AssetsTools.NET
{
	// Token: 0x02000037 RID: 55
	public class AssetBundleHeader
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000E627 File Offset: 0x0000C827
		// (set) Token: 0x06000150 RID: 336 RVA: 0x0000E62F File Offset: 0x0000C82F
		public string Signature { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000E638 File Offset: 0x0000C838
		// (set) Token: 0x06000152 RID: 338 RVA: 0x0000E640 File Offset: 0x0000C840
		public uint Version { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000E649 File Offset: 0x0000C849
		// (set) Token: 0x06000154 RID: 340 RVA: 0x0000E651 File Offset: 0x0000C851
		public string GenerationVersion { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000E65A File Offset: 0x0000C85A
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000E662 File Offset: 0x0000C862
		public string EngineVersion { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000E66B File Offset: 0x0000C86B
		// (set) Token: 0x06000158 RID: 344 RVA: 0x0000E673 File Offset: 0x0000C873
		public AssetBundleFSHeader FileStreamHeader { get; set; }

		// Token: 0x06000159 RID: 345 RVA: 0x0000E67C File Offset: 0x0000C87C
		public void Read(AssetsFileReader reader)
		{
			reader.BigEndian = true;
			this.Signature = reader.ReadNullTerminated();
			this.Version = reader.ReadUInt32();
			this.GenerationVersion = reader.ReadNullTerminated();
			this.EngineVersion = reader.ReadNullTerminated();
			bool flag = this.Signature == "UnityFS";
			if (flag)
			{
				this.FileStreamHeader = new AssetBundleFSHeader();
				this.FileStreamHeader.Read(reader);
				return;
			}
			throw new NotSupportedException(this.Signature + " signature not supported!");
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000E710 File Offset: 0x0000C910
		public void Write(AssetsFileWriter writer)
		{
			writer.BigEndian = true;
			writer.WriteNullTerminated(this.Signature);
			writer.Write(this.Version);
			writer.WriteNullTerminated(this.GenerationVersion);
			writer.WriteNullTerminated(this.EngineVersion);
			bool flag = this.Signature == "UnityFS";
			if (flag)
			{
				this.FileStreamHeader.Write(writer);
				return;
			}
			throw new NotSupportedException(this.Signature + " signature not supported!");
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000E798 File Offset: 0x0000C998
		public long GetBundleInfoOffset()
		{
			bool flag = this.Signature != "UnityFS";
			if (flag)
			{
				throw new NotSupportedException(this.Signature + " signature not supported!");
			}
			AssetBundleFSHeaderFlags flags = this.FileStreamHeader.Flags;
			long totalFileSize = this.FileStreamHeader.TotalFileSize;
			long num = (long)((ulong)this.FileStreamHeader.CompressedSize);
			bool flag2 = (flags & AssetBundleFSHeaderFlags.BlockAndDirAtEnd) > AssetBundleFSHeaderFlags.None;
			long num2;
			if (flag2)
			{
				bool flag3 = totalFileSize == 0L;
				if (flag3)
				{
					num2 = -1L;
				}
				else
				{
					num2 = totalFileSize - num;
				}
			}
			else
			{
				long num3 = (long)(this.GenerationVersion.Length + this.EngineVersion.Length + 26);
				bool flag4 = this.Version >= 7U;
				if (flag4)
				{
					bool flag5 = (flags & AssetBundleFSHeaderFlags.OldWebPluginCompatibility) > AssetBundleFSHeaderFlags.None;
					if (flag5)
					{
						num2 = (num3 + 10L + 15L) & -16L;
					}
					else
					{
						num2 = (num3 + (long)this.Signature.Length + 1L + 15L) & -16L;
					}
				}
				else
				{
					bool flag6 = (flags & AssetBundleFSHeaderFlags.OldWebPluginCompatibility) > AssetBundleFSHeaderFlags.None;
					if (flag6)
					{
						num2 = num3 + 10L;
					}
					else
					{
						num2 = num3 + (long)this.Signature.Length + 1L;
					}
				}
			}
			return num2;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000E8C8 File Offset: 0x0000CAC8
		public long GetFileDataOffset()
		{
			bool flag = this.Signature != "UnityFS";
			if (flag)
			{
				throw new NotSupportedException(this.Signature + " signature not supported!");
			}
			AssetBundleFSHeaderFlags flags = this.FileStreamHeader.Flags;
			long num = (long)((ulong)this.FileStreamHeader.CompressedSize);
			long num2 = (long)(this.GenerationVersion.Length + this.EngineVersion.Length + 26);
			bool flag2 = (flags & AssetBundleFSHeaderFlags.OldWebPluginCompatibility) > AssetBundleFSHeaderFlags.None;
			if (flag2)
			{
				num2 += 10L;
			}
			else
			{
				num2 += (long)(this.Signature.Length + 1);
			}
			bool flag3 = this.Version >= 7U;
			if (flag3)
			{
				num2 = (num2 + 15L) & -16L;
			}
			bool flag4 = (flags & AssetBundleFSHeaderFlags.BlockAndDirAtEnd) == AssetBundleFSHeaderFlags.None;
			if (flag4)
			{
				num2 += num;
			}
			bool flag5 = (flags & AssetBundleFSHeaderFlags.BlockInfoNeedPaddingAtStart) > AssetBundleFSHeaderFlags.None;
			if (flag5)
			{
				num2 = (num2 + 15L) & -16L;
			}
			return num2;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000E9B0 File Offset: 0x0000CBB0
		public byte GetCompressionType()
		{
			bool flag = this.Signature != "UnityFS";
			if (flag)
			{
				throw new NotSupportedException(this.Signature + " signature not supported!");
			}
			return (byte)(this.FileStreamHeader.Flags & AssetBundleFSHeaderFlags.CompressionMask);
		}
	}
}
