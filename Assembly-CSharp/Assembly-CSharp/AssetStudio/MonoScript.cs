using System;

namespace AssetStudio
{
	// Token: 0x02000101 RID: 257
	public sealed class MonoScript : NamedObject
	{
		// Token: 0x0600034F RID: 847 RVA: 0x0001185C File Offset: 0x0000FA5C
		public MonoScript(ObjectReader reader)
			: base(reader)
		{
			if (this.version[0] > 3 || (this.version[0] == 3 && this.version[1] >= 4))
			{
				reader.ReadInt32();
			}
			if (this.version[0] < 5)
			{
				reader.ReadUInt32();
			}
			else
			{
				reader.ReadBytes(16);
			}
			if (this.version[0] < 3)
			{
				reader.ReadAlignedString();
			}
			this.m_ClassName = reader.ReadAlignedString();
			if (this.version[0] >= 3)
			{
				this.m_Namespace = reader.ReadAlignedString();
			}
			this.m_AssemblyName = reader.ReadAlignedString();
			if (this.version[0] < 2018 || (this.version[0] == 2018 && this.version[1] < 2))
			{
				reader.ReadBoolean();
			}
		}

		// Token: 0x0400075B RID: 1883
		public string m_ClassName;

		// Token: 0x0400075C RID: 1884
		public string m_Namespace;

		// Token: 0x0400075D RID: 1885
		public string m_AssemblyName;
	}
}
