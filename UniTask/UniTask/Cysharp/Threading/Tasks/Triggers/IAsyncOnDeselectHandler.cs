using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200020C RID: 524
	public interface IAsyncOnDeselectHandler
	{
		// Token: 0x06000C25 RID: 3109
		UniTask<BaseEventData> OnDeselectAsync();
	}
}
