using System;
using System.Collections.Generic;

namespace AssetStudio
{
	// Token: 0x02000129 RID: 297
	public class SerializedPass
	{
		// Token: 0x0600037F RID: 895 RVA: 0x00012F9C File Offset: 0x0001119C
		public SerializedPass(ObjectReader reader)
		{
			int[] version = reader.version;
			if (version[0] > 2020 || (version[0] == 2020 && version[1] >= 2))
			{
				int numEditorDataHash = reader.ReadInt32();
				this.m_EditorDataHash = new Hash128[numEditorDataHash];
				for (int i = 0; i < numEditorDataHash; i++)
				{
					this.m_EditorDataHash[i] = new Hash128(reader);
				}
				reader.AlignStream();
				this.m_Platforms = reader.ReadUInt8Array();
				reader.AlignStream();
				if (version[0] < 2021 || (version[0] == 2021 && version[1] < 2))
				{
					this.m_LocalKeywordMask = reader.ReadUInt16Array();
					reader.AlignStream();
					this.m_GlobalKeywordMask = reader.ReadUInt16Array();
					reader.AlignStream();
				}
			}
			int numIndices = reader.ReadInt32();
			this.m_NameIndices = new KeyValuePair<string, int>[numIndices];
			for (int j = 0; j < numIndices; j++)
			{
				this.m_NameIndices[j] = new KeyValuePair<string, int>(reader.ReadAlignedString(), reader.ReadInt32());
			}
			this.m_Type = (PassType)reader.ReadInt32();
			this.m_State = new SerializedShaderState(reader);
			this.m_ProgramMask = reader.ReadUInt32();
			this.progVertex = new SerializedProgram(reader);
			this.progFragment = new SerializedProgram(reader);
			this.progGeometry = new SerializedProgram(reader);
			this.progHull = new SerializedProgram(reader);
			this.progDomain = new SerializedProgram(reader);
			if (version[0] > 2019 || (version[0] == 2019 && version[1] >= 3))
			{
				this.progRayTracing = new SerializedProgram(reader);
			}
			this.m_HasInstancingVariant = reader.ReadBoolean();
			if (version[0] >= 2018)
			{
				reader.ReadBoolean();
			}
			reader.AlignStream();
			this.m_UseName = reader.ReadAlignedString();
			this.m_Name = reader.ReadAlignedString();
			this.m_TextureName = reader.ReadAlignedString();
			this.m_Tags = new SerializedTagMap(reader);
			if (version[0] == 2021 && version[1] >= 2)
			{
				this.m_SerializedKeywordStateMask = reader.ReadUInt16Array();
				reader.AlignStream();
			}
		}

		// Token: 0x04000826 RID: 2086
		public Hash128[] m_EditorDataHash;

		// Token: 0x04000827 RID: 2087
		public byte[] m_Platforms;

		// Token: 0x04000828 RID: 2088
		public ushort[] m_LocalKeywordMask;

		// Token: 0x04000829 RID: 2089
		public ushort[] m_GlobalKeywordMask;

		// Token: 0x0400082A RID: 2090
		public KeyValuePair<string, int>[] m_NameIndices;

		// Token: 0x0400082B RID: 2091
		public PassType m_Type;

		// Token: 0x0400082C RID: 2092
		public SerializedShaderState m_State;

		// Token: 0x0400082D RID: 2093
		public uint m_ProgramMask;

		// Token: 0x0400082E RID: 2094
		public SerializedProgram progVertex;

		// Token: 0x0400082F RID: 2095
		public SerializedProgram progFragment;

		// Token: 0x04000830 RID: 2096
		public SerializedProgram progGeometry;

		// Token: 0x04000831 RID: 2097
		public SerializedProgram progHull;

		// Token: 0x04000832 RID: 2098
		public SerializedProgram progDomain;

		// Token: 0x04000833 RID: 2099
		public SerializedProgram progRayTracing;

		// Token: 0x04000834 RID: 2100
		public bool m_HasInstancingVariant;

		// Token: 0x04000835 RID: 2101
		public string m_UseName;

		// Token: 0x04000836 RID: 2102
		public string m_Name;

		// Token: 0x04000837 RID: 2103
		public string m_TextureName;

		// Token: 0x04000838 RID: 2104
		public SerializedTagMap m_Tags;

		// Token: 0x04000839 RID: 2105
		public ushort[] m_SerializedKeywordStateMask;
	}
}
