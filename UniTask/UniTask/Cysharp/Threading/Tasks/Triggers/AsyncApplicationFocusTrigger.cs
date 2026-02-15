using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200019F RID: 415
	[DisallowMultipleComponent]
	public sealed class AsyncApplicationFocusTrigger : AsyncTriggerBase<bool>
	{
		// Token: 0x06000AA5 RID: 2725 RVA: 0x0002B07D File Offset: 0x0002927D
		private void OnApplicationFocus(bool hasFocus)
		{
			base.RaiseEvent(hasFocus);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002B086 File Offset: 0x00029286
		public IAsyncOnApplicationFocusHandler GetOnApplicationFocusAsyncHandler()
		{
			return new AsyncTriggerHandler<bool>(this, false);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002B08F File Offset: 0x0002928F
		public IAsyncOnApplicationFocusHandler GetOnApplicationFocusAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<bool>(this, cancellationToken, false);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0002B099 File Offset: 0x00029299
		public UniTask<bool> OnApplicationFocusAsync()
		{
			return ((IAsyncOnApplicationFocusHandler)new AsyncTriggerHandler<bool>(this, true)).OnApplicationFocusAsync();
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0002B0A7 File Offset: 0x000292A7
		public UniTask<bool> OnApplicationFocusAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnApplicationFocusHandler)new AsyncTriggerHandler<bool>(this, cancellationToken, true)).OnApplicationFocusAsync();
		}
	}
}
