using System;

namespace YgomGame.Menu
{
	// Token: 0x02000A82 RID: 2690
	public class HomeAction
	{
		// Token: 0x06004EC2 RID: 20162 RVA: 0x00002739 File Offset: 0x00000939
		public HomeAction(int priority, Action<Action> action)
		{
		}

		// Token: 0x04008C8D RID: 35981
		public int priority;

		// Token: 0x04008C8E RID: 35982
		public Action<Action> action;
	}
}
