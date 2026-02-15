using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001A3 RID: 419
	[DisallowMultipleComponent]
	public sealed class AsyncApplicationQuitTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000AB3 RID: 2739 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnApplicationQuit()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnApplicationQuitHandler GetOnApplicationQuitAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnApplicationQuitHandler GetOnApplicationQuitAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0002B0DB File Offset: 0x000292DB
		public UniTask OnApplicationQuitAsync()
		{
			return ((IAsyncOnApplicationQuitHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnApplicationQuitAsync();
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002B0E9 File Offset: 0x000292E9
		public UniTask OnApplicationQuitAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnApplicationQuitHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnApplicationQuitAsync();
		}
	}
}
