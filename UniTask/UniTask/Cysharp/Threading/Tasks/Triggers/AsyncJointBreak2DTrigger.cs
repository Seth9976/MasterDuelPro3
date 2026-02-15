using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001C9 RID: 457
	[DisallowMultipleComponent]
	public sealed class AsyncJointBreak2DTrigger : AsyncTriggerBase<Joint2D>
	{
		// Token: 0x06000B38 RID: 2872 RVA: 0x0002B3BC File Offset: 0x000295BC
		private void OnJointBreak2D(Joint2D brokenJoint)
		{
			base.RaiseEvent(brokenJoint);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0002B3C5 File Offset: 0x000295C5
		public IAsyncOnJointBreak2DHandler GetOnJointBreak2DAsyncHandler()
		{
			return new AsyncTriggerHandler<Joint2D>(this, false);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0002B3CE File Offset: 0x000295CE
		public IAsyncOnJointBreak2DHandler GetOnJointBreak2DAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<Joint2D>(this, cancellationToken, false);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0002B3D8 File Offset: 0x000295D8
		public UniTask<Joint2D> OnJointBreak2DAsync()
		{
			return ((IAsyncOnJointBreak2DHandler)new AsyncTriggerHandler<Joint2D>(this, true)).OnJointBreak2DAsync();
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0002B3E6 File Offset: 0x000295E6
		public UniTask<Joint2D> OnJointBreak2DAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnJointBreak2DHandler)new AsyncTriggerHandler<Joint2D>(this, cancellationToken, true)).OnJointBreak2DAsync();
		}
	}
}
