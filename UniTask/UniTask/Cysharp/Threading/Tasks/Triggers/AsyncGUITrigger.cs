using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001C5 RID: 453
	[DisallowMultipleComponent]
	public sealed class AsyncGUITrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000B2A RID: 2858 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnGUI()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnGUIHandler GetOnGUIAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnGUIHandler GetOnGUIAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002B35E File Offset: 0x0002955E
		public UniTask OnGUIAsync()
		{
			return ((IAsyncOnGUIHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnGUIAsync();
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0002B36C File Offset: 0x0002956C
		public UniTask OnGUIAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnGUIHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnGUIAsync();
		}
	}
}
