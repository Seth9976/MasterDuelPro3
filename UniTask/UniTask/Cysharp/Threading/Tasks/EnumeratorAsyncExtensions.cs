using System;
using System.Collections;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks.Sources;
using Cysharp.Threading.Tasks.Internal;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000028 RID: 40
	public static class EnumeratorAsyncExtensions
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00003D80 File Offset: 0x00001F80
		public static UniTask.Awaiter GetAwaiter<T>(this T enumerator) where T : IEnumerator
		{
			T t = enumerator;
			Error.ThrowArgumentNullException<IEnumerator>(t, "enumerator");
			short token;
			return new UniTask(EnumeratorAsyncExtensions.EnumeratorPromise.Create(t, PlayerLoopTiming.Update, CancellationToken.None, out token), token).GetAwaiter();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003DBC File Offset: 0x00001FBC
		public static UniTask WithCancellation(this IEnumerator enumerator, CancellationToken cancellationToken)
		{
			Error.ThrowArgumentNullException<IEnumerator>(enumerator, "enumerator");
			short token;
			return new UniTask(EnumeratorAsyncExtensions.EnumeratorPromise.Create(enumerator, PlayerLoopTiming.Update, cancellationToken, out token), token);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003DE4 File Offset: 0x00001FE4
		public static UniTask ToUniTask(this IEnumerator enumerator, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
		{
			Error.ThrowArgumentNullException<IEnumerator>(enumerator, "enumerator");
			short token;
			return new UniTask(EnumeratorAsyncExtensions.EnumeratorPromise.Create(enumerator, timing, cancellationToken, out token), token);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003E0C File Offset: 0x0000200C
		public static UniTask ToUniTask(this IEnumerator enumerator, MonoBehaviour coroutineRunner)
		{
			AutoResetUniTaskCompletionSource source = AutoResetUniTaskCompletionSource.Create();
			coroutineRunner.StartCoroutine(EnumeratorAsyncExtensions.Core(enumerator, coroutineRunner, source));
			return source.Task;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003E34 File Offset: 0x00002034
		private static IEnumerator Core(IEnumerator inner, MonoBehaviour coroutineRunner, AutoResetUniTaskCompletionSource source)
		{
			yield return coroutineRunner.StartCoroutine(inner);
			source.TrySetResult();
			yield break;
		}

		// Token: 0x02000029 RID: 41
		private sealed class EnumeratorPromise : IUniTaskSource, IValueTaskSource, IPlayerLoopItem, ITaskPoolNode<EnumeratorAsyncExtensions.EnumeratorPromise>
		{
			// Token: 0x1700001A RID: 26
			// (get) Token: 0x060000E1 RID: 225 RVA: 0x00003E51 File Offset: 0x00002051
			public ref EnumeratorAsyncExtensions.EnumeratorPromise NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x060000E2 RID: 226 RVA: 0x00003E59 File Offset: 0x00002059
			static EnumeratorPromise()
			{
				TaskPool.RegisterSizeGetter(typeof(EnumeratorAsyncExtensions.EnumeratorPromise), () => EnumeratorAsyncExtensions.EnumeratorPromise.pool.Size);
			}

			// Token: 0x060000E3 RID: 227 RVA: 0x000020BB File Offset: 0x000002BB
			private EnumeratorPromise()
			{
			}

			// Token: 0x060000E4 RID: 228 RVA: 0x00003E98 File Offset: 0x00002098
			public static IUniTaskSource Create(IEnumerator innerEnumerator, PlayerLoopTiming timing, CancellationToken cancellationToken, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
				}
				EnumeratorAsyncExtensions.EnumeratorPromise result;
				if (!EnumeratorAsyncExtensions.EnumeratorPromise.pool.TryPop(out result))
				{
					result = new EnumeratorAsyncExtensions.EnumeratorPromise();
				}
				result.innerEnumerator = EnumeratorAsyncExtensions.EnumeratorPromise.ConsumeEnumerator(innerEnumerator);
				result.cancellationToken = cancellationToken;
				result.loopRunning = true;
				result.calledGetResult = false;
				result.initialFrame = -1;
				token = result.core.Version;
				if (result.MoveNext())
				{
					PlayerLoopHelper.AddAction(timing, result);
				}
				return result;
			}

			// Token: 0x060000E5 RID: 229 RVA: 0x00003F10 File Offset: 0x00002110
			public void GetResult(short token)
			{
				try
				{
					this.calledGetResult = true;
					this.core.GetResult(token);
				}
				finally
				{
					if (!this.loopRunning)
					{
						this.TryReturn();
					}
				}
			}

			// Token: 0x060000E6 RID: 230 RVA: 0x00003F54 File Offset: 0x00002154
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060000E7 RID: 231 RVA: 0x00003F62 File Offset: 0x00002162
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060000E8 RID: 232 RVA: 0x00003F6F File Offset: 0x0000216F
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060000E9 RID: 233 RVA: 0x00003F80 File Offset: 0x00002180
			public bool MoveNext()
			{
				if (this.calledGetResult)
				{
					this.loopRunning = false;
					this.TryReturn();
					return false;
				}
				if (this.innerEnumerator == null)
				{
					return false;
				}
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.loopRunning = false;
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.initialFrame == -1)
				{
					if (PlayerLoopHelper.IsMainThread)
					{
						this.initialFrame = Time.frameCount;
					}
				}
				else if (this.initialFrame == Time.frameCount)
				{
					return true;
				}
				try
				{
					if (this.innerEnumerator.MoveNext())
					{
						return true;
					}
				}
				catch (Exception ex)
				{
					this.loopRunning = false;
					this.core.TrySetException(ex);
					return false;
				}
				this.loopRunning = false;
				this.core.TrySetResult(null);
				return false;
			}

			// Token: 0x060000EA RID: 234 RVA: 0x00004058 File Offset: 0x00002258
			private bool TryReturn()
			{
				this.core.Reset();
				this.innerEnumerator = null;
				this.cancellationToken = default(CancellationToken);
				return EnumeratorAsyncExtensions.EnumeratorPromise.pool.TryPush(this);
			}

			// Token: 0x060000EB RID: 235 RVA: 0x00004083 File Offset: 0x00002283
			private static IEnumerator ConsumeEnumerator(IEnumerator enumerator)
			{
				while (enumerator.MoveNext())
				{
					object current = enumerator.Current;
					if (current == null)
					{
						yield return null;
					}
					else
					{
						CustomYieldInstruction cyi = current as CustomYieldInstruction;
						if (cyi == null)
						{
							if (current is YieldInstruction)
							{
								IEnumerator innerCoroutine = null;
								AsyncOperation ao = current as AsyncOperation;
								if (ao == null)
								{
									WaitForSeconds wfs = current as WaitForSeconds;
									if (wfs != null)
									{
										innerCoroutine = EnumeratorAsyncExtensions.EnumeratorPromise.UnwrapWaitForSeconds(wfs);
									}
								}
								else
								{
									innerCoroutine = EnumeratorAsyncExtensions.EnumeratorPromise.UnwrapWaitAsyncOperation(ao);
								}
								if (innerCoroutine != null)
								{
									while (innerCoroutine.MoveNext())
									{
										yield return null;
									}
									innerCoroutine = null;
									goto IL_0159;
								}
							}
							else
							{
								IEnumerator e3 = current as IEnumerator;
								if (e3 != null)
								{
									IEnumerator innerCoroutine = EnumeratorAsyncExtensions.EnumeratorPromise.ConsumeEnumerator(e3);
									while (innerCoroutine.MoveNext())
									{
										yield return null;
									}
									innerCoroutine = null;
									goto IL_0159;
								}
							}
							Debug.LogWarning("yield " + current.GetType().Name + " is not supported on await IEnumerator or IEnumerator.ToUniTask(), please use ToUniTask(MonoBehaviour coroutineRunner) instead.");
							yield return null;
							continue;
						}
						while (cyi.keepWaiting)
						{
							yield return null;
						}
						IL_0159:
						cyi = null;
					}
				}
				yield break;
			}

			// Token: 0x060000EC RID: 236 RVA: 0x00004092 File Offset: 0x00002292
			private static IEnumerator UnwrapWaitForSeconds(WaitForSeconds waitForSeconds)
			{
				float second = (float)EnumeratorAsyncExtensions.EnumeratorPromise.waitForSeconds_Seconds.GetValue(waitForSeconds);
				float elapsed = 0f;
				do
				{
					yield return null;
					elapsed += Time.deltaTime;
				}
				while (elapsed < second);
				yield break;
			}

			// Token: 0x060000ED RID: 237 RVA: 0x000040A1 File Offset: 0x000022A1
			private static IEnumerator UnwrapWaitAsyncOperation(AsyncOperation asyncOperation)
			{
				while (!asyncOperation.isDone)
				{
					yield return null;
				}
				yield break;
			}

			// Token: 0x04000075 RID: 117
			private static TaskPool<EnumeratorAsyncExtensions.EnumeratorPromise> pool;

			// Token: 0x04000076 RID: 118
			private EnumeratorAsyncExtensions.EnumeratorPromise nextNode;

			// Token: 0x04000077 RID: 119
			private IEnumerator innerEnumerator;

			// Token: 0x04000078 RID: 120
			private CancellationToken cancellationToken;

			// Token: 0x04000079 RID: 121
			private int initialFrame;

			// Token: 0x0400007A RID: 122
			private bool loopRunning;

			// Token: 0x0400007B RID: 123
			private bool calledGetResult;

			// Token: 0x0400007C RID: 124
			private UniTaskCompletionSourceCore<object> core;

			// Token: 0x0400007D RID: 125
			private static readonly FieldInfo waitForSeconds_Seconds = typeof(WaitForSeconds).GetField("m_Seconds", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);
		}
	}
}
