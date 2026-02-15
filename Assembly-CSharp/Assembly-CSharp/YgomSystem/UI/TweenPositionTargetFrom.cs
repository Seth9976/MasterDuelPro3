using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x0200062A RID: 1578
	public class TweenPositionTargetFrom : Tween
	{
		// Token: 0x060031DB RID: 12763 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x04002E5C RID: 11868
		private RectTransform rtrans;

		// Token: 0x04002E5D RID: 11869
		[SerializeField]
		public Vector3 from;

		// Token: 0x04002E5E RID: 11870
		[SerializeField]
		public bool updateX;

		// Token: 0x04002E5F RID: 11871
		[SerializeField]
		public bool updateY;

		// Token: 0x04002E60 RID: 11872
		[SerializeField]
		public bool updateZ;

		// Token: 0x04002E61 RID: 11873
		private Vector3 to;
	}
}
