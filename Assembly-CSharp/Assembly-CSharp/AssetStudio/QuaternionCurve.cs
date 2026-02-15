using System;

namespace AssetStudio
{
	// Token: 0x0200009C RID: 156
	public class QuaternionCurve
	{
		// Token: 0x060002D1 RID: 721 RVA: 0x0000C692 File Offset: 0x0000A892
		public QuaternionCurve(ObjectReader reader)
		{
			this.curve = new AnimationCurve<Quaternion>(reader, new Func<Quaternion>(reader.ReadQuaternion));
			this.path = reader.ReadAlignedString();
		}

		// Token: 0x04000545 RID: 1349
		public AnimationCurve<Quaternion> curve;

		// Token: 0x04000546 RID: 1350
		public string path;
	}
}
