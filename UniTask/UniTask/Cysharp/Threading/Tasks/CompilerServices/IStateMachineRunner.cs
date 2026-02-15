using System;

namespace Cysharp.Threading.Tasks.CompilerServices
{
	// Token: 0x0200025F RID: 607
	internal interface IStateMachineRunner
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000D93 RID: 3475
		Action MoveNext { get; }

		// Token: 0x06000D94 RID: 3476
		void Return();
	}
}
