using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001F6 RID: 502
	public interface IAsyncOnTriggerEnter2DHandler
	{
		// Token: 0x06000BD8 RID: 3032
		UniTask<Collider2D> OnTriggerEnter2DAsync();
	}
}
