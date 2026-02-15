using System;

namespace AssetStudio
{
	// Token: 0x020000AF RID: 175
	public class ConstantClip
	{
		// Token: 0x060002EA RID: 746 RVA: 0x0000D1EF File Offset: 0x0000B3EF
		public ConstantClip(ObjectReader reader)
		{
			this.data = reader.ReadSingleArray();
		}

		// Token: 0x0400058D RID: 1421
		public float[] data;
	}
}
