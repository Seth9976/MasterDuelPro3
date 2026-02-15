using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001D5 RID: 469
	[DisallowMultipleComponent]
	public sealed class AsyncMouseUpTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000B62 RID: 2914 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnMouseUp()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnMouseUpHandler GetOnMouseUpAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnMouseUpHandler GetOnMouseUpAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0002B48E File Offset: 0x0002968E
		public UniTask OnMouseUpAsync()
		{
			return ((IAsyncOnMouseUpHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnMouseUpAsync();
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0002B49C File Offset: 0x0002969C
		public UniTask OnMouseUpAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnMouseUpHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnMouseUpAsync();
		}
	}
}
