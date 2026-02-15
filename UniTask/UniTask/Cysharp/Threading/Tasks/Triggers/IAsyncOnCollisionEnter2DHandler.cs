using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001B0 RID: 432
	public interface IAsyncOnCollisionEnter2DHandler
	{
		// Token: 0x06000AE3 RID: 2787
		UniTask<Collision2D> OnCollisionEnter2DAsync();
	}
}
