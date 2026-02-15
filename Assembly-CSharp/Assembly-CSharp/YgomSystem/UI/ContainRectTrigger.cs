using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x0200058B RID: 1419
	public class ContainRectTrigger : MonoBehaviour
	{
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06002CD1 RID: 11473 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002CD2 RID: 11474 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isContain
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06002CD3 RID: 11475 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002CD4 RID: 11476 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<bool> onChangeEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x0000216A File Offset: 0x0000036A
		public static ContainRectTrigger Attach(RectTransform target, RectTransform container, Action<bool> onChangeCallback = null)
		{
			return null;
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsInViewport()
		{
			return false;
		}

		// Token: 0x04002B14 RID: 11028
		private RectTransform m_Container;

		// Token: 0x04002B15 RID: 11029
		private RectTransform m_Target;
	}
}
