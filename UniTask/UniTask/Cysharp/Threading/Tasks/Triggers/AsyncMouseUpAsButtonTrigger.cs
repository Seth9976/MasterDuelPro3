using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001D7 RID: 471
	[DisallowMultipleComponent]
	public sealed class AsyncMouseUpAsButtonTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000B69 RID: 2921 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnMouseUpAsButton()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnMouseUpAsButtonHandler GetOnMouseUpAsButtonAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnMouseUpAsButtonHandler GetOnMouseUpAsButtonAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0002B4AB File Offset: 0x000296AB
		public UniTask OnMouseUpAsButtonAsync()
		{
			return ((IAsyncOnMouseUpAsButtonHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnMouseUpAsButtonAsync();
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0002B4B9 File Offset: 0x000296B9
		public UniTask OnMouseUpAsButtonAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnMouseUpAsButtonHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnMouseUpAsButtonAsync();
		}
	}
}
