using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000226 RID: 550
	public interface IAsyncOnSubmitHandler
	{
		// Token: 0x06000C80 RID: 3200
		UniTask<BaseEventData> OnSubmitAsync();
	}
}
