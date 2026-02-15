using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200021C RID: 540
	public interface IAsyncOnPointerEnterHandler
	{
		// Token: 0x06000C5D RID: 3165
		UniTask<PointerEventData> OnPointerEnterAsync();
	}
}
