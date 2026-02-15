using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000574 RID: 1396
	[ExecuteInEditMode]
	public class BokehCamera : MonoBehaviour
	{
		// Token: 0x06002C74 RID: 11380 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x0000216D File Offset: 0x0000036D
		public void BokeStart()
		{
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x0000216D File Offset: 0x0000036D
		public void BokeStop()
		{
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnRenderImage(RenderTexture src, RenderTexture dst)
		{
		}

		// Token: 0x04002AB6 RID: 10934
		public Shader shader;

		// Token: 0x04002AB7 RID: 10935
		private Material material;

		// Token: 0x04002AB8 RID: 10936
		private RenderTexture[] renderTexture;
	}
}
