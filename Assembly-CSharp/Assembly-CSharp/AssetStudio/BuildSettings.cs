using System;

namespace AssetStudio
{
	// Token: 0x020000E4 RID: 228
	public sealed class BuildSettings : Object
	{
		// Token: 0x06000322 RID: 802 RVA: 0x0000F199 File Offset: 0x0000D399
		public BuildSettings(ObjectReader reader)
			: base(reader)
		{
			reader.ReadStringArray();
			reader.ReadBoolean();
			reader.ReadBoolean();
			reader.ReadBoolean();
			reader.ReadBoolean();
			this.m_Version = reader.ReadAlignedString();
		}

		// Token: 0x040006CA RID: 1738
		public string m_Version;
	}
}
