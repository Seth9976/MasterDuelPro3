using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Scenario
{
	// Token: 0x020009D1 RID: 2513
	public class ScenarioCardsAsset : ScriptableObject
	{
		// Token: 0x06004910 RID: 18704 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadAsync(string scenarioName, Action<ScenarioCardsAsset> onFinished)
		{
		}

		// Token: 0x040086FB RID: 34555
		private const string k_PathFormat = "Scenarios/Gates/ScenarioCards/{0}";

		// Token: 0x040086FC RID: 34556
		public List<int> mrks;
	}
}
