using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001FE RID: 510
	public interface IAsyncOnTriggerStay2DHandler
	{
		// Token: 0x06000BF4 RID: 3060
		UniTask<Collider2D> OnTriggerStay2DAsync();
	}
}
