using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000222 RID: 546
	public interface IAsyncOnScrollHandler
	{
		// Token: 0x06000C72 RID: 3186
		UniTask<PointerEventData> OnScrollAsync();
	}
}
