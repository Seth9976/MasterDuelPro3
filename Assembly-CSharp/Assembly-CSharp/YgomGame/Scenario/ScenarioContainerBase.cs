using System;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Scenario
{
	// Token: 0x020009D2 RID: 2514
	public class ScenarioContainerBase : ElementWidgetBase
	{
		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06004912 RID: 18706 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual GameObject shakeTarget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public ScenarioContainerBase(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06004914 RID: 18708 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayShake(float amount, float cycle, bool isShakeX, bool isShakeY, float autoStopSec = 0f)
		{
		}

		// Token: 0x06004915 RID: 18709 RVA: 0x0000216D File Offset: 0x0000036D
		public void StopShake()
		{
		}

		// Token: 0x06004916 RID: 18710 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPlayingShake()
		{
			return false;
		}

		// Token: 0x040086FD RID: 34557
		private ScenarioShaker m_Shaker;
	}
}
