using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

namespace Willow
{
	// Token: 0x02001547 RID: 5447
	public class BasePostProcessVolumeControl : MonoBehaviour, ITimeControl
	{
		// Token: 0x1700149E RID: 5278
		// (get) Token: 0x06009DD8 RID: 40408 RVA: 0x0000216A File Offset: 0x0000036A
		protected Volume volume
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06009DD9 RID: 40409 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTime(double time)
		{
		}

		// Token: 0x06009DDA RID: 40410 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnControlTimeStart()
		{
		}

		// Token: 0x06009DDB RID: 40411 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnControlTimeStop()
		{
		}

		// Token: 0x06009DDC RID: 40412 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void CheckPostProcess()
		{
		}

		// Token: 0x06009DDD RID: 40413 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void StartPostProcess()
		{
		}

		// Token: 0x06009DDE RID: 40414 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void UpdatePostProcess()
		{
		}

		// Token: 0x0400DDA1 RID: 56737
		private Volume m_volume;

		// Token: 0x0400DDA2 RID: 56738
		private bool m_isValid;

		// Token: 0x0400DDA3 RID: 56739
		private bool m_isStart;
	}
}
