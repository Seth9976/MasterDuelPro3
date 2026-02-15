using System;

namespace AssetStudio
{
	// Token: 0x02000175 RID: 373
	public class ObjectReader : EndianBinaryReader
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0001845A File Offset: 0x0001665A
		public int[] version
		{
			get
			{
				return this.assetsFile.version;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00018467 File Offset: 0x00016667
		public BuildType buildType
		{
			get
			{
				return this.assetsFile.buildType;
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00018474 File Offset: 0x00016674
		public ObjectReader(EndianBinaryReader reader, SerializedFile assetsFile, ObjectInfo objectInfo)
			: base(reader.BaseStream, reader.Endian)
		{
			this.assetsFile = assetsFile;
			this.m_PathID = objectInfo.m_PathID;
			this.byteStart = objectInfo.byteStart;
			this.byteSize = objectInfo.byteSize;
			if (Enum.IsDefined(typeof(ClassIDType), objectInfo.classID))
			{
				this.type = (ClassIDType)objectInfo.classID;
			}
			else
			{
				this.type = ClassIDType.UnknownType;
			}
			this.serializedType = objectInfo.serializedType;
			this.platform = assetsFile.m_TargetPlatform;
			this.m_Version = assetsFile.header.m_Version;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00018518 File Offset: 0x00016718
		public void Reset()
		{
			base.Position = this.byteStart;
		}

		// Token: 0x040009C4 RID: 2500
		public SerializedFile assetsFile;

		// Token: 0x040009C5 RID: 2501
		public long m_PathID;

		// Token: 0x040009C6 RID: 2502
		public long byteStart;

		// Token: 0x040009C7 RID: 2503
		public uint byteSize;

		// Token: 0x040009C8 RID: 2504
		public ClassIDType type;

		// Token: 0x040009C9 RID: 2505
		public SerializedType serializedType;

		// Token: 0x040009CA RID: 2506
		public BuildTarget platform;

		// Token: 0x040009CB RID: 2507
		public SerializedFileFormatVersion m_Version;
	}
}
