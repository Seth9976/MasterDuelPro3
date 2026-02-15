using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001B7 RID: 439
	[DisallowMultipleComponent]
	public sealed class AsyncCollisionStayTrigger : AsyncTriggerBase<Collision>
	{
		// Token: 0x06000AF9 RID: 2809 RVA: 0x0002B1B3 File Offset: 0x000293B3
		private void OnCollisionStay(Collision coll)
		{
			base.RaiseEvent(coll);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0002B1BC File Offset: 0x000293BC
		public IAsyncOnCollisionStayHandler GetOnCollisionStayAsyncHandler()
		{
			return new AsyncTriggerHandler<Collision>(this, false);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0002B1C5 File Offset: 0x000293C5
		public IAsyncOnCollisionStayHandler GetOnCollisionStayAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<Collision>(this, cancellationToken, false);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0002B26F File Offset: 0x0002946F
		public UniTask<Collision> OnCollisionStayAsync()
		{
			return ((IAsyncOnCollisionStayHandler)new AsyncTriggerHandler<Collision>(this, true)).OnCollisionStayAsync();
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0002B27D File Offset: 0x0002947D
		public UniTask<Collision> OnCollisionStayAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnCollisionStayHandler)new AsyncTriggerHandler<Collision>(this, cancellationToken, true)).OnCollisionStayAsync();
		}
	}
}
