using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200018D RID: 397
	[DisallowMultipleComponent]
	public sealed class AsyncDestroyTrigger : MonoBehaviour
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0002A7EF File Offset: 0x000289EF
		public CancellationToken CancellationToken
		{
			get
			{
				if (this.cancellationTokenSource == null)
				{
					this.cancellationTokenSource = new CancellationTokenSource();
					if (!this.awakeCalled)
					{
						PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, new AsyncDestroyTrigger.AwakeMonitor(this));
					}
				}
				return this.cancellationTokenSource.Token;
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0002A823 File Offset: 0x00028A23
		private void Awake()
		{
			this.awakeCalled = true;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0002A82C File Offset: 0x00028A2C
		private void OnDestroy()
		{
			this.called = true;
			CancellationTokenSource cancellationTokenSource = this.cancellationTokenSource;
			if (cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
			}
			CancellationTokenSource cancellationTokenSource2 = this.cancellationTokenSource;
			if (cancellationTokenSource2 == null)
			{
				return;
			}
			cancellationTokenSource2.Dispose();
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0002A858 File Offset: 0x00028A58
		public UniTask OnDestroyAsync()
		{
			if (this.called)
			{
				return UniTask.CompletedTask;
			}
			UniTaskCompletionSource tcs = new UniTaskCompletionSource();
			this.CancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
			{
				((UniTaskCompletionSource)state).TrySetResult();
			}, tcs);
			return tcs.Task;
		}

		// Token: 0x04000638 RID: 1592
		private bool awakeCalled;

		// Token: 0x04000639 RID: 1593
		private bool called;

		// Token: 0x0400063A RID: 1594
		private CancellationTokenSource cancellationTokenSource;

		// Token: 0x0200018E RID: 398
		private class AwakeMonitor : IPlayerLoopItem
		{
			// Token: 0x06000A09 RID: 2569 RVA: 0x0002A8B3 File Offset: 0x00028AB3
			public AwakeMonitor(AsyncDestroyTrigger trigger)
			{
				this.trigger = trigger;
			}

			// Token: 0x06000A0A RID: 2570 RVA: 0x0002A8C2 File Offset: 0x00028AC2
			public bool MoveNext()
			{
				if (this.trigger.called || this.trigger.awakeCalled)
				{
					return false;
				}
				if (this.trigger == null)
				{
					this.trigger.OnDestroy();
					return false;
				}
				return true;
			}

			// Token: 0x0400063B RID: 1595
			private readonly AsyncDestroyTrigger trigger;
		}
	}
}
