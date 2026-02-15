using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x020003EA RID: 1002
	public struct Cookie
	{
		// Token: 0x06001B21 RID: 6945 RVA: 0x0003BB44 File Offset: 0x00039D44
		public static Cookie Defaults()
		{
			Cookie c;
			c.instanceID = 0;
			c.scale = 1f;
			c.sizes = new Vector2(1f, 1f);
			return c;
		}

		// Token: 0x04000D6A RID: 3434
		public int instanceID;

		// Token: 0x04000D6B RID: 3435
		public float scale;

		// Token: 0x04000D6C RID: 3436
		public Vector2 sizes;
	}
}
