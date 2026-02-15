using System;
using System.Collections.Specialized;

namespace AssetStudio
{
	// Token: 0x02000104 RID: 260
	public class Object
	{
		// Token: 0x06000352 RID: 850 RVA: 0x0001196C File Offset: 0x0000FB6C
		public Object(ObjectReader reader)
		{
			this.reader = reader;
			reader.Reset();
			this.assetsFile = reader.assetsFile;
			this.type = reader.type;
			this.m_PathID = reader.m_PathID;
			this.version = reader.version;
			this.buildType = reader.buildType;
			this.platform = reader.platform;
			this.serializedType = reader.serializedType;
			this.byteSize = reader.byteSize;
			if (this.platform == BuildTarget.NoTarget)
			{
				reader.ReadUInt32();
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x000119FD File Offset: 0x0000FBFD
		public string Dump()
		{
			SerializedType serializedType = this.serializedType;
			if (((serializedType != null) ? serializedType.m_Type : null) != null)
			{
				return TypeTreeHelper.ReadTypeString(this.serializedType.m_Type, this.reader);
			}
			return null;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00011A2B File Offset: 0x0000FC2B
		public string Dump(TypeTree m_Type)
		{
			if (m_Type != null)
			{
				return TypeTreeHelper.ReadTypeString(m_Type, this.reader);
			}
			return null;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00011A3E File Offset: 0x0000FC3E
		public OrderedDictionary ToType()
		{
			SerializedType serializedType = this.serializedType;
			if (((serializedType != null) ? serializedType.m_Type : null) != null)
			{
				return TypeTreeHelper.ReadType(this.serializedType.m_Type, this.reader);
			}
			return null;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00011A6C File Offset: 0x0000FC6C
		public OrderedDictionary ToType(TypeTree m_Type)
		{
			if (m_Type != null)
			{
				return TypeTreeHelper.ReadType(m_Type, this.reader);
			}
			return null;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00011A7F File Offset: 0x0000FC7F
		public byte[] GetRawData()
		{
			this.reader.Reset();
			return this.reader.ReadBytes((int)this.byteSize);
		}

		// Token: 0x04000761 RID: 1889
		public SerializedFile assetsFile;

		// Token: 0x04000762 RID: 1890
		public ObjectReader reader;

		// Token: 0x04000763 RID: 1891
		public long m_PathID;

		// Token: 0x04000764 RID: 1892
		public int[] version;

		// Token: 0x04000765 RID: 1893
		protected BuildType buildType;

		// Token: 0x04000766 RID: 1894
		public BuildTarget platform;

		// Token: 0x04000767 RID: 1895
		public ClassIDType type;

		// Token: 0x04000768 RID: 1896
		public SerializedType serializedType;

		// Token: 0x04000769 RID: 1897
		public uint byteSize;
	}
}
