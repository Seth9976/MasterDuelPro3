using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02001175 RID: 4469
	public class OutlineDouble : Shadow
	{
		// Token: 0x060084A9 RID: 33961 RVA: 0x000F751A File Offset: 0x000F571A
		protected OutlineDouble()
		{
		}

		// Token: 0x060084AA RID: 33962 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ModifyMesh(VertexHelper vh)
		{
		}

		// Token: 0x060084AB RID: 33963 RVA: 0x000029CC File Offset: 0x00000BCC
		private int addOutlineVerts(List<UIVertex> verts, int start, Color color, Vector2 ofs, Vector2 dist)
		{
			return 0;
		}

		// Token: 0x0400C030 RID: 49200
		public Vector2 offset;

		// Token: 0x0400C031 RID: 49201
		public Color effectColor2;

		// Token: 0x0400C032 RID: 49202
		public Vector2 effectDistance2;
	}
}
