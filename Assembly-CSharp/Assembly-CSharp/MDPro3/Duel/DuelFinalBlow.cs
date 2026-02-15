using System;
using UnityEngine;

namespace MDPro3.Duel
{
	// Token: 0x020014BC RID: 5308
	public class DuelFinalBlow : LoopTrackManager
	{
		// Token: 0x06009B05 RID: 39685 RVA: 0x0017F46F File Offset: 0x0017D66F
		public void Destroy()
		{
			base.StopLoop();
			global::UnityEngine.Object.Destroy(base.gameObject, 0.5f);
		}
	}
}
