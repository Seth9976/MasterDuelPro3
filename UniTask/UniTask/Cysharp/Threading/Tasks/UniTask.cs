using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Cysharp.Threading.Tasks.CompilerServices;
using Cysharp.Threading.Tasks.Internal;
using UnityEngine;
using UnityEngine.Events;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000078 RID: 120
	[global::System.Runtime.CompilerServices.AsyncMethodBuilder(typeof(AsyncUniTaskMethodBuilder))]
	[StructLayout(LayoutKind.Auto)]
	public readonly struct UniTask
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x000060C2 File Offset: 0x000042C2
		public static IEnumerator ToCoroutine(Func<UniTask> taskFactory)
		{
			return taskFactory().ToCoroutine(null);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000060D0 File Offset: 0x000042D0
		public static YieldAwaitable Yield()
		{
			return new YieldAwaitable(PlayerLoopTiming.Update);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000060D8 File Offset: 0x000042D8
		public static YieldAwaitable Yield(PlayerLoopTiming timing)
		{
			return new YieldAwaitable(timing);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000060E0 File Offset: 0x000042E0
		public static UniTask Yield(CancellationToken cancellationToken, bool cancelImmediately = false)
		{
			short token;
			return new UniTask(UniTask.YieldPromise.Create(PlayerLoopTiming.Update, cancellationToken, cancelImmediately, out token), token);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006100 File Offset: 0x00004300
		public static UniTask Yield(PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately = false)
		{
			short token;
			return new UniTask(UniTask.YieldPromise.Create(timing, cancellationToken, cancelImmediately, out token), token);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006120 File Offset: 0x00004320
		public static UniTask NextFrame()
		{
			short token;
			return new UniTask(UniTask.NextFramePromise.Create(PlayerLoopTiming.Update, CancellationToken.None, false, out token), token);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00006144 File Offset: 0x00004344
		public static UniTask NextFrame(PlayerLoopTiming timing)
		{
			short token;
			return new UniTask(UniTask.NextFramePromise.Create(timing, CancellationToken.None, false, out token), token);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00006168 File Offset: 0x00004368
		public static UniTask NextFrame(CancellationToken cancellationToken, bool cancelImmediately = false)
		{
			short token;
			return new UniTask(UniTask.NextFramePromise.Create(PlayerLoopTiming.Update, cancellationToken, cancelImmediately, out token), token);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00006188 File Offset: 0x00004388
		public static UniTask NextFrame(PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately = false)
		{
			short token;
			return new UniTask(UniTask.NextFramePromise.Create(timing, cancellationToken, cancelImmediately, out token), token);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000061A8 File Offset: 0x000043A8
		public static async UniTask WaitForEndOfFrame(CancellationToken cancellationToken = default(CancellationToken))
		{
			await Awaitable.EndOfFrameAsync(cancellationToken);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000061EC File Offset: 0x000043EC
		public static UniTask WaitForEndOfFrame(MonoBehaviour coroutineRunner)
		{
			short token;
			return new UniTask(UniTask.WaitForEndOfFramePromise.Create(coroutineRunner, CancellationToken.None, false, out token), token);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006210 File Offset: 0x00004410
		public static UniTask WaitForEndOfFrame(MonoBehaviour coroutineRunner, CancellationToken cancellationToken, bool cancelImmediately = false)
		{
			short token;
			return new UniTask(UniTask.WaitForEndOfFramePromise.Create(coroutineRunner, cancellationToken, cancelImmediately, out token), token);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000622D File Offset: 0x0000442D
		public static YieldAwaitable WaitForFixedUpdate()
		{
			return UniTask.Yield(PlayerLoopTiming.LastFixedUpdate);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00006235 File Offset: 0x00004435
		public static UniTask WaitForFixedUpdate(CancellationToken cancellationToken, bool cancelImmediately = false)
		{
			return UniTask.Yield(PlayerLoopTiming.LastFixedUpdate, cancellationToken, cancelImmediately);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000623F File Offset: 0x0000443F
		public static UniTask WaitForSeconds(float duration, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			return UniTask.Delay(Mathf.RoundToInt(1000f * duration), ignoreTimeScale, delayTiming, cancellationToken, cancelImmediately);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006257 File Offset: 0x00004457
		public static UniTask WaitForSeconds(int duration, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			return UniTask.Delay(1000 * duration, ignoreTimeScale, delayTiming, cancellationToken, cancelImmediately);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000626C File Offset: 0x0000446C
		public static UniTask DelayFrame(int delayFrameCount, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			if (delayFrameCount < 0)
			{
				throw new ArgumentOutOfRangeException("Delay does not allow minus delayFrameCount. delayFrameCount:" + delayFrameCount.ToString());
			}
			short token;
			return new UniTask(UniTask.DelayFramePromise.Create(delayFrameCount, delayTiming, cancellationToken, cancelImmediately, out token), token);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000062A5 File Offset: 0x000044A5
		public static UniTask Delay(int millisecondsDelay, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			return UniTask.Delay(TimeSpan.FromMilliseconds((double)millisecondsDelay), ignoreTimeScale, delayTiming, cancellationToken, cancelImmediately);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000062B8 File Offset: 0x000044B8
		public static UniTask Delay(TimeSpan delayTimeSpan, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			DelayType delayType = (ignoreTimeScale ? DelayType.UnscaledDeltaTime : DelayType.DeltaTime);
			return UniTask.Delay(delayTimeSpan, delayType, delayTiming, cancellationToken, cancelImmediately);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000062D8 File Offset: 0x000044D8
		public static UniTask Delay(int millisecondsDelay, DelayType delayType, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			return UniTask.Delay(TimeSpan.FromMilliseconds((double)millisecondsDelay), delayType, delayTiming, cancellationToken, cancelImmediately);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000062EC File Offset: 0x000044EC
		public static UniTask Delay(TimeSpan delayTimeSpan, DelayType delayType, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			if (delayTimeSpan < TimeSpan.Zero)
			{
				string text = "Delay does not allow minus delayTimeSpan. delayTimeSpan:";
				TimeSpan timeSpan = delayTimeSpan;
				throw new ArgumentOutOfRangeException(text + timeSpan.ToString());
			}
			switch (delayType)
			{
			case DelayType.UnscaledDeltaTime:
			{
				short token;
				return new UniTask(UniTask.DelayIgnoreTimeScalePromise.Create(delayTimeSpan, delayTiming, cancellationToken, cancelImmediately, out token), token);
			}
			case DelayType.Realtime:
			{
				short token2;
				return new UniTask(UniTask.DelayRealtimePromise.Create(delayTimeSpan, delayTiming, cancellationToken, cancelImmediately, out token2), token2);
			}
			}
			short token3;
			return new UniTask(UniTask.DelayPromise.Create(delayTimeSpan, delayTiming, cancellationToken, cancelImmediately, out token3), token3);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00006374 File Offset: 0x00004574
		public static UniTask FromException(Exception ex)
		{
			OperationCanceledException oce = ex as OperationCanceledException;
			if (oce != null)
			{
				return UniTask.FromCanceled(oce.CancellationToken);
			}
			return new UniTask(new UniTask.ExceptionResultSource(ex), 0);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000063A4 File Offset: 0x000045A4
		public static UniTask<T> FromException<T>(Exception ex)
		{
			OperationCanceledException oce = ex as OperationCanceledException;
			if (oce != null)
			{
				return UniTask.FromCanceled<T>(oce.CancellationToken);
			}
			return new UniTask<T>(new UniTask.ExceptionResultSource<T>(ex), 0);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000063D3 File Offset: 0x000045D3
		public static UniTask<T> FromResult<T>(T value)
		{
			return new UniTask<T>(value);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000063DB File Offset: 0x000045DB
		public static UniTask FromCanceled(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken == CancellationToken.None)
			{
				return UniTask.CanceledUniTask;
			}
			return new UniTask(new UniTask.CanceledResultSource(cancellationToken), 0);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000063FC File Offset: 0x000045FC
		public static UniTask<T> FromCanceled<T>(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken == CancellationToken.None)
			{
				return UniTask.CanceledUniTaskCache<T>.Task;
			}
			return new UniTask<T>(new UniTask.CanceledResultSource<T>(cancellationToken), 0);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000641D File Offset: 0x0000461D
		public static UniTask Create(Func<UniTask> factory)
		{
			return factory();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00006425 File Offset: 0x00004625
		public static UniTask Create(Func<CancellationToken, UniTask> factory, CancellationToken cancellationToken)
		{
			return factory(cancellationToken);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000642E File Offset: 0x0000462E
		public static UniTask Create<T>(T state, Func<T, UniTask> factory)
		{
			return factory(state);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00006437 File Offset: 0x00004637
		public static UniTask<T> Create<T>(Func<UniTask<T>> factory)
		{
			return factory();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000643F File Offset: 0x0000463F
		public static AsyncLazy Lazy(Func<UniTask> factory)
		{
			return new AsyncLazy(factory);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00006447 File Offset: 0x00004647
		public static AsyncLazy<T> Lazy<T>(Func<UniTask<T>> factory)
		{
			return new AsyncLazy<T>(factory);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00006450 File Offset: 0x00004650
		public static void Void(Func<UniTaskVoid> asyncAction)
		{
			asyncAction().Forget();
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000646C File Offset: 0x0000466C
		public static void Void(Func<CancellationToken, UniTaskVoid> asyncAction, CancellationToken cancellationToken)
		{
			asyncAction(cancellationToken).Forget();
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00006488 File Offset: 0x00004688
		public static void Void<T>(Func<T, UniTaskVoid> asyncAction, T state)
		{
			asyncAction(state).Forget();
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000064A4 File Offset: 0x000046A4
		public static Action Action(Func<UniTaskVoid> asyncAction)
		{
			return delegate
			{
				asyncAction().Forget();
			};
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000064BD File Offset: 0x000046BD
		public static Action Action(Func<CancellationToken, UniTaskVoid> asyncAction, CancellationToken cancellationToken)
		{
			return delegate
			{
				asyncAction(cancellationToken).Forget();
			};
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000064DD File Offset: 0x000046DD
		public static Action Action<T>(T state, Func<T, UniTaskVoid> asyncAction)
		{
			return delegate
			{
				asyncAction(state).Forget();
			};
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000064FD File Offset: 0x000046FD
		public static UnityAction UnityAction(Func<UniTaskVoid> asyncAction)
		{
			return delegate
			{
				asyncAction().Forget();
			};
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00006516 File Offset: 0x00004716
		public static UnityAction UnityAction(Func<CancellationToken, UniTaskVoid> asyncAction, CancellationToken cancellationToken)
		{
			return delegate
			{
				asyncAction(cancellationToken).Forget();
			};
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00006536 File Offset: 0x00004736
		public static UnityAction UnityAction<T>(T state, Func<T, UniTaskVoid> asyncAction)
		{
			return delegate
			{
				asyncAction(state).Forget();
			};
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00006556 File Offset: 0x00004756
		public static UnityAction<T> UnityAction<T>(Func<T, UniTaskVoid> asyncAction)
		{
			return delegate(T arg)
			{
				asyncAction(arg).Forget();
			};
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000656F File Offset: 0x0000476F
		public static UnityAction<T0, T1> UnityAction<T0, T1>(Func<T0, T1, UniTaskVoid> asyncAction)
		{
			return delegate(T0 arg0, T1 arg1)
			{
				asyncAction(arg0, arg1).Forget();
			};
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00006588 File Offset: 0x00004788
		public static UnityAction<T0, T1, T2> UnityAction<T0, T1, T2>(Func<T0, T1, T2, UniTaskVoid> asyncAction)
		{
			return delegate(T0 arg0, T1 arg1, T2 arg2)
			{
				asyncAction(arg0, arg1, arg2).Forget();
			};
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000065A1 File Offset: 0x000047A1
		public static UnityAction<T0, T1, T2, T3> UnityAction<T0, T1, T2, T3>(Func<T0, T1, T2, T3, UniTaskVoid> asyncAction)
		{
			return delegate(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
			{
				asyncAction(arg0, arg1, arg2, arg3).Forget();
			};
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000065BA File Offset: 0x000047BA
		public static UnityAction<T> UnityAction<T>(Func<T, CancellationToken, UniTaskVoid> asyncAction, CancellationToken cancellationToken)
		{
			return delegate(T arg)
			{
				asyncAction(arg, cancellationToken).Forget();
			};
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000065DA File Offset: 0x000047DA
		public static UnityAction<T0, T1> UnityAction<T0, T1>(Func<T0, T1, CancellationToken, UniTaskVoid> asyncAction, CancellationToken cancellationToken)
		{
			return delegate(T0 arg0, T1 arg1)
			{
				asyncAction(arg0, arg1, cancellationToken).Forget();
			};
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000065FA File Offset: 0x000047FA
		public static UnityAction<T0, T1, T2> UnityAction<T0, T1, T2>(Func<T0, T1, T2, CancellationToken, UniTaskVoid> asyncAction, CancellationToken cancellationToken)
		{
			return delegate(T0 arg0, T1 arg1, T2 arg2)
			{
				asyncAction(arg0, arg1, arg2, cancellationToken).Forget();
			};
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000661A File Offset: 0x0000481A
		public static UnityAction<T0, T1, T2, T3> UnityAction<T0, T1, T2, T3>(Func<T0, T1, T2, T3, CancellationToken, UniTaskVoid> asyncAction, CancellationToken cancellationToken)
		{
			return delegate(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
			{
				asyncAction(arg0, arg1, arg2, arg3, cancellationToken).Forget();
			};
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000663A File Offset: 0x0000483A
		public static UniTask Defer(Func<UniTask> factory)
		{
			return new UniTask(new UniTask.DeferPromise(factory), 0);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006648 File Offset: 0x00004848
		public static UniTask<T> Defer<T>(Func<UniTask<T>> factory)
		{
			return new UniTask<T>(new UniTask.DeferPromise<T>(factory), 0);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00006656 File Offset: 0x00004856
		public static UniTask Defer<TState>(TState state, Func<TState, UniTask> factory)
		{
			return new UniTask(new UniTask.DeferPromiseWithState<TState>(state, factory), 0);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00006665 File Offset: 0x00004865
		public static UniTask<TResult> Defer<TState, TResult>(TState state, Func<TState, UniTask<TResult>> factory)
		{
			return new UniTask<TResult>(new UniTask.DeferPromiseWithState<TState, TResult>(state, factory), 0);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00006674 File Offset: 0x00004874
		public static UniTask Never(CancellationToken cancellationToken)
		{
			return new UniTask<AsyncUnit>(new UniTask.NeverPromise<AsyncUnit>(cancellationToken), 0);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006687 File Offset: 0x00004887
		public static UniTask<T> Never<T>(CancellationToken cancellationToken)
		{
			return new UniTask<T>(new UniTask.NeverPromise<T>(cancellationToken), 0);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006695 File Offset: 0x00004895
		[Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
		public static UniTask Run(Action action, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			return UniTask.RunOnThreadPool(action, configureAwait, cancellationToken);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000669F File Offset: 0x0000489F
		[Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
		public static UniTask Run(Action<object> action, object state, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			return UniTask.RunOnThreadPool(action, state, configureAwait, cancellationToken);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000066AA File Offset: 0x000048AA
		[Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
		public static UniTask Run(Func<UniTask> action, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			return UniTask.RunOnThreadPool(action, configureAwait, cancellationToken);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000066B4 File Offset: 0x000048B4
		[Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
		public static UniTask Run(Func<object, UniTask> action, object state, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			return UniTask.RunOnThreadPool(action, state, configureAwait, cancellationToken);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000066BF File Offset: 0x000048BF
		[Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
		public static UniTask<T> Run<T>(Func<T> func, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			return UniTask.RunOnThreadPool<T>(func, configureAwait, cancellationToken);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000066C9 File Offset: 0x000048C9
		[Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
		public static UniTask<T> Run<T>(Func<UniTask<T>> func, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			return UniTask.RunOnThreadPool<T>(func, configureAwait, cancellationToken);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000066D3 File Offset: 0x000048D3
		[Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
		public static UniTask<T> Run<T>(Func<object, T> func, object state, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			return UniTask.RunOnThreadPool<T>(func, state, configureAwait, cancellationToken);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000066DE File Offset: 0x000048DE
		[Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
		public static UniTask<T> Run<T>(Func<object, UniTask<T>> func, object state, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			return UniTask.RunOnThreadPool<T>(func, state, configureAwait, cancellationToken);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000066EC File Offset: 0x000048EC
		public static async UniTask RunOnThreadPool(Action action, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			await UniTask.SwitchToThreadPool();
			cancellationToken.ThrowIfCancellationRequested();
			if (configureAwait)
			{
				object obj = null;
				try
				{
					action();
				}
				catch (object obj)
				{
				}
				await UniTask.Yield();
				object obj2 = obj;
				if (obj2 != null)
				{
					Exception ex = obj2 as Exception;
					if (ex == null)
					{
						throw obj2;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				obj = null;
			}
			else
			{
				action();
			}
			cancellationToken.ThrowIfCancellationRequested();
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006740 File Offset: 0x00004940
		public static async UniTask RunOnThreadPool(Action<object> action, object state, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			await UniTask.SwitchToThreadPool();
			cancellationToken.ThrowIfCancellationRequested();
			if (configureAwait)
			{
				object obj = null;
				try
				{
					action(state);
				}
				catch (object obj)
				{
				}
				await UniTask.Yield();
				object obj2 = obj;
				if (obj2 != null)
				{
					Exception ex = obj2 as Exception;
					if (ex == null)
					{
						throw obj2;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				obj = null;
			}
			else
			{
				action(state);
			}
			cancellationToken.ThrowIfCancellationRequested();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000679C File Offset: 0x0000499C
		public static async UniTask RunOnThreadPool(Func<UniTask> action, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			await UniTask.SwitchToThreadPool();
			cancellationToken.ThrowIfCancellationRequested();
			if (configureAwait)
			{
				object obj = null;
				try
				{
					await action();
				}
				catch (object obj)
				{
				}
				await UniTask.Yield();
				object obj2 = obj;
				if (obj2 != null)
				{
					Exception ex = obj2 as Exception;
					if (ex == null)
					{
						throw obj2;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				obj = null;
			}
			else
			{
				await action();
			}
			cancellationToken.ThrowIfCancellationRequested();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000067F0 File Offset: 0x000049F0
		public static async UniTask RunOnThreadPool(Func<object, UniTask> action, object state, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			await UniTask.SwitchToThreadPool();
			cancellationToken.ThrowIfCancellationRequested();
			if (configureAwait)
			{
				object obj = null;
				try
				{
					await action(state);
				}
				catch (object obj)
				{
				}
				await UniTask.Yield();
				object obj2 = obj;
				if (obj2 != null)
				{
					Exception ex = obj2 as Exception;
					if (ex == null)
					{
						throw obj2;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				obj = null;
			}
			else
			{
				await action(state);
			}
			cancellationToken.ThrowIfCancellationRequested();
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000684C File Offset: 0x00004A4C
		public static async UniTask<T> RunOnThreadPool<T>(Func<T> func, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			await UniTask.SwitchToThreadPool();
			cancellationToken.ThrowIfCancellationRequested();
			T t2;
			if (configureAwait)
			{
				object obj = null;
				int num = 0;
				T t;
				try
				{
					t = func();
					num = 1;
				}
				catch (object obj)
				{
				}
				await UniTask.Yield();
				cancellationToken.ThrowIfCancellationRequested();
				object obj2 = obj;
				if (obj2 != null)
				{
					Exception ex = obj2 as Exception;
					if (ex == null)
					{
						throw obj2;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				if (num == 1)
				{
					t2 = t;
				}
				else
				{
					obj = null;
					t = default(T);
				}
			}
			else
			{
				t2 = func();
			}
			return t2;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000068A0 File Offset: 0x00004AA0
		public static async UniTask<T> RunOnThreadPool<T>(Func<UniTask<T>> func, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			await UniTask.SwitchToThreadPool();
			cancellationToken.ThrowIfCancellationRequested();
			T t2;
			if (configureAwait)
			{
				object obj = null;
				int num = 0;
				T t;
				try
				{
					t = await func();
					num = 1;
				}
				catch (object obj)
				{
				}
				cancellationToken.ThrowIfCancellationRequested();
				await UniTask.Yield();
				cancellationToken.ThrowIfCancellationRequested();
				object obj2 = obj;
				if (obj2 != null)
				{
					Exception ex = obj2 as Exception;
					if (ex == null)
					{
						throw obj2;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				if (num == 1)
				{
					t2 = t;
				}
				else
				{
					obj = null;
					t = default(T);
				}
			}
			else
			{
				T t3 = await func();
				cancellationToken.ThrowIfCancellationRequested();
				t2 = t3;
			}
			return t2;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000068F4 File Offset: 0x00004AF4
		public static async UniTask<T> RunOnThreadPool<T>(Func<object, T> func, object state, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			await UniTask.SwitchToThreadPool();
			cancellationToken.ThrowIfCancellationRequested();
			T t2;
			if (configureAwait)
			{
				object obj = null;
				int num = 0;
				T t;
				try
				{
					t = func(state);
					num = 1;
				}
				catch (object obj)
				{
				}
				await UniTask.Yield();
				cancellationToken.ThrowIfCancellationRequested();
				object obj2 = obj;
				if (obj2 != null)
				{
					Exception ex = obj2 as Exception;
					if (ex == null)
					{
						throw obj2;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				if (num == 1)
				{
					t2 = t;
				}
				else
				{
					obj = null;
					t = default(T);
				}
			}
			else
			{
				t2 = func(state);
			}
			return t2;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006950 File Offset: 0x00004B50
		public static async UniTask<T> RunOnThreadPool<T>(Func<object, UniTask<T>> func, object state, bool configureAwait = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			await UniTask.SwitchToThreadPool();
			cancellationToken.ThrowIfCancellationRequested();
			T t2;
			if (configureAwait)
			{
				object obj = null;
				int num = 0;
				T t;
				try
				{
					t = await func(state);
					num = 1;
				}
				catch (object obj)
				{
				}
				cancellationToken.ThrowIfCancellationRequested();
				await UniTask.Yield();
				cancellationToken.ThrowIfCancellationRequested();
				object obj2 = obj;
				if (obj2 != null)
				{
					Exception ex = obj2 as Exception;
					if (ex == null)
					{
						throw obj2;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				if (num == 1)
				{
					t2 = t;
				}
				else
				{
					obj = null;
					t = default(T);
				}
			}
			else
			{
				T t3 = await func(state);
				cancellationToken.ThrowIfCancellationRequested();
				t2 = t3;
			}
			return t2;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000069AB File Offset: 0x00004BAB
		public static SwitchToMainThreadAwaitable SwitchToMainThread(CancellationToken cancellationToken = default(CancellationToken))
		{
			return new SwitchToMainThreadAwaitable(PlayerLoopTiming.Update, cancellationToken);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000069B4 File Offset: 0x00004BB4
		public static SwitchToMainThreadAwaitable SwitchToMainThread(PlayerLoopTiming timing, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new SwitchToMainThreadAwaitable(timing, cancellationToken);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000069BD File Offset: 0x00004BBD
		public static ReturnToMainThread ReturnToMainThread(CancellationToken cancellationToken = default(CancellationToken))
		{
			return new ReturnToMainThread(PlayerLoopTiming.Update, cancellationToken);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000069C6 File Offset: 0x00004BC6
		public static ReturnToMainThread ReturnToMainThread(PlayerLoopTiming timing, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new ReturnToMainThread(timing, cancellationToken);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000069CF File Offset: 0x00004BCF
		public static void Post(Action action, PlayerLoopTiming timing = PlayerLoopTiming.Update)
		{
			PlayerLoopHelper.AddContinuation(timing, action);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000069D8 File Offset: 0x00004BD8
		public static SwitchToThreadPoolAwaitable SwitchToThreadPool()
		{
			return default(SwitchToThreadPoolAwaitable);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000069F0 File Offset: 0x00004BF0
		public static SwitchToTaskPoolAwaitable SwitchToTaskPool()
		{
			return default(SwitchToTaskPoolAwaitable);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006A06 File Offset: 0x00004C06
		public static SwitchToSynchronizationContextAwaitable SwitchToSynchronizationContext(SynchronizationContext synchronizationContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			Error.ThrowArgumentNullException<SynchronizationContext>(synchronizationContext, "synchronizationContext");
			return new SwitchToSynchronizationContextAwaitable(synchronizationContext, cancellationToken);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006A1A File Offset: 0x00004C1A
		public static ReturnToSynchronizationContext ReturnToSynchronizationContext(SynchronizationContext synchronizationContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new ReturnToSynchronizationContext(synchronizationContext, false, cancellationToken);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006A24 File Offset: 0x00004C24
		public static ReturnToSynchronizationContext ReturnToCurrentSynchronizationContext(bool dontPostWhenSameContext = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new ReturnToSynchronizationContext(SynchronizationContext.Current, dontPostWhenSameContext, cancellationToken);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00006A34 File Offset: 0x00004C34
		public static UniTask WaitUntil(Func<bool> predicate, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			short token;
			return new UniTask(UniTask.WaitUntilPromise.Create(predicate, timing, cancellationToken, cancelImmediately, out token), token);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00006A54 File Offset: 0x00004C54
		public static UniTask WaitUntil<T>(T state, Func<T, bool> predicate, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			short token;
			return new UniTask(UniTask.WaitUntilPromise<T>.Create(state, predicate, timing, cancellationToken, cancelImmediately, out token), token);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006A74 File Offset: 0x00004C74
		public static UniTask WaitWhile(Func<bool> predicate, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			short token;
			return new UniTask(UniTask.WaitWhilePromise.Create(predicate, timing, cancellationToken, cancelImmediately, out token), token);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006A94 File Offset: 0x00004C94
		public static UniTask WaitWhile<T>(T state, Func<T, bool> predicate, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			short token;
			return new UniTask(UniTask.WaitWhilePromise<T>.Create(state, predicate, timing, cancellationToken, cancelImmediately, out token), token);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006AB4 File Offset: 0x00004CB4
		public static UniTask WaitUntilCanceled(CancellationToken cancellationToken, PlayerLoopTiming timing = PlayerLoopTiming.Update, bool completeImmediately = false)
		{
			short token;
			return new UniTask(UniTask.WaitUntilCanceledPromise.Create(cancellationToken, timing, completeImmediately, out token), token);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006AD4 File Offset: 0x00004CD4
		public static UniTask<U> WaitUntilValueChanged<T, U>(T target, Func<T, U> monitorFunction, PlayerLoopTiming monitorTiming = PlayerLoopTiming.Update, IEqualityComparer<U> equalityComparer = null, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false) where T : class
		{
			short token;
			return new UniTask<U>((target is global::UnityEngine.Object) ? UniTask.WaitUntilValueChangedUnityObjectPromise<T, U>.Create(target, monitorFunction, equalityComparer, monitorTiming, cancellationToken, cancelImmediately, out token) : UniTask.WaitUntilValueChangedStandardObjectPromise<T, U>.Create(target, monitorFunction, equalityComparer, monitorTiming, cancellationToken, cancelImmediately, out token), token);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006B18 File Offset: 0x00004D18
		public static UniTask<ValueTuple<T1, T2>> WhenAll<T1, T2>(UniTask<T1> task1, UniTask<T2> task2)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2>>(new ValueTuple<T1, T2>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult()));
			}
			return new UniTask<ValueTuple<T1, T2>>(new UniTask.WhenAllPromise<T1, T2>(task1, task2), 0);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006B78 File Offset: 0x00004D78
		public static UniTask<ValueTuple<T1, T2, T3>> WhenAll<T1, T2, T3>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3>>(new ValueTuple<T1, T2, T3>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult()));
			}
			return new UniTask<ValueTuple<T1, T2, T3>>(new UniTask.WhenAllPromise<T1, T2, T3>(task1, task2, task3), 0);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006BF8 File Offset: 0x00004DF8
		public static UniTask<ValueTuple<T1, T2, T3, T4>> WhenAll<T1, T2, T3, T4>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4>>(new ValueTuple<T1, T2, T3, T4>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult()));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4>>(new UniTask.WhenAllPromise<T1, T2, T3, T4>(task1, task2, task3, task4), 0);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00006C94 File Offset: 0x00004E94
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5>> WhenAll<T1, T2, T3, T4, T5>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully() && task5.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4, T5>>(new ValueTuple<T1, T2, T3, T4, T5>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult(), task5.GetAwaiter().GetResult()));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4, T5>>(new UniTask.WhenAllPromise<T1, T2, T3, T4, T5>(task1, task2, task3, task4, task5), 0);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00006D58 File Offset: 0x00004F58
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6>> WhenAll<T1, T2, T3, T4, T5, T6>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully() && task5.Status.IsCompletedSuccessfully() && task6.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6>>(new ValueTuple<T1, T2, T3, T4, T5, T6>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult(), task5.GetAwaiter().GetResult(), task6.GetAwaiter().GetResult()));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6>>(new UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>(task1, task2, task3, task4, task5, task6), 0);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00006E40 File Offset: 0x00005040
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7>> WhenAll<T1, T2, T3, T4, T5, T6, T7>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully() && task5.Status.IsCompletedSuccessfully() && task6.Status.IsCompletedSuccessfully() && task7.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>(new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult(), task5.GetAwaiter().GetResult(), task6.GetAwaiter().GetResult(), task7.GetAwaiter().GetResult()));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>(new UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>(task1, task2, task3, task4, task5, task6, task7), 0);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006F50 File Offset: 0x00005150
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>> WhenAll<T1, T2, T3, T4, T5, T6, T7, T8>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully() && task5.Status.IsCompletedSuccessfully() && task6.Status.IsCompletedSuccessfully() && task7.Status.IsCompletedSuccessfully() && task8.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>>(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult(), task5.GetAwaiter().GetResult(), task6.GetAwaiter().GetResult(), task7.GetAwaiter().GetResult(), new ValueTuple<T8>(task8.GetAwaiter().GetResult())));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>>(new UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>(task1, task2, task3, task4, task5, task6, task7, task8), 0);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00007088 File Offset: 0x00005288
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>> WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully() && task5.Status.IsCompletedSuccessfully() && task6.Status.IsCompletedSuccessfully() && task7.Status.IsCompletedSuccessfully() && task8.Status.IsCompletedSuccessfully() && task9.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>>(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult(), task5.GetAwaiter().GetResult(), task6.GetAwaiter().GetResult(), task7.GetAwaiter().GetResult(), new ValueTuple<T8, T9>(task8.GetAwaiter().GetResult(), task9.GetAwaiter().GetResult())));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>>(new UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>(task1, task2, task3, task4, task5, task6, task7, task8, task9), 0);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000071E4 File Offset: 0x000053E4
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>> WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully() && task5.Status.IsCompletedSuccessfully() && task6.Status.IsCompletedSuccessfully() && task7.Status.IsCompletedSuccessfully() && task8.Status.IsCompletedSuccessfully() && task9.Status.IsCompletedSuccessfully() && task10.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>>(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult(), task5.GetAwaiter().GetResult(), task6.GetAwaiter().GetResult(), task7.GetAwaiter().GetResult(), new ValueTuple<T8, T9, T10>(task8.GetAwaiter().GetResult(), task9.GetAwaiter().GetResult(), task10.GetAwaiter().GetResult())));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>>(new UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10), 0);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00007364 File Offset: 0x00005564
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>> WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully() && task5.Status.IsCompletedSuccessfully() && task6.Status.IsCompletedSuccessfully() && task7.Status.IsCompletedSuccessfully() && task8.Status.IsCompletedSuccessfully() && task9.Status.IsCompletedSuccessfully() && task10.Status.IsCompletedSuccessfully() && task11.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>>(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult(), task5.GetAwaiter().GetResult(), task6.GetAwaiter().GetResult(), task7.GetAwaiter().GetResult(), new ValueTuple<T8, T9, T10, T11>(task8.GetAwaiter().GetResult(), task9.GetAwaiter().GetResult(), task10.GetAwaiter().GetResult(), task11.GetAwaiter().GetResult())));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>>(new UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11), 0);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00007508 File Offset: 0x00005708
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>> WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully() && task5.Status.IsCompletedSuccessfully() && task6.Status.IsCompletedSuccessfully() && task7.Status.IsCompletedSuccessfully() && task8.Status.IsCompletedSuccessfully() && task9.Status.IsCompletedSuccessfully() && task10.Status.IsCompletedSuccessfully() && task11.Status.IsCompletedSuccessfully() && task12.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>>(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult(), task5.GetAwaiter().GetResult(), task6.GetAwaiter().GetResult(), task7.GetAwaiter().GetResult(), new ValueTuple<T8, T9, T10, T11, T12>(task8.GetAwaiter().GetResult(), task9.GetAwaiter().GetResult(), task10.GetAwaiter().GetResult(), task11.GetAwaiter().GetResult(), task12.GetAwaiter().GetResult())));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>>(new UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12), 0);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000076CC File Offset: 0x000058CC
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>> WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully() && task5.Status.IsCompletedSuccessfully() && task6.Status.IsCompletedSuccessfully() && task7.Status.IsCompletedSuccessfully() && task8.Status.IsCompletedSuccessfully() && task9.Status.IsCompletedSuccessfully() && task10.Status.IsCompletedSuccessfully() && task11.Status.IsCompletedSuccessfully() && task12.Status.IsCompletedSuccessfully() && task13.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>>(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult(), task5.GetAwaiter().GetResult(), task6.GetAwaiter().GetResult(), task7.GetAwaiter().GetResult(), new ValueTuple<T8, T9, T10, T11, T12, T13>(task8.GetAwaiter().GetResult(), task9.GetAwaiter().GetResult(), task10.GetAwaiter().GetResult(), task11.GetAwaiter().GetResult(), task12.GetAwaiter().GetResult(), task13.GetAwaiter().GetResult())));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>>(new UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13), 0);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000078B4 File Offset: 0x00005AB4
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>> WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13, UniTask<T14> task14)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully() && task5.Status.IsCompletedSuccessfully() && task6.Status.IsCompletedSuccessfully() && task7.Status.IsCompletedSuccessfully() && task8.Status.IsCompletedSuccessfully() && task9.Status.IsCompletedSuccessfully() && task10.Status.IsCompletedSuccessfully() && task11.Status.IsCompletedSuccessfully() && task12.Status.IsCompletedSuccessfully() && task13.Status.IsCompletedSuccessfully() && task14.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>>(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult(), task5.GetAwaiter().GetResult(), task6.GetAwaiter().GetResult(), task7.GetAwaiter().GetResult(), new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(task8.GetAwaiter().GetResult(), task9.GetAwaiter().GetResult(), task10.GetAwaiter().GetResult(), task11.GetAwaiter().GetResult(), task12.GetAwaiter().GetResult(), task13.GetAwaiter().GetResult(), task14.GetAwaiter().GetResult())));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>>(new UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13, task14), 0);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00007AC0 File Offset: 0x00005CC0
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>> WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13, UniTask<T14> task14, UniTask<T15> task15)
		{
			if (task1.Status.IsCompletedSuccessfully() && task2.Status.IsCompletedSuccessfully() && task3.Status.IsCompletedSuccessfully() && task4.Status.IsCompletedSuccessfully() && task5.Status.IsCompletedSuccessfully() && task6.Status.IsCompletedSuccessfully() && task7.Status.IsCompletedSuccessfully() && task8.Status.IsCompletedSuccessfully() && task9.Status.IsCompletedSuccessfully() && task10.Status.IsCompletedSuccessfully() && task11.Status.IsCompletedSuccessfully() && task12.Status.IsCompletedSuccessfully() && task13.Status.IsCompletedSuccessfully() && task14.Status.IsCompletedSuccessfully() && task15.Status.IsCompletedSuccessfully())
			{
				return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>>(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(task1.GetAwaiter().GetResult(), task2.GetAwaiter().GetResult(), task3.GetAwaiter().GetResult(), task4.GetAwaiter().GetResult(), task5.GetAwaiter().GetResult(), task6.GetAwaiter().GetResult(), task7.GetAwaiter().GetResult(), new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(task8.GetAwaiter().GetResult(), task9.GetAwaiter().GetResult(), task10.GetAwaiter().GetResult(), task11.GetAwaiter().GetResult(), task12.GetAwaiter().GetResult(), task13.GetAwaiter().GetResult(), task14.GetAwaiter().GetResult(), new ValueTuple<T15>(task15.GetAwaiter().GetResult()))));
			}
			return new UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>>(new UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13, task14, task15), 0);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00007CF2 File Offset: 0x00005EF2
		public static UniTask<T[]> WhenAll<T>(params UniTask<T>[] tasks)
		{
			if (tasks.Length == 0)
			{
				return UniTask.FromResult<T[]>(Array.Empty<T>());
			}
			return new UniTask<T[]>(new UniTask.WhenAllPromise<T>(tasks, tasks.Length), 0);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00007D14 File Offset: 0x00005F14
		public static UniTask<T[]> WhenAll<T>(IEnumerable<UniTask<T>> tasks)
		{
			UniTask<T[]> uniTask;
			using (ArrayPoolUtil.RentArray<UniTask<T>> span = ArrayPoolUtil.Materialize<UniTask<T>>(tasks))
			{
				uniTask = new UniTask<T[]>(new UniTask.WhenAllPromise<T>(span.Array, span.Length), 0);
			}
			return uniTask;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007D64 File Offset: 0x00005F64
		public static UniTask WhenAll(params UniTask[] tasks)
		{
			if (tasks.Length == 0)
			{
				return UniTask.CompletedTask;
			}
			return new UniTask(new UniTask.WhenAllPromise(tasks, tasks.Length), 0);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00007D80 File Offset: 0x00005F80
		public static UniTask WhenAll(IEnumerable<UniTask> tasks)
		{
			UniTask uniTask;
			using (ArrayPoolUtil.RentArray<UniTask> span = ArrayPoolUtil.Materialize<UniTask>(tasks))
			{
				uniTask = new UniTask(new UniTask.WhenAllPromise(span.Array, span.Length), 0);
			}
			return uniTask;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00007DD0 File Offset: 0x00005FD0
		[return: TupleElementNames(new string[] { "winArgumentIndex", "result1", "result2" })]
		public static UniTask<ValueTuple<int, T1, T2>> WhenAny<T1, T2>(UniTask<T1> task1, UniTask<T2> task2)
		{
			return new UniTask<ValueTuple<int, T1, T2>>(new UniTask.WhenAnyPromise<T1, T2>(task1, task2), 0);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00007DDF File Offset: 0x00005FDF
		[return: TupleElementNames(new string[] { "winArgumentIndex", "result1", "result2", "result3" })]
		public static UniTask<ValueTuple<int, T1, T2, T3>> WhenAny<T1, T2, T3>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3>>(new UniTask.WhenAnyPromise<T1, T2, T3>(task1, task2, task3), 0);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00007DEF File Offset: 0x00005FEF
		[return: TupleElementNames(new string[] { "winArgumentIndex", "result1", "result2", "result3", "result4" })]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4>> WhenAny<T1, T2, T3, T4>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4>(task1, task2, task3, task4), 0);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00007E00 File Offset: 0x00006000
		[return: TupleElementNames(new string[] { "winArgumentIndex", "result1", "result2", "result3", "result4", "result5" })]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4, T5>> WhenAny<T1, T2, T3, T4, T5>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4, T5>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>(task1, task2, task3, task4, task5), 0);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007E13 File Offset: 0x00006013
		[return: TupleElementNames(new string[] { "winArgumentIndex", "result1", "result2", "result3", "result4", "result5", "result6" })]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6>> WhenAny<T1, T2, T3, T4, T5, T6>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>(task1, task2, task3, task4, task5, task6), 0);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00007E28 File Offset: 0x00006028
		[return: TupleElementNames(new string[] { "winArgumentIndex", "result1", "result2", "result3", "result4", "result5", "result6", "result7", null })]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>> WhenAny<T1, T2, T3, T4, T5, T6, T7>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>(task1, task2, task3, task4, task5, task6, task7), 0);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00007E3F File Offset: 0x0000603F
		[return: TupleElementNames(new string[]
		{
			"winArgumentIndex", "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", null,
			null
		})]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>(task1, task2, task3, task4, task5, task6, task7, task8), 0);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007E58 File Offset: 0x00006058
		[return: TupleElementNames(new string[]
		{
			"winArgumentIndex", "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
			null, null, null
		})]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>(task1, task2, task3, task4, task5, task6, task7, task8, task9), 0);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00007E80 File Offset: 0x00006080
		[return: TupleElementNames(new string[]
		{
			"winArgumentIndex", "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
			"result10", null, null, null, null
		})]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10), 0);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007EA8 File Offset: 0x000060A8
		[return: TupleElementNames(new string[]
		{
			"winArgumentIndex", "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
			"result10", "result11", null, null, null, null, null
		})]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11), 0);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00007ED4 File Offset: 0x000060D4
		[return: TupleElementNames(new string[]
		{
			"winArgumentIndex", "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
			"result10", "result11", "result12", null, null, null, null, null, null
		})]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12), 0);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00007F00 File Offset: 0x00006100
		[return: TupleElementNames(new string[]
		{
			"winArgumentIndex", "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
			"result10", "result11", "result12", "result13", null, null, null, null, null, null,
			null
		})]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13), 0);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00007F30 File Offset: 0x00006130
		[return: TupleElementNames(new string[]
		{
			"winArgumentIndex", "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
			"result10", "result11", "result12", "result13", "result14", null, null, null, null, null,
			null, null, null, null
		})]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13, UniTask<T14> task14)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13, task14), 0);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007F60 File Offset: 0x00006160
		[return: TupleElementNames(new string[]
		{
			"winArgumentIndex", "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
			"result10", "result11", "result12", "result13", "result14", "result15", null, null, null, null,
			null, null, null, null, null, null, null
		})]
		public static UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13, UniTask<T14> task14, UniTask<T15> task15)
		{
			return new UniTask<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>>(new UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13, task14, task15), 0);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007F92 File Offset: 0x00006192
		[return: TupleElementNames(new string[] { "hasResultLeft", "result" })]
		public static UniTask<ValueTuple<bool, T>> WhenAny<T>(UniTask<T> leftTask, UniTask rightTask)
		{
			return new UniTask<ValueTuple<bool, T>>(new UniTask.WhenAnyLRPromise<T>(leftTask, rightTask), 0);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00007FA1 File Offset: 0x000061A1
		[return: TupleElementNames(new string[] { "winArgumentIndex", "result" })]
		public static UniTask<ValueTuple<int, T>> WhenAny<T>(params UniTask<T>[] tasks)
		{
			return new UniTask<ValueTuple<int, T>>(new UniTask.WhenAnyPromise<T>(tasks, tasks.Length), 0);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00007FB4 File Offset: 0x000061B4
		[return: TupleElementNames(new string[] { "winArgumentIndex", "result" })]
		public static UniTask<ValueTuple<int, T>> WhenAny<T>(IEnumerable<UniTask<T>> tasks)
		{
			UniTask<ValueTuple<int, T>> uniTask;
			using (ArrayPoolUtil.RentArray<UniTask<T>> span = ArrayPoolUtil.Materialize<UniTask<T>>(tasks))
			{
				uniTask = new UniTask<ValueTuple<int, T>>(new UniTask.WhenAnyPromise<T>(span.Array, span.Length), 0);
			}
			return uniTask;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008004 File Offset: 0x00006204
		public static UniTask<int> WhenAny(params UniTask[] tasks)
		{
			return new UniTask<int>(new UniTask.WhenAnyPromise(tasks, tasks.Length), 0);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00008018 File Offset: 0x00006218
		public static UniTask<int> WhenAny(IEnumerable<UniTask> tasks)
		{
			UniTask<int> uniTask;
			using (ArrayPoolUtil.RentArray<UniTask> span = ArrayPoolUtil.Materialize<UniTask>(tasks))
			{
				uniTask = new UniTask<int>(new UniTask.WhenAnyPromise(span.Array, span.Length), 0);
			}
			return uniTask;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00008068 File Offset: 0x00006268
		public static IUniTaskAsyncEnumerable<WhenEachResult<T>> WhenEach<T>(IEnumerable<UniTask<T>> tasks)
		{
			return new WhenEachEnumerable<T>(tasks);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00008068 File Offset: 0x00006268
		public static IUniTaskAsyncEnumerable<WhenEachResult<T>> WhenEach<T>(params UniTask<T>[] tasks)
		{
			return new WhenEachEnumerable<T>(tasks);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008070 File Offset: 0x00006270
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public UniTask(IUniTaskSource source, short token)
		{
			this.source = source;
			this.token = token;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00008080 File Offset: 0x00006280
		public UniTaskStatus Status
		{
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (this.source == null)
				{
					return UniTaskStatus.Succeeded;
				}
				return this.source.GetStatus(this.token);
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000809D File Offset: 0x0000629D
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public UniTask.Awaiter GetAwaiter()
		{
			return new UniTask.Awaiter(in this);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000080A8 File Offset: 0x000062A8
		public UniTask<bool> SuppressCancellationThrow()
		{
			UniTaskStatus status = this.Status;
			if (status == UniTaskStatus.Succeeded)
			{
				return CompletedTasks.False;
			}
			if (status == UniTaskStatus.Canceled)
			{
				return CompletedTasks.True;
			}
			return new UniTask<bool>(new UniTask.IsCanceledSource(this.source), this.token);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000080E8 File Offset: 0x000062E8
		public static implicit operator ValueTask(in UniTask self)
		{
			if (self.source == null)
			{
				return default(ValueTask);
			}
			return new ValueTask(self.source, self.token);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00008118 File Offset: 0x00006318
		public override string ToString()
		{
			if (this.source == null)
			{
				return "()";
			}
			return "(" + this.source.UnsafeGetStatus().ToString() + ")";
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000815B File Offset: 0x0000635B
		public UniTask Preserve()
		{
			if (this.source == null)
			{
				return this;
			}
			return new UniTask(new UniTask.MemoizeSource(this.source), this.token);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00008184 File Offset: 0x00006384
		public UniTask<AsyncUnit> AsAsyncUnitUniTask()
		{
			if (this.source == null)
			{
				return CompletedTasks.AsyncUnit;
			}
			if (this.source.GetStatus(this.token).IsCompletedSuccessfully())
			{
				this.source.GetResult(this.token);
				return CompletedTasks.AsyncUnit;
			}
			IUniTaskSource<AsyncUnit> asyncUnitSource = this.source as IUniTaskSource<AsyncUnit>;
			if (asyncUnitSource != null)
			{
				return new UniTask<AsyncUnit>(asyncUnitSource, this.token);
			}
			return new UniTask<AsyncUnit>(new UniTask.AsyncUnitSource(this.source), this.token);
		}

		// Token: 0x04000104 RID: 260
		private static readonly UniTask CanceledUniTask = (() => new UniTask(new UniTask.CanceledResultSource(CancellationToken.None), 0))();

		// Token: 0x04000105 RID: 261
		public static readonly UniTask CompletedTask = default(UniTask);

		// Token: 0x04000106 RID: 262
		private readonly IUniTaskSource source;

		// Token: 0x04000107 RID: 263
		private readonly short token;

		// Token: 0x02000079 RID: 121
		private sealed class YieldPromise : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UniTask.YieldPromise>
		{
			// Token: 0x17000031 RID: 49
			// (get) Token: 0x0600022B RID: 555 RVA: 0x00008227 File Offset: 0x00006427
			public ref UniTask.YieldPromise NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x0600022C RID: 556 RVA: 0x0000822F File Offset: 0x0000642F
			static YieldPromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.YieldPromise), () => UniTask.YieldPromise.pool.Size);
			}

			// Token: 0x0600022D RID: 557 RVA: 0x000020BB File Offset: 0x000002BB
			private YieldPromise()
			{
			}

			// Token: 0x0600022E RID: 558 RVA: 0x00008250 File Offset: 0x00006450
			public static IUniTaskSource Create(PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.YieldPromise result;
				if (!UniTask.YieldPromise.pool.TryPop(out result))
				{
					result = new UniTask.YieldPromise();
				}
				result.cancellationToken = cancellationToken;
				result.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.YieldPromise promise = (UniTask.YieldPromise)state;
						promise.core.TrySetCanceled(promise.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x0600022F RID: 559 RVA: 0x000082E0 File Offset: 0x000064E0
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x06000230 RID: 560 RVA: 0x0000832C File Offset: 0x0000652C
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000231 RID: 561 RVA: 0x0000833A File Offset: 0x0000653A
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000232 RID: 562 RVA: 0x00008347 File Offset: 0x00006547
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000233 RID: 563 RVA: 0x00008357 File Offset: 0x00006557
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				this.core.TrySetResult(null);
				return false;
			}

			// Token: 0x06000234 RID: 564 RVA: 0x00008388 File Offset: 0x00006588
			private bool TryReturn()
			{
				this.core.Reset();
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.YieldPromise.pool.TryPush(this);
			}

			// Token: 0x04000108 RID: 264
			private static TaskPool<UniTask.YieldPromise> pool;

			// Token: 0x04000109 RID: 265
			private UniTask.YieldPromise nextNode;

			// Token: 0x0400010A RID: 266
			private CancellationToken cancellationToken;

			// Token: 0x0400010B RID: 267
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x0400010C RID: 268
			private bool cancelImmediately;

			// Token: 0x0400010D RID: 269
			private UniTaskCompletionSourceCore<object> core;
		}

		// Token: 0x0200007B RID: 123
		private sealed class NextFramePromise : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UniTask.NextFramePromise>
		{
			// Token: 0x17000032 RID: 50
			// (get) Token: 0x06000239 RID: 569 RVA: 0x000083FE File Offset: 0x000065FE
			public ref UniTask.NextFramePromise NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x0600023A RID: 570 RVA: 0x00008406 File Offset: 0x00006606
			static NextFramePromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.NextFramePromise), () => UniTask.NextFramePromise.pool.Size);
			}

			// Token: 0x0600023B RID: 571 RVA: 0x000020BB File Offset: 0x000002BB
			private NextFramePromise()
			{
			}

			// Token: 0x0600023C RID: 572 RVA: 0x00008428 File Offset: 0x00006628
			public static IUniTaskSource Create(PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.NextFramePromise result;
				if (!UniTask.NextFramePromise.pool.TryPop(out result))
				{
					result = new UniTask.NextFramePromise();
				}
				result.frameCount = (PlayerLoopHelper.IsMainThread ? Time.frameCount : (-1));
				result.cancellationToken = cancellationToken;
				result.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.NextFramePromise promise = (UniTask.NextFramePromise)state;
						promise.core.TrySetCanceled(promise.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x0600023D RID: 573 RVA: 0x000084CC File Offset: 0x000066CC
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x0600023E RID: 574 RVA: 0x00008518 File Offset: 0x00006718
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600023F RID: 575 RVA: 0x00008526 File Offset: 0x00006726
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000240 RID: 576 RVA: 0x00008533 File Offset: 0x00006733
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000241 RID: 577 RVA: 0x00008544 File Offset: 0x00006744
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.frameCount == Time.frameCount)
				{
					return true;
				}
				this.core.TrySetResult(AsyncUnit.Default);
				return false;
			}

			// Token: 0x06000242 RID: 578 RVA: 0x00008593 File Offset: 0x00006793
			private bool TryReturn()
			{
				this.core.Reset();
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				return UniTask.NextFramePromise.pool.TryPush(this);
			}

			// Token: 0x04000110 RID: 272
			private static TaskPool<UniTask.NextFramePromise> pool;

			// Token: 0x04000111 RID: 273
			private UniTask.NextFramePromise nextNode;

			// Token: 0x04000112 RID: 274
			private int frameCount;

			// Token: 0x04000113 RID: 275
			private UniTaskCompletionSourceCore<AsyncUnit> core;

			// Token: 0x04000114 RID: 276
			private CancellationToken cancellationToken;

			// Token: 0x04000115 RID: 277
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x04000116 RID: 278
			private bool cancelImmediately;
		}

		// Token: 0x0200007D RID: 125
		private sealed class WaitForEndOfFramePromise : IUniTaskSource, IValueTaskSource, ITaskPoolNode<UniTask.WaitForEndOfFramePromise>, IEnumerator
		{
			// Token: 0x17000033 RID: 51
			// (get) Token: 0x06000247 RID: 583 RVA: 0x00008602 File Offset: 0x00006802
			public ref UniTask.WaitForEndOfFramePromise NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x06000248 RID: 584 RVA: 0x0000860A File Offset: 0x0000680A
			static WaitForEndOfFramePromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.WaitForEndOfFramePromise), () => UniTask.WaitForEndOfFramePromise.pool.Size);
			}

			// Token: 0x06000249 RID: 585 RVA: 0x00008635 File Offset: 0x00006835
			private WaitForEndOfFramePromise()
			{
			}

			// Token: 0x0600024A RID: 586 RVA: 0x00008644 File Offset: 0x00006844
			public static IUniTaskSource Create(MonoBehaviour coroutineRunner, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.WaitForEndOfFramePromise result;
				if (!UniTask.WaitForEndOfFramePromise.pool.TryPop(out result))
				{
					result = new UniTask.WaitForEndOfFramePromise();
				}
				result.cancellationToken = cancellationToken;
				result.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.WaitForEndOfFramePromise promise = (UniTask.WaitForEndOfFramePromise)state;
						promise.core.TrySetCanceled(promise.cancellationToken);
					}, result);
				}
				coroutineRunner.StartCoroutine(result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x0600024B RID: 587 RVA: 0x000086D4 File Offset: 0x000068D4
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x0600024C RID: 588 RVA: 0x00008720 File Offset: 0x00006920
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600024D RID: 589 RVA: 0x0000872E File Offset: 0x0000692E
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600024E RID: 590 RVA: 0x0000873B File Offset: 0x0000693B
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600024F RID: 591 RVA: 0x0000874B File Offset: 0x0000694B
			private bool TryReturn()
			{
				this.core.Reset();
				this.Reset();
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				return UniTask.WaitForEndOfFramePromise.pool.TryPush(this);
			}

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x06000250 RID: 592 RVA: 0x00008780 File Offset: 0x00006980
			object IEnumerator.Current
			{
				get
				{
					return UniTask.WaitForEndOfFramePromise.waitForEndOfFrameYieldInstruction;
				}
			}

			// Token: 0x06000251 RID: 593 RVA: 0x00008788 File Offset: 0x00006988
			bool IEnumerator.MoveNext()
			{
				if (this.isFirst)
				{
					this.isFirst = false;
					return true;
				}
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				this.core.TrySetResult(null);
				return false;
			}

			// Token: 0x06000252 RID: 594 RVA: 0x000087D5 File Offset: 0x000069D5
			public void Reset()
			{
				this.isFirst = true;
			}

			// Token: 0x04000119 RID: 281
			private static TaskPool<UniTask.WaitForEndOfFramePromise> pool;

			// Token: 0x0400011A RID: 282
			private UniTask.WaitForEndOfFramePromise nextNode;

			// Token: 0x0400011B RID: 283
			private UniTaskCompletionSourceCore<object> core;

			// Token: 0x0400011C RID: 284
			private CancellationToken cancellationToken;

			// Token: 0x0400011D RID: 285
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x0400011E RID: 286
			private bool cancelImmediately;

			// Token: 0x0400011F RID: 287
			private static readonly WaitForEndOfFrame waitForEndOfFrameYieldInstruction = new WaitForEndOfFrame();

			// Token: 0x04000120 RID: 288
			private bool isFirst = true;
		}

		// Token: 0x0200007F RID: 127
		private sealed class DelayFramePromise : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UniTask.DelayFramePromise>
		{
			// Token: 0x17000035 RID: 53
			// (get) Token: 0x06000257 RID: 599 RVA: 0x0000881E File Offset: 0x00006A1E
			public ref UniTask.DelayFramePromise NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x06000258 RID: 600 RVA: 0x00008826 File Offset: 0x00006A26
			static DelayFramePromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.DelayFramePromise), () => UniTask.DelayFramePromise.pool.Size);
			}

			// Token: 0x06000259 RID: 601 RVA: 0x000020BB File Offset: 0x000002BB
			private DelayFramePromise()
			{
			}

			// Token: 0x0600025A RID: 602 RVA: 0x00008848 File Offset: 0x00006A48
			public static IUniTaskSource Create(int delayFrameCount, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.DelayFramePromise result;
				if (!UniTask.DelayFramePromise.pool.TryPop(out result))
				{
					result = new UniTask.DelayFramePromise();
				}
				result.delayFrameCount = delayFrameCount;
				result.cancellationToken = cancellationToken;
				result.initialFrame = (PlayerLoopHelper.IsMainThread ? Time.frameCount : (-1));
				result.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.DelayFramePromise promise = (UniTask.DelayFramePromise)state;
						promise.core.TrySetCanceled(promise.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x0600025B RID: 603 RVA: 0x000088F4 File Offset: 0x00006AF4
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x0600025C RID: 604 RVA: 0x00008940 File Offset: 0x00006B40
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600025D RID: 605 RVA: 0x0000894E File Offset: 0x00006B4E
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600025E RID: 606 RVA: 0x0000895B File Offset: 0x00006B5B
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600025F RID: 607 RVA: 0x0000896C File Offset: 0x00006B6C
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.currentFrameCount == 0)
				{
					if (this.delayFrameCount == 0)
					{
						this.core.TrySetResult(AsyncUnit.Default);
						return false;
					}
					if (this.initialFrame == Time.frameCount)
					{
						return true;
					}
				}
				int num = this.currentFrameCount + 1;
				this.currentFrameCount = num;
				if (num >= this.delayFrameCount)
				{
					this.core.TrySetResult(AsyncUnit.Default);
					return false;
				}
				return true;
			}

			// Token: 0x06000260 RID: 608 RVA: 0x000089FC File Offset: 0x00006BFC
			private bool TryReturn()
			{
				this.core.Reset();
				this.currentFrameCount = 0;
				this.delayFrameCount = 0;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.DelayFramePromise.pool.TryPush(this);
			}

			// Token: 0x04000123 RID: 291
			private static TaskPool<UniTask.DelayFramePromise> pool;

			// Token: 0x04000124 RID: 292
			private UniTask.DelayFramePromise nextNode;

			// Token: 0x04000125 RID: 293
			private int initialFrame;

			// Token: 0x04000126 RID: 294
			private int delayFrameCount;

			// Token: 0x04000127 RID: 295
			private CancellationToken cancellationToken;

			// Token: 0x04000128 RID: 296
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x04000129 RID: 297
			private bool cancelImmediately;

			// Token: 0x0400012A RID: 298
			private int currentFrameCount;

			// Token: 0x0400012B RID: 299
			private UniTaskCompletionSourceCore<AsyncUnit> core;
		}

		// Token: 0x02000081 RID: 129
		private sealed class DelayPromise : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UniTask.DelayPromise>
		{
			// Token: 0x17000036 RID: 54
			// (get) Token: 0x06000265 RID: 613 RVA: 0x00008A8A File Offset: 0x00006C8A
			public ref UniTask.DelayPromise NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x06000266 RID: 614 RVA: 0x00008A92 File Offset: 0x00006C92
			static DelayPromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.DelayPromise), () => UniTask.DelayPromise.pool.Size);
			}

			// Token: 0x06000267 RID: 615 RVA: 0x000020BB File Offset: 0x000002BB
			private DelayPromise()
			{
			}

			// Token: 0x06000268 RID: 616 RVA: 0x00008AB4 File Offset: 0x00006CB4
			public static IUniTaskSource Create(TimeSpan delayTimeSpan, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.DelayPromise result;
				if (!UniTask.DelayPromise.pool.TryPop(out result))
				{
					result = new UniTask.DelayPromise();
				}
				result.elapsed = 0f;
				result.delayTimeSpan = (float)delayTimeSpan.TotalSeconds;
				result.cancellationToken = cancellationToken;
				result.initialFrame = (PlayerLoopHelper.IsMainThread ? Time.frameCount : (-1));
				result.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.DelayPromise promise = (UniTask.DelayPromise)state;
						promise.core.TrySetCanceled(promise.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x06000269 RID: 617 RVA: 0x00008B74 File Offset: 0x00006D74
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x0600026A RID: 618 RVA: 0x00008BC0 File Offset: 0x00006DC0
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600026B RID: 619 RVA: 0x00008BCE File Offset: 0x00006DCE
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600026C RID: 620 RVA: 0x00008BDB File Offset: 0x00006DDB
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600026D RID: 621 RVA: 0x00008BEC File Offset: 0x00006DEC
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.elapsed == 0f && this.initialFrame == Time.frameCount)
				{
					return true;
				}
				this.elapsed += Time.deltaTime;
				if (this.elapsed >= this.delayTimeSpan)
				{
					this.core.TrySetResult(null);
					return false;
				}
				return true;
			}

			// Token: 0x0600026E RID: 622 RVA: 0x00008C68 File Offset: 0x00006E68
			private bool TryReturn()
			{
				this.core.Reset();
				this.delayTimeSpan = 0f;
				this.elapsed = 0f;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.DelayPromise.pool.TryPush(this);
			}

			// Token: 0x0400012E RID: 302
			private static TaskPool<UniTask.DelayPromise> pool;

			// Token: 0x0400012F RID: 303
			private UniTask.DelayPromise nextNode;

			// Token: 0x04000130 RID: 304
			private int initialFrame;

			// Token: 0x04000131 RID: 305
			private float delayTimeSpan;

			// Token: 0x04000132 RID: 306
			private float elapsed;

			// Token: 0x04000133 RID: 307
			private CancellationToken cancellationToken;

			// Token: 0x04000134 RID: 308
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x04000135 RID: 309
			private bool cancelImmediately;

			// Token: 0x04000136 RID: 310
			private UniTaskCompletionSourceCore<object> core;
		}

		// Token: 0x02000083 RID: 131
		private sealed class DelayIgnoreTimeScalePromise : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UniTask.DelayIgnoreTimeScalePromise>
		{
			// Token: 0x17000037 RID: 55
			// (get) Token: 0x06000273 RID: 627 RVA: 0x00008CFE File Offset: 0x00006EFE
			public ref UniTask.DelayIgnoreTimeScalePromise NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x06000274 RID: 628 RVA: 0x00008D06 File Offset: 0x00006F06
			static DelayIgnoreTimeScalePromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.DelayIgnoreTimeScalePromise), () => UniTask.DelayIgnoreTimeScalePromise.pool.Size);
			}

			// Token: 0x06000275 RID: 629 RVA: 0x000020BB File Offset: 0x000002BB
			private DelayIgnoreTimeScalePromise()
			{
			}

			// Token: 0x06000276 RID: 630 RVA: 0x00008D28 File Offset: 0x00006F28
			public static IUniTaskSource Create(TimeSpan delayFrameTimeSpan, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.DelayIgnoreTimeScalePromise result;
				if (!UniTask.DelayIgnoreTimeScalePromise.pool.TryPop(out result))
				{
					result = new UniTask.DelayIgnoreTimeScalePromise();
				}
				result.elapsed = 0f;
				result.delayFrameTimeSpan = (float)delayFrameTimeSpan.TotalSeconds;
				result.initialFrame = (PlayerLoopHelper.IsMainThread ? Time.frameCount : (-1));
				result.cancellationToken = cancellationToken;
				result.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.DelayIgnoreTimeScalePromise promise = (UniTask.DelayIgnoreTimeScalePromise)state;
						promise.core.TrySetCanceled(promise.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x06000277 RID: 631 RVA: 0x00008DE8 File Offset: 0x00006FE8
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x06000278 RID: 632 RVA: 0x00008E34 File Offset: 0x00007034
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000279 RID: 633 RVA: 0x00008E42 File Offset: 0x00007042
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600027A RID: 634 RVA: 0x00008E4F File Offset: 0x0000704F
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600027B RID: 635 RVA: 0x00008E60 File Offset: 0x00007060
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.elapsed == 0f && this.initialFrame == Time.frameCount)
				{
					return true;
				}
				this.elapsed += Time.unscaledDeltaTime;
				if (this.elapsed >= this.delayFrameTimeSpan)
				{
					this.core.TrySetResult(null);
					return false;
				}
				return true;
			}

			// Token: 0x0600027C RID: 636 RVA: 0x00008EDC File Offset: 0x000070DC
			private bool TryReturn()
			{
				this.core.Reset();
				this.delayFrameTimeSpan = 0f;
				this.elapsed = 0f;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.DelayIgnoreTimeScalePromise.pool.TryPush(this);
			}

			// Token: 0x04000139 RID: 313
			private static TaskPool<UniTask.DelayIgnoreTimeScalePromise> pool;

			// Token: 0x0400013A RID: 314
			private UniTask.DelayIgnoreTimeScalePromise nextNode;

			// Token: 0x0400013B RID: 315
			private float delayFrameTimeSpan;

			// Token: 0x0400013C RID: 316
			private float elapsed;

			// Token: 0x0400013D RID: 317
			private int initialFrame;

			// Token: 0x0400013E RID: 318
			private CancellationToken cancellationToken;

			// Token: 0x0400013F RID: 319
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x04000140 RID: 320
			private bool cancelImmediately;

			// Token: 0x04000141 RID: 321
			private UniTaskCompletionSourceCore<object> core;
		}

		// Token: 0x02000085 RID: 133
		private sealed class DelayRealtimePromise : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UniTask.DelayRealtimePromise>
		{
			// Token: 0x17000038 RID: 56
			// (get) Token: 0x06000281 RID: 641 RVA: 0x00008F72 File Offset: 0x00007172
			public ref UniTask.DelayRealtimePromise NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x06000282 RID: 642 RVA: 0x00008F7A File Offset: 0x0000717A
			static DelayRealtimePromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.DelayRealtimePromise), () => UniTask.DelayRealtimePromise.pool.Size);
			}

			// Token: 0x06000283 RID: 643 RVA: 0x000020BB File Offset: 0x000002BB
			private DelayRealtimePromise()
			{
			}

			// Token: 0x06000284 RID: 644 RVA: 0x00008F9C File Offset: 0x0000719C
			public static IUniTaskSource Create(TimeSpan delayTimeSpan, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.DelayRealtimePromise result;
				if (!UniTask.DelayRealtimePromise.pool.TryPop(out result))
				{
					result = new UniTask.DelayRealtimePromise();
				}
				result.stopwatch = ValueStopwatch.StartNew();
				result.delayTimeSpanTicks = delayTimeSpan.Ticks;
				result.cancellationToken = cancellationToken;
				result.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.DelayRealtimePromise promise = (UniTask.DelayRealtimePromise)state;
						promise.core.TrySetCanceled(promise.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x06000285 RID: 645 RVA: 0x00009044 File Offset: 0x00007244
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x06000286 RID: 646 RVA: 0x00009090 File Offset: 0x00007290
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000287 RID: 647 RVA: 0x0000909E File Offset: 0x0000729E
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000288 RID: 648 RVA: 0x000090AB File Offset: 0x000072AB
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000289 RID: 649 RVA: 0x000090BC File Offset: 0x000072BC
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.stopwatch.IsInvalid)
				{
					this.core.TrySetResult(AsyncUnit.Default);
					return false;
				}
				if (this.stopwatch.ElapsedTicks >= this.delayTimeSpanTicks)
				{
					this.core.TrySetResult(AsyncUnit.Default);
					return false;
				}
				return true;
			}

			// Token: 0x0600028A RID: 650 RVA: 0x00009134 File Offset: 0x00007334
			private bool TryReturn()
			{
				this.core.Reset();
				this.stopwatch = default(ValueStopwatch);
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.DelayRealtimePromise.pool.TryPush(this);
			}

			// Token: 0x04000144 RID: 324
			private static TaskPool<UniTask.DelayRealtimePromise> pool;

			// Token: 0x04000145 RID: 325
			private UniTask.DelayRealtimePromise nextNode;

			// Token: 0x04000146 RID: 326
			private long delayTimeSpanTicks;

			// Token: 0x04000147 RID: 327
			private ValueStopwatch stopwatch;

			// Token: 0x04000148 RID: 328
			private CancellationToken cancellationToken;

			// Token: 0x04000149 RID: 329
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x0400014A RID: 330
			private bool cancelImmediately;

			// Token: 0x0400014B RID: 331
			private UniTaskCompletionSourceCore<AsyncUnit> core;
		}

		// Token: 0x02000087 RID: 135
		private static class CanceledUniTaskCache<T>
		{
			// Token: 0x0400014E RID: 334
			public static readonly UniTask<T> Task = new UniTask<T>(new UniTask.CanceledResultSource<T>(CancellationToken.None), 0);
		}

		// Token: 0x02000088 RID: 136
		private sealed class ExceptionResultSource : IUniTaskSource, IValueTaskSource
		{
			// Token: 0x06000290 RID: 656 RVA: 0x000091D9 File Offset: 0x000073D9
			public ExceptionResultSource(Exception exception)
			{
				this.exception = ExceptionDispatchInfo.Capture(exception);
			}

			// Token: 0x06000291 RID: 657 RVA: 0x000091ED File Offset: 0x000073ED
			public void GetResult(short token)
			{
				if (!this.calledGet)
				{
					this.calledGet = true;
					GC.SuppressFinalize(this);
				}
				this.exception.Throw();
			}

			// Token: 0x06000292 RID: 658 RVA: 0x0000920F File Offset: 0x0000740F
			public UniTaskStatus GetStatus(short token)
			{
				return UniTaskStatus.Faulted;
			}

			// Token: 0x06000293 RID: 659 RVA: 0x0000920F File Offset: 0x0000740F
			public UniTaskStatus UnsafeGetStatus()
			{
				return UniTaskStatus.Faulted;
			}

			// Token: 0x06000294 RID: 660 RVA: 0x00009212 File Offset: 0x00007412
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				continuation(state);
			}

			// Token: 0x06000295 RID: 661 RVA: 0x0000921C File Offset: 0x0000741C
			~ExceptionResultSource()
			{
				if (!this.calledGet)
				{
					UniTaskScheduler.PublishUnobservedTaskException(this.exception.SourceException);
				}
			}

			// Token: 0x0400014F RID: 335
			private readonly ExceptionDispatchInfo exception;

			// Token: 0x04000150 RID: 336
			private bool calledGet;
		}

		// Token: 0x02000089 RID: 137
		private sealed class ExceptionResultSource<T> : IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>
		{
			// Token: 0x06000296 RID: 662 RVA: 0x0000925C File Offset: 0x0000745C
			public ExceptionResultSource(Exception exception)
			{
				this.exception = ExceptionDispatchInfo.Capture(exception);
			}

			// Token: 0x06000297 RID: 663 RVA: 0x00009270 File Offset: 0x00007470
			public T GetResult(short token)
			{
				if (!this.calledGet)
				{
					this.calledGet = true;
					GC.SuppressFinalize(this);
				}
				this.exception.Throw();
				return default(T);
			}

			// Token: 0x06000298 RID: 664 RVA: 0x000092A6 File Offset: 0x000074A6
			void IUniTaskSource.GetResult(short token)
			{
				if (!this.calledGet)
				{
					this.calledGet = true;
					GC.SuppressFinalize(this);
				}
				this.exception.Throw();
			}

			// Token: 0x06000299 RID: 665 RVA: 0x0000920F File Offset: 0x0000740F
			public UniTaskStatus GetStatus(short token)
			{
				return UniTaskStatus.Faulted;
			}

			// Token: 0x0600029A RID: 666 RVA: 0x0000920F File Offset: 0x0000740F
			public UniTaskStatus UnsafeGetStatus()
			{
				return UniTaskStatus.Faulted;
			}

			// Token: 0x0600029B RID: 667 RVA: 0x00009212 File Offset: 0x00007412
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				continuation(state);
			}

			// Token: 0x0600029C RID: 668 RVA: 0x000092C8 File Offset: 0x000074C8
			~ExceptionResultSource()
			{
				if (!this.calledGet)
				{
					UniTaskScheduler.PublishUnobservedTaskException(this.exception.SourceException);
				}
			}

			// Token: 0x04000151 RID: 337
			private readonly ExceptionDispatchInfo exception;

			// Token: 0x04000152 RID: 338
			private bool calledGet;
		}

		// Token: 0x0200008A RID: 138
		private sealed class CanceledResultSource : IUniTaskSource, IValueTaskSource
		{
			// Token: 0x0600029D RID: 669 RVA: 0x00009308 File Offset: 0x00007508
			public CanceledResultSource(CancellationToken cancellationToken)
			{
				this.cancellationToken = cancellationToken;
			}

			// Token: 0x0600029E RID: 670 RVA: 0x00009317 File Offset: 0x00007517
			public void GetResult(short token)
			{
				throw new OperationCanceledException(this.cancellationToken);
			}

			// Token: 0x0600029F RID: 671 RVA: 0x00009324 File Offset: 0x00007524
			public UniTaskStatus GetStatus(short token)
			{
				return UniTaskStatus.Canceled;
			}

			// Token: 0x060002A0 RID: 672 RVA: 0x00009324 File Offset: 0x00007524
			public UniTaskStatus UnsafeGetStatus()
			{
				return UniTaskStatus.Canceled;
			}

			// Token: 0x060002A1 RID: 673 RVA: 0x00009212 File Offset: 0x00007412
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				continuation(state);
			}

			// Token: 0x04000153 RID: 339
			private readonly CancellationToken cancellationToken;
		}

		// Token: 0x0200008B RID: 139
		private sealed class CanceledResultSource<T> : IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>
		{
			// Token: 0x060002A2 RID: 674 RVA: 0x00009327 File Offset: 0x00007527
			public CanceledResultSource(CancellationToken cancellationToken)
			{
				this.cancellationToken = cancellationToken;
			}

			// Token: 0x060002A3 RID: 675 RVA: 0x00009336 File Offset: 0x00007536
			public T GetResult(short token)
			{
				throw new OperationCanceledException(this.cancellationToken);
			}

			// Token: 0x060002A4 RID: 676 RVA: 0x00009336 File Offset: 0x00007536
			void IUniTaskSource.GetResult(short token)
			{
				throw new OperationCanceledException(this.cancellationToken);
			}

			// Token: 0x060002A5 RID: 677 RVA: 0x00009324 File Offset: 0x00007524
			public UniTaskStatus GetStatus(short token)
			{
				return UniTaskStatus.Canceled;
			}

			// Token: 0x060002A6 RID: 678 RVA: 0x00009324 File Offset: 0x00007524
			public UniTaskStatus UnsafeGetStatus()
			{
				return UniTaskStatus.Canceled;
			}

			// Token: 0x060002A7 RID: 679 RVA: 0x00009212 File Offset: 0x00007412
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				continuation(state);
			}

			// Token: 0x04000154 RID: 340
			private readonly CancellationToken cancellationToken;
		}

		// Token: 0x0200008C RID: 140
		private sealed class DeferPromise : IUniTaskSource, IValueTaskSource
		{
			// Token: 0x060002A8 RID: 680 RVA: 0x00009343 File Offset: 0x00007543
			public DeferPromise(Func<UniTask> factory)
			{
				this.factory = factory;
			}

			// Token: 0x060002A9 RID: 681 RVA: 0x00009352 File Offset: 0x00007552
			public void GetResult(short token)
			{
				this.awaiter.GetResult();
			}

			// Token: 0x060002AA RID: 682 RVA: 0x00009360 File Offset: 0x00007560
			public UniTaskStatus GetStatus(short token)
			{
				Func<UniTask> f = Interlocked.Exchange<Func<UniTask>>(ref this.factory, null);
				if (f != null)
				{
					this.task = f();
					this.awaiter = this.task.GetAwaiter();
				}
				return this.task.Status;
			}

			// Token: 0x060002AB RID: 683 RVA: 0x000093A5 File Offset: 0x000075A5
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.awaiter.SourceOnCompleted(continuation, state);
			}

			// Token: 0x060002AC RID: 684 RVA: 0x000093B4 File Offset: 0x000075B4
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.task.Status;
			}

			// Token: 0x04000155 RID: 341
			private Func<UniTask> factory;

			// Token: 0x04000156 RID: 342
			private UniTask task;

			// Token: 0x04000157 RID: 343
			private UniTask.Awaiter awaiter;
		}

		// Token: 0x0200008D RID: 141
		private sealed class DeferPromise<T> : IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>
		{
			// Token: 0x060002AD RID: 685 RVA: 0x000093C1 File Offset: 0x000075C1
			public DeferPromise(Func<UniTask<T>> factory)
			{
				this.factory = factory;
			}

			// Token: 0x060002AE RID: 686 RVA: 0x000093D0 File Offset: 0x000075D0
			public T GetResult(short token)
			{
				return this.awaiter.GetResult();
			}

			// Token: 0x060002AF RID: 687 RVA: 0x000093DD File Offset: 0x000075DD
			void IUniTaskSource.GetResult(short token)
			{
				this.awaiter.GetResult();
			}

			// Token: 0x060002B0 RID: 688 RVA: 0x000093EC File Offset: 0x000075EC
			public UniTaskStatus GetStatus(short token)
			{
				Func<UniTask<T>> f = Interlocked.Exchange<Func<UniTask<T>>>(ref this.factory, null);
				if (f != null)
				{
					this.task = f();
					this.awaiter = this.task.GetAwaiter();
				}
				return this.task.Status;
			}

			// Token: 0x060002B1 RID: 689 RVA: 0x00009431 File Offset: 0x00007631
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.awaiter.SourceOnCompleted(continuation, state);
			}

			// Token: 0x060002B2 RID: 690 RVA: 0x00009440 File Offset: 0x00007640
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.task.Status;
			}

			// Token: 0x04000158 RID: 344
			private Func<UniTask<T>> factory;

			// Token: 0x04000159 RID: 345
			private UniTask<T> task;

			// Token: 0x0400015A RID: 346
			private UniTask<T>.Awaiter awaiter;
		}

		// Token: 0x0200008E RID: 142
		private sealed class DeferPromiseWithState<TState> : IUniTaskSource, IValueTaskSource
		{
			// Token: 0x060002B3 RID: 691 RVA: 0x0000944D File Offset: 0x0000764D
			public DeferPromiseWithState(TState argument, Func<TState, UniTask> factory)
			{
				this.argument = argument;
				this.factory = factory;
			}

			// Token: 0x060002B4 RID: 692 RVA: 0x00009463 File Offset: 0x00007663
			public void GetResult(short token)
			{
				this.awaiter.GetResult();
			}

			// Token: 0x060002B5 RID: 693 RVA: 0x00009470 File Offset: 0x00007670
			public UniTaskStatus GetStatus(short token)
			{
				Func<TState, UniTask> f = Interlocked.Exchange<Func<TState, UniTask>>(ref this.factory, null);
				if (f != null)
				{
					this.task = f(this.argument);
					this.awaiter = this.task.GetAwaiter();
				}
				return this.task.Status;
			}

			// Token: 0x060002B6 RID: 694 RVA: 0x000094BB File Offset: 0x000076BB
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.awaiter.SourceOnCompleted(continuation, state);
			}

			// Token: 0x060002B7 RID: 695 RVA: 0x000094CA File Offset: 0x000076CA
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.task.Status;
			}

			// Token: 0x0400015B RID: 347
			private Func<TState, UniTask> factory;

			// Token: 0x0400015C RID: 348
			private TState argument;

			// Token: 0x0400015D RID: 349
			private UniTask task;

			// Token: 0x0400015E RID: 350
			private UniTask.Awaiter awaiter;
		}

		// Token: 0x0200008F RID: 143
		private sealed class DeferPromiseWithState<TState, TResult> : IUniTaskSource<TResult>, IUniTaskSource, IValueTaskSource, IValueTaskSource<TResult>
		{
			// Token: 0x060002B8 RID: 696 RVA: 0x000094D7 File Offset: 0x000076D7
			public DeferPromiseWithState(TState argument, Func<TState, UniTask<TResult>> factory)
			{
				this.argument = argument;
				this.factory = factory;
			}

			// Token: 0x060002B9 RID: 697 RVA: 0x000094ED File Offset: 0x000076ED
			public TResult GetResult(short token)
			{
				return this.awaiter.GetResult();
			}

			// Token: 0x060002BA RID: 698 RVA: 0x000094FA File Offset: 0x000076FA
			void IUniTaskSource.GetResult(short token)
			{
				this.awaiter.GetResult();
			}

			// Token: 0x060002BB RID: 699 RVA: 0x00009508 File Offset: 0x00007708
			public UniTaskStatus GetStatus(short token)
			{
				Func<TState, UniTask<TResult>> f = Interlocked.Exchange<Func<TState, UniTask<TResult>>>(ref this.factory, null);
				if (f != null)
				{
					this.task = f(this.argument);
					this.awaiter = this.task.GetAwaiter();
				}
				return this.task.Status;
			}

			// Token: 0x060002BC RID: 700 RVA: 0x00009553 File Offset: 0x00007753
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.awaiter.SourceOnCompleted(continuation, state);
			}

			// Token: 0x060002BD RID: 701 RVA: 0x00009562 File Offset: 0x00007762
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.task.Status;
			}

			// Token: 0x0400015F RID: 351
			private Func<TState, UniTask<TResult>> factory;

			// Token: 0x04000160 RID: 352
			private TState argument;

			// Token: 0x04000161 RID: 353
			private UniTask<TResult> task;

			// Token: 0x04000162 RID: 354
			private UniTask<TResult>.Awaiter awaiter;
		}

		// Token: 0x02000090 RID: 144
		private sealed class NeverPromise<T> : IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>
		{
			// Token: 0x060002BE RID: 702 RVA: 0x0000956F File Offset: 0x0000776F
			public NeverPromise(CancellationToken cancellationToken)
			{
				this.cancellationToken = cancellationToken;
				if (this.cancellationToken.CanBeCanceled)
				{
					this.cancellationToken.RegisterWithoutCaptureExecutionContext(UniTask.NeverPromise<T>.cancellationCallback, this);
				}
			}

			// Token: 0x060002BF RID: 703 RVA: 0x000095A0 File Offset: 0x000077A0
			private static void CancellationCallback(object state)
			{
				UniTask.NeverPromise<T> self = (UniTask.NeverPromise<T>)state;
				self.core.TrySetCanceled(self.cancellationToken);
			}

			// Token: 0x060002C0 RID: 704 RVA: 0x000095C6 File Offset: 0x000077C6
			public T GetResult(short token)
			{
				return this.core.GetResult(token);
			}

			// Token: 0x060002C1 RID: 705 RVA: 0x000095D4 File Offset: 0x000077D4
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060002C2 RID: 706 RVA: 0x000095E2 File Offset: 0x000077E2
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060002C3 RID: 707 RVA: 0x000095EF File Offset: 0x000077EF
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060002C4 RID: 708 RVA: 0x000095FF File Offset: 0x000077FF
			void IUniTaskSource.GetResult(short token)
			{
				this.core.GetResult(token);
			}

			// Token: 0x04000163 RID: 355
			private static readonly Action<object> cancellationCallback = new Action<object>(UniTask.NeverPromise<T>.CancellationCallback);

			// Token: 0x04000164 RID: 356
			private CancellationToken cancellationToken;

			// Token: 0x04000165 RID: 357
			private UniTaskCompletionSourceCore<T> core;
		}

		// Token: 0x02000091 RID: 145
		private sealed class WaitUntilPromise : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UniTask.WaitUntilPromise>
		{
			// Token: 0x17000039 RID: 57
			// (get) Token: 0x060002C6 RID: 710 RVA: 0x00009621 File Offset: 0x00007821
			public ref UniTask.WaitUntilPromise NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x060002C7 RID: 711 RVA: 0x00009629 File Offset: 0x00007829
			static WaitUntilPromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.WaitUntilPromise), () => UniTask.WaitUntilPromise.pool.Size);
			}

			// Token: 0x060002C8 RID: 712 RVA: 0x000020BB File Offset: 0x000002BB
			private WaitUntilPromise()
			{
			}

			// Token: 0x060002C9 RID: 713 RVA: 0x0000964C File Offset: 0x0000784C
			public static IUniTaskSource Create(Func<bool> predicate, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.WaitUntilPromise result;
				if (!UniTask.WaitUntilPromise.pool.TryPop(out result))
				{
					result = new UniTask.WaitUntilPromise();
				}
				result.predicate = predicate;
				result.cancellationToken = cancellationToken;
				result.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.WaitUntilPromise promise = (UniTask.WaitUntilPromise)state;
						promise.core.TrySetCanceled(promise.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x060002CA RID: 714 RVA: 0x000096E4 File Offset: 0x000078E4
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x060002CB RID: 715 RVA: 0x00009730 File Offset: 0x00007930
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060002CC RID: 716 RVA: 0x0000973E File Offset: 0x0000793E
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060002CD RID: 717 RVA: 0x0000974B File Offset: 0x0000794B
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060002CE RID: 718 RVA: 0x0000975C File Offset: 0x0000795C
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				try
				{
					if (!this.predicate())
					{
						return true;
					}
				}
				catch (Exception ex)
				{
					this.core.TrySetException(ex);
					return false;
				}
				this.core.TrySetResult(null);
				return false;
			}

			// Token: 0x060002CF RID: 719 RVA: 0x000097D0 File Offset: 0x000079D0
			private bool TryReturn()
			{
				this.core.Reset();
				this.predicate = null;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.WaitUntilPromise.pool.TryPush(this);
			}

			// Token: 0x04000166 RID: 358
			private static TaskPool<UniTask.WaitUntilPromise> pool;

			// Token: 0x04000167 RID: 359
			private UniTask.WaitUntilPromise nextNode;

			// Token: 0x04000168 RID: 360
			private Func<bool> predicate;

			// Token: 0x04000169 RID: 361
			private CancellationToken cancellationToken;

			// Token: 0x0400016A RID: 362
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x0400016B RID: 363
			private bool cancelImmediately;

			// Token: 0x0400016C RID: 364
			private UniTaskCompletionSourceCore<object> core;
		}

		// Token: 0x02000093 RID: 147
		private sealed class WaitUntilPromise<T> : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UniTask.WaitUntilPromise<T>>
		{
			// Token: 0x1700003A RID: 58
			// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000984E File Offset: 0x00007A4E
			public ref UniTask.WaitUntilPromise<T> NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x060002D5 RID: 725 RVA: 0x00009856 File Offset: 0x00007A56
			static WaitUntilPromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.WaitUntilPromise<T>), () => UniTask.WaitUntilPromise<T>.pool.Size);
			}

			// Token: 0x060002D6 RID: 726 RVA: 0x000020BB File Offset: 0x000002BB
			private WaitUntilPromise()
			{
			}

			// Token: 0x060002D7 RID: 727 RVA: 0x00009878 File Offset: 0x00007A78
			public static IUniTaskSource Create(T argument, Func<T, bool> predicate, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.WaitUntilPromise<T> result;
				if (!UniTask.WaitUntilPromise<T>.pool.TryPop(out result))
				{
					result = new UniTask.WaitUntilPromise<T>();
				}
				result.predicate = predicate;
				result.argument = argument;
				result.cancellationToken = cancellationToken;
				result.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.WaitUntilPromise<T> promise = (UniTask.WaitUntilPromise<T>)state;
						promise.core.TrySetCanceled(promise.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x060002D8 RID: 728 RVA: 0x00009918 File Offset: 0x00007B18
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x060002D9 RID: 729 RVA: 0x00009964 File Offset: 0x00007B64
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060002DA RID: 730 RVA: 0x00009972 File Offset: 0x00007B72
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060002DB RID: 731 RVA: 0x0000997F File Offset: 0x00007B7F
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060002DC RID: 732 RVA: 0x00009990 File Offset: 0x00007B90
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				try
				{
					if (!this.predicate(this.argument))
					{
						return true;
					}
				}
				catch (Exception ex)
				{
					this.core.TrySetException(ex);
					return false;
				}
				this.core.TrySetResult(null);
				return false;
			}

			// Token: 0x060002DD RID: 733 RVA: 0x00009A0C File Offset: 0x00007C0C
			private bool TryReturn()
			{
				this.core.Reset();
				this.predicate = null;
				this.argument = default(T);
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.WaitUntilPromise<T>.pool.TryPush(this);
			}

			// Token: 0x0400016F RID: 367
			private static TaskPool<UniTask.WaitUntilPromise<T>> pool;

			// Token: 0x04000170 RID: 368
			private UniTask.WaitUntilPromise<T> nextNode;

			// Token: 0x04000171 RID: 369
			private Func<T, bool> predicate;

			// Token: 0x04000172 RID: 370
			private T argument;

			// Token: 0x04000173 RID: 371
			private CancellationToken cancellationToken;

			// Token: 0x04000174 RID: 372
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x04000175 RID: 373
			private bool cancelImmediately;

			// Token: 0x04000176 RID: 374
			private UniTaskCompletionSourceCore<object> core;
		}

		// Token: 0x02000095 RID: 149
		private sealed class WaitWhilePromise : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UniTask.WaitWhilePromise>
		{
			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060002E2 RID: 738 RVA: 0x00009A9E File Offset: 0x00007C9E
			public ref UniTask.WaitWhilePromise NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x060002E3 RID: 739 RVA: 0x00009AA6 File Offset: 0x00007CA6
			static WaitWhilePromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.WaitWhilePromise), () => UniTask.WaitWhilePromise.pool.Size);
			}

			// Token: 0x060002E4 RID: 740 RVA: 0x000020BB File Offset: 0x000002BB
			private WaitWhilePromise()
			{
			}

			// Token: 0x060002E5 RID: 741 RVA: 0x00009AC8 File Offset: 0x00007CC8
			public static IUniTaskSource Create(Func<bool> predicate, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.WaitWhilePromise result;
				if (!UniTask.WaitWhilePromise.pool.TryPop(out result))
				{
					result = new UniTask.WaitWhilePromise();
				}
				result.predicate = predicate;
				result.cancellationToken = cancellationToken;
				result.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.WaitWhilePromise promise = (UniTask.WaitWhilePromise)state;
						promise.core.TrySetCanceled(promise.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x060002E6 RID: 742 RVA: 0x00009B60 File Offset: 0x00007D60
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x060002E7 RID: 743 RVA: 0x00009BAC File Offset: 0x00007DAC
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060002E8 RID: 744 RVA: 0x00009BBA File Offset: 0x00007DBA
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060002E9 RID: 745 RVA: 0x00009BC7 File Offset: 0x00007DC7
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060002EA RID: 746 RVA: 0x00009BD8 File Offset: 0x00007DD8
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				try
				{
					if (this.predicate())
					{
						return true;
					}
				}
				catch (Exception ex)
				{
					this.core.TrySetException(ex);
					return false;
				}
				this.core.TrySetResult(null);
				return false;
			}

			// Token: 0x060002EB RID: 747 RVA: 0x00009C4C File Offset: 0x00007E4C
			private bool TryReturn()
			{
				this.core.Reset();
				this.predicate = null;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.WaitWhilePromise.pool.TryPush(this);
			}

			// Token: 0x04000179 RID: 377
			private static TaskPool<UniTask.WaitWhilePromise> pool;

			// Token: 0x0400017A RID: 378
			private UniTask.WaitWhilePromise nextNode;

			// Token: 0x0400017B RID: 379
			private Func<bool> predicate;

			// Token: 0x0400017C RID: 380
			private CancellationToken cancellationToken;

			// Token: 0x0400017D RID: 381
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x0400017E RID: 382
			private bool cancelImmediately;

			// Token: 0x0400017F RID: 383
			private UniTaskCompletionSourceCore<object> core;
		}

		// Token: 0x02000097 RID: 151
		private sealed class WaitWhilePromise<T> : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UniTask.WaitWhilePromise<T>>
		{
			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060002F0 RID: 752 RVA: 0x00009CCA File Offset: 0x00007ECA
			public ref UniTask.WaitWhilePromise<T> NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x060002F1 RID: 753 RVA: 0x00009CD2 File Offset: 0x00007ED2
			static WaitWhilePromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.WaitWhilePromise<T>), () => UniTask.WaitWhilePromise<T>.pool.Size);
			}

			// Token: 0x060002F2 RID: 754 RVA: 0x000020BB File Offset: 0x000002BB
			private WaitWhilePromise()
			{
			}

			// Token: 0x060002F3 RID: 755 RVA: 0x00009CF4 File Offset: 0x00007EF4
			public static IUniTaskSource Create(T argument, Func<T, bool> predicate, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.WaitWhilePromise<T> result;
				if (!UniTask.WaitWhilePromise<T>.pool.TryPop(out result))
				{
					result = new UniTask.WaitWhilePromise<T>();
				}
				result.predicate = predicate;
				result.argument = argument;
				result.cancellationToken = cancellationToken;
				result.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.WaitWhilePromise<T> promise = (UniTask.WaitWhilePromise<T>)state;
						promise.core.TrySetCanceled(promise.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x060002F4 RID: 756 RVA: 0x00009D94 File Offset: 0x00007F94
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x060002F5 RID: 757 RVA: 0x00009DE0 File Offset: 0x00007FE0
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060002F6 RID: 758 RVA: 0x00009DEE File Offset: 0x00007FEE
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060002F7 RID: 759 RVA: 0x00009DFB File Offset: 0x00007FFB
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060002F8 RID: 760 RVA: 0x00009E0C File Offset: 0x0000800C
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				try
				{
					if (this.predicate(this.argument))
					{
						return true;
					}
				}
				catch (Exception ex)
				{
					this.core.TrySetException(ex);
					return false;
				}
				this.core.TrySetResult(null);
				return false;
			}

			// Token: 0x060002F9 RID: 761 RVA: 0x00009E88 File Offset: 0x00008088
			private bool TryReturn()
			{
				this.core.Reset();
				this.predicate = null;
				this.argument = default(T);
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.WaitWhilePromise<T>.pool.TryPush(this);
			}

			// Token: 0x04000182 RID: 386
			private static TaskPool<UniTask.WaitWhilePromise<T>> pool;

			// Token: 0x04000183 RID: 387
			private UniTask.WaitWhilePromise<T> nextNode;

			// Token: 0x04000184 RID: 388
			private Func<T, bool> predicate;

			// Token: 0x04000185 RID: 389
			private T argument;

			// Token: 0x04000186 RID: 390
			private CancellationToken cancellationToken;

			// Token: 0x04000187 RID: 391
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x04000188 RID: 392
			private bool cancelImmediately;

			// Token: 0x04000189 RID: 393
			private UniTaskCompletionSourceCore<object> core;
		}

		// Token: 0x02000099 RID: 153
		private sealed class WaitUntilCanceledPromise : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UniTask.WaitUntilCanceledPromise>
		{
			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060002FE RID: 766 RVA: 0x00009F1A File Offset: 0x0000811A
			public ref UniTask.WaitUntilCanceledPromise NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x060002FF RID: 767 RVA: 0x00009F22 File Offset: 0x00008122
			static WaitUntilCanceledPromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.WaitUntilCanceledPromise), () => UniTask.WaitUntilCanceledPromise.pool.Size);
			}

			// Token: 0x06000300 RID: 768 RVA: 0x000020BB File Offset: 0x000002BB
			private WaitUntilCanceledPromise()
			{
			}

			// Token: 0x06000301 RID: 769 RVA: 0x00009F44 File Offset: 0x00008144
			public static IUniTaskSource Create(CancellationToken cancellationToken, PlayerLoopTiming timing, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.WaitUntilCanceledPromise result;
				if (!UniTask.WaitUntilCanceledPromise.pool.TryPop(out result))
				{
					result = new UniTask.WaitUntilCanceledPromise();
				}
				result.cancellationToken = cancellationToken;
				result.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						((UniTask.WaitUntilCanceledPromise)state).core.TrySetResult(null);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x06000302 RID: 770 RVA: 0x00009FD4 File Offset: 0x000081D4
			public void GetResult(short token)
			{
				try
				{
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x06000303 RID: 771 RVA: 0x0000A020 File Offset: 0x00008220
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000304 RID: 772 RVA: 0x0000A02E File Offset: 0x0000822E
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000305 RID: 773 RVA: 0x0000A03B File Offset: 0x0000823B
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000306 RID: 774 RVA: 0x0000A04B File Offset: 0x0000824B
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetResult(null);
					return false;
				}
				return true;
			}

			// Token: 0x06000307 RID: 775 RVA: 0x0000A06A File Offset: 0x0000826A
			private bool TryReturn()
			{
				this.core.Reset();
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.WaitUntilCanceledPromise.pool.TryPush(this);
			}

			// Token: 0x0400018C RID: 396
			private static TaskPool<UniTask.WaitUntilCanceledPromise> pool;

			// Token: 0x0400018D RID: 397
			private UniTask.WaitUntilCanceledPromise nextNode;

			// Token: 0x0400018E RID: 398
			private CancellationToken cancellationToken;

			// Token: 0x0400018F RID: 399
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x04000190 RID: 400
			private bool cancelImmediately;

			// Token: 0x04000191 RID: 401
			private UniTaskCompletionSourceCore<object> core;
		}

		// Token: 0x0200009B RID: 155
		private sealed class WaitUntilValueChangedUnityObjectPromise<T, U> : IUniTaskSource<U>, IUniTaskSource, IValueTaskSource, IValueTaskSource<U>, IPlayerLoopItem, ITaskPoolNode<UniTask.WaitUntilValueChangedUnityObjectPromise<T, U>>
		{
			// Token: 0x1700003E RID: 62
			// (get) Token: 0x0600030C RID: 780 RVA: 0x0000A0CC File Offset: 0x000082CC
			public ref UniTask.WaitUntilValueChangedUnityObjectPromise<T, U> NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x0600030D RID: 781 RVA: 0x0000A0D4 File Offset: 0x000082D4
			static WaitUntilValueChangedUnityObjectPromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.WaitUntilValueChangedUnityObjectPromise<T, U>), () => UniTask.WaitUntilValueChangedUnityObjectPromise<T, U>.pool.Size);
			}

			// Token: 0x0600030E RID: 782 RVA: 0x000020BB File Offset: 0x000002BB
			private WaitUntilValueChangedUnityObjectPromise()
			{
			}

			// Token: 0x0600030F RID: 783 RVA: 0x0000A0F8 File Offset: 0x000082F8
			public static IUniTaskSource<U> Create(T target, Func<T, U> monitorFunction, IEqualityComparer<U> equalityComparer, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource<U>.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.WaitUntilValueChangedUnityObjectPromise<T, U> result;
				if (!UniTask.WaitUntilValueChangedUnityObjectPromise<T, U>.pool.TryPop(out result))
				{
					result = new UniTask.WaitUntilValueChangedUnityObjectPromise<T, U>();
				}
				result.target = target;
				result.targetAsUnityObject = target as global::UnityEngine.Object;
				result.monitorFunction = monitorFunction;
				result.currentValue = monitorFunction(target);
				result.equalityComparer = equalityComparer ?? UnityEqualityComparer.GetDefault<U>();
				result.cancellationToken = cancellationToken;
				result.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.WaitUntilValueChangedUnityObjectPromise<T, U> promise = (UniTask.WaitUntilValueChangedUnityObjectPromise<T, U>)state;
						promise.core.TrySetCanceled(promise.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x06000310 RID: 784 RVA: 0x0000A1C8 File Offset: 0x000083C8
			public U GetResult(short token)
			{
				U result;
				try
				{
					result = this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
				return result;
			}

			// Token: 0x06000311 RID: 785 RVA: 0x0000A214 File Offset: 0x00008414
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x06000312 RID: 786 RVA: 0x0000A21E File Offset: 0x0000841E
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000313 RID: 787 RVA: 0x0000A22C File Offset: 0x0000842C
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000314 RID: 788 RVA: 0x0000A239 File Offset: 0x00008439
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000315 RID: 789 RVA: 0x0000A24C File Offset: 0x0000844C
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested || this.targetAsUnityObject == null)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				U nextValue = default(U);
				try
				{
					nextValue = this.monitorFunction(this.target);
					if (this.equalityComparer.Equals(this.currentValue, nextValue))
					{
						return true;
					}
				}
				catch (Exception ex)
				{
					this.core.TrySetException(ex);
					return false;
				}
				this.core.TrySetResult(nextValue);
				return false;
			}

			// Token: 0x06000316 RID: 790 RVA: 0x0000A2F0 File Offset: 0x000084F0
			private bool TryReturn()
			{
				this.core.Reset();
				this.target = default(T);
				this.currentValue = default(U);
				this.monitorFunction = null;
				this.equalityComparer = null;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.WaitUntilValueChangedUnityObjectPromise<T, U>.pool.TryPush(this);
			}

			// Token: 0x04000194 RID: 404
			private static TaskPool<UniTask.WaitUntilValueChangedUnityObjectPromise<T, U>> pool;

			// Token: 0x04000195 RID: 405
			private UniTask.WaitUntilValueChangedUnityObjectPromise<T, U> nextNode;

			// Token: 0x04000196 RID: 406
			private T target;

			// Token: 0x04000197 RID: 407
			private global::UnityEngine.Object targetAsUnityObject;

			// Token: 0x04000198 RID: 408
			private U currentValue;

			// Token: 0x04000199 RID: 409
			private Func<T, U> monitorFunction;

			// Token: 0x0400019A RID: 410
			private IEqualityComparer<U> equalityComparer;

			// Token: 0x0400019B RID: 411
			private CancellationToken cancellationToken;

			// Token: 0x0400019C RID: 412
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x0400019D RID: 413
			private bool cancelImmediately;

			// Token: 0x0400019E RID: 414
			private UniTaskCompletionSourceCore<U> core;
		}

		// Token: 0x0200009D RID: 157
		private sealed class WaitUntilValueChangedStandardObjectPromise<T, U> : IUniTaskSource<U>, IUniTaskSource, IValueTaskSource, IValueTaskSource<U>, IPlayerLoopItem, ITaskPoolNode<UniTask.WaitUntilValueChangedStandardObjectPromise<T, U>> where T : class
		{
			// Token: 0x1700003F RID: 63
			// (get) Token: 0x0600031B RID: 795 RVA: 0x0000A396 File Offset: 0x00008596
			public ref UniTask.WaitUntilValueChangedStandardObjectPromise<T, U> NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x0600031C RID: 796 RVA: 0x0000A39E File Offset: 0x0000859E
			static WaitUntilValueChangedStandardObjectPromise()
			{
				TaskPool.RegisterSizeGetter(typeof(UniTask.WaitUntilValueChangedStandardObjectPromise<T, U>), () => UniTask.WaitUntilValueChangedStandardObjectPromise<T, U>.pool.Size);
			}

			// Token: 0x0600031D RID: 797 RVA: 0x000020BB File Offset: 0x000002BB
			private WaitUntilValueChangedStandardObjectPromise()
			{
			}

			// Token: 0x0600031E RID: 798 RVA: 0x0000A3C0 File Offset: 0x000085C0
			public static IUniTaskSource<U> Create(T target, Func<T, U> monitorFunction, IEqualityComparer<U> equalityComparer, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource<U>.CreateFromCanceled(cancellationToken, out token);
				}
				UniTask.WaitUntilValueChangedStandardObjectPromise<T, U> result;
				if (!UniTask.WaitUntilValueChangedStandardObjectPromise<T, U>.pool.TryPop(out result))
				{
					result = new UniTask.WaitUntilValueChangedStandardObjectPromise<T, U>();
				}
				result.target = new WeakReference<T>(target, false);
				result.monitorFunction = monitorFunction;
				result.currentValue = monitorFunction(target);
				result.equalityComparer = equalityComparer ?? UnityEqualityComparer.GetDefault<U>();
				result.cancellationToken = cancellationToken;
				result.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UniTask.WaitUntilValueChangedStandardObjectPromise<T, U> promise = (UniTask.WaitUntilValueChangedStandardObjectPromise<T, U>)state;
						promise.core.TrySetCanceled(promise.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x0600031F RID: 799 RVA: 0x0000A488 File Offset: 0x00008688
			public U GetResult(short token)
			{
				U result;
				try
				{
					result = this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
				return result;
			}

			// Token: 0x06000320 RID: 800 RVA: 0x0000A4D4 File Offset: 0x000086D4
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x06000321 RID: 801 RVA: 0x0000A4DE File Offset: 0x000086DE
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000322 RID: 802 RVA: 0x0000A4EC File Offset: 0x000086EC
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000323 RID: 803 RVA: 0x0000A4F9 File Offset: 0x000086F9
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000324 RID: 804 RVA: 0x0000A50C File Offset: 0x0000870C
			public bool MoveNext()
			{
				T t;
				if (this.cancellationToken.IsCancellationRequested || !this.target.TryGetTarget(out t))
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				U nextValue = default(U);
				try
				{
					nextValue = this.monitorFunction(t);
					if (this.equalityComparer.Equals(this.currentValue, nextValue))
					{
						return true;
					}
				}
				catch (Exception ex)
				{
					this.core.TrySetException(ex);
					return false;
				}
				this.core.TrySetResult(nextValue);
				return false;
			}

			// Token: 0x06000325 RID: 805 RVA: 0x0000A5AC File Offset: 0x000087AC
			private bool TryReturn()
			{
				this.core.Reset();
				this.target = null;
				this.currentValue = default(U);
				this.monitorFunction = null;
				this.equalityComparer = null;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UniTask.WaitUntilValueChangedStandardObjectPromise<T, U>.pool.TryPush(this);
			}

			// Token: 0x040001A1 RID: 417
			private static TaskPool<UniTask.WaitUntilValueChangedStandardObjectPromise<T, U>> pool;

			// Token: 0x040001A2 RID: 418
			private UniTask.WaitUntilValueChangedStandardObjectPromise<T, U> nextNode;

			// Token: 0x040001A3 RID: 419
			private WeakReference<T> target;

			// Token: 0x040001A4 RID: 420
			private U currentValue;

			// Token: 0x040001A5 RID: 421
			private Func<T, U> monitorFunction;

			// Token: 0x040001A6 RID: 422
			private IEqualityComparer<U> equalityComparer;

			// Token: 0x040001A7 RID: 423
			private CancellationToken cancellationToken;

			// Token: 0x040001A8 RID: 424
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x040001A9 RID: 425
			private bool cancelImmediately;

			// Token: 0x040001AA RID: 426
			private UniTaskCompletionSourceCore<U> core;
		}

		// Token: 0x0200009F RID: 159
		private sealed class WhenAllPromise<T1, T2> : IUniTaskSource<ValueTuple<T1, T2>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2>>
		{
			// Token: 0x0600032A RID: 810 RVA: 0x0000A650 File Offset: 0x00008850
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAllPromise<T1, T2>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2>.TryInvokeContinuationT2(this, in awaiter2);
					return;
				}
				awaiter2.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAllPromise<T1, T2>, UniTask<T2>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2>, UniTask<T2>.Awaiter>(this, awaiter2));
			}

			// Token: 0x0600032B RID: 811 RVA: 0x0000A6FC File Offset: 0x000088FC
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 2)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2>(self.t1, self.t2));
				}
			}

			// Token: 0x0600032C RID: 812 RVA: 0x0000A764 File Offset: 0x00008964
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 2)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2>(self.t1, self.t2));
				}
			}

			// Token: 0x0600032D RID: 813 RVA: 0x0000A7CC File Offset: 0x000089CC
			public ValueTuple<T1, T2> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600032E RID: 814 RVA: 0x0000A7E0 File Offset: 0x000089E0
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0600032F RID: 815 RVA: 0x0000A7EA File Offset: 0x000089EA
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000330 RID: 816 RVA: 0x0000A7F8 File Offset: 0x000089F8
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000331 RID: 817 RVA: 0x0000A805 File Offset: 0x00008A05
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x040001AD RID: 429
			private T1 t1;

			// Token: 0x040001AE RID: 430
			private T2 t2;

			// Token: 0x040001AF RID: 431
			private int completedCount;

			// Token: 0x040001B0 RID: 432
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2>> core;
		}

		// Token: 0x020000A1 RID: 161
		private sealed class WhenAllPromise<T1, T2, T3> : IUniTaskSource<ValueTuple<T1, T2, T3>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3>>
		{
			// Token: 0x06000336 RID: 822 RVA: 0x0000A8AC File Offset: 0x00008AAC
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3>.TryInvokeContinuationT3(this, in awaiter3);
					return;
				}
				awaiter3.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3>, UniTask<T3>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3>, UniTask<T3>.Awaiter>(this, awaiter3));
			}

			// Token: 0x06000337 RID: 823 RVA: 0x0000A9A0 File Offset: 0x00008BA0
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 3)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3>(self.t1, self.t2, self.t3));
				}
			}

			// Token: 0x06000338 RID: 824 RVA: 0x0000AA0C File Offset: 0x00008C0C
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 3)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3>(self.t1, self.t2, self.t3));
				}
			}

			// Token: 0x06000339 RID: 825 RVA: 0x0000AA78 File Offset: 0x00008C78
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 3)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3>(self.t1, self.t2, self.t3));
				}
			}

			// Token: 0x0600033A RID: 826 RVA: 0x0000AAE4 File Offset: 0x00008CE4
			public ValueTuple<T1, T2, T3> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600033B RID: 827 RVA: 0x0000AAF8 File Offset: 0x00008CF8
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0600033C RID: 828 RVA: 0x0000AB02 File Offset: 0x00008D02
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600033D RID: 829 RVA: 0x0000AB10 File Offset: 0x00008D10
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600033E RID: 830 RVA: 0x0000AB1D File Offset: 0x00008D1D
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x040001B4 RID: 436
			private T1 t1;

			// Token: 0x040001B5 RID: 437
			private T2 t2;

			// Token: 0x040001B6 RID: 438
			private T3 t3;

			// Token: 0x040001B7 RID: 439
			private int completedCount;

			// Token: 0x040001B8 RID: 440
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3>> core;
		}

		// Token: 0x020000A3 RID: 163
		private sealed class WhenAllPromise<T1, T2, T3, T4> : IUniTaskSource<ValueTuple<T1, T2, T3, T4>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4>>
		{
			// Token: 0x06000344 RID: 836 RVA: 0x0000AC08 File Offset: 0x00008E08
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4>.TryInvokeContinuationT4(this, in awaiter4);
					return;
				}
				awaiter4.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T4>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4>, UniTask<T4>.Awaiter>(this, awaiter4));
			}

			// Token: 0x06000345 RID: 837 RVA: 0x0000AD44 File Offset: 0x00008F44
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 4)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4>(self.t1, self.t2, self.t3, self.t4));
				}
			}

			// Token: 0x06000346 RID: 838 RVA: 0x0000ADB8 File Offset: 0x00008FB8
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 4)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4>(self.t1, self.t2, self.t3, self.t4));
				}
			}

			// Token: 0x06000347 RID: 839 RVA: 0x0000AE2C File Offset: 0x0000902C
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 4)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4>(self.t1, self.t2, self.t3, self.t4));
				}
			}

			// Token: 0x06000348 RID: 840 RVA: 0x0000AEA0 File Offset: 0x000090A0
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 4)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4>(self.t1, self.t2, self.t3, self.t4));
				}
			}

			// Token: 0x06000349 RID: 841 RVA: 0x0000AF14 File Offset: 0x00009114
			public ValueTuple<T1, T2, T3, T4> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600034A RID: 842 RVA: 0x0000AF28 File Offset: 0x00009128
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0600034B RID: 843 RVA: 0x0000AF32 File Offset: 0x00009132
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600034C RID: 844 RVA: 0x0000AF40 File Offset: 0x00009140
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600034D RID: 845 RVA: 0x0000AF4D File Offset: 0x0000914D
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x040001BD RID: 445
			private T1 t1;

			// Token: 0x040001BE RID: 446
			private T2 t2;

			// Token: 0x040001BF RID: 447
			private T3 t3;

			// Token: 0x040001C0 RID: 448
			private T4 t4;

			// Token: 0x040001C1 RID: 449
			private int completedCount;

			// Token: 0x040001C2 RID: 450
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4>> core;
		}

		// Token: 0x020000A5 RID: 165
		private sealed class WhenAllPromise<T1, T2, T3, T4, T5> : IUniTaskSource<ValueTuple<T1, T2, T3, T4, T5>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4, T5>>
		{
			// Token: 0x06000354 RID: 852 RVA: 0x0000B07C File Offset: 0x0000927C
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT5(this, in awaiter5);
					return;
				}
				awaiter5.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T5>.Awaiter> t5 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T5>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT5(t5.Item1, in t5.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5>, UniTask<T5>.Awaiter>(this, awaiter5));
			}

			// Token: 0x06000355 RID: 853 RVA: 0x0000B200 File Offset: 0x00009400
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4, T5> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 5)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5>(self.t1, self.t2, self.t3, self.t4, self.t5));
				}
			}

			// Token: 0x06000356 RID: 854 RVA: 0x0000B278 File Offset: 0x00009478
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4, T5> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 5)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5>(self.t1, self.t2, self.t3, self.t4, self.t5));
				}
			}

			// Token: 0x06000357 RID: 855 RVA: 0x0000B2F0 File Offset: 0x000094F0
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4, T5> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 5)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5>(self.t1, self.t2, self.t3, self.t4, self.t5));
				}
			}

			// Token: 0x06000358 RID: 856 RVA: 0x0000B368 File Offset: 0x00009568
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4, T5> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 5)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5>(self.t1, self.t2, self.t3, self.t4, self.t5));
				}
			}

			// Token: 0x06000359 RID: 857 RVA: 0x0000B3E0 File Offset: 0x000095E0
			private static void TryInvokeContinuationT5(UniTask.WhenAllPromise<T1, T2, T3, T4, T5> self, in UniTask<T5>.Awaiter awaiter)
			{
				try
				{
					self.t5 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 5)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5>(self.t1, self.t2, self.t3, self.t4, self.t5));
				}
			}

			// Token: 0x0600035A RID: 858 RVA: 0x0000B458 File Offset: 0x00009658
			public ValueTuple<T1, T2, T3, T4, T5> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600035B RID: 859 RVA: 0x0000B46C File Offset: 0x0000966C
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0600035C RID: 860 RVA: 0x0000B476 File Offset: 0x00009676
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600035D RID: 861 RVA: 0x0000B484 File Offset: 0x00009684
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600035E RID: 862 RVA: 0x0000B491 File Offset: 0x00009691
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x040001C8 RID: 456
			private T1 t1;

			// Token: 0x040001C9 RID: 457
			private T2 t2;

			// Token: 0x040001CA RID: 458
			private T3 t3;

			// Token: 0x040001CB RID: 459
			private T4 t4;

			// Token: 0x040001CC RID: 460
			private T5 t5;

			// Token: 0x040001CD RID: 461
			private int completedCount;

			// Token: 0x040001CE RID: 462
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4, T5>> core;
		}

		// Token: 0x020000A7 RID: 167
		private sealed class WhenAllPromise<T1, T2, T3, T4, T5, T6> : IUniTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6>>
		{
			// Token: 0x06000366 RID: 870 RVA: 0x0000B604 File Offset: 0x00009804
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T5>.Awaiter> t5 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT5(t5.Item1, in t5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT6(this, in awaiter6);
					return;
				}
				awaiter6.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T6>.Awaiter> t6 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T6>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT6(t6.Item1, in t6.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6>, UniTask<T6>.Awaiter>(this, awaiter6));
			}

			// Token: 0x06000367 RID: 871 RVA: 0x0000B7D4 File Offset: 0x000099D4
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 6)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6));
				}
			}

			// Token: 0x06000368 RID: 872 RVA: 0x0000B854 File Offset: 0x00009A54
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 6)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6));
				}
			}

			// Token: 0x06000369 RID: 873 RVA: 0x0000B8D4 File Offset: 0x00009AD4
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 6)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6));
				}
			}

			// Token: 0x0600036A RID: 874 RVA: 0x0000B954 File Offset: 0x00009B54
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 6)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6));
				}
			}

			// Token: 0x0600036B RID: 875 RVA: 0x0000B9D4 File Offset: 0x00009BD4
			private static void TryInvokeContinuationT5(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T5>.Awaiter awaiter)
			{
				try
				{
					self.t5 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 6)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6));
				}
			}

			// Token: 0x0600036C RID: 876 RVA: 0x0000BA54 File Offset: 0x00009C54
			private static void TryInvokeContinuationT6(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T6>.Awaiter awaiter)
			{
				try
				{
					self.t6 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 6)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6));
				}
			}

			// Token: 0x0600036D RID: 877 RVA: 0x0000BAD4 File Offset: 0x00009CD4
			public ValueTuple<T1, T2, T3, T4, T5, T6> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600036E RID: 878 RVA: 0x0000BAE8 File Offset: 0x00009CE8
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0600036F RID: 879 RVA: 0x0000BAF2 File Offset: 0x00009CF2
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000370 RID: 880 RVA: 0x0000BB00 File Offset: 0x00009D00
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000371 RID: 881 RVA: 0x0000BB0D File Offset: 0x00009D0D
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x040001D5 RID: 469
			private T1 t1;

			// Token: 0x040001D6 RID: 470
			private T2 t2;

			// Token: 0x040001D7 RID: 471
			private T3 t3;

			// Token: 0x040001D8 RID: 472
			private T4 t4;

			// Token: 0x040001D9 RID: 473
			private T5 t5;

			// Token: 0x040001DA RID: 474
			private T6 t6;

			// Token: 0x040001DB RID: 475
			private int completedCount;

			// Token: 0x040001DC RID: 476
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4, T5, T6>> core;
		}

		// Token: 0x020000A9 RID: 169
		private sealed class WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> : IUniTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>
		{
			// Token: 0x0600037A RID: 890 RVA: 0x0000BCC4 File Offset: 0x00009EC4
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T5>.Awaiter> t5 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT5(t5.Item1, in t5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T6>.Awaiter> t6 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT6(t6.Item1, in t6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT7(this, in awaiter7);
					return;
				}
				awaiter7.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T7>.Awaiter> t7 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T7>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT7(t7.Item1, in t7.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T7>.Awaiter>(this, awaiter7));
			}

			// Token: 0x0600037B RID: 891 RVA: 0x0000BEDC File Offset: 0x0000A0DC
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 7)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7));
				}
			}

			// Token: 0x0600037C RID: 892 RVA: 0x0000BF60 File Offset: 0x0000A160
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 7)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7));
				}
			}

			// Token: 0x0600037D RID: 893 RVA: 0x0000BFE4 File Offset: 0x0000A1E4
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 7)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7));
				}
			}

			// Token: 0x0600037E RID: 894 RVA: 0x0000C068 File Offset: 0x0000A268
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 7)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7));
				}
			}

			// Token: 0x0600037F RID: 895 RVA: 0x0000C0EC File Offset: 0x0000A2EC
			private static void TryInvokeContinuationT5(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T5>.Awaiter awaiter)
			{
				try
				{
					self.t5 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 7)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7));
				}
			}

			// Token: 0x06000380 RID: 896 RVA: 0x0000C170 File Offset: 0x0000A370
			private static void TryInvokeContinuationT6(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T6>.Awaiter awaiter)
			{
				try
				{
					self.t6 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 7)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7));
				}
			}

			// Token: 0x06000381 RID: 897 RVA: 0x0000C1F4 File Offset: 0x0000A3F4
			private static void TryInvokeContinuationT7(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T7>.Awaiter awaiter)
			{
				try
				{
					self.t7 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 7)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7));
				}
			}

			// Token: 0x06000382 RID: 898 RVA: 0x0000C278 File Offset: 0x0000A478
			public ValueTuple<T1, T2, T3, T4, T5, T6, T7> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x06000383 RID: 899 RVA: 0x0000C28C File Offset: 0x0000A48C
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x06000384 RID: 900 RVA: 0x0000C296 File Offset: 0x0000A496
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000385 RID: 901 RVA: 0x0000C2A4 File Offset: 0x0000A4A4
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000386 RID: 902 RVA: 0x0000C2B1 File Offset: 0x0000A4B1
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x040001E4 RID: 484
			private T1 t1;

			// Token: 0x040001E5 RID: 485
			private T2 t2;

			// Token: 0x040001E6 RID: 486
			private T3 t3;

			// Token: 0x040001E7 RID: 487
			private T4 t4;

			// Token: 0x040001E8 RID: 488
			private T5 t5;

			// Token: 0x040001E9 RID: 489
			private T6 t6;

			// Token: 0x040001EA RID: 490
			private T7 t7;

			// Token: 0x040001EB RID: 491
			private int completedCount;

			// Token: 0x040001EC RID: 492
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4, T5, T6, T7>> core;
		}

		// Token: 0x020000AB RID: 171
		private sealed class WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8> : IUniTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>>
		{
			// Token: 0x06000390 RID: 912 RVA: 0x0000C4AC File Offset: 0x0000A6AC
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T5>.Awaiter> t5 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT5(t5.Item1, in t5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T6>.Awaiter> t6 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT6(t6.Item1, in t6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T7>.Awaiter> t7 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT7(t7.Item1, in t7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT8(this, in awaiter8);
					return;
				}
				awaiter8.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T8>.Awaiter> t8 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T8>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT8(t8.Item1, in t8.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T8>.Awaiter>(this, awaiter8));
			}

			// Token: 0x06000391 RID: 913 RVA: 0x0000C710 File Offset: 0x0000A910
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 8)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8>(self.t8)));
				}
			}

			// Token: 0x06000392 RID: 914 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 8)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8>(self.t8)));
				}
			}

			// Token: 0x06000393 RID: 915 RVA: 0x0000C830 File Offset: 0x0000AA30
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 8)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8>(self.t8)));
				}
			}

			// Token: 0x06000394 RID: 916 RVA: 0x0000C8C0 File Offset: 0x0000AAC0
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 8)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8>(self.t8)));
				}
			}

			// Token: 0x06000395 RID: 917 RVA: 0x0000C950 File Offset: 0x0000AB50
			private static void TryInvokeContinuationT5(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T5>.Awaiter awaiter)
			{
				try
				{
					self.t5 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 8)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8>(self.t8)));
				}
			}

			// Token: 0x06000396 RID: 918 RVA: 0x0000C9E0 File Offset: 0x0000ABE0
			private static void TryInvokeContinuationT6(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T6>.Awaiter awaiter)
			{
				try
				{
					self.t6 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 8)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8>(self.t8)));
				}
			}

			// Token: 0x06000397 RID: 919 RVA: 0x0000CA70 File Offset: 0x0000AC70
			private static void TryInvokeContinuationT7(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T7>.Awaiter awaiter)
			{
				try
				{
					self.t7 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 8)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8>(self.t8)));
				}
			}

			// Token: 0x06000398 RID: 920 RVA: 0x0000CB00 File Offset: 0x0000AD00
			private static void TryInvokeContinuationT8(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T8>.Awaiter awaiter)
			{
				try
				{
					self.t8 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 8)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8>(self.t8)));
				}
			}

			// Token: 0x06000399 RID: 921 RVA: 0x0000CB90 File Offset: 0x0000AD90
			public ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600039A RID: 922 RVA: 0x0000CBA4 File Offset: 0x0000ADA4
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0600039B RID: 923 RVA: 0x0000CBAE File Offset: 0x0000ADAE
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600039C RID: 924 RVA: 0x0000CBBC File Offset: 0x0000ADBC
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600039D RID: 925 RVA: 0x0000CBC9 File Offset: 0x0000ADC9
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x040001F5 RID: 501
			private T1 t1;

			// Token: 0x040001F6 RID: 502
			private T2 t2;

			// Token: 0x040001F7 RID: 503
			private T3 t3;

			// Token: 0x040001F8 RID: 504
			private T4 t4;

			// Token: 0x040001F9 RID: 505
			private T5 t5;

			// Token: 0x040001FA RID: 506
			private T6 t6;

			// Token: 0x040001FB RID: 507
			private T7 t7;

			// Token: 0x040001FC RID: 508
			private T8 t8;

			// Token: 0x040001FD RID: 509
			private int completedCount;

			// Token: 0x040001FE RID: 510
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>> core;
		}

		// Token: 0x020000AD RID: 173
		private sealed class WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IUniTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>>
		{
			// Token: 0x060003A8 RID: 936 RVA: 0x0000CE08 File Offset: 0x0000B008
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T5>.Awaiter> t5 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT5(t5.Item1, in t5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T6>.Awaiter> t6 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT6(t6.Item1, in t6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T7>.Awaiter> t7 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT7(t7.Item1, in t7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T8>.Awaiter> t8 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT8(t8.Item1, in t8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT9(this, in awaiter9);
					return;
				}
				awaiter9.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T9>.Awaiter> t9 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T9>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT9(t9.Item1, in t9.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T9>.Awaiter>(this, awaiter9));
			}

			// Token: 0x060003A9 RID: 937 RVA: 0x0000D0B4 File Offset: 0x0000B2B4
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 9)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9>(self.t8, self.t9)));
				}
			}

			// Token: 0x060003AA RID: 938 RVA: 0x0000D14C File Offset: 0x0000B34C
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 9)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9>(self.t8, self.t9)));
				}
			}

			// Token: 0x060003AB RID: 939 RVA: 0x0000D1E4 File Offset: 0x0000B3E4
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 9)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9>(self.t8, self.t9)));
				}
			}

			// Token: 0x060003AC RID: 940 RVA: 0x0000D27C File Offset: 0x0000B47C
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 9)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9>(self.t8, self.t9)));
				}
			}

			// Token: 0x060003AD RID: 941 RVA: 0x0000D314 File Offset: 0x0000B514
			private static void TryInvokeContinuationT5(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T5>.Awaiter awaiter)
			{
				try
				{
					self.t5 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 9)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9>(self.t8, self.t9)));
				}
			}

			// Token: 0x060003AE RID: 942 RVA: 0x0000D3AC File Offset: 0x0000B5AC
			private static void TryInvokeContinuationT6(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T6>.Awaiter awaiter)
			{
				try
				{
					self.t6 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 9)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9>(self.t8, self.t9)));
				}
			}

			// Token: 0x060003AF RID: 943 RVA: 0x0000D444 File Offset: 0x0000B644
			private static void TryInvokeContinuationT7(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T7>.Awaiter awaiter)
			{
				try
				{
					self.t7 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 9)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9>(self.t8, self.t9)));
				}
			}

			// Token: 0x060003B0 RID: 944 RVA: 0x0000D4DC File Offset: 0x0000B6DC
			private static void TryInvokeContinuationT8(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T8>.Awaiter awaiter)
			{
				try
				{
					self.t8 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 9)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9>(self.t8, self.t9)));
				}
			}

			// Token: 0x060003B1 RID: 945 RVA: 0x0000D574 File Offset: 0x0000B774
			private static void TryInvokeContinuationT9(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T9>.Awaiter awaiter)
			{
				try
				{
					self.t9 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 9)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9>(self.t8, self.t9)));
				}
			}

			// Token: 0x060003B2 RID: 946 RVA: 0x0000D60C File Offset: 0x0000B80C
			public ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060003B3 RID: 947 RVA: 0x0000D620 File Offset: 0x0000B820
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x060003B4 RID: 948 RVA: 0x0000D62A File Offset: 0x0000B82A
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060003B5 RID: 949 RVA: 0x0000D638 File Offset: 0x0000B838
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060003B6 RID: 950 RVA: 0x0000D645 File Offset: 0x0000B845
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x04000208 RID: 520
			private T1 t1;

			// Token: 0x04000209 RID: 521
			private T2 t2;

			// Token: 0x0400020A RID: 522
			private T3 t3;

			// Token: 0x0400020B RID: 523
			private T4 t4;

			// Token: 0x0400020C RID: 524
			private T5 t5;

			// Token: 0x0400020D RID: 525
			private T6 t6;

			// Token: 0x0400020E RID: 526
			private T7 t7;

			// Token: 0x0400020F RID: 527
			private T8 t8;

			// Token: 0x04000210 RID: 528
			private T9 t9;

			// Token: 0x04000211 RID: 529
			private int completedCount;

			// Token: 0x04000212 RID: 530
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>> core;
		}

		// Token: 0x020000AF RID: 175
		private sealed class WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IUniTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>>
		{
			// Token: 0x060003C2 RID: 962 RVA: 0x0000D8C8 File Offset: 0x0000BAC8
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T5>.Awaiter> t5 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT5(t5.Item1, in t5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T6>.Awaiter> t6 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT6(t6.Item1, in t6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T7>.Awaiter> t7 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT7(t7.Item1, in t7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T8>.Awaiter> t8 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT8(t8.Item1, in t8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T9>.Awaiter> t9 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT9(t9.Item1, in t9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT10(this, in awaiter10);
					return;
				}
				awaiter10.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T10>.Awaiter> t10 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T10>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT10(t10.Item1, in t10.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T10>.Awaiter>(this, awaiter10));
			}

			// Token: 0x060003C3 RID: 963 RVA: 0x0000DBC0 File Offset: 0x0000BDC0
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 10)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10>(self.t8, self.t9, self.t10)));
				}
			}

			// Token: 0x060003C4 RID: 964 RVA: 0x0000DC5C File Offset: 0x0000BE5C
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 10)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10>(self.t8, self.t9, self.t10)));
				}
			}

			// Token: 0x060003C5 RID: 965 RVA: 0x0000DCF8 File Offset: 0x0000BEF8
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 10)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10>(self.t8, self.t9, self.t10)));
				}
			}

			// Token: 0x060003C6 RID: 966 RVA: 0x0000DD94 File Offset: 0x0000BF94
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 10)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10>(self.t8, self.t9, self.t10)));
				}
			}

			// Token: 0x060003C7 RID: 967 RVA: 0x0000DE30 File Offset: 0x0000C030
			private static void TryInvokeContinuationT5(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T5>.Awaiter awaiter)
			{
				try
				{
					self.t5 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 10)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10>(self.t8, self.t9, self.t10)));
				}
			}

			// Token: 0x060003C8 RID: 968 RVA: 0x0000DECC File Offset: 0x0000C0CC
			private static void TryInvokeContinuationT6(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T6>.Awaiter awaiter)
			{
				try
				{
					self.t6 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 10)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10>(self.t8, self.t9, self.t10)));
				}
			}

			// Token: 0x060003C9 RID: 969 RVA: 0x0000DF68 File Offset: 0x0000C168
			private static void TryInvokeContinuationT7(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T7>.Awaiter awaiter)
			{
				try
				{
					self.t7 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 10)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10>(self.t8, self.t9, self.t10)));
				}
			}

			// Token: 0x060003CA RID: 970 RVA: 0x0000E004 File Offset: 0x0000C204
			private static void TryInvokeContinuationT8(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T8>.Awaiter awaiter)
			{
				try
				{
					self.t8 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 10)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10>(self.t8, self.t9, self.t10)));
				}
			}

			// Token: 0x060003CB RID: 971 RVA: 0x0000E0A0 File Offset: 0x0000C2A0
			private static void TryInvokeContinuationT9(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T9>.Awaiter awaiter)
			{
				try
				{
					self.t9 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 10)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10>(self.t8, self.t9, self.t10)));
				}
			}

			// Token: 0x060003CC RID: 972 RVA: 0x0000E13C File Offset: 0x0000C33C
			private static void TryInvokeContinuationT10(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T10>.Awaiter awaiter)
			{
				try
				{
					self.t10 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 10)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10>(self.t8, self.t9, self.t10)));
				}
			}

			// Token: 0x060003CD RID: 973 RVA: 0x0000E1D8 File Offset: 0x0000C3D8
			public ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060003CE RID: 974 RVA: 0x0000E1EC File Offset: 0x0000C3EC
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x060003CF RID: 975 RVA: 0x0000E1F6 File Offset: 0x0000C3F6
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060003D0 RID: 976 RVA: 0x0000E204 File Offset: 0x0000C404
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060003D1 RID: 977 RVA: 0x0000E211 File Offset: 0x0000C411
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0400021D RID: 541
			private T1 t1;

			// Token: 0x0400021E RID: 542
			private T2 t2;

			// Token: 0x0400021F RID: 543
			private T3 t3;

			// Token: 0x04000220 RID: 544
			private T4 t4;

			// Token: 0x04000221 RID: 545
			private T5 t5;

			// Token: 0x04000222 RID: 546
			private T6 t6;

			// Token: 0x04000223 RID: 547
			private T7 t7;

			// Token: 0x04000224 RID: 548
			private T8 t8;

			// Token: 0x04000225 RID: 549
			private T9 t9;

			// Token: 0x04000226 RID: 550
			private T10 t10;

			// Token: 0x04000227 RID: 551
			private int completedCount;

			// Token: 0x04000228 RID: 552
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>> core;
		}

		// Token: 0x020000B1 RID: 177
		private sealed class WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : IUniTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>>
		{
			// Token: 0x060003DE RID: 990 RVA: 0x0000E4D8 File Offset: 0x0000C6D8
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T5>.Awaiter> t5 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT5(t5.Item1, in t5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T6>.Awaiter> t6 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT6(t6.Item1, in t6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T7>.Awaiter> t7 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT7(t7.Item1, in t7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T8>.Awaiter> t8 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT8(t8.Item1, in t8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T9>.Awaiter> t9 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT9(t9.Item1, in t9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT10(this, in awaiter10);
				}
				else
				{
					awaiter10.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T10>.Awaiter> t10 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T10>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT10(t10.Item1, in t10.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T10>.Awaiter>(this, awaiter10));
				}
				UniTask<T11>.Awaiter awaiter11 = task11.GetAwaiter();
				if (awaiter11.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT11(this, in awaiter11);
					return;
				}
				awaiter11.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T11>.Awaiter> t11 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T11>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT11(t11.Item1, in t11.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T11>.Awaiter>(this, awaiter11));
			}

			// Token: 0x060003DF RID: 991 RVA: 0x0000E818 File Offset: 0x0000CA18
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 11)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11>(self.t8, self.t9, self.t10, self.t11)));
				}
			}

			// Token: 0x060003E0 RID: 992 RVA: 0x0000E8BC File Offset: 0x0000CABC
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 11)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11>(self.t8, self.t9, self.t10, self.t11)));
				}
			}

			// Token: 0x060003E1 RID: 993 RVA: 0x0000E960 File Offset: 0x0000CB60
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 11)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11>(self.t8, self.t9, self.t10, self.t11)));
				}
			}

			// Token: 0x060003E2 RID: 994 RVA: 0x0000EA04 File Offset: 0x0000CC04
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 11)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11>(self.t8, self.t9, self.t10, self.t11)));
				}
			}

			// Token: 0x060003E3 RID: 995 RVA: 0x0000EAA8 File Offset: 0x0000CCA8
			private static void TryInvokeContinuationT5(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T5>.Awaiter awaiter)
			{
				try
				{
					self.t5 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 11)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11>(self.t8, self.t9, self.t10, self.t11)));
				}
			}

			// Token: 0x060003E4 RID: 996 RVA: 0x0000EB4C File Offset: 0x0000CD4C
			private static void TryInvokeContinuationT6(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T6>.Awaiter awaiter)
			{
				try
				{
					self.t6 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 11)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11>(self.t8, self.t9, self.t10, self.t11)));
				}
			}

			// Token: 0x060003E5 RID: 997 RVA: 0x0000EBF0 File Offset: 0x0000CDF0
			private static void TryInvokeContinuationT7(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T7>.Awaiter awaiter)
			{
				try
				{
					self.t7 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 11)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11>(self.t8, self.t9, self.t10, self.t11)));
				}
			}

			// Token: 0x060003E6 RID: 998 RVA: 0x0000EC94 File Offset: 0x0000CE94
			private static void TryInvokeContinuationT8(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T8>.Awaiter awaiter)
			{
				try
				{
					self.t8 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 11)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11>(self.t8, self.t9, self.t10, self.t11)));
				}
			}

			// Token: 0x060003E7 RID: 999 RVA: 0x0000ED38 File Offset: 0x0000CF38
			private static void TryInvokeContinuationT9(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T9>.Awaiter awaiter)
			{
				try
				{
					self.t9 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 11)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11>(self.t8, self.t9, self.t10, self.t11)));
				}
			}

			// Token: 0x060003E8 RID: 1000 RVA: 0x0000EDDC File Offset: 0x0000CFDC
			private static void TryInvokeContinuationT10(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T10>.Awaiter awaiter)
			{
				try
				{
					self.t10 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 11)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11>(self.t8, self.t9, self.t10, self.t11)));
				}
			}

			// Token: 0x060003E9 RID: 1001 RVA: 0x0000EE80 File Offset: 0x0000D080
			private static void TryInvokeContinuationT11(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T11>.Awaiter awaiter)
			{
				try
				{
					self.t11 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 11)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11>(self.t8, self.t9, self.t10, self.t11)));
				}
			}

			// Token: 0x060003EA RID: 1002 RVA: 0x0000EF24 File Offset: 0x0000D124
			public ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060003EB RID: 1003 RVA: 0x0000EF38 File Offset: 0x0000D138
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x060003EC RID: 1004 RVA: 0x0000EF42 File Offset: 0x0000D142
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060003ED RID: 1005 RVA: 0x0000EF50 File Offset: 0x0000D150
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060003EE RID: 1006 RVA: 0x0000EF5D File Offset: 0x0000D15D
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x04000234 RID: 564
			private T1 t1;

			// Token: 0x04000235 RID: 565
			private T2 t2;

			// Token: 0x04000236 RID: 566
			private T3 t3;

			// Token: 0x04000237 RID: 567
			private T4 t4;

			// Token: 0x04000238 RID: 568
			private T5 t5;

			// Token: 0x04000239 RID: 569
			private T6 t6;

			// Token: 0x0400023A RID: 570
			private T7 t7;

			// Token: 0x0400023B RID: 571
			private T8 t8;

			// Token: 0x0400023C RID: 572
			private T9 t9;

			// Token: 0x0400023D RID: 573
			private T10 t10;

			// Token: 0x0400023E RID: 574
			private T11 t11;

			// Token: 0x0400023F RID: 575
			private int completedCount;

			// Token: 0x04000240 RID: 576
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>> core;
		}

		// Token: 0x020000B3 RID: 179
		private sealed class WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : IUniTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>>
		{
			// Token: 0x060003FC RID: 1020 RVA: 0x0000F268 File Offset: 0x0000D468
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T5>.Awaiter> t5 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT5(t5.Item1, in t5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T6>.Awaiter> t6 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT6(t6.Item1, in t6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T7>.Awaiter> t7 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT7(t7.Item1, in t7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T8>.Awaiter> t8 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT8(t8.Item1, in t8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T9>.Awaiter> t9 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT9(t9.Item1, in t9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT10(this, in awaiter10);
				}
				else
				{
					awaiter10.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T10>.Awaiter> t10 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T10>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT10(t10.Item1, in t10.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T10>.Awaiter>(this, awaiter10));
				}
				UniTask<T11>.Awaiter awaiter11 = task11.GetAwaiter();
				if (awaiter11.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT11(this, in awaiter11);
				}
				else
				{
					awaiter11.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T11>.Awaiter> t11 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T11>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT11(t11.Item1, in t11.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T11>.Awaiter>(this, awaiter11));
				}
				UniTask<T12>.Awaiter awaiter12 = task12.GetAwaiter();
				if (awaiter12.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT12(this, in awaiter12);
					return;
				}
				awaiter12.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T12>.Awaiter> t12 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T12>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT12(t12.Item1, in t12.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T12>.Awaiter>(this, awaiter12));
			}

			// Token: 0x060003FD RID: 1021 RVA: 0x0000F5F4 File Offset: 0x0000D7F4
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x060003FE RID: 1022 RVA: 0x0000F69C File Offset: 0x0000D89C
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x060003FF RID: 1023 RVA: 0x0000F744 File Offset: 0x0000D944
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x06000400 RID: 1024 RVA: 0x0000F7EC File Offset: 0x0000D9EC
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x06000401 RID: 1025 RVA: 0x0000F894 File Offset: 0x0000DA94
			private static void TryInvokeContinuationT5(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T5>.Awaiter awaiter)
			{
				try
				{
					self.t5 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x06000402 RID: 1026 RVA: 0x0000F93C File Offset: 0x0000DB3C
			private static void TryInvokeContinuationT6(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T6>.Awaiter awaiter)
			{
				try
				{
					self.t6 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x06000403 RID: 1027 RVA: 0x0000F9E4 File Offset: 0x0000DBE4
			private static void TryInvokeContinuationT7(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T7>.Awaiter awaiter)
			{
				try
				{
					self.t7 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x06000404 RID: 1028 RVA: 0x0000FA8C File Offset: 0x0000DC8C
			private static void TryInvokeContinuationT8(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T8>.Awaiter awaiter)
			{
				try
				{
					self.t8 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x06000405 RID: 1029 RVA: 0x0000FB34 File Offset: 0x0000DD34
			private static void TryInvokeContinuationT9(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T9>.Awaiter awaiter)
			{
				try
				{
					self.t9 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x06000406 RID: 1030 RVA: 0x0000FBDC File Offset: 0x0000DDDC
			private static void TryInvokeContinuationT10(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T10>.Awaiter awaiter)
			{
				try
				{
					self.t10 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x06000407 RID: 1031 RVA: 0x0000FC84 File Offset: 0x0000DE84
			private static void TryInvokeContinuationT11(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T11>.Awaiter awaiter)
			{
				try
				{
					self.t11 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x06000408 RID: 1032 RVA: 0x0000FD2C File Offset: 0x0000DF2C
			private static void TryInvokeContinuationT12(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T12>.Awaiter awaiter)
			{
				try
				{
					self.t12 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 12)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12>(self.t8, self.t9, self.t10, self.t11, self.t12)));
				}
			}

			// Token: 0x06000409 RID: 1033 RVA: 0x0000FDD4 File Offset: 0x0000DFD4
			public ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600040A RID: 1034 RVA: 0x0000FDE8 File Offset: 0x0000DFE8
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0600040B RID: 1035 RVA: 0x0000FDF2 File Offset: 0x0000DFF2
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600040C RID: 1036 RVA: 0x0000FE00 File Offset: 0x0000E000
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600040D RID: 1037 RVA: 0x0000FE0D File Offset: 0x0000E00D
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0400024D RID: 589
			private T1 t1;

			// Token: 0x0400024E RID: 590
			private T2 t2;

			// Token: 0x0400024F RID: 591
			private T3 t3;

			// Token: 0x04000250 RID: 592
			private T4 t4;

			// Token: 0x04000251 RID: 593
			private T5 t5;

			// Token: 0x04000252 RID: 594
			private T6 t6;

			// Token: 0x04000253 RID: 595
			private T7 t7;

			// Token: 0x04000254 RID: 596
			private T8 t8;

			// Token: 0x04000255 RID: 597
			private T9 t9;

			// Token: 0x04000256 RID: 598
			private T10 t10;

			// Token: 0x04000257 RID: 599
			private T11 t11;

			// Token: 0x04000258 RID: 600
			private T12 t12;

			// Token: 0x04000259 RID: 601
			private int completedCount;

			// Token: 0x0400025A RID: 602
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>> core;
		}

		// Token: 0x020000B5 RID: 181
		private sealed class WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : IUniTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>>
		{
			// Token: 0x0600041C RID: 1052 RVA: 0x0001015C File Offset: 0x0000E35C
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T5>.Awaiter> t5 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT5(t5.Item1, in t5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T6>.Awaiter> t6 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT6(t6.Item1, in t6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T7>.Awaiter> t7 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT7(t7.Item1, in t7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T8>.Awaiter> t8 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT8(t8.Item1, in t8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T9>.Awaiter> t9 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT9(t9.Item1, in t9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT10(this, in awaiter10);
				}
				else
				{
					awaiter10.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T10>.Awaiter> t10 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T10>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT10(t10.Item1, in t10.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T10>.Awaiter>(this, awaiter10));
				}
				UniTask<T11>.Awaiter awaiter11 = task11.GetAwaiter();
				if (awaiter11.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT11(this, in awaiter11);
				}
				else
				{
					awaiter11.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T11>.Awaiter> t11 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T11>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT11(t11.Item1, in t11.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T11>.Awaiter>(this, awaiter11));
				}
				UniTask<T12>.Awaiter awaiter12 = task12.GetAwaiter();
				if (awaiter12.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT12(this, in awaiter12);
				}
				else
				{
					awaiter12.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T12>.Awaiter> t12 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T12>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT12(t12.Item1, in t12.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T12>.Awaiter>(this, awaiter12));
				}
				UniTask<T13>.Awaiter awaiter13 = task13.GetAwaiter();
				if (awaiter13.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT13(this, in awaiter13);
					return;
				}
				awaiter13.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T13>.Awaiter> t13 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T13>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT13(t13.Item1, in t13.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T13>.Awaiter>(this, awaiter13));
			}

			// Token: 0x0600041D RID: 1053 RVA: 0x00010530 File Offset: 0x0000E730
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x0600041E RID: 1054 RVA: 0x000105E0 File Offset: 0x0000E7E0
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x0600041F RID: 1055 RVA: 0x00010690 File Offset: 0x0000E890
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x06000420 RID: 1056 RVA: 0x00010740 File Offset: 0x0000E940
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x06000421 RID: 1057 RVA: 0x000107F0 File Offset: 0x0000E9F0
			private static void TryInvokeContinuationT5(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T5>.Awaiter awaiter)
			{
				try
				{
					self.t5 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x06000422 RID: 1058 RVA: 0x000108A0 File Offset: 0x0000EAA0
			private static void TryInvokeContinuationT6(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T6>.Awaiter awaiter)
			{
				try
				{
					self.t6 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x06000423 RID: 1059 RVA: 0x00010950 File Offset: 0x0000EB50
			private static void TryInvokeContinuationT7(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T7>.Awaiter awaiter)
			{
				try
				{
					self.t7 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x06000424 RID: 1060 RVA: 0x00010A00 File Offset: 0x0000EC00
			private static void TryInvokeContinuationT8(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T8>.Awaiter awaiter)
			{
				try
				{
					self.t8 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x06000425 RID: 1061 RVA: 0x00010AB0 File Offset: 0x0000ECB0
			private static void TryInvokeContinuationT9(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T9>.Awaiter awaiter)
			{
				try
				{
					self.t9 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x06000426 RID: 1062 RVA: 0x00010B60 File Offset: 0x0000ED60
			private static void TryInvokeContinuationT10(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T10>.Awaiter awaiter)
			{
				try
				{
					self.t10 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x06000427 RID: 1063 RVA: 0x00010C10 File Offset: 0x0000EE10
			private static void TryInvokeContinuationT11(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T11>.Awaiter awaiter)
			{
				try
				{
					self.t11 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x06000428 RID: 1064 RVA: 0x00010CC0 File Offset: 0x0000EEC0
			private static void TryInvokeContinuationT12(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T12>.Awaiter awaiter)
			{
				try
				{
					self.t12 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x06000429 RID: 1065 RVA: 0x00010D70 File Offset: 0x0000EF70
			private static void TryInvokeContinuationT13(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T13>.Awaiter awaiter)
			{
				try
				{
					self.t13 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 13)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13)));
				}
			}

			// Token: 0x0600042A RID: 1066 RVA: 0x00010E20 File Offset: 0x0000F020
			public ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600042B RID: 1067 RVA: 0x00010E34 File Offset: 0x0000F034
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0600042C RID: 1068 RVA: 0x00010E3E File Offset: 0x0000F03E
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600042D RID: 1069 RVA: 0x00010E4C File Offset: 0x0000F04C
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600042E RID: 1070 RVA: 0x00010E59 File Offset: 0x0000F059
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x04000268 RID: 616
			private T1 t1;

			// Token: 0x04000269 RID: 617
			private T2 t2;

			// Token: 0x0400026A RID: 618
			private T3 t3;

			// Token: 0x0400026B RID: 619
			private T4 t4;

			// Token: 0x0400026C RID: 620
			private T5 t5;

			// Token: 0x0400026D RID: 621
			private T6 t6;

			// Token: 0x0400026E RID: 622
			private T7 t7;

			// Token: 0x0400026F RID: 623
			private T8 t8;

			// Token: 0x04000270 RID: 624
			private T9 t9;

			// Token: 0x04000271 RID: 625
			private T10 t10;

			// Token: 0x04000272 RID: 626
			private T11 t11;

			// Token: 0x04000273 RID: 627
			private T12 t12;

			// Token: 0x04000274 RID: 628
			private T13 t13;

			// Token: 0x04000275 RID: 629
			private int completedCount;

			// Token: 0x04000276 RID: 630
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>> core;
		}

		// Token: 0x020000B7 RID: 183
		private sealed class WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : IUniTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>>
		{
			// Token: 0x0600043E RID: 1086 RVA: 0x000111EC File Offset: 0x0000F3EC
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13, UniTask<T14> task14)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T5>.Awaiter> t5 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT5(t5.Item1, in t5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T6>.Awaiter> t6 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT6(t6.Item1, in t6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T7>.Awaiter> t7 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT7(t7.Item1, in t7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T8>.Awaiter> t8 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT8(t8.Item1, in t8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T9>.Awaiter> t9 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT9(t9.Item1, in t9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT10(this, in awaiter10);
				}
				else
				{
					awaiter10.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T10>.Awaiter> t10 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T10>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT10(t10.Item1, in t10.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T10>.Awaiter>(this, awaiter10));
				}
				UniTask<T11>.Awaiter awaiter11 = task11.GetAwaiter();
				if (awaiter11.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT11(this, in awaiter11);
				}
				else
				{
					awaiter11.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T11>.Awaiter> t11 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T11>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT11(t11.Item1, in t11.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T11>.Awaiter>(this, awaiter11));
				}
				UniTask<T12>.Awaiter awaiter12 = task12.GetAwaiter();
				if (awaiter12.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT12(this, in awaiter12);
				}
				else
				{
					awaiter12.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T12>.Awaiter> t12 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T12>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT12(t12.Item1, in t12.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T12>.Awaiter>(this, awaiter12));
				}
				UniTask<T13>.Awaiter awaiter13 = task13.GetAwaiter();
				if (awaiter13.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT13(this, in awaiter13);
				}
				else
				{
					awaiter13.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T13>.Awaiter> t13 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T13>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT13(t13.Item1, in t13.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T13>.Awaiter>(this, awaiter13));
				}
				UniTask<T14>.Awaiter awaiter14 = task14.GetAwaiter();
				if (awaiter14.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT14(this, in awaiter14);
					return;
				}
				awaiter14.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T14>.Awaiter> t14 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T14>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT14(t14.Item1, in t14.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T14>.Awaiter>(this, awaiter14));
			}

			// Token: 0x0600043F RID: 1087 RVA: 0x0001160C File Offset: 0x0000F80C
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x06000440 RID: 1088 RVA: 0x000116C0 File Offset: 0x0000F8C0
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x06000441 RID: 1089 RVA: 0x00011774 File Offset: 0x0000F974
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x06000442 RID: 1090 RVA: 0x00011828 File Offset: 0x0000FA28
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x06000443 RID: 1091 RVA: 0x000118DC File Offset: 0x0000FADC
			private static void TryInvokeContinuationT5(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T5>.Awaiter awaiter)
			{
				try
				{
					self.t5 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x06000444 RID: 1092 RVA: 0x00011990 File Offset: 0x0000FB90
			private static void TryInvokeContinuationT6(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T6>.Awaiter awaiter)
			{
				try
				{
					self.t6 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x06000445 RID: 1093 RVA: 0x00011A44 File Offset: 0x0000FC44
			private static void TryInvokeContinuationT7(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T7>.Awaiter awaiter)
			{
				try
				{
					self.t7 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x06000446 RID: 1094 RVA: 0x00011AF8 File Offset: 0x0000FCF8
			private static void TryInvokeContinuationT8(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T8>.Awaiter awaiter)
			{
				try
				{
					self.t8 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x06000447 RID: 1095 RVA: 0x00011BAC File Offset: 0x0000FDAC
			private static void TryInvokeContinuationT9(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T9>.Awaiter awaiter)
			{
				try
				{
					self.t9 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x06000448 RID: 1096 RVA: 0x00011C60 File Offset: 0x0000FE60
			private static void TryInvokeContinuationT10(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T10>.Awaiter awaiter)
			{
				try
				{
					self.t10 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x06000449 RID: 1097 RVA: 0x00011D14 File Offset: 0x0000FF14
			private static void TryInvokeContinuationT11(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T11>.Awaiter awaiter)
			{
				try
				{
					self.t11 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x0600044A RID: 1098 RVA: 0x00011DC8 File Offset: 0x0000FFC8
			private static void TryInvokeContinuationT12(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T12>.Awaiter awaiter)
			{
				try
				{
					self.t12 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x0600044B RID: 1099 RVA: 0x00011E7C File Offset: 0x0001007C
			private static void TryInvokeContinuationT13(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T13>.Awaiter awaiter)
			{
				try
				{
					self.t13 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x0600044C RID: 1100 RVA: 0x00011F30 File Offset: 0x00010130
			private static void TryInvokeContinuationT14(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T14>.Awaiter awaiter)
			{
				try
				{
					self.t14 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 14)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14)));
				}
			}

			// Token: 0x0600044D RID: 1101 RVA: 0x00011FE4 File Offset: 0x000101E4
			public ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600044E RID: 1102 RVA: 0x00011FF8 File Offset: 0x000101F8
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0600044F RID: 1103 RVA: 0x00012002 File Offset: 0x00010202
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000450 RID: 1104 RVA: 0x00012010 File Offset: 0x00010210
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000451 RID: 1105 RVA: 0x0001201D File Offset: 0x0001021D
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x04000285 RID: 645
			private T1 t1;

			// Token: 0x04000286 RID: 646
			private T2 t2;

			// Token: 0x04000287 RID: 647
			private T3 t3;

			// Token: 0x04000288 RID: 648
			private T4 t4;

			// Token: 0x04000289 RID: 649
			private T5 t5;

			// Token: 0x0400028A RID: 650
			private T6 t6;

			// Token: 0x0400028B RID: 651
			private T7 t7;

			// Token: 0x0400028C RID: 652
			private T8 t8;

			// Token: 0x0400028D RID: 653
			private T9 t9;

			// Token: 0x0400028E RID: 654
			private T10 t10;

			// Token: 0x0400028F RID: 655
			private T11 t11;

			// Token: 0x04000290 RID: 656
			private T12 t12;

			// Token: 0x04000291 RID: 657
			private T13 t13;

			// Token: 0x04000292 RID: 658
			private T14 t14;

			// Token: 0x04000293 RID: 659
			private int completedCount;

			// Token: 0x04000294 RID: 660
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>> core;
		}

		// Token: 0x020000B9 RID: 185
		private sealed class WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : IUniTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>>
		{
			// Token: 0x06000462 RID: 1122 RVA: 0x000123F4 File Offset: 0x000105F4
			public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13, UniTask<T14> task14, UniTask<T15> task15)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T5>.Awaiter> t5 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT5(t5.Item1, in t5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T6>.Awaiter> t6 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT6(t6.Item1, in t6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T7>.Awaiter> t7 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT7(t7.Item1, in t7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T8>.Awaiter> t8 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT8(t8.Item1, in t8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T9>.Awaiter> t9 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT9(t9.Item1, in t9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT10(this, in awaiter10);
				}
				else
				{
					awaiter10.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T10>.Awaiter> t10 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T10>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT10(t10.Item1, in t10.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T10>.Awaiter>(this, awaiter10));
				}
				UniTask<T11>.Awaiter awaiter11 = task11.GetAwaiter();
				if (awaiter11.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT11(this, in awaiter11);
				}
				else
				{
					awaiter11.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T11>.Awaiter> t11 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T11>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT11(t11.Item1, in t11.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T11>.Awaiter>(this, awaiter11));
				}
				UniTask<T12>.Awaiter awaiter12 = task12.GetAwaiter();
				if (awaiter12.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT12(this, in awaiter12);
				}
				else
				{
					awaiter12.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T12>.Awaiter> t12 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T12>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT12(t12.Item1, in t12.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T12>.Awaiter>(this, awaiter12));
				}
				UniTask<T13>.Awaiter awaiter13 = task13.GetAwaiter();
				if (awaiter13.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT13(this, in awaiter13);
				}
				else
				{
					awaiter13.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T13>.Awaiter> t13 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T13>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT13(t13.Item1, in t13.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T13>.Awaiter>(this, awaiter13));
				}
				UniTask<T14>.Awaiter awaiter14 = task14.GetAwaiter();
				if (awaiter14.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT14(this, in awaiter14);
				}
				else
				{
					awaiter14.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T14>.Awaiter> t14 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T14>.Awaiter>)state)
						{
							UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT14(t14.Item1, in t14.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T14>.Awaiter>(this, awaiter14));
				}
				UniTask<T15>.Awaiter awaiter15 = task15.GetAwaiter();
				if (awaiter15.IsCompleted)
				{
					UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT15(this, in awaiter15);
					return;
				}
				awaiter15.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T15>.Awaiter> t15 = (StateTuple<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T15>.Awaiter>)state)
					{
						UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT15(t15.Item1, in t15.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T15>.Awaiter>(this, awaiter15));
			}

			// Token: 0x06000463 RID: 1123 RVA: 0x0001285C File Offset: 0x00010A5C
			private static void TryInvokeContinuationT1(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T1>.Awaiter awaiter)
			{
				try
				{
					self.t1 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x06000464 RID: 1124 RVA: 0x00012920 File Offset: 0x00010B20
			private static void TryInvokeContinuationT2(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T2>.Awaiter awaiter)
			{
				try
				{
					self.t2 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x06000465 RID: 1125 RVA: 0x000129E4 File Offset: 0x00010BE4
			private static void TryInvokeContinuationT3(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T3>.Awaiter awaiter)
			{
				try
				{
					self.t3 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x06000466 RID: 1126 RVA: 0x00012AA8 File Offset: 0x00010CA8
			private static void TryInvokeContinuationT4(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T4>.Awaiter awaiter)
			{
				try
				{
					self.t4 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x06000467 RID: 1127 RVA: 0x00012B6C File Offset: 0x00010D6C
			private static void TryInvokeContinuationT5(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T5>.Awaiter awaiter)
			{
				try
				{
					self.t5 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x06000468 RID: 1128 RVA: 0x00012C30 File Offset: 0x00010E30
			private static void TryInvokeContinuationT6(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T6>.Awaiter awaiter)
			{
				try
				{
					self.t6 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x06000469 RID: 1129 RVA: 0x00012CF4 File Offset: 0x00010EF4
			private static void TryInvokeContinuationT7(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T7>.Awaiter awaiter)
			{
				try
				{
					self.t7 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x0600046A RID: 1130 RVA: 0x00012DB8 File Offset: 0x00010FB8
			private static void TryInvokeContinuationT8(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T8>.Awaiter awaiter)
			{
				try
				{
					self.t8 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x0600046B RID: 1131 RVA: 0x00012E7C File Offset: 0x0001107C
			private static void TryInvokeContinuationT9(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T9>.Awaiter awaiter)
			{
				try
				{
					self.t9 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x0600046C RID: 1132 RVA: 0x00012F40 File Offset: 0x00011140
			private static void TryInvokeContinuationT10(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T10>.Awaiter awaiter)
			{
				try
				{
					self.t10 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x0600046D RID: 1133 RVA: 0x00013004 File Offset: 0x00011204
			private static void TryInvokeContinuationT11(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T11>.Awaiter awaiter)
			{
				try
				{
					self.t11 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x0600046E RID: 1134 RVA: 0x000130C8 File Offset: 0x000112C8
			private static void TryInvokeContinuationT12(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T12>.Awaiter awaiter)
			{
				try
				{
					self.t12 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x0600046F RID: 1135 RVA: 0x0001318C File Offset: 0x0001138C
			private static void TryInvokeContinuationT13(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T13>.Awaiter awaiter)
			{
				try
				{
					self.t13 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x06000470 RID: 1136 RVA: 0x00013250 File Offset: 0x00011450
			private static void TryInvokeContinuationT14(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T14>.Awaiter awaiter)
			{
				try
				{
					self.t14 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x06000471 RID: 1137 RVA: 0x00013314 File Offset: 0x00011514
			private static void TryInvokeContinuationT15(UniTask.WhenAllPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T15>.Awaiter awaiter)
			{
				try
				{
					self.t15 = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 15)
				{
					self.core.TrySetResult(new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(self.t1, self.t2, self.t3, self.t4, self.t5, self.t6, self.t7, new ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(self.t8, self.t9, self.t10, self.t11, self.t12, self.t13, self.t14, new ValueTuple<T15>(self.t15))));
				}
			}

			// Token: 0x06000472 RID: 1138 RVA: 0x000133D8 File Offset: 0x000115D8
			public ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x06000473 RID: 1139 RVA: 0x000133EC File Offset: 0x000115EC
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x06000474 RID: 1140 RVA: 0x000133F6 File Offset: 0x000115F6
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000475 RID: 1141 RVA: 0x00013404 File Offset: 0x00011604
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000476 RID: 1142 RVA: 0x00013411 File Offset: 0x00011611
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x040002A4 RID: 676
			private T1 t1;

			// Token: 0x040002A5 RID: 677
			private T2 t2;

			// Token: 0x040002A6 RID: 678
			private T3 t3;

			// Token: 0x040002A7 RID: 679
			private T4 t4;

			// Token: 0x040002A8 RID: 680
			private T5 t5;

			// Token: 0x040002A9 RID: 681
			private T6 t6;

			// Token: 0x040002AA RID: 682
			private T7 t7;

			// Token: 0x040002AB RID: 683
			private T8 t8;

			// Token: 0x040002AC RID: 684
			private T9 t9;

			// Token: 0x040002AD RID: 685
			private T10 t10;

			// Token: 0x040002AE RID: 686
			private T11 t11;

			// Token: 0x040002AF RID: 687
			private T12 t12;

			// Token: 0x040002B0 RID: 688
			private T13 t13;

			// Token: 0x040002B1 RID: 689
			private T14 t14;

			// Token: 0x040002B2 RID: 690
			private T15 t15;

			// Token: 0x040002B3 RID: 691
			private int completedCount;

			// Token: 0x040002B4 RID: 692
			private UniTaskCompletionSourceCore<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>> core;
		}

		// Token: 0x020000BB RID: 187
		private sealed class WhenAllPromise<T> : IUniTaskSource<T[]>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T[]>
		{
			// Token: 0x06000488 RID: 1160 RVA: 0x0001382C File Offset: 0x00011A2C
			public WhenAllPromise(UniTask<T>[] tasks, int tasksLength)
			{
				this.completeCount = 0;
				if (tasksLength == 0)
				{
					this.result = Array.Empty<T>();
					this.core.TrySetResult(this.result);
					return;
				}
				this.result = new T[tasksLength];
				int i = 0;
				while (i < tasksLength)
				{
					UniTask<T>.Awaiter awaiter;
					try
					{
						awaiter = tasks[i].GetAwaiter();
					}
					catch (Exception ex)
					{
						this.core.TrySetException(ex);
						goto IL_00A0;
					}
					goto IL_005E;
					IL_00A0:
					i++;
					continue;
					IL_005E:
					if (awaiter.IsCompleted)
					{
						UniTask.WhenAllPromise<T>.TryInvokeContinuation(this, in awaiter, i);
						goto IL_00A0;
					}
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise<T>, UniTask<T>.Awaiter, int> t = (StateTuple<UniTask.WhenAllPromise<T>, UniTask<T>.Awaiter, int>)state)
						{
							UniTask.WhenAllPromise<T>.TryInvokeContinuation(t.Item1, in t.Item2, t.Item3);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise<T>, UniTask<T>.Awaiter, int>(this, awaiter, i));
					goto IL_00A0;
				}
			}

			// Token: 0x06000489 RID: 1161 RVA: 0x000138F4 File Offset: 0x00011AF4
			private static void TryInvokeContinuation(UniTask.WhenAllPromise<T> self, in UniTask<T>.Awaiter awaiter, int i)
			{
				try
				{
					self.result[i] = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completeCount) == self.result.Length)
				{
					self.core.TrySetResult(self.result);
				}
			}

			// Token: 0x0600048A RID: 1162 RVA: 0x0001395C File Offset: 0x00011B5C
			public T[] GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600048B RID: 1163 RVA: 0x00013970 File Offset: 0x00011B70
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0600048C RID: 1164 RVA: 0x0001397A File Offset: 0x00011B7A
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600048D RID: 1165 RVA: 0x00013988 File Offset: 0x00011B88
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600048E RID: 1166 RVA: 0x00013995 File Offset: 0x00011B95
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x040002C5 RID: 709
			private T[] result;

			// Token: 0x040002C6 RID: 710
			private int completeCount;

			// Token: 0x040002C7 RID: 711
			private UniTaskCompletionSourceCore<T[]> core;
		}

		// Token: 0x020000BD RID: 189
		private sealed class WhenAllPromise : IUniTaskSource, IValueTaskSource
		{
			// Token: 0x06000492 RID: 1170 RVA: 0x000139FC File Offset: 0x00011BFC
			public WhenAllPromise(UniTask[] tasks, int tasksLength)
			{
				this.tasksLength = tasksLength;
				this.completeCount = 0;
				if (tasksLength == 0)
				{
					this.core.TrySetResult(AsyncUnit.Default);
					return;
				}
				int i = 0;
				while (i < tasksLength)
				{
					UniTask.Awaiter awaiter;
					try
					{
						awaiter = tasks[i].GetAwaiter();
					}
					catch (Exception ex)
					{
						this.core.TrySetException(ex);
						goto IL_008D;
					}
					goto IL_004D;
					IL_008D:
					i++;
					continue;
					IL_004D:
					if (awaiter.IsCompleted)
					{
						UniTask.WhenAllPromise.TryInvokeContinuation(this, in awaiter);
						goto IL_008D;
					}
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAllPromise, UniTask.Awaiter> t = (StateTuple<UniTask.WhenAllPromise, UniTask.Awaiter>)state)
						{
							UniTask.WhenAllPromise.TryInvokeContinuation(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAllPromise, UniTask.Awaiter>(this, awaiter));
					goto IL_008D;
				}
			}

			// Token: 0x06000493 RID: 1171 RVA: 0x00013AB0 File Offset: 0x00011CB0
			private static void TryInvokeContinuation(UniTask.WhenAllPromise self, in UniTask.Awaiter awaiter)
			{
				try
				{
					awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completeCount) == self.tasksLength)
				{
					self.core.TrySetResult(AsyncUnit.Default);
				}
			}

			// Token: 0x06000494 RID: 1172 RVA: 0x00013B0C File Offset: 0x00011D0C
			public void GetResult(short token)
			{
				GC.SuppressFinalize(this);
				this.core.GetResult(token);
			}

			// Token: 0x06000495 RID: 1173 RVA: 0x00013B21 File Offset: 0x00011D21
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000496 RID: 1174 RVA: 0x00013B2F File Offset: 0x00011D2F
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000497 RID: 1175 RVA: 0x00013B3C File Offset: 0x00011D3C
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x040002CA RID: 714
			private int completeCount;

			// Token: 0x040002CB RID: 715
			private int tasksLength;

			// Token: 0x040002CC RID: 716
			private UniTaskCompletionSourceCore<AsyncUnit> core;
		}

		// Token: 0x020000BF RID: 191
		private sealed class WhenAnyPromise<T1, T2> : IUniTaskSource<ValueTuple<int, T1, T2>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2>>
		{
			// Token: 0x0600049B RID: 1179 RVA: 0x00013B9C File Offset: 0x00011D9C
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAnyPromise<T1, T2>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2>.TryInvokeContinuationT2(this, in awaiter2);
					return;
				}
				awaiter2.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2>, UniTask<T2>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2>, UniTask<T2>.Awaiter>(this, awaiter2));
			}

			// Token: 0x0600049C RID: 1180 RVA: 0x00013C48 File Offset: 0x00011E48
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2>(0, result, default(T2)));
				}
			}

			// Token: 0x0600049D RID: 1181 RVA: 0x00013CA8 File Offset: 0x00011EA8
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2>(1, default(T1), result));
				}
			}

			// Token: 0x0600049E RID: 1182 RVA: 0x00013D08 File Offset: 0x00011F08
			[return: TupleElementNames(new string[] { null, "result1", "result2" })]
			public ValueTuple<int, T1, T2> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600049F RID: 1183 RVA: 0x00013D1C File Offset: 0x00011F1C
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060004A0 RID: 1184 RVA: 0x00013D2A File Offset: 0x00011F2A
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060004A1 RID: 1185 RVA: 0x00013D3A File Offset: 0x00011F3A
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060004A2 RID: 1186 RVA: 0x00013D47 File Offset: 0x00011F47
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x040002CF RID: 719
			private int completedCount;

			// Token: 0x040002D0 RID: 720
			[TupleElementNames(new string[] { null, "result1", "result2" })]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2>> core;
		}

		// Token: 0x020000C1 RID: 193
		private sealed class WhenAnyPromise<T1, T2, T3> : IUniTaskSource<ValueTuple<int, T1, T2, T3>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3>>
		{
			// Token: 0x060004A7 RID: 1191 RVA: 0x00013DE8 File Offset: 0x00011FE8
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3>.TryInvokeContinuationT3(this, in awaiter3);
					return;
				}
				awaiter3.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3>, UniTask<T3>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3>, UniTask<T3>.Awaiter>(this, awaiter3));
			}

			// Token: 0x060004A8 RID: 1192 RVA: 0x00013EDC File Offset: 0x000120DC
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3>(0, result, default(T2), default(T3)));
				}
			}

			// Token: 0x060004A9 RID: 1193 RVA: 0x00013F48 File Offset: 0x00012148
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3>(1, default(T1), result, default(T3)));
				}
			}

			// Token: 0x060004AA RID: 1194 RVA: 0x00013FB4 File Offset: 0x000121B4
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3>(2, default(T1), default(T2), result));
				}
			}

			// Token: 0x060004AB RID: 1195 RVA: 0x00014020 File Offset: 0x00012220
			[return: TupleElementNames(new string[] { null, "result1", "result2", "result3" })]
			public ValueTuple<int, T1, T2, T3> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060004AC RID: 1196 RVA: 0x00014034 File Offset: 0x00012234
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060004AD RID: 1197 RVA: 0x00014042 File Offset: 0x00012242
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060004AE RID: 1198 RVA: 0x00014052 File Offset: 0x00012252
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060004AF RID: 1199 RVA: 0x0001405F File Offset: 0x0001225F
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x040002D4 RID: 724
			private int completedCount;

			// Token: 0x040002D5 RID: 725
			[TupleElementNames(new string[] { null, "result1", "result2", "result3" })]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3>> core;
		}

		// Token: 0x020000C3 RID: 195
		private sealed class WhenAnyPromise<T1, T2, T3, T4> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4>>
		{
			// Token: 0x060004B5 RID: 1205 RVA: 0x00014144 File Offset: 0x00012344
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4>.TryInvokeContinuationT4(this, in awaiter4);
					return;
				}
				awaiter4.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T4>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4>, UniTask<T4>.Awaiter>(this, awaiter4));
			}

			// Token: 0x060004B6 RID: 1206 RVA: 0x00014280 File Offset: 0x00012480
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4>(0, result, default(T2), default(T3), default(T4)));
				}
			}

			// Token: 0x060004B7 RID: 1207 RVA: 0x000142F4 File Offset: 0x000124F4
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4>(1, default(T1), result, default(T3), default(T4)));
				}
			}

			// Token: 0x060004B8 RID: 1208 RVA: 0x00014368 File Offset: 0x00012568
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4>(2, default(T1), default(T2), result, default(T4)));
				}
			}

			// Token: 0x060004B9 RID: 1209 RVA: 0x000143DC File Offset: 0x000125DC
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4>(3, default(T1), default(T2), default(T3), result));
				}
			}

			// Token: 0x060004BA RID: 1210 RVA: 0x00014450 File Offset: 0x00012650
			[return: TupleElementNames(new string[] { null, "result1", "result2", "result3", "result4" })]
			public ValueTuple<int, T1, T2, T3, T4> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060004BB RID: 1211 RVA: 0x00014464 File Offset: 0x00012664
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060004BC RID: 1212 RVA: 0x00014472 File Offset: 0x00012672
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060004BD RID: 1213 RVA: 0x00014482 File Offset: 0x00012682
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060004BE RID: 1214 RVA: 0x0001448F File Offset: 0x0001268F
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x040002DA RID: 730
			private int completedCount;

			// Token: 0x040002DB RID: 731
			[TupleElementNames(new string[] { null, "result1", "result2", "result3", "result4" })]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4>> core;
		}

		// Token: 0x020000C5 RID: 197
		private sealed class WhenAnyPromise<T1, T2, T3, T4, T5> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4, T5>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4, T5>>
		{
			// Token: 0x060004C5 RID: 1221 RVA: 0x000145B8 File Offset: 0x000127B8
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT5(this, in awaiter5);
					return;
				}
				awaiter5.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T5>.Awaiter> t5 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T5>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>.TryInvokeContinuationT5(t5.Item1, in t5.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T5>.Awaiter>(this, awaiter5));
			}

			// Token: 0x060004C6 RID: 1222 RVA: 0x0001473C File Offset: 0x0001293C
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5>(0, result, default(T2), default(T3), default(T4), default(T5)));
				}
			}

			// Token: 0x060004C7 RID: 1223 RVA: 0x000147BC File Offset: 0x000129BC
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5>(1, default(T1), result, default(T3), default(T4), default(T5)));
				}
			}

			// Token: 0x060004C8 RID: 1224 RVA: 0x0001483C File Offset: 0x00012A3C
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5>(2, default(T1), default(T2), result, default(T4), default(T5)));
				}
			}

			// Token: 0x060004C9 RID: 1225 RVA: 0x000148BC File Offset: 0x00012ABC
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5>(3, default(T1), default(T2), default(T3), result, default(T5)));
				}
			}

			// Token: 0x060004CA RID: 1226 RVA: 0x0001493C File Offset: 0x00012B3C
			private static void TryInvokeContinuationT5(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5> self, in UniTask<T5>.Awaiter awaiter)
			{
				T5 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5>(4, default(T1), default(T2), default(T3), default(T4), result));
				}
			}

			// Token: 0x060004CB RID: 1227 RVA: 0x000149BC File Offset: 0x00012BBC
			[return: TupleElementNames(new string[] { null, "result1", "result2", "result3", "result4", "result5" })]
			public ValueTuple<int, T1, T2, T3, T4, T5> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060004CC RID: 1228 RVA: 0x000149D0 File Offset: 0x00012BD0
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060004CD RID: 1229 RVA: 0x000149DE File Offset: 0x00012BDE
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060004CE RID: 1230 RVA: 0x000149EE File Offset: 0x00012BEE
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060004CF RID: 1231 RVA: 0x000149FB File Offset: 0x00012BFB
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x040002E1 RID: 737
			private int completedCount;

			// Token: 0x040002E2 RID: 738
			[TupleElementNames(new string[] { null, "result1", "result2", "result3", "result4", "result5" })]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4, T5>> core;
		}

		// Token: 0x020000C7 RID: 199
		private sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6>>
		{
			// Token: 0x060004D7 RID: 1239 RVA: 0x00014B68 File Offset: 0x00012D68
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T5>.Awaiter> t5 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT5(t5.Item1, in t5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT6(this, in awaiter6);
					return;
				}
				awaiter6.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T6>.Awaiter> t6 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T6>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>.TryInvokeContinuationT6(t6.Item1, in t6.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T6>.Awaiter>(this, awaiter6));
			}

			// Token: 0x060004D8 RID: 1240 RVA: 0x00014D38 File Offset: 0x00012F38
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6>(0, result, default(T2), default(T3), default(T4), default(T5), default(T6)));
				}
			}

			// Token: 0x060004D9 RID: 1241 RVA: 0x00014DC0 File Offset: 0x00012FC0
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6>(1, default(T1), result, default(T3), default(T4), default(T5), default(T6)));
				}
			}

			// Token: 0x060004DA RID: 1242 RVA: 0x00014E48 File Offset: 0x00013048
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6>(2, default(T1), default(T2), result, default(T4), default(T5), default(T6)));
				}
			}

			// Token: 0x060004DB RID: 1243 RVA: 0x00014ED0 File Offset: 0x000130D0
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6>(3, default(T1), default(T2), default(T3), result, default(T5), default(T6)));
				}
			}

			// Token: 0x060004DC RID: 1244 RVA: 0x00014F58 File Offset: 0x00013158
			private static void TryInvokeContinuationT5(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T5>.Awaiter awaiter)
			{
				T5 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6>(4, default(T1), default(T2), default(T3), default(T4), result, default(T6)));
				}
			}

			// Token: 0x060004DD RID: 1245 RVA: 0x00014FE0 File Offset: 0x000131E0
			private static void TryInvokeContinuationT6(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T6>.Awaiter awaiter)
			{
				T6 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6>(5, default(T1), default(T2), default(T3), default(T4), default(T5), result));
				}
			}

			// Token: 0x060004DE RID: 1246 RVA: 0x00015068 File Offset: 0x00013268
			[return: TupleElementNames(new string[] { null, "result1", "result2", "result3", "result4", "result5", "result6" })]
			public ValueTuple<int, T1, T2, T3, T4, T5, T6> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060004DF RID: 1247 RVA: 0x0001507C File Offset: 0x0001327C
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060004E0 RID: 1248 RVA: 0x0001508A File Offset: 0x0001328A
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060004E1 RID: 1249 RVA: 0x0001509A File Offset: 0x0001329A
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060004E2 RID: 1250 RVA: 0x000150A7 File Offset: 0x000132A7
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x040002E9 RID: 745
			private int completedCount;

			// Token: 0x040002EA RID: 746
			[TupleElementNames(new string[] { null, "result1", "result2", "result3", "result4", "result5", "result6" })]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4, T5, T6>> core;
		}

		// Token: 0x020000C9 RID: 201
		private sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>>
		{
			// Token: 0x060004EB RID: 1259 RVA: 0x00015258 File Offset: 0x00013458
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T5>.Awaiter> t5 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT5(t5.Item1, in t5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T6>.Awaiter> t6 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT6(t6.Item1, in t6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT7(this, in awaiter7);
					return;
				}
				awaiter7.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T7>.Awaiter> t7 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T7>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>.TryInvokeContinuationT7(t7.Item1, in t7.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T7>.Awaiter>(this, awaiter7));
			}

			// Token: 0x060004EC RID: 1260 RVA: 0x00015470 File Offset: 0x00013670
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>(0, result, default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7>(default(T7))));
				}
			}

			// Token: 0x060004ED RID: 1261 RVA: 0x00015508 File Offset: 0x00013708
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>(1, default(T1), result, default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7>(default(T7))));
				}
			}

			// Token: 0x060004EE RID: 1262 RVA: 0x000155A0 File Offset: 0x000137A0
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>(2, default(T1), default(T2), result, default(T4), default(T5), default(T6), new ValueTuple<T7>(default(T7))));
				}
			}

			// Token: 0x060004EF RID: 1263 RVA: 0x00015638 File Offset: 0x00013838
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>(3, default(T1), default(T2), default(T3), result, default(T5), default(T6), new ValueTuple<T7>(default(T7))));
				}
			}

			// Token: 0x060004F0 RID: 1264 RVA: 0x000156D0 File Offset: 0x000138D0
			private static void TryInvokeContinuationT5(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T5>.Awaiter awaiter)
			{
				T5 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>(4, default(T1), default(T2), default(T3), default(T4), result, default(T6), new ValueTuple<T7>(default(T7))));
				}
			}

			// Token: 0x060004F1 RID: 1265 RVA: 0x00015768 File Offset: 0x00013968
			private static void TryInvokeContinuationT6(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T6>.Awaiter awaiter)
			{
				T6 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>(5, default(T1), default(T2), default(T3), default(T4), default(T5), result, new ValueTuple<T7>(default(T7))));
				}
			}

			// Token: 0x060004F2 RID: 1266 RVA: 0x00015800 File Offset: 0x00013A00
			private static void TryInvokeContinuationT7(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T7>.Awaiter awaiter)
			{
				T7 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>(6, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7>(result)));
				}
			}

			// Token: 0x060004F3 RID: 1267 RVA: 0x00015898 File Offset: 0x00013A98
			[return: TupleElementNames(new string[] { null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", null })]
			public ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060004F4 RID: 1268 RVA: 0x000158AC File Offset: 0x00013AAC
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060004F5 RID: 1269 RVA: 0x000158BA File Offset: 0x00013ABA
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060004F6 RID: 1270 RVA: 0x000158CA File Offset: 0x00013ACA
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060004F7 RID: 1271 RVA: 0x000158D7 File Offset: 0x00013AD7
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x040002F2 RID: 754
			private int completedCount;

			// Token: 0x040002F3 RID: 755
			[TupleElementNames(new string[] { null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", null })]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7>>> core;
		}

		// Token: 0x020000CB RID: 203
		private sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>>
		{
			// Token: 0x06000501 RID: 1281 RVA: 0x00015ACC File Offset: 0x00013CCC
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T5>.Awaiter> t5 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT5(t5.Item1, in t5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T6>.Awaiter> t6 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT6(t6.Item1, in t6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T7>.Awaiter> t7 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT7(t7.Item1, in t7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT8(this, in awaiter8);
					return;
				}
				awaiter8.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T8>.Awaiter> t8 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T8>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>.TryInvokeContinuationT8(t8.Item1, in t8.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T8>.Awaiter>(this, awaiter8));
			}

			// Token: 0x06000502 RID: 1282 RVA: 0x00015D30 File Offset: 0x00013F30
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>(0, result, default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8>(default(T7), default(T8))));
				}
			}

			// Token: 0x06000503 RID: 1283 RVA: 0x00015DD0 File Offset: 0x00013FD0
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>(1, default(T1), result, default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8>(default(T7), default(T8))));
				}
			}

			// Token: 0x06000504 RID: 1284 RVA: 0x00015E70 File Offset: 0x00014070
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>(2, default(T1), default(T2), result, default(T4), default(T5), default(T6), new ValueTuple<T7, T8>(default(T7), default(T8))));
				}
			}

			// Token: 0x06000505 RID: 1285 RVA: 0x00015F10 File Offset: 0x00014110
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>(3, default(T1), default(T2), default(T3), result, default(T5), default(T6), new ValueTuple<T7, T8>(default(T7), default(T8))));
				}
			}

			// Token: 0x06000506 RID: 1286 RVA: 0x00015FB0 File Offset: 0x000141B0
			private static void TryInvokeContinuationT5(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T5>.Awaiter awaiter)
			{
				T5 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>(4, default(T1), default(T2), default(T3), default(T4), result, default(T6), new ValueTuple<T7, T8>(default(T7), default(T8))));
				}
			}

			// Token: 0x06000507 RID: 1287 RVA: 0x00016050 File Offset: 0x00014250
			private static void TryInvokeContinuationT6(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T6>.Awaiter awaiter)
			{
				T6 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>(5, default(T1), default(T2), default(T3), default(T4), default(T5), result, new ValueTuple<T7, T8>(default(T7), default(T8))));
				}
			}

			// Token: 0x06000508 RID: 1288 RVA: 0x000160F0 File Offset: 0x000142F0
			private static void TryInvokeContinuationT7(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T7>.Awaiter awaiter)
			{
				T7 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>(6, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8>(result, default(T8))));
				}
			}

			// Token: 0x06000509 RID: 1289 RVA: 0x00016190 File Offset: 0x00014390
			private static void TryInvokeContinuationT8(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T8>.Awaiter awaiter)
			{
				T8 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>(7, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8>(default(T7), result)));
				}
			}

			// Token: 0x0600050A RID: 1290 RVA: 0x00016230 File Offset: 0x00014430
			[return: TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", null,
				null
			})]
			public ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600050B RID: 1291 RVA: 0x00016244 File Offset: 0x00014444
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600050C RID: 1292 RVA: 0x00016252 File Offset: 0x00014452
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600050D RID: 1293 RVA: 0x00016262 File Offset: 0x00014462
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600050E RID: 1294 RVA: 0x0001626F File Offset: 0x0001446F
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x040002FC RID: 764
			private int completedCount;

			// Token: 0x040002FD RID: 765
			[TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", null,
				null
			})]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8>>> core;
		}

		// Token: 0x020000CD RID: 205
		private sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>>
		{
			// Token: 0x06000519 RID: 1305 RVA: 0x000164A8 File Offset: 0x000146A8
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T5>.Awaiter> t5 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT5(t5.Item1, in t5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T6>.Awaiter> t6 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT6(t6.Item1, in t6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T7>.Awaiter> t7 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT7(t7.Item1, in t7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T8>.Awaiter> t8 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT8(t8.Item1, in t8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT9(this, in awaiter9);
					return;
				}
				awaiter9.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T9>.Awaiter> t9 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T9>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>.TryInvokeContinuationT9(t9.Item1, in t9.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T9>.Awaiter>(this, awaiter9));
			}

			// Token: 0x0600051A RID: 1306 RVA: 0x00016754 File Offset: 0x00014954
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>(0, result, default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9>(default(T7), default(T8), default(T9))));
				}
			}

			// Token: 0x0600051B RID: 1307 RVA: 0x00016800 File Offset: 0x00014A00
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>(1, default(T1), result, default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9>(default(T7), default(T8), default(T9))));
				}
			}

			// Token: 0x0600051C RID: 1308 RVA: 0x000168AC File Offset: 0x00014AAC
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>(2, default(T1), default(T2), result, default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9>(default(T7), default(T8), default(T9))));
				}
			}

			// Token: 0x0600051D RID: 1309 RVA: 0x00016958 File Offset: 0x00014B58
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>(3, default(T1), default(T2), default(T3), result, default(T5), default(T6), new ValueTuple<T7, T8, T9>(default(T7), default(T8), default(T9))));
				}
			}

			// Token: 0x0600051E RID: 1310 RVA: 0x00016A04 File Offset: 0x00014C04
			private static void TryInvokeContinuationT5(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T5>.Awaiter awaiter)
			{
				T5 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>(4, default(T1), default(T2), default(T3), default(T4), result, default(T6), new ValueTuple<T7, T8, T9>(default(T7), default(T8), default(T9))));
				}
			}

			// Token: 0x0600051F RID: 1311 RVA: 0x00016AB0 File Offset: 0x00014CB0
			private static void TryInvokeContinuationT6(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T6>.Awaiter awaiter)
			{
				T6 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>(5, default(T1), default(T2), default(T3), default(T4), default(T5), result, new ValueTuple<T7, T8, T9>(default(T7), default(T8), default(T9))));
				}
			}

			// Token: 0x06000520 RID: 1312 RVA: 0x00016B5C File Offset: 0x00014D5C
			private static void TryInvokeContinuationT7(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T7>.Awaiter awaiter)
			{
				T7 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>(6, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9>(result, default(T8), default(T9))));
				}
			}

			// Token: 0x06000521 RID: 1313 RVA: 0x00016C08 File Offset: 0x00014E08
			private static void TryInvokeContinuationT8(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T8>.Awaiter awaiter)
			{
				T8 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>(7, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9>(default(T7), result, default(T9))));
				}
			}

			// Token: 0x06000522 RID: 1314 RVA: 0x00016CB4 File Offset: 0x00014EB4
			private static void TryInvokeContinuationT9(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T9>.Awaiter awaiter)
			{
				T9 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>(8, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9>(default(T7), default(T8), result)));
				}
			}

			// Token: 0x06000523 RID: 1315 RVA: 0x00016D60 File Offset: 0x00014F60
			[return: TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				null, null, null
			})]
			public ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x06000524 RID: 1316 RVA: 0x00016D74 File Offset: 0x00014F74
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000525 RID: 1317 RVA: 0x00016D82 File Offset: 0x00014F82
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000526 RID: 1318 RVA: 0x00016D92 File Offset: 0x00014F92
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000527 RID: 1319 RVA: 0x00016D9F File Offset: 0x00014F9F
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x04000307 RID: 775
			private int completedCount;

			// Token: 0x04000308 RID: 776
			[TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				null, null, null
			})]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9>>> core;
		}

		// Token: 0x020000CF RID: 207
		private sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>>
		{
			// Token: 0x06000533 RID: 1331 RVA: 0x0001701C File Offset: 0x0001521C
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T5>.Awaiter> t5 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT5(t5.Item1, in t5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T6>.Awaiter> t6 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT6(t6.Item1, in t6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T7>.Awaiter> t7 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT7(t7.Item1, in t7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T8>.Awaiter> t8 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT8(t8.Item1, in t8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T9>.Awaiter> t9 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT9(t9.Item1, in t9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT10(this, in awaiter10);
					return;
				}
				awaiter10.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T10>.Awaiter> t10 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T10>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.TryInvokeContinuationT10(t10.Item1, in t10.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T10>.Awaiter>(this, awaiter10));
			}

			// Token: 0x06000534 RID: 1332 RVA: 0x00017314 File Offset: 0x00015514
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>(0, result, default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10>(default(T7), default(T8), default(T9), default(T10))));
				}
			}

			// Token: 0x06000535 RID: 1333 RVA: 0x000173C8 File Offset: 0x000155C8
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>(1, default(T1), result, default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10>(default(T7), default(T8), default(T9), default(T10))));
				}
			}

			// Token: 0x06000536 RID: 1334 RVA: 0x0001747C File Offset: 0x0001567C
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>(2, default(T1), default(T2), result, default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10>(default(T7), default(T8), default(T9), default(T10))));
				}
			}

			// Token: 0x06000537 RID: 1335 RVA: 0x00017530 File Offset: 0x00015730
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>(3, default(T1), default(T2), default(T3), result, default(T5), default(T6), new ValueTuple<T7, T8, T9, T10>(default(T7), default(T8), default(T9), default(T10))));
				}
			}

			// Token: 0x06000538 RID: 1336 RVA: 0x000175E4 File Offset: 0x000157E4
			private static void TryInvokeContinuationT5(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T5>.Awaiter awaiter)
			{
				T5 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>(4, default(T1), default(T2), default(T3), default(T4), result, default(T6), new ValueTuple<T7, T8, T9, T10>(default(T7), default(T8), default(T9), default(T10))));
				}
			}

			// Token: 0x06000539 RID: 1337 RVA: 0x00017698 File Offset: 0x00015898
			private static void TryInvokeContinuationT6(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T6>.Awaiter awaiter)
			{
				T6 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>(5, default(T1), default(T2), default(T3), default(T4), default(T5), result, new ValueTuple<T7, T8, T9, T10>(default(T7), default(T8), default(T9), default(T10))));
				}
			}

			// Token: 0x0600053A RID: 1338 RVA: 0x0001774C File Offset: 0x0001594C
			private static void TryInvokeContinuationT7(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T7>.Awaiter awaiter)
			{
				T7 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>(6, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10>(result, default(T8), default(T9), default(T10))));
				}
			}

			// Token: 0x0600053B RID: 1339 RVA: 0x00017800 File Offset: 0x00015A00
			private static void TryInvokeContinuationT8(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T8>.Awaiter awaiter)
			{
				T8 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>(7, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10>(default(T7), result, default(T9), default(T10))));
				}
			}

			// Token: 0x0600053C RID: 1340 RVA: 0x000178B4 File Offset: 0x00015AB4
			private static void TryInvokeContinuationT9(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T9>.Awaiter awaiter)
			{
				T9 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>(8, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10>(default(T7), default(T8), result, default(T10))));
				}
			}

			// Token: 0x0600053D RID: 1341 RVA: 0x00017968 File Offset: 0x00015B68
			private static void TryInvokeContinuationT10(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T10>.Awaiter awaiter)
			{
				T10 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>(9, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10>(default(T7), default(T8), default(T9), result)));
				}
			}

			// Token: 0x0600053E RID: 1342 RVA: 0x00017A20 File Offset: 0x00015C20
			[return: TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", null, null, null, null
			})]
			public ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600053F RID: 1343 RVA: 0x00017A34 File Offset: 0x00015C34
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000540 RID: 1344 RVA: 0x00017A42 File Offset: 0x00015C42
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000541 RID: 1345 RVA: 0x00017A52 File Offset: 0x00015C52
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000542 RID: 1346 RVA: 0x00017A5F File Offset: 0x00015C5F
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x04000313 RID: 787
			private int completedCount;

			// Token: 0x04000314 RID: 788
			[TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", null, null, null, null
			})]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10>>> core;
		}

		// Token: 0x020000D1 RID: 209
		private sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>>
		{
			// Token: 0x0600054F RID: 1359 RVA: 0x00017D20 File Offset: 0x00015F20
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T5>.Awaiter> t5 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT5(t5.Item1, in t5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T6>.Awaiter> t6 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT6(t6.Item1, in t6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T7>.Awaiter> t7 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT7(t7.Item1, in t7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T8>.Awaiter> t8 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT8(t8.Item1, in t8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T9>.Awaiter> t9 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT9(t9.Item1, in t9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT10(this, in awaiter10);
				}
				else
				{
					awaiter10.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T10>.Awaiter> t10 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T10>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT10(t10.Item1, in t10.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T10>.Awaiter>(this, awaiter10));
				}
				UniTask<T11>.Awaiter awaiter11 = task11.GetAwaiter();
				if (awaiter11.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT11(this, in awaiter11);
					return;
				}
				awaiter11.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T11>.Awaiter> t11 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T11>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.TryInvokeContinuationT11(t11.Item1, in t11.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T11>.Awaiter>(this, awaiter11));
			}

			// Token: 0x06000550 RID: 1360 RVA: 0x00018060 File Offset: 0x00016260
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>(0, result, default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11>(default(T7), default(T8), default(T9), default(T10), default(T11))));
				}
			}

			// Token: 0x06000551 RID: 1361 RVA: 0x00018124 File Offset: 0x00016324
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>(1, default(T1), result, default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11>(default(T7), default(T8), default(T9), default(T10), default(T11))));
				}
			}

			// Token: 0x06000552 RID: 1362 RVA: 0x000181E8 File Offset: 0x000163E8
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>(2, default(T1), default(T2), result, default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11>(default(T7), default(T8), default(T9), default(T10), default(T11))));
				}
			}

			// Token: 0x06000553 RID: 1363 RVA: 0x000182AC File Offset: 0x000164AC
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>(3, default(T1), default(T2), default(T3), result, default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11>(default(T7), default(T8), default(T9), default(T10), default(T11))));
				}
			}

			// Token: 0x06000554 RID: 1364 RVA: 0x00018370 File Offset: 0x00016570
			private static void TryInvokeContinuationT5(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T5>.Awaiter awaiter)
			{
				T5 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>(4, default(T1), default(T2), default(T3), default(T4), result, default(T6), new ValueTuple<T7, T8, T9, T10, T11>(default(T7), default(T8), default(T9), default(T10), default(T11))));
				}
			}

			// Token: 0x06000555 RID: 1365 RVA: 0x00018434 File Offset: 0x00016634
			private static void TryInvokeContinuationT6(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T6>.Awaiter awaiter)
			{
				T6 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>(5, default(T1), default(T2), default(T3), default(T4), default(T5), result, new ValueTuple<T7, T8, T9, T10, T11>(default(T7), default(T8), default(T9), default(T10), default(T11))));
				}
			}

			// Token: 0x06000556 RID: 1366 RVA: 0x000184F8 File Offset: 0x000166F8
			private static void TryInvokeContinuationT7(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T7>.Awaiter awaiter)
			{
				T7 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>(6, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11>(result, default(T8), default(T9), default(T10), default(T11))));
				}
			}

			// Token: 0x06000557 RID: 1367 RVA: 0x000185BC File Offset: 0x000167BC
			private static void TryInvokeContinuationT8(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T8>.Awaiter awaiter)
			{
				T8 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>(7, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11>(default(T7), result, default(T9), default(T10), default(T11))));
				}
			}

			// Token: 0x06000558 RID: 1368 RVA: 0x00018680 File Offset: 0x00016880
			private static void TryInvokeContinuationT9(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T9>.Awaiter awaiter)
			{
				T9 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>(8, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11>(default(T7), default(T8), result, default(T10), default(T11))));
				}
			}

			// Token: 0x06000559 RID: 1369 RVA: 0x00018744 File Offset: 0x00016944
			private static void TryInvokeContinuationT10(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T10>.Awaiter awaiter)
			{
				T10 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>(9, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11>(default(T7), default(T8), default(T9), result, default(T11))));
				}
			}

			// Token: 0x0600055A RID: 1370 RVA: 0x00018808 File Offset: 0x00016A08
			private static void TryInvokeContinuationT11(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T11>.Awaiter awaiter)
			{
				T11 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>(10, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11>(default(T7), default(T8), default(T9), default(T10), result)));
				}
			}

			// Token: 0x0600055B RID: 1371 RVA: 0x000188CC File Offset: 0x00016ACC
			[return: TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", "result11", null, null, null, null, null
			})]
			public ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600055C RID: 1372 RVA: 0x000188E0 File Offset: 0x00016AE0
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600055D RID: 1373 RVA: 0x000188EE File Offset: 0x00016AEE
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600055E RID: 1374 RVA: 0x000188FE File Offset: 0x00016AFE
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600055F RID: 1375 RVA: 0x0001890B File Offset: 0x00016B0B
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x04000320 RID: 800
			private int completedCount;

			// Token: 0x04000321 RID: 801
			[TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", "result11", null, null, null, null, null
			})]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11>>> core;
		}

		// Token: 0x020000D3 RID: 211
		private sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>>
		{
			// Token: 0x0600056D RID: 1389 RVA: 0x00018C10 File Offset: 0x00016E10
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T5>.Awaiter> t5 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT5(t5.Item1, in t5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T6>.Awaiter> t6 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT6(t6.Item1, in t6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T7>.Awaiter> t7 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT7(t7.Item1, in t7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T8>.Awaiter> t8 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT8(t8.Item1, in t8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T9>.Awaiter> t9 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT9(t9.Item1, in t9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT10(this, in awaiter10);
				}
				else
				{
					awaiter10.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T10>.Awaiter> t10 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T10>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT10(t10.Item1, in t10.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T10>.Awaiter>(this, awaiter10));
				}
				UniTask<T11>.Awaiter awaiter11 = task11.GetAwaiter();
				if (awaiter11.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT11(this, in awaiter11);
				}
				else
				{
					awaiter11.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T11>.Awaiter> t11 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T11>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT11(t11.Item1, in t11.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T11>.Awaiter>(this, awaiter11));
				}
				UniTask<T12>.Awaiter awaiter12 = task12.GetAwaiter();
				if (awaiter12.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT12(this, in awaiter12);
					return;
				}
				awaiter12.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T12>.Awaiter> t12 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T12>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.TryInvokeContinuationT12(t12.Item1, in t12.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T12>.Awaiter>(this, awaiter12));
			}

			// Token: 0x0600056E RID: 1390 RVA: 0x00018F9C File Offset: 0x0001719C
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(0, result, default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12))));
				}
			}

			// Token: 0x0600056F RID: 1391 RVA: 0x0001906C File Offset: 0x0001726C
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(1, default(T1), result, default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12))));
				}
			}

			// Token: 0x06000570 RID: 1392 RVA: 0x0001913C File Offset: 0x0001733C
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(2, default(T1), default(T2), result, default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12))));
				}
			}

			// Token: 0x06000571 RID: 1393 RVA: 0x0001920C File Offset: 0x0001740C
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(3, default(T1), default(T2), default(T3), result, default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12))));
				}
			}

			// Token: 0x06000572 RID: 1394 RVA: 0x000192DC File Offset: 0x000174DC
			private static void TryInvokeContinuationT5(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T5>.Awaiter awaiter)
			{
				T5 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(4, default(T1), default(T2), default(T3), default(T4), result, default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12))));
				}
			}

			// Token: 0x06000573 RID: 1395 RVA: 0x000193AC File Offset: 0x000175AC
			private static void TryInvokeContinuationT6(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T6>.Awaiter awaiter)
			{
				T6 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(5, default(T1), default(T2), default(T3), default(T4), default(T5), result, new ValueTuple<T7, T8, T9, T10, T11, T12>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12))));
				}
			}

			// Token: 0x06000574 RID: 1396 RVA: 0x0001947C File Offset: 0x0001767C
			private static void TryInvokeContinuationT7(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T7>.Awaiter awaiter)
			{
				T7 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(6, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12>(result, default(T8), default(T9), default(T10), default(T11), default(T12))));
				}
			}

			// Token: 0x06000575 RID: 1397 RVA: 0x0001954C File Offset: 0x0001774C
			private static void TryInvokeContinuationT8(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T8>.Awaiter awaiter)
			{
				T8 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(7, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12>(default(T7), result, default(T9), default(T10), default(T11), default(T12))));
				}
			}

			// Token: 0x06000576 RID: 1398 RVA: 0x0001961C File Offset: 0x0001781C
			private static void TryInvokeContinuationT9(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T9>.Awaiter awaiter)
			{
				T9 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(8, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12>(default(T7), default(T8), result, default(T10), default(T11), default(T12))));
				}
			}

			// Token: 0x06000577 RID: 1399 RVA: 0x000196EC File Offset: 0x000178EC
			private static void TryInvokeContinuationT10(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T10>.Awaiter awaiter)
			{
				T10 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(9, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12>(default(T7), default(T8), default(T9), result, default(T11), default(T12))));
				}
			}

			// Token: 0x06000578 RID: 1400 RVA: 0x000197BC File Offset: 0x000179BC
			private static void TryInvokeContinuationT11(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T11>.Awaiter awaiter)
			{
				T11 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(10, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12>(default(T7), default(T8), default(T9), default(T10), result, default(T12))));
				}
			}

			// Token: 0x06000579 RID: 1401 RVA: 0x0001988C File Offset: 0x00017A8C
			private static void TryInvokeContinuationT12(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T12>.Awaiter awaiter)
			{
				T12 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>(11, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12>(default(T7), default(T8), default(T9), default(T10), default(T11), result)));
				}
			}

			// Token: 0x0600057A RID: 1402 RVA: 0x0001995C File Offset: 0x00017B5C
			[return: TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", "result11", "result12", null, null, null, null, null, null
			})]
			public ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600057B RID: 1403 RVA: 0x00019970 File Offset: 0x00017B70
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600057C RID: 1404 RVA: 0x0001997E File Offset: 0x00017B7E
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600057D RID: 1405 RVA: 0x0001998E File Offset: 0x00017B8E
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600057E RID: 1406 RVA: 0x0001999B File Offset: 0x00017B9B
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0400032E RID: 814
			private int completedCount;

			// Token: 0x0400032F RID: 815
			[TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", "result11", "result12", null, null, null, null, null, null
			})]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12>>> core;
		}

		// Token: 0x020000D5 RID: 213
		private sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>>
		{
			// Token: 0x0600058D RID: 1421 RVA: 0x00019CE4 File Offset: 0x00017EE4
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T5>.Awaiter> t5 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT5(t5.Item1, in t5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T6>.Awaiter> t6 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT6(t6.Item1, in t6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T7>.Awaiter> t7 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT7(t7.Item1, in t7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T8>.Awaiter> t8 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT8(t8.Item1, in t8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T9>.Awaiter> t9 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT9(t9.Item1, in t9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT10(this, in awaiter10);
				}
				else
				{
					awaiter10.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T10>.Awaiter> t10 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T10>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT10(t10.Item1, in t10.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T10>.Awaiter>(this, awaiter10));
				}
				UniTask<T11>.Awaiter awaiter11 = task11.GetAwaiter();
				if (awaiter11.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT11(this, in awaiter11);
				}
				else
				{
					awaiter11.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T11>.Awaiter> t11 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T11>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT11(t11.Item1, in t11.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T11>.Awaiter>(this, awaiter11));
				}
				UniTask<T12>.Awaiter awaiter12 = task12.GetAwaiter();
				if (awaiter12.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT12(this, in awaiter12);
				}
				else
				{
					awaiter12.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T12>.Awaiter> t12 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T12>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT12(t12.Item1, in t12.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T12>.Awaiter>(this, awaiter12));
				}
				UniTask<T13>.Awaiter awaiter13 = task13.GetAwaiter();
				if (awaiter13.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT13(this, in awaiter13);
					return;
				}
				awaiter13.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T13>.Awaiter> t13 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T13>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.TryInvokeContinuationT13(t13.Item1, in t13.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T13>.Awaiter>(this, awaiter13));
			}

			// Token: 0x0600058E RID: 1422 RVA: 0x0001A0B8 File Offset: 0x000182B8
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(0, result, default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13))));
				}
			}

			// Token: 0x0600058F RID: 1423 RVA: 0x0001A190 File Offset: 0x00018390
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(1, default(T1), result, default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13))));
				}
			}

			// Token: 0x06000590 RID: 1424 RVA: 0x0001A268 File Offset: 0x00018468
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(2, default(T1), default(T2), result, default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13))));
				}
			}

			// Token: 0x06000591 RID: 1425 RVA: 0x0001A340 File Offset: 0x00018540
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(3, default(T1), default(T2), default(T3), result, default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13))));
				}
			}

			// Token: 0x06000592 RID: 1426 RVA: 0x0001A418 File Offset: 0x00018618
			private static void TryInvokeContinuationT5(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T5>.Awaiter awaiter)
			{
				T5 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(4, default(T1), default(T2), default(T3), default(T4), result, default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13))));
				}
			}

			// Token: 0x06000593 RID: 1427 RVA: 0x0001A4F0 File Offset: 0x000186F0
			private static void TryInvokeContinuationT6(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T6>.Awaiter awaiter)
			{
				T6 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(5, default(T1), default(T2), default(T3), default(T4), default(T5), result, new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13))));
				}
			}

			// Token: 0x06000594 RID: 1428 RVA: 0x0001A5C8 File Offset: 0x000187C8
			private static void TryInvokeContinuationT7(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T7>.Awaiter awaiter)
			{
				T7 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(6, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(result, default(T8), default(T9), default(T10), default(T11), default(T12), default(T13))));
				}
			}

			// Token: 0x06000595 RID: 1429 RVA: 0x0001A6A0 File Offset: 0x000188A0
			private static void TryInvokeContinuationT8(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T8>.Awaiter awaiter)
			{
				T8 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(7, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), result, default(T9), default(T10), default(T11), default(T12), default(T13))));
				}
			}

			// Token: 0x06000596 RID: 1430 RVA: 0x0001A778 File Offset: 0x00018978
			private static void TryInvokeContinuationT9(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T9>.Awaiter awaiter)
			{
				T9 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(8, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), default(T8), result, default(T10), default(T11), default(T12), default(T13))));
				}
			}

			// Token: 0x06000597 RID: 1431 RVA: 0x0001A850 File Offset: 0x00018A50
			private static void TryInvokeContinuationT10(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T10>.Awaiter awaiter)
			{
				T10 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(9, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), default(T8), default(T9), result, default(T11), default(T12), default(T13))));
				}
			}

			// Token: 0x06000598 RID: 1432 RVA: 0x0001A92C File Offset: 0x00018B2C
			private static void TryInvokeContinuationT11(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T11>.Awaiter awaiter)
			{
				T11 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(10, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), default(T8), default(T9), default(T10), result, default(T12), default(T13))));
				}
			}

			// Token: 0x06000599 RID: 1433 RVA: 0x0001AA08 File Offset: 0x00018C08
			private static void TryInvokeContinuationT12(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T12>.Awaiter awaiter)
			{
				T12 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(11, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), default(T8), default(T9), default(T10), default(T11), result, default(T13))));
				}
			}

			// Token: 0x0600059A RID: 1434 RVA: 0x0001AAE4 File Offset: 0x00018CE4
			private static void TryInvokeContinuationT13(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T13>.Awaiter awaiter)
			{
				T13 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>(12, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), result)));
				}
			}

			// Token: 0x0600059B RID: 1435 RVA: 0x0001ABC0 File Offset: 0x00018DC0
			[return: TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", "result11", "result12", "result13", null, null, null, null, null, null,
				null
			})]
			public ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x0600059C RID: 1436 RVA: 0x0001ABD4 File Offset: 0x00018DD4
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600059D RID: 1437 RVA: 0x0001ABE2 File Offset: 0x00018DE2
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600059E RID: 1438 RVA: 0x0001ABF2 File Offset: 0x00018DF2
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600059F RID: 1439 RVA: 0x0001ABFF File Offset: 0x00018DFF
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0400033D RID: 829
			private int completedCount;

			// Token: 0x0400033E RID: 830
			[TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", "result11", "result12", "result13", null, null, null, null, null, null,
				null
			})]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13>>> core;
		}

		// Token: 0x020000D7 RID: 215
		private sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>>
		{
			// Token: 0x060005AF RID: 1455 RVA: 0x0001AF8C File Offset: 0x0001918C
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13, UniTask<T14> task14)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T5>.Awaiter> t5 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT5(t5.Item1, in t5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T6>.Awaiter> t6 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT6(t6.Item1, in t6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T7>.Awaiter> t7 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT7(t7.Item1, in t7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T8>.Awaiter> t8 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT8(t8.Item1, in t8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T9>.Awaiter> t9 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT9(t9.Item1, in t9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT10(this, in awaiter10);
				}
				else
				{
					awaiter10.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T10>.Awaiter> t10 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T10>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT10(t10.Item1, in t10.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T10>.Awaiter>(this, awaiter10));
				}
				UniTask<T11>.Awaiter awaiter11 = task11.GetAwaiter();
				if (awaiter11.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT11(this, in awaiter11);
				}
				else
				{
					awaiter11.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T11>.Awaiter> t11 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T11>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT11(t11.Item1, in t11.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T11>.Awaiter>(this, awaiter11));
				}
				UniTask<T12>.Awaiter awaiter12 = task12.GetAwaiter();
				if (awaiter12.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT12(this, in awaiter12);
				}
				else
				{
					awaiter12.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T12>.Awaiter> t12 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T12>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT12(t12.Item1, in t12.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T12>.Awaiter>(this, awaiter12));
				}
				UniTask<T13>.Awaiter awaiter13 = task13.GetAwaiter();
				if (awaiter13.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT13(this, in awaiter13);
				}
				else
				{
					awaiter13.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T13>.Awaiter> t13 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T13>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT13(t13.Item1, in t13.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T13>.Awaiter>(this, awaiter13));
				}
				UniTask<T14>.Awaiter awaiter14 = task14.GetAwaiter();
				if (awaiter14.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT14(this, in awaiter14);
					return;
				}
				awaiter14.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T14>.Awaiter> t14 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T14>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.TryInvokeContinuationT14(t14.Item1, in t14.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T14>.Awaiter>(this, awaiter14));
			}

			// Token: 0x060005B0 RID: 1456 RVA: 0x0001B3AC File Offset: 0x000195AC
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(0, result, default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060005B1 RID: 1457 RVA: 0x0001B494 File Offset: 0x00019694
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(1, default(T1), result, default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060005B2 RID: 1458 RVA: 0x0001B57C File Offset: 0x0001977C
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(2, default(T1), default(T2), result, default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060005B3 RID: 1459 RVA: 0x0001B664 File Offset: 0x00019864
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(3, default(T1), default(T2), default(T3), result, default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060005B4 RID: 1460 RVA: 0x0001B74C File Offset: 0x0001994C
			private static void TryInvokeContinuationT5(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T5>.Awaiter awaiter)
			{
				T5 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(4, default(T1), default(T2), default(T3), default(T4), result, default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060005B5 RID: 1461 RVA: 0x0001B834 File Offset: 0x00019A34
			private static void TryInvokeContinuationT6(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T6>.Awaiter awaiter)
			{
				T6 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(5, default(T1), default(T2), default(T3), default(T4), default(T5), result, new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060005B6 RID: 1462 RVA: 0x0001B91C File Offset: 0x00019B1C
			private static void TryInvokeContinuationT7(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T7>.Awaiter awaiter)
			{
				T7 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(6, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(result, default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060005B7 RID: 1463 RVA: 0x0001BA04 File Offset: 0x00019C04
			private static void TryInvokeContinuationT8(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T8>.Awaiter awaiter)
			{
				T8 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(7, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), result, default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060005B8 RID: 1464 RVA: 0x0001BAEC File Offset: 0x00019CEC
			private static void TryInvokeContinuationT9(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T9>.Awaiter awaiter)
			{
				T9 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(8, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), result, default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060005B9 RID: 1465 RVA: 0x0001BBD4 File Offset: 0x00019DD4
			private static void TryInvokeContinuationT10(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T10>.Awaiter awaiter)
			{
				T10 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(9, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), default(T9), result, default(T11), default(T12), default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060005BA RID: 1466 RVA: 0x0001BCBC File Offset: 0x00019EBC
			private static void TryInvokeContinuationT11(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T11>.Awaiter awaiter)
			{
				T11 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(10, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), default(T9), default(T10), result, default(T12), default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060005BB RID: 1467 RVA: 0x0001BDA4 File Offset: 0x00019FA4
			private static void TryInvokeContinuationT12(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T12>.Awaiter awaiter)
			{
				T12 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(11, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), default(T9), default(T10), default(T11), result, default(T13), new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060005BC RID: 1468 RVA: 0x0001BE8C File Offset: 0x0001A08C
			private static void TryInvokeContinuationT13(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T13>.Awaiter awaiter)
			{
				T13 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(12, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), result, new ValueTuple<T14>(default(T14)))));
				}
			}

			// Token: 0x060005BD RID: 1469 RVA: 0x0001BF74 File Offset: 0x0001A174
			private static void TryInvokeContinuationT14(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T14>.Awaiter awaiter)
			{
				T14 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>(13, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14>(result))));
				}
			}

			// Token: 0x060005BE RID: 1470 RVA: 0x0001C05C File Offset: 0x0001A25C
			[return: TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", "result11", "result12", "result13", "result14", null, null, null, null, null,
				null, null, null, null
			})]
			public ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060005BF RID: 1471 RVA: 0x0001C070 File Offset: 0x0001A270
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060005C0 RID: 1472 RVA: 0x0001C07E File Offset: 0x0001A27E
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060005C1 RID: 1473 RVA: 0x0001C08E File Offset: 0x0001A28E
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060005C2 RID: 1474 RVA: 0x0001C09B File Offset: 0x0001A29B
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0400034D RID: 845
			private int completedCount;

			// Token: 0x0400034E RID: 846
			[TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", "result11", "result12", "result13", "result14", null, null, null, null, null,
				null, null, null, null
			})]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14>>>> core;
		}

		// Token: 0x020000D9 RID: 217
		private sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : IUniTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>>
		{
			// Token: 0x060005D3 RID: 1491 RVA: 0x0001C46C File Offset: 0x0001A66C
			public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13, UniTask<T14> task14, UniTask<T15> task15)
			{
				this.completedCount = 0;
				UniTask<T1>.Awaiter awaiter = task1.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT1(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T1>.Awaiter> t = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T1>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT1(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T1>.Awaiter>(this, awaiter));
				}
				UniTask<T2>.Awaiter awaiter2 = task2.GetAwaiter();
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT2(this, in awaiter2);
				}
				else
				{
					awaiter2.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T2>.Awaiter> t2 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T2>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT2(t2.Item1, in t2.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T2>.Awaiter>(this, awaiter2));
				}
				UniTask<T3>.Awaiter awaiter3 = task3.GetAwaiter();
				if (awaiter3.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT3(this, in awaiter3);
				}
				else
				{
					awaiter3.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T3>.Awaiter> t3 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T3>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT3(t3.Item1, in t3.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T3>.Awaiter>(this, awaiter3));
				}
				UniTask<T4>.Awaiter awaiter4 = task4.GetAwaiter();
				if (awaiter4.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT4(this, in awaiter4);
				}
				else
				{
					awaiter4.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T4>.Awaiter> t4 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T4>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT4(t4.Item1, in t4.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T4>.Awaiter>(this, awaiter4));
				}
				UniTask<T5>.Awaiter awaiter5 = task5.GetAwaiter();
				if (awaiter5.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT5(this, in awaiter5);
				}
				else
				{
					awaiter5.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T5>.Awaiter> t5 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T5>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT5(t5.Item1, in t5.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T5>.Awaiter>(this, awaiter5));
				}
				UniTask<T6>.Awaiter awaiter6 = task6.GetAwaiter();
				if (awaiter6.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT6(this, in awaiter6);
				}
				else
				{
					awaiter6.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T6>.Awaiter> t6 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T6>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT6(t6.Item1, in t6.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T6>.Awaiter>(this, awaiter6));
				}
				UniTask<T7>.Awaiter awaiter7 = task7.GetAwaiter();
				if (awaiter7.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT7(this, in awaiter7);
				}
				else
				{
					awaiter7.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T7>.Awaiter> t7 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T7>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT7(t7.Item1, in t7.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T7>.Awaiter>(this, awaiter7));
				}
				UniTask<T8>.Awaiter awaiter8 = task8.GetAwaiter();
				if (awaiter8.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT8(this, in awaiter8);
				}
				else
				{
					awaiter8.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T8>.Awaiter> t8 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T8>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT8(t8.Item1, in t8.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T8>.Awaiter>(this, awaiter8));
				}
				UniTask<T9>.Awaiter awaiter9 = task9.GetAwaiter();
				if (awaiter9.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT9(this, in awaiter9);
				}
				else
				{
					awaiter9.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T9>.Awaiter> t9 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T9>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT9(t9.Item1, in t9.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T9>.Awaiter>(this, awaiter9));
				}
				UniTask<T10>.Awaiter awaiter10 = task10.GetAwaiter();
				if (awaiter10.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT10(this, in awaiter10);
				}
				else
				{
					awaiter10.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T10>.Awaiter> t10 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T10>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT10(t10.Item1, in t10.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T10>.Awaiter>(this, awaiter10));
				}
				UniTask<T11>.Awaiter awaiter11 = task11.GetAwaiter();
				if (awaiter11.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT11(this, in awaiter11);
				}
				else
				{
					awaiter11.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T11>.Awaiter> t11 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T11>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT11(t11.Item1, in t11.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T11>.Awaiter>(this, awaiter11));
				}
				UniTask<T12>.Awaiter awaiter12 = task12.GetAwaiter();
				if (awaiter12.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT12(this, in awaiter12);
				}
				else
				{
					awaiter12.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T12>.Awaiter> t12 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T12>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT12(t12.Item1, in t12.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T12>.Awaiter>(this, awaiter12));
				}
				UniTask<T13>.Awaiter awaiter13 = task13.GetAwaiter();
				if (awaiter13.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT13(this, in awaiter13);
				}
				else
				{
					awaiter13.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T13>.Awaiter> t13 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T13>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT13(t13.Item1, in t13.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T13>.Awaiter>(this, awaiter13));
				}
				UniTask<T14>.Awaiter awaiter14 = task14.GetAwaiter();
				if (awaiter14.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT14(this, in awaiter14);
				}
				else
				{
					awaiter14.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T14>.Awaiter> t14 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T14>.Awaiter>)state)
						{
							UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT14(t14.Item1, in t14.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T14>.Awaiter>(this, awaiter14));
				}
				UniTask<T15>.Awaiter awaiter15 = task15.GetAwaiter();
				if (awaiter15.IsCompleted)
				{
					UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT15(this, in awaiter15);
					return;
				}
				awaiter15.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T15>.Awaiter> t15 = (StateTuple<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T15>.Awaiter>)state)
					{
						UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.TryInvokeContinuationT15(t15.Item1, in t15.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T15>.Awaiter>(this, awaiter15));
			}

			// Token: 0x060005D4 RID: 1492 RVA: 0x0001C8D4 File Offset: 0x0001AAD4
			private static void TryInvokeContinuationT1(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T1>.Awaiter awaiter)
			{
				T1 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(0, result, default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060005D5 RID: 1493 RVA: 0x0001C9C8 File Offset: 0x0001ABC8
			private static void TryInvokeContinuationT2(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T2>.Awaiter awaiter)
			{
				T2 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(1, default(T1), result, default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060005D6 RID: 1494 RVA: 0x0001CABC File Offset: 0x0001ACBC
			private static void TryInvokeContinuationT3(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T3>.Awaiter awaiter)
			{
				T3 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(2, default(T1), default(T2), result, default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060005D7 RID: 1495 RVA: 0x0001CBB0 File Offset: 0x0001ADB0
			private static void TryInvokeContinuationT4(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T4>.Awaiter awaiter)
			{
				T4 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(3, default(T1), default(T2), default(T3), result, default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060005D8 RID: 1496 RVA: 0x0001CCA4 File Offset: 0x0001AEA4
			private static void TryInvokeContinuationT5(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T5>.Awaiter awaiter)
			{
				T5 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(4, default(T1), default(T2), default(T3), default(T4), result, default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060005D9 RID: 1497 RVA: 0x0001CD98 File Offset: 0x0001AF98
			private static void TryInvokeContinuationT6(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T6>.Awaiter awaiter)
			{
				T6 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(5, default(T1), default(T2), default(T3), default(T4), default(T5), result, new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060005DA RID: 1498 RVA: 0x0001CE8C File Offset: 0x0001B08C
			private static void TryInvokeContinuationT7(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T7>.Awaiter awaiter)
			{
				T7 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(6, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(result, default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060005DB RID: 1499 RVA: 0x0001CF80 File Offset: 0x0001B180
			private static void TryInvokeContinuationT8(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T8>.Awaiter awaiter)
			{
				T8 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(7, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), result, default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060005DC RID: 1500 RVA: 0x0001D074 File Offset: 0x0001B274
			private static void TryInvokeContinuationT9(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T9>.Awaiter awaiter)
			{
				T9 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(8, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), result, default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060005DD RID: 1501 RVA: 0x0001D168 File Offset: 0x0001B368
			private static void TryInvokeContinuationT10(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T10>.Awaiter awaiter)
			{
				T10 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(9, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), result, default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060005DE RID: 1502 RVA: 0x0001D25C File Offset: 0x0001B45C
			private static void TryInvokeContinuationT11(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T11>.Awaiter awaiter)
			{
				T11 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(10, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), default(T10), result, default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060005DF RID: 1503 RVA: 0x0001D350 File Offset: 0x0001B550
			private static void TryInvokeContinuationT12(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T12>.Awaiter awaiter)
			{
				T12 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(11, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), default(T10), default(T11), result, default(T13), new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060005E0 RID: 1504 RVA: 0x0001D444 File Offset: 0x0001B644
			private static void TryInvokeContinuationT13(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T13>.Awaiter awaiter)
			{
				T13 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(12, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), result, new ValueTuple<T14, T15>(default(T14), default(T15)))));
				}
			}

			// Token: 0x060005E1 RID: 1505 RVA: 0x0001D538 File Offset: 0x0001B738
			private static void TryInvokeContinuationT14(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T14>.Awaiter awaiter)
			{
				T14 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(13, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(result, default(T15)))));
				}
			}

			// Token: 0x060005E2 RID: 1506 RVA: 0x0001D62C File Offset: 0x0001B82C
			private static void TryInvokeContinuationT15(UniTask.WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T15>.Awaiter awaiter)
			{
				T15 result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>(14, default(T1), default(T2), default(T3), default(T4), default(T5), default(T6), new ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>(default(T7), default(T8), default(T9), default(T10), default(T11), default(T12), default(T13), new ValueTuple<T14, T15>(default(T14), result))));
				}
			}

			// Token: 0x060005E3 RID: 1507 RVA: 0x0001D720 File Offset: 0x0001B920
			[return: TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", "result11", "result12", "result13", "result14", "result15", null, null, null, null,
				null, null, null, null, null, null, null
			})]
			public ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060005E4 RID: 1508 RVA: 0x0001D734 File Offset: 0x0001B934
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060005E5 RID: 1509 RVA: 0x0001D742 File Offset: 0x0001B942
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060005E6 RID: 1510 RVA: 0x0001D752 File Offset: 0x0001B952
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060005E7 RID: 1511 RVA: 0x0001D75F File Offset: 0x0001B95F
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0400035E RID: 862
			private int completedCount;

			// Token: 0x0400035F RID: 863
			[TupleElementNames(new string[]
			{
				null, "result1", "result2", "result3", "result4", "result5", "result6", "result7", "result8", "result9",
				"result10", "result11", "result12", "result13", "result14", "result15", null, null, null, null,
				null, null, null, null, null, null, null
			})]
			private UniTaskCompletionSourceCore<ValueTuple<int, T1, T2, T3, T4, T5, T6, ValueTuple<T7, T8, T9, T10, T11, T12, T13, ValueTuple<T14, T15>>>> core;
		}

		// Token: 0x020000DB RID: 219
		private sealed class WhenAnyLRPromise<T> : IUniTaskSource<ValueTuple<bool, T>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<bool, T>>
		{
			// Token: 0x060005F9 RID: 1529 RVA: 0x0001DB74 File Offset: 0x0001BD74
			public WhenAnyLRPromise(UniTask<T> leftTask, UniTask rightTask)
			{
				UniTask<T>.Awaiter awaiter;
				try
				{
					awaiter = leftTask.GetAwaiter();
				}
				catch (Exception ex)
				{
					this.core.TrySetException(ex);
					goto IL_0060;
				}
				if (awaiter.IsCompleted)
				{
					UniTask.WhenAnyLRPromise<T>.TryLeftInvokeContinuation(this, in awaiter);
				}
				else
				{
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyLRPromise<T>, UniTask<T>.Awaiter> t = (StateTuple<UniTask.WhenAnyLRPromise<T>, UniTask<T>.Awaiter>)state)
						{
							UniTask.WhenAnyLRPromise<T>.TryLeftInvokeContinuation(t.Item1, in t.Item2);
						}
					}, StateTuple.Create<UniTask.WhenAnyLRPromise<T>, UniTask<T>.Awaiter>(this, awaiter));
				}
				IL_0060:
				UniTask.Awaiter awaiter2;
				try
				{
					awaiter2 = rightTask.GetAwaiter();
				}
				catch (Exception ex2)
				{
					this.core.TrySetException(ex2);
					return;
				}
				if (awaiter2.IsCompleted)
				{
					UniTask.WhenAnyLRPromise<T>.TryRightInvokeContinuation(this, in awaiter2);
					return;
				}
				awaiter2.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<UniTask.WhenAnyLRPromise<T>, UniTask.Awaiter> t2 = (StateTuple<UniTask.WhenAnyLRPromise<T>, UniTask.Awaiter>)state)
					{
						UniTask.WhenAnyLRPromise<T>.TryRightInvokeContinuation(t2.Item1, in t2.Item2);
					}
				}, StateTuple.Create<UniTask.WhenAnyLRPromise<T>, UniTask.Awaiter>(this, awaiter2));
			}

			// Token: 0x060005FA RID: 1530 RVA: 0x0001DC58 File Offset: 0x0001BE58
			private static void TryLeftInvokeContinuation(UniTask.WhenAnyLRPromise<T> self, in UniTask<T>.Awaiter awaiter)
			{
				T result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<bool, T>(true, result));
				}
			}

			// Token: 0x060005FB RID: 1531 RVA: 0x0001DCB0 File Offset: 0x0001BEB0
			private static void TryRightInvokeContinuation(UniTask.WhenAnyLRPromise<T> self, in UniTask.Awaiter awaiter)
			{
				try
				{
					awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<bool, T>(false, default(T)));
				}
			}

			// Token: 0x060005FC RID: 1532 RVA: 0x0001DD10 File Offset: 0x0001BF10
			public ValueTuple<bool, T> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x060005FD RID: 1533 RVA: 0x0001DD24 File Offset: 0x0001BF24
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060005FE RID: 1534 RVA: 0x0001DD32 File Offset: 0x0001BF32
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060005FF RID: 1535 RVA: 0x0001DD42 File Offset: 0x0001BF42
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000600 RID: 1536 RVA: 0x0001DD4F File Offset: 0x0001BF4F
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x04000370 RID: 880
			private int completedCount;

			// Token: 0x04000371 RID: 881
			private UniTaskCompletionSourceCore<ValueTuple<bool, T>> core;
		}

		// Token: 0x020000DD RID: 221
		private sealed class WhenAnyPromise<T> : IUniTaskSource<ValueTuple<int, T>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<int, T>>
		{
			// Token: 0x06000605 RID: 1541 RVA: 0x0001DDF0 File Offset: 0x0001BFF0
			public WhenAnyPromise(UniTask<T>[] tasks, int tasksLength)
			{
				if (tasksLength == 0)
				{
					throw new ArgumentException("The tasks argument contains no tasks.");
				}
				int i = 0;
				while (i < tasksLength)
				{
					UniTask<T>.Awaiter awaiter;
					try
					{
						awaiter = tasks[i].GetAwaiter();
					}
					catch (Exception ex)
					{
						this.core.TrySetException(ex);
						goto IL_007A;
					}
					goto IL_0038;
					IL_007A:
					i++;
					continue;
					IL_0038:
					if (awaiter.IsCompleted)
					{
						UniTask.WhenAnyPromise<T>.TryInvokeContinuation(this, in awaiter, i);
						goto IL_007A;
					}
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise<T>, UniTask<T>.Awaiter, int> t = (StateTuple<UniTask.WhenAnyPromise<T>, UniTask<T>.Awaiter, int>)state)
						{
							UniTask.WhenAnyPromise<T>.TryInvokeContinuation(t.Item1, in t.Item2, t.Item3);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise<T>, UniTask<T>.Awaiter, int>(this, awaiter, i));
					goto IL_007A;
				}
			}

			// Token: 0x06000606 RID: 1542 RVA: 0x0001DE90 File Offset: 0x0001C090
			private static void TryInvokeContinuation(UniTask.WhenAnyPromise<T> self, in UniTask<T>.Awaiter awaiter, int i)
			{
				T result;
				try
				{
					result = awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(new ValueTuple<int, T>(i, result));
				}
			}

			// Token: 0x06000607 RID: 1543 RVA: 0x0001DEE8 File Offset: 0x0001C0E8
			public ValueTuple<int, T> GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x06000608 RID: 1544 RVA: 0x0001DEFC File Offset: 0x0001C0FC
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000609 RID: 1545 RVA: 0x0001DF0A File Offset: 0x0001C10A
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600060A RID: 1546 RVA: 0x0001DF1A File Offset: 0x0001C11A
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600060B RID: 1547 RVA: 0x0001DF27 File Offset: 0x0001C127
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x04000375 RID: 885
			private int completedCount;

			// Token: 0x04000376 RID: 886
			private UniTaskCompletionSourceCore<ValueTuple<int, T>> core;
		}

		// Token: 0x020000DF RID: 223
		private sealed class WhenAnyPromise : IUniTaskSource<int>, IUniTaskSource, IValueTaskSource, IValueTaskSource<int>
		{
			// Token: 0x0600060F RID: 1551 RVA: 0x0001DF88 File Offset: 0x0001C188
			public WhenAnyPromise(UniTask[] tasks, int tasksLength)
			{
				if (tasksLength == 0)
				{
					throw new ArgumentException("The tasks argument contains no tasks.");
				}
				int i = 0;
				while (i < tasksLength)
				{
					UniTask.Awaiter awaiter;
					try
					{
						awaiter = tasks[i].GetAwaiter();
					}
					catch (Exception ex)
					{
						this.core.TrySetException(ex);
						goto IL_007A;
					}
					goto IL_0038;
					IL_007A:
					i++;
					continue;
					IL_0038:
					if (awaiter.IsCompleted)
					{
						UniTask.WhenAnyPromise.TryInvokeContinuation(this, in awaiter, i);
						goto IL_007A;
					}
					awaiter.SourceOnCompleted(delegate(object state)
					{
						using (StateTuple<UniTask.WhenAnyPromise, UniTask.Awaiter, int> t = (StateTuple<UniTask.WhenAnyPromise, UniTask.Awaiter, int>)state)
						{
							UniTask.WhenAnyPromise.TryInvokeContinuation(t.Item1, in t.Item2, t.Item3);
						}
					}, StateTuple.Create<UniTask.WhenAnyPromise, UniTask.Awaiter, int>(this, awaiter, i));
					goto IL_007A;
				}
			}

			// Token: 0x06000610 RID: 1552 RVA: 0x0001E028 File Offset: 0x0001C228
			private static void TryInvokeContinuation(UniTask.WhenAnyPromise self, in UniTask.Awaiter awaiter, int i)
			{
				try
				{
					awaiter.GetResult();
				}
				catch (Exception ex)
				{
					self.core.TrySetException(ex);
					return;
				}
				if (Interlocked.Increment(ref self.completedCount) == 1)
				{
					self.core.TrySetResult(i);
				}
			}

			// Token: 0x06000611 RID: 1553 RVA: 0x0001E078 File Offset: 0x0001C278
			public int GetResult(short token)
			{
				GC.SuppressFinalize(this);
				return this.core.GetResult(token);
			}

			// Token: 0x06000612 RID: 1554 RVA: 0x0001E08C File Offset: 0x0001C28C
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000613 RID: 1555 RVA: 0x0001E09A File Offset: 0x0001C29A
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000614 RID: 1556 RVA: 0x0001E0AA File Offset: 0x0001C2AA
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000615 RID: 1557 RVA: 0x0001E0B7 File Offset: 0x0001C2B7
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x04000379 RID: 889
			private int completedCount;

			// Token: 0x0400037A RID: 890
			private UniTaskCompletionSourceCore<int> core;
		}

		// Token: 0x020000E1 RID: 225
		private sealed class AsyncUnitSource : IUniTaskSource<AsyncUnit>, IUniTaskSource, IValueTaskSource, IValueTaskSource<AsyncUnit>
		{
			// Token: 0x06000619 RID: 1561 RVA: 0x0001E118 File Offset: 0x0001C318
			public AsyncUnitSource(IUniTaskSource source)
			{
				this.source = source;
			}

			// Token: 0x0600061A RID: 1562 RVA: 0x0001E127 File Offset: 0x0001C327
			public AsyncUnit GetResult(short token)
			{
				this.source.GetResult(token);
				return AsyncUnit.Default;
			}

			// Token: 0x0600061B RID: 1563 RVA: 0x0001E13A File Offset: 0x0001C33A
			public UniTaskStatus GetStatus(short token)
			{
				return this.source.GetStatus(token);
			}

			// Token: 0x0600061C RID: 1564 RVA: 0x0001E148 File Offset: 0x0001C348
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.source.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600061D RID: 1565 RVA: 0x0001E158 File Offset: 0x0001C358
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.source.UnsafeGetStatus();
			}

			// Token: 0x0600061E RID: 1566 RVA: 0x0001E165 File Offset: 0x0001C365
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0400037D RID: 893
			private readonly IUniTaskSource source;
		}

		// Token: 0x020000E2 RID: 226
		private sealed class IsCanceledSource : IUniTaskSource<bool>, IUniTaskSource, IValueTaskSource, IValueTaskSource<bool>
		{
			// Token: 0x0600061F RID: 1567 RVA: 0x0001E16F File Offset: 0x0001C36F
			public IsCanceledSource(IUniTaskSource source)
			{
				this.source = source;
			}

			// Token: 0x06000620 RID: 1568 RVA: 0x0001E17E File Offset: 0x0001C37E
			public bool GetResult(short token)
			{
				if (this.source.GetStatus(token) == UniTaskStatus.Canceled)
				{
					return true;
				}
				this.source.GetResult(token);
				return false;
			}

			// Token: 0x06000621 RID: 1569 RVA: 0x0001E19E File Offset: 0x0001C39E
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x06000622 RID: 1570 RVA: 0x0001E1A8 File Offset: 0x0001C3A8
			public UniTaskStatus GetStatus(short token)
			{
				return this.source.GetStatus(token);
			}

			// Token: 0x06000623 RID: 1571 RVA: 0x0001E1B6 File Offset: 0x0001C3B6
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.source.UnsafeGetStatus();
			}

			// Token: 0x06000624 RID: 1572 RVA: 0x0001E1C3 File Offset: 0x0001C3C3
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.source.OnCompleted(continuation, state, token);
			}

			// Token: 0x0400037E RID: 894
			private readonly IUniTaskSource source;
		}

		// Token: 0x020000E3 RID: 227
		private sealed class MemoizeSource : IUniTaskSource, IValueTaskSource
		{
			// Token: 0x06000625 RID: 1573 RVA: 0x0001E1D3 File Offset: 0x0001C3D3
			public MemoizeSource(IUniTaskSource source)
			{
				this.source = source;
			}

			// Token: 0x06000626 RID: 1574 RVA: 0x0001E1E4 File Offset: 0x0001C3E4
			public void GetResult(short token)
			{
				if (this.source == null)
				{
					if (this.exception != null)
					{
						this.exception.Throw();
						return;
					}
				}
				else
				{
					try
					{
						this.source.GetResult(token);
						this.status = UniTaskStatus.Succeeded;
					}
					catch (Exception ex)
					{
						this.exception = ExceptionDispatchInfo.Capture(ex);
						if (ex is OperationCanceledException)
						{
							this.status = UniTaskStatus.Canceled;
						}
						else
						{
							this.status = UniTaskStatus.Faulted;
						}
						throw;
					}
					finally
					{
						this.source = null;
					}
				}
			}

			// Token: 0x06000627 RID: 1575 RVA: 0x0001E270 File Offset: 0x0001C470
			public UniTaskStatus GetStatus(short token)
			{
				if (this.source == null)
				{
					return this.status;
				}
				return this.source.GetStatus(token);
			}

			// Token: 0x06000628 RID: 1576 RVA: 0x0001E28D File Offset: 0x0001C48D
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				if (this.source == null)
				{
					continuation(state);
					return;
				}
				this.source.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000629 RID: 1577 RVA: 0x0001E2AD File Offset: 0x0001C4AD
			public UniTaskStatus UnsafeGetStatus()
			{
				if (this.source == null)
				{
					return this.status;
				}
				return this.source.UnsafeGetStatus();
			}

			// Token: 0x0400037F RID: 895
			private IUniTaskSource source;

			// Token: 0x04000380 RID: 896
			private ExceptionDispatchInfo exception;

			// Token: 0x04000381 RID: 897
			private UniTaskStatus status;
		}

		// Token: 0x020000E4 RID: 228
		public readonly struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x0600062A RID: 1578 RVA: 0x0001E2C9 File Offset: 0x0001C4C9
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public Awaiter(in UniTask task)
			{
				this.task = task;
			}

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x0600062B RID: 1579 RVA: 0x0001E2D7 File Offset: 0x0001C4D7
			public bool IsCompleted
			{
				[DebuggerHidden]
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.task.Status.IsCompleted();
				}
			}

			// Token: 0x0600062C RID: 1580 RVA: 0x0001E2E9 File Offset: 0x0001C4E9
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void GetResult()
			{
				if (this.task.source == null)
				{
					return;
				}
				this.task.source.GetResult(this.task.token);
			}

			// Token: 0x0600062D RID: 1581 RVA: 0x0001E314 File Offset: 0x0001C514
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void OnCompleted(Action continuation)
			{
				if (this.task.source == null)
				{
					continuation();
					return;
				}
				this.task.source.OnCompleted(AwaiterActions.InvokeContinuationDelegate, continuation, this.task.token);
			}

			// Token: 0x0600062E RID: 1582 RVA: 0x0001E314 File Offset: 0x0001C514
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void UnsafeOnCompleted(Action continuation)
			{
				if (this.task.source == null)
				{
					continuation();
					return;
				}
				this.task.source.OnCompleted(AwaiterActions.InvokeContinuationDelegate, continuation, this.task.token);
			}

			// Token: 0x0600062F RID: 1583 RVA: 0x0001E34B File Offset: 0x0001C54B
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void SourceOnCompleted(Action<object> continuation, object state)
			{
				if (this.task.source == null)
				{
					continuation(state);
					return;
				}
				this.task.source.OnCompleted(continuation, state, this.task.token);
			}

			// Token: 0x04000382 RID: 898
			private readonly UniTask task;
		}
	}
}
