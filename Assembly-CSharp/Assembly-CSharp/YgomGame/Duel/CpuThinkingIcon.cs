using System;
using UnityEngine;
using UnityEngine.Events;

namespace YgomGame.Duel
{
	// Token: 0x02000D1E RID: 3358
	public class CpuThinkingIcon : MonoBehaviour
	{
		// Token: 0x06006121 RID: 24865 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(Transform parent, UnityAction<CpuThinkingIcon> onFinish)
		{
		}

		// Token: 0x06006122 RID: 24866 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCpuThinkingBegin()
		{
		}

		// Token: 0x06006123 RID: 24867 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCpuThinkingEnd()
		{
		}

		// Token: 0x06006124 RID: 24868 RVA: 0x0000216D File Offset: 0x0000036D
		protected void Initialize()
		{
		}

		// Token: 0x06006125 RID: 24869 RVA: 0x0000216D File Offset: 0x0000036D
		private void ResetMenbers()
		{
		}

		// Token: 0x06006126 RID: 24870 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x04009C40 RID: 40000
		private float MAXTHINKINGTIME;

		// Token: 0x04009C41 RID: 40001
		private float m_CpuThinkingCount;

		// Token: 0x04009C42 RID: 40002
		private bool m_ShowIcon;

		// Token: 0x04009C43 RID: 40003
		private CanvasGroup m_CanvasGroup;
	}
}
