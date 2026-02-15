using System;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200014C RID: 332
	public static class UniTaskObservableExtensions
	{
		// Token: 0x060007C7 RID: 1991 RVA: 0x000255CC File Offset: 0x000237CC
		public static UniTask<T> ToUniTask<T>(this IObservable<T> source, bool useFirstValue = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			UniTaskCompletionSource<T> promise = new UniTaskCompletionSource<T>();
			SingleAssignmentDisposable disposable = new SingleAssignmentDisposable();
			IObserver<T> observer3;
			if (!useFirstValue)
			{
				IObserver<T> observer2 = new UniTaskObservableExtensions.ToUniTaskObserver<T>(promise, disposable, cancellationToken);
				observer3 = observer2;
			}
			else
			{
				IObserver<T> observer2 = new UniTaskObservableExtensions.FirstValueToUniTaskObserver<T>(promise, disposable, cancellationToken);
				observer3 = observer2;
			}
			IObserver<T> observer = observer3;
			try
			{
				disposable.Disposable = source.Subscribe(observer);
			}
			catch (Exception ex)
			{
				promise.TrySetException(ex);
			}
			return promise.Task;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00025634 File Offset: 0x00023834
		public static IObservable<T> ToObservable<T>(this UniTask<T> task)
		{
			if (task.Status.IsCompleted())
			{
				try
				{
					return new UniTaskObservableExtensions.ReturnObservable<T>(task.GetAwaiter().GetResult());
				}
				catch (Exception ex)
				{
					return new UniTaskObservableExtensions.ThrowObservable<T>(ex);
				}
			}
			AsyncSubject<T> asyncSubject = new AsyncSubject<T>();
			UniTaskObservableExtensions.Fire<T>(asyncSubject, task).Forget();
			return asyncSubject;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00025694 File Offset: 0x00023894
		public static IObservable<AsyncUnit> ToObservable(this UniTask task)
		{
			if (task.Status.IsCompleted())
			{
				try
				{
					task.GetAwaiter().GetResult();
					return new UniTaskObservableExtensions.ReturnObservable<AsyncUnit>(AsyncUnit.Default);
				}
				catch (Exception ex)
				{
					return new UniTaskObservableExtensions.ThrowObservable<AsyncUnit>(ex);
				}
			}
			AsyncSubject<AsyncUnit> asyncSubject = new AsyncSubject<AsyncUnit>();
			UniTaskObservableExtensions.Fire(asyncSubject, task).Forget();
			return asyncSubject;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x000256FC File Offset: 0x000238FC
		private static async UniTaskVoid Fire<T>(AsyncSubject<T> subject, UniTask<T> task)
		{
			T value;
			try
			{
				value = await task;
			}
			catch (Exception ex)
			{
				subject.OnError(ex);
				return;
			}
			subject.OnNext(value);
			subject.OnCompleted();
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00025748 File Offset: 0x00023948
		private static async UniTaskVoid Fire(AsyncSubject<AsyncUnit> subject, UniTask task)
		{
			try
			{
				await task;
			}
			catch (Exception ex)
			{
				subject.OnError(ex);
				return;
			}
			subject.OnNext(AsyncUnit.Default);
			subject.OnCompleted();
		}

		// Token: 0x0200014D RID: 333
		private class ToUniTaskObserver<T> : IObserver<T>
		{
			// Token: 0x060007CC RID: 1996 RVA: 0x00025794 File Offset: 0x00023994
			public ToUniTaskObserver(UniTaskCompletionSource<T> promise, SingleAssignmentDisposable disposable, CancellationToken cancellationToken)
			{
				this.promise = promise;
				this.disposable = disposable;
				this.cancellationToken = cancellationToken;
				if (this.cancellationToken.CanBeCanceled)
				{
					this.registration = this.cancellationToken.RegisterWithoutCaptureExecutionContext(UniTaskObservableExtensions.ToUniTaskObserver<T>.callback, this);
				}
			}

			// Token: 0x060007CD RID: 1997 RVA: 0x000257E0 File Offset: 0x000239E0
			private static void OnCanceled(object state)
			{
				UniTaskObservableExtensions.ToUniTaskObserver<T> self = (UniTaskObservableExtensions.ToUniTaskObserver<T>)state;
				self.disposable.Dispose();
				self.promise.TrySetCanceled(self.cancellationToken);
			}

			// Token: 0x060007CE RID: 1998 RVA: 0x00025811 File Offset: 0x00023A11
			public void OnNext(T value)
			{
				this.hasValue = true;
				this.latestValue = value;
			}

			// Token: 0x060007CF RID: 1999 RVA: 0x00025824 File Offset: 0x00023A24
			public void OnError(Exception error)
			{
				try
				{
					this.promise.TrySetException(error);
				}
				finally
				{
					this.registration.Dispose();
					this.disposable.Dispose();
				}
			}

			// Token: 0x060007D0 RID: 2000 RVA: 0x0002586C File Offset: 0x00023A6C
			public void OnCompleted()
			{
				try
				{
					if (this.hasValue)
					{
						this.promise.TrySetResult(this.latestValue);
					}
					else
					{
						this.promise.TrySetException(new InvalidOperationException("Sequence has no elements"));
					}
				}
				finally
				{
					this.registration.Dispose();
					this.disposable.Dispose();
				}
			}

			// Token: 0x04000529 RID: 1321
			private static readonly Action<object> callback = new Action<object>(UniTaskObservableExtensions.ToUniTaskObserver<T>.OnCanceled);

			// Token: 0x0400052A RID: 1322
			private readonly UniTaskCompletionSource<T> promise;

			// Token: 0x0400052B RID: 1323
			private readonly SingleAssignmentDisposable disposable;

			// Token: 0x0400052C RID: 1324
			private readonly CancellationToken cancellationToken;

			// Token: 0x0400052D RID: 1325
			private readonly CancellationTokenRegistration registration;

			// Token: 0x0400052E RID: 1326
			private bool hasValue;

			// Token: 0x0400052F RID: 1327
			private T latestValue;
		}

		// Token: 0x0200014E RID: 334
		private class FirstValueToUniTaskObserver<T> : IObserver<T>
		{
			// Token: 0x060007D2 RID: 2002 RVA: 0x000258EC File Offset: 0x00023AEC
			public FirstValueToUniTaskObserver(UniTaskCompletionSource<T> promise, SingleAssignmentDisposable disposable, CancellationToken cancellationToken)
			{
				this.promise = promise;
				this.disposable = disposable;
				this.cancellationToken = cancellationToken;
				if (this.cancellationToken.CanBeCanceled)
				{
					this.registration = this.cancellationToken.RegisterWithoutCaptureExecutionContext(UniTaskObservableExtensions.FirstValueToUniTaskObserver<T>.callback, this);
				}
			}

			// Token: 0x060007D3 RID: 2003 RVA: 0x00025938 File Offset: 0x00023B38
			private static void OnCanceled(object state)
			{
				UniTaskObservableExtensions.FirstValueToUniTaskObserver<T> self = (UniTaskObservableExtensions.FirstValueToUniTaskObserver<T>)state;
				self.disposable.Dispose();
				self.promise.TrySetCanceled(self.cancellationToken);
			}

			// Token: 0x060007D4 RID: 2004 RVA: 0x0002596C File Offset: 0x00023B6C
			public void OnNext(T value)
			{
				this.hasValue = true;
				try
				{
					this.promise.TrySetResult(value);
				}
				finally
				{
					this.registration.Dispose();
					this.disposable.Dispose();
				}
			}

			// Token: 0x060007D5 RID: 2005 RVA: 0x000259BC File Offset: 0x00023BBC
			public void OnError(Exception error)
			{
				try
				{
					this.promise.TrySetException(error);
				}
				finally
				{
					this.registration.Dispose();
					this.disposable.Dispose();
				}
			}

			// Token: 0x060007D6 RID: 2006 RVA: 0x00025A04 File Offset: 0x00023C04
			public void OnCompleted()
			{
				try
				{
					if (!this.hasValue)
					{
						this.promise.TrySetException(new InvalidOperationException("Sequence has no elements"));
					}
				}
				finally
				{
					this.registration.Dispose();
					this.disposable.Dispose();
				}
			}

			// Token: 0x04000530 RID: 1328
			private static readonly Action<object> callback = new Action<object>(UniTaskObservableExtensions.FirstValueToUniTaskObserver<T>.OnCanceled);

			// Token: 0x04000531 RID: 1329
			private readonly UniTaskCompletionSource<T> promise;

			// Token: 0x04000532 RID: 1330
			private readonly SingleAssignmentDisposable disposable;

			// Token: 0x04000533 RID: 1331
			private readonly CancellationToken cancellationToken;

			// Token: 0x04000534 RID: 1332
			private readonly CancellationTokenRegistration registration;

			// Token: 0x04000535 RID: 1333
			private bool hasValue;
		}

		// Token: 0x0200014F RID: 335
		private class ReturnObservable<T> : IObservable<T>
		{
			// Token: 0x060007D8 RID: 2008 RVA: 0x00025A6F File Offset: 0x00023C6F
			public ReturnObservable(T value)
			{
				this.value = value;
			}

			// Token: 0x060007D9 RID: 2009 RVA: 0x00025A7E File Offset: 0x00023C7E
			public IDisposable Subscribe(IObserver<T> observer)
			{
				observer.OnNext(this.value);
				observer.OnCompleted();
				return EmptyDisposable.Instance;
			}

			// Token: 0x04000536 RID: 1334
			private readonly T value;
		}

		// Token: 0x02000150 RID: 336
		private class ThrowObservable<T> : IObservable<T>
		{
			// Token: 0x060007DA RID: 2010 RVA: 0x00025A97 File Offset: 0x00023C97
			public ThrowObservable(Exception value)
			{
				this.value = value;
			}

			// Token: 0x060007DB RID: 2011 RVA: 0x00025AA6 File Offset: 0x00023CA6
			public IDisposable Subscribe(IObserver<T> observer)
			{
				observer.OnError(this.value);
				return EmptyDisposable.Instance;
			}

			// Token: 0x04000537 RID: 1335
			private readonly Exception value;
		}
	}
}
