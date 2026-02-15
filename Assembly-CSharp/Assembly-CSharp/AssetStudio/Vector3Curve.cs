using System;

namespace AssetStudio
{
	// Token: 0x020000A1 RID: 161
	public class Vector3Curve
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x0000CB5B File Offset: 0x0000AD5B
		public Vector3Curve(ObjectReader reader)
		{
			this.curve = new AnimationCurve<Vector3>(reader, new Func<Vector3>(reader.ReadVector3));
			this.path = reader.ReadAlignedString();
		}

		// Token: 0x04000557 RID: 1367
		public AnimationCurve<Vector3> curve;

		// Token: 0x04000558 RID: 1368
		public string path;
	}
}
