using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02001178 RID: 4472
	public class TextVertexGradation : BaseMeshEffect
	{
		// Token: 0x060084BA RID: 33978 RVA: 0x0000216D File Offset: 0x0000036D
		public void ModifyVertices(List<UIVertex> verts)
		{
		}

		// Token: 0x060084BB RID: 33979 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ModifyMesh(VertexHelper vh)
		{
		}

		// Token: 0x0400C041 RID: 49217
		[SerializeField]
		public Color color;

		// Token: 0x0400C042 RID: 49218
		[SerializeField]
		public TextVertexGradation.Direction direction;

		// Token: 0x0400C043 RID: 49219
		private const int VERTLEN = 6;

		// Token: 0x0400C044 RID: 49220
		private int[] gradBit;

		// Token: 0x02001179 RID: 4473
		public enum Direction
		{
			// Token: 0x0400C046 RID: 49222
			UpToBottom,
			// Token: 0x0400C047 RID: 49223
			BottomToUp,
			// Token: 0x0400C048 RID: 49224
			LeftToRight,
			// Token: 0x0400C049 RID: 49225
			RightToLeft
		}
	}
}
