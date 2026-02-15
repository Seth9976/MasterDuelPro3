using System;

namespace AssetStudio
{
	// Token: 0x020000F2 RID: 242
	public class BoneWeights4
	{
		// Token: 0x06000336 RID: 822 RVA: 0x0000FC6C File Offset: 0x0000DE6C
		public BoneWeights4()
		{
			this.weight = new float[4];
			this.boneIndex = new int[4];
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000FC8C File Offset: 0x0000DE8C
		public BoneWeights4(ObjectReader reader)
		{
			this.weight = reader.ReadSingleArray(4);
			this.boneIndex = reader.ReadInt32Array(4);
		}

		// Token: 0x040006FE RID: 1790
		public float[] weight;

		// Token: 0x040006FF RID: 1791
		public int[] boneIndex;
	}
}
