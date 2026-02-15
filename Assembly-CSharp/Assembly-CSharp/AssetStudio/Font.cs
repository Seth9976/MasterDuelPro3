using System;

namespace AssetStudio
{
	// Token: 0x020000E7 RID: 231
	public sealed class Font : NamedObject
	{
		// Token: 0x06000325 RID: 805 RVA: 0x0000F208 File Offset: 0x0000D408
		public Font(ObjectReader reader)
			: base(reader)
		{
			if ((this.version[0] == 5 && this.version[1] >= 5) || this.version[0] > 5)
			{
				reader.ReadSingle();
				new PPtr<Material>(reader);
				reader.ReadSingle();
				new PPtr<Texture>(reader);
				reader.ReadInt32();
				reader.ReadSingle();
				reader.ReadInt32();
				reader.ReadInt32();
				reader.ReadInt32();
				int m_CharacterRects_size = reader.ReadInt32();
				for (int i = 0; i < m_CharacterRects_size; i++)
				{
					reader.Position += 44L;
				}
				int m_KerningValues_size = reader.ReadInt32();
				for (int j = 0; j < m_KerningValues_size; j++)
				{
					reader.Position += 8L;
				}
				reader.ReadSingle();
				int m_FontData_size = reader.ReadInt32();
				if (m_FontData_size > 0)
				{
					this.m_FontData = reader.ReadBytes(m_FontData_size);
					return;
				}
			}
			else
			{
				reader.ReadInt32();
				if (this.version[0] <= 3)
				{
					reader.ReadInt32();
					reader.ReadInt32();
				}
				reader.ReadSingle();
				reader.ReadSingle();
				if (this.version[0] <= 3)
				{
					int m_PerCharacterKerning_size = reader.ReadInt32();
					for (int k = 0; k < m_PerCharacterKerning_size; k++)
					{
						reader.ReadInt32();
						reader.ReadSingle();
					}
				}
				else
				{
					reader.ReadInt32();
					reader.ReadInt32();
				}
				reader.ReadInt32();
				new PPtr<Material>(reader);
				int m_CharacterRects_size2 = reader.ReadInt32();
				for (int l = 0; l < m_CharacterRects_size2; l++)
				{
					reader.ReadInt32();
					reader.ReadSingle();
					reader.ReadSingle();
					reader.ReadSingle();
					reader.ReadSingle();
					reader.ReadSingle();
					reader.ReadSingle();
					reader.ReadSingle();
					reader.ReadSingle();
					reader.ReadSingle();
					if (this.version[0] >= 4)
					{
						reader.ReadBoolean();
						reader.AlignStream();
					}
				}
				new PPtr<Texture>(reader);
				int m_KerningValues_size2 = reader.ReadInt32();
				for (int m = 0; m < m_KerningValues_size2; m++)
				{
					reader.ReadInt16();
					reader.ReadInt16();
					reader.ReadSingle();
				}
				if (this.version[0] <= 3)
				{
					reader.ReadBoolean();
					reader.AlignStream();
				}
				else
				{
					reader.ReadSingle();
				}
				int m_FontData_size2 = reader.ReadInt32();
				if (m_FontData_size2 > 0)
				{
					this.m_FontData = reader.ReadBytes(m_FontData_size2);
				}
			}
		}

		// Token: 0x040006CC RID: 1740
		public byte[] m_FontData;
	}
}
