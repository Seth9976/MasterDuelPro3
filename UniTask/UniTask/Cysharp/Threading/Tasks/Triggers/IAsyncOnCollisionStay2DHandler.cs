using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001B8 RID: 440
	public interface IAsyncOnCollisionStay2DHandler
	{
		// Token: 0x06000AFF RID: 2815
		UniTask<Collision2D> OnCollisionStay2DAsync();
	}
}
