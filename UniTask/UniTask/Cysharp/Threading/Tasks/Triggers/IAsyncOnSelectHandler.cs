using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000224 RID: 548
	public interface IAsyncOnSelectHandler
	{
		// Token: 0x06000C79 RID: 3193
		UniTask<BaseEventData> OnSelectAsync();
	}
}
