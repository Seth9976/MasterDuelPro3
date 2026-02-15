using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000627 RID: 1575
	public class TweenPosition : Tween
	{
		// Token: 0x060031D4 RID: 12756 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x04002E51 RID: 11857
		private RectTransform rtrans;

		// Token: 0x04002E52 RID: 11858
		[SerializeField]
		public Vector3 from;

		// Token: 0x04002E53 RID: 11859
		[SerializeField]
		public Vector3 to;
	}
}
