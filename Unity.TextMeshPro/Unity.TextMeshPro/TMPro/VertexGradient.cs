using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x020000AB RID: 171
	[Serializable]
	public struct VertexGradient
	{
		// Token: 0x06000641 RID: 1601 RVA: 0x0002EB58 File Offset: 0x0002CD58
		public VertexGradient(Color color)
		{
			this.topLeft = color;
			this.topRight = color;
			this.bottomLeft = color;
			this.bottomRight = color;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0002EB76 File Offset: 0x0002CD76
		public VertexGradient(Color color0, Color color1, Color color2, Color color3)
		{
			this.topLeft = color0;
			this.topRight = color1;
			this.bottomLeft = color2;
			this.bottomRight = color3;
		}

		// Token: 0x040005BE RID: 1470
		public Color topLeft;

		// Token: 0x040005BF RID: 1471
		public Color topRight;

		// Token: 0x040005C0 RID: 1472
		public Color bottomLeft;

		// Token: 0x040005C1 RID: 1473
		public Color bottomRight;
	}
}
