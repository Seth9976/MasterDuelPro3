using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x0200063E RID: 1598
	public class TweenSpeedWriter : MonoBehaviour
	{
		// Token: 0x0600320B RID: 12811 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(string label = null)
		{
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x0000216D File Offset: 0x0000036D
		private void WriteSpeed(float speed = 1f)
		{
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x0000216D File Offset: 0x0000036D
		private static void InnerTargetWriteSpeed(GameObject target, float speed = 1f, string label = null)
		{
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x0000216D File Offset: 0x0000036D
		public static void TargetWriteSpeed(GameObject target, float speed = 1f, string label = null, bool includeChildren = false)
		{
		}

		// Token: 0x04002EAB RID: 11947
		[SerializeField]
		private string m_Label;

		// Token: 0x04002EAC RID: 11948
		[SerializeField]
		private Tween[] m_TargetTweens;

		// Token: 0x04002EAD RID: 11949
		[SerializeField]
		private float[] m_OriginalDurations;

		// Token: 0x04002EAE RID: 11950
		[SerializeField]
		private float m_Speed;

		// Token: 0x04002EAF RID: 11951
		private bool m_Ready;
	}
}
