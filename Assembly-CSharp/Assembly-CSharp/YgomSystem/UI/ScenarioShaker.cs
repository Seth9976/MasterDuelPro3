using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x020005BC RID: 1468
	public class ScenarioShaker : MonoBehaviour
	{
		// Token: 0x06002E43 RID: 11843 RVA: 0x0000216A File Offset: 0x0000036A
		public static ScenarioShaker Attouch(GameObject target)
		{
			return null;
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayShake(float cycleDuration, float amount, bool shakeX, bool shakeY, float stopSec = 0f)
		{
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x0000216D File Offset: 0x0000036D
		public void StopShake()
		{
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnShakeXYSetValue(float par)
		{
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnShakeXSetValue(float par)
		{
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnShakeYSetValue(float par)
		{
		}

		// Token: 0x04002BF0 RID: 11248
		private bool m_Playing;

		// Token: 0x04002BF1 RID: 11249
		private float m_CycleDuration;

		// Token: 0x04002BF2 RID: 11250
		private float m_Amount;

		// Token: 0x04002BF3 RID: 11251
		private float m_AutoStopSec;

		// Token: 0x04002BF4 RID: 11252
		private float m_CycleSec;

		// Token: 0x04002BF5 RID: 11253
		private RectTransform m_RectTran;

		// Token: 0x04002BF6 RID: 11254
		private Vector3 m_StartPos;

		// Token: 0x04002BF7 RID: 11255
		private Vector3 m_From;

		// Token: 0x04002BF8 RID: 11256
		private Vector3 m_To;

		// Token: 0x04002BF9 RID: 11257
		private Vector3 m_MoveMin;

		// Token: 0x04002BFA RID: 11258
		private Vector3 m_MoveMax;

		// Token: 0x04002BFB RID: 11259
		private int m_MoveDir;

		// Token: 0x04002BFC RID: 11260
		private bool m_IsAutoStop;

		// Token: 0x04002BFD RID: 11261
		private Action<float> m_OnShakeSetValueAction;
	}
}
