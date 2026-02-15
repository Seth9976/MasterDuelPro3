using System;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks.CompilerServices
{
	// Token: 0x02000260 RID: 608
	internal interface IStateMachineRunnerPromise : IUniTaskSource, IValueTaskSource
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000D95 RID: 3477
		Action MoveNext { get; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000D96 RID: 3478
		UniTask Task { get; }

		// Token: 0x06000D97 RID: 3479
		void SetResult();

		// Token: 0x06000D98 RID: 3480
		void SetException(Exception exception);
	}
}
