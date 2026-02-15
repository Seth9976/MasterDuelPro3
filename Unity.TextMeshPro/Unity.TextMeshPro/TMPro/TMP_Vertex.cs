using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000017 RID: 23
	public struct TMP_Vertex
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public static TMP_Vertex zero
		{
			get
			{
				return TMP_Vertex.k_Zero;
			}
		}

		// Token: 0x04000039 RID: 57
		public Vector3 position;

		// Token: 0x0400003A RID: 58
		public Vector4 uv;

		// Token: 0x0400003B RID: 59
		public Vector2 uv2;

		// Token: 0x0400003C RID: 60
		public Color32 color;

		// Token: 0x0400003D RID: 61
		private static readonly TMP_Vertex k_Zero;
	}
}
