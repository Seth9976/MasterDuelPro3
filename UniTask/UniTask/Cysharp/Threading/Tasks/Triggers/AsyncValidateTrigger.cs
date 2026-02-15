using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000201 RID: 513
	[DisallowMultipleComponent]
	public sealed class AsyncValidateTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000BFC RID: 3068 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnValidate()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnValidateHandler GetOnValidateAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnValidateHandler GetOnValidateAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0002B7C6 File Offset: 0x000299C6
		public UniTask OnValidateAsync()
		{
			return ((IAsyncOnValidateHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnValidateAsync();
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0002B7D4 File Offset: 0x000299D4
		public UniTask OnValidateAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnValidateHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnValidateAsync();
		}
	}
}
