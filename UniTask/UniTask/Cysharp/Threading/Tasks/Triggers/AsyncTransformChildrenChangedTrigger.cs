using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001F1 RID: 497
	[DisallowMultipleComponent]
	public sealed class AsyncTransformChildrenChangedTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnTransformChildrenChanged()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnTransformChildrenChangedHandler GetOnTransformChildrenChangedAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnTransformChildrenChangedHandler GetOnTransformChildrenChangedAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002B696 File Offset: 0x00029896
		public UniTask OnTransformChildrenChangedAsync()
		{
			return ((IAsyncOnTransformChildrenChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnTransformChildrenChangedAsync();
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002B6A4 File Offset: 0x000298A4
		public UniTask OnTransformChildrenChangedAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnTransformChildrenChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnTransformChildrenChangedAsync();
		}
	}
}
