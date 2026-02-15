using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Utility
{
	// Token: 0x02000832 RID: 2098
	public class ServerLifeCycle : MonoBehaviour
	{
		// Token: 0x060040A7 RID: 16551 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x060040A9 RID: 16553 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCallMaintenance(Dictionary<string, object> param)
		{
		}

		// Token: 0x040039A1 RID: 14753
		private static ServerLifeCycle instance;
	}
}
