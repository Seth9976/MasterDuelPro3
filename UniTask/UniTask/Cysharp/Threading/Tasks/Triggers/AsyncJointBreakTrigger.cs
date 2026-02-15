using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001C7 RID: 455
	[DisallowMultipleComponent]
	public sealed class AsyncJointBreakTrigger : AsyncTriggerBase<float>
	{
		// Token: 0x06000B31 RID: 2865 RVA: 0x0002B37B File Offset: 0x0002957B
		private void OnJointBreak(float breakForce)
		{
			base.RaiseEvent(breakForce);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0002B384 File Offset: 0x00029584
		public IAsyncOnJointBreakHandler GetOnJointBreakAsyncHandler()
		{
			return new AsyncTriggerHandler<float>(this, false);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0002B38D File Offset: 0x0002958D
		public IAsyncOnJointBreakHandler GetOnJointBreakAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<float>(this, cancellationToken, false);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0002B397 File Offset: 0x00029597
		public UniTask<float> OnJointBreakAsync()
		{
			return ((IAsyncOnJointBreakHandler)new AsyncTriggerHandler<float>(this, true)).OnJointBreakAsync();
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002B3A5 File Offset: 0x000295A5
		public UniTask<float> OnJointBreakAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnJointBreakHandler)new AsyncTriggerHandler<float>(this, cancellationToken, true)).OnJointBreakAsync();
		}
	}
}
