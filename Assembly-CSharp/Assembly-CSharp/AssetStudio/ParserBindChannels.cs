using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x0200011D RID: 285
	public class ParserBindChannels
	{
		// Token: 0x06000375 RID: 885 RVA: 0x00012700 File Offset: 0x00010900
		public ParserBindChannels(BinaryReader reader)
		{
			int numChannels = reader.ReadInt32();
			this.m_Channels = new ShaderBindChannel[numChannels];
			for (int i = 0; i < numChannels; i++)
			{
				this.m_Channels[i] = new ShaderBindChannel(reader);
			}
			reader.AlignStream();
			this.m_SourceMap = reader.ReadUInt32();
		}

		// Token: 0x040007CB RID: 1995
		public ShaderBindChannel[] m_Channels;

		// Token: 0x040007CC RID: 1996
		public uint m_SourceMap;
	}
}
