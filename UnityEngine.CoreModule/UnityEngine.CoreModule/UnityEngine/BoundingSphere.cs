using System;

namespace UnityEngine
{
	// Token: 0x020000B0 RID: 176
	public struct BoundingSphere
	{
		// Token: 0x06000453 RID: 1107 RVA: 0x00008D8B File Offset: 0x00006F8B
		public BoundingSphere(Vector3 pos, float rad)
		{
			this.position = pos;
			this.radius = rad;
		}

		// Token: 0x0400022E RID: 558
		public Vector3 position;

		// Token: 0x0400022F RID: 559
		public float radius;
	}
}
