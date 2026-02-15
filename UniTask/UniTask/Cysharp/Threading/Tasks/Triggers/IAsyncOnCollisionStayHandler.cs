using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001B6 RID: 438
	public interface IAsyncOnCollisionStayHandler
	{
		// Token: 0x06000AF8 RID: 2808
		UniTask<Collision> OnCollisionStayAsync();
	}
}
