using System;
using System.Text;

namespace AssetsTools.NET
{
	// Token: 0x0200006C RID: 108
	public class ClassPackageHeader
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003BB RID: 955 RVA: 0x00016406 File Offset: 0x00014606
		// (set) Token: 0x060003BC RID: 956 RVA: 0x0001640E File Offset: 0x0001460E
		public string Magic { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003BD RID: 957 RVA: 0x00016417 File Offset: 0x00014617
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0001641F File Offset: 0x0001461F
		public byte FileVersion { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00016428 File Offset: 0x00014628
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x00016430 File Offset: 0x00014630
		public ClassFileCompressionType CompressionType { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00016439 File Offset: 0x00014639
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x00016441 File Offset: 0x00014641
		public byte DataType { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0001644A File Offset: 0x0001464A
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x00016452 File Offset: 0x00014652
		public uint CompressedSize { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0001645B File Offset: 0x0001465B
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x00016463 File Offset: 0x00014663
		public uint DecompressedSize { get; set; }

		// Token: 0x060003C7 RID: 967 RVA: 0x0001646C File Offset: 0x0001466C
		public void Read(AssetsFileReader reader)
		{
			reader.BigEndian = false;
			this.Magic = reader.ReadStringLength(4);
			bool flag = this.Magic != "TPK*";
			if (flag)
			{
				bool flag2 = this.Magic == "CLPK";
				if (flag2)
				{
					throw new NotSupportedException("Old CLPK style class packages are no longer supported.");
				}
				throw new NotSupportedException("TPK* magic not found. Is this really a tpk file?");
			}
			else
			{
				this.FileVersion = reader.ReadByte();
				bool flag3 = this.FileVersion > 1;
				if (flag3)
				{
					throw new Exception(string.Format("Unsupported or invalid file version {0}.", this.FileVersion));
				}
				this.CompressionType = (ClassFileCompressionType)reader.ReadByte();
				this.DataType = reader.ReadByte();
				reader.ReadByte();
				reader.ReadUInt32();
				this.CompressedSize = reader.ReadUInt32();
				this.DecompressedSize = reader.ReadUInt32();
				return;
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00016548 File Offset: 0x00014748
		public void Write(AssetsFileWriter writer)
		{
			writer.BigEndian = false;
			writer.Write(Encoding.ASCII.GetBytes(this.Magic));
			writer.Write(this.FileVersion);
			writer.Write((byte)this.CompressionType);
			writer.Write(this.DataType);
			writer.Write(0);
			writer.Write(0);
			writer.Write(this.CompressedSize);
			writer.Write(this.DecompressedSize);
		}
	}
}
