using System;
using AssetsTools.NET.Extra;

namespace AssetsTools.NET
{
	// Token: 0x0200006D RID: 109
	public class ClassPackageType
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003CA RID: 970 RVA: 0x000165C7 File Offset: 0x000147C7
		// (set) Token: 0x060003CB RID: 971 RVA: 0x000165CF File Offset: 0x000147CF
		public int ClassId { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003CC RID: 972 RVA: 0x000165D8 File Offset: 0x000147D8
		// (set) Token: 0x060003CD RID: 973 RVA: 0x000165E0 File Offset: 0x000147E0
		public ushort Name { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003CE RID: 974 RVA: 0x000165E9 File Offset: 0x000147E9
		// (set) Token: 0x060003CF RID: 975 RVA: 0x000165F1 File Offset: 0x000147F1
		public ushort BaseName { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x000165FA File Offset: 0x000147FA
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x00016602 File Offset: 0x00014802
		public ClassFileTypeFlags Flags { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0001660B File Offset: 0x0001480B
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x00016613 File Offset: 0x00014813
		public ushort EditorRootNode { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0001661C File Offset: 0x0001481C
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x00016624 File Offset: 0x00014824
		public ushort ReleaseRootNode { get; set; }

		// Token: 0x060003D6 RID: 982 RVA: 0x00016630 File Offset: 0x00014830
		public void Read(AssetsFileReader reader, int classId)
		{
			this.ClassId = classId;
			this.Name = reader.ReadUInt16();
			this.BaseName = reader.ReadUInt16();
			this.Flags = (ClassFileTypeFlags)reader.ReadByte();
			this.EditorRootNode = ushort.MaxValue;
			bool flag = Net35Polyfill.HasFlag(this.Flags, ClassFileTypeFlags.HasEditorRootNode);
			if (flag)
			{
				this.EditorRootNode = reader.ReadUInt16();
			}
			this.ReleaseRootNode = ushort.MaxValue;
			bool flag2 = Net35Polyfill.HasFlag(this.Flags, ClassFileTypeFlags.HasReleaseRootNode);
			if (flag2)
			{
				this.ReleaseRootNode = reader.ReadUInt16();
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x000166D8 File Offset: 0x000148D8
		public void Write(AssetsFileWriter writer)
		{
			writer.Write(this.Name);
			writer.Write(this.BaseName);
			writer.Write((byte)this.Flags);
			bool flag = Net35Polyfill.HasFlag(this.Flags, ClassFileTypeFlags.HasEditorRootNode);
			if (flag)
			{
				writer.Write(this.EditorRootNode);
			}
			bool flag2 = Net35Polyfill.HasFlag(this.Flags, ClassFileTypeFlags.HasReleaseRootNode);
			if (flag2)
			{
				writer.Write(this.ReleaseRootNode);
			}
		}
	}
}
