using System;

namespace AssetStudio
{
	// Token: 0x0200009A RID: 154
	public class Keyframe<T>
	{
		// Token: 0x060002CF RID: 719 RVA: 0x0000C59C File Offset: 0x0000A79C
		public Keyframe(ObjectReader reader, Func<T> readerFunc)
		{
			this.time = reader.ReadSingle();
			this.value = readerFunc();
			this.inSlope = readerFunc();
			this.outSlope = readerFunc();
			if (reader.version[0] >= 2018)
			{
				this.weightedMode = reader.ReadInt32();
				this.inWeight = readerFunc();
				this.outWeight = readerFunc();
			}
		}

		// Token: 0x0400053A RID: 1338
		public float time;

		// Token: 0x0400053B RID: 1339
		public T value;

		// Token: 0x0400053C RID: 1340
		public T inSlope;

		// Token: 0x0400053D RID: 1341
		public T outSlope;

		// Token: 0x0400053E RID: 1342
		public int weightedMode;

		// Token: 0x0400053F RID: 1343
		public T inWeight;

		// Token: 0x04000540 RID: 1344
		public T outWeight;
	}
}
