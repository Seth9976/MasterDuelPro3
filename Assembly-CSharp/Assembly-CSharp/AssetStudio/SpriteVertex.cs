using System;

namespace AssetStudio
{
	// Token: 0x02000138 RID: 312
	public class SpriteVertex
	{
		// Token: 0x0600038F RID: 911 RVA: 0x00013724 File Offset: 0x00011924
		public SpriteVertex(ObjectReader reader)
		{
			int[] version = reader.version;
			this.pos = reader.ReadVector3();
			if (version[0] < 4 || (version[0] == 4 && version[1] <= 3))
			{
				this.uv = reader.ReadVector2();
			}
		}

		// Token: 0x0400088B RID: 2187
		public Vector3 pos;

		// Token: 0x0400088C RID: 2188
		public Vector2 uv;
	}
}
