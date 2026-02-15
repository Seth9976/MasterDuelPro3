using System;

namespace AssetStudio
{
	// Token: 0x0200012E RID: 302
	public class SerializedShader
	{
		// Token: 0x06000384 RID: 900 RVA: 0x00013280 File Offset: 0x00011480
		public SerializedShader(ObjectReader reader)
		{
			int[] version = reader.version;
			this.m_PropInfo = new SerializedProperties(reader);
			int numSubShaders = reader.ReadInt32();
			this.m_SubShaders = new SerializedSubShader[numSubShaders];
			for (int i = 0; i < numSubShaders; i++)
			{
				this.m_SubShaders[i] = new SerializedSubShader(reader);
			}
			if (version[0] > 2021 || (version[0] == 2021 && version[1] >= 2))
			{
				this.m_KeywordNames = reader.ReadStringArray();
				this.m_KeywordFlags = reader.ReadUInt8Array();
				reader.AlignStream();
			}
			this.m_Name = reader.ReadAlignedString();
			this.m_CustomEditorName = reader.ReadAlignedString();
			this.m_FallbackName = reader.ReadAlignedString();
			int numDependencies = reader.ReadInt32();
			this.m_Dependencies = new SerializedShaderDependency[numDependencies];
			for (int j = 0; j < numDependencies; j++)
			{
				this.m_Dependencies[j] = new SerializedShaderDependency(reader);
			}
			if (version[0] >= 2021)
			{
				int m_CustomEditorForRenderPipelinesSize = reader.ReadInt32();
				this.m_CustomEditorForRenderPipelines = new SerializedCustomEditorForRenderPipeline[m_CustomEditorForRenderPipelinesSize];
				for (int k = 0; k < m_CustomEditorForRenderPipelinesSize; k++)
				{
					this.m_CustomEditorForRenderPipelines[k] = new SerializedCustomEditorForRenderPipeline(reader);
				}
			}
			this.m_DisableNoSubshadersMessage = reader.ReadBoolean();
			reader.AlignStream();
		}

		// Token: 0x04000842 RID: 2114
		public SerializedProperties m_PropInfo;

		// Token: 0x04000843 RID: 2115
		public SerializedSubShader[] m_SubShaders;

		// Token: 0x04000844 RID: 2116
		public string[] m_KeywordNames;

		// Token: 0x04000845 RID: 2117
		public byte[] m_KeywordFlags;

		// Token: 0x04000846 RID: 2118
		public string m_Name;

		// Token: 0x04000847 RID: 2119
		public string m_CustomEditorName;

		// Token: 0x04000848 RID: 2120
		public string m_FallbackName;

		// Token: 0x04000849 RID: 2121
		public SerializedShaderDependency[] m_Dependencies;

		// Token: 0x0400084A RID: 2122
		public SerializedCustomEditorForRenderPipeline[] m_CustomEditorForRenderPipelines;

		// Token: 0x0400084B RID: 2123
		public bool m_DisableNoSubshadersMessage;
	}
}
