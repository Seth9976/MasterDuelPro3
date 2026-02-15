using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001EB RID: 491
	[TupleElementNames(new string[] { "source", "destination" })]
	[DisallowMultipleComponent]
	public sealed class AsyncRenderImageTrigger : AsyncTriggerBase<ValueTuple<RenderTexture, RenderTexture>>
	{
		// Token: 0x06000BAF RID: 2991 RVA: 0x0002B615 File Offset: 0x00029815
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.RaiseEvent(new ValueTuple<RenderTexture, RenderTexture>(source, destination));
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002B624 File Offset: 0x00029824
		public IAsyncOnRenderImageHandler GetOnRenderImageAsyncHandler()
		{
			return new AsyncTriggerHandler<ValueTuple<RenderTexture, RenderTexture>>(this, false);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002B62D File Offset: 0x0002982D
		public IAsyncOnRenderImageHandler GetOnRenderImageAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<ValueTuple<RenderTexture, RenderTexture>>(this, cancellationToken, false);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002B637 File Offset: 0x00029837
		[return: TupleElementNames(new string[] { "source", "destination" })]
		public UniTask<ValueTuple<RenderTexture, RenderTexture>> OnRenderImageAsync()
		{
			return ((IAsyncOnRenderImageHandler)new AsyncTriggerHandler<ValueTuple<RenderTexture, RenderTexture>>(this, true)).OnRenderImageAsync();
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002B645 File Offset: 0x00029845
		[return: TupleElementNames(new string[] { "source", "destination" })]
		public UniTask<ValueTuple<RenderTexture, RenderTexture>> OnRenderImageAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnRenderImageHandler)new AsyncTriggerHandler<ValueTuple<RenderTexture, RenderTexture>>(this, cancellationToken, true)).OnRenderImageAsync();
		}
	}
}
