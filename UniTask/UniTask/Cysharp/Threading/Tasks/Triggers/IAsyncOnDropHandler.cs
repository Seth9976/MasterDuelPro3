using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000210 RID: 528
	public interface IAsyncOnDropHandler
	{
		// Token: 0x06000C33 RID: 3123
		UniTask<PointerEventData> OnDropAsync();
	}
}
