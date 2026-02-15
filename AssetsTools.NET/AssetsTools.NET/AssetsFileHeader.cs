using System;

namespace AssetsTools.NET
{
	// Token: 0x02000042 RID: 66
	public class AssetsFileHeader
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000FDF5 File Offset: 0x0000DFF5
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000FDFD File Offset: 0x0000DFFD
		public long MetadataSize { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000FE06 File Offset: 0x0000E006
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x0000FE0E File Offset: 0x0000E00E
		public long FileSize { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000FE17 File Offset: 0x0000E017
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x0000FE1F File Offset: 0x0000E01F
		public uint Version { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000FE28 File Offset: 0x0000E028
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000FE30 File Offset: 0x0000E030
		public long DataOffset { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000FE39 File Offset: 0x0000E039
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x0000FE41 File Offset: 0x0000E041
		public bool Endianness { get; set; }

		// Token: 0x060001C7 RID: 455 RVA: 0x0000FE4C File Offset: 0x0000E04C
		public void Read(AssetsFileReader reader)
		{
			reader.BigEndian = true;
			this.MetadataSize = (long)((ulong)reader.ReadUInt32());
			this.FileSize = (long)((ulong)reader.ReadUInt32());
			this.Version = reader.ReadUInt32();
			this.DataOffset = (long)((ulong)reader.ReadUInt32());
			this.Endianness = reader.ReadBoolean();
			reader.Position += 3L;
			bool flag = this.Version >= 22U;
			if (flag)
			{
				this.MetadataSize = (long)((ulong)reader.ReadUInt32());
				this.FileSize = reader.ReadInt64();
				this.DataOffset = reader.ReadInt64();
				reader.Position += 8L;
			}
			reader.BigEndian = this.Endianness;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000FF10 File Offset: 0x0000E110
		public void Write(AssetsFileWriter writer)
		{
			writer.BigEndian = true;
			bool flag = this.Version >= 22U;
			if (flag)
			{
				writer.Write(0);
				writer.Write(0);
				writer.Write(this.Version);
				writer.Write(0);
			}
			else
			{
				writer.Write((uint)this.MetadataSize);
				writer.Write((uint)this.FileSize);
				writer.Write(this.Version);
				writer.Write((uint)this.DataOffset);
			}
			writer.Write(this.Endianness);
			writer.Write(new byte[3]);
			bool flag2 = this.Version >= 22U;
			if (flag2)
			{
				writer.Write((uint)this.MetadataSize);
				writer.Write(this.FileSize);
				writer.Write(this.DataOffset);
				writer.Write(new byte[8]);
			}
			writer.BigEndian = this.Endianness;
		}
	}
}
