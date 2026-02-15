using System;
using AssetsTools.NET.Extra;

namespace AssetsTools.NET
{
	// Token: 0x02000065 RID: 101
	public class ClassDatabaseType
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00015572 File Offset: 0x00013772
		// (set) Token: 0x0600037F RID: 895 RVA: 0x0001557A File Offset: 0x0001377A
		public int ClassId { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00015583 File Offset: 0x00013783
		// (set) Token: 0x06000381 RID: 897 RVA: 0x0001558B File Offset: 0x0001378B
		public ushort Name { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00015594 File Offset: 0x00013794
		// (set) Token: 0x06000383 RID: 899 RVA: 0x0001559C File Offset: 0x0001379C
		public ushort BaseName { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000384 RID: 900 RVA: 0x000155A5 File Offset: 0x000137A5
		// (set) Token: 0x06000385 RID: 901 RVA: 0x000155AD File Offset: 0x000137AD
		public ClassFileTypeFlags Flags { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000386 RID: 902 RVA: 0x000155B6 File Offset: 0x000137B6
		// (set) Token: 0x06000387 RID: 903 RVA: 0x000155BE File Offset: 0x000137BE
		public ClassDatabaseTypeNode EditorRootNode { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000388 RID: 904 RVA: 0x000155C7 File Offset: 0x000137C7
		// (set) Token: 0x06000389 RID: 905 RVA: 0x000155CF File Offset: 0x000137CF
		public ClassDatabaseTypeNode ReleaseRootNode { get; set; }

		// Token: 0x0600038A RID: 906 RVA: 0x000155D8 File Offset: 0x000137D8
		public void Read(AssetsFileReader reader)
		{
			this.ClassId = reader.ReadInt32();
			this.Name = reader.ReadUInt16();
			this.BaseName = reader.ReadUInt16();
			this.Flags = (ClassFileTypeFlags)reader.ReadByte();
			this.EditorRootNode = null;
			bool flag = Net35Polyfill.HasFlag(this.Flags, ClassFileTypeFlags.HasEditorRootNode);
			if (flag)
			{
				this.EditorRootNode = new ClassDatabaseTypeNode();
				this.EditorRootNode.Read(reader);
			}
			this.ReleaseRootNode = null;
			bool flag2 = Net35Polyfill.HasFlag(this.Flags, ClassFileTypeFlags.HasReleaseRootNode);
			if (flag2)
			{
				this.ReleaseRootNode = new ClassDatabaseTypeNode();
				this.ReleaseRootNode.Read(reader);
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001569C File Offset: 0x0001389C
		public void Write(AssetsFileWriter writer)
		{
			writer.Write(this.ClassId);
			writer.Write(this.Name);
			writer.Write(this.BaseName);
			writer.Write((byte)this.Flags);
			bool flag = Net35Polyfill.HasFlag(this.Flags, ClassFileTypeFlags.HasEditorRootNode) && this.EditorRootNode != null;
			if (flag)
			{
				this.EditorRootNode.Write(writer);
			}
			bool flag2 = Net35Polyfill.HasFlag(this.Flags, ClassFileTypeFlags.HasReleaseRootNode) && this.ReleaseRootNode != null;
			if (flag2)
			{
				this.ReleaseRootNode.Write(writer);
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00015754 File Offset: 0x00013954
		public ClassDatabaseTypeNode GetPreferredNode(bool preferEditor = false)
		{
			bool flag = this.EditorRootNode != null && this.ReleaseRootNode != null;
			ClassDatabaseTypeNode classDatabaseTypeNode;
			if (flag)
			{
				classDatabaseTypeNode = (preferEditor ? this.EditorRootNode : this.ReleaseRootNode);
			}
			else
			{
				bool flag2 = this.EditorRootNode != null;
				if (flag2)
				{
					classDatabaseTypeNode = this.EditorRootNode;
				}
				else
				{
					bool flag3 = this.ReleaseRootNode != null;
					if (flag3)
					{
						classDatabaseTypeNode = this.ReleaseRootNode;
					}
					else
					{
						classDatabaseTypeNode = null;
					}
				}
			}
			return classDatabaseTypeNode;
		}
	}
}
