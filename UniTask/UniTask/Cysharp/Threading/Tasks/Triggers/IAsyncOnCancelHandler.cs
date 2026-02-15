using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200020A RID: 522
	public interface IAsyncOnCancelHandler
	{
		// Token: 0x06000C1E RID: 3102
		UniTask<BaseEventData> OnCancelAsync();
	}
}
