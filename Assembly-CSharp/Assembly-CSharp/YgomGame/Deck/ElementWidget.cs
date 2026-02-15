using System;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomGame.Deck
{
	// Token: 0x02000FE7 RID: 4071
	public abstract class ElementWidget : MonoBehaviour
	{
		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06007AB9 RID: 31417 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isIni
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007ABA RID: 31418 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x06007ABB RID: 31419 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06007ABC RID: 31420
		protected abstract void InitializeElements();

		// Token: 0x0400B170 RID: 45424
		protected ElementObjectManager m_Eom;

		// Token: 0x0400B171 RID: 45425
		protected bool isInitialized;
	}
}
