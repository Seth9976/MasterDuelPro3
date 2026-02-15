using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Scenario
{
	// Token: 0x020009BD RID: 2493
	public class ScenarioBehaviour_PrefabCreate : ScenarioBehaviour, IScenarioLoadGroupHandleBehaviour
	{
		// Token: 0x0600489B RID: 18587 RVA: 0x000F49AE File Offset: 0x000F2BAE
		public ScenarioBehaviour_PrefabCreate(object commandData)
			: base(null)
		{
		}

		// Token: 0x0600489C RID: 18588 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressInit()
		{
		}

		// Token: 0x0600489D RID: 18589 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressAction()
		{
		}

		// Token: 0x0600489E RID: 18590 RVA: 0x0000216D File Offset: 0x0000036D
		public void CollectLoadPath(List<ValueTuple<string, Type>> res)
		{
		}

		// Token: 0x0600489F RID: 18591 RVA: 0x0000216D File Offset: 0x0000036D
		public void CollectLoadMrk(List<int> res)
		{
		}

		// Token: 0x040086B1 RID: 34481
		private bool m_CreateRequested;

		// Token: 0x040086B2 RID: 34482
		private GameObject m_CreatedObj;
	}
}
