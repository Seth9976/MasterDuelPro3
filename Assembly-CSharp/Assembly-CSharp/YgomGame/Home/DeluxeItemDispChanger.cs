using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Home
{
	// Token: 0x02000BDD RID: 3037
	public class DeluxeItemDispChanger : MonoBehaviour
	{
		// Token: 0x0600567E RID: 22142 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x0600567F RID: 22143 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateActive()
		{
		}

		// Token: 0x0400936F RID: 37743
		[SerializeField]
		private int itemId;

		// Token: 0x04009370 RID: 37744
		[SerializeField]
		private List<Transform> DeluxeObjects;

		// Token: 0x04009371 RID: 37745
		[SerializeField]
		private List<Transform> NotDeluxeObjects;

		// Token: 0x04009372 RID: 37746
		[SerializeField]
		private bool isScaleZero;
	}
}
