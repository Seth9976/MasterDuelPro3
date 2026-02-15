using System;

namespace System.Threading.Tasks
{
	// Token: 0x020002BA RID: 698
	internal interface ITaskCompletionAction
	{
		// Token: 0x0600196C RID: 6508
		void Invoke(Task completingTask);

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600196D RID: 6509
		bool InvokeMayRunArbitraryCode { get; }
	}
}
