using System;

namespace AssetStudio
{
	// Token: 0x020000B0 RID: 176
	public class ValueConstant
	{
		// Token: 0x060002EB RID: 747 RVA: 0x0000D204 File Offset: 0x0000B404
		public ValueConstant(ObjectReader reader)
		{
			int[] version = reader.version;
			this.m_ID = reader.ReadUInt32();
			if (version[0] < 5 || (version[0] == 5 && version[1] < 5))
			{
				this.m_TypeID = reader.ReadUInt32();
			}
			this.m_Type = reader.ReadUInt32();
			this.m_Index = reader.ReadUInt32();
		}

		// Token: 0x0400058E RID: 1422
		public uint m_ID;

		// Token: 0x0400058F RID: 1423
		public uint m_TypeID;

		// Token: 0x04000590 RID: 1424
		public uint m_Type;

		// Token: 0x04000591 RID: 1425
		public uint m_Index;
	}
}
