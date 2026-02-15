using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001F4 RID: 500
	public interface IAsyncOnTriggerEnterHandler
	{
		// Token: 0x06000BD1 RID: 3025
		UniTask<Collider> OnTriggerEnterAsync();
	}
}
