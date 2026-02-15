using System;
using System.Collections.Generic;
using System.Linq;

namespace AssetStudio
{
	// Token: 0x020000E1 RID: 225
	public sealed class Avatar : NamedObject
	{
		// Token: 0x0600031D RID: 797 RVA: 0x0000F0CC File Offset: 0x0000D2CC
		public Avatar(ObjectReader reader)
			: base(reader)
		{
			this.m_AvatarSize = reader.ReadUInt32();
			this.m_Avatar = new AvatarConstant(reader);
			int numTOS = reader.ReadInt32();
			this.m_TOS = new KeyValuePair<uint, string>[numTOS];
			for (int i = 0; i < numTOS; i++)
			{
				this.m_TOS[i] = new KeyValuePair<uint, string>(reader.ReadUInt32(), reader.ReadAlignedString());
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000F134 File Offset: 0x0000D334
		public string FindBonePath(uint hash)
		{
			return this.m_TOS.FirstOrDefault((KeyValuePair<uint, string> pair) => pair.Key == hash).Value;
		}

		// Token: 0x040006C5 RID: 1733
		public uint m_AvatarSize;

		// Token: 0x040006C6 RID: 1734
		public AvatarConstant m_Avatar;

		// Token: 0x040006C7 RID: 1735
		public KeyValuePair<uint, string>[] m_TOS;
	}
}
