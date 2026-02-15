using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000255 RID: 597
	internal class TakeNObservable<TValue> : IObservable<TValue>
	{
		// Token: 0x060015A7 RID: 5543 RVA: 0x00062864 File Offset: 0x00060A64
		public TakeNObservable(IObservable<TValue> source, int count)
		{
			this.m_Source = source;
			this.m_Count = count;
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0006287A File Offset: 0x00060A7A
		public IDisposable Subscribe(IObserver<TValue> observer)
		{
			return this.m_Source.Subscribe(new TakeNObservable<TValue>.Take(this, observer));
		}

		// Token: 0x04000C8A RID: 3210
		private IObservable<TValue> m_Source;

		// Token: 0x04000C8B RID: 3211
		private int m_Count;

		// Token: 0x02000256 RID: 598
		private class Take : IObserver<TValue>
		{
			// Token: 0x060015A9 RID: 5545 RVA: 0x0006288E File Offset: 0x00060A8E
			public Take(TakeNObservable<TValue> observable, IObserver<TValue> observer)
			{
				this.m_Observer = observer;
				this.m_Remaining = observable.m_Count;
			}

			// Token: 0x060015AA RID: 5546 RVA: 0x000049FE File Offset: 0x00002BFE
			public void OnCompleted()
			{
			}

			// Token: 0x060015AB RID: 5547 RVA: 0x00054AD1 File Offset: 0x00052CD1
			public void OnError(Exception error)
			{
				Debug.LogException(error);
			}

			// Token: 0x060015AC RID: 5548 RVA: 0x000628A9 File Offset: 0x00060AA9
			public void OnNext(TValue evt)
			{
				if (this.m_Remaining <= 0)
				{
					return;
				}
				this.m_Remaining--;
				this.m_Observer.OnNext(evt);
				if (this.m_Remaining == 0)
				{
					this.m_Observer.OnCompleted();
					this.m_Observer = null;
				}
			}

			// Token: 0x04000C8C RID: 3212
			private IObserver<TValue> m_Observer;

			// Token: 0x04000C8D RID: 3213
			private int m_Remaining;
		}
	}
}
