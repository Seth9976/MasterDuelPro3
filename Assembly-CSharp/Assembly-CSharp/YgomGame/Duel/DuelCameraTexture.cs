using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000D2B RID: 3371
	public class DuelCameraTexture : MonoBehaviour
	{
		// Token: 0x060061CF RID: 25039 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Initialize(Camera camera)
		{
		}

		// Token: 0x060061D0 RID: 25040 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x060061D1 RID: 25041 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x04009CC4 RID: 40132
		private const string BLURMATERIALPATH = "Duel/Models/Materials/UIBlur";

		// Token: 0x04009CC5 RID: 40133
		private const string BLURMATERIALPATH2 = "Duel/Timeline/Materials/UIBlur00";

		// Token: 0x04009CC6 RID: 40134
		public static RenderTexture renderTexture;

		// Token: 0x04009CC7 RID: 40135
		private static int m_RefCounrt;
	}
}
