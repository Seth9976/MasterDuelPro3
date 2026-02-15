using System;
using System.Collections.Generic;

namespace AssetStudio
{
	// Token: 0x020000D2 RID: 210
	public sealed class AssetBundle : NamedObject
	{
		// Token: 0x06000310 RID: 784 RVA: 0x0000E8F0 File Offset: 0x0000CAF0
		public AssetBundle(ObjectReader reader)
			: base(reader)
		{
			int m_PreloadTableSize = reader.ReadInt32();
			this.m_PreloadTable = new PPtr<Object>[m_PreloadTableSize];
			for (int i = 0; i < m_PreloadTableSize; i++)
			{
				this.m_PreloadTable[i] = new PPtr<Object>(reader);
			}
			int m_ContainerSize = reader.ReadInt32();
			this.m_Container = new KeyValuePair<string, AssetInfo>[m_ContainerSize];
			for (int j = 0; j < m_ContainerSize; j++)
			{
				this.m_Container[j] = new KeyValuePair<string, AssetInfo>(reader.ReadAlignedString(), new AssetInfo(reader));
			}
		}

		// Token: 0x04000648 RID: 1608
		public PPtr<Object>[] m_PreloadTable;

		// Token: 0x04000649 RID: 1609
		public KeyValuePair<string, AssetInfo>[] m_Container;
	}
}
