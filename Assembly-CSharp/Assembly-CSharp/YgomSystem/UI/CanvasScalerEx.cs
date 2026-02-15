using System;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x0200057B RID: 1403
	public class CanvasScalerEx : CanvasScaler
	{
		// Token: 0x06002C97 RID: 11415 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnEnable()
		{
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x0000216D File Offset: 0x0000036D
		private void CalcScale()
		{
		}

		// Token: 0x04002ACA RID: 10954
		private Rect camPixelRext;

		// Token: 0x04002ACB RID: 10955
		private Vector2 orgResolution;

		// Token: 0x04002ACC RID: 10956
		private Canvas canvas;

		// Token: 0x04002ACD RID: 10957
		private bool isInit;
	}
}
