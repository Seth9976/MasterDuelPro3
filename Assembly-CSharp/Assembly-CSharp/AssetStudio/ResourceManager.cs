using System;
using System.Collections.Generic;

namespace AssetStudio
{
	// Token: 0x0200010C RID: 268
	public class ResourceManager : Object
	{
		// Token: 0x06000367 RID: 871 RVA: 0x00012250 File Offset: 0x00010450
		public ResourceManager(ObjectReader reader)
			: base(reader)
		{
			int m_ContainerSize = reader.ReadInt32();
			this.m_Container = new KeyValuePair<string, PPtr<Object>>[m_ContainerSize];
			for (int i = 0; i < m_ContainerSize; i++)
			{
				this.m_Container[i] = new KeyValuePair<string, PPtr<Object>>(reader.ReadAlignedString(), new PPtr<Object>(reader));
			}
		}

		// Token: 0x04000777 RID: 1911
		public KeyValuePair<string, PPtr<Object>>[] m_Container;
	}
}
