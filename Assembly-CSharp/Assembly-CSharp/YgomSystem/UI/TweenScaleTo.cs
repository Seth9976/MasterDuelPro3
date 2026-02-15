using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000634 RID: 1588
	public class TweenScaleTo : Tween
	{
		// Token: 0x060031F3 RID: 12787 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x04002E7A RID: 11898
		private Vector3 from;

		// Token: 0x04002E7B RID: 11899
		[SerializeField]
		public Vector3 to;
	}
}
