using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001E7 RID: 487
	[DisallowMultipleComponent]
	public sealed class AsyncRectTransformDimensionsChangeTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnRectTransformDimensionsChange()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnRectTransformDimensionsChangeHandler GetOnRectTransformDimensionsChangeAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnRectTransformDimensionsChangeHandler GetOnRectTransformDimensionsChangeAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002B5DB File Offset: 0x000297DB
		public UniTask OnRectTransformDimensionsChangeAsync()
		{
			return ((IAsyncOnRectTransformDimensionsChangeHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnRectTransformDimensionsChangeAsync();
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002B5E9 File Offset: 0x000297E9
		public UniTask OnRectTransformDimensionsChangeAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnRectTransformDimensionsChangeHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnRectTransformDimensionsChangeAsync();
		}
	}
}
