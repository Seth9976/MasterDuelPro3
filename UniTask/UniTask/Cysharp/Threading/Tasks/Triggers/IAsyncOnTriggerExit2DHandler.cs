using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001FA RID: 506
	public interface IAsyncOnTriggerExit2DHandler
	{
		// Token: 0x06000BE6 RID: 3046
		UniTask<Collider2D> OnTriggerExit2DAsync();
	}
}
