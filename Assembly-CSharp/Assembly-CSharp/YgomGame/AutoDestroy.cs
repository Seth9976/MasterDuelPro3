using System;
using System.Collections;
using UnityEngine;

namespace YgomGame
{
	// Token: 0x020007AB RID: 1963
	public class AutoDestroy : MonoBehaviour
	{
		// Token: 0x06003CDE RID: 15582 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator DelayDestroy()
		{
			return null;
		}

		// Token: 0x0400355E RID: 13662
		[SerializeField]
		private float delayTime;

		// Token: 0x0400355F RID: 13663
		private float restTime;
	}
}
