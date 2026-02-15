using System;
using System.Linq;

namespace AssetStudio
{
	// Token: 0x02000130 RID: 304
	public class Shader : NamedObject
	{
		// Token: 0x06000385 RID: 901 RVA: 0x000133B4 File Offset: 0x000115B4
		public Shader(ObjectReader reader)
			: base(reader)
		{
			if ((this.version[0] == 5 && this.version[1] >= 5) || this.version[0] > 5)
			{
				this.m_ParsedForm = new SerializedShader(reader);
				this.platforms = (from x in reader.ReadUInt32Array()
					select (ShaderCompilerPlatform)x).ToArray<ShaderCompilerPlatform>();
				if (this.version[0] > 2019 || (this.version[0] == 2019 && this.version[1] >= 3))
				{
					this.offsets = reader.ReadUInt32ArrayArray();
					this.compressedLengths = reader.ReadUInt32ArrayArray();
					this.decompressedLengths = reader.ReadUInt32ArrayArray();
				}
				else
				{
					this.offsets = (from x in reader.ReadUInt32Array()
						select new uint[] { x }).ToArray<uint[]>();
					this.compressedLengths = (from x in reader.ReadUInt32Array()
						select new uint[] { x }).ToArray<uint[]>();
					this.decompressedLengths = (from x in reader.ReadUInt32Array()
						select new uint[] { x }).ToArray<uint[]>();
				}
				this.compressedBlob = reader.ReadUInt8Array();
				reader.AlignStream();
				int m_DependenciesCount = reader.ReadInt32();
				for (int i = 0; i < m_DependenciesCount; i++)
				{
					new PPtr<Shader>(reader);
				}
				if (this.version[0] >= 2018)
				{
					int m_NonModifiableTexturesCount = reader.ReadInt32();
					for (int j = 0; j < m_NonModifiableTexturesCount; j++)
					{
						reader.ReadAlignedString();
						new PPtr<Texture>(reader);
					}
				}
				reader.ReadBoolean();
				reader.AlignStream();
				return;
			}
			this.m_Script = reader.ReadUInt8Array();
			reader.AlignStream();
			reader.ReadAlignedString();
			if (this.version[0] == 5 && this.version[1] >= 3)
			{
				this.decompressedSize = reader.ReadUInt32();
				this.m_SubProgramBlob = reader.ReadUInt8Array();
			}
		}

		// Token: 0x04000867 RID: 2151
		public byte[] m_Script;

		// Token: 0x04000868 RID: 2152
		public uint decompressedSize;

		// Token: 0x04000869 RID: 2153
		public byte[] m_SubProgramBlob;

		// Token: 0x0400086A RID: 2154
		public SerializedShader m_ParsedForm;

		// Token: 0x0400086B RID: 2155
		public ShaderCompilerPlatform[] platforms;

		// Token: 0x0400086C RID: 2156
		public uint[][] offsets;

		// Token: 0x0400086D RID: 2157
		public uint[][] compressedLengths;

		// Token: 0x0400086E RID: 2158
		public uint[][] decompressedLengths;

		// Token: 0x0400086F RID: 2159
		public byte[] compressedBlob;
	}
}
