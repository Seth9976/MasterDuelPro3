using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000205 RID: 517
	[DisallowMultipleComponent]
	public sealed class AsyncResetTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000C0A RID: 3082 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void Reset()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncResetHandler GetResetAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncResetHandler GetResetAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0002B800 File Offset: 0x00029A00
		public UniTask ResetAsync()
		{
			return ((IAsyncResetHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).ResetAsync();
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0002B80E File Offset: 0x00029A0E
		public UniTask ResetAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncResetHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).ResetAsync();
		}
	}
}
