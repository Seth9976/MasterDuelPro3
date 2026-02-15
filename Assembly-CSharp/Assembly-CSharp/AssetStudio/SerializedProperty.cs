using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x02000114 RID: 276
	public class SerializedProperty
	{
		// Token: 0x0600036D RID: 877 RVA: 0x0001238C File Offset: 0x0001058C
		public SerializedProperty(BinaryReader reader)
		{
			this.m_Name = reader.ReadAlignedString();
			this.m_Description = reader.ReadAlignedString();
			this.m_Attributes = reader.ReadStringArray();
			this.m_Type = (SerializedPropertyType)reader.ReadInt32();
			this.m_Flags = reader.ReadUInt32();
			this.m_DefValue = reader.ReadSingleArray(4);
			this.m_DefTexture = new SerializedTextureProperty(reader);
		}

		// Token: 0x0400078F RID: 1935
		public string m_Name;

		// Token: 0x04000790 RID: 1936
		public string m_Description;

		// Token: 0x04000791 RID: 1937
		public string[] m_Attributes;

		// Token: 0x04000792 RID: 1938
		public SerializedPropertyType m_Type;

		// Token: 0x04000793 RID: 1939
		public uint m_Flags;

		// Token: 0x04000794 RID: 1940
		public float[] m_DefValue;

		// Token: 0x04000795 RID: 1941
		public SerializedTextureProperty m_DefTexture;
	}
}
