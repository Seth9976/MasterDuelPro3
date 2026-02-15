using System;

namespace AssetStudio
{
	// Token: 0x020000C5 RID: 197
	public class BlendDirectDataConstant
	{
		// Token: 0x06000303 RID: 771 RVA: 0x0000E0B8 File Offset: 0x0000C2B8
		public BlendDirectDataConstant(ObjectReader reader)
		{
			this.m_ChildBlendEventIDArray = reader.ReadUInt32Array();
			this.m_NormalizedBlendValues = reader.ReadBoolean();
			reader.AlignStream();
		}

		// Token: 0x04000609 RID: 1545
		public uint[] m_ChildBlendEventIDArray;

		// Token: 0x0400060A RID: 1546
		public bool m_NormalizedBlendValues;
	}
}
