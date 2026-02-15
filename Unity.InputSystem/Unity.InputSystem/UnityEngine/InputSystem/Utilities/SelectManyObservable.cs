using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000253 RID: 595
	internal class SelectManyObservable<TSource, TResult> : IObservable<TResult>
	{
		// Token: 0x060015A1 RID: 5537 RVA: 0x000627C4 File Offset: 0x000609C4
		public SelectManyObservable(IObservable<TSource> source, Func<TSource, IEnumerable<TResult>> filter)
		{
			this.m_Source = source;
			this.m_Filter = filter;
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x000627DA File Offset: 0x000609DA
		public IDisposable Subscribe(IObserver<TResult> observer)
		{
			return this.m_Source.Subscribe(new SelectManyObservable<TSource, TResult>.Select(this, observer));
		}

		// Token: 0x04000C86 RID: 3206
		private readonly IObservable<TSource> m_Source;

		// Token: 0x04000C87 RID: 3207
		private readonly Func<TSource, IEnumerable<TResult>> m_Filter;

		// Token: 0x02000254 RID: 596
		private class Select : IObserver<TSource>
		{
			// Token: 0x060015A3 RID: 5539 RVA: 0x000627EE File Offset: 0x000609EE
			public Select(SelectManyObservable<TSource, TResult> observable, IObserver<TResult> observer)
			{
				this.m_Observable = observable;
				this.m_Observer = observer;
			}

			// Token: 0x060015A4 RID: 5540 RVA: 0x000049FE File Offset: 0x00002BFE
			public void OnCompleted()
			{
			}

			// Token: 0x060015A5 RID: 5541 RVA: 0x00054AD1 File Offset: 0x00052CD1
			public void OnError(Exception error)
			{
				Debug.LogException(error);
			}

			// Token: 0x060015A6 RID: 5542 RVA: 0x00062804 File Offset: 0x00060A04
			public void OnNext(TSource evt)
			{
				foreach (TResult result in this.m_Observable.m_Filter(evt))
				{
					this.m_Observer.OnNext(result);
				}
			}

			// Token: 0x04000C88 RID: 3208
			private SelectManyObservable<TSource, TResult> m_Observable;

			// Token: 0x04000C89 RID: 3209
			private readonly IObserver<TResult> m_Observer;
		}
	}
}
