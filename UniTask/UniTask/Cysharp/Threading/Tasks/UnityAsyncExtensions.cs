using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks.Sources;
using Cysharp.Threading.Tasks.Internal;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000157 RID: 343
	public static class UnityAsyncExtensions
	{
		// Token: 0x060007F0 RID: 2032 RVA: 0x0002609C File Offset: 0x0002429C
		public static UnityAsyncExtensions.AssetBundleRequestAllAssetsAwaiter AwaitForAllAssets(this AssetBundleRequest asyncOperation)
		{
			Error.ThrowArgumentNullException<AssetBundleRequest>(asyncOperation, "asyncOperation");
			return new UnityAsyncExtensions.AssetBundleRequestAllAssetsAwaiter(asyncOperation);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x000260AF File Offset: 0x000242AF
		public static UniTask<global::UnityEngine.Object[]> AwaitForAllAssets(this AssetBundleRequest asyncOperation, CancellationToken cancellationToken)
		{
			return asyncOperation.AwaitForAllAssets(null, PlayerLoopTiming.Update, cancellationToken, false);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x000260BB File Offset: 0x000242BB
		public static UniTask<global::UnityEngine.Object[]> AwaitForAllAssets(this AssetBundleRequest asyncOperation, CancellationToken cancellationToken, bool cancelImmediately)
		{
			return asyncOperation.AwaitForAllAssets(null, PlayerLoopTiming.Update, cancellationToken, cancelImmediately);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x000260C8 File Offset: 0x000242C8
		public static UniTask<global::UnityEngine.Object[]> AwaitForAllAssets(this AssetBundleRequest asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			Error.ThrowArgumentNullException<AssetBundleRequest>(asyncOperation, "asyncOperation");
			if (cancellationToken.IsCancellationRequested)
			{
				return UniTask.FromCanceled<global::UnityEngine.Object[]>(cancellationToken);
			}
			if (asyncOperation.isDone)
			{
				return UniTask.FromResult<global::UnityEngine.Object[]>(asyncOperation.allAssets);
			}
			short token;
			return new UniTask<global::UnityEngine.Object[]>(UnityAsyncExtensions.AssetBundleRequestAllAssetsConfiguredSource.Create(asyncOperation, timing, progress, cancellationToken, cancelImmediately, out token), token);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00026118 File Offset: 0x00024318
		public static UniTask<AsyncGPUReadbackRequest>.Awaiter GetAwaiter(this AsyncGPUReadbackRequest asyncOperation)
		{
			return asyncOperation.ToUniTask(PlayerLoopTiming.Update, default(CancellationToken), false).GetAwaiter();
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0002613E File Offset: 0x0002433E
		public static UniTask<AsyncGPUReadbackRequest> WithCancellation(this AsyncGPUReadbackRequest asyncOperation, CancellationToken cancellationToken)
		{
			return asyncOperation.ToUniTask(PlayerLoopTiming.Update, cancellationToken, false);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00026149 File Offset: 0x00024349
		public static UniTask<AsyncGPUReadbackRequest> WithCancellation(this AsyncGPUReadbackRequest asyncOperation, CancellationToken cancellationToken, bool cancelImmediately)
		{
			return asyncOperation.ToUniTask(PlayerLoopTiming.Update, cancellationToken, cancelImmediately);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00026154 File Offset: 0x00024354
		public static UniTask<AsyncGPUReadbackRequest> ToUniTask(this AsyncGPUReadbackRequest asyncOperation, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			if (asyncOperation.done)
			{
				return UniTask.FromResult<AsyncGPUReadbackRequest>(asyncOperation);
			}
			short token;
			return new UniTask<AsyncGPUReadbackRequest>(UnityAsyncExtensions.AsyncGPUReadbackRequestAwaiterConfiguredSource.Create(asyncOperation, timing, cancellationToken, cancelImmediately, out token), token);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00026184 File Offset: 0x00024384
		public static async UniTask WaitAsync(this JobHandle jobHandle, PlayerLoopTiming waitTiming, CancellationToken cancellationToken = default(CancellationToken))
		{
			await UniTask.Yield(waitTiming);
			jobHandle.Complete();
			cancellationToken.ThrowIfCancellationRequested();
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x000261D8 File Offset: 0x000243D8
		public static UniTask.Awaiter GetAwaiter(this JobHandle jobHandle)
		{
			short token;
			UnityAsyncExtensions.JobHandlePromise handler = UnityAsyncExtensions.JobHandlePromise.Create(jobHandle, out token);
			PlayerLoopHelper.AddAction(PlayerLoopTiming.EarlyUpdate, handler);
			PlayerLoopHelper.AddAction(PlayerLoopTiming.PreUpdate, handler);
			PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, handler);
			PlayerLoopHelper.AddAction(PlayerLoopTiming.PreLateUpdate, handler);
			PlayerLoopHelper.AddAction(PlayerLoopTiming.PostLateUpdate, handler);
			return new UniTask(handler, token).GetAwaiter();
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00026224 File Offset: 0x00024424
		public static UniTask ToUniTask(this JobHandle jobHandle, PlayerLoopTiming waitTiming)
		{
			short token;
			UnityAsyncExtensions.JobHandlePromise handler = UnityAsyncExtensions.JobHandlePromise.Create(jobHandle, out token);
			PlayerLoopHelper.AddAction(waitTiming, handler);
			return new UniTask(handler, token);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00026248 File Offset: 0x00024448
		public static UniTask StartAsyncCoroutine(this MonoBehaviour monoBehaviour, Func<CancellationToken, UniTask> asyncCoroutine)
		{
			CancellationToken token = monoBehaviour.GetCancellationTokenOnDestroy();
			return asyncCoroutine(token);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00026263 File Offset: 0x00024463
		public static UniTask WithCancellation(this AsyncOperation asyncOperation, CancellationToken cancellationToken)
		{
			return asyncOperation.ToUniTask(null, PlayerLoopTiming.Update, cancellationToken, false);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0002626F File Offset: 0x0002446F
		public static UniTask WithCancellation(this AsyncOperation asyncOperation, CancellationToken cancellationToken, bool cancelImmediately)
		{
			return asyncOperation.ToUniTask(null, PlayerLoopTiming.Update, cancellationToken, cancelImmediately);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0002627C File Offset: 0x0002447C
		public static UniTask ToUniTask(this AsyncOperation asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			Error.ThrowArgumentNullException<AsyncOperation>(asyncOperation, "asyncOperation");
			if (cancellationToken.IsCancellationRequested)
			{
				return UniTask.FromCanceled(cancellationToken);
			}
			if (asyncOperation.isDone)
			{
				return UniTask.CompletedTask;
			}
			short token;
			return new UniTask(UnityAsyncExtensions.AsyncOperationConfiguredSource.Create(asyncOperation, timing, progress, cancellationToken, cancelImmediately, out token), token);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x000262C5 File Offset: 0x000244C5
		public static UnityAsyncExtensions.ResourceRequestAwaiter GetAwaiter(this ResourceRequest asyncOperation)
		{
			Error.ThrowArgumentNullException<ResourceRequest>(asyncOperation, "asyncOperation");
			return new UnityAsyncExtensions.ResourceRequestAwaiter(asyncOperation);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x000262D8 File Offset: 0x000244D8
		public static UniTask<global::UnityEngine.Object> WithCancellation(this ResourceRequest asyncOperation, CancellationToken cancellationToken)
		{
			return asyncOperation.ToUniTask(null, PlayerLoopTiming.Update, cancellationToken, false);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x000262E4 File Offset: 0x000244E4
		public static UniTask<global::UnityEngine.Object> WithCancellation(this ResourceRequest asyncOperation, CancellationToken cancellationToken, bool cancelImmediately)
		{
			return asyncOperation.ToUniTask(null, PlayerLoopTiming.Update, cancellationToken, cancelImmediately);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x000262F0 File Offset: 0x000244F0
		public static UniTask<global::UnityEngine.Object> ToUniTask(this ResourceRequest asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			Error.ThrowArgumentNullException<ResourceRequest>(asyncOperation, "asyncOperation");
			if (cancellationToken.IsCancellationRequested)
			{
				return UniTask.FromCanceled<global::UnityEngine.Object>(cancellationToken);
			}
			if (asyncOperation.isDone)
			{
				return UniTask.FromResult<global::UnityEngine.Object>(asyncOperation.asset);
			}
			short token;
			return new UniTask<global::UnityEngine.Object>(UnityAsyncExtensions.ResourceRequestConfiguredSource.Create(asyncOperation, timing, progress, cancellationToken, cancelImmediately, out token), token);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0002633F File Offset: 0x0002453F
		public static UnityAsyncExtensions.AssetBundleRequestAwaiter GetAwaiter(this AssetBundleRequest asyncOperation)
		{
			Error.ThrowArgumentNullException<AssetBundleRequest>(asyncOperation, "asyncOperation");
			return new UnityAsyncExtensions.AssetBundleRequestAwaiter(asyncOperation);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00026352 File Offset: 0x00024552
		public static UniTask<global::UnityEngine.Object> WithCancellation(this AssetBundleRequest asyncOperation, CancellationToken cancellationToken)
		{
			return asyncOperation.ToUniTask(null, PlayerLoopTiming.Update, cancellationToken, false);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0002635E File Offset: 0x0002455E
		public static UniTask<global::UnityEngine.Object> WithCancellation(this AssetBundleRequest asyncOperation, CancellationToken cancellationToken, bool cancelImmediately)
		{
			return asyncOperation.ToUniTask(null, PlayerLoopTiming.Update, cancellationToken, cancelImmediately);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0002636C File Offset: 0x0002456C
		public static UniTask<global::UnityEngine.Object> ToUniTask(this AssetBundleRequest asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			Error.ThrowArgumentNullException<AssetBundleRequest>(asyncOperation, "asyncOperation");
			if (cancellationToken.IsCancellationRequested)
			{
				return UniTask.FromCanceled<global::UnityEngine.Object>(cancellationToken);
			}
			if (asyncOperation.isDone)
			{
				return UniTask.FromResult<global::UnityEngine.Object>(asyncOperation.asset);
			}
			short token;
			return new UniTask<global::UnityEngine.Object>(UnityAsyncExtensions.AssetBundleRequestConfiguredSource.Create(asyncOperation, timing, progress, cancellationToken, cancelImmediately, out token), token);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x000263BB File Offset: 0x000245BB
		public static UnityAsyncExtensions.AssetBundleCreateRequestAwaiter GetAwaiter(this AssetBundleCreateRequest asyncOperation)
		{
			Error.ThrowArgumentNullException<AssetBundleCreateRequest>(asyncOperation, "asyncOperation");
			return new UnityAsyncExtensions.AssetBundleCreateRequestAwaiter(asyncOperation);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x000263CE File Offset: 0x000245CE
		public static UniTask<AssetBundle> WithCancellation(this AssetBundleCreateRequest asyncOperation, CancellationToken cancellationToken)
		{
			return asyncOperation.ToUniTask(null, PlayerLoopTiming.Update, cancellationToken, false);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x000263DA File Offset: 0x000245DA
		public static UniTask<AssetBundle> WithCancellation(this AssetBundleCreateRequest asyncOperation, CancellationToken cancellationToken, bool cancelImmediately)
		{
			return asyncOperation.ToUniTask(null, PlayerLoopTiming.Update, cancellationToken, cancelImmediately);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x000263E8 File Offset: 0x000245E8
		public static UniTask<AssetBundle> ToUniTask(this AssetBundleCreateRequest asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			Error.ThrowArgumentNullException<AssetBundleCreateRequest>(asyncOperation, "asyncOperation");
			if (cancellationToken.IsCancellationRequested)
			{
				return UniTask.FromCanceled<AssetBundle>(cancellationToken);
			}
			if (asyncOperation.isDone)
			{
				return UniTask.FromResult<AssetBundle>(asyncOperation.assetBundle);
			}
			short token;
			return new UniTask<AssetBundle>(UnityAsyncExtensions.AssetBundleCreateRequestConfiguredSource.Create(asyncOperation, timing, progress, cancellationToken, cancelImmediately, out token), token);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00026437 File Offset: 0x00024637
		public static UnityAsyncExtensions.UnityWebRequestAsyncOperationAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOperation)
		{
			Error.ThrowArgumentNullException<UnityWebRequestAsyncOperation>(asyncOperation, "asyncOperation");
			return new UnityAsyncExtensions.UnityWebRequestAsyncOperationAwaiter(asyncOperation);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0002644A File Offset: 0x0002464A
		public static UniTask<UnityWebRequest> WithCancellation(this UnityWebRequestAsyncOperation asyncOperation, CancellationToken cancellationToken)
		{
			return asyncOperation.ToUniTask(null, PlayerLoopTiming.Update, cancellationToken, false);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00026456 File Offset: 0x00024656
		public static UniTask<UnityWebRequest> WithCancellation(this UnityWebRequestAsyncOperation asyncOperation, CancellationToken cancellationToken, bool cancelImmediately)
		{
			return asyncOperation.ToUniTask(null, PlayerLoopTiming.Update, cancellationToken, cancelImmediately);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00026464 File Offset: 0x00024664
		public static UniTask<UnityWebRequest> ToUniTask(this UnityWebRequestAsyncOperation asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			Error.ThrowArgumentNullException<UnityWebRequestAsyncOperation>(asyncOperation, "asyncOperation");
			if (cancellationToken.IsCancellationRequested)
			{
				return UniTask.FromCanceled<UnityWebRequest>(cancellationToken);
			}
			if (!asyncOperation.isDone)
			{
				short token;
				return new UniTask<UnityWebRequest>(UnityAsyncExtensions.UnityWebRequestAsyncOperationConfiguredSource.Create(asyncOperation, timing, progress, cancellationToken, cancelImmediately, out token), token);
			}
			if (asyncOperation.webRequest.IsError())
			{
				return UniTask.FromException<UnityWebRequest>(new UnityWebRequestException(asyncOperation.webRequest));
			}
			return UniTask.FromResult<UnityWebRequest>(asyncOperation.webRequest);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x000264D1 File Offset: 0x000246D1
		public static AsyncUnityEventHandler GetAsyncEventHandler(this UnityEvent unityEvent, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler(unityEvent, cancellationToken, false);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x000264DB File Offset: 0x000246DB
		public static UniTask OnInvokeAsync(this UnityEvent unityEvent, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler(unityEvent, cancellationToken, true).OnInvokeAsync();
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x000264EA File Offset: 0x000246EA
		public static IUniTaskAsyncEnumerable<AsyncUnit> OnInvokeAsAsyncEnumerable(this UnityEvent unityEvent, CancellationToken cancellationToken)
		{
			return new UnityEventHandlerAsyncEnumerable(unityEvent, cancellationToken);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x000264F3 File Offset: 0x000246F3
		public static AsyncUnityEventHandler<T> GetAsyncEventHandler<T>(this UnityEvent<T> unityEvent, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<T>(unityEvent, cancellationToken, false);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x000264FD File Offset: 0x000246FD
		public static UniTask<T> OnInvokeAsync<T>(this UnityEvent<T> unityEvent, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<T>(unityEvent, cancellationToken, true).OnInvokeAsync();
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0002650C File Offset: 0x0002470C
		public static IUniTaskAsyncEnumerable<T> OnInvokeAsAsyncEnumerable<T>(this UnityEvent<T> unityEvent, CancellationToken cancellationToken)
		{
			return new UnityEventHandlerAsyncEnumerable<T>(unityEvent, cancellationToken);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00026515 File Offset: 0x00024715
		public static IAsyncClickEventHandler GetAsyncClickEventHandler(this Button button)
		{
			return new AsyncUnityEventHandler(button.onClick, button.GetCancellationTokenOnDestroy(), false);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00026529 File Offset: 0x00024729
		public static IAsyncClickEventHandler GetAsyncClickEventHandler(this Button button, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler(button.onClick, cancellationToken, false);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00026538 File Offset: 0x00024738
		public static UniTask OnClickAsync(this Button button)
		{
			return new AsyncUnityEventHandler(button.onClick, button.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00026551 File Offset: 0x00024751
		public static UniTask OnClickAsync(this Button button, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler(button.onClick, cancellationToken, true).OnInvokeAsync();
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00026565 File Offset: 0x00024765
		public static IUniTaskAsyncEnumerable<AsyncUnit> OnClickAsAsyncEnumerable(this Button button)
		{
			return new UnityEventHandlerAsyncEnumerable(button.onClick, button.GetCancellationTokenOnDestroy());
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00026578 File Offset: 0x00024778
		public static IUniTaskAsyncEnumerable<AsyncUnit> OnClickAsAsyncEnumerable(this Button button, CancellationToken cancellationToken)
		{
			return new UnityEventHandlerAsyncEnumerable(button.onClick, cancellationToken);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00026586 File Offset: 0x00024786
		public static IAsyncValueChangedEventHandler<bool> GetAsyncValueChangedEventHandler(this Toggle toggle)
		{
			return new AsyncUnityEventHandler<bool>(toggle.onValueChanged, toggle.GetCancellationTokenOnDestroy(), false);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0002659A File Offset: 0x0002479A
		public static IAsyncValueChangedEventHandler<bool> GetAsyncValueChangedEventHandler(this Toggle toggle, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<bool>(toggle.onValueChanged, cancellationToken, false);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x000265A9 File Offset: 0x000247A9
		public static UniTask<bool> OnValueChangedAsync(this Toggle toggle)
		{
			return new AsyncUnityEventHandler<bool>(toggle.onValueChanged, toggle.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x000265C2 File Offset: 0x000247C2
		public static UniTask<bool> OnValueChangedAsync(this Toggle toggle, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<bool>(toggle.onValueChanged, cancellationToken, true).OnInvokeAsync();
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x000265D6 File Offset: 0x000247D6
		public static IUniTaskAsyncEnumerable<bool> OnValueChangedAsAsyncEnumerable(this Toggle toggle)
		{
			return new UnityEventHandlerAsyncEnumerable<bool>(toggle.onValueChanged, toggle.GetCancellationTokenOnDestroy());
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x000265E9 File Offset: 0x000247E9
		public static IUniTaskAsyncEnumerable<bool> OnValueChangedAsAsyncEnumerable(this Toggle toggle, CancellationToken cancellationToken)
		{
			return new UnityEventHandlerAsyncEnumerable<bool>(toggle.onValueChanged, cancellationToken);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x000265F7 File Offset: 0x000247F7
		public static IAsyncValueChangedEventHandler<float> GetAsyncValueChangedEventHandler(this Scrollbar scrollbar)
		{
			return new AsyncUnityEventHandler<float>(scrollbar.onValueChanged, scrollbar.GetCancellationTokenOnDestroy(), false);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0002660B File Offset: 0x0002480B
		public static IAsyncValueChangedEventHandler<float> GetAsyncValueChangedEventHandler(this Scrollbar scrollbar, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<float>(scrollbar.onValueChanged, cancellationToken, false);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0002661A File Offset: 0x0002481A
		public static UniTask<float> OnValueChangedAsync(this Scrollbar scrollbar)
		{
			return new AsyncUnityEventHandler<float>(scrollbar.onValueChanged, scrollbar.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00026633 File Offset: 0x00024833
		public static UniTask<float> OnValueChangedAsync(this Scrollbar scrollbar, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<float>(scrollbar.onValueChanged, cancellationToken, true).OnInvokeAsync();
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00026647 File Offset: 0x00024847
		public static IUniTaskAsyncEnumerable<float> OnValueChangedAsAsyncEnumerable(this Scrollbar scrollbar)
		{
			return new UnityEventHandlerAsyncEnumerable<float>(scrollbar.onValueChanged, scrollbar.GetCancellationTokenOnDestroy());
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0002665A File Offset: 0x0002485A
		public static IUniTaskAsyncEnumerable<float> OnValueChangedAsAsyncEnumerable(this Scrollbar scrollbar, CancellationToken cancellationToken)
		{
			return new UnityEventHandlerAsyncEnumerable<float>(scrollbar.onValueChanged, cancellationToken);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00026668 File Offset: 0x00024868
		public static IAsyncValueChangedEventHandler<Vector2> GetAsyncValueChangedEventHandler(this ScrollRect scrollRect)
		{
			return new AsyncUnityEventHandler<Vector2>(scrollRect.onValueChanged, scrollRect.GetCancellationTokenOnDestroy(), false);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0002667C File Offset: 0x0002487C
		public static IAsyncValueChangedEventHandler<Vector2> GetAsyncValueChangedEventHandler(this ScrollRect scrollRect, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<Vector2>(scrollRect.onValueChanged, cancellationToken, false);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0002668B File Offset: 0x0002488B
		public static UniTask<Vector2> OnValueChangedAsync(this ScrollRect scrollRect)
		{
			return new AsyncUnityEventHandler<Vector2>(scrollRect.onValueChanged, scrollRect.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x000266A4 File Offset: 0x000248A4
		public static UniTask<Vector2> OnValueChangedAsync(this ScrollRect scrollRect, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<Vector2>(scrollRect.onValueChanged, cancellationToken, true).OnInvokeAsync();
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x000266B8 File Offset: 0x000248B8
		public static IUniTaskAsyncEnumerable<Vector2> OnValueChangedAsAsyncEnumerable(this ScrollRect scrollRect)
		{
			return new UnityEventHandlerAsyncEnumerable<Vector2>(scrollRect.onValueChanged, scrollRect.GetCancellationTokenOnDestroy());
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x000266CB File Offset: 0x000248CB
		public static IUniTaskAsyncEnumerable<Vector2> OnValueChangedAsAsyncEnumerable(this ScrollRect scrollRect, CancellationToken cancellationToken)
		{
			return new UnityEventHandlerAsyncEnumerable<Vector2>(scrollRect.onValueChanged, cancellationToken);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x000266D9 File Offset: 0x000248D9
		public static IAsyncValueChangedEventHandler<float> GetAsyncValueChangedEventHandler(this Slider slider)
		{
			return new AsyncUnityEventHandler<float>(slider.onValueChanged, slider.GetCancellationTokenOnDestroy(), false);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x000266ED File Offset: 0x000248ED
		public static IAsyncValueChangedEventHandler<float> GetAsyncValueChangedEventHandler(this Slider slider, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<float>(slider.onValueChanged, cancellationToken, false);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x000266FC File Offset: 0x000248FC
		public static UniTask<float> OnValueChangedAsync(this Slider slider)
		{
			return new AsyncUnityEventHandler<float>(slider.onValueChanged, slider.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00026715 File Offset: 0x00024915
		public static UniTask<float> OnValueChangedAsync(this Slider slider, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<float>(slider.onValueChanged, cancellationToken, true).OnInvokeAsync();
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00026729 File Offset: 0x00024929
		public static IUniTaskAsyncEnumerable<float> OnValueChangedAsAsyncEnumerable(this Slider slider)
		{
			return new UnityEventHandlerAsyncEnumerable<float>(slider.onValueChanged, slider.GetCancellationTokenOnDestroy());
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0002673C File Offset: 0x0002493C
		public static IUniTaskAsyncEnumerable<float> OnValueChangedAsAsyncEnumerable(this Slider slider, CancellationToken cancellationToken)
		{
			return new UnityEventHandlerAsyncEnumerable<float>(slider.onValueChanged, cancellationToken);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0002674A File Offset: 0x0002494A
		public static IAsyncEndEditEventHandler<string> GetAsyncEndEditEventHandler(this InputField inputField)
		{
			return new AsyncUnityEventHandler<string>(inputField.onEndEdit, inputField.GetCancellationTokenOnDestroy(), false);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0002675E File Offset: 0x0002495E
		public static IAsyncEndEditEventHandler<string> GetAsyncEndEditEventHandler(this InputField inputField, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<string>(inputField.onEndEdit, cancellationToken, false);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0002676D File Offset: 0x0002496D
		public static UniTask<string> OnEndEditAsync(this InputField inputField)
		{
			return new AsyncUnityEventHandler<string>(inputField.onEndEdit, inputField.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00026786 File Offset: 0x00024986
		public static UniTask<string> OnEndEditAsync(this InputField inputField, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<string>(inputField.onEndEdit, cancellationToken, true).OnInvokeAsync();
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0002679A File Offset: 0x0002499A
		public static IUniTaskAsyncEnumerable<string> OnEndEditAsAsyncEnumerable(this InputField inputField)
		{
			return new UnityEventHandlerAsyncEnumerable<string>(inputField.onEndEdit, inputField.GetCancellationTokenOnDestroy());
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x000267AD File Offset: 0x000249AD
		public static IUniTaskAsyncEnumerable<string> OnEndEditAsAsyncEnumerable(this InputField inputField, CancellationToken cancellationToken)
		{
			return new UnityEventHandlerAsyncEnumerable<string>(inputField.onEndEdit, cancellationToken);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x000267BB File Offset: 0x000249BB
		public static IAsyncValueChangedEventHandler<string> GetAsyncValueChangedEventHandler(this InputField inputField)
		{
			return new AsyncUnityEventHandler<string>(inputField.onValueChanged, inputField.GetCancellationTokenOnDestroy(), false);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x000267CF File Offset: 0x000249CF
		public static IAsyncValueChangedEventHandler<string> GetAsyncValueChangedEventHandler(this InputField inputField, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<string>(inputField.onValueChanged, cancellationToken, false);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x000267DE File Offset: 0x000249DE
		public static UniTask<string> OnValueChangedAsync(this InputField inputField)
		{
			return new AsyncUnityEventHandler<string>(inputField.onValueChanged, inputField.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x000267F7 File Offset: 0x000249F7
		public static UniTask<string> OnValueChangedAsync(this InputField inputField, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<string>(inputField.onValueChanged, cancellationToken, true).OnInvokeAsync();
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0002680B File Offset: 0x00024A0B
		public static IUniTaskAsyncEnumerable<string> OnValueChangedAsAsyncEnumerable(this InputField inputField)
		{
			return new UnityEventHandlerAsyncEnumerable<string>(inputField.onValueChanged, inputField.GetCancellationTokenOnDestroy());
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0002681E File Offset: 0x00024A1E
		public static IUniTaskAsyncEnumerable<string> OnValueChangedAsAsyncEnumerable(this InputField inputField, CancellationToken cancellationToken)
		{
			return new UnityEventHandlerAsyncEnumerable<string>(inputField.onValueChanged, cancellationToken);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0002682C File Offset: 0x00024A2C
		public static IAsyncValueChangedEventHandler<int> GetAsyncValueChangedEventHandler(this Dropdown dropdown)
		{
			return new AsyncUnityEventHandler<int>(dropdown.onValueChanged, dropdown.GetCancellationTokenOnDestroy(), false);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00026840 File Offset: 0x00024A40
		public static IAsyncValueChangedEventHandler<int> GetAsyncValueChangedEventHandler(this Dropdown dropdown, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<int>(dropdown.onValueChanged, cancellationToken, false);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0002684F File Offset: 0x00024A4F
		public static UniTask<int> OnValueChangedAsync(this Dropdown dropdown)
		{
			return new AsyncUnityEventHandler<int>(dropdown.onValueChanged, dropdown.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00026868 File Offset: 0x00024A68
		public static UniTask<int> OnValueChangedAsync(this Dropdown dropdown, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<int>(dropdown.onValueChanged, cancellationToken, true).OnInvokeAsync();
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0002687C File Offset: 0x00024A7C
		public static IUniTaskAsyncEnumerable<int> OnValueChangedAsAsyncEnumerable(this Dropdown dropdown)
		{
			return new UnityEventHandlerAsyncEnumerable<int>(dropdown.onValueChanged, dropdown.GetCancellationTokenOnDestroy());
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0002688F File Offset: 0x00024A8F
		public static IUniTaskAsyncEnumerable<int> OnValueChangedAsAsyncEnumerable(this Dropdown dropdown, CancellationToken cancellationToken)
		{
			return new UnityEventHandlerAsyncEnumerable<int>(dropdown.onValueChanged, cancellationToken);
		}

		// Token: 0x02000158 RID: 344
		public struct AssetBundleRequestAllAssetsAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06000845 RID: 2117 RVA: 0x0002689D File Offset: 0x00024A9D
			public AssetBundleRequestAllAssetsAwaiter(AssetBundleRequest asyncOperation)
			{
				this.asyncOperation = asyncOperation;
				this.continuationAction = null;
			}

			// Token: 0x06000846 RID: 2118 RVA: 0x000268AD File Offset: 0x00024AAD
			public UnityAsyncExtensions.AssetBundleRequestAllAssetsAwaiter GetAwaiter()
			{
				return this;
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x06000847 RID: 2119 RVA: 0x000268B5 File Offset: 0x00024AB5
			public bool IsCompleted
			{
				get
				{
					return this.asyncOperation.isDone;
				}
			}

			// Token: 0x06000848 RID: 2120 RVA: 0x000268C4 File Offset: 0x00024AC4
			public global::UnityEngine.Object[] GetResult()
			{
				if (this.continuationAction != null)
				{
					this.asyncOperation.completed -= this.continuationAction;
					this.continuationAction = null;
					global::UnityEngine.Object[] allAssets = this.asyncOperation.allAssets;
					this.asyncOperation = null;
					return allAssets;
				}
				global::UnityEngine.Object[] allAssets2 = this.asyncOperation.allAssets;
				this.asyncOperation = null;
				return allAssets2;
			}

			// Token: 0x06000849 RID: 2121 RVA: 0x00026916 File Offset: 0x00024B16
			public void OnCompleted(Action continuation)
			{
				this.UnsafeOnCompleted(continuation);
			}

			// Token: 0x0600084A RID: 2122 RVA: 0x0002691F File Offset: 0x00024B1F
			public void UnsafeOnCompleted(Action continuation)
			{
				Error.ThrowWhenContinuationIsAlreadyRegistered<Action<AsyncOperation>>(this.continuationAction);
				this.continuationAction = PooledDelegate<AsyncOperation>.Create(continuation);
				this.asyncOperation.completed += this.continuationAction;
			}

			// Token: 0x04000552 RID: 1362
			private AssetBundleRequest asyncOperation;

			// Token: 0x04000553 RID: 1363
			private Action<AsyncOperation> continuationAction;
		}

		// Token: 0x02000159 RID: 345
		private sealed class AssetBundleRequestAllAssetsConfiguredSource : IUniTaskSource<global::UnityEngine.Object[]>, IUniTaskSource, IValueTaskSource, IValueTaskSource<global::UnityEngine.Object[]>, IPlayerLoopItem, ITaskPoolNode<UnityAsyncExtensions.AssetBundleRequestAllAssetsConfiguredSource>
		{
			// Token: 0x17000059 RID: 89
			// (get) Token: 0x0600084B RID: 2123 RVA: 0x00026949 File Offset: 0x00024B49
			public ref UnityAsyncExtensions.AssetBundleRequestAllAssetsConfiguredSource NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x0600084C RID: 2124 RVA: 0x00026951 File Offset: 0x00024B51
			static AssetBundleRequestAllAssetsConfiguredSource()
			{
				TaskPool.RegisterSizeGetter(typeof(UnityAsyncExtensions.AssetBundleRequestAllAssetsConfiguredSource), () => UnityAsyncExtensions.AssetBundleRequestAllAssetsConfiguredSource.pool.Size);
			}

			// Token: 0x0600084D RID: 2125 RVA: 0x00026972 File Offset: 0x00024B72
			private AssetBundleRequestAllAssetsConfiguredSource()
			{
				this.continuationAction = new Action<AsyncOperation>(this.Continuation);
			}

			// Token: 0x0600084E RID: 2126 RVA: 0x0002698C File Offset: 0x00024B8C
			public static IUniTaskSource<global::UnityEngine.Object[]> Create(AssetBundleRequest asyncOperation, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource<global::UnityEngine.Object[]>.CreateFromCanceled(cancellationToken, out token);
				}
				UnityAsyncExtensions.AssetBundleRequestAllAssetsConfiguredSource result;
				if (!UnityAsyncExtensions.AssetBundleRequestAllAssetsConfiguredSource.pool.TryPop(out result))
				{
					result = new UnityAsyncExtensions.AssetBundleRequestAllAssetsConfiguredSource();
				}
				result.asyncOperation = asyncOperation;
				result.progress = progress;
				result.cancellationToken = cancellationToken;
				result.cancelImmediately = cancelImmediately;
				result.completed = false;
				asyncOperation.completed += result.continuationAction;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UnityAsyncExtensions.AssetBundleRequestAllAssetsConfiguredSource source = (UnityAsyncExtensions.AssetBundleRequestAllAssetsConfiguredSource)state;
						source.core.TrySetCanceled(source.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x0600084F RID: 2127 RVA: 0x00026A40 File Offset: 0x00024C40
			public global::UnityEngine.Object[] GetResult(short token)
			{
				global::UnityEngine.Object[] result;
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

			// Token: 0x06000850 RID: 2128 RVA: 0x00026A8C File Offset: 0x00024C8C
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x06000851 RID: 2129 RVA: 0x00026A96 File Offset: 0x00024C96
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000852 RID: 2130 RVA: 0x00026AA4 File Offset: 0x00024CA4
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000853 RID: 2131 RVA: 0x00026AB1 File Offset: 0x00024CB1
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000854 RID: 2132 RVA: 0x00026AC4 File Offset: 0x00024CC4
			public bool MoveNext()
			{
				if (this.completed || this.asyncOperation == null)
				{
					return false;
				}
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.progress != null)
				{
					this.progress.Report(this.asyncOperation.progress);
				}
				if (this.asyncOperation.isDone)
				{
					this.core.TrySetResult(this.asyncOperation.allAssets);
					return false;
				}
				return true;
			}

			// Token: 0x06000855 RID: 2133 RVA: 0x00026B4C File Offset: 0x00024D4C
			private bool TryReturn()
			{
				this.core.Reset();
				this.asyncOperation = null;
				this.progress = null;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UnityAsyncExtensions.AssetBundleRequestAllAssetsConfiguredSource.pool.TryPush(this);
			}

			// Token: 0x06000856 RID: 2134 RVA: 0x00026B9C File Offset: 0x00024D9C
			private void Continuation(AsyncOperation _)
			{
				if (this.completed)
				{
					return;
				}
				this.completed = true;
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return;
				}
				this.core.TrySetResult(this.asyncOperation.allAssets);
			}

			// Token: 0x04000554 RID: 1364
			private static TaskPool<UnityAsyncExtensions.AssetBundleRequestAllAssetsConfiguredSource> pool;

			// Token: 0x04000555 RID: 1365
			private UnityAsyncExtensions.AssetBundleRequestAllAssetsConfiguredSource nextNode;

			// Token: 0x04000556 RID: 1366
			private AssetBundleRequest asyncOperation;

			// Token: 0x04000557 RID: 1367
			private IProgress<float> progress;

			// Token: 0x04000558 RID: 1368
			private CancellationToken cancellationToken;

			// Token: 0x04000559 RID: 1369
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x0400055A RID: 1370
			private bool cancelImmediately;

			// Token: 0x0400055B RID: 1371
			private bool completed;

			// Token: 0x0400055C RID: 1372
			private UniTaskCompletionSourceCore<global::UnityEngine.Object[]> core;

			// Token: 0x0400055D RID: 1373
			private Action<AsyncOperation> continuationAction;
		}

		// Token: 0x0200015B RID: 347
		private sealed class AsyncGPUReadbackRequestAwaiterConfiguredSource : IUniTaskSource<AsyncGPUReadbackRequest>, IUniTaskSource, IValueTaskSource, IValueTaskSource<AsyncGPUReadbackRequest>, IPlayerLoopItem, ITaskPoolNode<UnityAsyncExtensions.AsyncGPUReadbackRequestAwaiterConfiguredSource>
		{
			// Token: 0x1700005A RID: 90
			// (get) Token: 0x0600085B RID: 2139 RVA: 0x00026C2E File Offset: 0x00024E2E
			public ref UnityAsyncExtensions.AsyncGPUReadbackRequestAwaiterConfiguredSource NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x0600085C RID: 2140 RVA: 0x00026C36 File Offset: 0x00024E36
			static AsyncGPUReadbackRequestAwaiterConfiguredSource()
			{
				TaskPool.RegisterSizeGetter(typeof(UnityAsyncExtensions.AsyncGPUReadbackRequestAwaiterConfiguredSource), () => UnityAsyncExtensions.AsyncGPUReadbackRequestAwaiterConfiguredSource.pool.Size);
			}

			// Token: 0x0600085D RID: 2141 RVA: 0x000020BB File Offset: 0x000002BB
			private AsyncGPUReadbackRequestAwaiterConfiguredSource()
			{
			}

			// Token: 0x0600085E RID: 2142 RVA: 0x00026C58 File Offset: 0x00024E58
			public static IUniTaskSource<AsyncGPUReadbackRequest> Create(AsyncGPUReadbackRequest asyncOperation, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource<AsyncGPUReadbackRequest>.CreateFromCanceled(cancellationToken, out token);
				}
				UnityAsyncExtensions.AsyncGPUReadbackRequestAwaiterConfiguredSource result;
				if (!UnityAsyncExtensions.AsyncGPUReadbackRequestAwaiterConfiguredSource.pool.TryPop(out result))
				{
					result = new UnityAsyncExtensions.AsyncGPUReadbackRequestAwaiterConfiguredSource();
				}
				result.asyncOperation = asyncOperation;
				result.cancellationToken = cancellationToken;
				result.cancelImmediately = cancelImmediately;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UnityAsyncExtensions.AsyncGPUReadbackRequestAwaiterConfiguredSource promise = (UnityAsyncExtensions.AsyncGPUReadbackRequestAwaiterConfiguredSource)state;
						promise.core.TrySetCanceled(promise.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x0600085F RID: 2143 RVA: 0x00026CF0 File Offset: 0x00024EF0
			public AsyncGPUReadbackRequest GetResult(short token)
			{
				AsyncGPUReadbackRequest result;
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

			// Token: 0x06000860 RID: 2144 RVA: 0x00026D3C File Offset: 0x00024F3C
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x06000861 RID: 2145 RVA: 0x00026D46 File Offset: 0x00024F46
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000862 RID: 2146 RVA: 0x00026D54 File Offset: 0x00024F54
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000863 RID: 2147 RVA: 0x00026D61 File Offset: 0x00024F61
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000864 RID: 2148 RVA: 0x00026D74 File Offset: 0x00024F74
			public bool MoveNext()
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.asyncOperation.hasError)
				{
					this.core.TrySetException(new Exception("AsyncGPUReadbackRequest.hasError = true"));
					return false;
				}
				if (this.asyncOperation.done)
				{
					this.core.TrySetResult(this.asyncOperation);
					return false;
				}
				return true;
			}

			// Token: 0x06000865 RID: 2149 RVA: 0x00026DEC File Offset: 0x00024FEC
			private bool TryReturn()
			{
				this.core.Reset();
				this.asyncOperation = default(AsyncGPUReadbackRequest);
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UnityAsyncExtensions.AsyncGPUReadbackRequestAwaiterConfiguredSource.pool.TryPush(this);
			}

			// Token: 0x04000560 RID: 1376
			private static TaskPool<UnityAsyncExtensions.AsyncGPUReadbackRequestAwaiterConfiguredSource> pool;

			// Token: 0x04000561 RID: 1377
			private UnityAsyncExtensions.AsyncGPUReadbackRequestAwaiterConfiguredSource nextNode;

			// Token: 0x04000562 RID: 1378
			private AsyncGPUReadbackRequest asyncOperation;

			// Token: 0x04000563 RID: 1379
			private CancellationToken cancellationToken;

			// Token: 0x04000564 RID: 1380
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x04000565 RID: 1381
			private bool cancelImmediately;

			// Token: 0x04000566 RID: 1382
			private UniTaskCompletionSourceCore<AsyncGPUReadbackRequest> core;
		}

		// Token: 0x0200015D RID: 349
		private sealed class JobHandlePromise : IUniTaskSource, IValueTaskSource, IPlayerLoopItem
		{
			// Token: 0x0600086A RID: 2154 RVA: 0x00026E7C File Offset: 0x0002507C
			public static UnityAsyncExtensions.JobHandlePromise Create(JobHandle jobHandle, out short token)
			{
				UnityAsyncExtensions.JobHandlePromise result = new UnityAsyncExtensions.JobHandlePromise();
				result.jobHandle = jobHandle;
				token = result.core.Version;
				return result;
			}

			// Token: 0x0600086B RID: 2155 RVA: 0x00026EA4 File Offset: 0x000250A4
			public void GetResult(short token)
			{
				this.core.GetResult(token);
			}

			// Token: 0x0600086C RID: 2156 RVA: 0x00026EB3 File Offset: 0x000250B3
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600086D RID: 2157 RVA: 0x00026EC1 File Offset: 0x000250C1
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600086E RID: 2158 RVA: 0x00026ECE File Offset: 0x000250CE
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600086F RID: 2159 RVA: 0x00026EDE File Offset: 0x000250DE
			public bool MoveNext()
			{
				if (this.jobHandle.IsCompleted | PlayerLoopHelper.IsEditorApplicationQuitting)
				{
					this.jobHandle.Complete();
					this.core.TrySetResult(AsyncUnit.Default);
					return false;
				}
				return true;
			}

			// Token: 0x04000569 RID: 1385
			private JobHandle jobHandle;

			// Token: 0x0400056A RID: 1386
			private UniTaskCompletionSourceCore<AsyncUnit> core;
		}

		// Token: 0x0200015E RID: 350
		public struct AsyncOperationAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06000871 RID: 2161 RVA: 0x00026F12 File Offset: 0x00025112
			public AsyncOperationAwaiter(AsyncOperation asyncOperation)
			{
				this.asyncOperation = asyncOperation;
				this.continuationAction = null;
			}

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x06000872 RID: 2162 RVA: 0x00026F22 File Offset: 0x00025122
			public bool IsCompleted
			{
				get
				{
					return this.asyncOperation.isDone;
				}
			}

			// Token: 0x06000873 RID: 2163 RVA: 0x00026F2F File Offset: 0x0002512F
			public void GetResult()
			{
				if (this.continuationAction != null)
				{
					this.asyncOperation.completed -= this.continuationAction;
					this.continuationAction = null;
					this.asyncOperation = null;
					return;
				}
				this.asyncOperation = null;
			}

			// Token: 0x06000874 RID: 2164 RVA: 0x00026F60 File Offset: 0x00025160
			public void OnCompleted(Action continuation)
			{
				this.UnsafeOnCompleted(continuation);
			}

			// Token: 0x06000875 RID: 2165 RVA: 0x00026F69 File Offset: 0x00025169
			public void UnsafeOnCompleted(Action continuation)
			{
				Error.ThrowWhenContinuationIsAlreadyRegistered<Action<AsyncOperation>>(this.continuationAction);
				this.continuationAction = PooledDelegate<AsyncOperation>.Create(continuation);
				this.asyncOperation.completed += this.continuationAction;
			}

			// Token: 0x0400056B RID: 1387
			private AsyncOperation asyncOperation;

			// Token: 0x0400056C RID: 1388
			private Action<AsyncOperation> continuationAction;
		}

		// Token: 0x0200015F RID: 351
		private sealed class AsyncOperationConfiguredSource : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<UnityAsyncExtensions.AsyncOperationConfiguredSource>
		{
			// Token: 0x1700005C RID: 92
			// (get) Token: 0x06000876 RID: 2166 RVA: 0x00026F93 File Offset: 0x00025193
			public ref UnityAsyncExtensions.AsyncOperationConfiguredSource NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x06000877 RID: 2167 RVA: 0x00026F9B File Offset: 0x0002519B
			static AsyncOperationConfiguredSource()
			{
				TaskPool.RegisterSizeGetter(typeof(UnityAsyncExtensions.AsyncOperationConfiguredSource), () => UnityAsyncExtensions.AsyncOperationConfiguredSource.pool.Size);
			}

			// Token: 0x06000878 RID: 2168 RVA: 0x00026FBC File Offset: 0x000251BC
			private AsyncOperationConfiguredSource()
			{
				this.continuationAction = new Action<AsyncOperation>(this.Continuation);
			}

			// Token: 0x06000879 RID: 2169 RVA: 0x00026FD8 File Offset: 0x000251D8
			public static IUniTaskSource Create(AsyncOperation asyncOperation, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				UnityAsyncExtensions.AsyncOperationConfiguredSource result;
				if (!UnityAsyncExtensions.AsyncOperationConfiguredSource.pool.TryPop(out result))
				{
					result = new UnityAsyncExtensions.AsyncOperationConfiguredSource();
				}
				result.asyncOperation = asyncOperation;
				result.progress = progress;
				result.cancellationToken = cancellationToken;
				result.cancelImmediately = cancelImmediately;
				result.completed = false;
				asyncOperation.completed += result.continuationAction;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UnityAsyncExtensions.AsyncOperationConfiguredSource source = (UnityAsyncExtensions.AsyncOperationConfiguredSource)state;
						source.core.TrySetCanceled(source.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x0600087A RID: 2170 RVA: 0x0002708C File Offset: 0x0002528C
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

			// Token: 0x0600087B RID: 2171 RVA: 0x000270D8 File Offset: 0x000252D8
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600087C RID: 2172 RVA: 0x000270E6 File Offset: 0x000252E6
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0600087D RID: 2173 RVA: 0x000270F3 File Offset: 0x000252F3
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600087E RID: 2174 RVA: 0x00027104 File Offset: 0x00025304
			public bool MoveNext()
			{
				if (this.completed || this.asyncOperation == null)
				{
					return false;
				}
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.progress != null)
				{
					this.progress.Report(this.asyncOperation.progress);
				}
				if (this.asyncOperation.isDone)
				{
					this.core.TrySetResult(AsyncUnit.Default);
					return false;
				}
				return true;
			}

			// Token: 0x0600087F RID: 2175 RVA: 0x00027184 File Offset: 0x00025384
			private bool TryReturn()
			{
				this.core.Reset();
				this.asyncOperation.completed -= this.continuationAction;
				this.asyncOperation = null;
				this.progress = null;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UnityAsyncExtensions.AsyncOperationConfiguredSource.pool.TryPush(this);
			}

			// Token: 0x06000880 RID: 2176 RVA: 0x000271E4 File Offset: 0x000253E4
			private void Continuation(AsyncOperation _)
			{
				if (this.completed)
				{
					return;
				}
				this.completed = true;
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return;
				}
				this.core.TrySetResult(AsyncUnit.Default);
			}

			// Token: 0x0400056D RID: 1389
			private static TaskPool<UnityAsyncExtensions.AsyncOperationConfiguredSource> pool;

			// Token: 0x0400056E RID: 1390
			private UnityAsyncExtensions.AsyncOperationConfiguredSource nextNode;

			// Token: 0x0400056F RID: 1391
			private AsyncOperation asyncOperation;

			// Token: 0x04000570 RID: 1392
			private IProgress<float> progress;

			// Token: 0x04000571 RID: 1393
			private CancellationToken cancellationToken;

			// Token: 0x04000572 RID: 1394
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x04000573 RID: 1395
			private bool cancelImmediately;

			// Token: 0x04000574 RID: 1396
			private bool completed;

			// Token: 0x04000575 RID: 1397
			private UniTaskCompletionSourceCore<AsyncUnit> core;

			// Token: 0x04000576 RID: 1398
			private Action<AsyncOperation> continuationAction;
		}

		// Token: 0x02000161 RID: 353
		public struct ResourceRequestAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06000885 RID: 2181 RVA: 0x00027272 File Offset: 0x00025472
			public ResourceRequestAwaiter(ResourceRequest asyncOperation)
			{
				this.asyncOperation = asyncOperation;
				this.continuationAction = null;
			}

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x06000886 RID: 2182 RVA: 0x00027282 File Offset: 0x00025482
			public bool IsCompleted
			{
				get
				{
					return this.asyncOperation.isDone;
				}
			}

			// Token: 0x06000887 RID: 2183 RVA: 0x00027290 File Offset: 0x00025490
			public global::UnityEngine.Object GetResult()
			{
				if (this.continuationAction != null)
				{
					this.asyncOperation.completed -= this.continuationAction;
					this.continuationAction = null;
					global::UnityEngine.Object asset = this.asyncOperation.asset;
					this.asyncOperation = null;
					return asset;
				}
				global::UnityEngine.Object asset2 = this.asyncOperation.asset;
				this.asyncOperation = null;
				return asset2;
			}

			// Token: 0x06000888 RID: 2184 RVA: 0x000272E2 File Offset: 0x000254E2
			public void OnCompleted(Action continuation)
			{
				this.UnsafeOnCompleted(continuation);
			}

			// Token: 0x06000889 RID: 2185 RVA: 0x000272EB File Offset: 0x000254EB
			public void UnsafeOnCompleted(Action continuation)
			{
				Error.ThrowWhenContinuationIsAlreadyRegistered<Action<AsyncOperation>>(this.continuationAction);
				this.continuationAction = PooledDelegate<AsyncOperation>.Create(continuation);
				this.asyncOperation.completed += this.continuationAction;
			}

			// Token: 0x04000579 RID: 1401
			private ResourceRequest asyncOperation;

			// Token: 0x0400057A RID: 1402
			private Action<AsyncOperation> continuationAction;
		}

		// Token: 0x02000162 RID: 354
		private sealed class ResourceRequestConfiguredSource : IUniTaskSource<global::UnityEngine.Object>, IUniTaskSource, IValueTaskSource, IValueTaskSource<global::UnityEngine.Object>, IPlayerLoopItem, ITaskPoolNode<UnityAsyncExtensions.ResourceRequestConfiguredSource>
		{
			// Token: 0x1700005E RID: 94
			// (get) Token: 0x0600088A RID: 2186 RVA: 0x00027315 File Offset: 0x00025515
			public ref UnityAsyncExtensions.ResourceRequestConfiguredSource NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x0600088B RID: 2187 RVA: 0x0002731D File Offset: 0x0002551D
			static ResourceRequestConfiguredSource()
			{
				TaskPool.RegisterSizeGetter(typeof(UnityAsyncExtensions.ResourceRequestConfiguredSource), () => UnityAsyncExtensions.ResourceRequestConfiguredSource.pool.Size);
			}

			// Token: 0x0600088C RID: 2188 RVA: 0x0002733E File Offset: 0x0002553E
			private ResourceRequestConfiguredSource()
			{
				this.continuationAction = new Action<AsyncOperation>(this.Continuation);
			}

			// Token: 0x0600088D RID: 2189 RVA: 0x00027358 File Offset: 0x00025558
			public static IUniTaskSource<global::UnityEngine.Object> Create(ResourceRequest asyncOperation, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource<global::UnityEngine.Object>.CreateFromCanceled(cancellationToken, out token);
				}
				UnityAsyncExtensions.ResourceRequestConfiguredSource result;
				if (!UnityAsyncExtensions.ResourceRequestConfiguredSource.pool.TryPop(out result))
				{
					result = new UnityAsyncExtensions.ResourceRequestConfiguredSource();
				}
				result.asyncOperation = asyncOperation;
				result.progress = progress;
				result.cancellationToken = cancellationToken;
				result.cancelImmediately = cancelImmediately;
				result.completed = false;
				asyncOperation.completed += result.continuationAction;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UnityAsyncExtensions.ResourceRequestConfiguredSource source = (UnityAsyncExtensions.ResourceRequestConfiguredSource)state;
						source.core.TrySetCanceled(source.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x0600088E RID: 2190 RVA: 0x0002740C File Offset: 0x0002560C
			public global::UnityEngine.Object GetResult(short token)
			{
				global::UnityEngine.Object result;
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

			// Token: 0x0600088F RID: 2191 RVA: 0x00027458 File Offset: 0x00025658
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x06000890 RID: 2192 RVA: 0x00027462 File Offset: 0x00025662
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000891 RID: 2193 RVA: 0x00027470 File Offset: 0x00025670
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x06000892 RID: 2194 RVA: 0x0002747D File Offset: 0x0002567D
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000893 RID: 2195 RVA: 0x00027490 File Offset: 0x00025690
			public bool MoveNext()
			{
				if (this.completed || this.asyncOperation == null)
				{
					return false;
				}
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.progress != null)
				{
					this.progress.Report(this.asyncOperation.progress);
				}
				if (this.asyncOperation.isDone)
				{
					this.core.TrySetResult(this.asyncOperation.asset);
					return false;
				}
				return true;
			}

			// Token: 0x06000894 RID: 2196 RVA: 0x00027518 File Offset: 0x00025718
			private bool TryReturn()
			{
				this.core.Reset();
				this.asyncOperation.completed -= this.continuationAction;
				this.asyncOperation = null;
				this.progress = null;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UnityAsyncExtensions.ResourceRequestConfiguredSource.pool.TryPush(this);
			}

			// Token: 0x06000895 RID: 2197 RVA: 0x00027578 File Offset: 0x00025778
			private void Continuation(AsyncOperation _)
			{
				if (this.completed)
				{
					return;
				}
				this.completed = true;
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return;
				}
				this.core.TrySetResult(this.asyncOperation.asset);
			}

			// Token: 0x0400057B RID: 1403
			private static TaskPool<UnityAsyncExtensions.ResourceRequestConfiguredSource> pool;

			// Token: 0x0400057C RID: 1404
			private UnityAsyncExtensions.ResourceRequestConfiguredSource nextNode;

			// Token: 0x0400057D RID: 1405
			private ResourceRequest asyncOperation;

			// Token: 0x0400057E RID: 1406
			private IProgress<float> progress;

			// Token: 0x0400057F RID: 1407
			private CancellationToken cancellationToken;

			// Token: 0x04000580 RID: 1408
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x04000581 RID: 1409
			private bool cancelImmediately;

			// Token: 0x04000582 RID: 1410
			private bool completed;

			// Token: 0x04000583 RID: 1411
			private UniTaskCompletionSourceCore<global::UnityEngine.Object> core;

			// Token: 0x04000584 RID: 1412
			private Action<AsyncOperation> continuationAction;
		}

		// Token: 0x02000164 RID: 356
		public struct AssetBundleRequestAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x0600089A RID: 2202 RVA: 0x0002760A File Offset: 0x0002580A
			public AssetBundleRequestAwaiter(AssetBundleRequest asyncOperation)
			{
				this.asyncOperation = asyncOperation;
				this.continuationAction = null;
			}

			// Token: 0x1700005F RID: 95
			// (get) Token: 0x0600089B RID: 2203 RVA: 0x0002761A File Offset: 0x0002581A
			public bool IsCompleted
			{
				get
				{
					return this.asyncOperation.isDone;
				}
			}

			// Token: 0x0600089C RID: 2204 RVA: 0x00027628 File Offset: 0x00025828
			public global::UnityEngine.Object GetResult()
			{
				if (this.continuationAction != null)
				{
					this.asyncOperation.completed -= this.continuationAction;
					this.continuationAction = null;
					global::UnityEngine.Object asset = this.asyncOperation.asset;
					this.asyncOperation = null;
					return asset;
				}
				global::UnityEngine.Object asset2 = this.asyncOperation.asset;
				this.asyncOperation = null;
				return asset2;
			}

			// Token: 0x0600089D RID: 2205 RVA: 0x0002767A File Offset: 0x0002587A
			public void OnCompleted(Action continuation)
			{
				this.UnsafeOnCompleted(continuation);
			}

			// Token: 0x0600089E RID: 2206 RVA: 0x00027683 File Offset: 0x00025883
			public void UnsafeOnCompleted(Action continuation)
			{
				Error.ThrowWhenContinuationIsAlreadyRegistered<Action<AsyncOperation>>(this.continuationAction);
				this.continuationAction = PooledDelegate<AsyncOperation>.Create(continuation);
				this.asyncOperation.completed += this.continuationAction;
			}

			// Token: 0x04000587 RID: 1415
			private AssetBundleRequest asyncOperation;

			// Token: 0x04000588 RID: 1416
			private Action<AsyncOperation> continuationAction;
		}

		// Token: 0x02000165 RID: 357
		private sealed class AssetBundleRequestConfiguredSource : IUniTaskSource<global::UnityEngine.Object>, IUniTaskSource, IValueTaskSource, IValueTaskSource<global::UnityEngine.Object>, IPlayerLoopItem, ITaskPoolNode<UnityAsyncExtensions.AssetBundleRequestConfiguredSource>
		{
			// Token: 0x17000060 RID: 96
			// (get) Token: 0x0600089F RID: 2207 RVA: 0x000276AD File Offset: 0x000258AD
			public ref UnityAsyncExtensions.AssetBundleRequestConfiguredSource NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x060008A0 RID: 2208 RVA: 0x000276B5 File Offset: 0x000258B5
			static AssetBundleRequestConfiguredSource()
			{
				TaskPool.RegisterSizeGetter(typeof(UnityAsyncExtensions.AssetBundleRequestConfiguredSource), () => UnityAsyncExtensions.AssetBundleRequestConfiguredSource.pool.Size);
			}

			// Token: 0x060008A1 RID: 2209 RVA: 0x000276D6 File Offset: 0x000258D6
			private AssetBundleRequestConfiguredSource()
			{
				this.continuationAction = new Action<AsyncOperation>(this.Continuation);
			}

			// Token: 0x060008A2 RID: 2210 RVA: 0x000276F0 File Offset: 0x000258F0
			public static IUniTaskSource<global::UnityEngine.Object> Create(AssetBundleRequest asyncOperation, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource<global::UnityEngine.Object>.CreateFromCanceled(cancellationToken, out token);
				}
				UnityAsyncExtensions.AssetBundleRequestConfiguredSource result;
				if (!UnityAsyncExtensions.AssetBundleRequestConfiguredSource.pool.TryPop(out result))
				{
					result = new UnityAsyncExtensions.AssetBundleRequestConfiguredSource();
				}
				result.asyncOperation = asyncOperation;
				result.progress = progress;
				result.cancellationToken = cancellationToken;
				result.cancelImmediately = cancelImmediately;
				result.completed = false;
				asyncOperation.completed += result.continuationAction;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UnityAsyncExtensions.AssetBundleRequestConfiguredSource source = (UnityAsyncExtensions.AssetBundleRequestConfiguredSource)state;
						source.core.TrySetCanceled(source.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x060008A3 RID: 2211 RVA: 0x000277A4 File Offset: 0x000259A4
			public global::UnityEngine.Object GetResult(short token)
			{
				global::UnityEngine.Object result;
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

			// Token: 0x060008A4 RID: 2212 RVA: 0x000277F0 File Offset: 0x000259F0
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x060008A5 RID: 2213 RVA: 0x000277FA File Offset: 0x000259FA
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060008A6 RID: 2214 RVA: 0x00027808 File Offset: 0x00025A08
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060008A7 RID: 2215 RVA: 0x00027815 File Offset: 0x00025A15
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060008A8 RID: 2216 RVA: 0x00027828 File Offset: 0x00025A28
			public bool MoveNext()
			{
				if (this.completed || this.asyncOperation == null)
				{
					return false;
				}
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.progress != null)
				{
					this.progress.Report(this.asyncOperation.progress);
				}
				if (this.asyncOperation.isDone)
				{
					this.core.TrySetResult(this.asyncOperation.asset);
					return false;
				}
				return true;
			}

			// Token: 0x060008A9 RID: 2217 RVA: 0x000278B0 File Offset: 0x00025AB0
			private bool TryReturn()
			{
				this.core.Reset();
				this.asyncOperation.completed -= this.continuationAction;
				this.asyncOperation = null;
				this.progress = null;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UnityAsyncExtensions.AssetBundleRequestConfiguredSource.pool.TryPush(this);
			}

			// Token: 0x060008AA RID: 2218 RVA: 0x00027910 File Offset: 0x00025B10
			private void Continuation(AsyncOperation _)
			{
				if (this.completed)
				{
					return;
				}
				this.completed = true;
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return;
				}
				this.core.TrySetResult(this.asyncOperation.asset);
			}

			// Token: 0x04000589 RID: 1417
			private static TaskPool<UnityAsyncExtensions.AssetBundleRequestConfiguredSource> pool;

			// Token: 0x0400058A RID: 1418
			private UnityAsyncExtensions.AssetBundleRequestConfiguredSource nextNode;

			// Token: 0x0400058B RID: 1419
			private AssetBundleRequest asyncOperation;

			// Token: 0x0400058C RID: 1420
			private IProgress<float> progress;

			// Token: 0x0400058D RID: 1421
			private CancellationToken cancellationToken;

			// Token: 0x0400058E RID: 1422
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x0400058F RID: 1423
			private bool cancelImmediately;

			// Token: 0x04000590 RID: 1424
			private bool completed;

			// Token: 0x04000591 RID: 1425
			private UniTaskCompletionSourceCore<global::UnityEngine.Object> core;

			// Token: 0x04000592 RID: 1426
			private Action<AsyncOperation> continuationAction;
		}

		// Token: 0x02000167 RID: 359
		public struct AssetBundleCreateRequestAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x060008AF RID: 2223 RVA: 0x000279A2 File Offset: 0x00025BA2
			public AssetBundleCreateRequestAwaiter(AssetBundleCreateRequest asyncOperation)
			{
				this.asyncOperation = asyncOperation;
				this.continuationAction = null;
			}

			// Token: 0x17000061 RID: 97
			// (get) Token: 0x060008B0 RID: 2224 RVA: 0x000279B2 File Offset: 0x00025BB2
			public bool IsCompleted
			{
				get
				{
					return this.asyncOperation.isDone;
				}
			}

			// Token: 0x060008B1 RID: 2225 RVA: 0x000279C0 File Offset: 0x00025BC0
			public AssetBundle GetResult()
			{
				if (this.continuationAction != null)
				{
					this.asyncOperation.completed -= this.continuationAction;
					this.continuationAction = null;
					AssetBundle assetBundle = this.asyncOperation.assetBundle;
					this.asyncOperation = null;
					return assetBundle;
				}
				AssetBundle assetBundle2 = this.asyncOperation.assetBundle;
				this.asyncOperation = null;
				return assetBundle2;
			}

			// Token: 0x060008B2 RID: 2226 RVA: 0x00027A12 File Offset: 0x00025C12
			public void OnCompleted(Action continuation)
			{
				this.UnsafeOnCompleted(continuation);
			}

			// Token: 0x060008B3 RID: 2227 RVA: 0x00027A1B File Offset: 0x00025C1B
			public void UnsafeOnCompleted(Action continuation)
			{
				Error.ThrowWhenContinuationIsAlreadyRegistered<Action<AsyncOperation>>(this.continuationAction);
				this.continuationAction = PooledDelegate<AsyncOperation>.Create(continuation);
				this.asyncOperation.completed += this.continuationAction;
			}

			// Token: 0x04000595 RID: 1429
			private AssetBundleCreateRequest asyncOperation;

			// Token: 0x04000596 RID: 1430
			private Action<AsyncOperation> continuationAction;
		}

		// Token: 0x02000168 RID: 360
		private sealed class AssetBundleCreateRequestConfiguredSource : IUniTaskSource<AssetBundle>, IUniTaskSource, IValueTaskSource, IValueTaskSource<AssetBundle>, IPlayerLoopItem, ITaskPoolNode<UnityAsyncExtensions.AssetBundleCreateRequestConfiguredSource>
		{
			// Token: 0x17000062 RID: 98
			// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00027A45 File Offset: 0x00025C45
			public ref UnityAsyncExtensions.AssetBundleCreateRequestConfiguredSource NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x060008B5 RID: 2229 RVA: 0x00027A4D File Offset: 0x00025C4D
			static AssetBundleCreateRequestConfiguredSource()
			{
				TaskPool.RegisterSizeGetter(typeof(UnityAsyncExtensions.AssetBundleCreateRequestConfiguredSource), () => UnityAsyncExtensions.AssetBundleCreateRequestConfiguredSource.pool.Size);
			}

			// Token: 0x060008B6 RID: 2230 RVA: 0x00027A6E File Offset: 0x00025C6E
			private AssetBundleCreateRequestConfiguredSource()
			{
				this.continuationAction = new Action<AsyncOperation>(this.Continuation);
			}

			// Token: 0x060008B7 RID: 2231 RVA: 0x00027A88 File Offset: 0x00025C88
			public static IUniTaskSource<AssetBundle> Create(AssetBundleCreateRequest asyncOperation, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource<AssetBundle>.CreateFromCanceled(cancellationToken, out token);
				}
				UnityAsyncExtensions.AssetBundleCreateRequestConfiguredSource result;
				if (!UnityAsyncExtensions.AssetBundleCreateRequestConfiguredSource.pool.TryPop(out result))
				{
					result = new UnityAsyncExtensions.AssetBundleCreateRequestConfiguredSource();
				}
				result.asyncOperation = asyncOperation;
				result.progress = progress;
				result.cancellationToken = cancellationToken;
				result.cancelImmediately = cancelImmediately;
				result.completed = false;
				asyncOperation.completed += result.continuationAction;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UnityAsyncExtensions.AssetBundleCreateRequestConfiguredSource source = (UnityAsyncExtensions.AssetBundleCreateRequestConfiguredSource)state;
						source.core.TrySetCanceled(source.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x060008B8 RID: 2232 RVA: 0x00027B3C File Offset: 0x00025D3C
			public AssetBundle GetResult(short token)
			{
				AssetBundle result;
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

			// Token: 0x060008B9 RID: 2233 RVA: 0x00027B88 File Offset: 0x00025D88
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x060008BA RID: 2234 RVA: 0x00027B92 File Offset: 0x00025D92
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060008BB RID: 2235 RVA: 0x00027BA0 File Offset: 0x00025DA0
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060008BC RID: 2236 RVA: 0x00027BAD File Offset: 0x00025DAD
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060008BD RID: 2237 RVA: 0x00027BC0 File Offset: 0x00025DC0
			public bool MoveNext()
			{
				if (this.completed || this.asyncOperation == null)
				{
					return false;
				}
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.progress != null)
				{
					this.progress.Report(this.asyncOperation.progress);
				}
				if (this.asyncOperation.isDone)
				{
					this.core.TrySetResult(this.asyncOperation.assetBundle);
					return false;
				}
				return true;
			}

			// Token: 0x060008BE RID: 2238 RVA: 0x00027C48 File Offset: 0x00025E48
			private bool TryReturn()
			{
				this.core.Reset();
				this.asyncOperation.completed -= this.continuationAction;
				this.asyncOperation = null;
				this.progress = null;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UnityAsyncExtensions.AssetBundleCreateRequestConfiguredSource.pool.TryPush(this);
			}

			// Token: 0x060008BF RID: 2239 RVA: 0x00027CA8 File Offset: 0x00025EA8
			private void Continuation(AsyncOperation _)
			{
				if (this.completed)
				{
					return;
				}
				this.completed = true;
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return;
				}
				this.core.TrySetResult(this.asyncOperation.assetBundle);
			}

			// Token: 0x04000597 RID: 1431
			private static TaskPool<UnityAsyncExtensions.AssetBundleCreateRequestConfiguredSource> pool;

			// Token: 0x04000598 RID: 1432
			private UnityAsyncExtensions.AssetBundleCreateRequestConfiguredSource nextNode;

			// Token: 0x04000599 RID: 1433
			private AssetBundleCreateRequest asyncOperation;

			// Token: 0x0400059A RID: 1434
			private IProgress<float> progress;

			// Token: 0x0400059B RID: 1435
			private CancellationToken cancellationToken;

			// Token: 0x0400059C RID: 1436
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x0400059D RID: 1437
			private bool cancelImmediately;

			// Token: 0x0400059E RID: 1438
			private bool completed;

			// Token: 0x0400059F RID: 1439
			private UniTaskCompletionSourceCore<AssetBundle> core;

			// Token: 0x040005A0 RID: 1440
			private Action<AsyncOperation> continuationAction;
		}

		// Token: 0x0200016A RID: 362
		public struct UnityWebRequestAsyncOperationAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x060008C4 RID: 2244 RVA: 0x00027D3A File Offset: 0x00025F3A
			public UnityWebRequestAsyncOperationAwaiter(UnityWebRequestAsyncOperation asyncOperation)
			{
				this.asyncOperation = asyncOperation;
				this.continuationAction = null;
			}

			// Token: 0x17000063 RID: 99
			// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00027D4A File Offset: 0x00025F4A
			public bool IsCompleted
			{
				get
				{
					return this.asyncOperation.isDone;
				}
			}

			// Token: 0x060008C6 RID: 2246 RVA: 0x00027D58 File Offset: 0x00025F58
			public UnityWebRequest GetResult()
			{
				if (this.continuationAction != null)
				{
					this.asyncOperation.completed -= this.continuationAction;
					this.continuationAction = null;
					UnityWebRequest result = this.asyncOperation.webRequest;
					this.asyncOperation = null;
					if (result.IsError())
					{
						throw new UnityWebRequestException(result);
					}
					return result;
				}
				else
				{
					UnityWebRequest result2 = this.asyncOperation.webRequest;
					this.asyncOperation = null;
					if (result2.IsError())
					{
						throw new UnityWebRequestException(result2);
					}
					return result2;
				}
			}

			// Token: 0x060008C7 RID: 2247 RVA: 0x00027DCC File Offset: 0x00025FCC
			public void OnCompleted(Action continuation)
			{
				this.UnsafeOnCompleted(continuation);
			}

			// Token: 0x060008C8 RID: 2248 RVA: 0x00027DD5 File Offset: 0x00025FD5
			public void UnsafeOnCompleted(Action continuation)
			{
				Error.ThrowWhenContinuationIsAlreadyRegistered<Action<AsyncOperation>>(this.continuationAction);
				this.continuationAction = PooledDelegate<AsyncOperation>.Create(continuation);
				this.asyncOperation.completed += this.continuationAction;
			}

			// Token: 0x040005A3 RID: 1443
			private UnityWebRequestAsyncOperation asyncOperation;

			// Token: 0x040005A4 RID: 1444
			private Action<AsyncOperation> continuationAction;
		}

		// Token: 0x0200016B RID: 363
		private sealed class UnityWebRequestAsyncOperationConfiguredSource : IUniTaskSource<UnityWebRequest>, IUniTaskSource, IValueTaskSource, IValueTaskSource<UnityWebRequest>, IPlayerLoopItem, ITaskPoolNode<UnityAsyncExtensions.UnityWebRequestAsyncOperationConfiguredSource>
		{
			// Token: 0x17000064 RID: 100
			// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00027DFF File Offset: 0x00025FFF
			public ref UnityAsyncExtensions.UnityWebRequestAsyncOperationConfiguredSource NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x060008CA RID: 2250 RVA: 0x00027E07 File Offset: 0x00026007
			static UnityWebRequestAsyncOperationConfiguredSource()
			{
				TaskPool.RegisterSizeGetter(typeof(UnityAsyncExtensions.UnityWebRequestAsyncOperationConfiguredSource), () => UnityAsyncExtensions.UnityWebRequestAsyncOperationConfiguredSource.pool.Size);
			}

			// Token: 0x060008CB RID: 2251 RVA: 0x00027E28 File Offset: 0x00026028
			private UnityWebRequestAsyncOperationConfiguredSource()
			{
				this.continuationAction = new Action<AsyncOperation>(this.Continuation);
			}

			// Token: 0x060008CC RID: 2252 RVA: 0x00027E44 File Offset: 0x00026044
			public static IUniTaskSource<UnityWebRequest> Create(UnityWebRequestAsyncOperation asyncOperation, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource<UnityWebRequest>.CreateFromCanceled(cancellationToken, out token);
				}
				UnityAsyncExtensions.UnityWebRequestAsyncOperationConfiguredSource result;
				if (!UnityAsyncExtensions.UnityWebRequestAsyncOperationConfiguredSource.pool.TryPop(out result))
				{
					result = new UnityAsyncExtensions.UnityWebRequestAsyncOperationConfiguredSource();
				}
				result.asyncOperation = asyncOperation;
				result.progress = progress;
				result.cancellationToken = cancellationToken;
				result.cancelImmediately = cancelImmediately;
				result.completed = false;
				asyncOperation.completed += result.continuationAction;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						UnityAsyncExtensions.UnityWebRequestAsyncOperationConfiguredSource source = (UnityAsyncExtensions.UnityWebRequestAsyncOperationConfiguredSource)state;
						source.asyncOperation.webRequest.Abort();
						source.core.TrySetCanceled(source.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x060008CD RID: 2253 RVA: 0x00027EF8 File Offset: 0x000260F8
			public UnityWebRequest GetResult(short token)
			{
				UnityWebRequest result;
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

			// Token: 0x060008CE RID: 2254 RVA: 0x00027F44 File Offset: 0x00026144
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x060008CF RID: 2255 RVA: 0x00027F4E File Offset: 0x0002614E
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060008D0 RID: 2256 RVA: 0x00027F5C File Offset: 0x0002615C
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060008D1 RID: 2257 RVA: 0x00027F69 File Offset: 0x00026169
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060008D2 RID: 2258 RVA: 0x00027F7C File Offset: 0x0002617C
			public bool MoveNext()
			{
				if (this.completed || this.asyncOperation == null)
				{
					return false;
				}
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.asyncOperation.webRequest.Abort();
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.progress != null)
				{
					this.progress.Report(this.asyncOperation.progress);
				}
				if (this.asyncOperation.isDone)
				{
					if (this.asyncOperation.webRequest.IsError())
					{
						this.core.TrySetException(new UnityWebRequestException(this.asyncOperation.webRequest));
					}
					else
					{
						this.core.TrySetResult(this.asyncOperation.webRequest);
					}
					return false;
				}
				return true;
			}

			// Token: 0x060008D3 RID: 2259 RVA: 0x00028044 File Offset: 0x00026244
			private bool TryReturn()
			{
				this.core.Reset();
				this.asyncOperation.completed -= this.continuationAction;
				this.asyncOperation = null;
				this.progress = null;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return UnityAsyncExtensions.UnityWebRequestAsyncOperationConfiguredSource.pool.TryPush(this);
			}

			// Token: 0x060008D4 RID: 2260 RVA: 0x000280A4 File Offset: 0x000262A4
			private void Continuation(AsyncOperation _)
			{
				if (this.completed)
				{
					return;
				}
				this.completed = true;
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return;
				}
				if (this.asyncOperation.webRequest.IsError())
				{
					this.core.TrySetException(new UnityWebRequestException(this.asyncOperation.webRequest));
					return;
				}
				this.core.TrySetResult(this.asyncOperation.webRequest);
			}

			// Token: 0x040005A5 RID: 1445
			private static TaskPool<UnityAsyncExtensions.UnityWebRequestAsyncOperationConfiguredSource> pool;

			// Token: 0x040005A6 RID: 1446
			private UnityAsyncExtensions.UnityWebRequestAsyncOperationConfiguredSource nextNode;

			// Token: 0x040005A7 RID: 1447
			private UnityWebRequestAsyncOperation asyncOperation;

			// Token: 0x040005A8 RID: 1448
			private IProgress<float> progress;

			// Token: 0x040005A9 RID: 1449
			private CancellationToken cancellationToken;

			// Token: 0x040005AA RID: 1450
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x040005AB RID: 1451
			private bool cancelImmediately;

			// Token: 0x040005AC RID: 1452
			private bool completed;

			// Token: 0x040005AD RID: 1453
			private UniTaskCompletionSourceCore<UnityWebRequest> core;

			// Token: 0x040005AE RID: 1454
			private Action<AsyncOperation> continuationAction;
		}
	}
}
