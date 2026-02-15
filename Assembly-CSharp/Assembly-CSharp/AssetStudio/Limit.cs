using System;

namespace AssetStudio
{
	// Token: 0x020000D8 RID: 216
	public class Limit
	{
		// Token: 0x06000314 RID: 788 RVA: 0x0000EBC4 File Offset: 0x0000CDC4
		public Limit(ObjectReader reader)
		{
			int[] version = reader.version;
			if (version[0] > 5 || (version[0] == 5 && version[1] >= 4))
			{
				this.m_Min = reader.ReadVector3();
				this.m_Max = reader.ReadVector3();
				return;
			}
			this.m_Min = reader.ReadVector4();
			this.m_Max = reader.ReadVector4();
		}

		// Token: 0x0400068B RID: 1675
		public object m_Min;

		// Token: 0x0400068C RID: 1676
		public object m_Max;
	}
}
