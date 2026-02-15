using System;

namespace AssetStudio
{
	// Token: 0x020000D4 RID: 212
	public sealed class AudioClip : NamedObject
	{
		// Token: 0x06000312 RID: 786 RVA: 0x0000E990 File Offset: 0x0000CB90
		public AudioClip(ObjectReader reader)
			: base(reader)
		{
			if (this.version[0] < 5)
			{
				this.m_Format = reader.ReadInt32();
				this.m_Type = (FMODSoundType)reader.ReadInt32();
				this.m_3D = reader.ReadBoolean();
				this.m_UseHardware = reader.ReadBoolean();
				reader.AlignStream();
				if (this.version[0] >= 4 || (this.version[0] == 3 && this.version[1] >= 2))
				{
					reader.ReadInt32();
					this.m_Size = (long)reader.ReadInt32();
					long tsize = ((this.m_Size % 4L != 0L) ? (this.m_Size + 4L - this.m_Size % 4L) : this.m_Size);
					if ((ulong)reader.byteSize + (ulong)reader.byteStart - (ulong)reader.Position != (ulong)tsize)
					{
						this.m_Offset = (long)((ulong)reader.ReadUInt32());
						this.m_Source = this.assetsFile.fullName + ".resS";
					}
				}
				else
				{
					this.m_Size = (long)reader.ReadInt32();
				}
			}
			else
			{
				this.m_LoadType = reader.ReadInt32();
				this.m_Channels = reader.ReadInt32();
				this.m_Frequency = reader.ReadInt32();
				this.m_BitsPerSample = reader.ReadInt32();
				this.m_Length = reader.ReadSingle();
				this.m_IsTrackerFormat = reader.ReadBoolean();
				reader.AlignStream();
				this.m_SubsoundIndex = reader.ReadInt32();
				this.m_PreloadAudioData = reader.ReadBoolean();
				this.m_LoadInBackground = reader.ReadBoolean();
				this.m_Legacy3D = reader.ReadBoolean();
				reader.AlignStream();
				this.m_Source = reader.ReadAlignedString();
				this.m_Offset = reader.ReadInt64();
				this.m_Size = reader.ReadInt64();
				this.m_CompressionFormat = (AudioCompressionFormat)reader.ReadInt32();
			}
			ResourceReader resourceReader;
			if (!string.IsNullOrEmpty(this.m_Source))
			{
				resourceReader = new ResourceReader(this.m_Source, this.assetsFile, this.m_Offset, this.m_Size);
			}
			else
			{
				resourceReader = new ResourceReader(reader, reader.BaseStream.Position, this.m_Size);
			}
			this.m_AudioData = resourceReader;
		}

		// Token: 0x0400064C RID: 1612
		public int m_Format;

		// Token: 0x0400064D RID: 1613
		public FMODSoundType m_Type;

		// Token: 0x0400064E RID: 1614
		public bool m_3D;

		// Token: 0x0400064F RID: 1615
		public bool m_UseHardware;

		// Token: 0x04000650 RID: 1616
		public int m_LoadType;

		// Token: 0x04000651 RID: 1617
		public int m_Channels;

		// Token: 0x04000652 RID: 1618
		public int m_Frequency;

		// Token: 0x04000653 RID: 1619
		public int m_BitsPerSample;

		// Token: 0x04000654 RID: 1620
		public float m_Length;

		// Token: 0x04000655 RID: 1621
		public bool m_IsTrackerFormat;

		// Token: 0x04000656 RID: 1622
		public int m_SubsoundIndex;

		// Token: 0x04000657 RID: 1623
		public bool m_PreloadAudioData;

		// Token: 0x04000658 RID: 1624
		public bool m_LoadInBackground;

		// Token: 0x04000659 RID: 1625
		public bool m_Legacy3D;

		// Token: 0x0400065A RID: 1626
		public AudioCompressionFormat m_CompressionFormat;

		// Token: 0x0400065B RID: 1627
		public string m_Source;

		// Token: 0x0400065C RID: 1628
		public long m_Offset;

		// Token: 0x0400065D RID: 1629
		public long m_Size;

		// Token: 0x0400065E RID: 1630
		public ResourceReader m_AudioData;
	}
}
