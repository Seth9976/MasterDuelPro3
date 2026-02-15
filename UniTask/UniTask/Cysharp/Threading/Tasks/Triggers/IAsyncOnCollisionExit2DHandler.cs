using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001B4 RID: 436
	public interface IAsyncOnCollisionExit2DHandler
	{
		// Token: 0x06000AF1 RID: 2801
		UniTask<Collision2D> OnCollisionExit2DAsync();
	}
}
