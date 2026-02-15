using System;

namespace AssetStudio
{
	// Token: 0x020000A4 RID: 164
	public class PPtrCurve
	{
		// Token: 0x060002DC RID: 732 RVA: 0x0000CC04 File Offset: 0x0000AE04
		public PPtrCurve(ObjectReader reader)
		{
			int numCurves = reader.ReadInt32();
			this.curve = new PPtrKeyframe[numCurves];
			for (int i = 0; i < numCurves; i++)
			{
				this.curve[i] = new PPtrKeyframe(reader);
			}
			this.attribute = reader.ReadAlignedString();
			this.path = reader.ReadAlignedString();
			this.classID = reader.ReadInt32();
			this.script = new PPtr<MonoScript>(reader);
		}

		// Token: 0x04000560 RID: 1376
		public PPtrKeyframe[] curve;

		// Token: 0x04000561 RID: 1377
		public string attribute;

		// Token: 0x04000562 RID: 1378
		public string path;

		// Token: 0x04000563 RID: 1379
		public int classID;

		// Token: 0x04000564 RID: 1380
		public PPtr<MonoScript> script;
	}
}
