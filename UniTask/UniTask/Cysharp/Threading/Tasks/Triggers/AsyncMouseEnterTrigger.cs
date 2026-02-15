using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001CF RID: 463
	[DisallowMultipleComponent]
	public sealed class AsyncMouseEnterTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000B4D RID: 2893 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnMouseEnter()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnMouseEnterHandler GetOnMouseEnterAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnMouseEnterHandler GetOnMouseEnterAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0002B437 File Offset: 0x00029637
		public UniTask OnMouseEnterAsync()
		{
			return ((IAsyncOnMouseEnterHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnMouseEnterAsync();
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0002B445 File Offset: 0x00029645
		public UniTask OnMouseEnterAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnMouseEnterHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnMouseEnterAsync();
		}
	}
}
