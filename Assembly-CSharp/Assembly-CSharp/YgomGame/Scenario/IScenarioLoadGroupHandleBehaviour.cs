using System;
using System.Collections.Generic;

namespace YgomGame.Scenario
{
	// Token: 0x0200099E RID: 2462
	public interface IScenarioLoadGroupHandleBehaviour
	{
		// Token: 0x060047E0 RID: 18400
		void CollectLoadPath(List<ValueTuple<string, Type>> res);

		// Token: 0x060047E1 RID: 18401
		void CollectLoadMrk(List<int> res);
	}
}
