using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000228 RID: 552
	public interface IAsyncOnUpdateSelectedHandler
	{
		// Token: 0x06000C87 RID: 3207
		UniTask<BaseEventData> OnUpdateSelectedAsync();
	}
}
