using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x02000115 RID: 277
	public class SerializedProperties
	{
		// Token: 0x0600036E RID: 878 RVA: 0x000123F4 File Offset: 0x000105F4
		public SerializedProperties(BinaryReader reader)
		{
			int numProps = reader.ReadInt32();
			this.m_Props = new SerializedProperty[numProps];
			for (int i = 0; i < numProps; i++)
			{
				this.m_Props[i] = new SerializedProperty(reader);
			}
		}

		// Token: 0x04000796 RID: 1942
		public SerializedProperty[] m_Props;
	}
}
