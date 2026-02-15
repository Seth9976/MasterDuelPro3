using System;

namespace AssetStudio
{
	// Token: 0x020000E3 RID: 227
	public abstract class Behaviour : Component
	{
		// Token: 0x06000321 RID: 801 RVA: 0x0000F17E File Offset: 0x0000D37E
		protected Behaviour(ObjectReader reader)
			: base(reader)
		{
			this.m_Enabled = reader.ReadByte();
			reader.AlignStream();
		}

		// Token: 0x040006C9 RID: 1737
		public byte m_Enabled;
	}
}
