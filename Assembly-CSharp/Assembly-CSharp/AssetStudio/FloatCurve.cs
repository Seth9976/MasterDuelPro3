using System;

namespace AssetStudio
{
	// Token: 0x020000A2 RID: 162
	public class FloatCurve
	{
		// Token: 0x060002DA RID: 730 RVA: 0x0000CB88 File Offset: 0x0000AD88
		public FloatCurve(ObjectReader reader)
		{
			this.curve = new AnimationCurve<float>(reader, new Func<float>(reader.ReadSingle));
			this.attribute = reader.ReadAlignedString();
			this.path = reader.ReadAlignedString();
			this.classID = (ClassIDType)reader.ReadInt32();
			this.script = new PPtr<MonoScript>(reader);
		}

		// Token: 0x04000559 RID: 1369
		public AnimationCurve<float> curve;

		// Token: 0x0400055A RID: 1370
		public string attribute;

		// Token: 0x0400055B RID: 1371
		public string path;

		// Token: 0x0400055C RID: 1372
		public ClassIDType classID;

		// Token: 0x0400055D RID: 1373
		public PPtr<MonoScript> script;
	}
}
