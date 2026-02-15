using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001B2 RID: 434
	public interface IAsyncOnCollisionExitHandler
	{
		// Token: 0x06000AEA RID: 2794
		UniTask<Collision> OnCollisionExitAsync();
	}
}
