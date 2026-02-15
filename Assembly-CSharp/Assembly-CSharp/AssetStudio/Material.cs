using System;

namespace AssetStudio
{
	// Token: 0x020000EB RID: 235
	public sealed class Material : NamedObject
	{
		// Token: 0x06000329 RID: 809 RVA: 0x0000F628 File Offset: 0x0000D828
		public Material(ObjectReader reader)
			: base(reader)
		{
			this.m_Shader = new PPtr<Shader>(reader);
			if (this.version[0] == 4 && this.version[1] >= 1)
			{
				reader.ReadStringArray();
			}
			if (this.version[0] > 2021 || (this.version[0] == 2021 && this.version[1] >= 3))
			{
				reader.ReadStringArray();
				reader.ReadStringArray();
			}
			else if (this.version[0] >= 5)
			{
				reader.ReadAlignedString();
			}
			if (this.version[0] >= 5)
			{
				reader.ReadUInt32();
			}
			if (this.version[0] > 5 || (this.version[0] == 5 && this.version[1] >= 6))
			{
				reader.ReadBoolean();
				reader.AlignStream();
			}
			if (this.version[0] > 4 || (this.version[0] == 4 && this.version[1] >= 3))
			{
				reader.ReadInt32();
			}
			if (this.version[0] > 5 || (this.version[0] == 5 && this.version[1] >= 1))
			{
				int stringTagMapSize = reader.ReadInt32();
				for (int i = 0; i < stringTagMapSize; i++)
				{
					reader.ReadAlignedString();
					reader.ReadAlignedString();
				}
			}
			if (this.version[0] > 5 || (this.version[0] == 5 && this.version[1] >= 6))
			{
				reader.ReadStringArray();
			}
			this.m_SavedProperties = new UnityPropertySheet(reader);
		}

		// Token: 0x040006DC RID: 1756
		public PPtr<Shader> m_Shader;

		// Token: 0x040006DD RID: 1757
		public UnityPropertySheet m_SavedProperties;
	}
}
