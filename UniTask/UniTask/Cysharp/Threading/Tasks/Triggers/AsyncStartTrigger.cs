using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000190 RID: 400
	[DisallowMultipleComponent]
	public sealed class AsyncStartTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000A0E RID: 2574 RVA: 0x0002A916 File Offset: 0x00028B16
		private void Start()
		{
			this.called = true;
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0002A92A File Offset: 0x00028B2A
		public UniTask StartAsync()
		{
			if (this.called)
			{
				return UniTask.CompletedTask;
			}
			return ((IAsyncOneShotTrigger)new AsyncTriggerHandler<AsyncUnit>(this, true)).OneShotAsync();
		}

		// Token: 0x0400063E RID: 1598
		private bool called;
	}
}
