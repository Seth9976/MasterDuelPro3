using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200020E RID: 526
	public interface IAsyncOnDragHandler
	{
		// Token: 0x06000C2C RID: 3116
		UniTask<PointerEventData> OnDragAsync();
	}
}
