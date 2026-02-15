using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001E5 RID: 485
	[DisallowMultipleComponent]
	public sealed class AsyncPreRenderTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000B9A RID: 2970 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnPreRender()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnPreRenderHandler GetOnPreRenderAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnPreRenderHandler GetOnPreRenderAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0002B5BE File Offset: 0x000297BE
		public UniTask OnPreRenderAsync()
		{
			return ((IAsyncOnPreRenderHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnPreRenderAsync();
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0002B5CC File Offset: 0x000297CC
		public UniTask OnPreRenderAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnPreRenderHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnPreRenderAsync();
		}
	}
}
