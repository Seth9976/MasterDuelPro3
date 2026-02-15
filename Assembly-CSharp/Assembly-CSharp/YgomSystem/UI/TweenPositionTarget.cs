using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000629 RID: 1577
	public class TweenPositionTarget : Tween
	{
		// Token: 0x060031D8 RID: 12760 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x04002E56 RID: 11862
		private RectTransform rtrans;

		// Token: 0x04002E57 RID: 11863
		[SerializeField]
		public Vector3 from;

		// Token: 0x04002E58 RID: 11864
		[SerializeField]
		public Vector3 to;

		// Token: 0x04002E59 RID: 11865
		[SerializeField]
		public bool updateX;

		// Token: 0x04002E5A RID: 11866
		[SerializeField]
		public bool updateY;

		// Token: 0x04002E5B RID: 11867
		[SerializeField]
		public bool updateZ;
	}
}
