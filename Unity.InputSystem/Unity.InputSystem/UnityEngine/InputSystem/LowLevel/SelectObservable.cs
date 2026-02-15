using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001E3 RID: 483
	internal class SelectObservable<TSource, TResult> : IObservable<TResult>
	{
		// Token: 0x06001205 RID: 4613 RVA: 0x00054A91 File Offset: 0x00052C91
		public SelectObservable(IObservable<TSource> source, Func<TSource, TResult> filter)
		{
			this.m_Source = source;
			this.m_Filter = filter;
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00054AA7 File Offset: 0x00052CA7
		public IDisposable Subscribe(IObserver<TResult> observer)
		{
			return this.m_Source.Subscribe(new SelectObservable<TSource, TResult>.Select(this, observer));
		}

		// Token: 0x04000AC8 RID: 2760
		private readonly IObservable<TSource> m_Source;

		// Token: 0x04000AC9 RID: 2761
		private readonly Func<TSource, TResult> m_Filter;

		// Token: 0x020001E4 RID: 484
		private class Select : IObserver<TSource>
		{
			// Token: 0x06001207 RID: 4615 RVA: 0x00054ABB File Offset: 0x00052CBB
			public Select(SelectObservable<TSource, TResult> observable, IObserver<TResult> observer)
			{
				this.m_Observable = observable;
				this.m_Observer = observer;
			}

			// Token: 0x06001208 RID: 4616 RVA: 0x000049FE File Offset: 0x00002BFE
			public void OnCompleted()
			{
			}

			// Token: 0x06001209 RID: 4617 RVA: 0x00054AD1 File Offset: 0x00052CD1
			public void OnError(Exception error)
			{
				Debug.LogException(error);
			}

			// Token: 0x0600120A RID: 4618 RVA: 0x00054ADC File Offset: 0x00052CDC
			public void OnNext(TSource evt)
			{
				TResult result = this.m_Observable.m_Filter(evt);
				this.m_Observer.OnNext(result);
			}

			// Token: 0x04000ACA RID: 2762
			private SelectObservable<TSource, TResult> m_Observable;

			// Token: 0x04000ACB RID: 2763
			private readonly IObserver<TResult> m_Observer;
		}
	}
}
