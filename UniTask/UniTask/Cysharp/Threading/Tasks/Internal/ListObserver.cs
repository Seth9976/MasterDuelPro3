using System;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000257 RID: 599
	internal class ListObserver<T> : IObserver<T>
	{
		// Token: 0x06000D5F RID: 3423 RVA: 0x0002EA54 File Offset: 0x0002CC54
		public ListObserver(ImmutableList<IObserver<T>> observers)
		{
			this._observers = observers;
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0002EA64 File Offset: 0x0002CC64
		public void OnCompleted()
		{
			IObserver<T>[] targetObservers = this._observers.Data;
			for (int i = 0; i < targetObservers.Length; i++)
			{
				targetObservers[i].OnCompleted();
			}
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0002EA94 File Offset: 0x0002CC94
		public void OnError(Exception error)
		{
			IObserver<T>[] targetObservers = this._observers.Data;
			for (int i = 0; i < targetObservers.Length; i++)
			{
				targetObservers[i].OnError(error);
			}
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0002EAC4 File Offset: 0x0002CCC4
		public void OnNext(T value)
		{
			IObserver<T>[] targetObservers = this._observers.Data;
			for (int i = 0; i < targetObservers.Length; i++)
			{
				targetObservers[i].OnNext(value);
			}
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0002EAF4 File Offset: 0x0002CCF4
		internal IObserver<T> Add(IObserver<T> observer)
		{
			return new ListObserver<T>(this._observers.Add(observer));
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0002EB08 File Offset: 0x0002CD08
		internal IObserver<T> Remove(IObserver<T> observer)
		{
			int i = Array.IndexOf<IObserver<T>>(this._observers.Data, observer);
			if (i < 0)
			{
				return this;
			}
			if (this._observers.Data.Length == 2)
			{
				return this._observers.Data[1 - i];
			}
			return new ListObserver<T>(this._observers.Remove(observer));
		}

		// Token: 0x040006C3 RID: 1731
		private readonly ImmutableList<IObserver<T>> _observers;
	}
}
