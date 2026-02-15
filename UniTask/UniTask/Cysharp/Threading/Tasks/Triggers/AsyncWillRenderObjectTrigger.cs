using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000203 RID: 515
	[DisallowMultipleComponent]
	public sealed class AsyncWillRenderObjectTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000C03 RID: 3075 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnWillRenderObject()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnWillRenderObjectHandler GetOnWillRenderObjectAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnWillRenderObjectHandler GetOnWillRenderObjectAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0002B7E3 File Offset: 0x000299E3
		public UniTask OnWillRenderObjectAsync()
		{
			return ((IAsyncOnWillRenderObjectHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnWillRenderObjectAsync();
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0002B7F1 File Offset: 0x000299F1
		public UniTask OnWillRenderObjectAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnWillRenderObjectHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnWillRenderObjectAsync();
		}
	}
}
