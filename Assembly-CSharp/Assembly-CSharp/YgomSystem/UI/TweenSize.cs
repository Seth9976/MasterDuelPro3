using System;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x0200063C RID: 1596
	public class TweenSize : Tween
	{
		// Token: 0x06003205 RID: 12805 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x04002E9F RID: 11935
		private RectTransform rtrans;

		// Token: 0x04002EA0 RID: 11936
		[SerializeField]
		private Vector2 from;

		// Token: 0x04002EA1 RID: 11937
		[SerializeField]
		public Vector2 to;

		// Token: 0x04002EA2 RID: 11938
		[SerializeField]
		public bool ignoreWidth;

		// Token: 0x04002EA3 RID: 11939
		[SerializeField]
		public bool ignoreHeight;

		// Token: 0x04002EA4 RID: 11940
		private LayoutElement layout;
	}
}
