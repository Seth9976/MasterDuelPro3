using System;
using System.Threading;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200001B RID: 27
	public static class CancellationTokenSourceExtensions
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00003469 File Offset: 0x00001669
		private static void CancelCancellationTokenSourceState(object state)
		{
			((CancellationTokenSource)state).Cancel();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003476 File Offset: 0x00001676
		public static IDisposable CancelAfterSlim(this CancellationTokenSource cts, int millisecondsDelay, DelayType delayType = DelayType.DeltaTime, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update)
		{
			return cts.CancelAfterSlim(TimeSpan.FromMilliseconds((double)millisecondsDelay), delayType, delayTiming);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003487 File Offset: 0x00001687
		public static IDisposable CancelAfterSlim(this CancellationTokenSource cts, TimeSpan delayTimeSpan, DelayType delayType = DelayType.DeltaTime, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update)
		{
			return PlayerLoopTimer.StartNew(delayTimeSpan, false, delayType, delayTiming, cts.Token, CancellationTokenSourceExtensions.CancelCancellationTokenSourceStateDelegate, cts);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000349E File Offset: 0x0000169E
		public static void RegisterRaiseCancelOnDestroy(this CancellationTokenSource cts, Component component)
		{
			cts.RegisterRaiseCancelOnDestroy(component.gameObject);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000034AC File Offset: 0x000016AC
		public static void RegisterRaiseCancelOnDestroy(this CancellationTokenSource cts, GameObject gameObject)
		{
			gameObject.GetAsyncDestroyTrigger().CancellationToken.RegisterWithoutCaptureExecutionContext(CancellationTokenSourceExtensions.CancelCancellationTokenSourceStateDelegate, cts);
		}

		// Token: 0x04000056 RID: 86
		private static readonly Action<object> CancelCancellationTokenSourceStateDelegate = new Action<object>(CancellationTokenSourceExtensions.CancelCancellationTokenSourceState);
	}
}
