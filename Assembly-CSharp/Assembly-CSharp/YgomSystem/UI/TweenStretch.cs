using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000641 RID: 1601
	public class TweenStretch : TweenStretchTo
	{
		// Token: 0x06003219 RID: 12825 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x04002EBC RID: 11964
		[SerializeField]
		public TweenStretchTo.StretchOffset from;
	}
}
