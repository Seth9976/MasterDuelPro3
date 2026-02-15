using System;

namespace AssetsTools.NET
{
	// Token: 0x02000040 RID: 64
	public class AssetsFileExternal
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000FC1E File Offset: 0x0000DE1E
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x0000FC26 File Offset: 0x0000DE26
		public string VirtualAssetPathName { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000FC2F File Offset: 0x0000DE2F
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000FC37 File Offset: 0x0000DE37
		public GUID128 Guid { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000FC40 File Offset: 0x0000DE40
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000FC48 File Offset: 0x0000DE48
		public AssetsFileExternalType Type { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000FC51 File Offset: 0x0000DE51
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x0000FC59 File Offset: 0x0000DE59
		public string PathName { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000FC62 File Offset: 0x0000DE62
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x0000FC6A File Offset: 0x0000DE6A
		public string OriginalPathName { get; set; }

		// Token: 0x060001BA RID: 442 RVA: 0x0000FC74 File Offset: 0x0000DE74
		public void Read(AssetsFileReader reader)
		{
			this.VirtualAssetPathName = reader.ReadNullTerminated();
			this.Guid = new GUID128(reader);
			this.Type = (AssetsFileExternalType)reader.ReadInt32();
			this.PathName = reader.ReadNullTerminated();
			this.OriginalPathName = this.PathName;
			bool flag = this.PathName == "resources/unity_builtin_extra";
			if (flag)
			{
				this.PathName = "Resources/unity_builtin_extra";
			}
			else
			{
				bool flag2 = this.PathName == "library/unity default resources" || this.PathName == "Library/unity default resources";
				if (flag2)
				{
					this.PathName = "Resources/unity default resources";
				}
				else
				{
					bool flag3 = this.PathName == "library/unity editor resources" || this.PathName == "Library/unity editor resources";
					if (flag3)
					{
						this.PathName = "Resources/unity editor resources";
					}
				}
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000FD58 File Offset: 0x0000DF58
		public void Write(AssetsFileWriter writer)
		{
			writer.WriteNullTerminated(this.VirtualAssetPathName);
			this.Guid.Write(writer);
			writer.Write((int)this.Type);
			string text = this.PathName;
			bool flag = (this.PathName == "Resources/unity_builtin_extra" || this.PathName == "Resources/unity default resources" || this.PathName == "Resources/unity editor resources") && this.OriginalPathName != string.Empty;
			if (flag)
			{
				text = this.OriginalPathName;
			}
			writer.WriteNullTerminated(text);
		}
	}
}
