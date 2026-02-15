using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000212 RID: 530
	public interface IAsyncOnEndDragHandler
	{
		// Token: 0x06000C3A RID: 3130
		UniTask<PointerEventData> OnEndDragAsync();
	}
}
