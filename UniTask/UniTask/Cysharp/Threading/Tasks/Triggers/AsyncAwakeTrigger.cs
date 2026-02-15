using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200018C RID: 396
	[DisallowMultipleComponent]
	public sealed class AsyncAwakeTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000A02 RID: 2562 RVA: 0x0002A7CB File Offset: 0x000289CB
		public UniTask AwakeAsync()
		{
			if (this.calledAwake)
			{
				return UniTask.CompletedTask;
			}
			return ((IAsyncOneShotTrigger)new AsyncTriggerHandler<AsyncUnit>(this, true)).OneShotAsync();
		}
	}
}
