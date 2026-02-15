using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000220 RID: 544
	public interface IAsyncOnPointerUpHandler
	{
		// Token: 0x06000C6B RID: 3179
		UniTask<PointerEventData> OnPointerUpAsync();
	}
}
