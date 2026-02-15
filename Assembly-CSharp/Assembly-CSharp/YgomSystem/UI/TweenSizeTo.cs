using System;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x0200063D RID: 1597
	public class TweenSizeTo : Tween
	{
		// Token: 0x06003208 RID: 12808 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x04002EA5 RID: 11941
		private RectTransform rtrans;

		// Token: 0x04002EA6 RID: 11942
		private Vector2 from;

		// Token: 0x04002EA7 RID: 11943
		[SerializeField]
		public Vector2 to;

		// Token: 0x04002EA8 RID: 11944
		[SerializeField]
		public bool ignoreWidth;

		// Token: 0x04002EA9 RID: 11945
		[SerializeField]
		public bool ignoreHeight;

		// Token: 0x04002EAA RID: 11946
		private LayoutElement layout;
	}
}
