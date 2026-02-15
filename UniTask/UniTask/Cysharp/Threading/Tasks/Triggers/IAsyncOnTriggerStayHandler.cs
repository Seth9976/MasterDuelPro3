using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001FC RID: 508
	public interface IAsyncOnTriggerStayHandler
	{
		// Token: 0x06000BED RID: 3053
		UniTask<Collider> OnTriggerStayAsync();
	}
}
