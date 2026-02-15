using System;
using System.Collections.Generic;

namespace AssetsTools.NET
{
	// Token: 0x02000066 RID: 102
	public class ClassDatabaseTypeNode
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600038E RID: 910 RVA: 0x000157BF File Offset: 0x000139BF
		// (set) Token: 0x0600038F RID: 911 RVA: 0x000157C7 File Offset: 0x000139C7
		public ushort TypeName { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000390 RID: 912 RVA: 0x000157D0 File Offset: 0x000139D0
		// (set) Token: 0x06000391 RID: 913 RVA: 0x000157D8 File Offset: 0x000139D8
		public ushort FieldName { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000392 RID: 914 RVA: 0x000157E1 File Offset: 0x000139E1
		// (set) Token: 0x06000393 RID: 915 RVA: 0x000157E9 File Offset: 0x000139E9
		public int ByteSize { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000394 RID: 916 RVA: 0x000157F2 File Offset: 0x000139F2
		// (set) Token: 0x06000395 RID: 917 RVA: 0x000157FA File Offset: 0x000139FA
		public ushort Version { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00015803 File Offset: 0x00013A03
		// (set) Token: 0x06000397 RID: 919 RVA: 0x0001580B File Offset: 0x00013A0B
		public byte TypeFlags { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00015814 File Offset: 0x00013A14
		// (set) Token: 0x06000399 RID: 921 RVA: 0x0001581C File Offset: 0x00013A1C
		public uint MetaFlag { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00015825 File Offset: 0x00013A25
		// (set) Token: 0x0600039B RID: 923 RVA: 0x0001582D File Offset: 0x00013A2D
		public List<ClassDatabaseTypeNode> Children { get; set; }

		// Token: 0x0600039C RID: 924 RVA: 0x00015838 File Offset: 0x00013A38
		public void Read(AssetsFileReader reader)
		{
			this.TypeName = reader.ReadUInt16();
			this.FieldName = reader.ReadUInt16();
			this.ByteSize = reader.ReadInt32();
			this.Version = reader.ReadUInt16();
			this.TypeFlags = reader.ReadByte();
			this.MetaFlag = reader.ReadUInt32();
			int num = (int)reader.ReadUInt16();
			this.Children = new List<ClassDatabaseTypeNode>(num);
			for (int i = 0; i < num; i++)
			{
				ClassDatabaseTypeNode classDatabaseTypeNode = new ClassDatabaseTypeNode();
				classDatabaseTypeNode.Read(reader);
				this.Children.Add(classDatabaseTypeNode);
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000158D8 File Offset: 0x00013AD8
		public void Write(AssetsFileWriter writer)
		{
			writer.Write(this.TypeName);
			writer.Write(this.FieldName);
			writer.Write(this.ByteSize);
			writer.Write(this.TypeFlags);
			writer.Write(this.MetaFlag);
			writer.Write((ushort)this.Children.Count);
			for (int i = 0; i < this.Children.Count; i++)
			{
				this.Children[i].Write(writer);
			}
		}
	}
}
