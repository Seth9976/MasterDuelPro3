using System;

namespace AssetStudio
{
	// Token: 0x0200013E RID: 318
	public sealed class TextAsset : NamedObject
	{
		// Token: 0x06000395 RID: 917 RVA: 0x00013CF9 File Offset: 0x00011EF9
		public TextAsset(ObjectReader reader)
			: base(reader)
		{
			this.m_Script = reader.ReadUInt8Array();
		}

		// Token: 0x040008B9 RID: 2233
		public byte[] m_Script;
	}
}
