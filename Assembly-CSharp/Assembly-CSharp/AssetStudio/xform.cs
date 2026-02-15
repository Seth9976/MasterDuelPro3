using System;

namespace AssetStudio
{
	// Token: 0x020000A6 RID: 166
	public class xform
	{
		// Token: 0x060002DE RID: 734 RVA: 0x0000CC94 File Offset: 0x0000AE94
		public xform(ObjectReader reader)
		{
			int[] version = reader.version;
			this.t = ((version[0] > 5 || (version[0] == 5 && version[1] >= 4)) ? reader.ReadVector3() : reader.ReadVector4());
			this.q = reader.ReadQuaternion();
			this.s = ((version[0] > 5 || (version[0] == 5 && version[1] >= 4)) ? reader.ReadVector3() : reader.ReadVector4());
		}

		// Token: 0x04000567 RID: 1383
		public Vector3 t;

		// Token: 0x04000568 RID: 1384
		public Quaternion q;

		// Token: 0x04000569 RID: 1385
		public Vector3 s;
	}
}
