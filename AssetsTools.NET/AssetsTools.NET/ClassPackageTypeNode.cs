using System;

namespace AssetsTools.NET
{
	// Token: 0x0200006E RID: 110
	public class ClassPackageTypeNode
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00016760 File Offset: 0x00014960
		// (set) Token: 0x060003DA RID: 986 RVA: 0x00016768 File Offset: 0x00014968
		public ushort TypeName { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00016771 File Offset: 0x00014971
		// (set) Token: 0x060003DC RID: 988 RVA: 0x00016779 File Offset: 0x00014979
		public ushort FieldName { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00016782 File Offset: 0x00014982
		// (set) Token: 0x060003DE RID: 990 RVA: 0x0001678A File Offset: 0x0001498A
		public int ByteSize { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00016793 File Offset: 0x00014993
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x0001679B File Offset: 0x0001499B
		public ushort Version { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x000167A4 File Offset: 0x000149A4
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x000167AC File Offset: 0x000149AC
		public byte TypeFlags { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x000167B5 File Offset: 0x000149B5
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x000167BD File Offset: 0x000149BD
		public uint MetaFlag { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x000167C6 File Offset: 0x000149C6
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x000167CE File Offset: 0x000149CE
		public ushort[] SubNodes { get; set; }

		// Token: 0x060003E7 RID: 999 RVA: 0x000167D8 File Offset: 0x000149D8
		public void Read(AssetsFileReader reader)
		{
			this.TypeName = reader.ReadUInt16();
			this.FieldName = reader.ReadUInt16();
			this.ByteSize = reader.ReadInt32();
			this.Version = reader.ReadUInt16();
			this.TypeFlags = reader.ReadByte();
			this.MetaFlag = reader.ReadUInt32();
			ushort num = reader.ReadUInt16();
			this.SubNodes = new ushort[(int)num];
			for (int i = 0; i < (int)num; i++)
			{
				this.SubNodes[i] = reader.ReadUInt16();
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00016868 File Offset: 0x00014A68
		public void Write(AssetsFileWriter writer)
		{
			writer.Write(this.TypeName);
			writer.Write(this.FieldName);
			writer.Write(this.ByteSize);
			writer.Write(this.Version);
			writer.Write(this.TypeFlags);
			writer.Write(this.MetaFlag);
			writer.Write(this.SubNodes.Length);
			for (int i = 0; i < this.SubNodes.Length; i++)
			{
				writer.Write(this.SubNodes[i]);
			}
		}
	}
}
