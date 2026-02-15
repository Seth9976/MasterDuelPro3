using System;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x02000623 RID: 1571
	public class TweenImage : Tween
	{
		// Token: 0x060031C8 RID: 12744 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x04002E48 RID: 11848
		[SerializeField]
		public Sprite[] frames;

		// Token: 0x04002E49 RID: 11849
		private Image image;
	}
}
