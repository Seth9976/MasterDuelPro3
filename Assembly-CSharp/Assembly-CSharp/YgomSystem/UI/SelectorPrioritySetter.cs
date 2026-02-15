using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x020005F5 RID: 1525
	public class SelectorPrioritySetter : MonoBehaviour
	{
		// Token: 0x060030E3 RID: 12515 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x04002D57 RID: 11607
		[SerializeField]
		private SelectorPrioritySetter.SetMode m_SetMode;

		// Token: 0x04002D58 RID: 11608
		[SerializeField]
		private Selector m_FromSelector;

		// Token: 0x020005F6 RID: 1526
		public enum SetMode
		{
			// Token: 0x04002D5A RID: 11610
			FromSelector
		}
	}
}
