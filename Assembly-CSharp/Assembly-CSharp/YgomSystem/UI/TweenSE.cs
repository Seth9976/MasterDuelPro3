using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000631 RID: 1585
	public class TweenSE : Tween
	{
		// Token: 0x060031EC RID: 12780 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x04002E72 RID: 11890
		[SerializeField]
		public string m_SoundLabel;

		// Token: 0x04002E73 RID: 11891
		[SerializeField]
		public bool m_PlayOnFinish;

		// Token: 0x04002E74 RID: 11892
		private int instanceId;

		// Token: 0x04002E75 RID: 11893
		private bool m_IsPlayInFrame;
	}
}
