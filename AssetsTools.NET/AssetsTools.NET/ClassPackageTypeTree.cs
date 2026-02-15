using System;
using System.Collections.Generic;
using AssetsTools.NET.Extra;

namespace AssetsTools.NET
{
	// Token: 0x0200006F RID: 111
	public class ClassPackageTypeTree
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x000168FB File Offset: 0x00014AFB
		// (set) Token: 0x060003EB RID: 1003 RVA: 0x00016903 File Offset: 0x00014B03
		public DateTime CreationTime { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0001690C File Offset: 0x00014B0C
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x00016914 File Offset: 0x00014B14
		public List<UnityVersion> Versions { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0001691D File Offset: 0x00014B1D
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x00016925 File Offset: 0x00014B25
		public List<ClassPackageClassInfo> ClassInformation { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0001692E File Offset: 0x00014B2E
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x00016936 File Offset: 0x00014B36
		public ClassPackageCommonString CommonString { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0001693F File Offset: 0x00014B3F
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x00016947 File Offset: 0x00014B47
		public List<ClassPackageTypeNode> Nodes { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00016950 File Offset: 0x00014B50
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x00016958 File Offset: 0x00014B58
		public ClassDatabaseStringTable StringTable { get; set; }

		// Token: 0x060003F6 RID: 1014 RVA: 0x00016964 File Offset: 0x00014B64
		public void Read(AssetsFileReader reader)
		{
			this.CreationTime = DateTime.FromBinary(reader.ReadInt64());
			int num = reader.ReadInt32();
			this.Versions = new List<UnityVersion>(num);
			for (int i = 0; i < num; i++)
			{
				this.Versions.Add(UnityVersion.FromUInt64(reader.ReadUInt64()));
			}
			int num2 = reader.ReadInt32();
			this.ClassInformation = new List<ClassPackageClassInfo>();
			for (int j = 0; j < num2; j++)
			{
				ClassPackageClassInfo classPackageClassInfo = new ClassPackageClassInfo();
				classPackageClassInfo.Read(reader);
				this.ClassInformation.Add(classPackageClassInfo);
			}
			this.CommonString = new ClassPackageCommonString();
			this.CommonString.Read(reader);
			int num3 = reader.ReadInt32();
			this.Nodes = new List<ClassPackageTypeNode>(num3);
			for (int k = 0; k < num3; k++)
			{
				ClassPackageTypeNode classPackageTypeNode = new ClassPackageTypeNode();
				classPackageTypeNode.Read(reader);
				this.Nodes.Add(classPackageTypeNode);
			}
			this.StringTable = new ClassDatabaseStringTable();
			this.StringTable.Read(reader);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00016A88 File Offset: 0x00014C88
		public void Write(AssetsFileWriter writer)
		{
			writer.Write(this.CreationTime.ToBinary());
			writer.Write(this.Versions.Count);
			for (int i = 0; i < this.Versions.Count; i++)
			{
				writer.Write(this.Versions[i].ToUInt64());
			}
			writer.Write(this.ClassInformation.Count);
			for (int j = 0; j < this.ClassInformation.Count; j++)
			{
				this.ClassInformation[j].Write(writer);
			}
			this.CommonString.Write(writer);
			writer.Write(this.Nodes.Count);
			for (int k = 0; k < this.Nodes.Count; k++)
			{
				this.Nodes[k].Write(writer);
			}
			this.StringTable.Write(writer);
		}
	}
}
