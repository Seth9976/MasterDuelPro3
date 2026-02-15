using System;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x0200055D RID: 1373
	public class UVScroll : MonoBehaviour
	{
		// Token: 0x06002BDF RID: 11231 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x04002A63 RID: 10851
		public float offsetU;

		// Token: 0x04002A64 RID: 10852
		public float offsetV;

		// Token: 0x04002A65 RID: 10853
		public bool isShared;

		// Token: 0x04002A66 RID: 10854
		private Renderer rend;

		// Token: 0x04002A67 RID: 10855
		private int propMainTex;
	}
}
