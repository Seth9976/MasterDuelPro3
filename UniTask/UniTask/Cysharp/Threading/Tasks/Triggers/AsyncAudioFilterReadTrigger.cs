using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001A5 RID: 421
	[TupleElementNames(new string[] { "data", "channels" })]
	[DisallowMultipleComponent]
	public sealed class AsyncAudioFilterReadTrigger : AsyncTriggerBase<ValueTuple<float[], int>>
	{
		// Token: 0x06000ABA RID: 2746 RVA: 0x0002B0F8 File Offset: 0x000292F8
		private void OnAudioFilterRead(float[] data, int channels)
		{
			base.RaiseEvent(new ValueTuple<float[], int>(data, channels));
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0002B107 File Offset: 0x00029307
		public IAsyncOnAudioFilterReadHandler GetOnAudioFilterReadAsyncHandler()
		{
			return new AsyncTriggerHandler<ValueTuple<float[], int>>(this, false);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0002B110 File Offset: 0x00029310
		public IAsyncOnAudioFilterReadHandler GetOnAudioFilterReadAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<ValueTuple<float[], int>>(this, cancellationToken, false);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0002B11A File Offset: 0x0002931A
		[return: TupleElementNames(new string[] { "data", "channels" })]
		public UniTask<ValueTuple<float[], int>> OnAudioFilterReadAsync()
		{
			return ((IAsyncOnAudioFilterReadHandler)new AsyncTriggerHandler<ValueTuple<float[], int>>(this, true)).OnAudioFilterReadAsync();
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0002B128 File Offset: 0x00029328
		[return: TupleElementNames(new string[] { "data", "channels" })]
		public UniTask<ValueTuple<float[], int>> OnAudioFilterReadAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnAudioFilterReadHandler)new AsyncTriggerHandler<ValueTuple<float[], int>>(this, cancellationToken, true)).OnAudioFilterReadAsync();
		}
	}
}
