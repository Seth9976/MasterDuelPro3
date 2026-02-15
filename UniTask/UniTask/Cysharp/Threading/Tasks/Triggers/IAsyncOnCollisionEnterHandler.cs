using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001AE RID: 430
	public interface IAsyncOnCollisionEnterHandler
	{
		// Token: 0x06000ADC RID: 2780
		UniTask<Collision> OnCollisionEnterAsync();
	}
}
