using System;
using System.Text;
using AssetsTools.NET.Extra;

namespace AssetsTools.NET
{
	// Token: 0x02000063 RID: 99
	public class ClassDatabaseFileHeader
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000368 RID: 872 RVA: 0x000152C6 File Offset: 0x000134C6
		// (set) Token: 0x06000369 RID: 873 RVA: 0x000152CE File Offset: 0x000134CE
		public string Magic { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600036A RID: 874 RVA: 0x000152D7 File Offset: 0x000134D7
		// (set) Token: 0x0600036B RID: 875 RVA: 0x000152DF File Offset: 0x000134DF
		public byte FileVersion { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600036C RID: 876 RVA: 0x000152E8 File Offset: 0x000134E8
		// (set) Token: 0x0600036D RID: 877 RVA: 0x000152F0 File Offset: 0x000134F0
		public UnityVersion Version { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600036E RID: 878 RVA: 0x000152F9 File Offset: 0x000134F9
		// (set) Token: 0x0600036F RID: 879 RVA: 0x00015301 File Offset: 0x00013501
		public ClassFileCompressionType CompressionType { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0001530A File Offset: 0x0001350A
		// (set) Token: 0x06000371 RID: 881 RVA: 0x00015312 File Offset: 0x00013512
		public int CompressedSize { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0001531B File Offset: 0x0001351B
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00015323 File Offset: 0x00013523
		public int DecompressedSize { get; set; }

		// Token: 0x06000374 RID: 884 RVA: 0x0001532C File Offset: 0x0001352C
		public void Read(AssetsFileReader reader)
		{
			this.Magic = reader.ReadStringLength(4);
			bool flag = this.Magic != "CLDB";
			if (flag)
			{
				bool flag2 = this.Magic == "cldb";
				if (flag2)
				{
					throw new NotSupportedException("Old cldb style class databases are no longer supported.");
				}
				throw new NotSupportedException("CLDB magic not found. Is this really a class database file?");
			}
			else
			{
				this.FileVersion = reader.ReadByte();
				bool flag3 = this.FileVersion > 1;
				if (flag3)
				{
					throw new Exception(string.Format("Unsupported or invalid file version {0}.", this.FileVersion));
				}
				this.Version = UnityVersion.FromUInt64(reader.ReadUInt64());
				this.CompressionType = (ClassFileCompressionType)reader.ReadByte();
				this.CompressedSize = reader.ReadInt32();
				this.DecompressedSize = reader.ReadInt32();
				return;
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000153F8 File Offset: 0x000135F8
		public void Write(AssetsFileWriter writer)
		{
			writer.Write(Encoding.ASCII.GetBytes(this.Magic));
			writer.Write(this.FileVersion);
			writer.Write(this.Version.ToUInt64());
			writer.Write((byte)this.CompressionType);
			writer.Write(this.CompressedSize);
			writer.Write(this.DecompressedSize);
		}
	}
}
