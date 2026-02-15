using System;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomGame.Menu
{
	// Token: 0x02000A53 RID: 2643
	public abstract class DialogViewControllerBase : BaseMenuViewController
	{
		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06004D38 RID: 19768 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual int selectorPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06004D39 RID: 19769 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Awake()
		{
		}

		// Token: 0x04008AD4 RID: 35540
		[SerializeField]
		private ElementObjectManager m_UIPrefab;

		// Token: 0x04008AD5 RID: 35541
		protected ElementObjectManager m_UI;
	}
}
