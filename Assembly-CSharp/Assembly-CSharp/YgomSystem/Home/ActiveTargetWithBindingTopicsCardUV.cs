using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.Home
{
	// Token: 0x02000762 RID: 1890
	public class ActiveTargetWithBindingTopicsCardUV : MonoBehaviour
	{
		// Token: 0x06003AF7 RID: 15095 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06003AF8 RID: 15096 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateActive()
		{
		}

		// Token: 0x0400346B RID: 13419
		[SerializeField]
		private List<Transform> activeTrueObjects;

		// Token: 0x0400346C RID: 13420
		[SerializeField]
		private List<Transform> activeFalseObjects;

		// Token: 0x0400346D RID: 13421
		[SerializeField]
		private bool isScaleZero;

		// Token: 0x0400346E RID: 13422
		protected BindingTopicsCardUV component;
	}
}
