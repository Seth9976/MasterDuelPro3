using System;

namespace AssetStudio
{
	// Token: 0x020000B7 RID: 183
	public class AnimationEvent
	{
		// Token: 0x060002F6 RID: 758 RVA: 0x0000D7F8 File Offset: 0x0000B9F8
		public AnimationEvent(ObjectReader reader)
		{
			int[] version = reader.version;
			this.time = reader.ReadSingle();
			this.functionName = reader.ReadAlignedString();
			this.data = reader.ReadAlignedString();
			this.objectReferenceParameter = new PPtr<Object>(reader);
			this.floatParameter = reader.ReadSingle();
			if (version[0] >= 3)
			{
				this.intParameter = reader.ReadInt32();
			}
			this.messageOptions = reader.ReadInt32();
		}

		// Token: 0x040005BF RID: 1471
		public float time;

		// Token: 0x040005C0 RID: 1472
		public string functionName;

		// Token: 0x040005C1 RID: 1473
		public string data;

		// Token: 0x040005C2 RID: 1474
		public PPtr<Object> objectReferenceParameter;

		// Token: 0x040005C3 RID: 1475
		public float floatParameter;

		// Token: 0x040005C4 RID: 1476
		public int intParameter;

		// Token: 0x040005C5 RID: 1477
		public int messageOptions;
	}
}
