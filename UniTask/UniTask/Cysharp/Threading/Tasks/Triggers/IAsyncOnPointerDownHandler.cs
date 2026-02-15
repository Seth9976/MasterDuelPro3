using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200021A RID: 538
	public interface IAsyncOnPointerDownHandler
	{
		// Token: 0x06000C56 RID: 3158
		UniTask<PointerEventData> OnPointerDownAsync();
	}
}
