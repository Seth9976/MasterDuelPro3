using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001F7 RID: 503
	[DisallowMultipleComponent]
	public sealed class AsyncTriggerEnter2DTrigger : AsyncTriggerBase<Collider2D>
	{
		// Token: 0x06000BD9 RID: 3033 RVA: 0x0002B711 File Offset: 0x00029911
		private void OnTriggerEnter2D(Collider2D other)
		{
			base.RaiseEvent(other);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0002B71A File Offset: 0x0002991A
		public IAsyncOnTriggerEnter2DHandler GetOnTriggerEnter2DAsyncHandler()
		{
			return new AsyncTriggerHandler<Collider2D>(this, false);
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0002B723 File Offset: 0x00029923
		public IAsyncOnTriggerEnter2DHandler GetOnTriggerEnter2DAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<Collider2D>(this, cancellationToken, false);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0002B72D File Offset: 0x0002992D
		public UniTask<Collider2D> OnTriggerEnter2DAsync()
		{
			return ((IAsyncOnTriggerEnter2DHandler)new AsyncTriggerHandler<Collider2D>(this, true)).OnTriggerEnter2DAsync();
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0002B73B File Offset: 0x0002993B
		public UniTask<Collider2D> OnTriggerEnter2DAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnTriggerEnter2DHandler)new AsyncTriggerHandler<Collider2D>(this, cancellationToken, true)).OnTriggerEnter2DAsync();
		}
	}
}
