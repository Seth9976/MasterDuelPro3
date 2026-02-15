using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001F8 RID: 504
	public interface IAsyncOnTriggerExitHandler
	{
		// Token: 0x06000BDF RID: 3039
		UniTask<Collider> OnTriggerExitAsync();
	}
}
