using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001AD RID: 429
	[DisallowMultipleComponent]
	public sealed class AsyncOnCanvasGroupChangedTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000AD6 RID: 2774 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnCanvasGroupChanged()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnCanvasGroupChangedHandler GetOnCanvasGroupChangedAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnCanvasGroupChangedHandler GetOnCanvasGroupChangedAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0002B196 File Offset: 0x00029396
		public UniTask OnCanvasGroupChangedAsync()
		{
			return ((IAsyncOnCanvasGroupChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnCanvasGroupChangedAsync();
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0002B1A4 File Offset: 0x000293A4
		public UniTask OnCanvasGroupChangedAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnCanvasGroupChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnCanvasGroupChangedAsync();
		}
	}
}
