using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001BA RID: 442
	public interface IAsyncOnControllerColliderHitHandler
	{
		// Token: 0x06000B06 RID: 2822
		UniTask<ControllerColliderHit> OnControllerColliderHitAsync();
	}
}
