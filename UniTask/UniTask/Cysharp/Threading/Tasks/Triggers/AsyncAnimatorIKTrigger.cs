using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200019B RID: 411
	[DisallowMultipleComponent]
	public sealed class AsyncAnimatorIKTrigger : AsyncTriggerBase<int>
	{
		// Token: 0x06000A97 RID: 2711 RVA: 0x0002B01F File Offset: 0x0002921F
		private void OnAnimatorIK(int layerIndex)
		{
			base.RaiseEvent(layerIndex);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0002B028 File Offset: 0x00029228
		public IAsyncOnAnimatorIKHandler GetOnAnimatorIKAsyncHandler()
		{
			return new AsyncTriggerHandler<int>(this, false);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0002B031 File Offset: 0x00029231
		public IAsyncOnAnimatorIKHandler GetOnAnimatorIKAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<int>(this, cancellationToken, false);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0002B03B File Offset: 0x0002923B
		public UniTask<int> OnAnimatorIKAsync()
		{
			return ((IAsyncOnAnimatorIKHandler)new AsyncTriggerHandler<int>(this, true)).OnAnimatorIKAsync();
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0002B049 File Offset: 0x00029249
		public UniTask<int> OnAnimatorIKAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnAnimatorIKHandler)new AsyncTriggerHandler<int>(this, cancellationToken, true)).OnAnimatorIKAsync();
		}
	}
}
