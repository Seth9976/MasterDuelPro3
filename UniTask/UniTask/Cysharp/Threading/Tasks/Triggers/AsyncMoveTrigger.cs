using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000217 RID: 535
	[DisallowMultipleComponent]
	public sealed class AsyncMoveTrigger : AsyncTriggerBase<AxisEventData>, IMoveHandler, IEventSystemHandler
	{
		// Token: 0x06000C49 RID: 3145 RVA: 0x0002B94D File Offset: 0x00029B4D
		void IMoveHandler.OnMove(AxisEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0002B956 File Offset: 0x00029B56
		public IAsyncOnMoveHandler GetOnMoveAsyncHandler()
		{
			return new AsyncTriggerHandler<AxisEventData>(this, false);
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0002B95F File Offset: 0x00029B5F
		public IAsyncOnMoveHandler GetOnMoveAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AxisEventData>(this, cancellationToken, false);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0002B969 File Offset: 0x00029B69
		public UniTask<AxisEventData> OnMoveAsync()
		{
			return ((IAsyncOnMoveHandler)new AsyncTriggerHandler<AxisEventData>(this, true)).OnMoveAsync();
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0002B977 File Offset: 0x00029B77
		public UniTask<AxisEventData> OnMoveAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnMoveHandler)new AsyncTriggerHandler<AxisEventData>(this, cancellationToken, true)).OnMoveAsync();
		}
	}
}
