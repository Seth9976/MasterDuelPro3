using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu;

namespace YgomGame.Home
{
	// Token: 0x02000BE0 RID: 3040
	public class HomePopIconWidget
	{
		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06005684 RID: 22148 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool enabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005685 RID: 22149 RVA: 0x0000216D File Offset: 0x0000036D
		public void Assign(HomePopIconWidget.PopIconType iconType, GameObject iconHolder)
		{
		}

		// Token: 0x06005686 RID: 22150 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsReady()
		{
			return false;
		}

		// Token: 0x06005687 RID: 22151 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateActive(HomePopIconWidget.PopIconType iconType, bool activeFlag)
		{
		}

		// Token: 0x06005688 RID: 22152 RVA: 0x0000216D File Offset: 0x0000036D
		public void Activate()
		{
		}

		// Token: 0x06005689 RID: 22153 RVA: 0x0000216D File Offset: 0x0000036D
		public void Deactivate()
		{
		}

		// Token: 0x0600568A RID: 22154 RVA: 0x0000216D File Offset: 0x0000036D
		public void TryPlayPopIn(HomeViewController hvc)
		{
		}

		// Token: 0x0600568B RID: 22155 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yPlayPopIn(HomeViewController hvc)
		{
			return null;
		}

		// Token: 0x0600568C RID: 22156 RVA: 0x0000216D File Offset: 0x0000036D
		public void TryPlayPopOut(HomeViewController hvc)
		{
		}

		// Token: 0x0600568D RID: 22157 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yPlayPopOut(HomeViewController hvc)
		{
			return null;
		}

		// Token: 0x04009383 RID: 37763
		private readonly string k_TLabel_PopIn;

		// Token: 0x04009384 RID: 37764
		private readonly string k_TLabel_PopOut;

		// Token: 0x04009385 RID: 37765
		private readonly Dictionary<HomePopIconWidget.PopIconType, GameObject> m_HolderMap;

		// Token: 0x04009386 RID: 37766
		private readonly Dictionary<HomePopIconWidget.PopIconType, bool> m_ActiveMap;

		// Token: 0x04009387 RID: 37767
		private readonly Dictionary<HomePopIconWidget.PopIconType, bool> m_LoadingStateMap;

		// Token: 0x04009388 RID: 37768
		private bool m_Activate;

		// Token: 0x04009389 RID: 37769
		private Coroutine m_TransitionRoutine;

		// Token: 0x02000BE1 RID: 3041
		public enum PopIconType
		{
			// Token: 0x0400938B RID: 37771
			StartingMission = 1
		}
	}
}
