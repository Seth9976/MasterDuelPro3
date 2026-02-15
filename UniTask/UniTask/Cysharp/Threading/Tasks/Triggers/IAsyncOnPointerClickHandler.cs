using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000218 RID: 536
	public interface IAsyncOnPointerClickHandler
	{
		// Token: 0x06000C4F RID: 3151
		UniTask<PointerEventData> OnPointerClickAsync();
	}
}
