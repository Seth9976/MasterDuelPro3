using System;

namespace AssetStudio
{
	// Token: 0x020000DC RID: 220
	public class Hand
	{
		// Token: 0x06000318 RID: 792 RVA: 0x0000ED80 File Offset: 0x0000CF80
		public Hand(ObjectReader reader)
		{
			this.m_HandBoneIndex = reader.ReadInt32Array();
		}

		// Token: 0x04000697 RID: 1687
		public int[] m_HandBoneIndex;
	}
}
