using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000208 RID: 520
	public interface IAsyncOnBeginDragHandler
	{
		// Token: 0x06000C17 RID: 3095
		UniTask<PointerEventData> OnBeginDragAsync();
	}
}
