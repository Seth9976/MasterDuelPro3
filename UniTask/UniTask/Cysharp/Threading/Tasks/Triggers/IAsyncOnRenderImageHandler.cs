using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001EA RID: 490
	public interface IAsyncOnRenderImageHandler
	{
		// Token: 0x06000BAE RID: 2990
		[return: TupleElementNames(new string[] { "source", "destination" })]
		UniTask<ValueTuple<RenderTexture, RenderTexture>> OnRenderImageAsync();
	}
}
