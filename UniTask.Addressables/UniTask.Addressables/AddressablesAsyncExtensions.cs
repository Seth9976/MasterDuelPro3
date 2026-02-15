using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks.Sources;
using Cysharp.Threading.Tasks.Internal;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000004 RID: 4
	public static class AddressablesAsyncExtensions
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BC File Offset: 0x000002BC
		public static UniTask.Awaiter GetAwaiter(this AsyncOperationHandle handle)
		{
			return handle.ToUniTask(null, PlayerLoopTiming.Update, default(CancellationToken), false, false).GetAwaiter();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E4 File Offset: 0x000002E4
		public static UniTask WithCancellation(this AsyncOperationHandle handle, CancellationToken cancellationToken, bool cancelImmediately = false, bool autoReleaseWhenCanceled = false)
		{
			return handle.ToUniTask(null, PlayerLoopTiming.Update, cancellationToken, cancelImmediately, autoReleaseWhenCanceled);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020F4 File Offset: 0x000002F4
		public static UniTask ToUniTask(this AsyncOperationHandle handle, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false, bool autoReleaseWhenCanceled = false)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return UniTask.FromCanceled(cancellationToken);
			}
			if (!handle.IsValid())
			{
				return UniTask.CompletedTask;
			}
			if (!handle.IsDone)
			{
				short token;
				return new UniTask(AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource.Create(handle, timing, progress, cancellationToken, cancelImmediately, autoReleaseWhenCanceled, out token), token);
			}
			if (handle.Status == AsyncOperationStatus.Failed)
			{
				return UniTask.FromException(handle.OperationException);
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000215C File Offset: 0x0000035C
		public static UniTask<T>.Awaiter GetAwaiter<T>(this AsyncOperationHandle<T> handle)
		{
			return handle.ToUniTask(null, PlayerLoopTiming.Update, default(CancellationToken), false, false).GetAwaiter();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002184 File Offset: 0x00000384
		public static UniTask<T> WithCancellation<T>(this AsyncOperationHandle<T> handle, CancellationToken cancellationToken, bool cancelImmediately = false, bool autoReleaseWhenCanceled = false)
		{
			return handle.ToUniTask(null, PlayerLoopTiming.Update, cancellationToken, cancelImmediately, autoReleaseWhenCanceled);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002194 File Offset: 0x00000394
		public static UniTask<T> ToUniTask<T>(this AsyncOperationHandle<T> handle, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false, bool autoReleaseWhenCanceled = false)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return UniTask.FromCanceled<T>(cancellationToken);
			}
			if (!handle.IsValid())
			{
				throw new Exception("Attempting to use an invalid operation handle");
			}
			if (!handle.IsDone)
			{
				short token;
				return new UniTask<T>(AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource<T>.Create(handle, timing, progress, cancellationToken, cancelImmediately, autoReleaseWhenCanceled, out token), token);
			}
			if (handle.Status == AsyncOperationStatus.Failed)
			{
				return UniTask.FromException<T>(handle.OperationException);
			}
			return UniTask.FromResult<T>(handle.Result);
		}

		// Token: 0x02000005 RID: 5
		public struct AsyncOperationHandleAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06000009 RID: 9 RVA: 0x00002207 File Offset: 0x00000407
			public AsyncOperationHandleAwaiter(AsyncOperationHandle handle)
			{
				this.handle = handle;
				this.continuationAction = null;
			}

			// Token: 0x17000001 RID: 1
			// (get) Token: 0x0600000A RID: 10 RVA: 0x00002217 File Offset: 0x00000417
			public bool IsCompleted
			{
				get
				{
					return this.handle.IsDone;
				}
			}

			// Token: 0x0600000B RID: 11 RVA: 0x00002224 File Offset: 0x00000424
			public void GetResult()
			{
				if (this.continuationAction != null)
				{
					this.handle.Completed -= this.continuationAction;
					this.continuationAction = null;
				}
				if (this.handle.Status == AsyncOperationStatus.Failed)
				{
					Exception operationException = this.handle.OperationException;
					this.handle = default(AsyncOperationHandle);
					ExceptionDispatchInfo.Capture(operationException).Throw();
				}
				object result = this.handle.Result;
				this.handle = default(AsyncOperationHandle);
			}

			// Token: 0x0600000C RID: 12 RVA: 0x00002298 File Offset: 0x00000498
			public void OnCompleted(Action continuation)
			{
				this.UnsafeOnCompleted(continuation);
			}

			// Token: 0x0600000D RID: 13 RVA: 0x000022A1 File Offset: 0x000004A1
			public void UnsafeOnCompleted(Action continuation)
			{
				Error.ThrowWhenContinuationIsAlreadyRegistered<Action<AsyncOperationHandle>>(this.continuationAction);
				this.continuationAction = PooledDelegate<AsyncOperationHandle>.Create(continuation);
				this.handle.Completed += this.continuationAction;
			}

			// Token: 0x04000006 RID: 6
			private AsyncOperationHandle handle;

			// Token: 0x04000007 RID: 7
			private Action<AsyncOperationHandle> continuationAction;
		}

		// Token: 0x02000006 RID: 6
		private sealed class AsyncOperationHandleConfiguredSource : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource>
		{
			// Token: 0x17000002 RID: 2
			// (get) Token: 0x0600000E RID: 14 RVA: 0x000022CB File Offset: 0x000004CB
			public ref AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x0600000F RID: 15 RVA: 0x000022D3 File Offset: 0x000004D3
			static AsyncOperationHandleConfiguredSource()
			{
				TaskPool.RegisterSizeGetter(typeof(AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource), () => AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource.pool.Size);
			}

			// Token: 0x06000010 RID: 16 RVA: 0x000022F4 File Offset: 0x000004F4
			private AsyncOperationHandleConfiguredSource()
			{
				this.completedCallback = new Action<AsyncOperationHandle>(this.HandleCompleted);
			}

			// Token: 0x06000011 RID: 17 RVA: 0x00002310 File Offset: 0x00000510
			public static IUniTaskSource Create(AsyncOperationHandle handle, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, bool cancelImmediately, bool autoReleaseWhenCanceled, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource result;
				if (!AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource.pool.TryPop(out result))
				{
					result = new AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource();
				}
				result.handle = handle;
				result.progress = progress;
				result.cancellationToken = cancellationToken;
				result.cancelImmediately = cancelImmediately;
				result.autoReleaseWhenCanceled = autoReleaseWhenCanceled;
				result.completed = false;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource promise = (AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource)state;
						if (promise.autoReleaseWhenCanceled && promise.handle.IsValid())
						{
							Addressables.Release(promise.handle);
						}
						promise.core.TrySetCanceled(promise.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				handle.Completed += result.completedCallback;
				token = result.core.Version;
				return result;
			}

			// Token: 0x06000012 RID: 18 RVA: 0x000023CC File Offset: 0x000005CC
			private void HandleCompleted(AsyncOperationHandle _)
			{
				if (this.handle.IsValid())
				{
					this.handle.Completed -= this.completedCallback;
				}
				if (this.completed)
				{
					return;
				}
				this.completed = true;
				if (this.cancellationToken.IsCancellationRequested)
				{
					if (this.autoReleaseWhenCanceled && this.handle.IsValid())
					{
						Addressables.Release(this.handle);
					}
					this.core.TrySetCanceled(this.cancellationToken);
					return;
				}
				if (this.handle.Status == AsyncOperationStatus.Failed)
				{
					this.core.TrySetException(this.handle.OperationException);
					return;
				}
				this.core.TrySetResult(AsyncUnit.Default);
			}

			// Token: 0x06000013 RID: 19 RVA: 0x00002480 File Offset: 0x00000680
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

			// Token: 0x06000014 RID: 20 RVA: 0x000024CC File Offset: 0x000006CC
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000015 RID: 21 RVA: 0x000024DA File Offset: 0x000006DA
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000016 RID: 22 RVA: 0x000024E7 File Offset: 0x000006E7
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000017 RID: 23 RVA: 0x000024F8 File Offset: 0x000006F8
			public bool MoveNext()
			{
				if (this.completed)
				{
					return false;
				}
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.completed = true;
					if (this.autoReleaseWhenCanceled && this.handle.IsValid())
					{
						Addressables.Release(this.handle);
					}
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.progress != null && this.handle.IsValid())
				{
					this.progress.Report(this.handle.GetDownloadStatus().Percent);
				}
				return true;
			}

			// Token: 0x06000018 RID: 24 RVA: 0x0000258C File Offset: 0x0000078C
			private bool TryReturn()
			{
				this.core.Reset();
				this.handle = default(AsyncOperationHandle);
				this.progress = null;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				return AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource.pool.TryPush(this);
			}

			// Token: 0x04000008 RID: 8
			private static TaskPool<AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource> pool;

			// Token: 0x04000009 RID: 9
			private AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource nextNode;

			// Token: 0x0400000A RID: 10
			private readonly Action<AsyncOperationHandle> completedCallback;

			// Token: 0x0400000B RID: 11
			private AsyncOperationHandle handle;

			// Token: 0x0400000C RID: 12
			private CancellationToken cancellationToken;

			// Token: 0x0400000D RID: 13
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x0400000E RID: 14
			private IProgress<float> progress;

			// Token: 0x0400000F RID: 15
			private bool autoReleaseWhenCanceled;

			// Token: 0x04000010 RID: 16
			private bool cancelImmediately;

			// Token: 0x04000011 RID: 17
			private bool completed;

			// Token: 0x04000012 RID: 18
			private UniTaskCompletionSourceCore<AsyncUnit> core;
		}

		// Token: 0x02000008 RID: 8
		private sealed class AsyncOperationHandleConfiguredSource<T> : IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>, IPlayerLoopItem, ITaskPoolNode<AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource<T>>
		{
			// Token: 0x17000003 RID: 3
			// (get) Token: 0x0600001D RID: 29 RVA: 0x0000263A File Offset: 0x0000083A
			public ref AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource<T> NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x0600001E RID: 30 RVA: 0x00002642 File Offset: 0x00000842
			static AsyncOperationHandleConfiguredSource()
			{
				TaskPool.RegisterSizeGetter(typeof(AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource<T>), () => AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource<T>.pool.Size);
			}

			// Token: 0x0600001F RID: 31 RVA: 0x00002663 File Offset: 0x00000863
			private AsyncOperationHandleConfiguredSource()
			{
				this.completedCallback = new Action<AsyncOperationHandle<T>>(this.HandleCompleted);
			}

			// Token: 0x06000020 RID: 32 RVA: 0x00002680 File Offset: 0x00000880
			public static IUniTaskSource<T> Create(AsyncOperationHandle<T> handle, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, bool cancelImmediately, bool autoReleaseWhenCanceled, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource<T>.CreateFromCanceled(cancellationToken, out token);
				}
				AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource<T> result;
				if (!AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource<T>.pool.TryPop(out result))
				{
					result = new AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource<T>();
				}
				result.handle = handle;
				result.cancellationToken = cancellationToken;
				result.completed = false;
				result.progress = progress;
				result.autoReleaseWhenCanceled = autoReleaseWhenCanceled;
				result.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource<T> promise = (AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource<T>)state;
						if (promise.autoReleaseWhenCanceled && promise.handle.IsValid())
						{
							Addressables.Release<T>(promise.handle);
						}
						promise.core.TrySetCanceled(promise.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				handle.Completed += result.completedCallback;
				token = result.core.Version;
				return result;
			}

			// Token: 0x06000021 RID: 33 RVA: 0x0000273C File Offset: 0x0000093C
			private void HandleCompleted(AsyncOperationHandle<T> argHandle)
			{
				if (this.handle.IsValid())
				{
					this.handle.Completed -= this.completedCallback;
				}
				if (this.completed)
				{
					return;
				}
				this.completed = true;
				if (this.cancellationToken.IsCancellationRequested)
				{
					if (this.autoReleaseWhenCanceled && this.handle.IsValid())
					{
						Addressables.Release<T>(this.handle);
					}
					this.core.TrySetCanceled(this.cancellationToken);
					return;
				}
				if (argHandle.Status == AsyncOperationStatus.Failed)
				{
					this.core.TrySetException(argHandle.OperationException);
					return;
				}
				this.core.TrySetResult(argHandle.Result);
			}

			// Token: 0x06000022 RID: 34 RVA: 0x000027E8 File Offset: 0x000009E8
			public T GetResult(short token)
			{
				T result;
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

			// Token: 0x06000023 RID: 35 RVA: 0x00002834 File Offset: 0x00000A34
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x06000024 RID: 36 RVA: 0x0000283E File Offset: 0x00000A3E
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000025 RID: 37 RVA: 0x0000284C File Offset: 0x00000A4C
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000026 RID: 38 RVA: 0x00002859 File Offset: 0x00000A59
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000027 RID: 39 RVA: 0x0000286C File Offset: 0x00000A6C
			public bool MoveNext()
			{
				if (this.completed)
				{
					return false;
				}
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.completed = true;
					if (this.autoReleaseWhenCanceled && this.handle.IsValid())
					{
						Addressables.Release<T>(this.handle);
					}
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.progress != null && this.handle.IsValid())
				{
					this.progress.Report(this.handle.GetDownloadStatus().Percent);
				}
				return true;
			}

			// Token: 0x06000028 RID: 40 RVA: 0x00002900 File Offset: 0x00000B00
			private bool TryReturn()
			{
				this.core.Reset();
				this.handle = default(AsyncOperationHandle<T>);
				this.progress = null;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				return AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource<T>.pool.TryPush(this);
			}

			// Token: 0x04000015 RID: 21
			private static TaskPool<AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource<T>> pool;

			// Token: 0x04000016 RID: 22
			private AddressablesAsyncExtensions.AsyncOperationHandleConfiguredSource<T> nextNode;

			// Token: 0x04000017 RID: 23
			private readonly Action<AsyncOperationHandle<T>> completedCallback;

			// Token: 0x04000018 RID: 24
			private AsyncOperationHandle<T> handle;

			// Token: 0x04000019 RID: 25
			private CancellationToken cancellationToken;

			// Token: 0x0400001A RID: 26
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x0400001B RID: 27
			private IProgress<float> progress;

			// Token: 0x0400001C RID: 28
			private bool autoReleaseWhenCanceled;

			// Token: 0x0400001D RID: 29
			private bool cancelImmediately;

			// Token: 0x0400001E RID: 30
			private bool completed;

			// Token: 0x0400001F RID: 31
			private UniTaskCompletionSourceCore<T> core;
		}
	}
}
