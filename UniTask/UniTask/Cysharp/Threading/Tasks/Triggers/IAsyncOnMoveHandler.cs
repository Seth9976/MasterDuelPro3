using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000216 RID: 534
	public interface IAsyncOnMoveHandler
	{
		// Token: 0x06000C48 RID: 3144
		UniTask<AxisEventData> OnMoveAsync();
	}
}
