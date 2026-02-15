using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000252 RID: 594
	internal class Observer<TValue> : IObserver<TValue>
	{
		// Token: 0x0600159D RID: 5533 RVA: 0x00062789 File Offset: 0x00060989
		public Observer(Action<TValue> onNext, Action onCompleted = null)
		{
			this.m_OnNext = onNext;
			this.m_OnCompleted = onCompleted;
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x0006279F File Offset: 0x0006099F
		public void OnCompleted()
		{
			Action onCompleted = this.m_OnCompleted;
			if (onCompleted == null)
			{
				return;
			}
			onCompleted();
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x00054AD1 File Offset: 0x00052CD1
		public void OnError(Exception error)
		{
			Debug.LogException(error);
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x000627B1 File Offset: 0x000609B1
		public void OnNext(TValue evt)
		{
			Action<TValue> onNext = this.m_OnNext;
			if (onNext == null)
			{
				return;
			}
			onNext(evt);
		}

		// Token: 0x04000C84 RID: 3204
		private Action<TValue> m_OnNext;

		// Token: 0x04000C85 RID: 3205
		private Action m_OnCompleted;
	}
}
