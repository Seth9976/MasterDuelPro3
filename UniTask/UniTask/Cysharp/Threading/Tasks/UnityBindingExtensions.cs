using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000185 RID: 389
	public static class UnityBindingExtensions
	{
		// Token: 0x06000944 RID: 2372 RVA: 0x00029390 File Offset: 0x00027590
		public static void BindTo(this IUniTaskAsyncEnumerable<string> source, Text text, bool rebindOnError = true)
		{
			UnityBindingExtensions.BindToCore(source, text, text.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000293B4 File Offset: 0x000275B4
		public static void BindTo(this IUniTaskAsyncEnumerable<string> source, Text text, CancellationToken cancellationToken, bool rebindOnError = true)
		{
			UnityBindingExtensions.BindToCore(source, text, cancellationToken, rebindOnError).Forget();
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x000293D4 File Offset: 0x000275D4
		private static async UniTaskVoid BindToCore(IUniTaskAsyncEnumerable<string> source, Text text, CancellationToken cancellationToken, bool rebindOnError)
		{
			bool repeat = false;
			object obj2;
			int num2;
			for (;;)
			{
				IUniTaskAsyncEnumerator<string> e = source.GetAsyncEnumerator(cancellationToken);
				object obj = null;
				int num = 0;
				try
				{
					for (;;)
					{
						bool moveNext;
						try
						{
							moveNext = await e.MoveNextAsync();
							repeat = false;
						}
						catch (Exception ex)
						{
							if (ex is OperationCanceledException)
							{
								goto IL_00FC;
							}
							if (rebindOnError && !repeat)
							{
								repeat = true;
								break;
							}
							throw;
						}
						if (!moveNext)
						{
							goto IL_00FC;
						}
						text.text = e.Current;
					}
					num = 1;
					goto IL_0111;
					IL_00FC:
					num = 2;
				}
				catch (object obj)
				{
				}
				IL_0111:
				if (e != null)
				{
					await e.DisposeAsync();
				}
				obj2 = obj;
				if (obj2 != null)
				{
					Exception ex2 = obj2 as Exception;
					if (ex2 == null)
					{
						break;
					}
					ExceptionDispatchInfo.Capture(ex2).Throw();
				}
				num2 = num;
				if (num2 != 1)
				{
					goto Block_5;
				}
			}
			throw obj2;
			Block_5:
			if (num2 != 2)
			{
				object obj = null;
				IUniTaskAsyncEnumerator<string> e = null;
			}
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00029430 File Offset: 0x00027630
		public static void BindTo<T>(this IUniTaskAsyncEnumerable<T> source, Text text, bool rebindOnError = true)
		{
			UnityBindingExtensions.BindToCore<T>(source, text, text.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00029454 File Offset: 0x00027654
		public static void BindTo<T>(this IUniTaskAsyncEnumerable<T> source, Text text, CancellationToken cancellationToken, bool rebindOnError = true)
		{
			UnityBindingExtensions.BindToCore<T>(source, text, cancellationToken, rebindOnError).Forget();
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00029474 File Offset: 0x00027674
		public static void BindTo<T>(this AsyncReactiveProperty<T> source, Text text, bool rebindOnError = true)
		{
			UnityBindingExtensions.BindToCore<T>(source, text, text.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00029498 File Offset: 0x00027698
		private static async UniTaskVoid BindToCore<T>(IUniTaskAsyncEnumerable<T> source, Text text, CancellationToken cancellationToken, bool rebindOnError)
		{
			bool repeat = false;
			object obj2;
			int num2;
			for (;;)
			{
				IUniTaskAsyncEnumerator<T> e = source.GetAsyncEnumerator(cancellationToken);
				object obj = null;
				int num = 0;
				try
				{
					for (;;)
					{
						bool moveNext;
						try
						{
							moveNext = await e.MoveNextAsync();
							repeat = false;
						}
						catch (Exception ex)
						{
							if (ex is OperationCanceledException)
							{
								goto IL_010B;
							}
							if (rebindOnError && !repeat)
							{
								repeat = true;
								break;
							}
							throw;
						}
						if (!moveNext)
						{
							goto IL_010B;
						}
						T t = e.Current;
						text.text = t.ToString();
					}
					num = 1;
					goto IL_0120;
					IL_010B:
					num = 2;
				}
				catch (object obj)
				{
				}
				IL_0120:
				if (e != null)
				{
					await e.DisposeAsync();
				}
				obj2 = obj;
				if (obj2 != null)
				{
					Exception ex2 = obj2 as Exception;
					if (ex2 == null)
					{
						break;
					}
					ExceptionDispatchInfo.Capture(ex2).Throw();
				}
				num2 = num;
				if (num2 != 1)
				{
					goto Block_5;
				}
			}
			throw obj2;
			Block_5:
			if (num2 != 2)
			{
				object obj = null;
				IUniTaskAsyncEnumerator<T> e = null;
			}
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x000294F4 File Offset: 0x000276F4
		public static void BindTo(this IUniTaskAsyncEnumerable<bool> source, Selectable selectable, bool rebindOnError = true)
		{
			UnityBindingExtensions.BindToCore(source, selectable, selectable.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00029518 File Offset: 0x00027718
		public static void BindTo(this IUniTaskAsyncEnumerable<bool> source, Selectable selectable, CancellationToken cancellationToken, bool rebindOnError = true)
		{
			UnityBindingExtensions.BindToCore(source, selectable, cancellationToken, rebindOnError).Forget();
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00029538 File Offset: 0x00027738
		private static async UniTaskVoid BindToCore(IUniTaskAsyncEnumerable<bool> source, Selectable selectable, CancellationToken cancellationToken, bool rebindOnError)
		{
			bool repeat = false;
			object obj2;
			int num2;
			for (;;)
			{
				IUniTaskAsyncEnumerator<bool> e = source.GetAsyncEnumerator(cancellationToken);
				object obj = null;
				int num = 0;
				try
				{
					for (;;)
					{
						bool moveNext;
						try
						{
							moveNext = await e.MoveNextAsync();
							repeat = false;
						}
						catch (Exception ex)
						{
							if (ex is OperationCanceledException)
							{
								goto IL_00FC;
							}
							if (rebindOnError && !repeat)
							{
								repeat = true;
								break;
							}
							throw;
						}
						if (!moveNext)
						{
							goto IL_00FC;
						}
						selectable.interactable = e.Current;
					}
					num = 1;
					goto IL_0111;
					IL_00FC:
					num = 2;
				}
				catch (object obj)
				{
				}
				IL_0111:
				if (e != null)
				{
					await e.DisposeAsync();
				}
				obj2 = obj;
				if (obj2 != null)
				{
					Exception ex2 = obj2 as Exception;
					if (ex2 == null)
					{
						break;
					}
					ExceptionDispatchInfo.Capture(ex2).Throw();
				}
				num2 = num;
				if (num2 != 1)
				{
					goto Block_5;
				}
			}
			throw obj2;
			Block_5:
			if (num2 != 2)
			{
				object obj = null;
				IUniTaskAsyncEnumerator<bool> e = null;
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00029594 File Offset: 0x00027794
		public static void BindTo<TSource, TObject>(this IUniTaskAsyncEnumerable<TSource> source, TObject monoBehaviour, Action<TObject, TSource> bindAction, bool rebindOnError = true) where TObject : MonoBehaviour
		{
			UnityBindingExtensions.BindToCore<TSource, TObject>(source, monoBehaviour, bindAction, monoBehaviour.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x000295C0 File Offset: 0x000277C0
		public static void BindTo<TSource, TObject>(this IUniTaskAsyncEnumerable<TSource> source, TObject bindTarget, Action<TObject, TSource> bindAction, CancellationToken cancellationToken, bool rebindOnError = true)
		{
			UnityBindingExtensions.BindToCore<TSource, TObject>(source, bindTarget, bindAction, cancellationToken, rebindOnError).Forget();
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x000295E0 File Offset: 0x000277E0
		private static async UniTaskVoid BindToCore<TSource, TObject>(IUniTaskAsyncEnumerable<TSource> source, TObject bindTarget, Action<TObject, TSource> bindAction, CancellationToken cancellationToken, bool rebindOnError)
		{
			bool repeat = false;
			object obj2;
			int num2;
			for (;;)
			{
				IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
				object obj = null;
				int num = 0;
				try
				{
					for (;;)
					{
						bool moveNext;
						try
						{
							moveNext = await e.MoveNextAsync();
							repeat = false;
						}
						catch (Exception ex)
						{
							if (ex is OperationCanceledException)
							{
								goto IL_0102;
							}
							if (rebindOnError && !repeat)
							{
								repeat = true;
								break;
							}
							throw;
						}
						if (!moveNext)
						{
							goto IL_0102;
						}
						bindAction(bindTarget, e.Current);
					}
					num = 1;
					goto IL_0117;
					IL_0102:
					num = 2;
				}
				catch (object obj)
				{
				}
				IL_0117:
				if (e != null)
				{
					await e.DisposeAsync();
				}
				obj2 = obj;
				if (obj2 != null)
				{
					Exception ex2 = obj2 as Exception;
					if (ex2 == null)
					{
						break;
					}
					ExceptionDispatchInfo.Capture(ex2).Throw();
				}
				num2 = num;
				if (num2 != 1)
				{
					goto Block_5;
				}
			}
			throw obj2;
			Block_5:
			if (num2 != 2)
			{
				object obj = null;
				IUniTaskAsyncEnumerator<TSource> e = null;
			}
		}
	}
}
