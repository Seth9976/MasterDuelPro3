using System;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x02000555 RID: 1365
	public class TransformRouter : MonoBehaviour
	{
		// Token: 0x06002BB7 RID: 11191 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x04002A41 RID: 10817
		public Transform routingTarget;

		// Token: 0x04002A42 RID: 10818
		public bool routingPosX;

		// Token: 0x04002A43 RID: 10819
		public bool routingPosY;

		// Token: 0x04002A44 RID: 10820
		public bool routingPosZ;

		// Token: 0x04002A45 RID: 10821
		public bool routingRotX;

		// Token: 0x04002A46 RID: 10822
		public bool routingRotY;

		// Token: 0x04002A47 RID: 10823
		public bool routingRotZ;

		// Token: 0x04002A48 RID: 10824
		public bool routingScaleX;

		// Token: 0x04002A49 RID: 10825
		public bool routingScaleY;

		// Token: 0x04002A4A RID: 10826
		public bool routingScaleZ;
	}
}
