using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001A1 RID: 417
	[DisallowMultipleComponent]
	public sealed class AsyncApplicationPauseTrigger : AsyncTriggerBase<bool>
	{
		// Token: 0x06000AAC RID: 2732 RVA: 0x0002B07D File Offset: 0x0002927D
		private void OnApplicationPause(bool pauseStatus)
		{
			base.RaiseEvent(pauseStatus);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002B086 File Offset: 0x00029286
		public IAsyncOnApplicationPauseHandler GetOnApplicationPauseAsyncHandler()
		{
			return new AsyncTriggerHandler<bool>(this, false);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0002B08F File Offset: 0x0002928F
		public IAsyncOnApplicationPauseHandler GetOnApplicationPauseAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<bool>(this, cancellationToken, false);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002B0BE File Offset: 0x000292BE
		public UniTask<bool> OnApplicationPauseAsync()
		{
			return ((IAsyncOnApplicationPauseHandler)new AsyncTriggerHandler<bool>(this, true)).OnApplicationPauseAsync();
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002B0CC File Offset: 0x000292CC
		public UniTask<bool> OnApplicationPauseAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnApplicationPauseHandler)new AsyncTriggerHandler<bool>(this, cancellationToken, true)).OnApplicationPauseAsync();
		}
	}
}
