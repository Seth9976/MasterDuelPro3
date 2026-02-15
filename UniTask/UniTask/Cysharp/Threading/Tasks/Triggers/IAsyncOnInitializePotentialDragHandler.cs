using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000214 RID: 532
	public interface IAsyncOnInitializePotentialDragHandler
	{
		// Token: 0x06000C41 RID: 3137
		UniTask<PointerEventData> OnInitializePotentialDragAsync();
	}
}
