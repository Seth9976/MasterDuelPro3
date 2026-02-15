using System;

namespace AssetStudio
{
	// Token: 0x020000A3 RID: 163
	public class PPtrKeyframe
	{
		// Token: 0x060002DB RID: 731 RVA: 0x0000CBE4 File Offset: 0x0000ADE4
		public PPtrKeyframe(ObjectReader reader)
		{
			this.time = reader.ReadSingle();
			this.value = new PPtr<Object>(reader);
		}

		// Token: 0x0400055E RID: 1374
		public float time;

		// Token: 0x0400055F RID: 1375
		public PPtr<Object> value;
	}
}
