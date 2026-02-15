using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000257 RID: 599
	internal class WhereObservable<TValue> : IObservable<TValue>
	{
		// Token: 0x060015AD RID: 5549 RVA: 0x000628E9 File Offset: 0x00060AE9
		public WhereObservable(IObservable<TValue> source, Func<TValue, bool> predicate)
		{
			this.m_Source = source;
			this.m_Predicate = predicate;
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x000628FF File Offset: 0x00060AFF
		public IDisposable Subscribe(IObserver<TValue> observer)
		{
			return this.m_Source.Subscribe(new WhereObservable<TValue>.Where(this, observer));
		}

		// Token: 0x04000C8E RID: 3214
		private readonly IObservable<TValue> m_Source;

		// Token: 0x04000C8F RID: 3215
		private readonly Func<TValue, bool> m_Predicate;

		// Token: 0x02000258 RID: 600
		private class Where : IObserver<TValue>
		{
			// Token: 0x060015AF RID: 5551 RVA: 0x00062913 File Offset: 0x00060B13
			public Where(WhereObservable<TValue> observable, IObserver<TValue> observer)
			{
				this.m_Observable = observable;
				this.m_Observer = observer;
			}

			// Token: 0x060015B0 RID: 5552 RVA: 0x000049FE File Offset: 0x00002BFE
			public void OnCompleted()
			{
			}

			// Token: 0x060015B1 RID: 5553 RVA: 0x00054AD1 File Offset: 0x00052CD1
			public void OnError(Exception error)
			{
				Debug.LogException(error);
			}

			// Token: 0x060015B2 RID: 5554 RVA: 0x00062929 File Offset: 0x00060B29
			public void OnNext(TValue evt)
			{
				if (this.m_Observable.m_Predicate(evt))
				{
					this.m_Observer.OnNext(evt);
				}
			}

			// Token: 0x04000C90 RID: 3216
			private WhereObservable<TValue> m_Observable;

			// Token: 0x04000C91 RID: 3217
			private readonly IObserver<TValue> m_Observer;
		}
	}
}
