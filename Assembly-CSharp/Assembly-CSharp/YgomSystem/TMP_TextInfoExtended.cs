using System;
using TMPro;
using UnityEngine;

namespace YgomSystem
{
	// Token: 0x020004DF RID: 1247
	public static class TMP_TextInfoExtended
	{
		// Token: 0x060027CF RID: 10191 RVA: 0x000F19DC File Offset: 0x000EFBDC
		public static Vector3 GetMeshVertex(this TMP_TextInfo tmpinfo, int charaindex, TMP_TextInfoExtended.VERTEXINDEX vertindex)
		{
			return default(Vector3);
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetMeshVertex(this TMP_TextInfo tmpinfo, int charaindex, TMP_TextInfoExtended.VERTEXINDEX vertindex, Vector3 vertex)
		{
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float GetMeshHeight(this TMP_TextInfo tmpinfo, int charaindex)
		{
			return 0f;
		}

		// Token: 0x020004E0 RID: 1248
		public enum VERTEXINDEX
		{
			// Token: 0x04002890 RID: 10384
			LEFT_BOTTOM,
			// Token: 0x04002891 RID: 10385
			LEFT_TOP,
			// Token: 0x04002892 RID: 10386
			RIGHT_TOP,
			// Token: 0x04002893 RID: 10387
			RIGHT_BOTTOM
		}
	}
}
