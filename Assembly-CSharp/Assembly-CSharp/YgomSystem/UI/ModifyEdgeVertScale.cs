using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x020005A6 RID: 1446
	[ExecuteAlways]
	public class ModifyEdgeVertScale : BaseMeshEffect
	{
		// Token: 0x06002DD5 RID: 11733 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ModifyMesh(VertexHelper vh)
		{
		}

		// Token: 0x04002B8E RID: 11150
		[SerializeField]
		private Vector2 m_UpperLeft;

		// Token: 0x04002B8F RID: 11151
		[SerializeField]
		private Vector2 m_UpperRight;

		// Token: 0x04002B90 RID: 11152
		[SerializeField]
		private Vector2 m_LowerLeft;

		// Token: 0x04002B91 RID: 11153
		[SerializeField]
		private Vector2 m_LowerRight;

		// Token: 0x04002B92 RID: 11154
		private List<UIVertex> m_VertsCache;
	}
}
