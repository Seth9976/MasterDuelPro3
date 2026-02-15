using System;

namespace AssetStudio
{
	// Token: 0x020000DD RID: 221
	public class Handle
	{
		// Token: 0x06000319 RID: 793 RVA: 0x0000ED94 File Offset: 0x0000CF94
		public Handle(ObjectReader reader)
		{
			this.m_X = new xform(reader);
			this.m_ParentHumanIndex = reader.ReadUInt32();
			this.m_ID = reader.ReadUInt32();
		}

		// Token: 0x04000698 RID: 1688
		public xform m_X;

		// Token: 0x04000699 RID: 1689
		public uint m_ParentHumanIndex;

		// Token: 0x0400069A RID: 1690
		public uint m_ID;
	}
}
