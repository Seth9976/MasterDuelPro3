using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200021E RID: 542
	public interface IAsyncOnPointerExitHandler
	{
		// Token: 0x06000C64 RID: 3172
		UniTask<PointerEventData> OnPointerExitAsync();
	}
}
