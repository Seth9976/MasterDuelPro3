using System;

namespace AssetStudio
{
	// Token: 0x020000E9 RID: 233
	public class UnityTexEnv
	{
		// Token: 0x06000327 RID: 807 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
		public UnityTexEnv(ObjectReader reader)
		{
			this.m_Texture = new PPtr<Texture>(reader);
			this.m_Scale = reader.ReadVector2();
			this.m_Offset = reader.ReadVector2();
		}

		// Token: 0x040006D5 RID: 1749
		public PPtr<Texture> m_Texture;

		// Token: 0x040006D6 RID: 1750
		public Vector2 m_Scale;

		// Token: 0x040006D7 RID: 1751
		public Vector2 m_Offset;
	}
}
