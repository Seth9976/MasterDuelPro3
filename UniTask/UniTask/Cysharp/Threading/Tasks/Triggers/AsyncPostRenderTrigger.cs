using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001E1 RID: 481
	[DisallowMultipleComponent]
	public sealed class AsyncPostRenderTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000B8C RID: 2956 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnPostRender()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnPostRenderHandler GetOnPostRenderAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnPostRenderHandler GetOnPostRenderAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002B584 File Offset: 0x00029784
		public UniTask OnPostRenderAsync()
		{
			return ((IAsyncOnPostRenderHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnPostRenderAsync();
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002B592 File Offset: 0x00029792
		public UniTask OnPostRenderAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnPostRenderHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnPostRenderAsync();
		}
	}
}
