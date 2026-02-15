using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000632 RID: 1586
	public class TweenScale : Tween
	{
		// Token: 0x060031EF RID: 12783 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x04002E76 RID: 11894
		[SerializeField]
		public Vector3 from;

		// Token: 0x04002E77 RID: 11895
		[SerializeField]
		public Vector3 to;
	}
}
