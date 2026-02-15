using System;
using System.Collections;
using System.Linq;

namespace AssetStudio
{
	// Token: 0x020000F0 RID: 240
	public class VertexData
	{
		// Token: 0x06000330 RID: 816 RVA: 0x0000F93C File Offset: 0x0000DB3C
		public VertexData(ObjectReader reader)
		{
			int[] version = reader.version;
			if (version[0] < 2018)
			{
				this.m_CurrentChannels = reader.ReadUInt32();
			}
			this.m_VertexCount = reader.ReadUInt32();
			if (version[0] >= 4)
			{
				int m_ChannelsSize = reader.ReadInt32();
				this.m_Channels = new ChannelInfo[m_ChannelsSize];
				for (int i = 0; i < m_ChannelsSize; i++)
				{
					this.m_Channels[i] = new ChannelInfo(reader);
				}
			}
			if (version[0] < 5)
			{
				if (version[0] < 4)
				{
					this.m_Streams = new StreamInfo[4];
				}
				else
				{
					this.m_Streams = new StreamInfo[reader.ReadInt32()];
				}
				for (int j = 0; j < this.m_Streams.Length; j++)
				{
					this.m_Streams[j] = new StreamInfo(reader);
				}
				if (version[0] < 4)
				{
					this.GetChannels(version);
				}
			}
			else
			{
				this.GetStreams(version);
			}
			this.m_DataSize = reader.ReadUInt8Array();
			reader.AlignStream();
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000FA20 File Offset: 0x0000DC20
		private void GetStreams(int[] version)
		{
			int streamCount = (int)(this.m_Channels.Max((ChannelInfo x) => x.stream) + 1);
			this.m_Streams = new StreamInfo[streamCount];
			uint offset = 0U;
			for (int s = 0; s < streamCount; s++)
			{
				uint chnMask = 0U;
				uint stride = 0U;
				for (int chn = 0; chn < this.m_Channels.Length; chn++)
				{
					ChannelInfo m_Channel = this.m_Channels[chn];
					if ((int)m_Channel.stream == s && m_Channel.dimension > 0)
					{
						chnMask |= 1U << chn;
						stride += (uint)m_Channel.dimension * MeshHelper.GetFormatSize(MeshHelper.ToVertexFormat((int)m_Channel.format, version));
					}
				}
				this.m_Streams[s] = new StreamInfo
				{
					channelMask = chnMask,
					offset = offset,
					stride = stride,
					dividerOp = 0,
					frequency = 0
				};
				offset += this.m_VertexCount * stride;
				offset = (offset + 15U) & 4294967280U;
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000FB24 File Offset: 0x0000DD24
		private void GetChannels(int[] version)
		{
			this.m_Channels = new ChannelInfo[6];
			for (int i = 0; i < 6; i++)
			{
				this.m_Channels[i] = new ChannelInfo();
			}
			for (int s = 0; s < this.m_Streams.Length; s++)
			{
				StreamInfo m_Stream = this.m_Streams[s];
				BitArray channelMask = new BitArray(new int[] { (int)m_Stream.channelMask });
				byte offset = 0;
				for (int j = 0; j < 6; j++)
				{
					if (channelMask.Get(j))
					{
						ChannelInfo m_Channel = this.m_Channels[j];
						m_Channel.stream = (byte)s;
						m_Channel.offset = offset;
						switch (j)
						{
						case 0:
						case 1:
							m_Channel.format = 0;
							m_Channel.dimension = 3;
							break;
						case 2:
							m_Channel.format = 2;
							m_Channel.dimension = 4;
							break;
						case 3:
						case 4:
							m_Channel.format = 0;
							m_Channel.dimension = 2;
							break;
						case 5:
							m_Channel.format = 0;
							m_Channel.dimension = 4;
							break;
						}
						offset += (byte)((uint)m_Channel.dimension * MeshHelper.GetFormatSize(MeshHelper.ToVertexFormat((int)m_Channel.format, version)));
					}
				}
			}
		}

		// Token: 0x040006F7 RID: 1783
		public uint m_CurrentChannels;

		// Token: 0x040006F8 RID: 1784
		public uint m_VertexCount;

		// Token: 0x040006F9 RID: 1785
		public ChannelInfo[] m_Channels;

		// Token: 0x040006FA RID: 1786
		public StreamInfo[] m_Streams;

		// Token: 0x040006FB RID: 1787
		public byte[] m_DataSize;
	}
}
