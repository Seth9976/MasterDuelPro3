using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001D8 RID: 472
	public interface IAsyncOnParticleCollisionHandler
	{
		// Token: 0x06000B6F RID: 2927
		UniTask<GameObject> OnParticleCollisionAsync();
	}
}
